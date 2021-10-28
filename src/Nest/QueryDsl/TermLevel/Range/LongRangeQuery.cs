// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(FieldNameQueryFormatter<LongRangeQuery, ILongRangeQuery>))]
	public interface ILongRangeQuery : IRangeQuery
	{
		[DataMember(Name ="gt")]
		long? GreaterThan { get; set; }

		[DataMember(Name ="gte")]
		long? GreaterThanOrEqualTo { get; set; }

		[DataMember(Name ="lt")]
		long? LessThan { get; set; }

		[DataMember(Name ="lte")]
		long? LessThanOrEqualTo { get; set; }

		[DataMember(Name = "relation")]
		RangeRelation? Relation { get; set; }

		/// <summary>
		/// WARNING: This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.
		/// </summary>
		[Obsolete("This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.")]
		[DataMember(Name = "from")]
		long? From { get; set; }
		/// <summary>
		/// WARNING: This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.
		/// </summary>
		[Obsolete("This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.")]
		[DataMember(Name = "to")]
		long? To { get; set; }
		/// <summary>
		/// WARNING: This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.
		/// </summary>
		[Obsolete("This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.")]
		[DataMember(Name = "include_lower")]
		bool? IncludeLower { get; set; }
		/// <summary>
		/// WARNING: This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.
		/// </summary>
		[Obsolete("This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.")]
		[DataMember(Name = "include_upper")]
		bool? IncludeUpper { get; set; }
	}

	public class LongRangeQuery : FieldNameQueryBase, ILongRangeQuery
	{
		public long? GreaterThan { get; set; }
		public long? GreaterThanOrEqualTo { get; set; }
		public long? LessThan { get; set; }
		public long? LessThanOrEqualTo { get; set; }

		/// <summary>
		/// WARNING: This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.
		/// </summary>
		[Obsolete("This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.")]
		public long? From { get; set; }
		/// <summary>
		/// WARNING: This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.
		/// </summary>
		[Obsolete("This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.")]
		public long? To { get; set; }
		/// <summary>
		/// WARNING: This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.
		/// </summary>
		[Obsolete("This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.")]
		public bool? IncludeLower { get; set; }
		/// <summary>
		/// WARNING: This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.
		/// </summary>
		[Obsolete("This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.")]
		public bool? IncludeUpper { get; set; }

		public RangeRelation? Relation { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Range = this;

		internal static bool IsConditionless(ILongRangeQuery q) => q.Field.IsConditionless()
			|| q.GreaterThanOrEqualTo == null
			&& q.LessThanOrEqualTo == null
			&& q.GreaterThan == null
			&& q.LessThan == null
#pragma warning disable CS0618 // Type or member is obsolete
			&& q.From == null
			&& q.To == null;
#pragma warning restore CS0618 // Type or member is obsolete
	}

	[DataContract]
	public class LongRangeQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<LongRangeQueryDescriptor<T>, ILongRangeQuery, T>, ILongRangeQuery where T : class
	{
		protected override bool Conditionless => LongRangeQuery.IsConditionless(this);
		long? ILongRangeQuery.GreaterThan { get; set; }
		long? ILongRangeQuery.GreaterThanOrEqualTo { get; set; }
		long? ILongRangeQuery.LessThan { get; set; }
		long? ILongRangeQuery.LessThanOrEqualTo { get; set; }
		RangeRelation? ILongRangeQuery.Relation { get; set; }

		// From, To, IncludeLower and IncludeUpper are not exposed as methods as they are considered deprecated and legacy.
		long? ILongRangeQuery.From { get; set; }
		long? ILongRangeQuery.To { get; set; }
		bool? ILongRangeQuery.IncludeLower { get; set; }
		bool? ILongRangeQuery.IncludeUpper { get; set; }

		public LongRangeQueryDescriptor<T> Relation(RangeRelation? relation) => Assign(relation, (a, v) => a.Relation = v);
		public LongRangeQueryDescriptor<T> GreaterThan(long? from) => Assign(from, (a, v) => a.GreaterThan = v);
		public LongRangeQueryDescriptor<T> GreaterThanOrEquals(long? from) => Assign(from, (a, v) => a.GreaterThanOrEqualTo = v);
		public LongRangeQueryDescriptor<T> LessThan(long? to) => Assign(to, (a, v) => a.LessThan = v);
		public LongRangeQueryDescriptor<T> LessThanOrEquals(long? to) => Assign(to, (a, v) => a.LessThanOrEqualTo = v);
	}
}
