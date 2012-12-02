using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Nest;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Nest.Tests.MockData.Domain;
using System.Linq.Expressions;

namespace Nest.Tests.Unit.FilterJson.ConditionLess
{
	[TestFixture]
	public class ConditionLessTests : BaseJsonTests
	{
		public class Criteria
		{
			public string Name1 { get; set; }
			public string Name2 { get; set; }
			public int? Int1 { get; set; }
			public DateTime? Date { get; set; }
		}
		private readonly Criteria _c = new Criteria();

		private void DoConditionlessFilter(Func<FilterDescriptor<ElasticSearchProject>, BaseFilter> filter)
		{
			var criteria = new Criteria { };
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Take(10)
				.Filter(filter);

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod(), "MatchAll");
		}
		private void DoNonConditionlessFilter(Func<FilterDescriptor<ElasticSearchProject>, BaseFilter> filter)
		{
			var criteria = new Criteria { };
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Take(10)
				.Filter(filter);

			this.JsonNotEquals(s, System.Reflection.MethodInfo.GetCurrentMethod(), "MatchAll");
		}

		[Test]
		public void AndTest()
		{
			this.DoConditionlessFilter(f => f.And());
		}


		[Test]
		public void BoolTest()
		{
			this.DoConditionlessFilter(f => f.Bool(bf=>bf.Must()));
			this.DoConditionlessFilter(f => f.Bool(bf => bf.Should()));
			this.DoConditionlessFilter(f => f.Bool(bf => bf.MustNot()));
			this.DoConditionlessFilter(f => f.Bool(bf => bf.MustNot().Should().Must()));
		}


		[Test]
		public void ExistsTest()
		{
			this.DoConditionlessFilter(f => f.Exists(string.Empty));
		}


		[Test]
		public void GeoBoundingBoxStringTest()
		{
			this.DoConditionlessFilter(f => f.GeoBoundingBox(string.Empty, string.Empty, string.Empty));
			this.DoConditionlessFilter(f => f.GeoBoundingBox("randomfield", "topleft", string.Empty));
			this.DoConditionlessFilter(f => f.GeoBoundingBox("randomfield", string.Empty, "bottomRight"));
			this.DoNonConditionlessFilter(f => f.GeoBoundingBox("randomfield", "topleft", "bottomRight"));
		}


		[Test]
		public void GeoBoundingBoxTest()
		{
			this.DoConditionlessFilter(f => f.GeoBoundingBox(string.Empty, 0, 0, 0, 0));
		}



		[Test]
		public void GeoDistanceTest()
		{
			this.DoConditionlessFilter(f => f.GeoDistance(string.Empty, null));
			this.DoConditionlessFilter(f => f.GeoDistance(string.Empty, ff=> ff.Location(string.Empty)));
			this.DoConditionlessFilter(f => f.GeoDistance("myfield", ff=> ff.Location(string.Empty)));
		}



		[Test]
		public void GeoDistanceRangeTest()
		{
			this.DoConditionlessFilter(f => f.GeoDistanceRange(string.Empty, null));
			this.DoConditionlessFilter(f => f.GeoDistanceRange(string.Empty, ff=>ff.Location(string.Empty)));
			this.DoConditionlessFilter(f => f.GeoDistanceRange("myfield", ff => ff.Location(string.Empty)));
			this.DoConditionlessFilter(f => f.GeoDistanceRange("myfield", ff => ff.Location("mylocation").Distance(string.Empty, string.Empty)));
		}


		[Test]
		public void GeoPolygonTest()
		{
			this.DoConditionlessFilter(f => f.GeoPolygon(string.Empty));
			this.DoConditionlessFilter(f => f.GeoPolygon("myfield", Enumerable.Empty<string>().ToArray()));
		}


		[Test]
		public void HasChildTest()
		{
			this.DoConditionlessFilter(f => f.HasChild<Person>(null));
			this.DoConditionlessFilter(f => f.HasChild<Person>(q=>q.Query(qq=>qq.Term(p=>p.FirstName, string.Empty))));
		}


		[Test]
		public void IdsTest()
		{
			this.DoConditionlessFilter(f => f.Ids(Enumerable.Empty<string>()));
			this.DoConditionlessFilter(f => f.Ids(null));
			this.DoConditionlessFilter(f => f.Ids(string.Empty, Enumerable.Empty<string>()));
		}



		[Test]
		public void LimitTest()
		{
			this.DoConditionlessFilter(f => f.Limit(null));
		}

		[Test]
		public void MissingTest()
		{
			this.DoConditionlessFilter(f => f.Missing(string.Empty));
		}



		[Test]
		public void NestedTest()
		{
			this.DoConditionlessFilter(f => f.Nested(nf=>nf.Query(q=>q.Term(string.Empty, string.Empty))));
			this.DoConditionlessFilter(f => f.Nested(null));
		}


		[Test]
		public void NotTest()
		{
			this.DoConditionlessFilter(f => f.Not(null));
			this.DoConditionlessFilter(f => f.Not(nf=>nf.Term(string.Empty, string.Empty)));
		}


		[Test]
		public void NumericRangeTest()
		{
			this.DoConditionlessFilter(f => f.NumericRange(null));
			this.DoConditionlessFilter(f => f.NumericRange(nrf=>nrf.From(string.Empty)));
			int? nullInt = null;
			this.DoConditionlessFilter(f => f.NumericRange(nrf => nrf.From(nullInt)));
			double? nullDouble = null;
			this.DoConditionlessFilter(f => f.NumericRange(nrf => nrf.From(nullDouble)));
			DateTime? nullDate = null;
			this.DoConditionlessFilter(f => f.NumericRange(nrf => nrf.From(nullDate)));
		}


		[Test]
		public void OrTest()
		{
			this.DoConditionlessFilter(f => f.Or());
			this.DoConditionlessFilter(f => f.Or(of=>of.And()));
			this.DoNonConditionlessFilter(f => f.Or(of => of.And(), of=>of.Term("field", "value")));
		}


		[Test]
		public void PrefixTest()
		{
			this.DoConditionlessFilter(f => f.Prefix(string.Empty, string.Empty));
			this.DoConditionlessFilter(f => f.Prefix(string.Empty, "pre"));
			this.DoConditionlessFilter(f => f.Prefix("field", string.Empty));
		}


		[Test]
		public void QueryTest()
		{
			this.DoConditionlessFilter(f => f.Query(q=>q.Term(string.Empty, string.Empty)));
		}


		[Test]
		public void RangeTest()
		{
			this.DoConditionlessFilter(f => f.Range(null));
			this.DoConditionlessFilter(f => f.Range(nrf => nrf.From(string.Empty)));
			int? nullInt = null;
			this.DoConditionlessFilter(f => f.Range(nrf => nrf.From(nullInt)));
			double? nullDouble = null;
			this.DoConditionlessFilter(f => f.Range(nrf => nrf.From(nullDouble)));
			DateTime? nullDate = null;
			this.DoConditionlessFilter(f => f.Range(nrf => nrf.From(nullDate)));
		}


		[Test]
		public void ScriptTest()
		{
			this.DoConditionlessFilter(f => f.Script(s=>s.Script(string.Empty)));
		}


		[Test]
		public void TermTest()
		{
			this.DoConditionlessFilter(f => f.Term(string.Empty, string.Empty));
			this.DoConditionlessFilter(f => f.Term(string.Empty, "term"));
			this.DoConditionlessFilter(f => f.Term("field", string.Empty));
		}



		[Test]
		public void TermsTest()
		{
			this.DoConditionlessFilter(f => f.Terms(string.Empty, null));
			this.DoConditionlessFilter(f => f.Terms(string.Empty, Enumerable.Empty<string>()));
			this.DoConditionlessFilter(f => f.Terms("field", new string[] {"", "", ""}));
			this.DoNonConditionlessFilter(f => f.Terms("field", new string[] { "", "", "term" }));
		}


		[Test]
		public void TypeTest()
		{
			this.DoConditionlessFilter(f => f.Type(string.Empty));
			this.DoConditionlessFilter(f => f.Type(null));
		}



	}
}
