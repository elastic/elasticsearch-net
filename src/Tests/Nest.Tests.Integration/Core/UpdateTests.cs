using System.Linq;
using FluentAssertions;
using Nest.Tests.MockData;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Core
{
	[TestFixture]
	public class UpdateIntegrationTests : IntegrationTests
	{
		[Test]
		public void TestUpdate()
		{
			var project = this.Client.Source<ElasticsearchProject>(s => s.Id(1));
			Assert.NotNull(project);
			Assert.Greater(project.LOC, 0);
			var loc = project.LOC;
			this.Client.Update<ElasticsearchProject>(u => u
			  .IdFrom(project)
			  .Script("ctx._source.loc += 10")
			  .RetryOnConflict(5)
			  .Refresh()
			);
			project = this.Client.Source<ElasticsearchProject>(s => s.Id(1));
			Assert.AreEqual(project.LOC, loc + 10);
			Assert.AreNotEqual(project.Version, "1");
		}
		
		[Test]
		public void TestUpdate_ObjectInitializer()
		{
			var id = NestTestData.Data.Last().Id;
			var project = this.Client.Source<ElasticsearchProject>(s => s.Id(id));
			Assert.NotNull(project);
			Assert.Greater(project.LOC, 0);
			var loc = project.LOC;
			this.Client.Update<ElasticsearchProject>(new UpdateRequest<ElasticsearchProject>(project.Id)
			{
				RetryOnConflict = 5,
				Refresh = true,
				Script = "ctx._source.loc += 10",
			});
			project = this.Client.Source<ElasticsearchProject>(s => s.Id(id));
			project.LOC.Should().Be(loc + 10);
			Assert.AreNotEqual(project.Version, "1");
		}

		public class ElasticsearchProjectLocUpdate
		{
			public int Id { get; set; }
			[ElasticProperty(Name="loc",AddSortField=true)]
			public int LOC { get; set; }
		}

		[Test]
		public void DocAsUpsert()
		{
			var project = this.Client.Source<ElasticsearchProject>(s => s.Id(1));
			Assert.NotNull(project);
			Assert.Greater(project.LOC, 0);
			var loc = project.LOC;
			this.Client.Update<ElasticsearchProject, ElasticsearchProjectLocUpdate>(u => u
				.Id(1)
				.Doc(new ElasticsearchProjectLocUpdate
				{
					Id = project.Id,
					LOC = project.LOC + 10
				})
				.DocAsUpsert()
				.Refresh()
			);
			project = this.Client.Source<ElasticsearchProject>(s => s.Id(1));
			Assert.AreEqual(project.LOC, loc + 10);
			Assert.AreNotEqual(project.Version, "1");
		}
	}
}
