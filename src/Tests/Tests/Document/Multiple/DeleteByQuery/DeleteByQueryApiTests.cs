using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Document.Multiple.DeleteByQuery
{
	public class DeleteByQueryApiTests : ApiIntegrationTestBase<IntrusiveOperationCluster, IDeleteByQueryResponse, IDeleteByQueryRequest, DeleteByQueryDescriptor<Project>, DeleteByQueryRequest>
	{
		public DeleteByQueryApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values)
			{
				this.Client.CreateIndex(index, c => c
					.Settings(s => s
						.NumberOfShards(2)
						.NumberOfReplicas(0)
						.Analysis(DefaultSeeder.ProjectAnalysisSettings)
					)
					.Mappings(m => m
						.Map<Project>(p => p
							.AutoMap()
							.Properties(DefaultSeeder.ProjectProperties)
						)
					)
				);

				this.Client.IndexMany(Project.Projects, index);
				var cloneIndex = index + "-clone";
				this.Client.CreateIndex(cloneIndex);
				this.Client.Refresh(Index(index).And(cloneIndex));
			}
		}
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.DeleteByQuery(f),
			fluentAsync: (client, f) => client.DeleteByQueryAsync(f),
			request: (client, r) => client.DeleteByQuery(r),
			requestAsync: (client, r) => client.DeleteByQueryAsync(r)
		);
		protected override void OnAfterCall(IElasticClient client) => client.Refresh(CallIsolatedValue);

		private string SecondIndex => $"{CallIsolatedValue}-clone";
		private Nest.Indices Indices => Index(CallIsolatedValue).And(SecondIndex);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => $"/{CallIsolatedValue}%2C{SecondIndex}/doc/_delete_by_query?ignore_unavailable=true";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new
		{
			query = new
			{
				ids = new
				{
					type = new[] { "doc" },
					values = new [] { Project.First.Name, "x" }
				}
			}
		};

		protected override DeleteByQueryDescriptor<Project> NewDescriptor() => new DeleteByQueryDescriptor<Project>(this.Indices);

		protected override Func<DeleteByQueryDescriptor<Project>, IDeleteByQueryRequest> Fluent => d => d
			.Index(this.Indices)
			.IgnoreUnavailable()
			.Query(q=>q
				.Ids(ids=>ids
					.Types(typeof(Project))
					.Values(Project.First.Name, "x")
				)
			);

		protected override DeleteByQueryRequest Initializer => new DeleteByQueryRequest(this.Indices, Type<Project>())
		{
			IgnoreUnavailable = true,
			Query = new IdsQuery
			{
				Types = Types.Type<Project>(),
				Values = new Id[] { Project.First.Name, "x" }
			}
		};

		protected override void ExpectResponse(IDeleteByQueryResponse response)
		{
			response.Took.Should().BeGreaterThan(0);
			response.Total.Should().Be(1);
			response.Deleted.Should().Be(1);

			response.Retries.Should().NotBeNull();
			response.Retries.Bulk.Should().Be(0);
			response.Retries.Search.Should().Be(0);

			response.RequestsPerSecond.Should().Be(-1);

			response.Failures.Should().BeEmpty();
		}
	}


	public class DeleteByQueryWaitForCompletionApiTests : DeleteByQueryApiTests
	{
		public DeleteByQueryWaitForCompletionApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override string UrlPath => $"/{CallIsolatedValue}/doc/_delete_by_query?wait_for_completion=false&conflicts=proceed";

		protected override DeleteByQueryDescriptor<Project> NewDescriptor() => new DeleteByQueryDescriptor<Project>(this.CallIsolatedValue);

		protected override object ExpectJson => new { query = new { match_all = new { } } };

		protected override Func<DeleteByQueryDescriptor<Project>, IDeleteByQueryRequest> Fluent => d => d
			.Index(this.CallIsolatedValue)
			.Query(q=>q.MatchAll())
			.WaitForCompletion(false)
			.Conflicts(Conflicts.Proceed);

		protected override DeleteByQueryRequest Initializer => new DeleteByQueryRequest(this.CallIsolatedValue, Type<Project>())
		{
			Query = new MatchAllQuery(),
			WaitForCompletion = false,
			Conflicts = Conflicts.Proceed
		};

		protected override void ExpectResponse(IDeleteByQueryResponse response)
		{
			response.Task.Should().NotBeNull();
			response.Task.TaskNumber.Should().BeGreaterThan(0);
			response.Task.NodeId.Should().NotBeNullOrWhiteSpace();
			response.Task.FullyQualifiedId.Should().NotBeNullOrWhiteSpace();
		}
	}

	public class DeleteByQueryWithFailuresApiTests : DeleteByQueryApiTests
	{
		public DeleteByQueryWithFailuresApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values)
			{
				this.Client.CreateIndex(index, c => c
					.Settings(s => s
						.RefreshInterval(-1)
					)
				);
				this.Client.Index(new Project { Name = "project1", Description = "description" },
					i => i.Index(index).Id(1).Refresh(Refresh.True).Routing(Project.Routing));
				this.Client.Index(new Project { Name = "project2", Description = "description" },
					i => i.Index(index).Id(1).Routing(Project.Routing));
			}
		}

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 409;

		protected override string UrlPath => $"/{CallIsolatedValue}/doc/_delete_by_query";
		protected override object ExpectJson =>
			new
			{
				query = new { match = new { description = new { query = "description" } } }
			};

		protected override DeleteByQueryDescriptor<Project> NewDescriptor() => new DeleteByQueryDescriptor<Project>(this.CallIsolatedValue);

		protected override Func<DeleteByQueryDescriptor<Project>, IDeleteByQueryRequest> Fluent => d => d
			.Index(this.CallIsolatedValue)
			.Query(q => q
				.Match(m => m
					.Field(p => p.Description)
					.Query("description")
				)
			)
			;

		protected override DeleteByQueryRequest Initializer => new DeleteByQueryRequest(this.CallIsolatedValue, Type<Project>())
		{
			Query = new MatchQuery
			{
				Field = Field<Project>(p => p.Description),
				Query = "description"
			},
		};

		protected override void ExpectResponse(IDeleteByQueryResponse response)
		{
			response.VersionConflicts.Should().Be(1);
			response.Failures.Should().NotBeEmpty();
			var failure = response.Failures.First();

			failure.Index.Should().NotBeNullOrWhiteSpace();
			failure.Type.Should().NotBeNullOrWhiteSpace();
			failure.Status.Should().Be(409);
			failure.Id.Should().NotBeNullOrWhiteSpace();

			failure.Cause.Should().NotBeNull();
			failure.Cause.IndexUniqueId.Should().NotBeNullOrWhiteSpace();
			failure.Cause.Reason.Should().NotBeNullOrWhiteSpace();
			failure.Cause.Index.Should().NotBeNullOrWhiteSpace();
			failure.Cause.Shard.Should().NotBeNull();
			failure.Cause.Type.Should().NotBeNullOrWhiteSpace();
		}
	}

	public class DeleteByQueryWithSlicesApiTests : DeleteByQueryApiTests
	{
		private static List<string> FirstTenProjectNames => Project.Projects.Take(10).Select(p => p.Name).ToList();

		public DeleteByQueryWithSlicesApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override string UrlPath => $"/{CallIsolatedValue}/doc/_delete_by_query";

		protected override object ExpectJson =>
			new
			{
				slice = new { id = 0, max = 2 },
				query = new { terms = new { name = FirstTenProjectNames } }
			};

		protected override DeleteByQueryDescriptor<Project> NewDescriptor() => new DeleteByQueryDescriptor<Project>(this.CallIsolatedValue);

		protected override Func<DeleteByQueryDescriptor<Project>, IDeleteByQueryRequest> Fluent => d => d
			.Index(this.CallIsolatedValue)
			.Slice(s => s
				.Id(0)
				.Max(2)
			)
			.Query(q => q
				.Terms(m => m
					.Field(p => p.Name)
					.Terms(FirstTenProjectNames)
				)
			)
			;

		protected override DeleteByQueryRequest Initializer => new DeleteByQueryRequest(this.CallIsolatedValue, Type<Project>())
		{
			Slice = new SlicedScroll
			{
				Id = 0,
				Max = 2
			},
			Query = new TermsQuery
			{
				Field = Field<Project>(p => p.Name),
				Terms = FirstTenProjectNames
			},
		};

		protected override void ExpectResponse(IDeleteByQueryResponse response)
		{
			response.ShouldBeValid();
			response.SliceId.Should().Be(0);

			// Since we only executed one slice of the two, some of the documents that
			// match the query will still exist.
			Client.Refresh(CallIsolatedValue);

			var countResponse = Client.Count<Project>(c => c
				.Index(CallIsolatedValue)
				.Query(q => q
					.Terms(m => m
						.Field(p => p.Name)
						.Terms(FirstTenProjectNames)
					)
				)
			);

			countResponse.Count.Should().BeGreaterThan(0);
		}
	}
}
