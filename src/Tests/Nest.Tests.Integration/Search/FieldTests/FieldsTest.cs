using FluentAssertions;

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
				.Except(new List<string> {"followers", "contributors", "product", "nestedFollowers", "myGeoShape"}).ToList();

			var queryResults = Client.Search<ElasticsearchProject>(s =>
					s.Skip(0)
					.Take(10)
					.Fields(fields.ConvertAll(x => x.ToCamelCase()).ToArray())
				);

			Assert.True(queryResults.IsValid);

			queryResults.Documents.Should().BeEmpty();
			
			foreach (var doc in queryResults.FieldSelections)	
			{
				// "content" is a string
				var content = doc.FieldValues(p => p.Content).First();
				content.Should().NotBeEmpty();

				// intValues is a List<int>
				// the string overload needs to be typed as int[]
				// because elasticsearch will return special fields such as _routing, _parent
				// as string not string[] return [] would make this unreachable
				var intValues = doc.FieldValues<int[]>("intValues");
				intValues.Should().NotBeEmpty().And.OnlyContain(i => i !=  0);
				
				//functionally equivalent, we need to flatten the expression with First()
				//so that the returned type is int[] and not List<int>[];
				intValues = doc.FieldValues(p => p.IntValues.First());
				intValues.Should().NotBeEmpty().And.OnlyContain(i => i !=  0);
			}

		}
	}
}