// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.Extensions;
using Tests.Framework.DocumentationTests;
using System.Linq;
using Tests.Core.Xunit;
using Newtonsoft.Json.Linq;

namespace Tests.Document.Single.Index;

[JsonNetSerializerOnly]
public class IndexJObjectIntegrationTests : IntegrationDocumentationTestBase, IClusterFixture<WritableCluster>
{
	public IndexJObjectIntegrationTests(WritableCluster cluster) : base(cluster) { }

	[I]
	public void Index()
	{
		var index = RandomString();
		var jObjects = Enumerable.Range(1, 1000)
			.Select(i =>
				new JObject
				{
					{ "id", i },
					{ "name", $"name {i}" },
					{ "value", Math.Pow(i, 2) },
					{ "date", new DateTime(2016, 1, 1) },
					{
						"child", new JObject
						{
							{ "child_name", $"child_name {i}{i}" },
							{ "child_value", 3 }
						}
					}
				});

		var jObject = jObjects.First();

		var indexResult = Client.Index(jObject, f => f
			.Index(index)
			.Id(jObject["id"].Value<int>())
		);

		indexResult.ShouldBeValid();
		indexResult.ApiCall.HttpStatusCode.Should().Be(201);
		indexResult.Result.Should().Be(Result.Created);
		indexResult.Index.Should().Be(index);
		indexResult.Shards.Should().NotBeNull();
		indexResult.Shards.Total.Should().BeGreaterOrEqualTo(1);
		indexResult.Shards.Successful.Should().BeGreaterOrEqualTo(1);
		indexResult.SeqNo.Should().BeGreaterOrEqualTo(0);
		indexResult.PrimaryTerm.Should().BeGreaterThan(0);

		var bulkResponse = Client.Bulk(b => b
			.Index(index)
			.IndexMany(jObjects.Skip(1), (bi, d) => bi
				.Id(d["id"].Value<int>())
			)
		);

		foreach (var item in bulkResponse.Items)
		{
			item.IsValid.Should().BeTrue();
			item.Status.Should().Be(201);
			item.Shards.Should().NotBeNull();
			item.Shards.Total.Should().BeGreaterOrEqualTo(1);
			item.Shards.Successful.Should().BeGreaterOrEqualTo(1);
			item.SeqNo.Should().BeGreaterOrEqualTo(0);
			item.PrimaryTerm.Should().BeGreaterThan(0);
		}
	}
}
