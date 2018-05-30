using System;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.ClientConcepts.HighLevel.CovariantHits
{
	public class CovariantSearchResults
	{
		/**=== Covariant search results
		 *
		 * NEST used to have a feature that allowed you to map multiple types in an index back into a covariant list.
		 *
		 * Since types are removed in Elasticsearch 6.0 this feature is no longer supported. Because you can
		 * now explicitly inject a serializer for user types only (_source, fields etcetera) please rely on a JsonConverter that
		 * can do this out of the box e.g `TypeNameHandling.All` from `Json.NET`
		 *
		 * https://www.newtonsoft.com/json/help/html/SerializeTypeNameHandling.htm
		 */
		public class C
		{
			public int Id { get; set; }
			public string Name { get; set; }
		}
		private readonly IElasticClient _client = TestClient.GetFixedReturnClient(SearchResultMock.Json);

		//hide
		[U] public void CanDeserializeHits()
		{
			var result = this._client.Search<C>(s => s
				.Size(100)
			);
			result.ApiCall.Should().NotBeNull();
			result.ShouldBeValid();
			result.HitsMetadata.Should().NotBeNull();
			result.HitsMetadata.Hits.Should().NotBeNull();
			result.HitsMetadata.MaxScore.Should().BeGreaterThan(1.0);
			result.HitsMetadata.Total.Should().Be(100);

			result.Hits.Should().OnlyContain(hit => hit.Index == "project", "_index on hit");
			result.Hits.Should().OnlyContain(hit => !string.IsNullOrEmpty(hit.Type), "_type on hit");
			result.Hits.Should().OnlyContain(hit => !string.IsNullOrEmpty(hit.Id), "_id on hit");
			result.Hits.Should().OnlyContain(hit => hit.Score.HasValue, "_score on hit");
			result.Hits.Should().OnlyContain(hit => hit.Source != null, "_source on hit");

			result.Documents.Count.Should().Be(100);
			result.Documents.Should().OnlyContain(d => d.Id > 0, "id on _source");
			result.Documents.Should().OnlyContain(d => !string.IsNullOrEmpty(d.Name), "name on _source");


		}
	}

	internal static class SearchResultMock
	{
		public static object Json = new
		{
			took = 1,
			timed_out = false,
			_shards = new {
				total = 2,
				successful = 2,
				failed = 0
			},
			hits = new {
				total = 100,
				max_score = 1.1,
				hits = Enumerable.Range(1, 25).Select(i => (object)new
				{
					_index = "project",
					_type = "a",
					_id = i,
					_score = 1.0,
					_source= new { name= "A object", id = i }
				}).Concat(Enumerable.Range(26, 25).Select(i => (object)new
				{
					_index = "project",
					_type = "b",
					_id = i,
					_score = 1.0,
					_source= new { name= "B object", id = i }
				})).Concat(Enumerable.Range(51, 50).Select(i => new
				{
					_index = "project",
					_type = "c",
					_id = i,
					_score = 1.0,
					_source= new { name= "C object", id = i }
				})).ToArray()
			}
		};
	}
}
