// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
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
	}

	public class WildcardQuery<T, TValue> : WildcardQuery
		where T : class
	{
		public WildcardQuery(Expression<Func<T, TValue>> field) => Field = field;
	}

	public class WildcardQuery : FieldNameQueryBase, IWildcardQuery
	{
		public MultiTermQueryRewrite Rewrite { get; set; }
		public object Value { get; set; }
		protected override bool Conditionless => TermQuery.IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Wildcard = this;
	}

	public class WildcardQueryDescriptor<T>
		: TermQueryDescriptorBase<WildcardQueryDescriptor<T>, IWildcardQuery, T>,
			IWildcardQuery
		where T : class
	{
		MultiTermQueryRewrite IWildcardQuery.Rewrite { get; set; }

		public WildcardQueryDescriptor<T> Rewrite(MultiTermQueryRewrite rewrite) => Assign(rewrite, (a, v) => a.Rewrite = v);
	}
}
