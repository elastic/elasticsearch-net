// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Slm
{
	[SkipVersion("<7.5.0", "All APIs exist in Elasticsearch 7.4.0, Retention, Status, Start and Stop added in 7.5.0")]
	public class SlmApiTests : CoordinatedIntegrationTestBase<SlmCluster>
	{
		private const string CreateRepositoryStep = nameof(CreateRepositoryStep);
		private const string DeleteSnapshotLifecycleStep = nameof(DeleteSnapshotLifecycleStep);
		private const string ExecuteSnapshotLifecycleStep = nameof(ExecuteSnapshotLifecycleStep);
		private const string ExecuteRetentionLifecycleStep = nameof(ExecuteRetentionLifecycleStep);
		private const string GetAllSnapshotLifecycleStep = nameof(GetAllSnapshotLifecycleStep);
		private const string GetSnapshotLifecycleStep = nameof(GetSnapshotLifecycleStep);
		private const string GetMissingSnapshotLifecycleStep = nameof(GetMissingSnapshotLifecycleStep);
		private const string GetSnapshotLifecycleStatusStep = nameof(GetSnapshotLifecycleStatusStep);
		private const string GetSnapshotLifecycleAfterExecuteStep = nameof(GetSnapshotLifecycleAfterExecuteStep);
		private const string PutSnapshotLifecycleStep = nameof(PutSnapshotLifecycleStep);
		private const string StopSnapshotLifecycleStep = nameof(StopSnapshotLifecycleStep);
		private const string GetSnapshotLifecycleStats = nameof(GetSnapshotLifecycleStats);

		public SlmApiTests(SlmCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage)
		{
			{
				CreateRepositoryStep, u =>
					u.Calls<CreateRepositoryDescriptor, CreateRepositoryRequest, ICreateRepositoryRequest, CreateRepositoryResponse>(
						v => new CreateRepositoryRequest(v)
						{
							Repository = new FileSystemRepository(new FileSystemRepositorySettings(cluster.FileSystem.RepositoryPath))
						},
						(v, d) => d.FileSystem(f => f.Settings(cluster.FileSystem.RepositoryPath)),
						(v, c, f) => c.Snapshot.CreateRepository(v, f),
						(v, c, f) => c.Snapshot.CreateRepositoryAsync(v, f),
						(v, c, r) => c.Snapshot.CreateRepository(r),
						(v, c, r) => c.Snapshot.CreateRepositoryAsync(r)
					)
			},
			{
				PutSnapshotLifecycleStep, u =>
					u.Calls<PutSnapshotLifecycleDescriptor, PutSnapshotLifecycleRequest, IPutSnapshotLifecycleRequest, PutSnapshotLifecycleResponse>(
						v => new PutSnapshotLifecycleRequest(v)
						{
							Name = v,
							Repository = v,
							Schedule = "0 0 0 1 1 ? *",
							Config = new SnapshotLifecycleConfig
							{
								Indices = typeof(Project)
							},
							Retention = new SnapshotRetentionConfig
							{
								ExpireAfter = "30d",
								MinimumCount = 1,
								MaximumCount = 5
							}
						},
						(v, d) => d
							.Name(v)
							.Repository(v)
							.Schedule("0 0 0 1 1 ? *")
							.Config(c => c
								.Indices<Project>()
							)
							.Retention(r => r
								.ExpireAfter("30d")
								.MinimumCount(1)
								.MaximumCount(5)
							),
						(v, c, f) => c.SnapshotLifecycleManagement.PutSnapshotLifecycle(v, f),
						(v, c, f) => c.SnapshotLifecycleManagement.PutSnapshotLifecycleAsync(v, f),
						(v, c, r) => c.SnapshotLifecycleManagement.PutSnapshotLifecycle(r),
						(v, c, r) => c.SnapshotLifecycleManagement.PutSnapshotLifecycleAsync(r)
					)
			},
			{
				GetMissingSnapshotLifecycleStep, u =>
					u.Calls<GetSnapshotLifecycleDescriptor, GetSnapshotLifecycleRequest, IGetSnapshotLifecycleRequest, GetSnapshotLifecycleResponse>(
						v => new GetSnapshotLifecycleRequest(v + "-missing")
						{
							Human = true
						},
						(v, d) => d.PolicyId(v + "-missing").Human(),
						(v, c, f) => c.SnapshotLifecycleManagement.GetSnapshotLifecycle(f),
						(v, c, f) => c.SnapshotLifecycleManagement.GetSnapshotLifecycleAsync(f),
						(v, c, r) => c.SnapshotLifecycleManagement.GetSnapshotLifecycle(r),
						(v, c, r) => c.SnapshotLifecycleManagement.GetSnapshotLifecycleAsync(r)
					)
			},
			{
				GetSnapshotLifecycleStep, u =>
					u.Calls<GetSnapshotLifecycleDescriptor, GetSnapshotLifecycleRequest, IGetSnapshotLifecycleRequest, GetSnapshotLifecycleResponse>(
						v => new GetSnapshotLifecycleRequest(v)
						{
							Human = true
						},
						(v, d) => d.PolicyId(v).Human(),
						(v, c, f) => c.SnapshotLifecycleManagement.GetSnapshotLifecycle(f),
						(v, c, f) => c.SnapshotLifecycleManagement.GetSnapshotLifecycleAsync(f),
						(v, c, r) => c.SnapshotLifecycleManagement.GetSnapshotLifecycle(r),
						(v, c, r) => c.SnapshotLifecycleManagement.GetSnapshotLifecycleAsync(r)
					)
			},
			{
				GetSnapshotLifecycleStatusStep, u => u.Calls<GetSnapshotLifecycleManagementStatusDescriptor, GetSnapshotLifecycleManagementStatusRequest, IGetSnapshotLifecycleManagementStatusRequest, GetSnapshotLifecycleManagementStatusResponse>(
					v => new GetSnapshotLifecycleManagementStatusRequest(),
					(v, d) => d,
					(v, c, f) => c.SnapshotLifecycleManagement.GetStatus(f),
					(v, c, f) => c.SnapshotLifecycleManagement.GetStatusAsync(f),
					(v, c, r) => c.SnapshotLifecycleManagement.GetStatus(r),
					(v, c, r) => c.SnapshotLifecycleManagement.GetStatusAsync(r)
				)
			},
			{
				GetAllSnapshotLifecycleStep, u =>
					u.Calls<GetSnapshotLifecycleDescriptor, GetSnapshotLifecycleRequest, IGetSnapshotLifecycleRequest, GetSnapshotLifecycleResponse>(
						v => new GetSnapshotLifecycleRequest(),
						(v, d) => d,
						(v, c, f) => c.SnapshotLifecycleManagement.GetSnapshotLifecycle(f),
						(v, c, f) => c.SnapshotLifecycleManagement.GetSnapshotLifecycleAsync(f),
						(v, c, r) => c.SnapshotLifecycleManagement.GetSnapshotLifecycle(r),
						(v, c, r) => c.SnapshotLifecycleManagement.GetSnapshotLifecycleAsync(r)
					)
			},
			{
				ExecuteSnapshotLifecycleStep, u =>
					u.Calls<ExecuteSnapshotLifecycleDescriptor, ExecuteSnapshotLifecycleRequest, IExecuteSnapshotLifecycleRequest,
						ExecuteSnapshotLifecycleResponse>(
						v => new ExecuteSnapshotLifecycleRequest(v),
						(v, d) => d,
						(v, c, f) => c.SnapshotLifecycleManagement.ExecuteSnapshotLifecycle(v, f),
						(v, c, f) => c.SnapshotLifecycleManagement.ExecuteSnapshotLifecycleAsync(v, f),
						(v, c, r) => c.SnapshotLifecycleManagement.ExecuteSnapshotLifecycle(r),
						(v, c, r) => c.SnapshotLifecycleManagement.ExecuteSnapshotLifecycleAsync(r)
					)
			},
			{
				ExecuteRetentionLifecycleStep, u =>
					u.Calls<ExecuteRetentionDescriptor, ExecuteRetentionRequest, IExecuteRetentionRequest,
						ExecuteRetentionResponse>(
						v => new ExecuteRetentionRequest(),
						(v, d) => d,
						(v, c, f) => c.SnapshotLifecycleManagement.ExecuteRetention(f),
						(v, c, f) => c.SnapshotLifecycleManagement.ExecuteRetentionAsync(f),
						(v, c, r) => c.SnapshotLifecycleManagement.ExecuteRetention(r),
						(v, c, r) => c.SnapshotLifecycleManagement.ExecuteRetentionAsync(r)
					)
			},
			{
				GetSnapshotLifecycleAfterExecuteStep, u =>
					u.Calls<GetSnapshotLifecycleDescriptor, GetSnapshotLifecycleRequest, IGetSnapshotLifecycleRequest, GetSnapshotLifecycleResponse>(
						v => new GetSnapshotLifecycleRequest(v)
						{
							Human = true
						},
						(v, d) => d.PolicyId(v).Human(),
						(v, c, f) => c.SnapshotLifecycleManagement.GetSnapshotLifecycle(f),
						(v, c, f) => c.SnapshotLifecycleManagement.GetSnapshotLifecycleAsync(f),
						(v, c, r) => c.SnapshotLifecycleManagement.GetSnapshotLifecycle(r),
						(v, c, r) => c.SnapshotLifecycleManagement.GetSnapshotLifecycleAsync(r)
					)
			},
			{
				GetSnapshotLifecycleStats, u =>
					u.Calls<GetSnapshotLifecycleStatsDescriptor, GetSnapshotLifecycleStatsRequest, IGetSnapshotLifecycleStatsRequest, GetSnapshotLifecycleStatsResponse>(
						v => new GetSnapshotLifecycleStatsRequest(),
						(v, d) => d,
						(v, c, f) => c.SnapshotLifecycleManagement.GetSnapshotLifecycleStats(f),
						(v, c, f) => c.SnapshotLifecycleManagement.GetSnapshotLifecycleStatsAsync(f),
						(v, c, r) => c.SnapshotLifecycleManagement.GetSnapshotLifecycleStats(r),
						(v, c, r) => c.SnapshotLifecycleManagement.GetSnapshotLifecycleStatsAsync(r)
					)
			},
			{
				StopSnapshotLifecycleStep, u => u.Calls<StopSnapshotLifecycleManagementDescriptor, StopSnapshotLifecycleManagementRequest, IStopSnapshotLifecycleManagementRequest, StopSnapshotLifecycleManagementResponse>(
					v => new StopSnapshotLifecycleManagementRequest(),
					(v, d) => d,
					(v, c, f) => c.SnapshotLifecycleManagement.Stop(f),
					(v, c, f) => c.SnapshotLifecycleManagement.StopAsync(f),
					(v, c, r) => c.SnapshotLifecycleManagement.Stop(r),
					(v, c, r) => c.SnapshotLifecycleManagement.StopAsync(r)
				)
			},
			{
				DeleteSnapshotLifecycleStep, u =>
					u.Calls<DeleteSnapshotLifecycleDescriptor, DeleteSnapshotLifecycleRequest, IDeleteSnapshotLifecycleRequest,
						DeleteSnapshotLifecycleResponse>(
						v => new DeleteSnapshotLifecycleRequest(v),
						(v, d) => d,
						(v, c, f) => c.SnapshotLifecycleManagement.DeleteSnapshotLifecycle(v, f),
						(v, c, f) => c.SnapshotLifecycleManagement.DeleteSnapshotLifecycleAsync(v, f),
						(v, c, r) => c.SnapshotLifecycleManagement.DeleteSnapshotLifecycle(r),
						(v, c, r) => c.SnapshotLifecycleManagement.DeleteSnapshotLifecycleAsync(r)
					)
			},
		}) { }

		[I] public async Task CreateRepositoryResponse() => await Assert<CreateRepositoryResponse>(CreateRepositoryStep, (v, r) =>
		{
			r.IsValid.Should().BeTrue();
			r.Acknowledged.Should().BeTrue();
			r.ApiCall.HttpStatusCode.Should().Be(200);
		});

		[I] public async Task PutSnapshotLifecycleResponse() => await Assert<PutSnapshotLifecycleResponse>(PutSnapshotLifecycleStep, (v, r) =>
		{
			r.IsValid.Should().BeTrue();
			r.Acknowledged.Should().BeTrue();
			r.ApiCall.HttpStatusCode.Should().Be(200);
		});

		[I] public async Task GetMissingSnapshotLifecycleResponse() => await Assert<GetSnapshotLifecycleResponse>(GetMissingSnapshotLifecycleStep, (v, r) =>
		{
			r.IsValid.Should().BeFalse();
			r.ServerError.Should().NotBeNull();
		});

		[I] public async Task GetSnapshotLifecycleResponse() => await Assert<GetSnapshotLifecycleResponse>(GetSnapshotLifecycleStep, (v, r) =>
		{
			r.IsValid.Should().BeTrue();
			r.Policies.Should().NotBeNull().And.HaveCount(1).And.ContainKey(v);

			var metadata = r.Policies[v];

			metadata.Version.Should().Be(1);
			metadata.ModifiedDate.Should().BeAfter(DateTimeOffset.MinValue);
			metadata.NextExecution.Should().BeAfter(DateTimeOffset.MinValue);
			metadata.Policy.Name.Should().Be(v);
			metadata.Policy.Repository.Should().Be(v);
			metadata.Policy.Schedule.Should().BeEquivalentTo(new CronExpression("0 0 0 1 1 ? *"));
			metadata.Policy.Config.Should().NotBeNull();
			metadata.Policy.Config.Indices.Should().NotBeNull().And.Be(Nest.Indices.Parse("project"));
			metadata.Policy.Retention.ExpireAfter.Should().Be("30d");
			metadata.Policy.Retention.MinimumCount.Should().Be(1);
			metadata.Policy.Retention.MaximumCount.Should().Be(5);
		});

		[I] public async Task GetAllSnapshotLifecycleResponse() => await Assert<GetSnapshotLifecycleResponse>(GetAllSnapshotLifecycleStep, (v, r) =>
		{
			r.IsValid.Should().BeTrue();
			r.Policies.Should().NotBeNull().And.HaveCount(4).And.ContainKey(v);
		});

		[I] public async Task GetSnapshotLifecycleStatusResponse() => await Assert<GetSnapshotLifecycleManagementStatusResponse>(GetSnapshotLifecycleStatusStep, (v, r) =>
		{
			r.IsValid.Should().BeTrue();
			r.ApiCall.HttpStatusCode.Should().Be(200);
			var m = r.OperationMode;
			var rightOperationMode =
				m == LifecycleOperationMode.Running || m == LifecycleOperationMode.Stopped;
			rightOperationMode.Should().BeTrue();
		});

		[I] public async Task StopSnapshotLifecycleResponse() => await Assert<StopSnapshotLifecycleManagementResponse>(StopSnapshotLifecycleStep, (v, r) =>
		{
			r.IsValid.Should().BeTrue();
			r.ApiCall.HttpStatusCode.Should().Be(200);
			r.Acknowledged.Should().BeTrue();
		});

		[I] public async Task ExecuteSnapshotLifecycleResponse() => await Assert<ExecuteSnapshotLifecycleResponse>(ExecuteSnapshotLifecycleStep,
			(v, r) =>
			{
				r.IsValid.Should().BeTrue();
				r.SnapshotName.Should().NotBeNull();
			});

		[I] public async Task ExecuteRetentionSnapshotLifecycleResponse() => await Assert<ExecuteRetentionResponse>(ExecuteRetentionLifecycleStep,
			(v, r) =>
			{
				r.IsValid.Should().BeTrue();
			});

		[I] public async Task GetSnapshotLifeCycleAfterExecuteResponse() => await Assert<GetSnapshotLifecycleResponse>(GetSnapshotLifecycleAfterExecuteStep, (v, r) =>
		{
			r.IsValid.Should().BeTrue();
			r.Policies.Should().NotBeNull().And.HaveCount(1).And.ContainKey(v);

			var metadata = r.Policies[v];
			//TODO temporary measure, find a way to make this less flakey
			if (metadata.InProgress != null)
			{
				metadata.InProgress.Should().NotBeNull();
				metadata.InProgress.Name.Should().NotBeNullOrWhiteSpace();
				metadata.InProgress.UUID.Should().NotBeNullOrWhiteSpace();
				metadata.InProgress.State.Should().NotBeNullOrWhiteSpace();
				metadata.InProgress.StartTime.Should().BeAfter(DateTimeOffset.MinValue);
			}

		});

		[I] public async Task DeleteSnapshotLifecycleResponse() => await Assert<DeleteSnapshotLifecycleResponse>(DeleteSnapshotLifecycleStep,
			(v, r) =>
			{
				r.IsValid.Should().BeTrue();
				r.Acknowledged.Should().BeTrue();
			});

		[I] public async Task GetSnapshotLifecycleStatsResponse() => await Assert<GetSnapshotLifecycleStatsResponse>(GetSnapshotLifecycleStats,
			(v, r) =>
			{
				r.IsValid.Should().BeTrue();
			});
	}
}
