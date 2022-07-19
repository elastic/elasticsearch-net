// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Domain;
using VerifyXunit;

namespace Tests.Serialization.Queries;

[UsesVerify]
public class SearchSerializationTests : SerializerTestBase
{
	[U]
	public async Task Search_WithMatchQuery_SerializesInferredField_ForObjectInitializer()
	{
		var container = QueryContainer.Match(new MatchQuery
		{
			Field = Infer.Field<Project>(d => d.Description),
			Query = "testing"
		});

		var json = SerializeAndGetJsonString(container);

		await Verifier.VerifyJson(json);
	}

	[U]
	public async Task Search_WithTermsQuery_Serializes_ForObjectInitializer()
	{
		var container = QueryContainer.Terms(new TermsQuery
		{
			Field = Infer.Field<Project>(d => d.Description),
			Terms = new TermsQueryField(new object[] { "term1", "term2" }),
			Boost = 1.2f
		});

		var json = SerializeAndGetJsonString(container);

		await Verifier.VerifyJson(json);
	}

	[U]
	public async Task Search_WithTermsQuery_Serializes_ForDescriptor()
	{
		var container = new QueryContainerDescriptor<Project>(q => q.
			Terms(t => t
				.Boost(1.2f)
				.Field(f => f.Description)
				.Terms(new TermsQueryField(new object[] { "term1", "term2" }))));

		var json = SerializeAndGetJsonString(container);

		await Verifier.VerifyJson(json);
	}
}
