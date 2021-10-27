// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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

		[DataMember(Name = "from")]
		string From { get; set; }

		[DataMember(Name = "to")]
		string To { get; set; }
	}

	public class TermRangeQuery : FieldNameQueryBase, ITermRangeQuery
	{
		public string GreaterThan { get; set; }
		public string GreaterThanOrEqualTo { get; set; }
		public string LessThan { get; set; }
		public string LessThanOrEqualTo { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Range = this;

		internal static bool IsConditionless(ITermRangeQuery q) => q.Field.IsConditionless()
			|| q.GreaterThanOrEqualTo.IsNullOrEmpty()
			&& q.LessThanOrEqualTo.IsNullOrEmpty()
			&& q.GreaterThan.IsNullOrEmpty()
			&& q.LessThan.IsNullOrEmpty()
			&& q.From.IsNullOrEmpty()
			&& q.To.IsNullOrEmpty();
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
		string ITermRangeQuery.From { get; set; }
		string ITermRangeQuery.To { get; set; }

		public TermRangeQueryDescriptor<T> GreaterThan(string from) => Assign(from, (a, v) => a.GreaterThan = v);
		public TermRangeQueryDescriptor<T> GreaterThanOrEquals(string from) => Assign(from, (a, v) => a.GreaterThanOrEqualTo = v);
		public TermRangeQueryDescriptor<T> LessThan(string to) => Assign(to, (a, v) => a.LessThan = v);
		public TermRangeQueryDescriptor<T> LessThanOrEquals(string to) => Assign(to, (a, v) => a.LessThanOrEqualTo = v);
		public TermRangeQueryDescriptor<T> From(string from) => Assign(from, (a, v) => a.From = v);
		public TermRangeQueryDescriptor<T> To(string to) => Assign(to, (a, v) => a.To = v);
	}
}
