// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[MapsApi("ml.estimate_model_memory.json")]
	[ReadAs(typeof(EstimateModelMemoryRequest))]
	public partial interface IEstimateModelMemoryRequest
	{
		/// <summary>
		/// For a list of the properties that you can specify in the analysis_config component of the body
		/// of this API, see analysis_config.
		/// </summary>
		[DataMember(Name ="analysis_config")]
		IAnalysisConfig AnalysisConfig { get; set; }

		/// <summary>
		/// Estimates of the cardinality that will be observed for fields over the whole time period that
		/// the job analyzes data. To produce a good answer, values must be provided for fields referenced
		/// in the by_field_name, over_field_name and partition_field_name of any detectors. It does not matter
		/// if values are provided for other fields. If no detectors have a by_field_name, over_field_name or
		/// partition_field_name then overall_cardinality can be omitted from the request.
		/// </summary>
		[DataMember(Name = "overall_cardinality")]
		IOverallCardinality OverallCardinality { get; set; }

		/// <summary>
		///  Estimates of the highest cardinality in a single bucket that will be observed for influencer
		/// fields over the time period that the job analyzes data. To produce a good answer, values must
		/// be provided for all influencer fields. It does not matter if values are provided for fields
		/// that are not listed as influencers. If there are no influencers then max_bucket_cardinality
		/// can be omitted from the request.
		/// </summary>
		[DataMember(Name = "max_bucket_cardinality")]
		IMaxBucketCardinality MaxBucketCardinality { get; set; }
	}

	public partial class EstimateModelMemoryRequest
	{
		/// <inheritdoc />
		public IAnalysisConfig AnalysisConfig { get; set; }

		/// <inheritdoc />
		public IOverallCardinality OverallCardinality  { get; set; }

		/// <inheritdoc />
		public IMaxBucketCardinality MaxBucketCardinality  { get; set; }
	}

	public partial class EstimateModelMemoryDescriptor<TDocument> where TDocument : class
	{
		IAnalysisConfig IEstimateModelMemoryRequest.AnalysisConfig { get; set; }
		IOverallCardinality IEstimateModelMemoryRequest.OverallCardinality { get; set; }
		IMaxBucketCardinality IEstimateModelMemoryRequest.MaxBucketCardinality { get; set; }

		/// <inheritdoc />
		public EstimateModelMemoryDescriptor<TDocument> AnalysisConfig(Func<AnalysisConfigDescriptor<TDocument>, IAnalysisConfig> selector) =>
			Assign(selector, (a, v) => a.AnalysisConfig = v?.Invoke(new AnalysisConfigDescriptor<TDocument>()));

		/// <inheritdoc />
		public EstimateModelMemoryDescriptor<TDocument> OverallCardinality(Func<OverallCardinalityDescriptor<TDocument>, IPromise<IOverallCardinality>> analyzerSelector) =>
			Assign(analyzerSelector, (a, v) => a.OverallCardinality = v?.Invoke(new OverallCardinalityDescriptor<TDocument>())?.Value);

		/// <inheritdoc />
		public EstimateModelMemoryDescriptor<TDocument> MaxBucketCardinality(Func<MaxBucketCardinalityDescriptor<TDocument>, IPromise<IMaxBucketCardinality>> analyzerSelector) =>
			Assign(analyzerSelector, (a, v) => a.MaxBucketCardinality = v?.Invoke(new MaxBucketCardinalityDescriptor<TDocument>())?.Value);
	}

	[JsonFormatter(typeof(VerbatimDictionaryKeysFormatter<OverallCardinality, IOverallCardinality, Field, long>))]
	public interface IOverallCardinality : IIsADictionary<Field, long> { }

	public class OverallCardinality : IsADictionaryBase<Field, long>, IOverallCardinality
	{
		public OverallCardinality() { }

		public OverallCardinality(IDictionary<Field, long> container) : base(container) { }

		public void Add(Field field, long cardinality) => BackingDictionary.Add(field, cardinality);
	}

	public class OverallCardinality<T> : OverallCardinality where T : class
	{
		public void Add<TValue>(Expression<Func<T, TValue>> field, long cardinality) => BackingDictionary.Add(field, cardinality);
	}

	public class OverallCardinalityDescriptor<T> : IsADictionaryDescriptorBase<OverallCardinalityDescriptor<T>, IOverallCardinality, Field, long> where T : class
	{
		public OverallCardinalityDescriptor() : base(new OverallCardinality()) { }

		public OverallCardinalityDescriptor<T> Field(Field field, long cardinality) => Assign(field, cardinality);

		public OverallCardinalityDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field, long cardinality) => Assign(field, cardinality);
	}

	[JsonFormatter(typeof(VerbatimDictionaryKeysFormatter<MaxBucketCardinality, IMaxBucketCardinality, Field, long>))]
	public interface IMaxBucketCardinality : IIsADictionary<Field, long> { }

	public class MaxBucketCardinality : IsADictionaryBase<Field, long>, IMaxBucketCardinality
	{
		public MaxBucketCardinality() { }

		public MaxBucketCardinality(IDictionary<Field, long> container) : base(container) { }

		public MaxBucketCardinality(Dictionary<Field, long> container) : base(container) { }

		public void Add(Field field, long cardinality) => BackingDictionary.Add(field, cardinality);
	}

	public class MaxBucketCardinality<T> : MaxBucketCardinality where T : class
	{
		public void Add<TValue>(Expression<Func<T, TValue>> field, long cardinality) => BackingDictionary.Add(field, cardinality);
	}

	public class MaxBucketCardinalityDescriptor<T> : IsADictionaryDescriptorBase<MaxBucketCardinalityDescriptor<T>, IMaxBucketCardinality, Field, long> where T : class
	{
		public MaxBucketCardinalityDescriptor() : base(new MaxBucketCardinality()) { }

		public MaxBucketCardinalityDescriptor<T> Field(Field field, long cardinality) => Assign(field, cardinality);

		public MaxBucketCardinalityDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field, long cardinality) => Assign(field, cardinality);
	}
}
