// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Elastic.Clients.Elasticsearch.Mapping;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Multiple.DeleteByQuery
{
	public class DeleteByQueryApiTests
		: ApiIntegrationTestBase<IntrusiveOperationCluster, DeleteByQueryResponse, DeleteByQueryRequestDescriptor<Project>,
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

		protected override Action<DeleteByQueryRequestDescriptor<Project>> Fluent => d => d
			.IgnoreUnavailable()
			.MaxDocs(Project.Projects.Count)
			.Query(q => q
				.Ids(ids => ids
					.Values(new Ids(new[] { Project.First.Name, "x" }))
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override DeleteByQueryRequest Initializer => new(Indices)
		{
			IgnoreUnavailable = true,
			MaxDocs = Project.Projects.Count,
			Query = QueryContainer.Ids(new IdsQuery
			{
				Values = new Ids(new[] { Project.First.Name, "x" })
			})
		};

		protected override bool SupportsDeserialization => false;

		protected override string ExpectedUrlPathAndQuery => $"/{CallIsolatedValue}%2C{SecondIndex}/_delete_by_query?ignore_unavailable=true";

		private Indices Indices => Infer.Index(CallIsolatedValue).And(SecondIndex);

		private string SecondIndex => $"{CallIsolatedValue}-clone";

		protected override void IntegrationSetup(ElasticsearchClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values)
			{
				// TODO: Reapply settings and mappings once default seeder is updated to use current APIs
				Client.Indices.Create(index, c => c
					.Settings(s => s
						.NumberOfShards(2)
						.NumberOfReplicas(0)
						//.Analysis(DefaultSeeder.ProjectAnalysisSettings) 
					)
					//.Mappings(p => p
					//	//.AutoMap()
					//	.Properties<Project>(DefaultSeeder.ProjectProperties)
					//)
					.Mappings(m => m.Properties(new Properties(new Dictionary<PropertyName, IProperty>
					{
						{ "name", new KeywordProperty() },
					})))
				);

				Client.IndexMany(Project.Projects, index);
				var cloneIndex = index + "-clone";
				Client.Indices.Create(cloneIndex);
				Client.Indices.Refresh(new RefreshRequest(Infer.Index(index).And(cloneIndex)));
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DeleteByQuery(Indices, f),
			(client, f) => client.DeleteByQueryAsync(Indices, f),
			(client, r) => client.DeleteByQuery(r),
			(client, r) => client.DeleteByQueryAsync(r)
		);

		protected override void OnAfterCall(ElasticsearchClient client) => client.Indices.Refresh(new RefreshRequest(CallIsolatedValue));

		protected override DeleteByQueryRequestDescriptor<Project> NewDescriptor() => new(Indices);

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

		protected override Action<DeleteByQueryRequestDescriptor<Project>> Fluent => d => d
			.Indices(CallIsolatedValue)
			.Query(q => q.MatchAll())
			.WaitForCompletion(false)
			.Conflicts(Conflicts.Proceed);

		protected override DeleteByQueryRequest Initializer => new(CallIsolatedValue)
		{
			Query = new MatchAllQuery(),
			WaitForCompletion = false,
			Conflicts = Conflicts.Proceed
		};

		protected override string ExpectedUrlPathAndQuery => $"/{CallIsolatedValue}/_delete_by_query?wait_for_completion=false&conflicts=proceed";

		protected override DeleteByQueryRequestDescriptor<Project> NewDescriptor() => new (CallIsolatedValue);

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

		protected override Action<DeleteByQueryRequestDescriptor<Project>> Fluent => d => d
			.Indices(CallIsolatedValue)
			.Query(q => q
				.Match(m => m
					.Field(p => p.Description)
					.Query("description")
				)
			);

		protected override DeleteByQueryRequest Initializer => new(CallIsolatedValue)
		{
			Query = QueryContainer.Match(new MatchQuery(Infer.Field<Project>(p => p.Description))
			{
				Query = "description"
			})
		};

		protected override string ExpectedUrlPathAndQuery => $"/{CallIsolatedValue}/_delete_by_query";

		protected override void IntegrationSetup(ElasticsearchClient client, CallUniqueValues values)
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

		protected override DeleteByQueryRequestDescriptor<Project> NewDescriptor() => new(CallIsolatedValue);

		protected override void ExpectResponse(DeleteByQueryResponse response)
		{
			response.VersionConflicts.Should().Be(1);
			response.Failures.Should().NotBeEmpty();
			var failure = response.Failures.First();

			failure.Index.Should().NotBeNullOrWhiteSpace();
			failure.Status.Should().Be(409);
			failure.Id.Should().NotBeNullOrWhiteSpace();

			failure.Cause.Should().NotBeNull();
			//failure.Cause.IndexUUID.Should().NotBeNullOrWhiteSpace(); // TODO: Specification may be lacking this property
			failure.Cause.Reason.Should().NotBeNullOrWhiteSpace();
			//failure.Cause.Index.Should().NotBeNullOrWhiteSpace(); // TODO: Specification may be lacking this property
			//failure.Cause.Shard.Should().NotBeNull(); // TODO: Specification may be lacking this property
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

		protected override Action<DeleteByQueryRequestDescriptor<Project>> Fluent => d => d
			.Indices(CallIsolatedValue)
			.Slice(s => s
				.Id(0)
				.Max(2)
			)
			.Query(q => q
				.Terms(m => m
					.Field(p => p.Name)
					.Terms(new TermsQueryField(FirstTenProjectNames))
				)
			);

		protected override DeleteByQueryRequest Initializer => new(CallIsolatedValue)
		{
			Slice = new SlicedScroll
			{
				Id = 0,
				Max = 2
			},
			Query = QueryContainer.Terms(new TermsQuery()
			{
				Field = Infer.Field<Project>(p => p.Name),
				Terms = new TermsQueryField(FirstTenProjectNames)
			})
		};

		protected override string ExpectedUrlPathAndQuery => $"/{CallIsolatedValue}/_delete_by_query";
		private static List<string> FirstTenProjectNames => Project.Projects.Take(10).Select(p => p.Name).ToList();

		protected override DeleteByQueryRequestDescriptor<Project> NewDescriptor() => new(CallIsolatedValue);

		protected override void ExpectResponse(DeleteByQueryResponse response)
		{
			response.ShouldBeValid();
			response.SliceId.Should().Be(0);

			// Since we only executed one slice of the two, some of the documents that
			// match the query will still exist.
			Client.Indices.Refresh(new RefreshRequest(CallIsolatedValue));

			var countResponse = Client.Count<Project>(c => c
				.Indices(CallIsolatedValue)
				.Query(q => q
					.Terms(m => m
						.Field(p => p.Name)
						.Terms(new TermsQueryField(FirstTenProjectNames))
					)
				)
			);

			countResponse.Count.Should().BeGreaterThan(0);
		}
	}
}
