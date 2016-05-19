using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IFiltersAggregation : IBucketAggregation
	{
		[JsonProperty("other_bucket")]
		bool? OtherBucket { get; set; }

		[JsonProperty("other_bucket_key")]
		string OtherBucketKey { get; set; }
	}


	public abstract class FiltersAggregationBase : BucketAggregationBase, IFiltersAggregation
	{
		internal FiltersAggregationBase() { }
		public FiltersAggregationBase(string name) : base(name) { }

		public bool? OtherBucket { get; set; }

		public string OtherBucketKey { get; set; }
	}

	public abstract class FiltersAggregationDescriptorBase<TFiltersAggregationDescriptor, TFiltersAggregationInterface, T>
		: BucketAggregationDescriptorBase<TFiltersAggregationDescriptor, TFiltersAggregationInterface, T>
			, IFiltersAggregation, IDescriptor
		where TFiltersAggregationDescriptor : FiltersAggregationDescriptorBase<TFiltersAggregationDescriptor, TFiltersAggregationInterface, T>
			, TFiltersAggregationInterface, IFiltersAggregation
		where T : class
		where TFiltersAggregationInterface : class, IFiltersAggregation
	{
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
		/// <returns>the <see cref="AnonymousFiltersAggregationDescriptor{T}"/></returns>
		public TFiltersAggregationDescriptor OtherBucket(bool otherBucket = true) =>
			Assign(a => a.OtherBucket = otherBucket);

		/// <summary>
		/// Sets the key for the other bucket to a value other than the default "_other_".
		/// Setting this parameter will implicitly set the <see cref="OtherBucket"/> parameter to true
		/// </summary>
		/// <param name="otherBucketKey">the name for the other bucket</param>
		/// <returns>the <see cref="AnonymousFiltersAggregationDescriptor{T}"/></returns>
		public TFiltersAggregationDescriptor OtherBucketKey(string otherBucketKey) =>
			Assign(a => a.OtherBucketKey = otherBucketKey);
	}
}
