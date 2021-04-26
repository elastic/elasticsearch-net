/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Runtime.Serialization;
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
