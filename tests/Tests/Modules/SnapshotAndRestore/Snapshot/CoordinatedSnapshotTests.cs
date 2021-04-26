/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Modules.SnapshotAndRestore.Snapshot
{
	public class CoordinatedSnapshotTests : CoordinatedIntegrationTestBase<IntrusiveOperationCluster>
	{
		private const string SetupStep = nameof(SetupStep);
		private const string CreateSnapshotStep = nameof(CreateSnapshotStep);
		private const string GetSnapshotStep = nameof(GetSnapshotStep);
		private const string CloneStep = nameof(CloneStep);
		private const string DeleteSnapshotStep = nameof(DeleteSnapshotStep);
		private const string DeleteNonExistentSnapshotStep = nameof(DeleteNonExistentSnapshotStep);

		public CoordinatedSnapshotTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage)
		{
			{
				SetupStep, u => u
					.Call(async (v, c) =>
					{
						await c.Snapshot.CreateRepositoryAsync($"{v}-repository", cr => cr.FileSystem(fs => fs.Settings(Path.Combine(cluster.FileSystem.RepositoryPath, v))));
						await c.Indices.CreateAsync($"{v}-index");
						await c.Cluster.HealthAsync($"{v}-index", ch => ch.WaitForStatus(WaitForStatus.Yellow));
					})
			},
			{
				CreateSnapshotStep, u => u
					.Calls<SnapshotDescriptor, SnapshotRequest, ISnapshotRequest, SnapshotResponse>(
						v => new SnapshotRequest($"{v}-repository", $"{v}-source") { Indices = $"{v}-index", WaitForCompletion = true, IncludeGlobalState = false },
						(v, d) => d.Index($"{v}-index").WaitForCompletion(true).IncludeGlobalState(false),
						(v, c, f) => c.Snapshot.Snapshot($"{v}-repository", $"{v}-source", f),
						(v, c, f) => c.Snapshot.SnapshotAsync($"{v}-repository", $"{v}-source", f),
						(_, c, r) => c.Snapshot.Snapshot(r),
						(_, c, r) => c.Snapshot.SnapshotAsync(r))
			},
			{
				GetSnapshotStep, u => u
					.Calls<GetSnapshotDescriptor, GetSnapshotRequest, IGetSnapshotRequest, GetSnapshotResponse>(
						v => new GetSnapshotRequest($"{v}-repository", $"{v}-source"),
						(v, d) => d,
						(v, c, f) => c.Snapshot.Get($"{v}-repository", $"{v}-source", f),
						(v, c, f) => c.Snapshot.GetAsync($"{v}-repository", $"{v}-source", f),
						(_, c, r) => c.Snapshot.Get(r),
						(_, c, r) => c.Snapshot.GetAsync(r))
			},
			{
				CloneStep, ">=7.10.0", u => u
					.Calls<CloneSnapshotDescriptor, CloneSnapshotRequest, ICloneSnapshotRequest, CloneSnapshotResponse>(
						v => new CloneSnapshotRequest($"{v}-repository", $"{v}-source", $"{v}-target"){ Indices = $"{v}-index" },
						(v, d) => d.Index($"{v}-index"),
						(v, c, f) => c.Snapshot.Clone($"{v}-repository", $"{v}-source", $"{v}-target", f),
						(v, c, f) => c.Snapshot.CloneAsync($"{v}-repository", $"{v}-source", $"{v}-target", f),
						(_, c, r) => c.Snapshot.Clone(r),
						(_, c, r) => c.Snapshot.CloneAsync(r))
			},
			{
				DeleteSnapshotStep, u => u
					.Calls<DeleteSnapshotDescriptor, DeleteSnapshotRequest, IDeleteSnapshotRequest, DeleteSnapshotResponse>(
						v => new DeleteSnapshotRequest($"{v}-repository", $"{v}-source"),
						(v, d) => d,
						(v, c, f) => c.Snapshot.Delete($"{v}-repository", $"{v}-source", f),
						(v, c, f) => c.Snapshot.DeleteAsync($"{v}-repository", $"{v}-source", f),
						(_, c, r) => c.Snapshot.Delete(r),
						(_, c, r) => c.Snapshot.DeleteAsync(r))
			},
			{
				DeleteNonExistentSnapshotStep, u => u
					.Calls<DeleteSnapshotDescriptor, DeleteSnapshotRequest, IDeleteSnapshotRequest, DeleteSnapshotResponse>(
						v => new DeleteSnapshotRequest($"{v}-repository", v),
						(v, d) => d,
						(v, c, f) => c.Snapshot.Delete($"{v}-repository", v, f),
						(v, c, f) => c.Snapshot.DeleteAsync($"{v}-repository", v, f),
						(_, c, r) => c.Snapshot.Delete(r),
						(_, c, r) => c.Snapshot.DeleteAsync(r))
			}
		})
		{ }

		[I]
		public async Task CreateSnapshotResponse() => await Assert<SnapshotResponse>(CreateSnapshotStep, r =>
		{
			r.Accepted.Should().BeTrue();
			r.Snapshot.Indices.Count.Should().Be(1);
			r.Snapshot.StartTime.Should().BeOnOrBefore(DateTime.Now);
		});

		[I]
		public async Task GetSnapshotResponse() => await Assert<GetSnapshotResponse>(GetSnapshotStep, r =>
		{
			r.Snapshots.Should().HaveCount(1);
			var snapshot = r.Snapshots.FirstOrDefault();
			snapshot.Should().NotBeNull();
		});

		[I]
		public async Task CloneSnapshotResponse() => await Assert<CloneSnapshotResponse>(CloneStep, r =>
		{
			r.Acknowledged.Should().BeTrue();
		});

		[I]
		public async Task DeleteSnapshotResponse() => await Assert<DeleteSnapshotResponse>(DeleteSnapshotStep, r =>
		{
			r.Acknowledged.Should().BeTrue();
		});

		[I]
		public async Task DeleteNonExistentSnapshotResponse() => await Assert<DeleteSnapshotResponse>(DeleteNonExistentSnapshotStep, r =>
		{
			r.ShouldNotBeValid();
			r.ServerError.Should().NotBeNull();
			r.ServerError.Status.Should().Be(404);
			r.ServerError.Error.Reason.Should().Contain("missing");
		});
	}
}
