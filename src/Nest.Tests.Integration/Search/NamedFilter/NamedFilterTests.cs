using System.Linq;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Search.NamedFilter
{
	[TestFixture]
	public class NamedFilterTest : IntegrationTests
	{
		private string _LookFor = NestTestData.Data.First().Name;

		[Test]
		public void SimpleNamedFilter()
		{
			var queryResults = this._client.Search<ElasticSearchProject>(s=>s
				.From(0)
				.Size(10)
				.Fields(p=>p.Name)
				.Filter(f => f.Name("myfilter").Terms(p => p.Name.Suffix("sort"), new [] {_LookFor.ToLower() })
					|| f.Name("myfilter2").Terms(p => p.Name.Suffix("sort"), new [] { "nest" }) 
				)
			);
			Assert.True(queryResults.IsValid);
			Assert.True(queryResults.Documents.Any());
			//Assert matched_filters is returned
			//Possible ES bug
			//https://github.com/elasticsearch/elasticsearch/issues/3097
		}

	}
}