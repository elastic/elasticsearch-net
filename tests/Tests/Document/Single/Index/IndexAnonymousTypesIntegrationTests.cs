// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.Extensions;
using Tests.Framework.DocumentationTests;

namespace Tests.Document.Single.Index;
public class IndexAnonymousTypesIntegrationTests : IntegrationDocumentationTestBase, IClusterFixture<WritableCluster>
{
	public IndexAnonymousTypesIntegrationTests(WritableCluster cluster) : base(cluster) { }

	[I]
	public void Index()
	{
		var index = RandomString();
		var anonymousType = new
		{
			name = "name",
			value = 3,
			date = new DateTime(2016, 1, 1),
			child = new
			{
				child_name = "child_name",
				child_value = 3
			}
		};

		var indexResult = Client.Index(anonymousType, f => f
			.Index(index)
			.Id(anonymousType.name)
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
	}
}
