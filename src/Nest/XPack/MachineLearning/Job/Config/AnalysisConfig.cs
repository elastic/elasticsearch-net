// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// An analysis configuration for a machine learning job.
	/// </summary>
	[ReadAs(typeof(AnalysisConfig))]
	public interface IAnalysisConfig
	{
		/// <summary>
		/// The size of the interval that the analysis is aggregated into, typically between 5m and 1h.
		/// The default value is 5m.
		/// </summary>
		[DataMember(Name = "bucket_span")]
		Time BucketSpan { get; set; }

		/// <summary>
		/// If this property is specified, the values of the specified field will be categorized.
		/// The resulting categories must be used in a detector by setting by_field_name, over_field_name,
		/// or partition_field_name to the keyword mlcategory
		/// </summary>
		[DataMember(Name = "categorization_field_name")]
		Field CategorizationFieldName { get; set; }

		/// <summary>
		/// If categorization_field_name is specified, you can also define optional filters.
		/// This property expects an array of regular expressions.
		/// The expressions are used to filter out matching sequences off the categorization field values.
		/// </summary>
		[DataMember(Name = "categorization_filters")]
		IEnumerable<string> CategorizationFilters { get; set; }

		/// <summary>
		/// A collection of detectors, which describe the anomaly detectors that are used in the job.
		/// </summary>
		[DataMember(Name = "detectors")]
		IEnumerable<IDetector> Detectors { get; set; }

		/// <summary>
		/// A collection of influencer field names. Typically these can be the by, over, or partition fields
		/// that are used in the detector configuration. You might also want to use a field name that is not
		/// specifically named in a detector, but is available as part of the input data.
		/// When you use multiple detectors, the use of influencers is recommended as it aggregates results for each influencer
		/// entity.
		/// </summary>
		[DataMember(Name = "influencers")]
		Fields Influencers { get; set; }

		/// <summary>
		/// The size of the window in which to expect data that is out of time order. The default value is 0 (no latency).
		/// </summary>
		[DataMember(Name = "latency")]
		Time Latency { get; set; }

		/// <summary>
		/// If set to true, the analysis will automatically find correlations between metrics
		/// for a given by field value and report anomalies when those correlations cease to hold.
		/// </summary>
		[DataMember(Name = "multivariate_by_fields")]
		bool? MultivariateByFields { get; set; }

		/// <summary>
		/// The name of the field that contains the count of raw data points that have been summarized, if
		/// data that is fed to the job is expected to be pre-summarized.
		/// </summary>
		[DataMember(Name = "summary_count_field_name")]
		Field SummaryCountFieldName { get; set; }
	}

	/// <inheritdoc />
	public class AnalysisConfig : IAnalysisConfig
	{
		/// <inheritdoc />
		public Time BucketSpan { get; set; }

		/// <inheritdoc />
		public Field CategorizationFieldName { get; set; }

		/// <inheritdoc />
		public IEnumerable<string> CategorizationFilters { get; set; }

		/// <inheritdoc />
		public IEnumerable<IDetector> Detectors { get; set; }

		/// <inheritdoc />
		public Fields Influencers { get; set; }

		/// <inheritdoc />
		public Time Latency { get; set; }

		/// <inheritdoc />
		public bool? MultivariateByFields { get; set; }

		/// <inheritdoc />
		public Field SummaryCountFieldName { get; set; }
	}

	/// <inheritdoc />
	public class AnalysisConfigDescriptor<T> : DescriptorBase<AnalysisConfigDescriptor<T>, IAnalysisConfig>, IAnalysisConfig where T : class
	{
		Time IAnalysisConfig.BucketSpan { get; set; }
		Field IAnalysisConfig.CategorizationFieldName { get; set; }
		IEnumerable<string> IAnalysisConfig.CategorizationFilters { get; set; }
		IEnumerable<IDetector> IAnalysisConfig.Detectors { get; set; }
		Fields IAnalysisConfig.Influencers { get; set; }
		Time IAnalysisConfig.Latency { get; set; }
		bool? IAnalysisConfig.MultivariateByFields { get; set; }
		Field IAnalysisConfig.SummaryCountFieldName { get; set; }

		/// <inheritdoc />
		public AnalysisConfigDescriptor<T> BucketSpan(Time bucketSpan) => Assign(bucketSpan, (a, v) => a.BucketSpan = v);

		/// <inheritdoc />
		public AnalysisConfigDescriptor<T> CategorizationFieldName(Field field) => Assign(field, (a, v) => a.CategorizationFieldName = v);

		/// <inheritdoc />
		public AnalysisConfigDescriptor<T> CategorizationFieldName<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.CategorizationFieldName = v);

		/// <inheritdoc />
		public AnalysisConfigDescriptor<T> CategorizationFilters(IEnumerable<string> filters) => Assign(filters, (a, v) => a.CategorizationFilters = v);

		/// <inheritdoc />
		public AnalysisConfigDescriptor<T> CategorizationFilters(params string[] filters) => Assign(filters, (a, v) => a.CategorizationFilters = v);

		/// <inheritdoc />
		public AnalysisConfigDescriptor<T> Detectors(Func<DetectorsDescriptor<T>, IPromise<IEnumerable<IDetector>>> selector) =>
			Assign(selector.InvokeOrDefault(new DetectorsDescriptor<T>()).Value, (a, v) => a.Detectors = v);

		/// <inheritdoc />
		public AnalysisConfigDescriptor<T> Influencers(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.Influencers = v?.Invoke(new FieldsDescriptor<T>())?.Value);

		/// <inheritdoc />
		public AnalysisConfigDescriptor<T> Influencers(Fields fields) => Assign(fields, (a, v) => a.Influencers = v);

		/// <inheritdoc />
		public AnalysisConfigDescriptor<T> Latency(Time latency) => Assign(latency, (a, v) => a.Latency = v);

		/// <inheritdoc />
		public AnalysisConfigDescriptor<T> MultivariateByFields(bool? multivariateByFields = true) =>
			Assign(multivariateByFields, (a, v) => a.MultivariateByFields = v);

		/// <inheritdoc />
		public AnalysisConfigDescriptor<T> SummaryCountFieldName(Field summaryCountFieldName) =>
			Assign(summaryCountFieldName, (a, v) => a.SummaryCountFieldName = v);

		/// <inheritdoc />
		public AnalysisConfigDescriptor<T> SummaryCountFieldName<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.SummaryCountFieldName = v);
	}
}
