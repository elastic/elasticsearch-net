// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A multi-bucket value source based aggregation which finds "rare" terms — terms that are at the long-tail of the distribution
	/// and are not frequent. Conceptually, this is like a terms aggregation that is sorted by _count ascending.
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(RareTermsAggregation))]
	public interface IRareTermsAggregation : IBucketAggregation
	{
		/// <summary>
		/// Terms that should be excluded from the aggregation
		/// </summary>
		[DataMember(Name = "exclude")]
		TermsExclude Exclude { get; set; }

		/// <summary>
		/// The field to find rare terms in
		/// </summary>
		[DataMember(Name = "field")]
		Field Field { get; set; }

		/// <summary>
		/// Terms that should be included in the aggregation
		/// </summary>
		[DataMember(Name = "include")]
		TermsInclude Include { get; set; }

		/// <summary>
		/// The maximum number of documents a term should appear in.
		/// Defaults to <c>1</c>
		/// </summary>
		[DataMember(Name = "max_doc_count")]
		long? MaximumDocumentCount { get; set; }

		/// <summary>
		/// The value that should be used if a document does not have the field being aggregated
		/// </summary>
		[DataMember(Name = "missing")]
		object Missing { get; set; }

		/// <summary>
		/// The precision of the internal CuckooFilters. Smaller precision leads to better approximation,
		/// but higher memory usage. Cannot be smaller than 0.00001. Defaults to 0.01
		/// </summary>
		[DataMember(Name = "precision")]
		double? Precision { get; set; }
	}

	/// <inheritdoc cref="IRareTermsAggregation"/>
	public class RareTermsAggregation : BucketAggregationBase, IRareTermsAggregation
	{
		internal RareTermsAggregation() { }

		public RareTermsAggregation(string name) : base(name) { }

		/// <inheritdoc />
		public TermsExclude Exclude { get; set; }
		/// <inheritdoc />
		public Field Field { get; set; }
		/// <inheritdoc />
		public TermsInclude Include { get; set; }
		/// <inheritdoc />
		public long? MaximumDocumentCount { get; set; }
		/// <inheritdoc />
		public object Missing { get; set; }
		/// <inheritdoc />
		public double? Precision { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.RareTerms = this;
	}

	/// <inheritdoc cref="IRareTermsAggregation"/>
	public class RareTermsAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<RareTermsAggregationDescriptor<T>, IRareTermsAggregation, T>, IRareTermsAggregation
		where T : class
	{
		TermsExclude IRareTermsAggregation.Exclude { get; set; }
		Field IRareTermsAggregation.Field { get; set; }
		TermsInclude IRareTermsAggregation.Include { get; set; }
		long? IRareTermsAggregation.MaximumDocumentCount { get; set; }
		object IRareTermsAggregation.Missing { get; set; }
		double? IRareTermsAggregation.Precision { get; set; }

		/// <inheritdoc cref="IRareTermsAggregation.Field" />
		public RareTermsAggregationDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IRareTermsAggregation.Field" />
		public RareTermsAggregationDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IRareTermsAggregation.MaximumDocumentCount" />
		public RareTermsAggregationDescriptor<T> MaximumDocumentCount(long? maximumDocumentCount) =>
			Assign(maximumDocumentCount, (a, v) => a.MaximumDocumentCount = v);

		/// <inheritdoc cref="IRareTermsAggregation.Include" />
		public RareTermsAggregationDescriptor<T> Include(long partition, long numberOfPartitions) =>
			Assign(new TermsInclude(partition, numberOfPartitions), (a, v) => a.Include = v);

		/// <inheritdoc cref="IRareTermsAggregation.Include" />
		public RareTermsAggregationDescriptor<T> Include(string includePattern) =>
			Assign(new TermsInclude(includePattern), (a, v) => a.Include = v);

		/// <inheritdoc cref="IRareTermsAggregation.Include" />
		public RareTermsAggregationDescriptor<T> Include(IEnumerable<string> values) =>
			Assign(new TermsInclude(values), (a, v) => a.Include = v);

		/// <inheritdoc cref="IRareTermsAggregation.Exclude" />
		public RareTermsAggregationDescriptor<T> Exclude(string excludePattern) =>
			Assign(new TermsExclude(excludePattern), (a, v) => a.Exclude = v);

		/// <inheritdoc cref="IRareTermsAggregation.Exclude" />
		public RareTermsAggregationDescriptor<T> Exclude(IEnumerable<string> values) =>
			Assign(new TermsExclude(values), (a, v) => a.Exclude = v);

		/// <inheritdoc cref="IRareTermsAggregation.Missing" />
		public RareTermsAggregationDescriptor<T> Missing(object missing) => Assign(missing, (a, v) => a.Missing = v);

		/// <inheritdoc cref="IRareTermsAggregation.Precision" />
		public RareTermsAggregationDescriptor<T> Precision(double? precision) => Assign(precision, (a, v) => a.Precision = v);
	}
}
