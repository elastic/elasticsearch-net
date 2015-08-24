using System;

namespace Nest
{
	public interface ITranslogFlushSettings
	{
		/// <summary>
		/// Once the translog hits this size, a flush will happen. Defaults to 512mb.
		/// </summary>
		string TresholdSize { get; set; }

		/// <summary>
		/// After how many operations to flush. Defaults to unlimited.
		/// </summary>
		int? TresholdOps { get; set; }

		/// <summary>
		/// How long to wait before triggering a flush regardless of translog size. Defaults to 30m.
		/// </summary>
		TimeUnitExpression TresholdPeriod { get; set; }

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
		public int? TresholdOps { get; set; }
		/// <inheritdoc/>
		public TimeUnitExpression TresholdPeriod { get; set; }
		/// <inheritdoc/>
		public string TresholdSize { get; set; }
	}

	public class TranslogFlushSettingsDescriptor : ITranslogFlushSettings
	{
		protected TranslogFlushSettingsDescriptor Assign(Action<ITranslogFlushSettings> assigner) => 
			Fluent.Assign(this, assigner);

		TimeUnitExpression ITranslogFlushSettings.Interval { get; set; }
		int? ITranslogFlushSettings.TresholdOps { get; set; }
		TimeUnitExpression ITranslogFlushSettings.TresholdPeriod { get; set; }
		string ITranslogFlushSettings.TresholdSize { get; set; }

		/// <inheritdoc/>
		public TranslogFlushSettingsDescriptor TresholdSize(string size) => Assign(a => a.TresholdSize = size);

		/// <inheritdoc/>
		public TranslogFlushSettingsDescriptor TresholdOps(int? operations) => Assign(a => a.TresholdOps = operations);

		/// <inheritdoc/>
		public TranslogFlushSettingsDescriptor TresholdPeriod(TimeUnitExpression time) => 
			Assign(a => a.TresholdPeriod = time);

		/// <inheritdoc/>
		public TranslogFlushSettingsDescriptor Interval(TimeUnitExpression time) => Assign(a => a.Interval = time);
	}
}