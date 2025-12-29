using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AForge.WindowsForms
{
	public class DataLoader
	{
		private static readonly Dictionary<string, FigureType> ClassIdentifierMap = new Dictionary<string, FigureType>
		{
			{"audi", FigureType.Audi},
			{"citroen", FigureType.Citroen},
			{"ford", FigureType.Ford},
			{"hyundai", FigureType.Hyundai},
			{"infiniti", FigureType.Infiniti},
			{"mercedes", FigureType.Mercedes},
			{"MM", FigureType.Mitsubishi},
			{"opel", FigureType.Opel},
			{"renault", FigureType.Renault},
			{"toyota", FigureType.Toyota}
		};

		public static string GetProjectRoot()
		{
			string currentDir = Directory.GetCurrentDirectory();

			DirectoryInfo dir = new DirectoryInfo(currentDir);

			while (dir != null)
			{
				var csprojFiles = dir.GetFiles("*.csproj", SearchOption.TopDirectoryOnly);
				var slnFiles = dir.GetFiles("*.sln", SearchOption.TopDirectoryOnly);

				if (csprojFiles.Length > 0 || slnFiles.Length > 0)
				{
					return dir.FullName;
				}

				var srcDir = dir.GetDirectories("src", SearchOption.TopDirectoryOnly);
				var propertiesDir = dir.GetDirectories("Properties", SearchOption.TopDirectoryOnly);

				if (propertiesDir.Length > 0 || srcDir.Length > 0)
				{
					return dir.FullName;
				}

				dir = dir.Parent;
			}

			return Directory.GetCurrentDirectory();
		}

		/// <summary>
		/// Создает SamplesSet из файлов изображений в указанной директории
		/// </summary>
		/// <returns>SamplesSet с загруженными образцами</returns>
		public static SamplesSet CreateFromImageFiles()
		{
			string directoryPath = GetProjectRoot() + "\\dataset_autos\\output_images";
			
			var samplesSet = new SamplesSet();

			if (!Directory.Exists(directoryPath))
			{
				throw new DirectoryNotFoundException($"Директория не найдена: {directoryPath}");
			}

			var imageFiles = Directory.GetFiles(directoryPath, "*.png");

			if (imageFiles.Length == 0)
			{
				throw new FileNotFoundException($"В директории {directoryPath} не найдено PNG файлов");
			}

			Console.WriteLine($"Найдено {imageFiles.Length} изображений для обработки...");

			var samples = new System.Collections.Concurrent.ConcurrentBag<Sample>();

			System.Threading.Tasks.Parallel.ForEach(imageFiles, filePath =>
			{
				try
				{
					var sample = CreateSampleFromImageFile(filePath);
					if (sample != null)
					{
						samples.Add(sample);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Ошибка при обработке файла {Path.GetFileName(filePath)}: {ex.Message}");
				}
			});

			Random rng = new Random();
			var shuffledSamples = samples.ToList().OrderBy(_ => rng.Next()).ToList();

			foreach (var sample in shuffledSamples)
			{
				samplesSet.AddSample(sample);
			}

			Console.WriteLine($"Успешно загружено {samplesSet.Count} образцов");
			return samplesSet;
		}

		/// <summary>
		/// Создает Sample из одного файла изображения
		/// </summary>
		/// <param name="filePath">Путь к файлу изображения</param>
		/// <returns>Созданный Sample или null в случае ошибки</returns>
		private static Sample CreateSampleFromImageFile(string filePath)
		{
			int classesCount = 10;
			var fileName = Path.GetFileNameWithoutExtension(filePath);

			var parts = fileName.Split('_');
			if (parts.Length < 1)
			{
				Console.WriteLine($"Некорректное имя файла: {fileName}");
				return null;
			}

			string classId = parts[0];
			int firstDigitIndex = -1;
			for (int i = 0; i < classId.Length; i++)
			{
				if (char.IsDigit(classId[i]))
				{
					firstDigitIndex = i;
					break;
				}
			}
			string classIdentifier = classId.Substring(0, firstDigitIndex);

			if (!ClassIdentifierMap.TryGetValue(classIdentifier, out var figureType))
			{
				Console.WriteLine($"Неизвестный идентификатор класса: {classIdentifier}");
				return null;
				
			}

			var bitmap = new Bitmap(filePath);

			return ConvertToSample(bitmap, classesCount, figureType);
		}

		public static Sample ConvertToSample(Bitmap bitmap,  int classesCount, FigureType figureType)
		{

			if (bitmap.Width != 100 || bitmap.Height != 100)
			{
				throw new ArgumentException($"Изображение должно быть 100x100 пикселей, а не {bitmap.Width}x{bitmap.Height}");
			}
			double[][] image = new double[100][];

			for (int y = 0; y < 100; y++)
			{
				image[y] = new double[100];
				for (int x = 0; x < 100; x++)
				{
					var pixel = bitmap.GetPixel(x, y);
					double value = (pixel.R + pixel.G + pixel.B) / (3.0 * 255.0);

					// Инвертируем чтобы белый был 0.0, черный - 1.0
					if (value > 0.4)
						value = 0;
					else
						value = 1.0;
					image[y][x] = value;
				}
			}


			double[][,] kernels = new double[][,]
			{
				new double[,] {
					{ -1, -1, -1 },
					{ 0, 0, 0 },
					{ 1, 1, 1 }
				},
        
				new double[,] {
					{ -1, 0, 1 },
					{ -1, 0, 1 },
					{ -1, 0, 1 }
				},
        
				new double[,] {
					{ -1, -1, 0 },
					{ -1, 0, 1 },
					{ 0, 1, 1 }
				}
			};

			int outputSize = 98;
			int poolFactor = 7; 
			int pooledSize = outputSize / poolFactor; 

			double[] inputValues = new double[3 * pooledSize * pooledSize];

			for (int k = 0; k < kernels.Length; k++)
			{
				double[,] kernel = kernels[k];

				for (int py = 0; py < pooledSize; py++)
				{
					for (int px = 0; px < pooledSize; px++)
					{
						double sum = 0;

						for (int dy = 0; dy < poolFactor; dy++)
						{
							for (int dx = 0; dx < poolFactor; dx++)
							{
								int origY = py * poolFactor + dy;
								int origX = px * poolFactor + dx;

								double convValue = 0;
								for (int ky = 0; ky < 3; ky++)
								{
									for (int kx = 0; kx < 3; kx++)
									{
										convValue += image[origY + ky][origX + kx] * kernel[ky, kx];
									}
								}

								sum += System.Math.Abs(convValue); 
							}
						}

						sum /= (poolFactor * poolFactor);

						int index = k * (pooledSize * pooledSize) + py * pooledSize + px;
						inputValues[index] = sum;
					}
				}
			}

			return new Sample(inputValues, classesCount, figureType);
		}
	}
}
