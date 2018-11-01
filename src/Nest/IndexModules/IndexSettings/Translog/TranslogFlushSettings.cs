namespace Nest
{
	public interface ITranslogFlushSettings
	{
		/// <summary>
		///     How often to check if a flush is needed, randomized between
		///     the interval value and 2x the interval value. Defaults to 5s.
		/// </summary>
		Time Interval { get; set; }

		/// <summary>
		///     How long to wait before triggering a flush regardless of translog size. Defaults to 30m.
		/// </summary>
		Time ThresholdPeriod { get; set; }

		/// <summary>
		///     Once the translog hits this size, a flush will happen. Defaults to 512mb.
		/// </summary>
		string ThresholdSize { get; set; }
	}

	public class TranslogFlushSettings : ITranslogFlushSettings
	{
		/// <inheritdoc />
		public Time Interval { get; set; }

		/// <inheritdoc />
		public Time ThresholdPeriod { get; set; }

		/// <inheritdoc />
		public string ThresholdSize { get; set; }
	}

	public class TranslogFlushSettingsDescriptor : DescriptorBase<TranslogFlushSettingsDescriptor, ITranslogFlushSettings>, ITranslogFlushSettings
	{
		Time ITranslogFlushSettings.Interval { get; set; }
		Time ITranslogFlushSettings.ThresholdPeriod { get; set; }
		string ITranslogFlushSettings.ThresholdSize { get; set; }

		/// <inheritdoc />
		public TranslogFlushSettingsDescriptor Interval(Time time) => Assign(a => a.Interval = time);

		/// <inheritdoc />
		public TranslogFlushSettingsDescriptor ThresholdPeriod(Time time) =>
			Assign(a => a.ThresholdPeriod = time);

		/// <inheritdoc />
		public TranslogFlushSettingsDescriptor ThresholdSize(string size) => Assign(a => a.ThresholdSize = size);
	}
}
