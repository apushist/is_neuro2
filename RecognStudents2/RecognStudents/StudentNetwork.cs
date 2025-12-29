using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace AForge.WindowsForms
{
    public class StudentNetwork : BaseNetwork
    {
		private int[] structure;
		private double[][] neurons;
		private double[][][] weights;
		private double[][] biases;
		private double learningRate = 0.1;
		private Random random;

		public Stopwatch stopWatch = new Stopwatch();

		public StudentNetwork(SamplesSet data)
		{
			int[] structure = { 588, 400, 150, 10 };

			this.structure = structure;
			neurons = new double[structure.Length][];
			weights = new double[structure.Length - 1][][];
			biases = new double[structure.Length - 1][];
			random = new Random();

			for (int i = 0; i < structure.Length; i++)
				neurons[i] = new double[structure[i]];

			for (int i = 0; i < structure.Length - 1; i++)
			{
				int currentLength = structure[i];
				int nextLength = structure[i + 1];

				weights[i] = new double[nextLength][];
				biases[i] = new double[nextLength];

				for (int j = 0; j < nextLength; j++)
				{
					weights[i][j] = new double[currentLength];
					for (int k = 0; k < currentLength; k++)
						weights[i][j][k] = random.NextDouble() * 2 - 1;
					biases[i][j] = random.NextDouble() * 2 - 1;
				}
			}

		}

		protected override double[] Compute(double[] input)
        {
			Array.Copy(input, neurons[0], input.Length);

			for (int layer = 0; layer < weights.Length; layer++)
			{
				Parallel.For(0, neurons[layer + 1].Length, neuron =>
				{
					double sum = biases[layer][neuron];

					for (int prevNeuron = 0; prevNeuron < neurons[layer].Length; prevNeuron++)
					{
						sum += neurons[layer][prevNeuron] * weights[layer][neuron][prevNeuron];
					}
					neurons[layer + 1][neuron] = Sigmoid(sum);
				});
			}

			return neurons[neurons.Length - 1];
		}


		private double Sigmoid(double x)
		{
			return 1.0 / (1.0 + System.Math.Exp(-x));
		}

		private double SigmoidDerivative(double x) // x - sigmoid(x)
		{
			return x * (1.0 - x);
		}

		private double TrainSample(double[] inputs, double[] expectedOutputs)
		{
			var outputs = Compute(inputs);

			double totalError = 0;
			double[][] errors = new double[structure.Length][];
			for (int i = 0; i < structure.Length; i++)
				errors[i] = new double[structure[i]];

			Parallel.For(0, expectedOutputs.Length, i =>
			{
				errors[errors.Length - 1][i] = expectedOutputs[i] - outputs[i];
				totalError += System.Math.Pow(errors[errors.Length - 1][i], 2);
			});
			totalError /= 2;

			for (int layer = weights.Length - 1; layer >= 0; layer--)
			{
				Parallel.For(0, weights[layer].Length, neuron =>
				{
					double delta = 0;

					if (layer == weights.Length - 1)
						delta = (expectedOutputs[neuron] - neurons[layer + 1][neuron]) * SigmoidDerivative(neurons[layer + 1][neuron]);
					else
						delta = errors[layer + 1][neuron] * SigmoidDerivative(neurons[layer + 1][neuron]);

					for (int prevNeuron = 0; prevNeuron < neurons[layer].Length; prevNeuron++)
					{
						weights[layer][neuron][prevNeuron] += learningRate * delta * neurons[layer][prevNeuron];
						errors[layer][prevNeuron] += delta * weights[layer][neuron][prevNeuron];
					}

					biases[layer][neuron] += learningRate * delta;
				});
			}

			return totalError;
		}

		public override double TrainOnDataSet(SamplesSet samplesSet, int epochsCount, double acceptableError)
		{
			double error = 0;

			stopWatch.Restart();

			for (int i = 0; i < epochsCount; i++)
			{
				error = 0;

				foreach (Sample sample in samplesSet.samples)
					error += TrainSample(sample.input, sample.Output);

				error /= samplesSet.Count;

				if (error <= acceptableError)
					break;

				OnTrainProgress((double)i / epochsCount, error, stopWatch.Elapsed);
			}

			OnTrainProgress(1.0, error, stopWatch.Elapsed);

			stopWatch.Stop();

			return error;
		}

		public override void SaveToBin()
		{
			using (var writer = new BinaryWriter(File.Open(DataLoader.GetProjectRoot() + "\\networks\\studentNet.bin", FileMode.Create)))
			{
				writer.Write(structure.Length);
				foreach (var layerSize in structure)
					writer.Write(layerSize);
				
				writer.Write(neurons.Length);
				foreach (var layer in neurons)
				{
					writer.Write(layer.Length);
					foreach (var neuron in layer)
						writer.Write(neuron);
				}

				writer.Write(weights.Length);
				foreach (var layerWeights in weights)
				{
					writer.Write(layerWeights.Length);
					foreach (var neuronWeights in layerWeights)
					{
						writer.Write(neuronWeights.Length);
						foreach (var weight in neuronWeights)
							writer.Write(weight);
					}
				}

				writer.Write(biases.Length);
				foreach (var layerBiases in biases)
				{
					writer.Write(layerBiases.Length);
					foreach (var bias in layerBiases)
						writer.Write(bias);
				}
			}
		}

		public override void LoadFromBin()
		{
			using (var reader = new BinaryReader(File.Open(DataLoader.GetProjectRoot() + "\\networks\\studentNet0.bin", FileMode.Open)))
			{
				int structureLength = reader.ReadInt32();
				structure = new int[structureLength];
				for (int i = 0; i < structureLength; i++)
					structure[i] = reader.ReadInt32();

				int neuronsLength = reader.ReadInt32();
				neurons = new double[neuronsLength][];
				for (int i = 0; i < neuronsLength; i++)
				{
					int layerSize = reader.ReadInt32();
					neurons[i] = new double[layerSize];
					for (int j = 0; j < layerSize; j++)
						neurons[i][j] = reader.ReadDouble();
				}

				int weightsLength = reader.ReadInt32();
				weights = new double[weightsLength][][];
				for (int i = 0; i < weightsLength; i++)
				{
					int layerLength = reader.ReadInt32();
					weights[i] = new double[layerLength][];
					for (int j = 0; j < layerLength; j++)
					{
						int neuronWeightsLength = reader.ReadInt32();
						weights[i][j] = new double[neuronWeightsLength];
						for (int k = 0; k < neuronWeightsLength; k++)
							weights[i][j][k] = reader.ReadDouble();
					}
				}

				int biasesLength = reader.ReadInt32();
				biases = new double[biasesLength][];
				for (int i = 0; i < biasesLength; i++)
				{
					int layerBiasesLength = reader.ReadInt32();
					biases[i] = new double[layerBiasesLength];
					for (int j = 0; j < layerBiasesLength; j++)
						biases[i][j] = reader.ReadDouble();
				}
			}
		}

	}
}