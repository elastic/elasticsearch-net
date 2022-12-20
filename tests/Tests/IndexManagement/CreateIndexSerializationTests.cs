// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Clients.Elasticsearch.Analysis;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Elastic.Clients.Elasticsearch.QueryDsl;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests.Serialization;
using VerifyXunit;

namespace Tests.IndexManagement;

[UsesVerify]
public class CreateIndexSerializationTests : SerializerTestBase
{
	[U]
	public async Task CreateIndexWithAliases_SerializesCorrectly()
	{
		var alias1 = new Alias();
		var alias2 = new Alias { Filter = Query.Term(new TermQuery("username") { Value = "stevegordon" }), Routing = "shard-1" };

		var descriptor = new CreateIndexRequestDescriptor("test")
			.Aliases(aliases => aliases
				.Add("alias_1", alias1)
				.Add("alias_2", alias2));

		var json = await SerializeAndGetJsonStringAsync(descriptor);

		await Verifier.VerifyJson(json);

		var createRequest = new CreateIndexRequest("test")
		{
			Aliases = new Dictionary<Name, Alias>()
			{
				{  "alias_1", alias1 },
				{  "alias_2", alias2 }
			}
		};

		var objectJson = await SerializeAndGetJsonStringAsync(createRequest);
		objectJson.Should().Be(json);
	}

	[U]
	public async Task CreateIndexWithAnalysisSettings_SerializesCorrectly()
	{
		var descriptor = new CreateIndexRequestDescriptor("test")
			.Settings(s => s
				.Analysis(a => a
					.Analyzers(a => a
						.Stop("stop-name", stop => stop.StopwordsPath("path.txt"))
						.Pattern("pattern-name", pattern => pattern.Version("version"))
						.Custom("my-custom-analyzer", c => c
							.Filter(new[] { "stop", "synonym" })
							.Tokenizer("standard")))
					.TokenFilters(f => f
						.Synonym("synonym", synonym => synonym
							.SynonymsPath("analysis/synonym.txt")))));

		var json = await SerializeAndGetJsonStringAsync(descriptor);

		await Verifier.VerifyJson(json);

		var createRequest = new CreateIndexRequest("test")
		{
			Settings = new IndexSettings
			{
				Analysis = new IndexSettingsAnalysis
				{
					Analyzers = new Analyzers
					{
						{ "stop-name", new StopAnalyzer { StopwordsPath = "path.txt" }},
						{ "pattern-name", new PatternAnalyzer { Version = "version" }},
						{ "my-custom-analyzer", new CustomAnalyzer { Filter = new[] { "stop", "synonym" }, Tokenizer = "standard" }}
					},
					TokenFilters = new TokenFilters {{ "synonym", new SynonymTokenFilter { SynonymsPath = "analysis/synonym.txt" }}}					
				}
			}
		};

		var objectJson = await SerializeAndGetJsonStringAsync(createRequest);
		objectJson.Should().Be(json);
	}
}
