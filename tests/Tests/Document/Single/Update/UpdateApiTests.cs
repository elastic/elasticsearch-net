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
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Single.Update
{
	public class UpdateApiTests
		: ApiIntegrationTestBase<WritableCluster, UpdateResponse<Project>, IUpdateRequest<Project, Project>, UpdateDescriptor<Project, Project>,
			UpdateRequest<Project, Project>>
	{
		public UpdateApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			doc = Project.InstanceAnonymous,
			doc_as_upsert = true,
			detect_noop = true
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<UpdateDescriptor<Project, Project>, IUpdateRequest<Project, Project>> Fluent => u => u
			.Routing(CallIsolatedValue)
			.Doc(Project.Instance)
			.DocAsUpsert()
			.DetectNoop();

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override UpdateRequest<Project, Project> Initializer => new UpdateRequest<Project, Project>(CallIsolatedValue)
		{
			Routing = CallIsolatedValue,
			Doc = Project.Instance,
			DocAsUpsert = true,
			DetectNoop = true
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/project/_update/{CallIsolatedValue}?routing={CallIsolatedValue}";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var id in values.Values)
				Client.Index(Project.Instance, i => i.Id(id).Routing(id));
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Update<Project>(CallIsolatedValue, f),
			(client, f) => client.UpdateAsync<Project>(CallIsolatedValue, f),
			(client, r) => client.Update(r),
			(client, r) => client.UpdateAsync(r)
		);

		protected override UpdateDescriptor<Project, Project> NewDescriptor() =>
			new UpdateDescriptor<Project, Project>(CallIsolatedValue);

		protected override void ExpectResponse(UpdateResponse<Project> response)
		{
			response.ShouldBeValid();
			response.Result.Should().Be(Result.Noop);
		}
	}
}
