using System;
using System.Reflection;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// Creates a machine learning job
	/// </summary>
	public partial interface IPutJobRequest
	{
		/// <summary>
		/// The analysis configuration, which specifies how to analyze the data.
		/// </summary>
		[JsonProperty("analysis_config")]
		IAnalysisConfig AnalysisConfig { get; set; }

		/// <summary>
		/// Defines approximate limits on the memory resource requirements for the job.
		/// </summary>
		[JsonProperty("analysis_limits")]
		IAnalysisLimits AnalysisLimits { get; set; }

		/// <summary>
		/// Describes the format of the input data. This object is required, but it can be empty
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
	public partial class PutJobRequest
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
	[DescriptorFor("XpackMlPutJob")]
	public partial class PutJobDescriptor<T> where T : class
	{
		IAnalysisConfig IPutJobRequest.AnalysisConfig { get; set; }
		IAnalysisLimits IPutJobRequest.AnalysisLimits { get; set; }
		IDataDescription IPutJobRequest.DataDescription { get; set; }
		string IPutJobRequest.Description { get; set; }
		IModelPlotConfig IPutJobRequest.ModelPlotConfig { get; set; }
		long? IPutJobRequest.ModelSnapshotRetentionDays { get; set; }
		IndexName IPutJobRequest.ResultsIndexName { get; set; }

		/// <inheritdoc />
		public PutJobDescriptor<T> AnalysisConfig(Func<AnalysisConfigDescriptor<T>, IAnalysisConfig> selector) =>
			Assign(a => a.AnalysisConfig = selector?.Invoke(new AnalysisConfigDescriptor<T>()));

		/// <inheritdoc />
		public PutJobDescriptor<T> AnalysisLimits(Func<AnalysisLimitsDescriptor, IAnalysisLimits> selector) =>
			Assign(a => a.AnalysisLimits = selector?.Invoke(new AnalysisLimitsDescriptor()));

		/// <inheritdoc />
		public PutJobDescriptor<T> DataDescription(Func<DataDescriptionDescriptor<T>, IDataDescription> selector) =>
			Assign(a => a.DataDescription = selector.InvokeOrDefault(new DataDescriptionDescriptor<T>()));

		/// <inheritdoc />
		public PutJobDescriptor<T> Description(string description) => Assign(a => a.Description = description);

		/// <inheritdoc />
		public PutJobDescriptor<T> ModelPlot(Func<ModelPlotConfigDescriptor<T>, IModelPlotConfig> selector) =>
			Assign(a => a.ModelPlotConfig = selector?.Invoke(new ModelPlotConfigDescriptor<T>()));

		/// <inheritdoc />
		public PutJobDescriptor<T> ModelSnapshotRetentionDays(long? modelSnapshotRetentionDays) =>
			Assign(a => a.ModelSnapshotRetentionDays = modelSnapshotRetentionDays);

		/// <inheritdoc />
		public PutJobDescriptor<T> ResultsIndexName(IndexName indexName) =>
			Assign(a => a.ResultsIndexName = indexName);

		/// <inheritdoc />
		public PutJobDescriptor<T> ResultsIndexName<TIndex>() =>
			Assign(a => a.ResultsIndexName = typeof(TIndex));
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum ExcludeFrequent
	{
		[EnumMember(Value = "all")]
		All,
		[EnumMember(Value = "none")]
		None,
		[EnumMember(Value = "by")]
		By,
		[EnumMember(Value = "over")]
		Over
	}
}
