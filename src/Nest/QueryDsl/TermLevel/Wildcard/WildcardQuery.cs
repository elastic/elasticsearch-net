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

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

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
		public bool? CaseInsensitive { get; set; }
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
