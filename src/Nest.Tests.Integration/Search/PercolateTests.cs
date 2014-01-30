using System.Linq;
using FluentAssertions;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Search
{
	[TestFixture]
	public class PercolateTests : IntegrationTests
	{
		private string _LookFor = NestTestData.Data.First().Followers.First().FirstName;

		[Test]
		public void RegisterPercolateTest()
		{
			var name = "mypercolator";
			var c = this._client;
			var r = c.RegisterPercolator<ElasticSearchProject>(p => p
				.Name(name)
				.Query(q => q
					.Term(f => f.Name, "elasticsearch.pm")
				)
			);
			Assert.True(r.IsValid);
			Assert.True(r.OK);
			Assert.AreEqual(r.Type, ElasticsearchConfiguration.DefaultIndex);
			Assert.AreEqual(r.Id, name);
			Assert.Greater(r.Version, 0);
			r.ConnectionStatus.Request.Should().NotBeNullOrEmpty().And.NotBe("{}");

		}
		[Test]
		public void UnregisterPercolateTest()
		{
			var name = "mypercolator";
			var c = this._client;
			var r = c.RegisterPercolator<ElasticSearchProject>(p => p
				.Name(name)
				.AddMetadata(md=>md.Add("color", "blue"))
				.Query(q => q
					.Term(f => f.Name, "elasticsearch.pm")
				)
			);
			Assert.True(r.IsValid);
			Assert.True(r.OK);
			Assert.AreEqual(r.Type, ElasticsearchConfiguration.DefaultIndex);
			Assert.AreEqual(r.Id, name);
			Assert.Greater(r.Version, 0);

			var re = c.UnregisterPercolator(name, ur=>ur.Index<ElasticSearchProject>());
			Assert.True(re.IsValid);
			Assert.True(re.OK);
			Assert.True(re.Found);
			Assert.AreEqual(re.Type, ElasticsearchConfiguration.DefaultIndex);
			Assert.AreEqual(re.Id, name);
			Assert.Greater(re.Version, 0);
			re = c.UnregisterPercolator(name, ur=>ur.Index<ElasticSearchProject>());
			Assert.True(re.IsValid);
			Assert.True(re.OK);
			Assert.False(re.Found);
		}

		[Test]
		public void PercolateDoc()
		{
			this.RegisterPercolateTest(); // I feel a little dirty.
			var c = this._client;
			var name = "mypercolator";
			var r = c.Percolate<ElasticSearchProject>(p=>p
				.Index<ElasticSearchProject>()
				.Object(new ElasticSearchProject()
			{
				Name = "elasticsearch.pm",
				Country = "netherlands",
				LOC = 100000, //Too many :(
			}));
			Assert.True(r.IsValid);
			Assert.True(r.OK);
			Assert.NotNull(r.Matches);
			Assert.True(r.Matches.Contains(name));
			var re = c.UnregisterPercolator(name, ur=>ur.Index<ElasticSearchProject>());
		}
		[Test]
		public void PercolateTypedDoc()
		{
			this.RegisterPercolateTest(); // I feel a little dirty.
			var c = this._client;
			var name = "eclecticsearch";
			var r = c.RegisterPercolator<ElasticSearchProject>(p => p
				 .Name(name)
				 .Query(q => q
					.Term(f => f.Country, "netherlands")
				 )
			 );
			Assert.True(r.IsValid);
			Assert.True(r.OK);
			var percolateResponse = this._client.Percolate<ElasticSearchProject>(p=>p.Object(
				new ElasticSearchProject()
				{
					Name = "NEST",
					Country = "netherlands",
					LOC = 100000, //Too many :(
				}
			));
			Assert.True(percolateResponse.IsValid);
			Assert.True(percolateResponse.OK);
			Assert.NotNull(percolateResponse.Matches);
			Assert.True(percolateResponse.Matches.Contains(name));

			var re = c.UnregisterPercolator(name, ur=>ur.Index<ElasticSearchProject>());
		}
		[Test]
		public void PercolateTypedDocWithQuery()
		{
			var c = this._client;
			var name = "eclecticsearch" + ElasticsearchConfiguration.NewUniqueIndexName();
			var re = c.UnregisterPercolator(name, ur=>ur.Index<ElasticSearchProject>());
			var r = c.RegisterPercolator<ElasticSearchProject>(p => p
				 .Name(name)
				 .AddMetadata(md=>md.Add("color", "blue"))
				 .Query(q => q
					.Term(f => f.Country, "netherlands")
				 )
			 );
			Assert.True(r.IsValid);
			Assert.True(r.OK);
			c.Refresh();
			var percolateResponse = this._client.Percolate<ElasticSearchProject>(p => p
				.Object(new ElasticSearchProject()
				{
					Name = "NEST",
					Country = "netherlands",
					LOC = 100000, //Too many :(
				})
				.Query(q=>q.Match(m=>m.OnField("color").QueryString("blue")))
			);
			Assert.True(percolateResponse.IsValid);
			Assert.True(percolateResponse.OK);
			Assert.NotNull(percolateResponse.Matches);
			Assert.True(percolateResponse.Matches.Contains(name), percolateResponse.Matches.Count().ToString());

			//should not match since we registered with the color blue
			percolateResponse = this._client.Percolate<ElasticSearchProject>(p => p
				.Object(new ElasticSearchProject()
				{
					Name = "NEST",
					Country = "netherlands",
					LOC = 100000, //Too many :(
				})
				.Query(q => q.Term("color", "green"))
			);
			Assert.True(percolateResponse.IsValid);
			Assert.True(percolateResponse.OK);
			Assert.NotNull(percolateResponse.Matches);
			Assert.False(percolateResponse.Matches.Contains(name));

			re = c.UnregisterPercolator(name, ur=>ur.Index<ElasticSearchProject>());

		}
	}
}