// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(FieldNameQueryFormatter<WildcardQuery, IWildcardQuery>))]
	public interface IWildcardQuery : ITermQuery
	{
		[DataMember(Name = "rewrite")]
		MultiTermQueryRewrite Rewrite { get; set; }

		[DataMember(Name = "wildcard")]
		string Wildcard { get; set; }
	}

	public class WildcardQuery<T, TValue> : WildcardQuery where T : class
	{
		public WildcardQuery(Expression<Func<T, TValue>> field) => Field = field;
	}

	public class WildcardQuery : FieldNameQueryBase, IWildcardQuery
	{
		public MultiTermQueryRewrite Rewrite { get; set; }
		public object Value { get; set; }
		public bool? CaseInsensitive { get; set; }
		public string Wildcard { get; set; }

		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Wildcard = this;

		// Wildcard queries must include the `field` and either a `value` OR a `wildcard` to match
		internal static bool IsConditionless(IWildcardQuery q) => (q.Value == null && q.Wildcard == null)
			|| ((q.Value?.ToString().IsNullOrEmpty() ?? true) && (q.Wildcard?.ToString().IsNullOrEmpty() ?? true))
			|| q.Field.IsConditionless();
	}

	public class WildcardQueryDescriptor<T>
		: TermQueryDescriptorBase<WildcardQueryDescriptor<T>, IWildcardQuery, T>, IWildcardQuery
			where T : class
	{
		MultiTermQueryRewrite IWildcardQuery.Rewrite { get; set; }
		string IWildcardQuery.Wildcard { get; set; }

		protected override bool Conditionless => WildcardQuery.IsConditionless(this);

		public WildcardQueryDescriptor<T> Rewrite(MultiTermQueryRewrite rewrite) => Assign(rewrite, (a, v) => a.Rewrite = v);

		public WildcardQueryDescriptor<T> Wildcard(string value) => Assign(value, (a, v) => a.Wildcard = v);
	}
}
