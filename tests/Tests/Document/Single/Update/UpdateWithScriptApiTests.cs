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
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Single.Update
{
	public class UpdateWithScriptApiTests
		: ApiIntegrationTestBase<WritableCluster, UpdateResponse<Project>, IUpdateRequest<Project, Project>, UpdateDescriptor<Project, Project>,
			UpdateRequest<Project, Project>>
	{
		public UpdateWithScriptApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			scripted_upsert = true,
			script = new
			{
				source = "ctx._source.name = \"params.name\"",
				lang = "painless",
				@params = new
				{
					name = "foo",
					other = (object)null
				}
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<UpdateDescriptor<Project, Project>, IUpdateRequest<Project, Project>> Fluent => d => d
			.Routing(Project.Instance.Name)
			.ScriptedUpsert()
			.Script(s => s
				.Source("ctx._source.name = \"params.name\"")
				.Lang("painless")
				.Params(p => p
					.Add("name", "foo")
					.Add("other", null)
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override UpdateRequest<Project, Project> Initializer => new UpdateRequest<Project, Project>(CallIsolatedValue)
		{
			Routing = Project.Instance.Name,
			ScriptedUpsert = true,
			Script = new InlineScript("ctx._source.name = \"params.name\"")
			{
				Lang = "painless",
				Params = new Dictionary<string, object>
				{
					{ "name", "foo" },
					{ "other", null }
				}
			}
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/project/_update/{CallIsolatedValue}?routing={U(Project.Instance.Name)}";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var id in values.Values)
				Client.Index(Project.Instance, i => i.Id(id).Routing(Project.Instance.Name));
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Update<Project>(CallIsolatedValue, f),
			(client, f) => client.UpdateAsync<Project>(CallIsolatedValue, f),
			(client, r) => client.Update(r),
			(client, r) => client.UpdateAsync(r)
		);

		protected override UpdateDescriptor<Project, Project> NewDescriptor() =>
			new UpdateDescriptor<Project, Project>(CallIsolatedValue).Routing(Project.Instance.Name);
	}
}
