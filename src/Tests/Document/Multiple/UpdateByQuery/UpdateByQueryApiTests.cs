using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Document.Multiple.UpdateByQuery
{
	[Collection(IntegrationContext.OwnIndex)]
	[SkipVersion("<2.3.0", "")]
	public class UpdateByQueryApiTests : ApiIntegrationTestBase<IUpdateByQueryResponse, IUpdateByQueryRequest, UpdateByQueryDescriptor<UpdateByQueryApiTests.Test>, UpdateByQueryRequest>
	{
		public class Test
		{
			public string Text { get; set; }
			public string Flag { get; set; }
		}

		public UpdateByQueryApiTests(OwnIndexCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values)
			{
				this.Client.CreateIndex(index, c => c
					.Mappings(m => m
						.Map<Test>(map => map
							.Dynamic(false)
							.Properties(props => props
								.String(s => s.Name(p => p.Text))
							)
						)
					)
				);
				this.Client.Index(new Test { Text = "words words", Flag = "bar" }, i=>i.Index(index).Refresh());
				this.Client.Index(new Test { Text = "words words", Flag = "foo" }, i=>i.Index(index).Refresh());
				this.Client.Map<Test>(m => m
					.Index(index)
					.Properties(props => props
						.String(s => s.Name(p => p.Text))
						.String(s => s.Name(p => p.Flag).Analyzer("keyword"))
					)
				);

				var searchResults = this.SearchFlags(index);
				searchResults.Total.Should().Be(0);
			}
		}
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.UpdateByQuery(CallIsolatedValue, Type<Test>(), f),
			fluentAsync: (client, f) => client.UpdateByQueryAsync(CallIsolatedValue, Type<Test>(), f),
			request: (client, r) => client.UpdateByQuery(r),
			requestAsync: (client, r) => client.UpdateByQueryAsync(r)
		);
		protected override void OnAfterCall(IElasticClient client) => client.Refresh(CallIsolatedValue);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => $"/{CallIsolatedValue}/test/_update_by_query?refresh=true&conflicts=proceed";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new { };

		protected override UpdateByQueryDescriptor<Test> NewDescriptor() => new UpdateByQueryDescriptor<Test>(CallIsolatedValue).Type<Test>();

		protected override Func<UpdateByQueryDescriptor<Test>, IUpdateByQueryRequest> Fluent => d => d
			.Refresh()
			.Conflicts(Conflicts.Proceed);

		protected override UpdateByQueryRequest Initializer => new UpdateByQueryRequest(CallIsolatedValue, Type<Test>())
		{
			Refresh = true,
			Conflicts = Conflicts.Proceed
		};

		private ISearchResponse<Test> SearchFlags(string index) =>
			this.Client.Search<Test>(s => s
				.Index(index)
				.Query(q => q.Match(m => m.Field(p => p.Flag).Query("foo")))
			);

		protected override void ExpectResponse(IUpdateByQueryResponse response)
		{
			response.Task.Should().BeNull();
			response.Took.Should().BeGreaterThan(0);
			response.Total.Should().Be(2);
			response.Updated.Should().Be(2);
			response.Batches.Should().Be(1);

			var searchResponse = this.SearchFlags(CallIsolatedValue);
			searchResponse.Total.Should().Be(1);
		}
	}

	[Collection(IntegrationContext.OwnIndex)]
	[SkipVersion("<2.3.0", "")]
	public class UpdateByQueryWaitForCompletionApiTests : UpdateByQueryApiTests
	{
		public UpdateByQueryWaitForCompletionApiTests(OwnIndexCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override string UrlPath => $"/{CallIsolatedValue}/test/_update_by_query?wait_for_completion=false&conflicts=proceed";

		protected override Func<UpdateByQueryDescriptor<Test>, IUpdateByQueryRequest> Fluent => d => d
			.WaitForCompletion(false)
			.Conflicts(Conflicts.Proceed);

		protected override UpdateByQueryRequest Initializer => new UpdateByQueryRequest(CallIsolatedValue, Type<Test>())
		{
			WaitForCompletion = false,
			Conflicts = Conflicts.Proceed
		};

		protected override void ExpectResponse(IUpdateByQueryResponse response)
		{
			response.Task.Should().NotBeNull();
			response.Task.TaskNumber.Should().BeGreaterThan(0);
			response.Task.NodeId.Should().NotBeNullOrWhiteSpace();
			response.Task.FullyQualifiedId.Should().NotBeNullOrWhiteSpace();
		}
	}

	[Collection(IntegrationContext.OwnIndex)]
	[SkipVersion("<2.3.0", "")]
	public class UpdateByQueryWithFailuresApiTests : UpdateByQueryApiTests
	{
		public UpdateByQueryWithFailuresApiTests(OwnIndexCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values)
			{
				this.Client.CreateIndex(index, c => c
					.Settings(s=>s
						.RefreshInterval(-1)
					)
				);
				this.Client.Index(new Test { Text = "test1", Flag = "bar" }, i=>i.Index(index).Id(1).Refresh());
				this.Client.Index(new Test { Text = "test2", Flag = "bar" }, i=>i.Index(index).Id(1));
			}
		}
		private static string _script = "ctx._source.text = 'x'";

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 409;

		protected override string UrlPath => $"/{CallIsolatedValue}/test/_update_by_query";
		protected override object ExpectJson { get; } =
			new
			{
				query = new { match = new { flag = new { query = "bar" } } },
				script = new { inline = "ctx._source.text = 'x'" }
			};

		protected override Func<UpdateByQueryDescriptor<Test>, IUpdateByQueryRequest> Fluent => d => d
			.Query(q=>q.Match(m=>m.Field(p=>p.Flag).Query("bar")))
			.Script(_script)
			;

		protected override UpdateByQueryRequest Initializer => new UpdateByQueryRequest(CallIsolatedValue, Type<Test>())
		{
			Query = new MatchQuery
			{
				Field = Field<Test>(p=>p.Flag),
				Query = "bar"
			},
			Script = new InlineScript(_script),
		};

		protected override void ExpectResponse(IUpdateByQueryResponse response)
		{
			response.VersionConflicts.Should().Be(1);
			response.Failures.Should().NotBeEmpty();
			var failure = response.Failures.First();
			failure.Id.Should().NotBeNullOrWhiteSpace();
			failure.Index.Should().NotBeNullOrWhiteSpace();
			failure.Cause.Should().NotBeNull();
			failure.Cause.Type.Should().NotBeNullOrWhiteSpace();
			failure.Cause.Reason.Should().NotBeNullOrWhiteSpace();
		}
	}
}
