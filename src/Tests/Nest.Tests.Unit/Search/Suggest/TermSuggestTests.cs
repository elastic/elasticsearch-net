using Elasticsearch.Net;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Suggest
{

	[TestFixture]
	public class TermSuggestTests : BaseJsonTests
	{
		[Test]
        public void TermSuggestDescriptorTest()
		{
		    var termSuggestDescriptor = new TermSuggestDescriptor<ElasticsearchProject>()
		        .MaxEdits(3)
		        .MaxInspections(17)
		        .OnField("field1")
		        .Size(3)
		        .SuggestMode(SuggestMode.Missing);

            var json = TestElasticClient.Serialize(termSuggestDescriptor);

            var expected = @"{
                              ""field"": ""field1"",
                              ""size"": 3,
                              ""suggest_mode"": ""missing"",
                              ""max_edits"": 3,
                              ""max_inspections"": 17
                            }";

            Assert.True(json.JsonEquals(expected), json);
        }

		[Test]
		public void TermSuggestOnSearchTest()
		{
			var search = this._client.Search<ElasticsearchProject>(s => s
				.SuggestTerm("mytermsuggest", ts=>ts
					.Text("n")
					.MaxEdits(3)
					.MaxInspections(17)
					.OnField("field1")
					.Size(3)
					.SuggestMode(SuggestMode.Missing)
				)
			);

			var expected = @"{
				suggest: {
					mytermsuggest: {
						text: ""n"",
						term: {
							field: ""field1"",
							size: 3,
							suggest_mode: ""missing"",
							max_edits: 3,
							max_inspections: 17

						}
					}
				}
			}";
			var json = search.ConnectionStatus.Request.Utf8String();
			Assert.True(json.JsonEquals(expected), json);
		}


	} 
}
