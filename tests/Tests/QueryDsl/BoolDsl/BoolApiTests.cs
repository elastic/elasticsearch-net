// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.Extensions;

namespace Tests.QueryDsl.BoolDsl;

public class BoolCluster : ClientTestClusterBase
{
	public enum E
	{
		Option1,
		Option2
	}

	protected override void SeedNode()
	{
		var client = Client;
		var index = client.Indices.Create<A>(Infer.Index<A>(), i => i
			.Mappings(m => m
				.Properties(props => props
					.Keyword(p => p.Option)
				)
			)
		);

		var bulkResponse = client.Bulk(b => b.IndexMany(A.Documents));

		if (!bulkResponse.IsValidResponse)
			throw new Exception("Could not bootstrap bool cluster, bulk was invalid.");

		client.Indices.Refresh(Infer.Indices<A>());
	}

	public class A
	{
		private static readonly E[] Options = new[] { E.Option1, E.Option2 };
		public static IList<A> Documents => Enumerable.Range(0, 20).Select(i => new A { Id = i + 1, Option = Options[i % 2] }).ToList();
		public int Id { get; set; }
		public E Option { get; set; }
	}
}
public class BoolsInPractice : IClusterFixture<BoolCluster>
{
	private readonly BoolCluster _cluster;

	public BoolsInPractice(BoolCluster cluster) => _cluster = cluster;

	private async Task BoolAsync(
		Func<BoolCluster.A, bool> programmatic,
		Query initializerQuery,
		int expectedCount
	)
	{
		var documents = BoolCluster.A.Documents.Where(programmatic).ToList();
		documents.Count.Should().Be(expectedCount, " filtering the documents in memory did not yield the expected count");

		var client = _cluster.Client;

		var initializer = client.Search<BoolCluster.A>(new SearchRequest<BoolCluster.A> { Query = initializerQuery });
		var initializerAsync = await client.SearchAsync<BoolCluster.A>(new SearchRequest<BoolCluster.A> { Query = initializerQuery });

		var responses = new[] { initializer, initializerAsync };
		foreach (var response in responses)
		{
			response.ShouldBeValid();
			response.Total.Should().Be(expectedCount);
		}
	}

	private static TermQuery Id(int id) => new("id") { Value = id };

	private static TermQuery Option(BoolCluster.E option) => new("option") { Value = FieldValue.Composite(option) };

	[I]
	public async Task CompareBoolQueryTranslationsToRealBooleanLogic()
	{
		await BoolAsync(
			a => a.Id == 1 && a.Option == BoolCluster.E.Option1,
			Id(1) && Option(BoolCluster.E.Option1),
			1
		);

		await BoolAsync(
			a => a.Id == 1 || a.Id == 2 || a.Id == 3 || a.Id == 4,
			+Id(1) || +Id(2) || +Id(3) || +Id(4),
			4
		);

		await BoolAsync(
			a => a.Id == 1 || a.Id == 2 || a.Id == 3 || a.Id == 4 && (a.Option != BoolCluster.E.Option1 || a.Option == BoolCluster.E.Option2),
			+Id(1) || +Id(2) || +Id(3) || +Id(4) && (!Option(BoolCluster.E.Option1) || Option(BoolCluster.E.Option2)),
			4
		);

		await BoolAsync(
			a => a.Id == 1 || a.Id == 2 || a.Id == 3 || a.Id == 4 && a.Option != BoolCluster.E.Option1 || a.Option == BoolCluster.E.Option2,
			+Id(1) || +Id(2) || +Id(3) || +Id(4) && !Option(BoolCluster.E.Option1) || Option(BoolCluster.E.Option2),
			12
		);

		await BoolAsync(
			a => a.Option != BoolCluster.E.Option1 && a.Id != 2 && a.Id != 3,
			!Option(BoolCluster.E.Option1) && !Id(2) && !+Id(3),
			9
		);
	}
}
