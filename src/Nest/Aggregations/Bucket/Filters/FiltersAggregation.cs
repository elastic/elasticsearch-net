// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(FiltersAggregation))]
	public interface IFiltersAggregation : IBucketAggregation
	{
		[DataMember(Name ="filters")]
		Union<INamedFiltersContainer, IEnumerable<QueryContainer>> Filters { get; set; }

		[DataMember(Name ="other_bucket")]
		bool? OtherBucket { get; set; }

		[DataMember(Name ="other_bucket_key")]
		string OtherBucketKey { get; set; }
	}

	public class FiltersAggregation : BucketAggregationBase, IFiltersAggregation
	{
		internal FiltersAggregation() { }

		public FiltersAggregation(string name) : base(name) { }

		public Union<INamedFiltersContainer, IEnumerable<QueryContainer>> Filters { get; set; }

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
		/// Setting this parameter will implicitly set the <see cref="OtherBucket" /> parameter to true
		/// </summary>
		public string OtherBucketKey { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.Filters = this;
	}

	public class FiltersAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<FiltersAggregationDescriptor<T>, IFiltersAggregation, T>
			, IFiltersAggregation
		where T : class
	{
		Union<INamedFiltersContainer, IEnumerable<QueryContainer>> IFiltersAggregation.Filters { get; set; }

		bool? IFiltersAggregation.OtherBucket { get; set; }

		string IFiltersAggregation.OtherBucketKey { get; set; }

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
		/// <returns>the <see cref="FiltersAggregationDescriptor{T}" /></returns>
		public FiltersAggregationDescriptor<T> OtherBucket(bool? otherBucket = true) =>
			Assign(otherBucket, (a, v) => a.OtherBucket = v);

		/// <summary>
		/// Sets the key for the other bucket to a value other than the default "_other_".
		/// Setting this parameter will implicitly set the <see cref="OtherBucket" /> parameter to true
		/// </summary>
		/// <param name="otherBucketKey">the name for the other bucket</param>
		/// <returns>the <see cref="FiltersAggregationDescriptor{T}" /></returns>
		public FiltersAggregationDescriptor<T> OtherBucketKey(string otherBucketKey) =>
			Assign(otherBucketKey, (a, v) => a.OtherBucketKey = v);

		public FiltersAggregationDescriptor<T> NamedFilters(Func<NamedFiltersContainerDescriptor<T>, IPromise<INamedFiltersContainer>> selector) =>
			Assign(selector, (a, v) => a.Filters =
				new Union<INamedFiltersContainer, IEnumerable<QueryContainer>>(v?.Invoke(new NamedFiltersContainerDescriptor<T>())?.Value));

		public FiltersAggregationDescriptor<T> AnonymousFilters(params Func<QueryContainerDescriptor<T>, QueryContainer>[] selectors) =>
			Assign(selectors, (a, v) => a.Filters = v.Select(vv => vv?.Invoke(new QueryContainerDescriptor<T>())).ToList());

		public FiltersAggregationDescriptor<T> AnonymousFilters(IEnumerable<Func<QueryContainerDescriptor<T>, QueryContainer>> selectors) =>
			Assign(selectors, (a, v) => a.Filters = v.Select(vv => vv?.Invoke(new QueryContainerDescriptor<T>())).ToList());
	}
}
