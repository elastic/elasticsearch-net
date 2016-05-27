using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<FiltersAggregation>))]
	public interface IFiltersAggregation : IBucketAggregation
	{
		[JsonProperty("filters")]
		Union<INamedFiltersContainer, List<QueryContainer>> Filters { get; set; }

		[JsonProperty("other_bucket")]
		bool? OtherBucket { get; set; }

		[JsonProperty("other_bucket_key")]
		string OtherBucketKey { get; set; }
	}

	public class FiltersAggregation : BucketAggregationBase, IFiltersAggregation
	{
		public Union<INamedFiltersContainer, List<QueryContainer>> Filters { get; set; }

		/// <summary>
		/// Gets or sets whether to add a bucket to the response which will contain all documents
		/// that do not match any of the given filters.
		/// When set to <c>true</c>, the other bucket will be returned either in a bucket
		/// (named "_other_" by default) if named filters are being used,
		///  or as the last bucket if anonymous filters are being used
		/// When set to <c>false</c>, does not compute
		/// the other bucket.
		/// </summary>
		public bool? OtherBucket { get; set; }

		/// <summary>
		/// Gets or sets the key for the other bucket to a value other than the default "_other_".
		/// Setting this parameter will implicitly set the <see cref="OtherBucket"/> parameter to true
		/// </summary>
		public string OtherBucketKey { get; set; }

		internal FiltersAggregation() { }

		public FiltersAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Filters = this;
	}

	public class FiltersAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<FiltersAggregationDescriptor<T>, IFiltersAggregation, T>
		, IFiltersAggregation
		where T : class
	{
		Union<INamedFiltersContainer, List<QueryContainer>> IFiltersAggregation.Filters { get; set; }

		bool? IFiltersAggregation.OtherBucket{ get; set; }

		string IFiltersAggregation.OtherBucketKey{ get; set; }

		/// <summary>
		/// Adds a bucket to the response which will contain all documents
		/// that do not match any of the given filters.
		/// When set to <c>true</c>, the other bucket will be returned either in a bucket
		/// (named "_other_" by default) if named filters are being used,
		///  or as the last bucket if anonymous filters are being used
		/// When set to <c>false</c>, does not compute
		/// the other bucket.
		/// </summary>
		/// <param name="otherBucket">whether to set the other bucket</param>
		/// <returns>the <see cref="FiltersAggregationDescriptor{T}"/></returns>
		public FiltersAggregationDescriptor<T> OtherBucket(bool otherBucket = true) =>
			Assign(a => a.OtherBucket = otherBucket);

		/// <summary>
		/// Sets the key for the other bucket to a value other than the default "_other_".
		/// Setting this parameter will implicitly set the <see cref="OtherBucket"/> parameter to true
		/// </summary>
		/// <param name="otherBucketKey">the name for the other bucket</param>
		/// <returns>the <see cref="FiltersAggregationDescriptor{T}"/></returns>
		public FiltersAggregationDescriptor<T> OtherBucketKey(string otherBucketKey) =>
			Assign(a => a.OtherBucketKey = otherBucketKey);

		public FiltersAggregationDescriptor<T> NamedFilters(Func<NamedFiltersContainerDescriptor<T>, IPromise<INamedFiltersContainer>> selector) =>
			Assign(a => a.Filters = new Union<INamedFiltersContainer, List<QueryContainer>>(selector?.Invoke(new NamedFiltersContainerDescriptor<T>())?.Value));

		public FiltersAggregationDescriptor<T> AnonymousFilters(params Func<QueryContainerDescriptor<T>, QueryContainer>[] selectors) =>
			Assign(a => a.Filters = selectors.Select(s=>s?.Invoke(new QueryContainerDescriptor<T>())).ToList());

		public FiltersAggregationDescriptor<T> AnonymousFilters(IEnumerable<Func<QueryContainerDescriptor<T>, QueryContainer>> selectors) =>
			Assign(a => a.Filters = selectors.Select(s=>s?.Invoke(new QueryContainerDescriptor<T>())).ToList());

	}
}
