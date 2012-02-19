using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Nest.DSL;
using Nest.TestData.Domain;

namespace Nest.Tests.Dsl.Json.Filter
{
	[TestFixture]
	public class BoolFilterJson
	{
		[Test]
		public void BoolFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(filter=>filter
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
				
			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
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
		public void BoolFilterWithNameAndCache()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(filter => filter
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

			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
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
