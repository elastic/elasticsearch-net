using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using System.Globalization;

namespace Nest
{
	public class FilterDescriptor<T> where T : class
	{
		internal string _Name { get; set; }
		internal bool? _Cache { get; set; }

		public FilterDescriptor<T> Name(string name)
		{
			name.ThrowIfNull("name");
			this._Name = name;
			return this;
		}
		public FilterDescriptor<T> Cache(bool cache)
		{
			cache.ThrowIfNull("cache");
			this._Cache = cache;
			return this;
		}

		[JsonProperty(PropertyName = "exists")]
		internal ExistsFilter ExistsFilter { get; set; }

		[JsonProperty(PropertyName = "missing")]
		internal MissingFilter MissingFilter { get; set; }

		[JsonProperty(PropertyName = "ids")]
		internal IdsFilter IdsFilter { get; set; }

		[JsonProperty(PropertyName = "geo_bounding_box")]
		internal Dictionary<string, object> GeoBoundingBoxFilter { get; set; }

		[JsonProperty(PropertyName = "geo_distance")]
		internal Dictionary<string, object> GeoDistanceFilter { get; set; }

		[JsonProperty(PropertyName = "geo_distance_range")]
		internal Dictionary<string, object> GeoDistanceRangeFilter { get; set; }

		[JsonProperty(PropertyName = "geo_polygon")]
		internal Dictionary<string, object> GeoPolygonFilter { get; set; }

		[JsonProperty(PropertyName = "limit")]
		internal LimitFilter LimitFilter { get; set; }

		[JsonProperty(PropertyName = "type")]
		internal TypeFilter TypeFilter { get; set; }

		[JsonProperty(PropertyName = "match_all")]
		internal MatchAllFilter MatchAllFilter { get; set; }

		[JsonProperty(PropertyName = "has_child")]
		internal object HasChildFilter { get; set; }

		[JsonProperty(PropertyName = "numeric_range")]
		internal Dictionary<string, object> NumericRangeFilter { get; set; }

		[JsonProperty(PropertyName = "range")]
		internal Dictionary<string, object> RangeFilter { get; set; }

		[JsonProperty(PropertyName = "prefix")]
		internal Dictionary<string, object> PrefixFilter { get; set; }

		[JsonProperty(PropertyName = "term")]
		internal Dictionary<string, object> TermFilter { get; set; }

		[JsonProperty(PropertyName = "terms")]
		internal Dictionary<string, object> TermsFilter { get; set; }

		[JsonProperty(PropertyName = "fquery")]
		internal Dictionary<string, object> QueryFilter { get; set; }

		[JsonProperty(PropertyName = "and")]
		internal Dictionary<string, object> AndFilter { get; set; }

		[JsonProperty(PropertyName = "or")]
		internal Dictionary<string, object> OrFilter { get; set; }

		[JsonProperty(PropertyName = "not")]
		internal Dictionary<string, object> NotFilter { get; set; }

		[JsonProperty(PropertyName = "bool")]
		internal BoolFilterDescriptor<T> BoolFilter { get; set; }

		[JsonProperty(PropertyName = "script")]
		internal ScriptFilterDescriptor ScriptFilter { get; set; }

		[JsonProperty(PropertyName = "nested")]
		internal NestedFilterDescriptor<T> NestedFilter { get; set; }

		public FilterDescriptor()
		{
			
		}
		private void SetCacheAndName(FilterBase filter)
		{
			if (this._Cache.HasValue)
				filter._Cache = this._Cache;
			if (!string.IsNullOrWhiteSpace(this._Name))
				filter._Name = this._Name;
		}

		private void SetDictionary(string fieldName, FilterBase filter, Action<Dictionary<string, object>> setter)
		{
			setter.ThrowIfNull("setter");
			var dictionary = new Dictionary<string, object>();
			dictionary.Add(fieldName, filter);
			if (this._Cache.HasValue)
				dictionary.Add("_cache", this._Cache);
			if (!string.IsNullOrWhiteSpace(this._Name))
				dictionary.Add("_name", this._Name);
			setter(dictionary);
		}
		private void SetDictionary(string key, object value, Action<Dictionary<string, object>> setter)
		{
			setter.ThrowIfNull("setter");
			var dictionary = new Dictionary<string, object>();
			dictionary.Add(key, value);
			if (this._Cache.HasValue)
				dictionary.Add("_cache", this._Cache);
			if (!string.IsNullOrWhiteSpace(this._Name))
				dictionary.Add("_name", this._Name);
			setter(dictionary);
		}

		/// <summary>
		/// Filters documents where a specific field has a value in them.
		/// </summary>
		public void Exists(Expression<Func<T, object>> fieldDescriptor)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.Exists(field);
		}
		/// <summary>
		/// Filters documents where a specific field has a value in them.
		/// </summary>
		public void Exists(string field)
		{
			this.ExistsFilter = new ExistsFilter { Field = field };
			this.SetCacheAndName(this.ExistsFilter);
		}
		/// <summary>
		/// Filters documents where a specific field has no value in them.
		/// </summary>
		public void Missing(Expression<Func<T, object>> fieldDescriptor)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.Missing(field);
		}
		/// <summary>
		/// Filters documents where a specific field has no value in them.
		/// </summary>
		public void Missing(string field)
		{
			this.MissingFilter = new MissingFilter { Field = field };
			this.SetCacheAndName(this.MissingFilter);
		}
		/// <summary>
		/// Filters documents that only have the provided ids. 
		/// Note, this filter does not require the _id field to be indexed since it works using the _uid field.
		/// </summary>
		public void Ids(IEnumerable<string> values)
		{
			this.IdsFilter = new IdsFilter { Values = values };
			this.SetCacheAndName(this.IdsFilter);
		}
		/// <summary>
		/// Filters documents that only have the provided ids. 
		/// Note, this filter does not require the _id field to be indexed since it works using the _uid field.
		/// </summary>
		public void Ids(string type, IEnumerable<string> values)
		{
			type.ThrowIfNullOrEmpty("type");
			this.IdsFilter = new IdsFilter { Values = values, Type = new []{ type } };
			this.SetCacheAndName(this.IdsFilter);
		}
		/// <summary>
		/// Filters documents that only have the provided ids. 
		/// Note, this filter does not require the _id field to be indexed since it works using the _uid field.
		/// </summary>
		public void Ids(IEnumerable<string> types, IEnumerable<string> values)
		{
			this.IdsFilter = new IdsFilter { Values = values, Type = types };
			this.SetCacheAndName(this.IdsFilter);
		}

		/// <summary>
		/// A filter allowing to filter hits based on a point location using a bounding box
		/// </summary>
		public void GeoBoundingBox(Expression<Func<T, object>> fieldDescriptor, string geoHashTopLeft, string geoHashBottomRight, GeoExecution? Type = null)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.GeoBoundingBox(field, geoHashTopLeft, geoHashBottomRight, Type);
		}
		/// <summary>
		/// A filter allowing to filter hits based on a point location using a bounding box
		/// </summary>
		public void GeoBoundingBox(Expression<Func<T, object>> fieldDescriptor, double topLeftX, double topLeftY, double bottomRightX, double bottomRightY, GeoExecution? Type = null)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.GeoBoundingBox(field, topLeftX, topLeftY, bottomRightX, bottomRightY, Type);
		}
		/// <summary>
		/// A filter allowing to filter hits based on a point location using a bounding box
		/// </summary>
		public void GeoBoundingBox(string fieldName, double topLeftX, double topLeftY, double bottomRightX, double bottomRightY, GeoExecution? Type = null)
		{
      var c = CultureInfo.InvariantCulture;
			topLeftX.ThrowIfNull("topLeftX");
			topLeftY.ThrowIfNull("topLeftY");
			bottomRightX.ThrowIfNull("bottomRightX");
			bottomRightY.ThrowIfNull("bottomRightY");
      this.GeoBoundingBox(fieldName, 
        "{0}, {1}".F(topLeftX.ToString(c), topLeftY.ToString(c)),
        "{0}, {1}".F(bottomRightX.ToString(c), bottomRightY.ToString(c)), Type);
		}
		/// <summary>
		/// A filter allowing to filter hits based on a point location using a bounding box
		/// </summary>
		public void GeoBoundingBox(string fieldName, string geoHashTopLeft, string geoHashBottomRight, GeoExecution? Type = null)
		{
			geoHashTopLeft.ThrowIfNullOrEmpty("geoHashTopLeft");
			geoHashBottomRight.ThrowIfNullOrEmpty("geoHashBottomRight");
			var filter = new GeoBoundingBoxFilter { TopLeft =  geoHashTopLeft, BottomRight = geoHashBottomRight };
			this.SetDictionary(fieldName, filter, (d) => { 
				if (Type.HasValue)
					d.Add("type", Enum.GetName(typeof(GeoExecution), Type.Value));
				this.GeoBoundingBoxFilter = d;
			});
		}
		/// <summary>
		/// Filters documents that include only hits that exists within a specific distance from a geo point. 
		/// </summary>
		public void GeoDistance(Expression<Func<T, object>> fieldDescriptor, Action<GeoDistanceFilterDescriptor> filterDescriptor)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.GeoDistance(field, filterDescriptor);
		}
		/// <summary>
		/// Filters documents that include only hits that exists within a specific distance from a geo point. 
		/// </summary>
		public void GeoDistance(string field, Action<GeoDistanceFilterDescriptor> filterDescriptor)
		{
			var filter = new GeoDistanceFilterDescriptor();
			filterDescriptor(filter);
			this.SetDictionary(field, filter._Location, d => {

				var dd = new Dictionary<string, object>();
				dd.Add("distance", filter._Distance);

				if (!string.IsNullOrWhiteSpace(filter._GeoUnit))
					dd.Add("distance_type", filter._GeoUnit);

				if (!string.IsNullOrWhiteSpace(filter._GeoOptimizeBBox))
					dd.Add("optimize_bbox", filter._GeoOptimizeBBox);

				d.ForEachWithIndex((kv, i) => dd.Add(kv.Key, kv.Value));

				this.GeoDistanceFilter = dd;			
			});
		}
		/// <summary>
		/// Filters documents that exists within a range from a specific point:
		/// </summary>
		public void GeoDistanceRange(Expression<Func<T, object>> fieldDescriptor, Action<GeoDistanceRangeFilterDescriptor> filterDescriptor)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.GeoDistanceRange(field, filterDescriptor);
		}
		/// <summary>
		/// Filters documents that exists within a range from a specific point:
		/// </summary>
		public void GeoDistanceRange(string field, Action<GeoDistanceRangeFilterDescriptor> filterDescriptor)
		{
			var filter = new GeoDistanceRangeFilterDescriptor();
			filterDescriptor(filter);
			if (filter._FromDistance == null)
				throw new ArgumentNullException("FromDistance", 
					"Distance should be set when using the geo distance range DSL");

			if (filter._ToDistance == null)
				throw new ArgumentNullException("ToDistance",
					"Distance should be set when using the geo distance range DSL");

			this.SetDictionary(field, filter._Location, d =>
			{

				var dd = new Dictionary<string, object>();
				dd.Add("from", filter._FromDistance);
				dd.Add("to", filter._ToDistance);
				if (!string.IsNullOrWhiteSpace(filter._GeoUnit))
					dd.Add("distance_type", filter._GeoUnit);

				if (!string.IsNullOrWhiteSpace(filter._GeoOptimizeBBox))
					dd.Add("optimize_bbox", filter._GeoOptimizeBBox);

				d.ForEachWithIndex((kv, i) => dd.Add(kv.Key, kv.Value));

				this.GeoDistanceRangeFilter = dd;
			});
		}
		/// <summary>
		/// A filter allowing to include hits that only fall within a polygon of points. 
		/// </summary>
		public void GeoPolygon(Expression<Func<T, object>> fieldDescriptor, IEnumerable<Tuple<double, double>> points)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.GeoPolygon(field, points);
		}
		/// <summary>
		/// A filter allowing to include hits that only fall within a polygon of points. 
		/// </summary>
		public void GeoPolygon(string field, IEnumerable<Tuple<double, double>> points)
		{
      var c = CultureInfo.InvariantCulture;
			this.GeoPolygon(field, points.Select(p=> "{0}, {1}".F(p.Item1.ToString(c), p.Item2.ToString(c))).ToArray());
		}
		/// <summary>
		/// A filter allowing to include hits that only fall within a polygon of points. 
		/// </summary>
		public void GeoPolygon(Expression<Func<T, object>> fieldDescriptor, params string[] points)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.GeoPolygon(field, points);
		}
		/// <summary>
		/// A filter allowing to include hits that only fall within a polygon of points. 
		/// </summary>
		public void GeoPolygon(string fieldName, params string[] points)
		{
			var filter = new GeoPolygonFilter { Points = points };
			this.SetDictionary(fieldName, filter, d => this.GeoPolygonFilter = d);
		}
		/// <summary>
		/// The has_child filter accepts a query and the child type to run against, 
		/// and results in parent documents that have child docs matching the query.
		/// </summary>
		/// <typeparam name="K">Type of the child</typeparam>
		public void HasChild<K>(Action<HasChildFilterDescriptor<K>> querySelector) where K : class
		{
			var descriptor = new HasChildFilterDescriptor<K>();
			querySelector(descriptor);
			this.HasChildFilter = descriptor;
		}
		/// <summary>
		/// A limit filter limits the number of documents (per shard) to execute on.
		/// </summary>
		public void Limit(int limit)
		{
			this.LimitFilter = new LimitFilter { Value = limit };
			this.SetCacheAndName(this.LimitFilter);
		}
		/// <summary>
		/// Filters documents matching the provided document / mapping type. 
		/// Note, this filter can work even when the _type field is not indexed 
		/// (using the _uid field).
		/// </summary>
		public void Type(string type)
		{
			type.ThrowIfNullOrEmpty("type");
			this.TypeFilter = new TypeFilter { Value = type };
			this.SetCacheAndName(this.TypeFilter);
		}
		/// <summary>
		/// A filter that matches on all documents.
		/// </summary>
		public void MatchAll()
		{
			this.MatchAllFilter = new MatchAllFilter { };
			this.SetCacheAndName(this.MatchAllFilter);
		}
		/// <summary>
		/// Filters documents with fields that have values within a certain numeric range. 
		/// Similar to range filter, except that it works only with numeric values, 
		/// and the filter execution works differently.
		/// </summary>
		public void NumericRange(Action<NumericRangeFilterDescriptor<T>> numericRangeSelector)
		{
			var filter = new NumericRangeFilterDescriptor<T>();
			numericRangeSelector(filter);
			this.SetDictionary(filter._Field, filter, (d) => this.NumericRangeFilter = d);
		}
		/// <summary>
		/// Filters documents with fields that have terms within a certain range. 
		/// Similar to range query, except that it acts as a filter. 
		/// </summary>
		public void Range(Action<RangeFilterDescriptor<T>> rangeSelector)
		{
			var filter = new RangeFilterDescriptor<T>();
			rangeSelector(filter);
			this.SetDictionary(filter._Field, filter, (d) => this.RangeFilter = d);			
		}
		/// <summary>
		/// A filter allowing to define scripts as filters. 
		/// </summary>
		public void Script(Action<ScriptFilterDescriptor> scriptSelector)
		{
			var descriptor = new ScriptFilterDescriptor();
			scriptSelector(descriptor);
			this.SetCacheAndName(descriptor);
			this.ScriptFilter = descriptor;
		}
		/// <summary>
		/// Filters documents that have fields containing terms with a specified prefix 
		/// (not analyzed). Similar to phrase query, except that it acts as a filter. 
		/// </summary>
		public void Prefix(Expression<Func<T, object>> fieldDescriptor, string prefix)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.Prefix(field, prefix);
		}
		/// <summary>
		/// Filters documents that have fields containing terms with a specified prefix 
		/// (not analyzed). Similar to phrase query, except that it acts as a filter. 
		/// </summary>
		public void Prefix(string field, string prefix)
		{
			this.SetDictionary(field, prefix, (d) => this.PrefixFilter = d);
		}
		/// <summary>
		/// Filters documents that have fields that contain a term (not analyzed). 
		/// Similar to term query, except that it acts as a filter
		/// </summary>
		public void Term(Expression<Func<T, object>> fieldDescriptor, string term)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.Term(field, term);
		}
		/// <summary>
		/// Filters documents that have fields that contain a term (not analyzed).
		/// Similar to term query, except that it acts as a filter
		/// </summary>
		public void Term(string field, string term)
		{
			this.SetDictionary(field, term, (d) => this.TermFilter = d);
		}
		/// <summary>
		/// Filters documents that have fields that match any of the provided terms (not analyzed). 
		/// </summary>
		public void Terms(Expression<Func<T, object>> fieldDescriptor, IEnumerable<string> terms, TermsExecution? Execution = null)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.Terms(field, terms, Execution);
		}
		/// <summary>
		/// Filters documents that have fields that match any of the provided terms (not analyzed). 
		/// </summary>
		public void Terms(string field, IEnumerable<string> terms, TermsExecution? Execution = null)
		{
			this.SetDictionary(field, terms, (d) => {
				if (Execution.HasValue)
					d.Add("execution", Enum.GetName(typeof(TermsExecution), Execution));
				this.TermsFilter = d;
			});
		}
		/// <summary>
		/// A filter that matches documents using AND boolean operator on other queries. 
		/// This filter is more performant then bool filter. 
		/// </summary>
		public void And(params Action<FilterDescriptor<T>>[] filters)
		{
			var descriptors = new List<FilterDescriptor<T>>();
			foreach (var selector in filters)
			{
				var filter = new FilterDescriptor<T>();
				selector(filter);
				descriptors.Add(filter);
			}
			this.SetDictionary("filters", descriptors, (d) => this.AndFilter = d);
		}
		/// <summary>
		/// A filter that matches documents using OR boolean operator on other queries. 
		/// This filter is more performant then bool filter
		/// </summary>
		public void Or(params Action<FilterDescriptor<T>>[] filters)
		{
			var descriptors = new List<FilterDescriptor<T>>();
			foreach (var selector in filters)
			{
				var filter = new FilterDescriptor<T>();
				selector(filter);
				descriptors.Add(filter);
			}
			this.SetDictionary("filters", descriptors, (d) => this.OrFilter = d);
		}
		/// <summary>
		/// A filter that filters out matched documents using a query. 
		/// This filter is more performant then bool filter. 
		/// </summary>
		public void Not(Action<FilterDescriptor<T>> selector)
		{
			var filter = new FilterDescriptor<T>();
			selector(filter);
			this.SetDictionary("filter", filter, (d) => this.NotFilter = d);
		}
		/// <summary>
		/// 
		/// A filter that matches documents matching boolean combinations of other queries.
		/// Similar in concept to Boolean query, except that the clauses are other filters. 
		/// </summary>
		public void Bool(Action<BoolFilterDescriptor<T>> booleanFilter)
		{
			var filter = new BoolFilterDescriptor<T>();
			booleanFilter(filter);
			this.SetCacheAndName(filter);
			this.BoolFilter = filter;

		}
		/// <summary>
		/// Wraps any query to be used as a filter. 
		/// </summary>
		public void Query(Action<QueryDescriptor<T>> querySelector)
		{
			var descriptor = new QueryDescriptor<T>();
			querySelector(descriptor);
			this.SetDictionary("query", descriptor, (d) => this.QueryFilter = d);
		}
		/// <summary>
		///  A nested filter, works in a similar fashion to the nested query, except used as a filter.
		///  It follows exactly the same structure, but also allows to cache the results 
		///  (set _cache to true), and have it named (set the _name value). 
		/// </summary>
		/// <param name="selector"></param>
		public void Nested(Action<NestedFilterDescriptor<T>> selector)
		{
			var descriptor = new NestedFilterDescriptor<T>();
			selector(descriptor);
			this.NestedFilter = descriptor;
			this.SetCacheAndName(this.NestedFilter);
		}

	}
}
