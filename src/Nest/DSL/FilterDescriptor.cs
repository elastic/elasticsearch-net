using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using System.Globalization;
using Elasticsearch.Net;
using Nest.Resolvers;
using System.Collections;

namespace Nest
{
	/* Adding a new filter:
	 *   - make sure it calls in to New() or SetDictionary() for immutable sake
	 *   - add a null check to IsConditionless
	 */
	public class FilterDescriptor<T> : BaseFilter, IFilterDescriptor<T> where T : class
	{
		internal string _Name { get; set; }
		internal string _CacheKey { get; set; }
		internal bool? _Cache { get; set; }

		

		private bool _forcedConditionless;
		public override bool IsConditionless
		{
			get
			{
				if (_forcedConditionless)
					return true;
				return this.ExistsFilter == null
					&& this.MissingFilter == null
					&& this.IdsFilter == null
					&& this.BoolFilterDescriptor == null
					&& this.GeoBoundingBoxFilter == null
					&& this.GeoDistanceFilter == null
					&& this.GeoDistanceRangeFilter == null
					&& this.GeoPolygonFilter == null
					&& this.GeoShapeFilter == null
					&& this.LimitFilter == null
					&& this.TypeFilter == null
					&& this.MatchAllFilter == null
					&& this.HasChildFilter == null
					&& this.HasParentFilter == null
					&& this.NumericRangeFilter == null
					&& this.RangeFilter == null
					&& this.PrefixFilter == null
					&& this.TermFilter == null
					&& this.TermsFilter == null
					&& this.QueryFilter == null
					&& this.AndFilter == null
					&& this.OrFilter == null
					&& this.NotFilter == null
					&& this.ScriptFilter == null
					&& this.NestedFilter == null
					&& this.RegexpFilter == null
					;
			}
			internal set
			{
				_forcedConditionless = value;
			}
		}


		[JsonProperty(PropertyName = "exists")]
		internal ExistsFilter ExistsFilter { get; set; }

		[JsonProperty(PropertyName = "missing")]
		internal MissingFilter MissingFilter { get; set; }

		[JsonProperty(PropertyName = "ids")]
		internal IdsFilter IdsFilter { get; set; }

		[JsonProperty(PropertyName = "geo_bounding_box")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<PropertyPathMarker, object> GeoBoundingBoxFilter { get; set; }

		[JsonProperty(PropertyName = "geo_distance")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<PropertyPathMarker, object> GeoDistanceFilter { get; set; }

		[JsonProperty(PropertyName = "geo_distance_range")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<PropertyPathMarker, object> GeoDistanceRangeFilter { get; set; }

		[JsonProperty(PropertyName = "geo_polygon")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<PropertyPathMarker, object> GeoPolygonFilter { get; set; }

		[JsonProperty(PropertyName = "geo_shape")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<PropertyPathMarker, object> GeoShapeFilter { get; set; }

		[JsonProperty(PropertyName = "limit")]
		internal LimitFilter LimitFilter { get; set; }

		[JsonProperty(PropertyName = "type")]
		internal TypeFilter TypeFilter { get; set; }

		[JsonProperty(PropertyName = "match_all")]
		internal MatchAllFilter MatchAllFilter { get; set; }

		[JsonProperty(PropertyName = "has_child")]
		internal object HasChildFilter { get; set; }

		[JsonProperty(PropertyName = "has_parent")]
		internal object HasParentFilter { get; set; }

		[JsonProperty(PropertyName = "numeric_range")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<PropertyPathMarker, object> NumericRangeFilter { get; set; }

		[JsonProperty(PropertyName = "range")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<PropertyPathMarker, object> RangeFilter { get; set; }

		[JsonProperty(PropertyName = "prefix")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<PropertyPathMarker, object> PrefixFilter { get; set; }

		[JsonProperty(PropertyName = "term")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<PropertyPathMarker, object> TermFilter { get; set; }

		[JsonProperty(PropertyName = "terms")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<PropertyPathMarker, object> TermsFilter { get; set; }

		[JsonProperty(PropertyName = "fquery")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<PropertyPathMarker, object> QueryFilter { get; set; }

		[JsonProperty(PropertyName = "and")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<PropertyPathMarker, object> AndFilter { get; set; }

		[JsonProperty(PropertyName = "or")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<PropertyPathMarker, object> OrFilter { get; set; }

		[JsonProperty(PropertyName = "not")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<PropertyPathMarker, object> NotFilter { get; set; }

		[JsonProperty(PropertyName = "script")]
		internal ScriptFilterDescriptor ScriptFilter { get; set; }

		[JsonProperty(PropertyName = "nested")]
		internal NestedFilterDescriptor<T> NestedFilter { get; set; }

		[JsonProperty(PropertyName = "regexp")]
		internal Dictionary<PropertyPathMarker, object> RegexpFilter { get; set; }

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
			return new FilterDescriptor<T> { IsStrict = strict };
		}

		public FilterDescriptor<T> Verbatim(bool verbatim = true)
		{
			return new FilterDescriptor<T> { IsVerbatim = verbatim, IsStrict = verbatim };
		}

		
		/// <summary>
		/// A thin wrapper allowing fined grained control what should happen if a filter is conditionless
		/// if you need to fallback to something other than a match_all query
		/// </summary>
		public BaseFilter Conditionless(Action<ConditionlessFilterDescriptor<T>> selector)
		{
			var filter = new ConditionlessFilterDescriptor<T>();
			selector(filter);

			return (filter._Filter == null || filter._Filter.IsConditionless) ? filter._Fallback : filter._Filter;
		}

		/// <summary>
		/// Filters documents where a specific field has a value in them.
		/// </summary>
		public BaseFilter Exists(Expression<Func<T, object>> fieldDescriptor)
		{
			var filter = new ExistsFilter { Field = fieldDescriptor };
			this.SetCacheAndName(filter);
			return this.New(filter, f => f.ExistsFilter = filter);
		}
		/// <summary>
		/// Filters documents where a specific field has a value in them.
		/// </summary>
		public BaseFilter Exists(string field)
		{
			var filter = new ExistsFilter { Field = field };
			this.SetCacheAndName(filter);
			return this.New(filter, f => f.ExistsFilter = filter);
		}
		/// <summary>
		/// Filters documents where a specific field has no value in them.
		/// </summary>
		public BaseFilter Missing(Expression<Func<T, object>> fieldDescriptor)
		{
			var filter = new MissingFilter { Field = fieldDescriptor };
			this.SetCacheAndName(filter);
			return  this.New(filter, f => f.MissingFilter = filter);
		}
		/// <summary>
		/// Filters documents where a specific field has no value in them.
		/// </summary>
		public BaseFilter Missing(string field)
		{
			var filter = new MissingFilter { Field = field };
			this.SetCacheAndName(filter);
			return  this.New(filter, f => f.MissingFilter = filter);
		}
		/// <summary>
		/// Filters documents that only have the provided ids. 
		/// Note, this filter does not require the _id field to be indexed since it works using the _uid field.
		/// </summary>
		public BaseFilter Ids(IEnumerable<string> values)
		{
			var filter = new IdsFilter { Values = values };
			this.SetCacheAndName(filter);
			return this.New(filter, f => f.IdsFilter = filter);
		}
		/// <summary>
		/// Filters documents that only have the provided ids. 
		/// Note, this filter does not require the _id field to be indexed since it works using the _uid field.
		/// </summary>
		public BaseFilter Ids(string type, IEnumerable<string> values)
		{
			if (type.IsNullOrEmpty())
				return CreateConditionlessFilterDescriptor("ids", null);

			var filter = new IdsFilter { Values = values, Type = new[] { type } };

			this.SetCacheAndName(filter);
			return this.New(filter, f => f.IdsFilter = filter);
		}
		/// <summary>
		/// Filters documents that only have the provided ids. 
		/// Note, this filter does not require the _id field to be indexed since it works using the _uid field.
		/// </summary>
		public BaseFilter Ids(IEnumerable<string> types, IEnumerable<string> values)
		{
			if (!types.HasAny() || types.All(t=>t.IsNullOrEmpty()))
				return CreateConditionlessFilterDescriptor("ids", null);
			
			var filter = new IdsFilter { Values = values, Type = types };
			
			this.SetCacheAndName(filter);
			return  this.New(filter, f => f.IdsFilter = filter);
		}

		/// <summary>
		/// A filter allowing to filter hits based on a point location using a bounding box
		/// </summary>
		public BaseFilter GeoBoundingBox(Expression<Func<T, object>> fieldDescriptor, double topLeftX, double topLeftY, double bottomRightX, double bottomRightY, GeoExecution? Type = null)
		{
			var c = CultureInfo.InvariantCulture;
			topLeftX.ThrowIfNull("topLeftX");
			topLeftY.ThrowIfNull("topLeftY");
			bottomRightX.ThrowIfNull("bottomRightX");
			bottomRightY.ThrowIfNull("bottomRightY");
			var geoHashTopLeft = "{0}, {1}".F(topLeftX.ToString(c), topLeftY.ToString(c));
			var geoHashBottomRight = "{0}, {1}".F(bottomRightX.ToString(c), bottomRightY.ToString(c));
			return this.GeoBoundingBox(fieldDescriptor, geoHashTopLeft, geoHashBottomRight, Type);
		}
		/// <summary>
		/// A filter allowing to filter hits based on a point location using a bounding box
		/// </summary>
		public BaseFilter GeoBoundingBox(string fieldName, double topLeftX, double topLeftY, double bottomRightX, double bottomRightY, GeoExecution? Type = null)
		{
			var c = CultureInfo.InvariantCulture;
			topLeftX.ThrowIfNull("topLeftX");
			topLeftY.ThrowIfNull("topLeftY");
			bottomRightX.ThrowIfNull("bottomRightX");
			bottomRightY.ThrowIfNull("bottomRightY");
			var geoHashTopLeft = "{0}, {1}".F(topLeftX.ToString(c), topLeftY.ToString(c));
			var geoHashBottomRight = "{0}, {1}".F(bottomRightX.ToString(c), bottomRightY.ToString(c));
			return this.GeoBoundingBox(fieldName, geoHashTopLeft, geoHashBottomRight, Type);
		}
		/// <summary>
		/// A filter allowing to filter hits based on a point location using a bounding box
		/// </summary>
		public BaseFilter GeoBoundingBox(Expression<Func<T, object>> fieldDescriptor, string geoHashTopLeft, string geoHashBottomRight, GeoExecution? Type = null)
		{
			var filter = new GeoBoundingBoxFilter { TopLeft = geoHashTopLeft, BottomRight = geoHashBottomRight };
			return this.SetDictionary("geo_bounding_box", fieldDescriptor, filter, (d, b) =>
			{
				if (Type.HasValue) d.Add("type", Enum.GetName(typeof(GeoExecution), Type.Value));
				b.GeoBoundingBoxFilter = d;
			});
		}
		/// <summary>
		/// A filter allowing to filter hits based on a point location using a bounding box
		/// </summary>
		public BaseFilter GeoBoundingBox(string fieldName, string geoHashTopLeft, string geoHashBottomRight, GeoExecution? Type = null)
		{
			var filter = new GeoBoundingBoxFilter { TopLeft = geoHashTopLeft, BottomRight = geoHashBottomRight };
			return this.SetDictionary("geo_bounding_box", fieldName, filter, (d, b) =>
			{
				if (Type.HasValue) d.Add("type", Enum.GetName(typeof(GeoExecution), Type.Value));
				b.GeoBoundingBoxFilter = d;
			});
		}

		/// <summary>
		/// Filters documents that include only hits that exists within a specific distance from a geo point. 
		/// </summary>
		public BaseFilter GeoDistance(Expression<Func<T, object>> fieldDescriptor, Action<GeoDistanceFilterDescriptor> filterDescriptor)
		{
			return _GeoDistance(fieldDescriptor, filterDescriptor);
		}

		/// <summary>
		/// Filters documents that include only hits that exists within a specific distance from a geo point. 
		/// </summary>
		public BaseFilter GeoDistance(string field, Action<GeoDistanceFilterDescriptor> filterDescriptor)
		{
			return _GeoDistance(field, filterDescriptor);
		}

		private BaseFilter _GeoDistance(PropertyPathMarker field, Action<GeoDistanceFilterDescriptor> filterDescriptor)
		{
			var filter = new GeoDistanceFilterDescriptor();
			if (filterDescriptor != null)
				filterDescriptor(filter);

			return this.SetDictionary("geo_distance", field, filter, (d, b) =>
			{
				var dd = new Dictionary<PropertyPathMarker, object>();
				dd.Add("distance", filter._Distance);

				if (!string.IsNullOrWhiteSpace(filter._GeoUnit))
					dd.Add("unit", filter._GeoUnit);

				if (!string.IsNullOrWhiteSpace(filter._GeoOptimizeBBox))
					dd.Add("optimize_bbox", filter._GeoOptimizeBBox);

				d.ForEachWithIndex((kv, i) => dd.Add(kv.Key, kv.Value));
				dd[field] = filter._Location;
				b.GeoDistanceFilter = dd;
			});
		}

		/// <summary>
		/// Filters documents that exists within a range from a specific point:
		/// </summary>
		public BaseFilter GeoDistanceRange(Expression<Func<T, object>> fieldDescriptor, Action<GeoDistanceRangeFilterDescriptor> filterDescriptor)
		{
			return _GeoDistanceRange(fieldDescriptor, filterDescriptor);
		}
		/// <summary>
		/// Filters documents that exists within a range from a specific point:
		/// </summary>
		public BaseFilter GeoDistanceRange(string field, Action<GeoDistanceRangeFilterDescriptor> filterDescriptor)
		{
			return _GeoDistanceRange(field, filterDescriptor);
		}

		private BaseFilter _GeoDistanceRange(PropertyPathMarker field, Action<GeoDistanceRangeFilterDescriptor> filterDescriptor)
		{
			var filter = new GeoDistanceRangeFilterDescriptor();
			if (filterDescriptor != null)
				filterDescriptor(filter);

			return this.SetDictionary("geo_distance_range", field, filter, (d, b) =>
			{
				var dd = new Dictionary<PropertyPathMarker, object>();
				dd.Add("from", filter._FromDistance);
				dd.Add("to", filter._ToDistance);
				if (!string.IsNullOrWhiteSpace(filter._GeoUnit))
					dd.Add("distance_type", filter._GeoUnit);

				if (!string.IsNullOrWhiteSpace(filter._GeoOptimizeBBox))
					dd.Add("optimize_bbox", filter._GeoOptimizeBBox);

				d.ForEachWithIndex((kv, i) => dd.Add(kv.Key, kv.Value));
				dd[field] = filter._Location;
				b.GeoDistanceRangeFilter = dd;
			});
		}

		/// <summary>
		/// Filter documents indexed using the geo_shape type.
		/// </summary>
		public BaseFilter GeoShape(Expression<Func<T, object>> fieldDescriptor, Action<GeoShapeFilterDescriptor> filterDescriptor)
		{
			return _GeoShape(fieldDescriptor, filterDescriptor);
		}
		/// <summary>
		/// Filter documents indexed using the geo_shape type.
		/// </summary>
		public BaseFilter GeoShape(string field, Action<GeoShapeFilterDescriptor> filterDescriptor)
		{
			return _GeoShape(field, filterDescriptor);
		}

		private BaseFilter _GeoShape(PropertyPathMarker field, Action<GeoShapeFilterDescriptor> filterDescriptor)
		{
			var filter = new GeoShapeFilterDescriptor();
			if (filterDescriptor != null)
				filterDescriptor(filter);

			return this.SetDictionary("geo_shape", field, filter, (d, b) => { b.GeoShapeFilter = d; });
		}

		/// <summary>
		/// Filter documents indexed using the geo_shape type.
		/// </summary>
		public BaseFilter GeoIndexedShape(Expression<Func<T, object>> fieldDescriptor, Action<GeoIndexedShapeFilterDescriptor> filterDescriptor)
		{
			return this._GeoIndexedShape(fieldDescriptor, filterDescriptor);
		}
		/// <summary>
		/// Filter documents indexed using the geo_shape type.
		/// </summary>
		public BaseFilter GeoIndexedShape(string field, Action<GeoIndexedShapeFilterDescriptor> filterDescriptor)
		{
			return _GeoIndexedShape(field, filterDescriptor);
		}

		private BaseFilter _GeoIndexedShape(PropertyPathMarker field, Action<GeoIndexedShapeFilterDescriptor> filterDescriptor)
		{
			var filter = new GeoIndexedShapeFilterDescriptor();
			if (filterDescriptor != null)
				filterDescriptor(filter);

			return this.SetDictionary("geo_shape", field, filter, (d, b) => { b.GeoShapeFilter = d; });
		}

		/// <summary>
		/// A filter allowing to include hits that only fall within a polygon of points. 
		/// </summary>
		public BaseFilter GeoPolygon(Expression<Func<T, object>> fieldDescriptor, IEnumerable<Tuple<double, double>> points)
		{
			var c = CultureInfo.InvariantCulture;
			return this._GeoPolygon(fieldDescriptor, points.Select(p => "{0}, {1}".F(p.Item1.ToString(c), p.Item2.ToString(c))).ToArray());
		}
		/// <summary>
		/// A filter allowing to include hits that only fall within a polygon of points. 
		/// </summary>
		public BaseFilter GeoPolygon(string field, IEnumerable<Tuple<double, double>> points)
		{
			var c = CultureInfo.InvariantCulture;
			return this.GeoPolygon(field, points.Select(p => "{0}, {1}".F(p.Item1.ToString(c), p.Item2.ToString(c))).ToArray());
		}
		/// <summary>
		/// A filter allowing to include hits that only fall within a polygon of points. 
		/// </summary>
		public BaseFilter GeoPolygon(Expression<Func<T, object>> fieldDescriptor, params string[] points)
		{
			return this._GeoPolygon(fieldDescriptor, points);
		}
		/// <summary>
		/// A filter allowing to include hits that only fall within a polygon of points. 
		/// </summary>
		public BaseFilter GeoPolygon(string fieldName, params string[] points)
		{
			return _GeoPolygon(fieldName, points);
		}

		private BaseFilter _GeoPolygon(PropertyPathMarker fieldName, string[] points)
		{
			var filter = new GeoPolygonFilter {Points = points};
			return this.SetDictionary("geo_polygon", fieldName, filter, (d, b) => { b.GeoPolygonFilter = d; });
		}

		/// <summary>
		/// The has_child filter accepts a query and the child type to run against, 
		/// and results in parent documents that have child docs matching the query.
		/// </summary>
		/// <typeparam name="K">Type of the child</typeparam>
		public BaseFilter HasChild<K>(Action<HasChildFilterDescriptor<K>> filterSelector) where K : class
		{
			var filter = new HasChildFilterDescriptor<K>();
			if (filterSelector != null)
				filterSelector(filter);

			return this.New(filter, f => f.HasChildFilter = filter);
		}

		/// <summary>
		/// The has_child filter accepts a query and the child type to run against, 
		/// and results in parent documents that have child docs matching the query.
		/// </summary>
		/// <typeparam name="K">Type of the child</typeparam>
		public BaseFilter HasParent<K>(Action<HasParentFilterDescriptor<K>> filterSelector) where K : class
		{
			var filter = new HasParentFilterDescriptor<K>();
			if (filterSelector != null)
				filterSelector(filter);

			return this.New(filter, f => f.HasParentFilter = filter);
		}

		/// <summary>
		/// A limit filter limits the number of documents (per shard) to execute on.
		/// </summary>
		public BaseFilter Limit(int? limit)
		{
			var filter = new LimitFilter { Value = limit };

			return  this.New(filter, f => f.LimitFilter = filter);
		}
		/// <summary>
		/// Filters documents matching the provided document / mapping type. 
		/// Note, this filter can work even when the _type field is not indexed 
		/// (using the _uid field).
		/// </summary>
		public BaseFilter Type(string type)
		{
			var filter = new TypeFilter { Value = type };
			return  this.New(filter, f => f.TypeFilter = filter);
		}

		/// <summary>
		/// Filters documents matching the provided document / mapping type. 
		/// Note, this filter can work even when the _type field is not indexed 
		/// (using the _uid field).
		/// </summary>
		public BaseFilter Type(Type type)
		{
			var filter = new TypeFilter { Value = type };
			return this.New(filter, f=> f.TypeFilter = filter);
		}

		/// <summary>
		/// A filter that matches on all documents.
		/// </summary>
		public BaseFilter MatchAll()
		{
			var filter = new MatchAllFilter { };
			return this.New(filter, f=> f.MatchAllFilter = filter);
		}
		/// <summary>
		/// Filters documents with fields that have values within a certain numeric range. 
		/// Similar to range filter, except that it works only with numeric values, 
		/// and the filter execution works differently.
		/// </summary>
		public BaseFilter NumericRange(Action<NumericRangeFilterDescriptor<T>> numericRangeSelector)
		{
			var filter = new NumericRangeFilterDescriptor<T>();
			if (numericRangeSelector != null)
				numericRangeSelector(filter);
			
			return this.SetDictionary("numeric_range", filter._Field, filter, (d, b) =>
			{
				b.NumericRangeFilter = d;
			});

		}
		/// <summary>
		/// Filters documents with fields that have terms within a certain range. 
		/// Similar to range query, except that it acts as a filter. 
		/// </summary>
		public BaseFilter Range(Action<RangeFilterDescriptor<T>> rangeSelector)
		{
			var filter = new RangeFilterDescriptor<T>();
			if (rangeSelector != null)
				rangeSelector(filter);
			
			return this.SetDictionary("range", filter._Field, filter, (d, b) =>
			{
				b.RangeFilter = d;
			});

		}
		/// <summary>
		/// A filter allowing to define scripts as filters. 
		/// </summary>
		public BaseFilter Script(Action<ScriptFilterDescriptor> scriptSelector)
		{
			var descriptor = new ScriptFilterDescriptor();
			if (scriptSelector != null)
				scriptSelector(descriptor);

			return this.New(descriptor, f=>f.ScriptFilter = descriptor);
		}
		/// <summary>
		/// Filters documents that have fields containing terms with a specified prefix 
		/// (not analyzed). Similar to phrase query, except that it acts as a filter. 
		/// </summary>
		public BaseFilter Prefix(Expression<Func<T, object>> fieldDescriptor, string prefix)
		{
			return this.SetDictionary("prefix", fieldDescriptor, prefix, (d, b) => { b.PrefixFilter = d; });
		}
		/// <summary>
		/// Filters documents that have fields containing terms with a specified prefix 
		/// (not analyzed). Similar to phrase query, except that it acts as a filter. 
		/// </summary>
		public BaseFilter Prefix(string field, string prefix)
		{
			return this.SetDictionary("prefix", field, prefix, (d, b) => { b.PrefixFilter = d; });

		}
		/// <summary>
		/// Filters documents that have fields that contain a term (not analyzed). 
		/// Similar to term query, except that it acts as a filter
		/// </summary>
		public BaseFilter Term<K>(Expression<Func<T, K>> fieldDescriptor, K term)
		{
			var t = new TermFilter() { Field = fieldDescriptor, Value = term };
			return this.SetDictionary("term", fieldDescriptor, term, (d, b) => { b.TermFilter = d; });
		}
		/// <summary>
		/// Filters documents that have fields that contain a term (not analyzed). 
		/// Similar to term query, except that it acts as a filter
		/// </summary>
		public BaseFilter Term(Expression<Func<T, object>> fieldDescriptor, object term)
		{
			var t = new TermFilter() { Field = fieldDescriptor, Value = term };
			return this.SetDictionary("term", fieldDescriptor, term, (d, b) => { b.TermFilter = d; });
		}
	
		/// <summary>
		/// Filters documents that have fields that contain a term (not analyzed).
		/// Similar to term query, except that it acts as a filter
		/// </summary>
		public BaseFilter Term(string field, object term)
		{
			var t = new TermFilter() { Field = field, Value = term };
			return this.SetDictionary("term", field, term, (d, b) => { b.TermFilter = d; });

		}
		/// <summary>
		/// Filters documents that have fields that match any of the provided terms (not analyzed). 
		/// </summary>
		public BaseFilter Terms<K>(Expression<Func<T, K>> fieldDescriptor, IEnumerable<K> terms, TermsExecution? Execution = null)
		{
			return this.SetDictionary("terms", fieldDescriptor, terms, (d, b) =>
			{
				if (Execution.HasValue) d.Add("execution", Enum.GetName(typeof(TermsExecution), Execution));
				b.TermsFilter = d;
			});
		}	
		
		/// <summary>
		/// Filters documents that have fields that match any of the provided terms (not analyzed). 
		/// </summary>
		public BaseFilter Terms(Expression<Func<T, object>> fieldDescriptor, IEnumerable<string> terms, TermsExecution? Execution = null)
		{
			return this.SetDictionary("terms", fieldDescriptor, terms, (d, b) =>
			{
				if (Execution.HasValue) d.Add("execution", Enum.GetName(typeof(TermsExecution), Execution));
				b.TermsFilter = d;
			});
		}

		/// <summary>
		/// Filters documents that have fields that match any of the provided terms (not analyzed). 
		/// </summary>
		public BaseFilter Terms(string field, IEnumerable<string> terms, TermsExecution? Execution = null)
		{
			return this.SetDictionary("terms", field, terms, (d, b) =>
			{
				if (Execution.HasValue) d.Add("execution", Enum.GetName(typeof(TermsExecution), Execution));
				b.TermsFilter = d;
			});
		}

		/// <summary>
		/// Filter documents indexed using the geo_shape type.
		/// </summary>
		public BaseFilter TermsLookup(Expression<Func<T, object>> fieldDescriptor, Action<TermsLookupFilterDescriptor> filterDescriptor)
		{
			var filter = new TermsLookupFilterDescriptor();
			if (filterDescriptor != null)
				filterDescriptor(filter);
			return this.SetDictionary("terms", fieldDescriptor, filter, (d, b) => { b.TermsFilter = d; });
		}
		/// <summary>
		/// Filter documents indexed using the geo_shape type.
		/// </summary>
		public BaseFilter TermsLookup(string field, Action<TermsLookupFilterDescriptor> filterDescriptor)
		{
			var filter = new TermsLookupFilterDescriptor();
			if (filterDescriptor != null)
				filterDescriptor(filter);
			return this.SetDictionary("terms", field, filter, (d, b) => { b.TermsFilter = d; });
		}


		/// <summary>
		/// A filter that matches documents using AND boolean operator on other queries. 
		/// This filter is more performant then bool filter. 
		/// </summary>
		public BaseFilter And(params Func<FilterDescriptor<T>, BaseFilter>[] selectors)
		{
			return this.And((from selector in selectors 
							 let filter = new FilterDescriptor<T>() 
							 select selector(filter)).ToArray());
		}
		/// <summary>
		/// A filter that matches documents using AND boolean operator on other queries. 
		/// This filter is more performant then bool filter. 
		/// </summary>
		public BaseFilter And(params BaseFilter[] filters)
		{
			return this.SetDictionary("and", "filters", filters.ToList(), (d, b) => b.AndFilter = d);
		}
		/// <summary>
		/// A filter that matches documents using OR boolean operator on other queries. 
		/// This filter is more performant then bool filter
		/// </summary>
		public BaseFilter Or(params Func<FilterDescriptor<T>, BaseFilter>[] selectors)
		{
			var descriptors = (from selector in selectors 
							   let filter = new FilterDescriptor<T>() 
							   select selector(filter)
							  ).ToArray();
			return this.Or(descriptors);

		}
		/// <summary>
		/// A filter that matches documents using OR boolean operator on other queries. 
		/// This filter is more performant then bool filter
		/// </summary>
		public BaseFilter Or(params BaseFilter[] filters)
		{
			return this.SetDictionary("or", "filters", filters.ToList(), (d, b) =>
			{
				b.OrFilter = d;
			});
		}
		/// <summary>
		/// A filter that filters out matched documents using a query. 
		/// This filter is more performant then bool filter. 
		/// </summary>
		public BaseFilter Not(Func<FilterDescriptor<T>, BaseFilter> selector)
		{
			var filter = new FilterDescriptor<T>();
			BaseFilter bf = filter;
			if (selector != null)
				bf = selector(filter);

			return this.SetDictionary("not", "filter", bf, (d, b) =>
			{
				b.NotFilter = d;
			});

		}
		/// <summary>
		/// 
		/// A filter that matches documents matching boolean combinations of other queries.
		/// Similar in concept to Boolean query, except that the clauses are other filters. 
		/// </summary>
		public BaseFilter Bool(Action<BoolFilterDescriptor<T>> booleanFilter)
		{
			var filter = new BoolFilterDescriptor<T>();
			if (booleanFilter != null)
				booleanFilter(filter);

			return this.New(filter, f => f.BoolFilterDescriptor = filter);

		}
		/// <summary>
		/// Wraps any query to be used as a filter. 
		/// </summary>
		public BaseFilter Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{

			var descriptor = new QueryDescriptor<T>();
			BaseQuery bq = descriptor;
			if (querySelector != null)
				bq = querySelector(descriptor);

			return this.SetDictionary("query", "query", bq, (d, b) =>
			{
				b.QueryFilter = d;
			});
		}


		/// <summary>
		///  A nested filter, works in a similar fashion to the nested query, except used as a filter.
		///  It follows exactly the same structure, but also allows to cache the results 
		///  (set _cache to true), and have it named (set the _name value). 
		/// </summary>
		/// <param name="selector"></param>
		public BaseFilter Nested(Action<NestedFilterDescriptor<T>> selector)
		{
			var filter = new NestedFilterDescriptor<T>();
			if (selector != null)
				selector(filter);

			return this.New(filter, f=>f.NestedFilter = filter);
		}

		/// <summary>
		///  The regexp filter allows you to use regular expression term queries. 
		/// </summary>
		/// <param name="selector"></param>
		public BaseFilter Regexp(Action<RegexpFilterDescriptor<T>> selector)
		{
			var filter = new RegexpFilterDescriptor<T>();
			if (selector != null)
				selector(filter);

			return this.SetDictionary("regexp", filter._Field, filter, (d, b) =>
			{
				b.RegexpFilter = d;
			});
		}

		private FilterDescriptor<T> CreateConditionlessFilterDescriptor(object filter, string type = null)
		{
			if (this.IsStrict && !this.IsVerbatim)
				throw new DslException("Filter resulted in a conditionless '{1}' filter (json by approx):\n{0}"
					.F(
						JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
						, type ?? filter.GetType().Name.Replace("Descriptor", "").Replace("`1", "")
					)
				);
			return new FilterDescriptor<T> { IsConditionless = true, IsVerbatim = this.IsVerbatim, IsStrict = this.IsStrict };
		}

		private FilterDescriptor<T> New(FilterBase filter, Action<FilterDescriptor<T>> fillProperty)
		{
			if (filter.IsConditionless && !this.IsVerbatim)
				return CreateConditionlessFilterDescriptor(filter);

			this.SetCacheAndName(filter);

			var f = new FilterDescriptor<T> { IsStrict = this.IsStrict, IsVerbatim = this.IsVerbatim };

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

		private void SetCacheAndName(FilterBase filter)
		{
			if (this._Cache.HasValue)
				filter._Cache = this._Cache;
			if (!string.IsNullOrWhiteSpace(this._Name))
				filter._Name = this._Name;
			if (!string.IsNullOrWhiteSpace(this._CacheKey))
				filter._CacheKey = this._Name;
		}

		private BaseFilter SetDictionary(
			string type,
			PropertyPathMarker key,
			object value,
			Action<Dictionary<PropertyPathMarker, object>, FilterDescriptor<T>> setter
		)
		{
			setter.ThrowIfNull("setter");
			var dictionary = new Dictionary<PropertyPathMarker, object>();

			if (key.IsConditionless())
				return CreateConditionlessFilterDescriptor(dictionary, type);

			dictionary.Add(key, value);
			if (this._Cache.HasValue)
				dictionary.Add("_cache", this._Cache);
			if (!string.IsNullOrWhiteSpace(this._Name))
				dictionary.Add("_name", this._Name);
			if (!string.IsNullOrWhiteSpace(this._CacheKey))
				dictionary.Add("_cache_key", this._CacheKey);

			this.ResetCache();

			var bucket = new FilterDescriptor<T> { IsStrict = this.IsStrict, IsVerbatim = this.IsVerbatim };
			setter(dictionary, bucket);
			if (this.IsVerbatim)
				return bucket;
			
			//find out if we are conditionless

			if (value == null)
				return CreateConditionlessFilterDescriptor(dictionary, type);
			else if (value is string)
			{
				if (string.IsNullOrEmpty(value.ToString()))
					return CreateConditionlessFilterDescriptor(value, type);
			}
			else if (value is IEnumerable<BaseFilter>)
			{
				var l = (IEnumerable<object>)value;
				var baseFilters = l.OfType<BaseFilter>().ToList();
				var allBaseFiltersConditionless = baseFilters.All(b => b.IsConditionless);
				if (!baseFilters.HasAny() || allBaseFiltersConditionless)
					return CreateConditionlessFilterDescriptor(dictionary, type);
			}
			else if (value is IEnumerable<string>)
			{
				var l = (IEnumerable<string>)value;
				var strings = l.ToList();
				var allStringsNullOrEmpty = strings.All(s => s.IsNullOrEmpty());
				if (!strings.HasAny() || allStringsNullOrEmpty)
					return CreateConditionlessFilterDescriptor(dictionary, type);
			}
			else if (value is IEnumerable<object>)
			{
				var l = (IEnumerable<object>)value;
				if (!l.HasAny())
					return CreateConditionlessFilterDescriptor(dictionary, type);
			}
			else if (value is FilterBase)
			{
				var bf = (FilterBase)value;
				if (bf.IsConditionless)
					return CreateConditionlessFilterDescriptor(bf, type);
			}
			else if (value is FilterDescriptor<T>)
			{
				var bf = (FilterDescriptor<T>)value;
				if (bf.IsConditionless)
					return CreateConditionlessFilterDescriptor(bf, type);
			}
			else if (value is BaseQuery)
			{
				var bf = (BaseQuery)value;
				if (bf.IsConditionless)
					return CreateConditionlessFilterDescriptor(bf, type);
			}

			if (key.IsConditionless())
				return CreateConditionlessFilterDescriptor(value, type);

			return bucket;
		}


	}
}
