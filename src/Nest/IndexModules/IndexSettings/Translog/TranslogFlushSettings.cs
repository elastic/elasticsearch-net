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
		TimeUnitExpression ThresholdPeriod { get; set; }

		/// <summary>
		/// How often to check if a flush is needed, randomized between 
		/// the interval value and 2x the interval value. Defaults to 5s.
		/// </summary>
		TimeUnitExpression Interval { get; set; }
	}

	public class TranslogFlushSettings : ITranslogFlushSettings
	{
		/// <inheritdoc/>
		public TimeUnitExpression Interval { get; set; }
		/// <inheritdoc/>
		public int? ThresholdOps { get; set; }
		/// <inheritdoc/>
		public TimeUnitExpression ThresholdPeriod { get; set; }
		/// <inheritdoc/>
		public string ThresholdSize { get; set; }
	}

	public class TranslogFlushSettingsDescriptor : ITranslogFlushSettings
	{
		protected TranslogFlushSettingsDescriptor Assign(Action<ITranslogFlushSettings> assigner) => 
			Fluent.Assign(this, assigner);

		TimeUnitExpression ITranslogFlushSettings.Interval { get; set; }
		int? ITranslogFlushSettings.ThresholdOps { get; set; }
		TimeUnitExpression ITranslogFlushSettings.ThresholdPeriod { get; set; }
		string ITranslogFlushSettings.ThresholdSize { get; set; }

		/// <inheritdoc/>
		public TranslogFlushSettingsDescriptor ThresholdSize(string size) => Assign(a => a.ThresholdSize = size);

		/// <inheritdoc/>
		public TranslogFlushSettingsDescriptor ThresholdOps(int? operations) => Assign(a => a.ThresholdOps = operations);

		/// <inheritdoc/>
		public TranslogFlushSettingsDescriptor ThresholdPeriod(TimeUnitExpression time) => 
			Assign(a => a.ThresholdPeriod = time);

		/// <inheritdoc/>
		public TranslogFlushSettingsDescriptor Interval(TimeUnitExpression time) => Assign(a => a.Interval = time);
	}
}