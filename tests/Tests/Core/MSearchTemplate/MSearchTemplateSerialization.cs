// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Core.MSearch;
using Elastic.Clients.Elasticsearch.Core.MSearchTemplate;
using Elastic.Clients.Elasticsearch.Serialization;
using Tests.Domain;
using VerifyTests;
using VerifyXunit;

namespace Tests.Core.MSearchTemplate;

[UsesVerify]
public class MSearchTemplateSerialization
{
	private readonly VerifySettings _verifySettings;

	public MSearchTemplateSerialization()
	{
		_verifySettings = new VerifySettings();
		_verifySettings.DisableRequireUniquePrefix();
	}

	[U]
	public async Task SerializesMultiSearchTemplateRequest()
	{
		var settings = new ElasticsearchClientSettings();
		settings.DefaultMappingFor<Project>(m => m.IndexName("projects"));

		var ms = new MemoryStream();

		var request = (IStreamSerializable)new MultiSearchTemplateRequest
		{
			SearchTemplates = new List<SearchTemplateRequestItem>
			{
				new SearchTemplateRequestItem(new MultisearchHeader { Index = Infer.Index<Project>() }, new TemplateConfig { Id = "my-search-template", Params = new Dictionary<string, object>
				{
					{ "query_string", "hello world" },
					{ "from", 0 }
				}}),
				new SearchTemplateRequestItem(new MultisearchHeader { Index = Infer.Index<Project>() }, new TemplateConfig { Id = "my-search-template", Params = new Dictionary<string, object>
				{
					{ "query_type", "match_all" }
				}}),
			}
		};

		await request.SerializeAsync(ms, settings);

		ms.Position = 0;
		var reader = new StreamReader(ms);
		var ndjson = reader.ReadToEnd();

		await Verifier.Verify(ndjson, _verifySettings);

		ms = new MemoryStream();
		request.Serialize(ms, settings);

		ms.Position = 0;
		reader = new StreamReader(ms);
		ndjson = reader.ReadToEnd();

		await Verifier.Verify(ndjson, _verifySettings);
	}
}
