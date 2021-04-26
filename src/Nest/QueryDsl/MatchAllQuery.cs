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
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(MatchAllQuery))]
	public interface IMatchAllQuery : IQuery
	{
		/// <summary>
		/// When indexing, a boost value can either be associated on the document level, or per field.
		/// The match all query does not take boosting into account by default. In order to take
		/// boosting into account, the norms_field needs to be provided in order to explicitly specify which
		/// field the boosting will be done on (Note, this will result in slower execution time).
		/// </summary>
		[DataMember(Name ="norm_field")]
		string NormField { get; set; }
	}

	public class MatchAllQuery : QueryBase, IMatchAllQuery
	{
		/// <inheritdoc />
		public string NormField { get; set; }

		protected override bool Conditionless => false;

		internal override void InternalWrapInContainer(IQueryContainer container) => container.MatchAll = this;
	}

	public class MatchAllQueryDescriptor
		: QueryDescriptorBase<MatchAllQueryDescriptor, IMatchAllQuery>
			, IMatchAllQuery
	{
		protected override bool Conditionless => false;

		string IMatchAllQuery.NormField { get; set; }

		/// <inheritdoc />
		public MatchAllQueryDescriptor NormField(string normField) => Assign(normField, (a, v) => a.NormField = v);
	}
}
