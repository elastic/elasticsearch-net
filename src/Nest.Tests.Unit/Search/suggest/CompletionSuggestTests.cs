using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.Tests.Unit.Search.Suggest
{
    [TestFixture]
    public class CompletionSuggestTests : BaseJsonTests
    {
        [Test]
        public void CompletionSuggestDescriptorTest()
        {
            var completionSuggestDescriptor = new CompletionSuggestDescriptor<ElasticSearchProject>()
                .OnField("suggest")
                .Text("n");

            var json = TestElasticClient.Serialize(completionSuggestDescriptor);

            var expected = @"{ ""field"": ""suggest"" }";

            Assert.IsTrue(json.JsonEquals(expected), json);
        }

        [Test]
        public void CompletionSuggestDescriptorDefaultFuzzyTest()
        {
            var completionSuggestDescriptor = new CompletionSuggestDescriptor<ElasticSearchProject>()
                .OnField("suggest")
                .Text("n")
                .Fuzzy();

            var json = TestElasticClient.Serialize(completionSuggestDescriptor);

            var expected = @"{
                              ""fuzzy"": {
                                ""edit_distance"": 1,
                                ""transpositions"": true,
                                ""min_length"": 3,
                                ""prefix_length"": 1
                              },
                              ""field"": ""suggest""
                            }";

            Assert.IsTrue(json.JsonEquals(expected), json);
        }

        [Test]
        public void CompletionSuggestDescriptorFuzzyTest()
        {
            var completionSuggestDescriptor = new CompletionSuggestDescriptor<ElasticSearchProject>()
                .OnField("suggest")
                .Text("n")
                .Fuzzy(f => f
                    .EditDistance(2)
                    .Transpositions(false)
                    .MinLength(5)
                    .PrefixLength(4));

            var json = TestElasticClient.Serialize(completionSuggestDescriptor);

            var expected = @"{
                              ""fuzzy"": {
                                ""edit_distance"": 2,
                                ""transpositions"": false,
                                ""min_length"": 5,
                                ""prefix_length"": 4
                              },
                              ""field"": ""suggest""
                            }";

            Assert.IsTrue(json.JsonEquals(expected), json);
        }

		[Test]
		public void CompletionSuggestOnSearchTest()
		{
			var search = this._client.Search<ElasticSearchProject>(s => s
				.SuggestCompletion("mycompletionsuggest", ts => ts
					.Text("n")
					.OnField(p=>p.Name)
					.Fuzzy()
				)
			);

			var expected = @"{
				suggest: {
					mycompletionsuggest: {
						text: ""n"",
						completion: {
							fuzzy: {
								edit_distance: 1,
								transpositions: true,
								min_length: 3,
								prefix_length: 1
							},
							field: ""name""
						}
					}
				}
			}";
			var json = search.ConnectionStatus.Request;
			Assert.True(json.JsonEquals(expected), json);
		}
    }
}
