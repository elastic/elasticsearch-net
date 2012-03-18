using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;
using Nest.TestData;
using Nest.TestData.Domain;
using NUnit.Framework;


namespace Nest.Tests.Search
{
	[TestFixture]
	public class ExplainTests : BaseElasticSearchTests
	{
		private string _LookFor = NestTestData.Data.First().Followers.First().FirstName;

		[Test]
		public void SimpleExplain()
		{
			var queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
					@" {
						""explain"": true,
						""query"" : {
							""match_all"" : { }
					} }"
				);
			Assert.True(queryResults.DocumentsWithMetaData.All(h=>h.Explanation != null));
			Assert.True(queryResults.DocumentsWithMetaData.All(h => h.Explanation.Details.Any()));
			Assert.True(queryResults.DocumentsWithMetaData.All(h => h.Explanation.Details.All(d=>!d.Description.IsNullOrEmpty())));
		}
		[Test]
		public void ComplexExplain()
		{
			var queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
					@" { ""explain"": true, 
						""query"" : {
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

			Assert.True(queryResults.DocumentsWithMetaData.All(h=>h.Explanation != null));
			Assert.True(queryResults.DocumentsWithMetaData.All(h => h.Explanation.Details.Any()));
			Assert.True(queryResults.DocumentsWithMetaData.All(h => h.Explanation.Details.All(d=>!d.Description.IsNullOrEmpty())));
		}

		
	}
}
;