using System.Linq;
using System.Text;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Search.NamedFilter
{
	using System.Collections.Generic;
	using Elasticsearch.Net;

	[TestFixture]
	public class NamedFilterTest : IntegrationTests
	{
		private string _LookFor = NestTestData.Data.First().Name;

		[Test]
		public void SimpleNamedFilter()
		{
			var queryResults = this._client.Search<ElasticsearchProject>(s=>s
				.From(0)
				.Size(10)
				.Fields(p=>p.Name)
				.Filter(f => 
					f.Name("myfilter").Terms(p => p.Name.Suffix("sort"), new [] {_LookFor.ToLower() })
					|| f.Name("myfilter2").Terms(p => p.Name.Suffix("sort"), new [] { "nest" }) 
				)
			);
			Assert.True(queryResults.IsValid);
			//Assert.True(queryResults.Documents.Any());
			//Assert matched_filters is returned
			//Possible ES bug
			//https://github.com/elasticsearch/elasticsearch/issues/3097
		}

		/// <summary>
		/// Fields string param usage produces correct search string (ref: Nest.Tests.Unit.Search.SearchOptions.TestFieldsWithExclusionsByProperty)
		/// Results fail to be correctly deserialized, resulting doc count is correct but docs are all null.
		/// https://github.com/elasticsearch/elasticsearch-net/issues/606
		/// </summary>
		[Test]		
		public void Search_WithFieldsRemoved_ReturnsDocuments_ButAllDocumentsAreNull()
		{
		  // Left in followers + contributors will cause a leaf node exception, so good test victims
		  var fields = typeof(ElasticsearchProject).GetProperties()
			.Select(x => x.Name.ToCamelCase())
			.Except(new List<string> { "followers", "contributors", "nestedFollowers", "myGeoShape" }).ToList();

		  var queryResults = _client.Search<ElasticsearchProject>(s =>
				  s.Skip(0)
				  .Take(10)
				  .Fields(fields.ConvertAll(x => x.ToCamelCase()).ToArray())				  
				  .AllTypes());

		  Assert.True(queryResults.IsValid);

		  foreach (var d in queryResults.Documents)
			Assert.IsNotNull(d);
		}
	}
}