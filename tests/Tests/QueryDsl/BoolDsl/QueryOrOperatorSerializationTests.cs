// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Domain;
using Tests.Serialization;
using VerifyXunit;

namespace Tests.QueryDsl.BoolDsl;

[UsesVerify]
public class QueryOrOperatorSerializationTests : SerializerTestBase
{
	[U]
	public async Task SearchQueriesCombinedWithOrOperation_CanSerialize()
	{
		var search = new SearchRequest<Project>
		{
			Query = new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "x" } ||
					new TermQuery(Infer.Field<Project>(p => p.Name)) { Value = "y" }
		};

		var serialisedJson = await SerializeAndGetJsonStringAsync(search);

		await Verifier.VerifyJson(serialisedJson);
	}

	[U]
	public async Task SearchQueryDescriptorsCombinedWithOrOperation_CanSerialize()
	{
		var search = new SearchRequestDescriptor<Project>()
			.Query(q => q
				.Term(p => p.Name, "x") || q
				.Term(p => p.Name, "y"));

		var serialisedJson = await SerializeAndGetJsonStringAsync(search);

		await Verifier.VerifyJson(serialisedJson);
	}
}
