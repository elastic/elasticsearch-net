// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.Xunit;
using Tests.Domain;
using Tests.Framework.DocumentationTests;

namespace Tests.Document.Single.Source
{
	public class SourceIntegrationTests : IntegrationDocumentationTestBase, IClusterFixture<ReadOnlyCluster>
	{
		public SourceIntegrationTests(ReadOnlyCluster cluster) : base(cluster) { }

		[I] public void SourceReturnsDocument()
		{
			var project = Client.Source<Project>(Project.Instance.Name, s => s.Routing(Project.Instance.Name)).Body;
			var p = Project.Projects.FirstOrDefault(i => i.Name == Project.Instance.Name);
			p.Should().NotBeNull("Test setup failure, project instance not found in projects indexed into readonly cluster");

			project.Name.Should().Be(p.Name);
			project.CuratedTags.Should().HaveCount(p.CuratedTags.Count());
			project.LastActivity.Should().Be(p.LastActivity);
			project.StartedOn.Should().Be(p.StartedOn);
		}

		[I]
		[JsonNetSerializerOnly]
		public void UseSourceSerializer()
		{
			var project = Client.Source<Project>(Project.Instance.Name, s => s.Routing(Project.Instance.Name)).Body;

			var sourceOnly = project.SourceOnly;
			sourceOnly.Should().NotBeNull();
			sourceOnly.NotReadByDefaultSerializer.Should().Be("read");
			sourceOnly.NotWrittenByDefaultSerializer.Should().Be("written");
		}
	}
}
