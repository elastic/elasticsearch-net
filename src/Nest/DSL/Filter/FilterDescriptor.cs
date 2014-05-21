using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Globalization;
using Elasticsearch.Net;
using Nest.Resolvers;
using System.Collections;

namespace Nest
{
	public class FilterDescriptor<T> : FilterContainer
		where T : class
	{

		public FilterDescriptor<T> Name(string name)
		{
			this._Name = name;
			return this;
		}
		public FilterDescriptor<T> CacheKey(string cacheKey)
		{
			this._CacheKey = cacheKey;
			return this;
		}
		public FilterDescriptor<T> Cache(bool cache)
		{
			this._Cache = cache;
			return this;
		}
		

		public FilterDescriptor<T> Strict(bool strict = true)
		{
			var f = new FilterDescriptor<T>();
			((IFilterContainer)f).IsStrict = strict;
			((IFilterContainer)f).IsVerbatim = ((IFilterContainer)this).IsVerbatim;
			return f;
		}

		public FilterDescriptor<T> Verbatim(bool verbatim = true)
		{
			var f = new FilterDescriptor<T>();
			((IFilterContainer)f).IsStrict = ((IFilterContainer)this).IsStrict;
			((IFilterContainer)f).IsVerbatim = verbatim;
			return f;
		}

		/// <summary>
		/// A thin wrapper allowing fined grained control what should happen if a filter is conditionless
		/// if you need to fallback to something other than a match_all query
		/// </summary>
		public FilterContainer Conditionless(Action<ConditionlessFilterDescriptor<T>> selector)
		{
			var filter = new ConditionlessFilterDescriptor<T>();
			selector(filter);

			return (filter.FilterDescriptor == null || filter.FilterDescriptor.IsConditionless) ? filter._Fallback : filter.FilterDescriptor;
		}

		internal FilterContainer Raw(string rawJson)
		{
			var f = new FilterDescriptor<T>();
			((IFilterContainer)f).IsStrict = ((IFilterContainer)this).IsStrict;
			((IFilterContainer)f).IsVerbatim = ((IFilterContainer)this).IsVerbatim;
			((IFilterContainer)f).RawFilter = rawJson;
			return f;
		}

		/// <summary>
		/// Filters documents where a specific field has a value in them.
		/// </summary>
		public FilterContainer Exists(Expression<Func<T, object>> fieldDescriptor)
		{
			var filter = new ExistsFilter();
			((IExistsFilter)filter).Field = fieldDescriptor;
			this.SetCacheAndName(filter);
			return this.New(filter, f => f.Exists = filter);
		}
		/// <summary>
		/// Filters documents where a specific field has a value in them.
		/// </summary>
		public FilterContainer Exists(string field)
		{
			var filter = new ExistsFilter();
			((IExistsFilter)filter).Field = field;
			this.SetCacheAndName(filter);
			return this.New(filter, f => f.Exists = filter);
		}
		/// <summary>
		/// Filters documents where a specific field has no value in them.
		/// </summary>
		public FilterContainer Missing(Expression<Func<T, object>> fieldDescriptor)
		{
			var filter = new MissingFilter();
			((IMissingFilter)filter).Field = fieldDescriptor;
			this.SetCacheAndName(filter);
			return  this.New(filter, f => f.Missing = filter);
		}
		/// <summary>
		/// Filters documents where a specific field has no value in them.
		/// </summary>
		public FilterContainer Missing(string field)
		{
			var filter = new MissingFilter();
			((IMissingFilter)filter).Field = field;
			this.SetCacheAndName(filter);
			return  this.New(filter, f => f.Missing = filter);
		}
		/// <summary>
		/// Filters documents that only have the provided ids. 
		/// Note, this filter does not require the _id field to be indexed since it works using the _uid field.
		/// </summary>
		public FilterContainer Ids(IEnumerable<string> values)
		{
			var filter = new IdsFilter();
			((IIdsFilter)filter).Values = values;
			this.SetCacheAndName(filter);
			return this.New(filter, f => f.Ids = filter);
		}
		/// <summary>
		/// Filters documents that only have the provided ids. 
		/// Note, this filter does not require the _id field to be indexed since it works using the _uid field.
		/// </summary>
		public FilterContainer Ids(string type, IEnumerable<string> values)
		{
			if (type.IsNullOrEmpty())
				return CreateConditionlessFilterDescriptor("ids", null);

			var filter = new IdsFilter();
			((IIdsFilter)filter).Values = values;
			((IIdsFilter)filter).Type = new [] { type };

			this.SetCacheAndName(filter);
			return this.New(filter, f => f.Ids = filter);
		}
		/// <summary>
		/// Filters documents that only have the provided ids. 
		/// Note, this filter does not require the _id field to be indexed since it works using the _uid field.
		/// </summary>
		public FilterContainer Ids(IEnumerable<string> types, IEnumerable<string> values)
		{
			if (!types.HasAny() || types.All(t=>t.IsNullOrEmpty()))
				return CreateConditionlessFilterDescriptor("ids", null);

			var filter = new IdsFilter();
			((IIdsFilter)filter).Values = values;
			((IIdsFilter)filter).Type = types;
			
			this.SetCacheAndName(filter);
			return  this.New(filter, f => f.Ids = filter);
		}

		/// <summary>
		/// A filter allowing to filter hits based on a point location using a bounding box
		/// </summary>
		public FilterContainer GeoBoundingBox(Expression<Func<T, object>> fieldDescriptor, double topLeftX, double topLeftY, double bottomRightX, double bottomRightY, GeoExecution? type = null)
		{
			var c = CultureInfo.InvariantCulture;
			topLeftX.ThrowIfNull("topLeftX");
			topLeftY.ThrowIfNull("topLeftY");
			bottomRightX.ThrowIfNull("bottomRightX");
			bottomRightY.ThrowIfNull("bottomRightY");
			var geoHashTopLeft = "{0}, {1}".F(topLeftX.ToString(c), topLeftY.ToString(c));
			var geoHashBottomRight = "{0}, {1}".F(bottomRightX.ToString(c), bottomRightY.ToString(c));
			return this.GeoBoundingBox(fieldDescriptor, geoHashTopLeft, geoHashBottomRight, type);
		}
		/// <summary>
		/// A filter allowing to filter hits based on a point location using a bounding box
		/// </summary>
		public FilterContainer GeoBoundingBox(string fieldName, double topLeftX, double topLeftY, double bottomRightX, double bottomRightY, GeoExecution? type = null)
		{
			var c = CultureInfo.InvariantCulture;
			topLeftX.ThrowIfNull("topLeftX");
			topLeftY.ThrowIfNull("topLeftY");
			bottomRightX.ThrowIfNull("bottomRightX");
			bottomRightY.ThrowIfNull("bottomRightY");
			var geoHashTopLeft = "{0}, {1}".F(topLeftX.ToString(c), topLeftY.ToString(c));
			var geoHashBottomRight = "{0}, {1}".F(bottomRightX.ToString(c), bottomRightY.ToString(c));
			return this.GeoBoundingBox(fieldName, geoHashTopLeft, geoHashBottomRight, type);
		}
		/// <summary>
		/// A filter allowing to filter hits based on a point location using a bounding box
		/// </summary>
		public FilterContainer GeoBoundingBox(Expression<Func<T, object>> fieldDescriptor, string geoHashTopLeft, string geoHashBottomRight, GeoExecution? type = null)
		{
			IGeoBoundingBoxFilter filter = new GeoBoundingBoxFilter();
			filter.TopLeft = geoHashTopLeft;
			filter.BottomRight = geoHashBottomRight;
			filter.GeoExecution = type;
			filter.Field = fieldDescriptor;
			return this.New(filter, f => f.GeoBoundingBox = filter);
		}
		/// <summary>
		/// A filter allowing to filter hits based on a point location using a bounding box
		/// </summary>
		public FilterContainer GeoBoundingBox(string fieldName, string geoHashTopLeft, string geoHashBottomRight, GeoExecution? type = null)
		{
			IGeoBoundingBoxFilter filter = new GeoBoundingBoxFilter();
			filter.TopLeft = geoHashTopLeft;
			filter.BottomRight = geoHashBottomRight;
			filter.GeoExecution = type;
			filter.Field = fieldName;
			return this.New(filter, f => f.GeoBoundingBox = filter);
		}

		/// <summary>
		/// Filters documents that include only hits that exists within a specific distance from a geo point. 
		/// </summary>
		public FilterContainer GeoDistance(Expression<Func<T, object>> fieldDescriptor, Action<GeoDistanceFilterDescriptor> filterDescriptor)
		{
			return _GeoDistance(fieldDescriptor, filterDescriptor);
		}

		/// <summary>
		/// Filters documents that include only hits that exists within a specific distance from a geo point. 
		/// </summary>
		public FilterContainer GeoDistance(string field, Action<GeoDistanceFilterDescriptor> filterDescriptor)
		{
			return _GeoDistance(field, filterDescriptor);
		}

		private FilterContainer _GeoDistance(PropertyPathMarker field, Action<GeoDistanceFilterDescriptor> filterDescriptor)
		{
			var filter = new GeoDistanceFilterDescriptor();
			if (filterDescriptor != null)
				filterDescriptor(filter);

			IGeoDistanceFilter ff = filter;
			ff.Field = field;
			return this.New(filter, f => f.GeoDistance = filter);
		}

		/// <summary>
		/// Filters documents that exists within a range from a specific point:
		/// </summary>
		public FilterContainer GeoDistanceRange(Expression<Func<T, object>> fieldDescriptor, Action<GeoDistanceRangeFilterDescriptor> filterDescriptor)
		{
			return _GeoDistanceRange(fieldDescriptor, filterDescriptor);
		}
		/// <summary>
		/// Filters documents that exists within a range from a specific point:
		/// </summary>
		public FilterContainer GeoDistanceRange(string field, Action<GeoDistanceRangeFilterDescriptor> filterDescriptor)
		{
			return _GeoDistanceRange(field, filterDescriptor);
		}

		private FilterContainer _GeoDistanceRange(PropertyPathMarker field, Action<GeoDistanceRangeFilterDescriptor> filterDescriptor)
		{
			var filter = new GeoDistanceRangeFilterDescriptor();
			if (filterDescriptor != null)
				filterDescriptor(filter);

			IGeoDistanceRangeFilter ff = filter;
			ff.Field = field;
			return this.New(ff, f => f.GeoDistanceRange = ff);
		}

		/// <summary>
		/// Filter documents indexed using the geo_shape type.
		/// </summary>
		public FilterContainer GeoShape(Expression<Func<T, object>> fieldDescriptor, Action<GeoShapeFilterDescriptor> filterDescriptor)
		{
			return _GeoShape(fieldDescriptor, filterDescriptor);
		}
		/// <summary>
		/// Filter documents indexed using the geo_shape type.
		/// </summary>
		public FilterContainer GeoShape(string field, Action<GeoShapeFilterDescriptor> filterDescriptor)
		{
			return _GeoShape(field, filterDescriptor);
		}

		private FilterContainer _GeoShape(PropertyPathMarker field, Action<GeoShapeFilterDescriptor> filterDescriptor)
		{
			var filter = new GeoShapeFilterDescriptor();
			if (filterDescriptor != null)
				filterDescriptor(filter);
			((IGeoShapeFilter)filter).Field = field;
			return this.New(filter, f => f.GeoShape = filter);
		}

		/// <summary>
		/// Filter documents indexed using the geo_shape type.
		/// </summary>
		public FilterContainer GeoIndexedShape(Expression<Func<T, object>> fieldDescriptor, Action<GeoIndexedShapeFilterDescriptor> filterDescriptor)
		{
			return this._GeoIndexedShape(fieldDescriptor, filterDescriptor);
		}
		/// <summary>
		/// Filter documents indexed using the geo_shape type.
		/// </summary>
		public FilterContainer GeoIndexedShape(string field, Action<GeoIndexedShapeFilterDescriptor> filterDescriptor)
		{
			return _GeoIndexedShape(field, filterDescriptor);
		}

		private FilterContainer _GeoIndexedShape(PropertyPathMarker field, Action<GeoIndexedShapeFilterDescriptor> filterDescriptor)
		{
			var filter = new GeoIndexedShapeFilterDescriptor();
			if (filterDescriptor != null)
				filterDescriptor(filter);
			((IGeoIndexedShapeFilter)filter).Field = field;
			return this.New(filter, f => f.GeoShape = filter);
		}

		/// <summary>
		/// A filter allowing to include hits that only fall within a polygon of points. 
		/// </summary>
		public FilterContainer GeoPolygon(Expression<Func<T, object>> fieldDescriptor, IEnumerable<Tuple<double, double>> points)
		{
			var c = CultureInfo.InvariantCulture;
			return this._GeoPolygon(fieldDescriptor, points.Select(p => "{0}, {1}".F(p.Item1.ToString(c), p.Item2.ToString(c))).ToArray());
		}
		/// <summary>
		/// A filter allowing to include hits that only fall within a polygon of points. 
		/// </summary>
		public FilterContainer GeoPolygon(string field, IEnumerable<Tuple<double, double>> points)
		{
			var c = CultureInfo.InvariantCulture;
			return this.GeoPolygon(field, points.Select(p => "{0}, {1}".F(p.Item1.ToString(c), p.Item2.ToString(c))).ToArray());
		}
		/// <summary>
		/// A filter allowing to include hits that only fall within a polygon of points. 
		/// </summary>
		public FilterContainer GeoPolygon(Expression<Func<T, object>> fieldDescriptor, params string[] points)
		{
			return this._GeoPolygon(fieldDescriptor, points);
		}
		/// <summary>
		/// A filter allowing to include hits that only fall within a polygon of points. 
		/// </summary>
		public FilterContainer GeoPolygon(string fieldName, params string[] points)
		{
			return _GeoPolygon(fieldName, points);
		}

		private FilterContainer _GeoPolygon(PropertyPathMarker fieldName, string[] points)
		{
			IGeoPolygonFilter filter = new GeoPolygonFilter();
			filter.Points = points;
			filter.Field = fieldName;
			return this.New(filter, f => f.GeoPolygon = filter);
		}

		/// <summary>
		/// The has_child filter accepts a query and the child type to run against, 
		/// and results in parent documents that have child docs matching the query.
		/// </summary>
		/// <typeparam name="K">Type of the child</typeparam>
		public FilterContainer HasChild<K>(Action<HasChildFilterDescriptor<K>> filterSelector) where K : class
		{
			var filter = new HasChildFilterDescriptor<K>();
			if (filterSelector != null)
				filterSelector(filter);

			return this.New(filter, f => f.HasChild = filter);
		}

		/// <summary>
		/// The has_child filter accepts a query and the child type to run against, 
		/// and results in parent documents that have child docs matching the query.
		/// </summary>
		/// <typeparam name="K">Type of the child</typeparam>
		public FilterContainer HasParent<K>(Action<HasParentFilterDescriptor<K>> filterSelector) where K : class
		{
			var filter = new HasParentFilterDescriptor<K>();
			if (filterSelector != null)
				filterSelector(filter);

			return this.New(filter, f => f.HasParent = filter);
		}

		/// <summary>
		/// A limit filter limits the number of documents (per shard) to execute on.
		/// </summary>
		public FilterContainer Limit(int? limit)
		{
			ILimitFilter filter = new LimitFilter {};
			filter.Value = limit;

			return  this.New(filter, f => f.Limit = filter);
		}
		/// <summary>
		/// Filters documents matching the provided document / mapping type. 
		/// Note, this filter can work even when the _type field is not indexed 
		/// (using the _uid field).
		/// </summary>
		public FilterContainer Type(string type)
		{
			ITypeFilter filter = new TypeFilter {};
			filter.Value = type;
			return  this.New(filter, f => f.Type = filter);
		}

		/// <summary>
		/// Filters documents matching the provided document / mapping type. 
		/// Note, this filter can work even when the _type field is not indexed 
		/// (using the _uid field).
		/// </summary>
		public FilterContainer Type(Type type)
		{
			ITypeFilter filter = new TypeFilter {};
			filter.Value = type;
			return this.New(filter, f=> f.Type = filter);
		}

		/// <summary>
		/// A filter that matches on all documents.
		/// </summary>
		public FilterContainer MatchAll()
		{
			var filter = new MatchAllFilter { };
			return this.New(filter, f=> f.MatchAll = filter);
		}
		
		/// <summary>
		/// Filters documents with fields that have terms within a certain range. 
		/// Similar to range query, except that it acts as a filter. 
		/// </summary>
		public FilterContainer Range(Action<RangeFilterDescriptor<T>> rangeSelector)
		{
			var filter = new RangeFilterDescriptor<T>();
			if (rangeSelector != null)
				rangeSelector(filter);
			
			return this.New(filter, f=>f.Range = filter);
		}
		/// <summary>
		/// A filter allowing to define scripts as filters. 
		/// </summary>
		public FilterContainer Script(Action<ScriptFilterDescriptor> scriptSelector)
		{
			var descriptor = new ScriptFilterDescriptor();
			if (scriptSelector != null)
				scriptSelector(descriptor);

			return this.New(descriptor, f=>f.Script = descriptor);
		}
		/// <summary>
		/// Filters documents that have fields containing terms with a specified prefix 
		/// (not analyzed). Similar to phrase query, except that it acts as a filter. 
		/// </summary>
		public FilterContainer Prefix(Expression<Func<T, object>> fieldDescriptor, string prefix)
		{
			IPrefixFilter filter = new PrefixFilter();
			filter.Field = fieldDescriptor;
			filter.Prefix = prefix;
			return this.New(filter, f=>f.Prefix = filter);
		}
		/// <summary>
		/// Filters documents that have fields containing terms with a specified prefix 
		/// (not analyzed). Similar to phrase query, except that it acts as a filter. 
		/// </summary>
		public FilterContainer Prefix(string field, string prefix)
		{
			IPrefixFilter filter = new PrefixFilter();
			filter.Field = field;
			filter.Prefix = prefix;
			return this.New(filter, f=>f.Prefix = filter);
		}
		/// <summary>
		/// Filters documents that have fields that contain a term (not analyzed). 
		/// Similar to term query, except that it acts as a filter
		/// </summary>
		public FilterContainer Term<K>(Expression<Func<T, K>> fieldDescriptor, K term)
		{
			ITermFilter filter = new TermFilter();
			filter.Field = fieldDescriptor;
			filter.Value = term;
			return this.New(filter, f=>f.Term = filter);
		}
		/// <summary>
		/// Filters documents that have fields that contain a term (not analyzed). 
		/// Similar to term query, except that it acts as a filter
		/// </summary>
		public FilterContainer Term(Expression<Func<T, object>> fieldDescriptor, object term)
		{
			ITermFilter filter = new TermFilter();
			filter.Field = fieldDescriptor;
			filter.Value = term;
			return this.New(filter, f=>f.Term = filter);
		}
	
		/// <summary>
		/// Filters documents that have fields that contain a term (not analyzed).
		/// Similar to term query, except that it acts as a filter
		/// </summary>
		public FilterContainer Term(string field, object term)
		{


			ITermFilter filter = new TermFilter();
			filter.Field = field;
			filter.Value = term;
			return this.New(filter, f=>f.Term = filter);

		}
		/// <summary>
		/// Filters documents that have fields that match any of the provided terms (not analyzed). 
		/// </summary>
		public FilterContainer Terms<K>(Expression<Func<T, K>> fieldDescriptor, IEnumerable<K> terms, TermsExecution? Execution = null)
		{
			ITermsFilter filter = new TermsFilter();
			filter.Field = fieldDescriptor;
			filter.Terms = terms.Cast<object>();
			filter.Execution = Execution;
			return this.New(filter, f=>f.Terms = filter);
		}	
		
		/// <summary>
		/// Filters documents that have fields that match any of the provided terms (not analyzed). 
		/// </summary>
		public FilterContainer Terms(Expression<Func<T, object>> fieldDescriptor, IEnumerable<string> terms, TermsExecution? Execution = null)
		{
			ITermsFilter filter = new TermsFilter();
			filter.Field = fieldDescriptor;
			filter.Terms = terms;
			return this.New(filter, f=>f.Terms = filter);
		}

		/// <summary>
		/// Filters documents that have fields that match any of the provided terms (not analyzed). 
		/// </summary>
		public FilterContainer Terms(string field, IEnumerable<string> terms, TermsExecution? Execution = null)
		{
			ITermsFilter filter = new TermsFilter();
			filter.Field = field;
			filter.Terms = terms ?? Enumerable.Empty<string>();
			return this.New(filter, f=>f.Terms = filter);
		}

		/// <summary>
		/// Filter documents indexed using the geo_shape type.
		/// </summary>
		public FilterContainer TermsLookup(Expression<Func<T, object>> fieldDescriptor, Action<TermsLookupFilterDescriptor> filterDescriptor)
		{
			var filter = new TermsLookupFilterDescriptor();
			if (filterDescriptor != null)
				filterDescriptor(filter);

			((ITermsBaseFilter)filter).Field = fieldDescriptor;
			return this.New(filter, f=>f.Terms = filter);
		}
		/// <summary>
		/// Filter documents indexed using the geo_shape type.
		/// </summary>
		public FilterContainer TermsLookup(string field, Action<TermsLookupFilterDescriptor> filterDescriptor)
		{
			var filter = new TermsLookupFilterDescriptor();
			if (filterDescriptor != null)
				filterDescriptor(filter);

			((ITermsBaseFilter)filter).Field = field;
			return this.New(filter, f=>f.Terms = filter);
		}

		/// <summary>
		/// A filter that matches documents using AND boolean operator on other queries. 
		/// This filter is more performant then bool filter. 
		/// </summary>
		public FilterContainer And(params Func<FilterDescriptor<T>, FilterContainer>[] selectors)
		{
			return this.And((from selector in selectors 
							 let filter = new FilterDescriptor<T>() { IsConditionless = true}
							 select selector(filter)).ToArray());
		}
		/// <summary>
		/// A filter that matches documents using AND boolean operator on other queries. 
		/// This filter is more performant then bool filter. 
		/// </summary>
		public FilterContainer And(params FilterContainer[] filtersDescriptor)
		{
			var andFilter = new AndFilter();
			((IAndFilter)andFilter).Filters = filtersDescriptor.Cast<IFilterContainer>().ToList();
			return this.New(andFilter, f=>f.And = andFilter);
		}
		/// <summary>
		/// A filter that matches documents using OR boolean operator on other queries. 
		/// This filter is more performant then bool filter
		/// </summary>
		public FilterContainer Or(params Func<FilterDescriptor<T>, FilterContainer>[] selectors)
		{
			var descriptors = (from selector in selectors 
							   let filter = new FilterDescriptor<T>() { IsConditionless = true}
							   select selector(filter)
							  ).ToArray();
			return this.Or(descriptors);

		}
		/// <summary>
		/// A filter that matches documents using OR boolean operator on other queries. 
		/// This filter is more performant then bool filter
		/// </summary>
		public FilterContainer Or(params FilterContainer[] filtersDescriptor)
		{
			var orFilter = new OrFilter();
			((IOrFilter)orFilter).Filters = filtersDescriptor.Cast<IFilterContainer>().ToList();
			return this.New(orFilter, f=> f.Or = orFilter);
			
		}
		/// <summary>
		/// A filter that filters out matched documents using a query. 
		/// This filter is more performant then bool filter. 
		/// </summary>
		public FilterContainer Not(Func<FilterDescriptor<T>, FilterContainer> selector)
		{

			var notFilter = new NotFilter();

			var filter = new FilterDescriptor<T>() { IsConditionless = true };
			FilterContainer bf = filter;
			if (selector != null)
				bf = selector(filter);

			((INotFilter)notFilter).Filter = bf;
			return this.New(notFilter, f=>f.Not = notFilter);

		}
		/// <summary>
		/// 
		/// A filter that matches documents matching boolean combinations of other queries.
		/// Similar in concept to Boolean query, except that the clauses are other filters. 
		/// </summary>
		public FilterContainer Bool(Action<BoolFilterDescriptor<T>> booleanFilter)
		{
			var filter = new BoolFilterDescriptor<T>();
			if (booleanFilter != null)
				booleanFilter(filter);

			return this.New(filter, f=>f.Bool = filter);

		}
		/// <summary>
		/// Wraps any query to be used as a filter. 
		/// </summary>
		public FilterContainer Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			var filter = new QueryFilter();
			var descriptor = new QueryDescriptor<T>();
			QueryContainer bq = descriptor;
			if (querySelector != null)
				bq = querySelector(descriptor);

			((IQueryFilter)filter).Query = bq;
			return this.New(filter, f=>f.Query = filter);
		}


		/// <summary>
		///  A nested filter, works in a similar fashion to the nested query, except used as a filter.
		///  It follows exactly the same structure, but also allows to cache the results 
		///  (set _cache to true), and have it named (set the _name value). 
		/// </summary>
		/// <param name="selector"></param>
		public FilterContainer Nested(Action<NestedFilterDescriptor<T>> selector)
		{
			var filter = new NestedFilterDescriptor<T>();
			if (selector != null)
				selector(filter);

			return this.New(filter, f=>f.Nested = filter);
		}

		/// <summary>
		///  The regexp filter allows you to use regular expression term queries. 
		/// </summary>
		/// <param name="selector"></param>
		public FilterContainer Regexp(Action<RegexpFilterDescriptor<T>> selector)
		{
			var filter = new RegexpFilterDescriptor<T>();
			if (selector != null)
				selector(filter);

			return this.New(filter, f=>f.Regexp = filter);
		}

		private FilterDescriptor<T> CreateConditionlessFilterDescriptor(object filter, string type = null)
		{
			var self = ((IFilterContainer)this);
			if (self.IsStrict && !self.IsVerbatim)
				throw new DslException("Filter resulted in a conditionless '{1}' filter (json by approx):\n{0}"
					.F(
						JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
						, type ?? filter.GetType().Name.Replace("Descriptor", "").Replace("`1", "")
					)
				);
			var f = new FilterDescriptor<T>();
			((IFilterContainer)f).IsStrict = self.IsStrict;
			((IFilterContainer)f).IsVerbatim = self.IsVerbatim;
			f.IsConditionless = true;
			return f;
		}

		private FilterDescriptor<T> New(IFilter filter, Action<IFilterContainer> fillProperty)
		{
			var self = ((IFilterContainer)this);
			if (filter.IsConditionless && !self.IsVerbatim)
			{
				this.ResetCache();
				return CreateConditionlessFilterDescriptor(filter);
			}

			this.SetCacheAndName(filter);
			var f = new FilterDescriptor<T>();
			((IFilterContainer)f).IsStrict = self.IsStrict;
			((IFilterContainer)f).IsVerbatim = self.IsVerbatim;

			if (fillProperty != null)
				fillProperty(f);

			this.ResetCache();
			return f;
		}

		private void ResetCache()
		{
			this._Cache = null;
			this._CacheKey = null;
			this._Name = null;
		}

		private void SetCacheAndName(IFilter filter)
		{
			if (this._Cache.HasValue)
				filter.Cache = this._Cache;
			if (!string.IsNullOrWhiteSpace(this._Name))
				filter.CacheName = this._Name;
			if (!string.IsNullOrWhiteSpace(this._CacheKey))
				filter.CacheKey = this._CacheKey;
		}


	}
}
