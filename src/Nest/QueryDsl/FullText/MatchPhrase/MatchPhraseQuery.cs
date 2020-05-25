// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(FieldNameQueryFormatter<MatchPhraseQuery, IMatchPhraseQuery>))]
	public interface IMatchPhraseQuery : IFieldNameQuery
	{
		[DataMember(Name = "analyzer")]
		string Analyzer { get; set; }

		[DataMember(Name = "query")]
		string Query { get; set; }

		[DataMember(Name = "slop")]
		int? Slop { get; set; }
	}

	public class MatchPhraseQuery : FieldNameQueryBase, IMatchPhraseQuery
	{
		public string Analyzer { get; set; }
		public string Query { get; set; }
		public int? Slop { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.MatchPhrase = this;

		internal static bool IsConditionless(IMatchPhraseQuery q) => q.Field.IsConditionless() || q.Query.IsNullOrEmpty();
	}

	public class MatchPhraseQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<MatchPhraseQueryDescriptor<T>, IMatchPhraseQuery, T>, IMatchPhraseQuery
		where T : class
	{
		protected override bool Conditionless => MatchPhraseQuery.IsConditionless(this);

		string IMatchPhraseQuery.Analyzer { get; set; }
		string IMatchPhraseQuery.Query { get; set; }
		int? IMatchPhraseQuery.Slop { get; set; }

		public MatchPhraseQueryDescriptor<T> Query(string query) => Assign(query, (a, v) => a.Query = v);

		public MatchPhraseQueryDescriptor<T> Analyzer(string analyzer) => Assign(analyzer, (a, v) => a.Analyzer = v);

		public MatchPhraseQueryDescriptor<T> Slop(int? slop) => Assign(slop, (a, v) => a.Slop = v);
	}
}
