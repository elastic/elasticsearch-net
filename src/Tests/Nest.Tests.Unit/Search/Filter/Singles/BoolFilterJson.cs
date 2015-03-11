using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.Singles
{
	[TestFixture]
	public class BoolFilterJson
	{
		[Test]
		public void BoolFilter()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.PostFilter(filter=>filter
					.Bool(b=>b
						.Must(
							f => f.MatchAll()
						)
						.MustNot(
							f => f.Missing(p => p.LOC)
						)
						.Should(
							f=> f.Exists(p => p.LOC)
						)
					)
				);
				
			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				post_filter : {
						""bool"": {
							""must"": [
								{
									""match_all"": {}
								}
							],
							""must_not"": [
								{
									""missing"": {
										""field"": ""loc""
									}
								}
							],
							""should"": [
								{
									""exists"": {
										""field"": ""loc""
									}
								}
							]
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);		
		}
		[Test]
		public void BoolFilterOverload()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.PostFilter(f => f.MatchAll() & f.Missing(p => p.LOC))
			;

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				post_filter : {
						""bool"": {
							""must"": [
								{
									""match_all"": {}
								},
								{
									""missing"": {
										""field"": ""loc""
									}
								}
							]
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void BoolFilterOverloadConditional()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.PostFilter(f => (f.MatchAll() && f.Missing(p => p.LOC)) || f.Term("x","y"))
			;

			var json = TestElasticClient.Serialize(s);
            var expected = @"{ from: 0, size: 10, 
				""post_filter"": {
					""bool"": {
						""should"": [
							{
							""bool"": {
								""must"": [
									{""match_all"": {}},
									{""missing"": {""field"": ""loc""}}
								]
								}
							},
							{""term"": {""x"": ""y""}}
						]
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void BoolFilterOverloadStatic()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.PostFilter(Filter<dynamic>.MatchAll() && Filter<ElasticsearchProject>.Missing(p => p.LOC))
			;

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				post_filter : {
						""bool"": {
							""must"": [
								{
									""match_all"": {}
								},
								{
									""missing"": {
										""field"": ""loc""
									}
								}
							]
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}


		[Test]
		public void BoolFilterWithNameAndCache()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.PostFilter(filter => filter
					.Name("my_bool_filter")
					.Cache(true)
					.Bool(b => b
						.Must(
							f => f.MatchAll()
						)
						.MustNot(
							f => f.Missing(p => p.LOC)
						)
						.Should(
							f => f.Exists(p => p.LOC)
						)
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				post_filter : {
						""bool"": {
							""must"": [
								{
									""match_all"": {}
								}
							],
							""must_not"": [
								{
									""missing"": {
										""field"": ""loc""
									}
								}
							],
							""should"": [
								{
									""exists"": {
										""field"": ""loc""
									}
								}
							],
							_cache : true,
							_name: ""my_bool_filter""
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
