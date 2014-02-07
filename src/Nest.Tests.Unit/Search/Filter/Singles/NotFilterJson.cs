using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class NotFilterJson
	{
		[Test]
		public void NotFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Filter(filter => filter
					.Not(f => f.Missing(p => p.LOC))
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						""not"": {
							""filter"": {
									""missing"": {
										""field"": ""loc""
									}
							}
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void NotFilterCacheNamed()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Filter(fd => fd
					.Cache(true)
					.Name("my_not_filter")
					.Not(f => f.Missing(p => p.LOC))
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						""not"": {
							""filter"": {
									""missing"": {
										""field"": ""loc""
									}
							},
							_cache:true,
							_name: ""my_not_filter""
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
