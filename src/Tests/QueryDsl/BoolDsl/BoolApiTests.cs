using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;
using A = Tests.QueryDsl.BoolDsl.BoolCluster.A;
using E = Tests.QueryDsl.BoolDsl.BoolCluster.E;
using static Nest.Infer;


namespace Tests.QueryDsl.BoolDsl
{
	internal static class BoolDslTestExtensions
	{
		public static QueryContainer Id(this QueryContainerDescriptor<A> q, int id) => q.Term(p => p.Id, id);
		public static QueryContainer O(this QueryContainerDescriptor<A> q, E option) => q.Term(p => p.Option, option);
	}

	[CollectionDefinition(IntegrationContext.Bool)]
	public class BoolCluster : ClusterBase, ICollectionFixture<BoolCluster>
	{
		//TODO discuss make this default?
		[JsonConverter(typeof(StringEnumConverter))]
		public enum E { Option1, Option2 }

		public class A
		{
			public int Id { get; set; }
			public E Option { get; set; }

			private static E[] Options = new[] { E.Option1, E.Option2 };
			public static IList<A> Documents => Enumerable.Range(0, 20).Select(i => new A { Id = i + 1, Option = Options[i % 2] }).ToList();
		}

		public override void Boostrap()
		{
			var client = this.Client();
			var index = client.CreateIndex(Index<A>(), i => i
				.Mappings(map => map
					.Map<A>(m => m
						.AutoMap()
						.Properties(props => props
							.String(s => s.Name(p => p.Option).NotAnalyzed())
						)
					)
				)
			);
			var bulkResponse = client.Bulk(b => b.IndexMany(A.Documents));
			if (!bulkResponse.IsValid) throw new Exception("Could not bootstrap bool cluster, bulk was invalid");
			client.Refresh(Indices<A>());
		}
	}

	[Collection(IntegrationContext.Bool)]
	public class BoolsInPractice
	{

		private readonly BoolCluster _cluster;

		public BoolsInPractice(BoolCluster cluster)
		{
			this._cluster = cluster;
		}

		private async Task Bool(
			Func<A, bool> programmatic,
			Func<QueryContainerDescriptor<A>, QueryContainer> fluentQuery,
			QueryContainer initializerQuery,
			int expectedCount
			)
		{
			var documents = A.Documents.Where(programmatic).ToList();
			documents.Count().Should().Be(expectedCount, " filtering the documents in memory did not yield the expected count");

			var client = this._cluster.Client();

			var fluent = client.Search<A>(s => s.Query(fluentQuery));
			var fluentAsync = await client.SearchAsync<A>(s => s.Query(fluentQuery));

			var initializer = client.Search<A>(new SearchRequest<A> { Query = initializerQuery });
			var initializerAsync = await client.SearchAsync<A>(new SearchRequest<A> { Query = initializerQuery });

			var responses = new[] { fluent, fluentAsync, initializer, initializerAsync };
			foreach (var response in responses)
			{
				response.IsValid.Should().BeTrue();
				response.Total.Should().Be(expectedCount);
			}
		}

		private TermQuery Id(int id) => new TermQuery { Field = "id", Value = id };
		private TermQuery O(E option) => new TermQuery { Field = "option", Value = option };

		[I]
		public async Task CompareBoolQueryTranslationsToRealBooleanLogic()
		{
			await Bool(
				a => a.Id == 1 && a.Option == E.Option1,
				a => a.Id(1) && a.O(E.Option1),
				Id(1) && O(E.Option1),
				expectedCount: 1
			);

			await Bool(
				a => a.Id == 1 || a.Id == 2 || a.Id == 3 || a.Id == 4,
				a => +a.Id(1) || +a.Id(2) || +a.Id(3) || +a.Id(4),
				+Id(1) || +Id(2) || +Id(3) || +Id(4),
				expectedCount: 4
			);

			await Bool(
				a => a.Id == 1 || a.Id == 2 || a.Id == 3 || a.Id == 4 && (a.Option != E.Option1 || a.Option == E.Option2),
				a => +a.Id(1) || +a.Id(2) || +a.Id(3) || +a.Id(4) && (!a.O(E.Option1) || a.O(E.Option2)),
				+Id(1) || +Id(2) || +Id(3) || +Id(4) && (!O(E.Option1) || O(E.Option2)),
				expectedCount: 4
			);

			await Bool(
				a => a.Id == 1 || a.Id == 2 || a.Id == 3 || a.Id == 4 && a.Option != E.Option1 || a.Option == E.Option2,
				a => +a.Id(1) || +a.Id(2) || +a.Id(3) || +a.Id(4) && !a.O(E.Option1) || a.O(E.Option2),
				+Id(1) || +Id(2) || +Id(3) || +Id(4) && !O(E.Option1) || O(E.Option2),
				expectedCount: 12
			);

			await Bool(
				a => a.Option != E.Option1 && a.Id != 2 && a.Id != 3,
				a => !a.O(E.Option1) && !a.Id(2) && !+a.Id(3),
				!O(E.Option1) && !Id(2) && !+Id(3),
				expectedCount: 9
			);

		}
	}
}
