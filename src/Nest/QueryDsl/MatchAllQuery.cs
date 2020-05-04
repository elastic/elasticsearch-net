// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

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
