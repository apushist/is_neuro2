using System;
using System.IO;

namespace AForge.WindowsForms
{

	public delegate void TrainProgressHandler(double progress, double error, TimeSpan time);
    /// <summary>
    /// Базовый класс для реализации как самодельного персептрона, так и обёртки для ActivationNetwork из Accord.Net
    /// </summary>
    public abstract class BaseNetwork
    {

		// Событие обновления прогресса обучения (форма подписывается для того чтобы знать о том, сколько процентов работы сделано, и обновлять прогрессбар)
		public event TrainProgressHandler TrainProgress;

        /// <summary>
        /// Обучение сети на основе датасета
        /// </summary>
        /// <param name="samplesSet">Обучающая выборка</param>
        /// <param name="epochsCount">Максимальное количество эпох обучения</param>
        /// <param name="acceptableError">Желаемый уровень ошибки</param>
        /// <param name="parallel">Распараллеливать ли обучение</param>
        /// <returns></returns>
        public abstract double TrainOnDataSet(SamplesSet samplesSet, int epochsCount, double acceptableError);

        /// <summary>
        /// Подсчёт результата работы сети на данном входе.
        /// </summary>
        /// <param name="input">Входные данные для первого слоя</param>
        /// <returns></returns>
        protected abstract double[] Compute(double[] input);

        /// <summary>
        /// Угадывает тип фигуры на основе результатов подсчётов сети.
        /// </summary>
        /// <param name="sample">Фигура, которую необходимо определить</param>
        /// <returns></returns>
        public FigureType Predict(Sample sample)
        {
            return sample.ProcessPrediction(Compute(sample.input));
        }

        /// <summary>
        /// Обёртка над событием для оповещения подписчиков
        /// </summary>
        /// <param name="progress">Приблизительная оценка прогресса от 0 до 1</param>
        /// <param name="error">Текущая ошибка</param>
        /// <param name="time">Сколько времени прошло с начала обучения</param>
        protected virtual void OnTrainProgress(double progress, double error, TimeSpan time)
        {
            TrainProgress?.Invoke(progress, error, time);
        }

        public abstract void SaveToBin();

		public abstract void LoadFromBin();
	}
}
