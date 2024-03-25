// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.IO;
using System.Linq;
using System.Reactive.Linq;
using Elastic.Transport.Extensions;
using Tests.Core.Client;
using Tests.Framework.DocumentationTests;

namespace Tests.Document.Multiple;

public class BulkResponseParstingTests : DocumentationTestBase
{
	[U]
	public void CanDeserialize()
	{
		var client = TestClient.DefaultInMemoryClient;
		var count = 100000;
		var bytes = client.RequestResponseSerializer.SerializeToBytes(ReturnBulkResponse(count), SerializationFormatting.None);
		var x = Deserialize(bytes, client);
		x.Items.Should().HaveCount(count).And.NotContain(i => i == null);
	}

	private static BulkResponse Deserialize(byte[] response, ElasticsearchClient client)
	{
		using var ms = new MemoryStream(response);
		return client.RequestResponseSerializer.Deserialize<BulkResponse>(ms);
	}

	private static object BulkItemResponse()
	{
		return new
		{
			index = new
			{
				_index = "nest-52cfd7aa",
				_type = "project",
				_id = "Kuhn LLC",
				_version = 1,
				_shards = new { total = 2, successful = 1, failed = 0 },
				created = true,
				status = 201
			}
		};
	}


	private static object ReturnBulkResponse(int numberOfItems)
	{
		return new
		{
			took = 276,
			errors = false,
			items = Enumerable.Range(0, numberOfItems)
				.Select(i => BulkItemResponse())
				.ToArray()
		};
	}
}
