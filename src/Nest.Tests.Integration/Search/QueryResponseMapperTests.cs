using System;
using System.Linq;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Search
{
	/// <summary>
	///  Tests that test whether the query response can be successfully mapped or not
	/// </summary>
	[TestFixture]
	public class QueryResponseMapperTests : BaseElasticSearchTests
	{
		private string _LookFor = NestTestData.Data.First().Followers.First().FirstName;


		protected void TestDefaultAssertions(IQueryResponse<ElasticSearchProject> queryResponse)
		{
			Assert.True(queryResponse.IsValid);
			Assert.NotNull(queryResponse.ConnectionStatus);
			Assert.Null(queryResponse.ConnectionStatus.Error);
			Assert.True(queryResponse.Total > 0, "No hits");
			Assert.True(queryResponse.Documents.Any());
			Assert.True(queryResponse.Documents.Count() > 0);
			Assert.True(queryResponse.Shards.Total > 0);
			Assert.True(queryResponse.Shards.Successful == queryResponse.Shards.Total);
			Assert.True(queryResponse.Shards.Failed == 0);
		}

		[Test]
		public void BogusQuery()
		{
			var client = this.ConnectedClient;
			IQueryResponse<ElasticSearchProject> queryResults = client.Search<ElasticSearchProject>(s=>s
				.QueryRawJson("here be dragons")
			);
			Assert.False(queryResults.IsValid);
			var error = queryResults.ConnectionStatus.Error;
			Assert.NotNull(error);
			Assert.True(error.HttpStatusCode == System.Net.HttpStatusCode.InternalServerError);
		}

		[Test]
		public void HitsMaxScoreIsSet()
		{
			//arrange
			//pull existing example through method we know is functional based on other passing unit tests
			var queryResults = this.ConnectedClient.Search<ElasticSearchProject>(s => s
				.QueryString("*")
				//.SortAscending(p => p.Id)
			);

			var hits = queryResults.Hits;

			Assert.AreEqual(1, hits.MaxScore);
			Assert.AreEqual(hits.Hits.Max(h => h.Score), hits.MaxScore);
		}

		[Test]
		public void HitsScoreIsSet()
		{
			//arrange
			//pull existing example through method we know is functional based on other passing unit tests
			var queryResults = this.ConnectedClient.Search<ElasticSearchProject>(s => s
				.QueryString("*")
				.SortAscending("_score")
				.SortDescending(p=>p.Id)
			);

			var hits = queryResults.Hits;

			Assert.True(hits.Hits.All(h=>h.Sorts.Count() == 2));
		}

		[Test]
		public void BoolQuery()
		{
			var lookFor = this._LookFor.ToLower();
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						""bool"" : {
							""must"" : {
								""term"" : { ""followers.firstName"" : """ + lookFor + @""" }
							},
							""must_not"" : {
								""range"" : {
									""id"" : { ""from"" : 1, ""to"" : 20 }
								}
							},
							""should"" : [
								{
									""term"" : { ""followers.firstName"" : """ + lookFor + @""" }
								},
								{
									""term"" : { ""followers.firstName"" : """ + lookFor + @""" }
								}
							],
							""minimum_number_should_match"" : 1,
							""boost"" : 1.0
						}	
					} }"
			);
			this.TestDefaultAssertions(queryResults);
			Assert.True(queryResults.Documents.First().Followers
				.Any(f => f.FirstName.Equals(this._LookFor, StringComparison.InvariantCultureIgnoreCase)));
		}

		[Test]
		public void BoostingQuery()
		{
			var boost2nd = NestTestData.Data.ToList()[2].Followers.First().FirstName.ToLower();
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						""boosting"" : {
							""positive"" : {
								""term"" : {
									""followers.firstName"" : """ + boost2nd + @"""
								}
							},
							""negative"" : {
								""term"" : {
									""followers.firstName"" : """ + this._LookFor.ToLower() + @"""
								}
							},
							""negative_boost"" : 0.2
						}
					} }"
			);
			this.TestDefaultAssertions(queryResults);
			Assert.True(queryResults.Documents.First().Followers.First().FirstName != this._LookFor);
		}

		[Test]
		public void ScoringQuery()
		{
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						""custom_score"" : {
							""query"" : {
								""term"" : {
									""followers.firstName"" : """ + this._LookFor.ToLower() + @"""
								}
							},
							""script"" : ""_score * 2""
						}
					} }"
			);
			this.TestDefaultAssertions(queryResults);
		}
		[Test]
		public void ConstantScoreQuery()
		{
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						""constant_score"" : {
							""filter"" : {
								""term"" : {
									""followers.firstName"" : """ + this._LookFor.ToLower() + @"""
								}
							},
							""boost"" : 1.2
						}
					} }"
			);
			this.TestDefaultAssertions(queryResults);
		}
		[Test]
		public void DismaxQuery()
		{
			var boost2nd = NestTestData.Data.ToList()[2].Followers.First().FirstName.ToLower();


			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						""dis_max"" : {
							""tie_breaker"" : 0.7,
							""boost"" : 1.2,
							""queries"" : [
								{
									""term"" : {
										""followers.firstName"" : """ + boost2nd + @"""
									}
								},
								{
									""term"" : {
										""followers.firstName"" : """ + this._LookFor.ToLower() + @"""
									}
								}
							]
						}
					} }"
			);
			this.TestDefaultAssertions(queryResults);
		}
		[Test]
		public void FieldQuery()
		{
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						 ""field"" : { 
							""followers.firstName"" : ""+" + this._LookFor.ToLower() + @" -something else""
						}
					} }"
			);
			this.TestDefaultAssertions(queryResults);
		}
		[Test]
		public void ExtendedFieldQuery()
		{
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						""field"" : { 
							""followers.firstName"" : {
								""query"" : ""+" + this._LookFor.ToLower() + @" -something else"",
								""boost"" : 2.0,
								""enable_position_increments"": false
							}
						}
					} }"
			);
			this.TestDefaultAssertions(queryResults);
		}
		[Test]
		public void FilteredQuery()
		{
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						""filtered"" : {
							""query"" : {
								""term"" : {
									""followers.firstName"" : """ + this._LookFor.ToLower() + @"""
								}
							},
							""filter"" : {
								""range"" : {
									""id"" : { ""from"" : 1, ""to"" : 20 }
								}
							}
						}
					} }"
			);
			this.TestDefaultAssertions(queryResults);
		}
		[Test]
		public void FuzzyLikeThisQuery()
		{
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						""fuzzy_like_this"" : {
							""fields"" : [""_all""],
							""like_text"" : """ + this._LookFor.ToLower() + @"x"",
							""max_query_terms"" : 12
						}
					} }"
			);
			this.TestDefaultAssertions(queryResults);
		}
		[Test]
		public void FuzzyLikeThisFieldQuery()
		{
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						 ""fuzzy_like_this_field"" : {
							""followers.firstName"" : {
								""like_text"" : """ + this._LookFor.ToLower() + @"x"",
								""max_query_terms"" : 12
							}
						}
					} }"
			);
			this.TestDefaultAssertions(queryResults);
		}
		[Test]
		public void FuzzyQuery()
		{
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						 ""fuzzy"" : {
							""followers.firstName"" : """ + this._LookFor.ToLower() + @"x""
						}
					} }"
			);
			this.TestDefaultAssertions(queryResults);
			Assert.True(queryResults.Documents.First().Followers
				.Any(f => f.FirstName.Equals(this._LookFor, StringComparison.InvariantCultureIgnoreCase)));
		}
		[Test]
		public void ExtendedFuzzyQuery()
		{
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						  ""fuzzy"" : { 
							""followers.firstName"" : {
								""value"" : """ + this._LookFor.ToLower() + @"x"",
								""boost"" : 1.0,
								""min_similarity"" : 0.5,
								""prefix_length"" : 0
							}
						}
					} }"
			);
			this.TestDefaultAssertions(queryResults);
			Assert.True(queryResults.Documents.First().Followers
				.Any(f => f.FirstName.Equals(this._LookFor, StringComparison.InvariantCultureIgnoreCase)));
		}

		//TODO: has_child query support!

		[Test]
		public void MatchAllQuery()
		{
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
							""match_all"" : { }
					} }"
			);
			this.TestDefaultAssertions(queryResults);
			Assert.True(queryResults.Total > 0);
			
		}

		[Test]
		public void MoreLikeThisQuery()
		{
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						""more_like_this"" : {
							""fields"" : [""_all""],
							""like_text"" : """ + this._LookFor.ToLower() + @""",
							""max_query_terms"" : 12,
							""min_doc_freq"" : 1,
							""min_term_freq"" : 1
						}
					} }"
			);
			this.TestDefaultAssertions(queryResults);
		}
		[Test]
		public void MoreLikeThisFieldQuery()
		{
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						 ""more_like_this_field"" : {
							""followers.firstName"" : {
								""like_text"" : """ + this._LookFor.ToLower() + @""",
								""min_doc_freq"" : 1,
								""min_term_freq"" : 1,
								""max_query_terms"" : 12
							}
						}
					} }"
			);
			this.TestDefaultAssertions(queryResults);
		}
		[Test]
		public void PrefixQuery()
		{
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						 ""prefix"" : {
							""followers.firstName"" : """ + this._LookFor.ToLower().Substring(0, 4) + @"""
						}
					} }"
			);
			this.TestDefaultAssertions(queryResults);
		}
		[Test]
		public void PrefixExtendedQuery()
		{
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						 ""prefix"" : {
							""followers.firstName"" : { ""value"" : """ + this._LookFor.ToLower().Substring(0, 4) + @""", ""boost"" : 1.2 }
						}
					} }"
			);
			this.TestDefaultAssertions(queryResults);
		}
		[Test]
		public void QueryStringQuery()
		{
			var firstFollower = NestTestData.Data.First().Followers.First();
			var firstName = firstFollower.FirstName;
			var lastName = firstFollower.LastName;
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						""query_string"" : { 
							""default_field"" : ""_all"", 
							""query"" : """+firstName+@" AND "+lastName+@"""
						}
					} }"
			);
			this.TestDefaultAssertions(queryResults);
		}
		[Test]
		public void QueryStringMultiFieldQuery()
		{
			var firstFollower = NestTestData.Data.First().Followers.First();
			var firstName = firstFollower.FirstName;
			var lastName = firstFollower.LastName;
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						""query_string"" : { 
							""fields"" : [""followers.firstName"", ""followers.lastName^5""], 
							""query"" : """ + firstName + @" OR " + lastName + @"""
						}
					} }"
			);
			this.TestDefaultAssertions(queryResults);
		}

		[Test]
		public void RangeQuery()
		{
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						 ""range"" : {
							""id"" : { 
								""from"" : 1, 
								""to"" : 20, 
								""include_lower"" : true, 
								""include_upper"": false, 
								""boost"" : 2.0
							}
						}
					} }"
			);
			this.TestDefaultAssertions(queryResults);
		}

		//TODO: Update test data to include a blob of text so we can write decent span_* queries tests

		[Test]
		public void TermQuery()
		{
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						 ""term"" : {
							""followers.firstName"" : """ + this._LookFor.ToLower() + @"""
						}
					} }"
			);
			this.TestDefaultAssertions(queryResults);
			Assert.True(queryResults.Documents.First().Followers
				.Any(f => f.FirstName.Equals(this._LookFor, StringComparison.InvariantCultureIgnoreCase)));
		}
		[Test]
		public void ExtendedTermQuery()
		{
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						 ""term"" : {
							""followers.firstName"" : { ""value"" : """ + this._LookFor.ToLower() + @""", ""boost"" : 2.0 }
						}
					} }"
			);
			this.TestDefaultAssertions(queryResults);
			Assert.True(queryResults.Documents.First().Followers
				.Any(f => f.FirstName.Equals(this._LookFor, StringComparison.InvariantCultureIgnoreCase)));
		}
		[Test]
		public void TermsQuery()
		{
			var firstFollower = NestTestData.Data.First().Followers.First();
			var firstName = firstFollower.FirstName.ToLower();
			var lastName = firstFollower.LastName.ToLower();

			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						 ""terms"" : {
							""followers.firstName"" : [ """ + firstName + @""", """ + lastName + @""" ],
							""minimum_match"" : 1
						}
					} }"
			);
			this.TestDefaultAssertions(queryResults);
			Assert.True(queryResults.Documents.First().Followers
				.Any(f => f.FirstName.Equals(this._LookFor, StringComparison.InvariantCultureIgnoreCase)));
		}

		[Test]
		public void WildcardQuery()
		{
			var wildcard = this._LookFor.ToLower().Substring(0, this._LookFor.Length - 1).Replace("a", "?") + "*";
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						 ""wildcard"" : {
							""followers.firstName"" : """ + wildcard + @"""
						}
					} }"
			);
			this.TestDefaultAssertions(queryResults);
			Assert.True(queryResults.Documents.First().Followers
				.Any(f => f.FirstName.Equals(this._LookFor, StringComparison.InvariantCultureIgnoreCase)));
		}
		[Test]
		public void WildcardExtendedQuery()
		{
			var wildcard = this._LookFor.ToLower().Substring(0, this._LookFor.Length - 1).Replace("a", "?") + "*";
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(
				@" { ""query"" : {
						 ""wildcard"" : {
							""followers.firstName"" : { ""value"" : """ + wildcard + @""", ""boost"" : 2.0 }
						}
					} }"
			);
			this.TestDefaultAssertions(queryResults);
			Assert.True(queryResults.Documents.First().Followers
				.Any(f => f.FirstName.Equals(this._LookFor, StringComparison.InvariantCultureIgnoreCase)));
		}
		[Test]
		public void QueryWithHighlightTest()
		{
			//arrange
			var firstFollower = NestTestData.Data.First().Followers.First();
			var firstName = firstFollower.FirstName.ToLower();
			string query = "{\"query\":{\"query_string\":{\"default_field\":\"_all\",\"query\":\"pork\"}},\"highlight\":{\"pre_tags\":[\"<span class=\\\"searchTerm\\\">\"],\"post_tags\":[\"</span>\"],\"fields\":{\"content\":{\"fragment_size\":150,\"number_of_fragments\":3}}}}";

			//act
			var queryResults = this.ConnectedClient.SearchRaw<ElasticSearchProject>(query);

			//assert
			Assert.IsTrue(queryResults.DocumentsWithMetaData.First().Highlight["content"].Count > 0);
	}
	//TODO: Implement top_children once we support mapping and mapping of parent child relations.
	}
}
