using System;
using System.Linq;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Facet
{
	/// <summary>
	///  Tests that test whether the query response can be successfully mapped or not
	/// </summary>
	[TestFixture]
	public class RangeFacetResponseTests : BaseFacetTestFixture
	{
		[Test]
		public void SimpleRangeFacet()
		{
			var queryResults = this.SearchRaw<ElasticSearchProject>(
				@"
				{ 
					""query"" : { ""match_all"" : { } },
					""facets"" : 
					{
						""loc"" : 
						{ 
							""range"" : 
							{
								""field"" : ""loc"",
								""ranges"" : [
									{ ""to"" : 10000 },
									{ ""from"" : 10000, ""to"" : 15000 },
									{ ""from"" : 15000, ""to"" : 20000 },
									{ ""from"" : 20000 }
								]
							}
						}
					}
				}"
			);
			
			var facet = queryResults.Facets["loc"];
			this.TestDefaultAssertions(queryResults);
			
            Assert.IsInstanceOf<RangeFacet>(facet);

            var rf = (RangeFacet)facet;

			Assert.AreEqual(rf.Items.Count(), 4);
			Assert.AreEqual(1, rf.Items.Count(r => r.From == null && r.To == 10000));
			Assert.AreEqual(1, rf.Items.Count(r => r.From == 10000 && r.To == 15000));
			Assert.AreEqual(1, rf.Items.Count(r => r.From == 15000 && r.To == 20000));
			Assert.AreEqual(1, rf.Items.Count(r => r.From == 20000 && r.To == null));
		}

		[Test]
		public void DateRangeFacet()
		{
			var queryResults = this.SearchRaw<ElasticSearchProject>(
				@"
				{ 
					""query"" : { ""match_all"" : { } },
					""facets"" : 
					{
						""dob"" : 
						{ 
							""range"" : 
							{
								""field"" : ""followers.dateOfBirth"",
								""ranges"" : [
									{ ""to"" : ""1900-01-01"" },
									{ ""from"" : ""1900-01-01"", ""to"" : ""1950-01-01"" },
									{ ""from"" : ""1950-01-01"", ""to"" : ""1980-01-01"" },
									{ ""from"" : ""1980-01-01"", ""to"" : ""1990-01-01"" },
									{ ""from"" : ""1990-01-01""}
								]
							}
						}
					}
				}"
			);

            var facet = queryResults.Facets["dob"];
            this.TestDefaultAssertions(queryResults);

            Assert.IsInstanceOf<DateRangeFacet>(facet);

            var drf = (DateRangeFacet)facet;

            Assert.AreEqual(1, drf.Items.Count(r => r.From == null && r.To.GetValueOrDefault() == new DateTime(1900, 1, 1)));
			Assert.AreEqual(1, drf.Items.Count(r => r.From.GetValueOrDefault() == new DateTime(1950, 1, 1) && r.To.GetValueOrDefault() == new DateTime(1980, 1, 1)));
			Assert.AreEqual(1, drf.Items.Count(r => r.From.GetValueOrDefault() == new DateTime(1980, 1, 1) && r.To.GetValueOrDefault() == new DateTime(1990, 1, 1)));
			Assert.AreEqual(1, drf.Items.Count(r => r.From.GetValueOrDefault() == new DateTime(1990, 1, 1) && r.To == null));

			Assert.AreEqual(drf.Items.Count(), 5);
		}
	}
}
