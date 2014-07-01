using System.Globalization;
using Elasticsearch.Net;
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
			var project = this._client.Source<ElasticsearchProject>(s => s.Id(1));
			Assert.NotNull(project);
			Assert.Greater(project.LOC, 0);
			var loc = project.LOC;
			this._client.Update<ElasticsearchProject>(u => u
			  .Object(project)
			  .Script("ctx._source.loc += 10")
			  .RetryOnConflict(5)
			  .Refresh()
			);
			project = this._client.Source<ElasticsearchProject>(s => s.Id(1));
			Assert.AreEqual(project.LOC, loc + 10);
			Assert.AreNotEqual(project.Version, "1");
		}
		
		[Test]
		public void TestUpdate_ObjectInitializer()
		{
			var project = this._client.Source<ElasticsearchProject>(s => s.Id(2));
			Assert.NotNull(project);
			Assert.Greater(project.LOC, 0);
			var loc = project.LOC;
			this._client.Update<ElasticsearchProject>(new UpdateRequest<ElasticsearchProject>
			{
				Id = project.Id.ToString(CultureInfo.InvariantCulture),
				RetryOnConflict = 5,
				Refresh = true,
				Script = "ctx._source.loc += 10",
			});
			project = this._client.Source<ElasticsearchProject>(s => s.Id(2));
			Assert.AreEqual(project.LOC, loc + 10);
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
			var project = this._client.Source<ElasticsearchProject>(s => s.Id(1));
			Assert.NotNull(project);
			Assert.Greater(project.LOC, 0);
			var loc = project.LOC;
			this._client.Update<ElasticsearchProject, ElasticsearchProjectLocUpdate>(u => u
				.Document(new ElasticsearchProjectLocUpdate
				{
					Id = project.Id,
					LOC = project.LOC + 10
				})
				.DocAsUpsert()
				.Refresh()
			);
			project = this._client.Source<ElasticsearchProject>(s => s.Id(1));
			Assert.AreEqual(project.LOC, loc + 10);
			Assert.AreNotEqual(project.Version, "1");
		}
	}
}
