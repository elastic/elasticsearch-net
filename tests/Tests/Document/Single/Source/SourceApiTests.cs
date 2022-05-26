// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Linq;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.DocumentationTests;

namespace Tests.Document.Single.Source;

public class SourceIntegrationTests : IntegrationDocumentationTestBase, IClusterFixture<ReadOnlyCluster>
{
	public SourceIntegrationTests(ReadOnlyCluster cluster) : base(cluster) { }

	[I]
	public void SourceReturnsDocument()
	{
		var firstSeededProject = Project.Projects.FirstOrDefault(i => i.Name == Project.Instance.Name);
		firstSeededProject.Should().NotBeNull("Test setup failure, project instance not found in projects indexed into readonly cluster");

		var project = Client.GetSource<Project>(Project.Instance.Name, s => s.Routing(Project.Instance.Name)).Body;

		project.Should().NotBeNull();
		project.Name.Should().Be(firstSeededProject.Name);
		project.CuratedTags.Should().HaveCount(firstSeededProject.CuratedTags.Count);
		project.LastActivity.Should().Be(firstSeededProject.LastActivity);
		project.StartedOn.Should().Be(firstSeededProject.StartedOn);
	}

	//[I]
	//[JsonNetSerializerOnly]
	//public void UseSourceSerializer()
	//{
	//	var project = Client.Source<Project>(Project.Instance.Name, s => s.Routing(Project.Instance.Name)).Body;

	//	var sourceOnly = project.SourceOnly;
	//	sourceOnly.Should().NotBeNull();
	//	sourceOnly.NotReadByDefaultSerializer.Should().Be("read");
	//	sourceOnly.NotWrittenByDefaultSerializer.Should().Be("written");
	//}
}
