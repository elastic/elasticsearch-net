using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Highlight
{
	[TestFixture]
	public class HighlightTests
	{
		[Test]
		public void TestHighlight()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.Highlight(h => h
					.HighlightQuery(hq => hq
						.Match(m => m
							.OnField(p => p.Name)
							.Query("nest")
						)
					)
					.BoundaryCharacters(".,!? \t\n")
					.BoundaryMaxSize(20)
					.Encoder("html")
					.FragmentOffset(0)
					.FragmentSize(3)
					.NumberOfFragments(5)
					.Order("sort")
					.PreTags("<b>")
					.PostTags("</b>")
					.RequireFieldMatch(true)
					.OnFields(
					f => f
						.OnAll()
                        .NoMatchSize(200)
						.PreTags("<em>")
						.PostTags("</em>")
					)
				);
			var json = TestElasticClient.Serialize(s);
			var expected = @"  {
		  from: 0,
		  size: 10,
		  highlight: {
			pre_tags: [""<b>""],
			post_tags: [""</b>""],
			fragment_size: 3,
			number_of_fragments: 5,
			fragment_offset: 0,
			boundary_max_size: 20,
			encoder: ""html"",
			order: ""sort"",
			fields: {
			  _all: {
				pre_tags: [""<em>""],
				post_tags: [""</em>""],
				no_match_size: 200
			  }
			},
			require_field_match: true,
			boundary_chars: "".,!? \t\n"",
			highlight_query: {
				match: {
					name: {
						query: ""nest""
					}
				}
			}
		  }
		}";
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
