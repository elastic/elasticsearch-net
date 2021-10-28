// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(FieldNameQueryFormatter<NumericRangeQuery, INumericRangeQuery>))]
	public interface INumericRangeQuery : IRangeQuery
	{
		[DataMember(Name = "gt")]
		double? GreaterThan { get; set; }

		[DataMember(Name = "gte")]
		double? GreaterThanOrEqualTo { get; set; }

		[DataMember(Name = "lt")]
		double? LessThan { get; set; }

		[DataMember(Name = "lte")]
		double? LessThanOrEqualTo { get; set; }

		[DataMember(Name = "relation")]
		RangeRelation? Relation { get; set; }

		/// <summary>
		/// WARNING: This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.
		/// </summary>
		[Obsolete("This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.")]
		[DataMember(Name = "from")]
		double? From { get; set; }

		/// <summary>
		/// WARNING: This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.
		/// </summary>
		[Obsolete("This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.")]
		[DataMember(Name = "to")]
		double? To { get; set; }

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

	public class NumericRangeQuery : FieldNameQueryBase, INumericRangeQuery
	{
		public double? GreaterThan { get; set; }
		public double? GreaterThanOrEqualTo { get; set; }
		public double? LessThan { get; set; }
		public double? LessThanOrEqualTo { get; set; }
		public RangeRelation? Relation { get; set; }

		/// <summary>
		/// WARNING: This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.
		/// </summary>
		[Obsolete("This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.")]
		public double? From { get; set; }
		/// <summary>
		/// WARNING: This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.
		/// </summary>
		[Obsolete("This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.")]
		public double? To { get; set; }
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

		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Range = this;

		internal static bool IsConditionless(INumericRangeQuery q) => q.Field.IsConditionless()
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
	public class NumericRangeQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<NumericRangeQueryDescriptor<T>, INumericRangeQuery, T>, INumericRangeQuery where T : class
	{
		protected override bool Conditionless => NumericRangeQuery.IsConditionless(this);

		double? INumericRangeQuery.GreaterThan { get; set; }
		double? INumericRangeQuery.GreaterThanOrEqualTo { get; set; }
		double? INumericRangeQuery.LessThan { get; set; }
		double? INumericRangeQuery.LessThanOrEqualTo { get; set; }
		RangeRelation? INumericRangeQuery.Relation { get; set; }

		// From, To, IncludeLower and IncludeUpper are not exposed as methods as they are considered deprecated and legacy.
		double? INumericRangeQuery.From { get; set; }
		double? INumericRangeQuery.To { get; set; }
		bool? INumericRangeQuery.IncludeLower { get; set; }
		bool? INumericRangeQuery.IncludeUpper { get; set; }

		public NumericRangeQueryDescriptor<T> GreaterThan(double? from) => Assign(from, (a, v) => a.GreaterThan = v);
		public NumericRangeQueryDescriptor<T> GreaterThanOrEquals(double? from) => Assign(from, (a, v) => a.GreaterThanOrEqualTo = v);
		public NumericRangeQueryDescriptor<T> LessThan(double? to) => Assign(to, (a, v) => a.LessThan = v);
		public NumericRangeQueryDescriptor<T> LessThanOrEquals(double? to) => Assign(to, (a, v) => a.LessThanOrEqualTo = v);
		public NumericRangeQueryDescriptor<T> Relation(RangeRelation? relation) => Assign(relation, (a, v) => a.Relation = v);
	}
}
