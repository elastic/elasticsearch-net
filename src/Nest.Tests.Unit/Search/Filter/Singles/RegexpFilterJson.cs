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
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff=>ff
					.Regexp(r=>r.OnField(p=>p.Name).Value("ab?"))
				);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
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
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(Filter<ElasticSearchProject>.Regexp(r => r.OnField(p => p.Name).Value("ab?"))
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
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
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(ff => ff
					.Cache(true)
					.Name("regexp_filter")
					.Regexp(r => r.OnField(p => p.Name).Value("ab?").Flags("INTERSECTION|COMPLEMENT|EMPTY"))
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
						regexp: {
							""name"": {
								""value"" : ""ab?"",
								""flags"": ""INTERSECTION|COMPLEMENT|EMPTY""
							},
							_cache:true,
							_name : ""regexp_filter""
						}
					}
			}";
			
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
