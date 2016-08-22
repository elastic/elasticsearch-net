using System;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Indices.Warmers
{
	public class WarmerCrudTests
		: CrudTestBase<IPutWarmerResponse, IGetWarmerResponse, IPutWarmerResponse, IDeleteWarmerResponse>
	{
		private static readonly string WarmerIndexName = Guid.NewGuid().ToString("N").Substring(8);
		public WarmerCrudTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
			var createIndex = this.Client.CreateIndex(WarmerIndexName);
			var waitForIndex = this.Client.ClusterHealth(c=>c
				.WaitForStatus(WaitForStatus.Yellow)
				.Index(WarmerIndexName)
			);
		}

		protected override LazyResponses Create() => Calls<PutWarmerDescriptor, PutWarmerRequest, IPutWarmerRequest, IPutWarmerResponse>(
			CreateInitializer,
			CreateFluent,
			fluent: (s, c, f) => c.PutWarmer(s, f),
			fluentAsync: (s, c, f) => c.PutWarmerAsync(s, f),
			request: (s, c, r) => c.PutWarmer(r),
			requestAsync: (s, c, r) => c.PutWarmerAsync(r)
		);

		protected PutWarmerRequest CreateInitializer(string name) => new PutWarmerRequest(WarmerIndexName, name)
		{
			Search = new SearchRequest<Project>
			{
				Query = new QueryContainer(new MatchQuery
				{
					Field = "name",
					Query = "NEST"
				})
			}
		};

		protected IPutWarmerRequest CreateFluent(string name, PutWarmerDescriptor d) => d
			.Index(WarmerIndexName)
			.Search<Project>(s => s
				.Query(q => q
					.Match(m => m
						.Field(p => p.Name)
						.Query("NEST")
					)
				)
			);

		protected override LazyResponses Read() => Calls<GetWarmerDescriptor, GetWarmerRequest, IGetWarmerRequest, IGetWarmerResponse>(
			ReadInitializer,
			ReadFluent,
			fluent: (s, c, f) => c.GetWarmer(f),
			fluentAsync: (s, c, f) => c.GetWarmerAsync(f),
			request: (s, c, r) => c.GetWarmer(r),
			requestAsync: (s, c, r) => c.GetWarmerAsync(r)
		);

		protected GetWarmerRequest ReadInitializer(string name) => new GetWarmerRequest(WarmerIndexName, name);
		protected IGetWarmerRequest ReadFluent(string name, GetWarmerDescriptor d) => d.Index(WarmerIndexName).Name(name);

		protected override LazyResponses Update() => LazyResponses.Empty;

		protected override LazyResponses Delete() => Calls<DeleteWarmerDescriptor, DeleteWarmerRequest, IDeleteWarmerRequest, IDeleteWarmerResponse>(
			DeleteInitializer,
			DeleteFluent,
			fluent: (s, c, f) => c.DeleteWarmer(WarmerIndexName, s),
			fluentAsync: (s, c, f) => c.DeleteWarmerAsync(WarmerIndexName, s),
			request: (s, c, r) => c.DeleteWarmer(r),
			requestAsync: (s, c, r) => c.DeleteWarmerAsync(r)
		);

		protected DeleteWarmerRequest DeleteInitializer(string name) => new DeleteWarmerRequest(WarmerIndexName, name);
		protected IDeleteWarmerRequest DeleteFluent(string name, DeleteWarmerDescriptor d) => null;

		// https://github.com/elastic/elasticsearch/issues/5155
		// Getting a deleted warmer still returns a 200 status code
		protected override async Task GetAfterDeleteIsValid() => await this.AssertOnGetAfterDelete(r => r.IsValid.Should().BeTrue());

		protected override void ExpectAfterCreate(IGetWarmerResponse response)
		{
			response.Indices.Should().NotBeNull().And.HaveCount(1);
			var warmers = response.Indices.Values.FirstOrDefault();
			warmers.Should().NotBeNull().And.HaveCount(1);
			var kvp = warmers.FirstOrDefault();
			kvp.Should().NotBeNull();
			var name = kvp.Key;
			var warmer = kvp.Value;
			warmer.Source.Should().NotBeNull();
			warmer.Source.Query.Should().NotBeNull();
		}
	}
}
