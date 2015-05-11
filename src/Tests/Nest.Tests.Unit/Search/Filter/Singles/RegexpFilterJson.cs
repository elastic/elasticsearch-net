using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class RegexpFilterJson
	{
		[Test]
		public void RegexpFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.PostFilter(ff=>ff
					.Regexp(r=>r.OnField(p=>p.Name).Value("ab?"))
				);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				post_filter : {
						regexp: {
							""name"": {
								""value"" : ""ab?""
							}
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}

		[Test]
		public void RegexpFilterStatic()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.PostFilter(Filter<ElasticsearchProject>.Regexp(r => r.OnField(p => p.Name).Value("ab?"))
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				post_filter : {
						regexp: {
							""name"": {
								""value"" : ""ab?""
							}
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}

		[Test]
		public void RegexpFilterWithCache()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.PostFilter(ff => ff
					.Cache(true)
					.Name("regexp_filter")
					.CacheKey("2problems")
					.Regexp(r => r
						.OnField(p => p.Name)
						.Value("ab?")
						.Flags("INTERSECTION|COMPLEMENT|EMPTY")
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				post_filter : {
						regexp: {
							""name"": {
								""value"" : ""ab?"",
								""flags"": ""INTERSECTION|COMPLEMENT|EMPTY""
							},
							_cache:true,
							_cache_key:""2problems"",
							_name : ""regexp_filter""
						}
					}
			}";
			
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
