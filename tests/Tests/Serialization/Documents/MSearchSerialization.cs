// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Tests.Domain;
using VerifyTests;
using VerifyXunit;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Tests.Serialization.Documents;

[UsesVerify]
public class MSearchSerialization
{
	private readonly VerifySettings _verifySettings;

	public MSearchSerialization()
	{
		_verifySettings = new VerifySettings();
		_verifySettings.DisableRequireUniquePrefix();
	}

	[U]
	public async Task SerializesRequest()
	{
		var settings = new ElasticsearchClientSettings();
		settings.DefaultMappingFor<Project>(m => m.IndexName("projects"));

		var ms = new MemoryStream();

		var request = (IStreamSerializable)new MultiSearchRequest
		{
			Searches = new List<SearchRequestItem>
			{
				//new SearchRequestItem(new MultisearchHeader { Index = Infer.Index<Project>() }, new MultisearchBody { From = 0, Query = new MatchAllQuery() }),
				//new SearchRequestItem(new MultisearchHeader { Index = Infer.Index<Project>() }, new MultisearchBody { From = 0, Query = new MatchAllQuery() })
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
