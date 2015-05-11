using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles
{
	[TestFixture]
	public class IdsQueryJson
	{
		[Test]
		public void IdsQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>().From(0).Size(10)
				.Query(filter=>filter
					.Ids(new[] { "1", "4", "100" })
			);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						ids : { 
							values : [""1"", ""4"", ""100""]
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}

		[Test]
		public void IdsQueryWithType()
		{
			var s = new SearchDescriptor<ElasticsearchProject>().From(0).Size(10)
				.Query(filter => filter
					.Ids("my_type", new[] { "1", "4", "100" })
			);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						ids : { 
							type: [""my_type""],
							values : [""1"", ""4"", ""100""]
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void IdsQueryWithTypes()
		{
			var s = new SearchDescriptor<ElasticsearchProject>().From(0).Size(10)
				.Query(filter => filter
					.Ids(new []{"my_type", "my_other_type"}, new[] { "1", "4", "100" })
			);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						ids : { 
							type: [""my_type"", ""my_other_type""],
							values : [""1"", ""4"", ""100""]
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
		
		[Test]
		public void IdsQueryFull()
		{
			var s = new SearchDescriptor<ElasticsearchProject>().From(0).Size(10)
				.Query(filter => filter
					.Ids(i=>i.Name("named_query").Values(1,4, 1000))
			);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						ids : { 
							_name: ""named_query"",
							values : [""1"", ""4"", ""1000""]
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}

	}
}
