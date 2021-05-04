// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Validates a machine learning job
	/// </summary>
	[MapsApi("ml.validate.json")]
	public partial interface IValidateJobRequest
	{
		/// <summary>
		/// The analysis configuration, which specifies how to analyze the data.
		/// </summary>
		[DataMember(Name ="analysis_config")]
		IAnalysisConfig AnalysisConfig { get; set; }

		/// <summary>
		/// Optionally specifies runtime limits for the job.
		/// </summary>
		[DataMember(Name ="analysis_limits")]
		IAnalysisLimits AnalysisLimits { get; set; }

		/// <summary>
		/// Describes the format of the input data. This object is required, but it can be empty.
		/// </summary>
		[DataMember(Name ="data_description")]
		IDataDescription DataDescription { get; set; }

		/// <summary>
		/// An optional description of the job
		/// </summary>
		[DataMember(Name ="description")]
		string Description { get; set; }

		/// <summary>
		/// This advanced configuration option stores model information along with the results.
		/// This adds overhead to the performance of the system and is not feasible for jobs with many entities
		/// </summary>
		[DataMember(Name ="model_plot")]
		IModelPlotConfig ModelPlotConfig { get; set; }

		/// <summary>
		/// The time in days that model snapshots are retained for the job.
		/// Older snapshots are deleted. The default value is 1 day.
		/// </summary>
		[DataMember(Name ="model_snapshot_retention_days")]
		long? ModelSnapshotRetentionDays { get; set; }

		/// <summary>
		/// The name of the index in which to store the machine learning results.
		/// The default value is shared, which corresponds to the index name .ml-anomalies-shared.
		/// </summary>
		[DataMember(Name ="results_index_name")]
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
	public partial class ValidateJobDescriptor<TDocument> where TDocument : class
	{
		IAnalysisConfig IValidateJobRequest.AnalysisConfig { get; set; }
		IAnalysisLimits IValidateJobRequest.AnalysisLimits { get; set; }
		IDataDescription IValidateJobRequest.DataDescription { get; set; }
		string IValidateJobRequest.Description { get; set; }
		IModelPlotConfig IValidateJobRequest.ModelPlotConfig { get; set; }
		long? IValidateJobRequest.ModelSnapshotRetentionDays { get; set; }
		IndexName IValidateJobRequest.ResultsIndexName { get; set; }

		/// <inheritdoc />
		public ValidateJobDescriptor<TDocument> AnalysisConfig(Func<AnalysisConfigDescriptor<TDocument>, IAnalysisConfig> selector) =>
			Assign(selector, (a, v) => a.AnalysisConfig = v?.Invoke(new AnalysisConfigDescriptor<TDocument>()));

		/// <inheritdoc />
		public ValidateJobDescriptor<TDocument> AnalysisLimits(Func<AnalysisLimitsDescriptor, IAnalysisLimits> selector) =>
			Assign(selector, (a, v) => a.AnalysisLimits = v?.Invoke(new AnalysisLimitsDescriptor()));

		/// <inheritdoc />
		public ValidateJobDescriptor<TDocument> DataDescription(Func<DataDescriptionDescriptor<TDocument>, IDataDescription> selector) =>
			Assign(selector.InvokeOrDefault(new DataDescriptionDescriptor<TDocument>()), (a, v) => a.DataDescription = v);

		/// <inheritdoc />
		public ValidateJobDescriptor<TDocument> Description(string description) => Assign(description, (a, v) => a.Description = v);

		/// <inheritdoc />
		public ValidateJobDescriptor<TDocument> ModelPlot(Func<ModelPlotConfigDescriptor<TDocument>, IModelPlotConfig> selector) =>
			Assign(selector, (a, v) => a.ModelPlotConfig = v?.Invoke(new ModelPlotConfigDescriptor<TDocument>()));

		/// <inheritdoc />
		public ValidateJobDescriptor<TDocument> ModelSnapshotRetentionDays(long? modelSnapshotRetentionDays) =>
			Assign(modelSnapshotRetentionDays, (a, v) => a.ModelSnapshotRetentionDays = v);

		/// <inheritdoc />
		public ValidateJobDescriptor<TDocument> ResultsIndexName(IndexName indexName) => Assign(indexName, (a, v) => a.ResultsIndexName = v);

		/// <inheritdoc />
		public ValidateJobDescriptor<TDocument> ResultsIndexName<TIndex>() => Assign(typeof(TIndex), (a, v) => a.ResultsIndexName = v);
	}
}
