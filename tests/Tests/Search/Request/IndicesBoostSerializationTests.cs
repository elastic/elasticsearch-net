// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Client;

namespace Tests.Search.Request
{
	public class IndicesBoostSerializationTests
	{
		[U] public void CanDeserializeArrayFormat()
		{
			var json = "{\"indices_boost\": [{\"project\":1.4},{\"devs\":1.3}]}";

			using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
			{
				var searchRequest = TestClient.Default.RequestResponseSerializer.Deserialize<SearchRequest>(stream);

				searchRequest.Should().NotBeNull();
				searchRequest.IndicesBoost.Should().NotBeNull().And.ContainKeys((IndexName)"project", (IndexName)"devs");
			}
		}

		[U] public void CanDeserializeObjectFormat()
		{
			var json = "{\"indices_boost\": {\"project\":1.4,\"devs\":1.3}}";

			using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
			{
				var searchRequest = TestClient.Default.RequestResponseSerializer.Deserialize<SearchRequest>(stream);

				searchRequest.Should().NotBeNull();
				searchRequest.IndicesBoost.Should().NotBeNull().And.ContainKeys((IndexName)"project", (IndexName)"devs");
			}
		}
	}
}
