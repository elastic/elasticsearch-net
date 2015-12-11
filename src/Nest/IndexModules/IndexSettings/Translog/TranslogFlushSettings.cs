using System;

namespace Nest
{
	public interface ITranslogFlushSettings
	{
		/// <summary>
		/// Once the translog hits this size, a flush will happen. Defaults to 512mb.
		/// </summary>
		string ThresholdSize { get; set; }

		/// <summary>
		/// After how many operations to flush. Defaults to unlimited.
		/// </summary>
		int? ThresholdOps { get; set; }

		/// <summary>
		/// How long to wait before triggering a flush regardless of translog size. Defaults to 30m.
		/// </summary>
		TimeUnit ThresholdPeriod { get; set; }

		/// <summary>
		/// How often to check if a flush is needed, randomized between 
		/// the interval value and 2x the interval value. Defaults to 5s.
		/// </summary>
		TimeUnit Interval { get; set; }
	}

	public class TranslogFlushSettings : ITranslogFlushSettings
	{
		/// <inheritdoc/>
		public TimeUnit Interval { get; set; }
		/// <inheritdoc/>
		public int? ThresholdOps { get; set; }
		/// <inheritdoc/>
		public TimeUnit ThresholdPeriod { get; set; }
		/// <inheritdoc/>
		public string ThresholdSize { get; set; }
	}

	public class TranslogFlushSettingsDescriptor: DescriptorBase<TranslogFlushSettingsDescriptor, ITranslogFlushSettings>, ITranslogFlushSettings
	{
		TimeUnit ITranslogFlushSettings.Interval { get; set; }
		int? ITranslogFlushSettings.ThresholdOps { get; set; }
		TimeUnit ITranslogFlushSettings.ThresholdPeriod { get; set; }
		string ITranslogFlushSettings.ThresholdSize { get; set; }

		/// <inheritdoc/>
		public TranslogFlushSettingsDescriptor ThresholdSize(string size) => Assign(a => a.ThresholdSize = size);

		/// <inheritdoc/>
		public TranslogFlushSettingsDescriptor ThresholdOps(int? operations) => Assign(a => a.ThresholdOps = operations);

		/// <inheritdoc/>
		public TranslogFlushSettingsDescriptor ThresholdPeriod(TimeUnit time) => 
			Assign(a => a.ThresholdPeriod = time);

		/// <inheritdoc/>
		public TranslogFlushSettingsDescriptor Interval(TimeUnit time) => Assign(a => a.Interval = time);
	}
}