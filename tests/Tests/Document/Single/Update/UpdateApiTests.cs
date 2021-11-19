// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Single.Update
{
	public class UpdateApiTests
		: ApiIntegrationTestBase<WritableCluster, UpdateResponse<Project>, UpdateRequestDescriptor<Project, Project>,
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

		protected override Action<UpdateRequestDescriptor<Project, Project>> Fluent => u => u
			.Routing(CallIsolatedValue)
			.Doc(Project.Instance)
			.DocAsUpsert()
			.DetectNoop();

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override UpdateRequest<Project, Project> Initializer => new(Infer.Index<Project>(), CallIsolatedValue)
		{
			Routing = CallIsolatedValue,
			Doc = Project.Instance,
			DocAsUpsert = true,
			DetectNoop = true
		};

		protected override bool SupportsDeserialization => false;
		protected override string ExpectedUrlPathAndQuery => $"/project/_update/{CallIsolatedValue}?routing={CallIsolatedValue}";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var id in values.Values)
				Client.Index(Project.Instance, i => i.Id(id).Routing(id));
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Update(Infer.Index<Project>(), CallIsolatedValue, f),
			(client, f) => client.UpdateAsync(Infer.Index<Project>(), CallIsolatedValue, f),
			(client, r) => client.Update(r),
			(client, r) => client.UpdateAsync(r)
		);

		protected override UpdateRequestDescriptor<Project, Project> NewDescriptor() =>
			new(Infer.Index<Project>(), CallIsolatedValue);

		protected override void ExpectResponse(UpdateResponse<Project> response)
		{
			response.ShouldBeValid();
			response.Result.Should().Be(Result.NoOp);
		}
	}
}
