// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Document.Single.Update
{
	public class UpdateWithSourceApiTests
		: ApiIntegrationTestBase<WritableCluster, UpdateResponse<Project>, IUpdateRequest<Project, Project>, UpdateDescriptor<Project, Project>,
			UpdateRequest<Project, Project>>
	{
		public UpdateWithSourceApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			doc = Project.InstanceAnonymous,
			doc_as_upsert = true,
			_source = new
			{
				includes = new[] { "name", "sourceOnly" }
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<UpdateDescriptor<Project, Project>, IUpdateRequest<Project, Project>> Fluent => d => d
			.Routing(CallIsolatedValue)
			.Doc(Project.Instance)
			.Source(s => s.Includes(f => f.Field(p => p.Name).Field("sourceOnly")))
			.DocAsUpsert();

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override UpdateRequest<Project, Project> Initializer => new UpdateRequest<Project, Project>(CallIsolatedValue)
		{
			Routing = CallIsolatedValue,
			Doc = Project.Instance,
			DocAsUpsert = true,
			Source = new SourceFilter
			{
				Includes = Field<Project>(p => p.Name).And("sourceOnly")
			}
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/project/_update/{CallIsolatedValue}?routing={U(CallIsolatedValue)}";

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
			new UpdateDescriptor<Project, Project>(CallIsolatedValue).Routing(CallIsolatedValue);

		[I] public Task ReturnsSourceAndFields() => AssertOnAllResponses(r =>
		{
			r.Get.Should().NotBeNull();
			r.Get.Found.Should().BeTrue();
			r.Get.Source.Should().NotBeNull();
			var name = Project.First.Name;
			r.Get.Source.Name.Should().Be(name);
			r.Get.Source.Description.Should().BeNullOrEmpty();
			r.Get.Source.ShouldAdhereToSourceSerializerWhenSet();
			r.Get.Fields.Should().BeNull();
		});
	}
}
