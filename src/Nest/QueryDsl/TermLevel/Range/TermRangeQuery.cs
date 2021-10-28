// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(FieldNameQueryFormatter<TermRangeQuery, ITermRangeQuery>))]
	public interface ITermRangeQuery : IRangeQuery
	{
		[DataMember(Name = "gt")]
		string GreaterThan { get; set; }
		[DataMember(Name = "gte")]
		string GreaterThanOrEqualTo { get; set; }
		[DataMember(Name = "lt")]
		string LessThan { get; set; }
		[DataMember(Name = "lte")]
		string LessThanOrEqualTo { get; set; }

		/// <summary>
		/// WARNING: This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.
		/// </summary>
		[Obsolete("This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.")]
		[DataMember(Name = "from")]
		string From { get; set; }

		/// <summary>
		/// WARNING: This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.
		/// </summary>
		[Obsolete("This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.")]
		[DataMember(Name = "to")]
		string To { get; set; }

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

	public class TermRangeQuery : FieldNameQueryBase, ITermRangeQuery
	{
		public string GreaterThan { get; set; }
		public string GreaterThanOrEqualTo { get; set; }
		public string LessThan { get; set; }
		public string LessThanOrEqualTo { get; set; }

		/// <summary>
		/// WARNING: This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.
		/// </summary>
		[Obsolete("This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.")]
		public string From { get; set; }
		/// <summary>
		/// WARNING: This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.
		/// </summary>
		[Obsolete("This property is considered deprecated and will be removed in the next major release. Range queries should prefer the gt, lt, gte and lte properties instead.")]
		public string To { get; set; }
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

		internal static bool IsConditionless(ITermRangeQuery q) => q.Field.IsConditionless()
			|| q.GreaterThanOrEqualTo.IsNullOrEmpty()
			&& q.LessThanOrEqualTo.IsNullOrEmpty()
			&& q.GreaterThan.IsNullOrEmpty()
			&& q.LessThan.IsNullOrEmpty()
#pragma warning disable CS0618 // Type or member is obsolete
			&& q.From.IsNullOrEmpty()
			&& q.To.IsNullOrEmpty();
#pragma warning restore CS0618 // Type or member is obsolete
	}

	[DataContract]
	public class TermRangeQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<TermRangeQueryDescriptor<T>, ITermRangeQuery, T>, ITermRangeQuery where T : class
	{
		protected override bool Conditionless => TermRangeQuery.IsConditionless(this);
		string ITermRangeQuery.GreaterThan { get; set; }
		string ITermRangeQuery.GreaterThanOrEqualTo { get; set; }
		string ITermRangeQuery.LessThan { get; set; }
		string ITermRangeQuery.LessThanOrEqualTo { get; set; }

		// From, To, IncludeLower and IncludeUpper are not exposed as methods as they are considered deprecated and legacy.
		string ITermRangeQuery.From { get; set; }
		string ITermRangeQuery.To { get; set; }
		bool? ITermRangeQuery.IncludeLower { get; set; }
		bool? ITermRangeQuery.IncludeUpper { get; set; }

		public TermRangeQueryDescriptor<T> GreaterThan(string from) => Assign(from, (a, v) => a.GreaterThan = v);
		public TermRangeQueryDescriptor<T> GreaterThanOrEquals(string from) => Assign(from, (a, v) => a.GreaterThanOrEqualTo = v);
		public TermRangeQueryDescriptor<T> LessThan(string to) => Assign(to, (a, v) => a.LessThan = v);
		public TermRangeQueryDescriptor<T> LessThanOrEquals(string to) => Assign(to, (a, v) => a.LessThanOrEqualTo = v);
	}
}
