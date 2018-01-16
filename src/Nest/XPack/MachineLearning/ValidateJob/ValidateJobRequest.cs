using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Validates a machine learning job
	/// </summary>
	public partial interface IValidateJobRequest
	{
		/// <summary>
		/// The analysis configuration, which specifies how to analyze the data.
		/// </summary>
		[JsonProperty("analysis_config")]
		IAnalysisConfig AnalysisConfig { get; set; }

		/// <summary>
		/// Optionally specifies runtime limits for the job.
		/// </summary>
		[JsonProperty("analysis_limits")]
		IAnalysisLimits AnalysisLimits { get; set; }

		/// <summary>
		/// Describes the format of the input data. This object is required, but it can be empty.
		/// </summary>
		[JsonProperty("data_description")]
		IDataDescription DataDescription { get; set; }

		/// <summary>
		/// An optional description of the job
		/// </summary>
		[JsonProperty("description")]
		string Description { get; set; }

		/// <summary>
		/// This advanced configuration option stores model information along with the results.
		/// This adds overhead to the performance of the system and is not feasible for jobs with many entities
		/// </summary>
		[JsonProperty("model_plot")]
		IModelPlotConfig ModelPlotConfig { get; set; }

		/// <summary>
		/// The time in days that model snapshots are retained for the job.
		/// Older snapshots are deleted. The default value is 1 day.
		/// </summary>
		[JsonProperty("model_snapshot_retention_days")]
		long? ModelSnapshotRetentionDays { get; set; }

		/// <summary>
		/// The name of the index in which to store the machine learning results.
		/// The default value is shared, which corresponds to the index name .ml-anomalies-shared.
		/// </summary>
		[JsonProperty("results_index_name")]
		IndexName ResultsIndexName { get; set; }
	}

	/// <inheritdoc />
	public partial class ValidateJobRequest
	{
		/// <inheritdoc />
		public IAnalysisConfig AnalysisConfig { get; set; }
		/// <inheritdoc />
		public IAnalysisLimits AnalysisLimits { get; set; }
		/// <inheritdoc />
		public IDataDescription DataDescription { get; set; }
		/// <inheritdoc />
		public string Description { get; set; }
		/// <inheritdoc />
		public IModelPlotConfig ModelPlotConfig { get; set; }
		/// <inheritdoc />
		public long? ModelSnapshotRetentionDays { get; set; }
		/// <inheritdoc />
		public IndexName ResultsIndexName { get; set; }
	}

	/// <inheritdoc />
	[DescriptorFor("XpackMlValidate")]
	public partial class ValidateJobDescriptor<T> where T : class
	{
		IAnalysisConfig IValidateJobRequest.AnalysisConfig { get; set; }
		IAnalysisLimits IValidateJobRequest.AnalysisLimits { get; set; }
		IDataDescription IValidateJobRequest.DataDescription { get; set; }
		string IValidateJobRequest.Description { get; set; }
		IModelPlotConfig IValidateJobRequest.ModelPlotConfig { get; set; }
		long? IValidateJobRequest.ModelSnapshotRetentionDays { get; set; }
		IndexName IValidateJobRequest.ResultsIndexName { get; set; }

		/// <inheritdoc />
		public ValidateJobDescriptor<T> AnalysisConfig(Func<AnalysisConfigDescriptor<T>, IAnalysisConfig> selector) =>
			Assign(a => a.AnalysisConfig = selector?.Invoke(new AnalysisConfigDescriptor<T>()));

		/// <inheritdoc />
		public ValidateJobDescriptor<T> AnalysisLimits(Func<AnalysisLimitsDescriptor, IAnalysisLimits> selector) => Assign(a => a.AnalysisLimits = selector?.Invoke(new AnalysisLimitsDescriptor()));

		/// <inheritdoc />
		public ValidateJobDescriptor<T> DataDescription(Func<DataDescriptionDescriptor<T>, IDataDescription> selector) =>
			Assign(a => a.DataDescription = selector.InvokeOrDefault(new DataDescriptionDescriptor<T>()));

		/// <inheritdoc />
		public ValidateJobDescriptor<T> Description(string description) => Assign(a => a.Description = description);

		/// <inheritdoc />
		public ValidateJobDescriptor<T> ModelPlot(Func<ModelPlotConfigDescriptor<T>, IModelPlotConfig> selector) => Assign(a => a.ModelPlotConfig = selector?.Invoke(new ModelPlotConfigDescriptor<T>()));

		/// <inheritdoc />
		public ValidateJobDescriptor<T> ModelSnapshotRetentionDays(long? modelSnapshotRetentionDays) => Assign(a => a.ModelSnapshotRetentionDays = modelSnapshotRetentionDays);

		/// <inheritdoc />
		public ValidateJobDescriptor<T> ResultsIndexName(IndexName indexName) => Assign(a => a.ResultsIndexName = indexName);

		/// <inheritdoc />
		public ValidateJobDescriptor<T> ResultsIndexName<TIndex>() => Assign(a => a.ResultsIndexName = typeof(TIndex));
	}
}
