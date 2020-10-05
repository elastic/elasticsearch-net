// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Transport;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Document.Multiple.DeleteByQuery
{
	public class DeleteByQueryApiTests
		: ApiIntegrationTestBase<IntrusiveOperationCluster, DeleteByQueryResponse, IDeleteByQueryRequest, DeleteByQueryDescriptor<Project>,
			DeleteByQueryRequest>
	{
		public DeleteByQueryApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			max_docs = Project.Projects.Count,
			query = new
			{
				ids = new
				{
					values = new[] { Project.First.Name, "x" }
				}
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<DeleteByQueryDescriptor<Project>, IDeleteByQueryRequest> Fluent => d => d
			.Index(Indices)
			.IgnoreUnavailable()
			.MaximumDocuments(Project.Projects.Count)
			.Query(q => q
				.Ids(ids => ids
					.Values(Project.First.Name, "x")
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override DeleteByQueryRequest Initializer => new DeleteByQueryRequest(Indices)
		{
			IgnoreUnavailable = true,
			MaximumDocuments = Project.Projects.Count,
			Query = new IdsQuery
			{
				Values = new Id[] { Project.First.Name, "x" }
			}
		};

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath => $"/{CallIsolatedValue}%2C{SecondIndex}/_delete_by_query?ignore_unavailable=true";
		private Nest.Indices Indices => Index(CallIsolatedValue).And(SecondIndex);

		private string SecondIndex => $"{CallIsolatedValue}-clone";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values)
			{
				Client.Indices.Create(index, c => c
					.Settings(s => s
						.NumberOfShards(2)
						.NumberOfReplicas(0)
						.Analysis(DefaultSeeder.ProjectAnalysisSettings)
					)
					.Map<Project>(p => p
						.AutoMap()
						.Properties(DefaultSeeder.ProjectProperties)
					)
				);

				Client.IndexMany(Project.Projects, index);
				var cloneIndex = index + "-clone";
				Client.Indices.Create(cloneIndex);
				Client.Indices.Refresh(Index(index).And(cloneIndex));
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DeleteByQuery(f),
			(client, f) => client.DeleteByQueryAsync(f),
			(client, r) => client.DeleteByQuery(r),
			(client, r) => client.DeleteByQueryAsync(r)
		);

		protected override void OnAfterCall(IElasticClient client) => client.Indices.Refresh(CallIsolatedValue);

		protected override DeleteByQueryDescriptor<Project> NewDescriptor() => new DeleteByQueryDescriptor<Project>(Indices);

		protected override void ExpectResponse(DeleteByQueryResponse response)
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

		protected override object ExpectJson => new { query = new { match_all = new { } } };

		protected override Func<DeleteByQueryDescriptor<Project>, IDeleteByQueryRequest> Fluent => d => d
			.Index(CallIsolatedValue)
			.Query(q => q.MatchAll())
			.WaitForCompletion(false)
			.Conflicts(Conflicts.Proceed);

		protected override DeleteByQueryRequest Initializer => new DeleteByQueryRequest(CallIsolatedValue)
		{
			Query = new MatchAllQuery(),
			WaitForCompletion = false,
			Conflicts = Conflicts.Proceed
		};

		protected override string UrlPath => $"/{CallIsolatedValue}/_delete_by_query?wait_for_completion=false&conflicts=proceed";

		protected override DeleteByQueryDescriptor<Project> NewDescriptor() => new DeleteByQueryDescriptor<Project>(CallIsolatedValue);

		protected override void ExpectResponse(DeleteByQueryResponse response)
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

		protected override bool ExpectIsValid => false;

		protected override object ExpectJson =>
			new
			{
				query = new { match = new { description = new { query = "description" } } }
			};

		protected override int ExpectStatusCode => 409;

		protected override Func<DeleteByQueryDescriptor<Project>, IDeleteByQueryRequest> Fluent => d => d
			.Index(CallIsolatedValue)
			.Query(q => q
				.Match(m => m
					.Field(p => p.Description)
					.Query("description")
				)
			);

		protected override DeleteByQueryRequest Initializer => new DeleteByQueryRequest(CallIsolatedValue)
		{
			Query = new MatchQuery
			{
				Field = Field<Project>(p => p.Description),
				Query = "description"
			},
		};

		protected override string UrlPath => $"/{CallIsolatedValue}/_delete_by_query";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values)
			{
				Client.Indices.Create(index, c => c
					.Settings(s => s
						.RefreshInterval(-1)
					)
				);
				Client.Index(new Project { Name = "project1", Description = "description" },
					i => i.Index(index).Id(1).Refresh(Refresh.True).Routing(1));
				Client.Index(new Project { Name = "project2", Description = "description" },
					i => i.Index(index).Id(1).Routing(1));
			}
		}

		protected override DeleteByQueryDescriptor<Project> NewDescriptor() => new DeleteByQueryDescriptor<Project>(CallIsolatedValue);

		protected override void ExpectResponse(DeleteByQueryResponse response)
		{
			response.VersionConflicts.Should().Be(1);
			response.Failures.Should().NotBeEmpty();
			var failure = response.Failures.First();

			failure.Index.Should().NotBeNullOrWhiteSpace();
			failure.Status.Should().Be(409);
			failure.Id.Should().NotBeNullOrWhiteSpace();

			failure.Cause.Should().NotBeNull();
			failure.Cause.IndexUUID.Should().NotBeNullOrWhiteSpace();
			failure.Cause.Reason.Should().NotBeNullOrWhiteSpace();
			failure.Cause.Index.Should().NotBeNullOrWhiteSpace();
			failure.Cause.Shard.Should().NotBeNull();
			failure.Cause.Type.Should().NotBeNullOrWhiteSpace();
		}
	}

	public class DeleteByQueryWithSlicesApiTests : DeleteByQueryApiTests
	{
		public DeleteByQueryWithSlicesApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson =>
			new
			{
				slice = new { id = 0, max = 2 },
				query = new { terms = new { name = FirstTenProjectNames } }
			};

		protected override int ExpectStatusCode => 200;

		protected override Func<DeleteByQueryDescriptor<Project>, IDeleteByQueryRequest> Fluent => d => d
			.Index(CallIsolatedValue)
			.Slice(s => s
				.Id(0)
				.Max(2)
			)
			.Query(q => q
				.Terms(m => m
					.Field(p => p.Name)
					.Terms(FirstTenProjectNames)
				)
			);

		protected override DeleteByQueryRequest Initializer => new DeleteByQueryRequest(CallIsolatedValue)
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

		protected override string UrlPath => $"/{CallIsolatedValue}/_delete_by_query";
		private static List<string> FirstTenProjectNames => Project.Projects.Take(10).Select(p => p.Name).ToList();

		protected override DeleteByQueryDescriptor<Project> NewDescriptor() => new DeleteByQueryDescriptor<Project>(CallIsolatedValue);

		protected override void ExpectResponse(DeleteByQueryResponse response)
		{
			response.ShouldBeValid();
			response.SliceId.Should().Be(0);

			// Since we only executed one slice of the two, some of the documents that
			// match the query will still exist.
			Client.Indices.Refresh(CallIsolatedValue);

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
