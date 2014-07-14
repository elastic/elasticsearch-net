using System.Reflection;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Query.Singles.Terms
{
	[TestFixture]
	public class TermsQueryJson : BaseJsonTests
	{
		[Test]
		public void TermsQuery()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(ff => ff
					.Terms(f => f.Name, new[] { "elasticsearch.pm" })
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						terms: {
							""name"": [""elasticsearch.pm""]
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void TermsQueryParams()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(ff => ff.Terms(f => f.Name, new [] {"elasticsearch.pm", "nest"})
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						terms: {
							""name"": [""elasticsearch.pm"", ""nest""],
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
		[Test]
		public void TermsQueryDescriptor()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(ff => ff.
					TermsDescriptor(tq => tq
						.OnField(f=>f.Name)
						.Terms(new [] {"elasticsearch.pm", "nest"})
						.MinimumShouldMatch(2)
						.DisableCoord()
					)
				);

			var json = TestElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				query : {
						terms: {
							""name"": [""elasticsearch.pm"", ""nest""],
							disable_coord : true,
							minimum_should_match: ""2""
						}
					}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
		
		[Test]
		public void TermsQueryDescriptorUsingExternalField()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(ff => ff.
					TermsDescriptor(tq => tq
						.OnField(p=>p.IntValues)
						.MinimumShouldMatch(2)
						.DisableCoord()
						.OnExternalField<Person>(ef=>ef
							.Path(p=>p.Id)
							.Id(4)
						)
					)
				);

			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}
		
		[Test]
		public void TermsOfTypeInt()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Query(ff => ff.
					TermsDescriptor<int>(tq => tq
						.OnField(p=>p.LOC)
						.MinimumShouldMatch(2)
						.DisableCoord()
						.Terms(new [] {1,2,3,4 })
					)
				);

			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}
	}
}
