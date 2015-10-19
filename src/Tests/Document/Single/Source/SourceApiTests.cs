using System;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using System.Collections.Generic;
using Elasticsearch.Net;
using System.Threading.Tasks;
using System.Linq;
using FluentAssertions;

namespace Tests.Document.Single
{
	[Collection(IntegrationContext.ReadOnly)]
	public class SourceIntegrationTests : SimpleIntegration
	{
		public SourceIntegrationTests(ReadOnlyCluster cluster) : base(cluster) { }

		[I] public void SourceReturnsDocument()
		{
			var project = this.Client.Source<Project>(Project.Instance.Name);
			var p = Project.Projects.FirstOrDefault(i=>i.Name == Project.Instance.Name);
			p.Should().NotBeNull("Test setup failure, project instance not found in projects indexed into readonly cluster");

			project.Name.Should().Be(p.Name);
			project.CuratedTags.Should().Equal(p.CuratedTags);
			project.LastActivity.Should().Be(p.LastActivity);
			project.Metadata.Should().Equal(p.Metadata);
			project.StartedOn.Should().Be(p.StartedOn);
		}
	}
}
