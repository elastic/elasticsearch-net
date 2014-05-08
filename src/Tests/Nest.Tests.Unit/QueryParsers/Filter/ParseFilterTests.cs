using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class ParseFilterTests : BaseParserTests
	{
		private BaseFilterDescriptor Filter1 = Filter<object>.Term("w", "x");
		private BaseFilterDescriptor Filter2 = Filter<object>.Term("y", "z");
		private BaseFilterDescriptor Filter3 = Filter<object>.Term("a", "b");

		[Test]
		[TestCase("cacheName", "cacheKey", true, "myterm")]
		[TestCase("cacheName", "cacheKey", false, "myterm")]
		public void Term_Deserializes(string cacheName, string cacheKey, bool cache, string term)
		{
			var termFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.Term,
				f=>f.Term(p=>p.Name, term)
			);
			termFilter.Field.Should().Be("name");
			termFilter.Value.Should().Be(term);
		}

		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void And_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var andFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.And,
				f=>f.And(Filter1, Filter2)
			);
			andFilter.Filters.Should().NotBeEmpty().And.HaveCount(2);

			AssertIsTermFilter(this.Filter1, andFilter.Filters.First().Term);
			AssertIsTermFilter(this.Filter2, andFilter.Filters.Last().Term);
		}

		private static void AssertIsTermFilter(BaseFilterDescriptor compareTo, ITermFilter firstTermFilter)
		{
			var c = (IFilterDescriptor)compareTo;
			firstTermFilter.Should().NotBeNull();
			firstTermFilter.Field.Should().Be(c.Term.Field);
			firstTermFilter.Value.Should().Be(c.Term.Value);
		}

		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Bool_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var boolFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.Bool,
				f=>f.Bool(b=>b.Must(Filter1).MustNot(Filter2).Should(Filter3))
			);
			boolFilter.Must.Should().NotBeEmpty().And.HaveCount(1);
			boolFilter.MustNot.Should().NotBeEmpty().And.HaveCount(1);
			boolFilter.Should.Should().NotBeEmpty().And.HaveCount(1);

			AssertIsTermFilter(this.Filter1, boolFilter.Must.First().Term);
			AssertIsTermFilter(this.Filter2, boolFilter.MustNot.First().Term);
			AssertIsTermFilter(this.Filter3, boolFilter.Should.First().Term);
		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Exists_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var existsFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.Exists,
				f=>f.Exists(p=>p.Name)
			);

			existsFilter.Field.Should().Be("name");
		}

		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void GeoBoundingBox_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var geoBoundingBox = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.GeoBoundingBox,
				f=>f.GeoBoundingBox(p=>p.Origin, 0.1, 0.2, 0.3, 0.4, GeoExecution.memory)
			);
			
			geoBoundingBox.Field.Should().Be("origin");
			geoBoundingBox.GeoExecution.Should().Be(GeoExecution.memory);
			geoBoundingBox.TopLeft.Should().Be("0.1, 0.2");
			geoBoundingBox.BottomRight.Should().Be("0.3, 0.4");
		}

		[Test]
		public void GeoBoundingBox_Array_Deserializes()
		{
			//nest will always generate top_left: ""; but while parsing it will need to handle top_left: [] as well
			var filter = this.CreateSearchDesriptor(f => f.GeoBoundingBox, MethodBase.GetCurrentMethod(), @"GeoBoundingBox\Array");
			filter.Field.Should().Be("origin");
			filter.GeoExecution.Should().Be(GeoExecution.memory);
			filter.TopLeft.Should().Be("0.1, 0.2");
			filter.BottomRight.Should().Be("0.3, 0.4");
		}
		
		[Test]
		public void GeoBoudingBox_Vertices_Serializes()
		{
			//nest will always generate top_left: ""; but while parsing it will need to handle top: left: right: bottom: as well
			var filter = this.CreateSearchDesriptor(f => f.GeoBoundingBox, MethodBase.GetCurrentMethod(), @"GeoBoundingBox\Vertices");
			filter.Field.Should().Be("origin");
			filter.GeoExecution.Should().Be(GeoExecution.memory);
			filter.TopLeft.Should().Be("-74.1, 40.73");
			filter.BottomRight.Should().Be("-71.12, 40.01");
		}

		[Test]
		public void GeoBoudingBox_LatLon_Serializes()
		{
			//nest will always generate top_left: ""; but while parsing it will need to handle top: left: right: bottom: as well
			var filter = this.CreateSearchDesriptor(f => f.GeoBoundingBox, MethodBase.GetCurrentMethod(), @"GeoBoundingBox\LatLon");
			filter.Field.Should().Be("origin");
			filter.GeoExecution.Should().Be(GeoExecution.memory);
			filter.TopLeft.Should().Be("-74.1, 40.73");
			filter.BottomRight.Should().Be("-71.12, 40.01");
		}
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void GeoDistance_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var geoDistanceFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.GeoDistance,
				f=>f.GeoDistance(p=>p.Origin, gd=>gd
					.Distance(1.0, GeoUnit.km)
					.Location(2.1, 4.1)
					.Optimize(GeoOptimizeBBox.indexed)
				)
			);

			geoDistanceFilter.Field.Should().Be("origin");
			geoDistanceFilter.Location.Should().Be("2.1, 4.1");
			geoDistanceFilter.OptimizeBoundingBox.Should().Be(GeoOptimizeBBox.indexed);
		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void GeoDistanceRange_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var geoDistanceRangeFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.GeoDistanceRange,
				f=>f.GeoDistanceRange(p=>p.Origin, d=>d
					.Location(Lat: 40, Lon: -70)
					.Distance(From: 12, To: 200, Unit: GeoUnit.km)
					.FromExclusive()
					.ToExclusive()
					.DistanceType(GeoDistanceType.arc)
					.Optimize(GeoOptimizeBBox.memory)
				)
			);

			geoDistanceRangeFilter.Field.Should().Be("origin");

			var from = (double)(geoDistanceRangeFilter.From);
			from.Should().Be(12);
			geoDistanceRangeFilter.Unit.Should().Be(GeoUnit.km);
			geoDistanceRangeFilter.DistanceType.Should().Be(GeoDistanceType.arc);
		}
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void GeoDistanceRange_FromString_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var geoDistanceRangeFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.GeoDistanceRange,
				f=>f.GeoDistanceRange(p=>p.Origin, d=>d
					.Location(Lat: 40, Lon: -70)
					.Distance(From: "12km", To:"15km")
					.FromExclusive()
					.ToExclusive()
					.DistanceType(GeoDistanceType.arc)
					.Optimize(GeoOptimizeBBox.memory)
				)
			);
			geoDistanceRangeFilter.Field.Should().Be("origin");
			var from = (string)(geoDistanceRangeFilter.From);
			from.Should().Be("12km");
			geoDistanceRangeFilter.DistanceType.Should().Be(GeoDistanceType.arc);
		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void GeoPolygon_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var geoPolygonFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.GeoPolygon,
				f=>f.GeoPolygon(p => p.Origin, new List<Tuple<double, double>>
				{
					Tuple.Create(30.0, -80.0), Tuple.Create(20.0, -90.0)
				})
			);

			geoPolygonFilter.Field.Should().Be("origin");
			geoPolygonFilter.Points.Should().BeEquivalentTo(new []{"30, -80", "20, -90"});
		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void GeoShape_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var geoBaseShapeFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.GeoShape,
				f=>f.GeoShape(p=>p.Origin, d=>d
						.Type("envelope")
						.Coordinates(new[] { new[] { 13.0, 53.0 }, new[] { 14.0, 52.0 } })
					)
			);
			geoBaseShapeFilter.Field.Should().Be("origin");
			var geoShapeFilter = geoBaseShapeFilter as IGeoShapeFilter;
			geoShapeFilter.Should().NotBeNull();
			geoShapeFilter.Shape.Should().NotBeNull();
			geoShapeFilter.Shape.Type.Should().Be("envelope");

		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void GeoIndexShape_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var geoBaseShapeFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.GeoShape,
				f=>f.GeoIndexedShape(p=>p.Origin, d=>d
						.Lookup<ElasticsearchProject>(p=>p.Origin, "1337", index: "myindex", type:"mytype")
					)
			);
			geoBaseShapeFilter.Field.Should().Be("origin");
			var geoShapeFilter = geoBaseShapeFilter as IGeoIndexedShapeFilter;
			geoShapeFilter.Should().NotBeNull();
			geoShapeFilter.IndexedShape.Should().NotBeNull();
			geoShapeFilter.IndexedShape.Field.Should().Be("origin");
			geoShapeFilter.IndexedShape.Id.Should().Be("1337");
			geoShapeFilter.IndexedShape.Index.Should().Be("myindex");
			geoShapeFilter.IndexedShape.Type.Should().Be("mytype");

		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void HasChild_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var hasChildFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.HasChild,
				f=>f.HasChild<Person>(d=>d
						.Scope("my_scope")
						.Query(q=>q.Term(p=>p.FirstName, "value"))
					)
			);

			hasChildFilter._Scope.Should().Be("my_scope");
			var query = hasChildFilter._QueryDescriptor;
			query.Should().NotBeNull();
			query.TermQueryDescriptor.Field.Should().Be("firstName");
		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void HasParent_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var hasParentFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.HasParent,
				f=>f.HasParent<ElasticsearchProject>(d=>d
						.Scope("my_scope")
						.Query(q=>q.Term(p=>p.Country, "value"))
					)
			);
			hasParentFilter.Scope.Should().Be("my_scope");
			var query = hasParentFilter.Query;
			query.Should().NotBeNull();
			query.TermQueryDescriptor.Field.Should().Be("country");
		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Ids_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var idsFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.Ids,
				f=>f.Ids(new []{"my_type", "my_other_type"}, new[] { "1", "4", "100" })
			);

			idsFilter.Values.Should().BeEquivalentTo(new[] { "1", "4", "100" });
			idsFilter.Type.Should().BeEquivalentTo(new[] { "my_type", "my_other_type" });
		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Limit_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var limitFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.Limit,
				f=>f.Limit(100)
			);
			limitFilter.Value.Should().Be(100);
		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void MatchAll_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var matchAllFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.MatchAll,
				f=>f.MatchAll()
			);
		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Missing_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var missingFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.Missing,
				f=>f.Missing(p=>p.Name)
			);

			missingFilter.Field.Should().Be("name");
		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Nested_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var nestedFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.Nested,
				f=>f.Nested(n=>n
						.Scope("my-scope")
						.Score(NestedScore.max)
						.Path(p=>p.Followers[0])
						.Query(q=>q.Term(p=>p.Followers[0].FirstName,"elasticsearch.pm"))
					)
			);
			nestedFilter.Path.Should().Be("followers");
			nestedFilter.Scope.Should().Be("my-scope");
			nestedFilter.Score.Should().Be(NestedScore.max);
			var query = nestedFilter.Query;
			query.Should().NotBeNull();
			var termQuery = query.TermQueryDescriptor;
			termQuery.Field.Should().Be("followers.firstName");
			termQuery.Value.Should().Be("elasticsearch.pm");
		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Not_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var notFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.Not,
				f=>f.Not(ff => Filter1)
			);
			AssertIsTermFilter(Filter1, notFilter.Filter.Term);
		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void NumericRange_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var numericRangeFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.NumericRange,
				f=>f.NumericRange(n=>n
						.OnField(p=>p.LOC)
						.From(10)
						.To(20)
						.FromExclusive()
					)
			);

			numericRangeFilter.Field.Should().Be("loc");
			numericRangeFilter.From.Should().Be(10);
			numericRangeFilter.To.Should().Be(20);
			numericRangeFilter.IncludeUpper.Should().Be(true);
			numericRangeFilter.IncludeLower.Should().Be(false);
		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Or_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var orFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.Or,
				f=>f.Or(Filter1, Filter2)
			);

			orFilter.Filters.Should().NotBeEmpty().And.HaveCount(2);
			AssertIsTermFilter(Filter1, orFilter.Filters.First().Term);
			AssertIsTermFilter(Filter2, orFilter.Filters.Last().Term);
		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Prefix_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var prefixFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.Prefix,
				f=>f.Prefix(p=>p.Name, "elast")
			);

			prefixFilter.Field.Should().Be("name");
			prefixFilter.Prefix.Should().Be("elast");
		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Query_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var queryFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.Query,
				f=>f.Query(q=>q.Term(p=>p.Name,"elasticsearch.pm"))
			);
			queryFilter.Query.Should().NotBeNull();
		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Range_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var rangeFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.Range,
				f=>f.Range(n => n
						.OnField(p=>p.LOC)
						.From("10")
						.To("20")
						.FromExclusive()
					)
			);

			rangeFilter.Field.Should().Be("loc");
			rangeFilter.From.Should().Be(10);
			rangeFilter.To.Should().Be(20);
			rangeFilter.IncludeLower.Should().Be(false);
			rangeFilter.IncludeUpper.Should().Be(true);

		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Regexp_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var regexpFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.Regexp,
				f=>f.Regexp(r => r
						.OnField(p => p.Name)
						.Value("ab?")
						.Flags("INTERSECTION|COMPLEMENT|EMPTY")
					)
			);
			regexpFilter.Field.Should().Be("name");
			regexpFilter.Value.Should().Be("ab?");
			regexpFilter.Flags.Should().Be("INTERSECTION|COMPLEMENT|EMPTY");
		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Script_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var scriptFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.Script,
				f=>f.Script(sc => sc
						.Script("doc['num1'].value > param1")
						.Params(p => p.Add("param1", 12))
						.Lang(Lang.mvel)
					)
			);
			scriptFilter._Script.Should().Be("doc['num1'].value > param1");
			scriptFilter._Params.Should().NotBeEmpty().And.HaveCount(1);
			var keyValuePair = scriptFilter._Params.First();
			keyValuePair.Key.Should().Be("param1");
		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void TermsFilterLookup_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var termsBaseFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.Terms,
				f=>f.TermsLookup(p => p.Name, l=>l
					.Lookup<ElasticsearchProject>(p=>p.NestedFollowers.First().FirstName, "1347", "idx", "tpe")
				)
			);
			termsBaseFilter.Field.Should().Be("name");
			var termsFilter = termsBaseFilter as ITermsLookupFilter;
			termsFilter.Should().NotBeNull();
			termsFilter.Id.Should().Be("1347");
			termsFilter.Index.Should().Be("idx");
			termsFilter.Type.Should().Be("tpe");
			termsFilter.Path.Should().Be("nestedFollowers.firstName");
		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void TermsFilter_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var termsBaseFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.Terms,
				f=>f.Terms(p => p.Name, new [] {"elasticsearch.pm"}, Execution:TermsExecution.@bool)
			);
			termsBaseFilter.Field.Should().Be("name");
			var termsFilter = termsBaseFilter as ITermsFilter;
			termsFilter.Should().NotBeNull();
			termsFilter.Execution.Should().Be(TermsExecution.@bool);
			termsFilter.Terms.Should().BeEquivalentTo(new []{"elasticsearch.pm"});
		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void TypeFilter_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var typeFilter = this.TestBaseFilterProperties(cacheName, cacheKey, cache, 
				f=>f.Type,
				f=>f.Type("my-type")
			);
			typeFilter.Value.Should().Be("my-type");
		}
		
		private T CreateSearchDesriptor<T>(Func<IFilterDescriptor, T> filterBaseSelector, MethodBase method, string fileName = null)
			where T : IFilterBase
		{
			var descriptor = this.DeserializeInto<SearchDescriptor<ElasticsearchProject>>(method, fileName);
			var filter = filterBaseSelector(((ISearchDescriptor)descriptor).Filter);
			filter.Should().NotBeNull();
			return filter;
		}
		
		private T TestBaseFilterProperties<T>(string cacheName, string cacheKey, bool cache, 
			Func<IFilterDescriptor, T> filterBaseSelector,
			Func<FilterDescriptorDescriptor<ElasticsearchProject>, BaseFilterDescriptor> create
			)
			where T : IFilterBase
		{
			var descriptor = this.GetSearchDescriptor(s=>s
				.Filter(f=>create(f
					.Name(cacheName)
					.CacheKey(cacheKey)
					.Cache(cache)
				))
			);
			var filter = filterBaseSelector(descriptor.Filter);
			filter.Should().NotBeNull();
			filter.CacheName.Should().Be(cacheName);
			filter.Cache.Should().Be(cache);
			filter.CacheKey.Should().Be(cacheKey);
			return filter;
		}
	}
}
