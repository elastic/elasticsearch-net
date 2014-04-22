namespace Nest.Tests.Integration.Search.FieldTests
{
	using System.Collections.Generic;
	using System.Linq;
	using Elasticsearch.Net;
	using Nest.Tests.MockData.Domain;
	using NUnit.Framework;

	[TestFixture]
	public class FieldsTest : IntegrationTests
	{
		/// <summary>
		/// Fields string param usage produces correct search string (ref: Nest.Tests.Unit.Search.SearchOptions.TestFieldsWithExclusionsByProperty)
		/// Results fail to be correctly deserialized, resulting doc count is correct but docs are all null.
		/// https://github.com/elasticsearch/elasticsearch-net/issues/606
		/// </summary>
		[Test]
		public void Search_WithFieldsRemoved_ReturnsDocuments_ResultingArrayOfDocsShouldNotBeNull()
		{
			// Left in followers + contributors will cause a leaf node exception, so good test victims
			var fields = typeof (ElasticsearchProject).GetProperties()
				.Select(x => x.Name.ToCamelCase())
				.Except(new List<string> {"followers", "contributors", "nestedFollowers", "myGeoShape"}).ToList();

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