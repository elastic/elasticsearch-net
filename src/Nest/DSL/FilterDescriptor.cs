using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using System.Globalization;
using Nest.Resolvers;
using System.Collections;

namespace Nest
{
	public class FilterDescriptor : FilterDescriptor<dynamic>
	{

	}

	public class FilterDescriptor<T> : BaseFilter, IFilterDescriptor<T> where T : class
	{
		internal string _Name { get; set; }
		internal string _CacheKey { get; set; }
		internal bool? _Cache { get; set; }

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

		[JsonProperty(PropertyName = "exists")]
		internal ExistsFilter ExistsFilter { get; set; }

		[JsonProperty(PropertyName = "missing")]
		internal MissingFilter MissingFilter { get; set; }

		[JsonProperty(PropertyName = "ids")]
		internal IdsFilter IdsFilter { get; set; }

		[JsonProperty(PropertyName = "geo_bounding_box")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, object> GeoBoundingBoxFilter { get; set; }

		[JsonProperty(PropertyName = "geo_distance")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, object> GeoDistanceFilter { get; set; }

		[JsonProperty(PropertyName = "geo_distance_range")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, object> GeoDistanceRangeFilter { get; set; }

		[JsonProperty(PropertyName = "geo_polygon")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, object> GeoPolygonFilter { get; set; }

		[JsonProperty(PropertyName = "geo_shape")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, object> GeoShapeFilter { get; set; }

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
		internal Dictionary<string, object> NumericRangeFilter { get; set; }

		[JsonProperty(PropertyName = "range")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, object> RangeFilter { get; set; }

		[JsonProperty(PropertyName = "prefix")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, object> PrefixFilter { get; set; }

		[JsonProperty(PropertyName = "term")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, object> TermFilter { get; set; }

		[JsonProperty(PropertyName = "terms")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, object> TermsFilter { get; set; }

		[JsonProperty(PropertyName = "fquery")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, object> QueryFilter { get; set; }

		[JsonProperty(PropertyName = "and")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, object> AndFilter { get; set; }

		[JsonProperty(PropertyName = "or")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, object> OrFilter { get; set; }

		[JsonProperty(PropertyName = "not")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, object> NotFilter { get; set; }

		[JsonProperty(PropertyName = "script")]
		internal ScriptFilterDescriptor ScriptFilter { get; set; }

		[JsonProperty(PropertyName = "nested")]
		internal NestedFilterDescriptor<T> NestedFilter { get; set; }

		[JsonProperty(PropertyName = "regexp")]
		internal Dictionary<string, object> RegexpFilter { get; set; }



		public FilterDescriptor<T> Strict(bool strict = true)
		{
			return new FilterDescriptor<T> { IsStrict = strict };

		}

		internal BaseFilter CreateConditionlessFilterDescriptor(string type, object filter)
		{
			if (this.IsStrict)
				throw new DslException("Filter resulted in a conditionless '{1}' filter (json by approx):\n{0}"
					.F(
						JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
						, type ?? filter.GetType().Name.Replace("Descriptor", "").Replace("`1", "")
					)
				);
			return new FilterDescriptor<T> { IsConditionless = !this.IsStrict };
		}

		internal FilterDescriptor<T> New(Action<FilterDescriptor<T>> fillProperty)
		{
			var f = new FilterDescriptor<T>();
			f.IsStrict = this.IsStrict;

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
			string key,
			object value,
			Action<Dictionary<string, object>, FilterDescriptor<T>> setter
		)
		{
			setter.ThrowIfNull("setter");

			var dictionary = new Dictionary<string, object>();
			dictionary.Add(key, value);
			if (this._Cache.HasValue)
				dictionary.Add("_cache", this._Cache);
			if (!string.IsNullOrWhiteSpace(this._Name))
				dictionary.Add("_name", this._Name);
			if (!string.IsNullOrWhiteSpace(this._CacheKey))
				dictionary.Add("_cache_key", this._CacheKey);

			this.ResetCache();

			var bucket = this.New(null);
			setter(dictionary, bucket);

			var conditionlessReturn = CreateConditionlessFilterDescriptor(type, dictionary);


			if (value is IEnumerable<BaseFilter>)
			{
				var l = (IEnumerable<object>)value;
				var baseFilters = l.OfType<BaseFilter>();
				var allBaseFiltersConditionless = baseFilters.All(b => b.IsConditionless);
				if (!baseFilters.HasAny() || allBaseFiltersConditionless)
					return conditionlessReturn;
			}
			else if (value is IEnumerable<string>)
			{
				var l = (IEnumerable<string>)value;
				var strings = l.OfType<string>();
				var allStringsNullOrEmpty = strings.All(s=>s.IsNullOrEmpty());
				if (!strings.HasAny() || allStringsNullOrEmpty)
					return conditionlessReturn;
			}
			else if (value is IEnumerable<object>)
			{
				var l = (IEnumerable<object>)value;
				if (!l.HasAny())
					return conditionlessReturn;
			}
			else if (value is FilterBase)
			{
				var bf = (FilterBase)value;
				if (bf.IsConditionless)
					return CreateConditionlessFilterDescriptor(type, bf);
				else if (key.IsNullOrEmpty())
					return CreateConditionlessFilterDescriptor(type, bf);
			}
			else if (value is string)
			{
				if (string.IsNullOrEmpty(value.ToString()))
					return CreateConditionlessFilterDescriptor(type, value);
			}
			if (key.IsNullOrEmpty())
				return CreateConditionlessFilterDescriptor(type, value);

			return bucket;
		}

		/// <summary>
		/// Filters documents where a specific field has a value in them.
		/// </summary>
		public BaseFilter Exists(Expression<Func<T, object>> fieldDescriptor)
		{
			var field = new PropertyNameResolver().Resolve(fieldDescriptor);
			return this.Exists(field);
		}
		/// <summary>
		/// Filters documents where a specific field has a value in them.
		/// </summary>
		public BaseFilter Exists(string field)
		{
			var filter = new ExistsFilter { Field = field };
			this.SetCacheAndName(filter);
			if (filter.IsConditionless)
				return CreateConditionlessFilterDescriptor("exists", filter);
			return this.New(f => f.ExistsFilter = filter);
		}
		/// <summary>
		/// Filters documents where a specific field has no value in them.
		/// </summary>
		public BaseFilter Missing(Expression<Func<T, object>> fieldDescriptor)
		{
			var field = new PropertyNameResolver().Resolve(fieldDescriptor);
			return this.Missing(field);
		}
		/// <summary>
		/// Filters documents where a specific field has no value in them.
		/// </summary>
		public BaseFilter Missing(string field)
		{
			var filter = new MissingFilter { Field = field };
			if (filter.IsConditionless)
				return CreateConditionlessFilterDescriptor("ids", filter);

			this.SetCacheAndName(filter);
			return  this.New(f => f.MissingFilter = filter);
		}
		/// <summary>
		/// Filters documents that only have the provided ids. 
		/// Note, this filter does not require the _id field to be indexed since it works using the _uid field.
		/// </summary>
		public BaseFilter Ids(IEnumerable<string> values)
		{
			var filter = new IdsFilter { Values = values };
			if (filter.IsConditionless)
				return CreateConditionlessFilterDescriptor("ids", filter);

			this.SetCacheAndName(filter);
			return this.New(f => f.IdsFilter = filter);
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
			if (filter.IsConditionless)
				return CreateConditionlessFilterDescriptor("ids", filter);

			this.SetCacheAndName(filter);
			return this.New(f => f.IdsFilter = filter);
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
			if (filter.IsConditionless)
				return CreateConditionlessFilterDescriptor("ids", filter);
			
			this.SetCacheAndName(filter);
			return  this.New(f => f.IdsFilter = filter);
		}

		/// <summary>
		/// A filter allowing to filter hits based on a point location using a bounding box
		/// </summary>
		public BaseFilter GeoBoundingBox(Expression<Func<T, object>> fieldDescriptor, string geoHashTopLeft, string geoHashBottomRight, GeoExecution? Type = null)
		{
			var field = new PropertyNameResolver().Resolve(fieldDescriptor);
			return this.GeoBoundingBox(field, geoHashTopLeft, geoHashBottomRight, Type);
		}
		/// <summary>
		/// A filter allowing to filter hits based on a point location using a bounding box
		/// </summary>
		public BaseFilter GeoBoundingBox(Expression<Func<T, object>> fieldDescriptor, double topLeftX, double topLeftY, double bottomRightX, double bottomRightY, GeoExecution? Type = null)
		{
			var field = new PropertyNameResolver().Resolve(fieldDescriptor);
			return this.GeoBoundingBox(field, topLeftX, topLeftY, bottomRightX, bottomRightY, Type);
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
			return this.GeoBoundingBox(fieldName,
				"{0}, {1}".F(topLeftX.ToString(c), topLeftY.ToString(c)),
				"{0}, {1}".F(bottomRightX.ToString(c), bottomRightY.ToString(c)), Type);
		}
		/// <summary>
		/// A filter allowing to filter hits based on a point location using a bounding box
		/// </summary>
		public BaseFilter GeoBoundingBox(string fieldName, string geoHashTopLeft, string geoHashBottomRight, GeoExecution? Type = null)
		{
			var filter = new GeoBoundingBoxFilter { TopLeft = geoHashTopLeft, BottomRight = geoHashBottomRight };
			return this.SetDictionary("geo_bounding_box", fieldName, filter, (d, b) =>
			{
				if (Type.HasValue)
					d.Add("type", Enum.GetName(typeof(GeoExecution), Type.Value));
				b.GeoBoundingBoxFilter = d;
			});
		}
		/// <summary>
		/// Filters documents that include only hits that exists within a specific distance from a geo point. 
		/// </summary>
		public BaseFilter GeoDistance(Expression<Func<T, object>> fieldDescriptor, Action<GeoDistanceFilterDescriptor> filterDescriptor)
		{
			var field = new PropertyNameResolver().Resolve(fieldDescriptor);
			return this.GeoDistance(field, filterDescriptor);
		}
		/// <summary>
		/// Filters documents that include only hits that exists within a specific distance from a geo point. 
		/// </summary>
		public BaseFilter GeoDistance(string field, Action<GeoDistanceFilterDescriptor> filterDescriptor)
		{
			var filter = new GeoDistanceFilterDescriptor();
			if (filterDescriptor == null)
				return CreateConditionlessFilterDescriptor("geo_distance", filter);
			filterDescriptor(filter);

			return this.SetDictionary("geo_distance", field, filter._Location, (d, b) =>
			{
				var dd = new Dictionary<string, object>();
				dd.Add("distance", filter._Distance);

				if (!string.IsNullOrWhiteSpace(filter._GeoUnit))
					dd.Add("unit", filter._GeoUnit);

				if (!string.IsNullOrWhiteSpace(filter._GeoOptimizeBBox))
					dd.Add("optimize_bbox", filter._GeoOptimizeBBox);

				d.ForEachWithIndex((kv, i) => dd.Add(kv.Key, kv.Value));
				b.GeoDistanceFilter = dd;
			});

		}
		
		/// <summary>
		/// Filters documents that exists within a range from a specific point:
		/// </summary>
		public BaseFilter GeoDistanceRange(Expression<Func<T, object>> fieldDescriptor, Action<GeoDistanceRangeFilterDescriptor> filterDescriptor)
		{
			var field = new PropertyNameResolver().Resolve(fieldDescriptor);
			return this.GeoDistanceRange(field, filterDescriptor);
		}
		/// <summary>
		/// Filters documents that exists within a range from a specific point:
		/// </summary>
		public BaseFilter GeoDistanceRange(string field, Action<GeoDistanceRangeFilterDescriptor> filterDescriptor)
		{
			var filter = new GeoDistanceRangeFilterDescriptor();
			if (filterDescriptor == null)
				return CreateConditionlessFilterDescriptor("geo_distance_range", filter);

			filterDescriptor(filter);
			if (filter.IsConditionless)
				return CreateConditionlessFilterDescriptor("geo_distance_range", filter);
			
			return this.SetDictionary("geo_distance_range", field, filter._Location, (d, b) =>
			{

				var dd = new Dictionary<string, object>();
				dd.Add("from", filter._FromDistance);
				dd.Add("to", filter._ToDistance);
				if (!string.IsNullOrWhiteSpace(filter._GeoUnit))
					dd.Add("distance_type", filter._GeoUnit);

				if (!string.IsNullOrWhiteSpace(filter._GeoOptimizeBBox))
					dd.Add("optimize_bbox", filter._GeoOptimizeBBox);

				d.ForEachWithIndex((kv, i) => dd.Add(kv.Key, kv.Value));
				b.GeoDistanceRangeFilter = dd;
			});
		}

		/// <summary>
		/// Filter documents indexed using the geo_shape type.
		/// </summary>
		public BaseFilter GeoShape(Expression<Func<T, object>> fieldDescriptor, Action<GeoShapeFilterDescriptor> filterDescriptor)
		{
			var field = new PropertyNameResolver().Resolve(fieldDescriptor);
			return this.GeoShape(field, filterDescriptor);
		}
		/// <summary>
		/// Filter documents indexed using the geo_shape type.
		/// </summary>
		public BaseFilter GeoShape(string field, Action<GeoShapeFilterDescriptor> filterDescriptor)
		{
			var filter = new GeoShapeFilterDescriptor();
			if (filterDescriptor == null)
				return CreateConditionlessFilterDescriptor("geo_shape", filter);

			filterDescriptor(filter);
			if (filter.IsConditionless)
				return CreateConditionlessFilterDescriptor("geo_shape", filter);

			return this.SetDictionary("geo_shape", field, filter, (d, b) =>
			{
				b.GeoShapeFilter = d;
			});

		}
		/// <summary>
		/// Filter documents indexed using the geo_shape type.
		/// </summary>
		public BaseFilter GeoIndexedShape(Expression<Func<T, object>> fieldDescriptor, Action<GeoIndexedShapeFilterDescriptor> filterDescriptor)
		{
			var field = new PropertyNameResolver().Resolve(fieldDescriptor);
			return this.GeoIndexedShape(field, filterDescriptor);
		}
		/// <summary>
		/// Filter documents indexed using the geo_shape type.
		/// </summary>
		public BaseFilter GeoIndexedShape(string field, Action<GeoIndexedShapeFilterDescriptor> filterDescriptor)
		{
			var filter = new GeoIndexedShapeFilterDescriptor();
			if (filterDescriptor == null)
				return CreateConditionlessFilterDescriptor("geo_shape", filter);

			filterDescriptor(filter);
			if (filter.IsConditionless)
				return CreateConditionlessFilterDescriptor("geo_shape", filter);

			return this.SetDictionary("geo_shape", field, filter, (d, b) =>
			{
				b.GeoShapeFilter = d;
			});

		}


		/// <summary>
		/// A filter allowing to include hits that only fall within a polygon of points. 
		/// </summary>
		public BaseFilter GeoPolygon(Expression<Func<T, object>> fieldDescriptor, IEnumerable<Tuple<double, double>> points)
		{
			var field = new PropertyNameResolver().Resolve(fieldDescriptor);
			return this.GeoPolygon(field, points);
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
			var field = new PropertyNameResolver().Resolve(fieldDescriptor);
			return this.GeoPolygon(field, points);
		}
		/// <summary>
		/// A filter allowing to include hits that only fall within a polygon of points. 
		/// </summary>
		public BaseFilter GeoPolygon(string fieldName, params string[] points)
		{
			var filter = new GeoPolygonFilter { Points = points };
			return this.SetDictionary("geo_polygon", fieldName, filter, (d, b) =>
			{
				b.GeoPolygonFilter = d;
			});

		}
		/// <summary>
		/// The has_child filter accepts a query and the child type to run against, 
		/// and results in parent documents that have child docs matching the query.
		/// </summary>
		/// <typeparam name="K">Type of the child</typeparam>
		public BaseFilter HasChild<K>(Action<HasChildFilterDescriptor<K>> filterSelector) where K : class
		{
			var filter = new HasChildFilterDescriptor<K>();
			if (filterSelector == null)
				return CreateConditionlessFilterDescriptor("has_child", filter);
			
			filterSelector(filter);
			
			if (filter.IsConditionless)
				return CreateConditionlessFilterDescriptor("has_child", filter);

			return  this.New(f => f.HasChildFilter = filter);
		}

		/// <summary>
		/// The has_child filter accepts a query and the child type to run against, 
		/// and results in parent documents that have child docs matching the query.
		/// </summary>
		/// <typeparam name="K">Type of the child</typeparam>
		public BaseFilter HasParent<K>(Action<HasParentFilterDescriptor<K>> filterSelector) where K : class
		{
			var filter = new HasParentFilterDescriptor<K>();
			if (filterSelector == null)
				return CreateConditionlessFilterDescriptor("has_parent", filter);

			filterSelector(filter);

			if (filter.IsConditionless)
				return CreateConditionlessFilterDescriptor("has_parent", filter);

			return  this.New(f => f.HasParentFilter = filter);
		}

		/// <summary>
		/// A limit filter limits the number of documents (per shard) to execute on.
		/// </summary>
		public BaseFilter Limit(int? limit)
		{
			var filter = new LimitFilter { Value = limit };
			if (filter.IsConditionless)
				return CreateConditionlessFilterDescriptor("limit", filter);

			this.SetCacheAndName(filter);
			return  this.New(f => f.LimitFilter = filter);
		}
		/// <summary>
		/// Filters documents matching the provided document / mapping type. 
		/// Note, this filter can work even when the _type field is not indexed 
		/// (using the _uid field).
		/// </summary>
		public BaseFilter Type(string type)
		{
			var filter = new TypeFilter { Value = type };
			if (filter.IsConditionless)
				return CreateConditionlessFilterDescriptor("filter", filter);

			this.SetCacheAndName(filter);
			return  this.New(f => f.TypeFilter = filter);
		}

		/// <summary>
		/// Filters documents matching the provided document / mapping type. 
		/// Note, this filter can work even when the _type field is not indexed 
		/// (using the _uid field).
		/// </summary>
		public BaseFilter Type(Type type)
		{
			var filter = new TypeFilter { Value = type };
			if (filter.IsConditionless)
				return CreateConditionlessFilterDescriptor("filter", filter);

			this.SetCacheAndName(filter);
			return this.New(f=> f.TypeFilter = filter);
		}

		/// <summary>
		/// A filter that matches on all documents.
		/// </summary>
		public BaseFilter MatchAll()
		{
			var filter = new MatchAllFilter { };
			this.SetCacheAndName(filter);
			return this.New(f=> f.MatchAllFilter = filter);
		}
		/// <summary>
		/// Filters documents with fields that have values within a certain numeric range. 
		/// Similar to range filter, except that it works only with numeric values, 
		/// and the filter execution works differently.
		/// </summary>
		public BaseFilter NumericRange(Action<NumericRangeFilterDescriptor<T>> numericRangeSelector)
		{
			var filter = new NumericRangeFilterDescriptor<T>();
			if (numericRangeSelector == null)
				return CreateConditionlessFilterDescriptor("numeric_range", filter);

			numericRangeSelector(filter);
			if (filter.IsConditionless)
				return CreateConditionlessFilterDescriptor("numeric_range", filter);

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
			if (rangeSelector == null)
				return CreateConditionlessFilterDescriptor("range", filter);
			
			rangeSelector(filter);
			if (filter.IsConditionless)
				return CreateConditionlessFilterDescriptor("range", filter);

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
			if (scriptSelector == null)
				return CreateConditionlessFilterDescriptor("script", descriptor);
			scriptSelector(descriptor);
			if (descriptor.IsConditionless)
				return CreateConditionlessFilterDescriptor("script", descriptor);

			this.SetCacheAndName(descriptor);
			return this.New(f=>f.ScriptFilter = descriptor);
		}
		/// <summary>
		/// Filters documents that have fields containing terms with a specified prefix 
		/// (not analyzed). Similar to phrase query, except that it acts as a filter. 
		/// </summary>
		public BaseFilter Prefix(Expression<Func<T, object>> fieldDescriptor, string prefix)
		{
			var field = new PropertyNameResolver().Resolve(fieldDescriptor);
			return this.Prefix(field, prefix);
		}
		/// <summary>
		/// Filters documents that have fields containing terms with a specified prefix 
		/// (not analyzed). Similar to phrase query, except that it acts as a filter. 
		/// </summary>
		public BaseFilter Prefix(string field, string prefix)
		{
			var descriptor = new FilterDescriptor<T>();
			return this.SetDictionary("prefix", field, prefix, (d, b) =>
			{
				b.PrefixFilter = d;
			});

		}
		/// <summary>
		/// Filters documents that have fields that contain a term (not analyzed). 
		/// Similar to term query, except that it acts as a filter
		/// </summary>
		public BaseFilter Term<K>(Expression<Func<T, K>> fieldDescriptor, K term)
		{
			var field = new PropertyNameResolver().Resolve(fieldDescriptor);
			return this.Term(field, term);
		}
		/// <summary>
		/// Filters documents that have fields that contain a term (not analyzed).
		/// Similar to term query, except that it acts as a filter
		/// </summary>
		public BaseFilter Term<K>(string field, K term)
		{
			var t = new Term() { Field = field, Value = (object)term };
			if (t.IsConditionless)
				return CreateConditionlessFilterDescriptor("term", new { term = term, field = field });

			return this.SetDictionary("term", field, term, (d, b) =>
			{
				b.TermFilter = d;
			});

		}
		/// <summary>
		/// Filters documents that have fields that match any of the provided terms (not analyzed). 
		/// </summary>
		public BaseFilter Terms(Expression<Func<T, object>> fieldDescriptor, IEnumerable<string> terms, TermsExecution? Execution = null)
		{
			var field = new PropertyNameResolver().Resolve(fieldDescriptor);
			return this.Terms(field, terms, Execution);
		}
		/// <summary>
		/// Filters documents that have fields that match any of the provided terms (not analyzed). 
		/// </summary>
		public BaseFilter Terms(string field, IEnumerable<string> terms, TermsExecution? Execution = null)
		{
			return this.SetDictionary("terms", field, terms, (d, b) =>
			{
				if (Execution.HasValue)
					d.Add("execution", Enum.GetName(typeof(TermsExecution), Execution));
				b.TermsFilter = d;
			});

		}

		/// <summary>
		/// Filter documents indexed using the geo_shape type.
		/// </summary>
		public BaseFilter TermsLookup(Expression<Func<T, object>> fieldDescriptor, Action<TermsLookupFilterDescriptor> filterDescriptor)
		{
			var field = new PropertyNameResolver().Resolve(fieldDescriptor);
			return this.TermsLookup(field, filterDescriptor);
		}
		/// <summary>
		/// Filter documents indexed using the geo_shape type.
		/// </summary>
		public BaseFilter TermsLookup(string field, Action<TermsLookupFilterDescriptor> filterDescriptor)
		{
			var filter = new TermsLookupFilterDescriptor();
			if (filterDescriptor == null)
				return CreateConditionlessFilterDescriptor("terms", filter);

			filterDescriptor(filter);
			if (filter.IsConditionless)
				return CreateConditionlessFilterDescriptor("terms", filter);

			return this.SetDictionary("terms", field, filter, (d, b) =>
			{
				b.TermsFilter = d;
			});

		}


		/// <summary>
		/// A filter that matches documents using AND boolean operator on other queries. 
		/// This filter is more performant then bool filter. 
		/// </summary>
		public BaseFilter And(params Func<FilterDescriptor<T>, BaseFilter>[] filters)
		{
			var descriptors = new List<BaseFilter>();
			foreach (var selector in filters)
			{
				var filter = new FilterDescriptor<T>();
				var f = selector(filter);
				if (f.IsConditionless)
					continue;
				descriptors.Add(f);
			}
			return this.SetDictionary("and", "filters", descriptors, (d, b) => b.AndFilter = d);
		}
		/// <summary>
		/// A filter that matches documents using AND boolean operator on other queries. 
		/// This filter is more performant then bool filter. 
		/// </summary>
		public BaseFilter And(params BaseFilter[] filters)
		{
			var descriptors = new List<BaseFilter>();
			foreach (var f in filters)
			{
				var filter = new FilterDescriptor<T>();
				if (f.IsConditionless)
					continue;
				descriptors.Add(f);
			}
			return this.SetDictionary("and", "filters", descriptors, (d, b) => b.AndFilter = d);
		}
		/// <summary>
		/// A filter that matches documents using OR boolean operator on other queries. 
		/// This filter is more performant then bool filter
		/// </summary>
		public BaseFilter Or(params Func<FilterDescriptor<T>, BaseFilter>[] filters)
		{
			var descriptors = new List<BaseFilter>();
			foreach (var selector in filters)
			{
				var filter = new FilterDescriptor<T>();
				var f = selector(filter);
				if (f.IsConditionless)
					continue;
				descriptors.Add(f);
			}
			return this.SetDictionary("or", "filters", descriptors, (d, b) =>
			{
				b.OrFilter = d;
			});
		}
		/// <summary>
		/// A filter that matches documents using OR boolean operator on other queries. 
		/// This filter is more performant then bool filter
		/// </summary>
		public BaseFilter Or(params BaseFilter[] filters)
		{
			var descriptors = new List<BaseFilter>();
			foreach (var f in filters)
			{
				var filter = new FilterDescriptor<T>();
				if (f.IsConditionless)
					continue;
				descriptors.Add(f);
			}
			return this.SetDictionary("or", "filters", descriptors, (d, b) =>
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
			if (selector == null)
				return CreateConditionlessFilterDescriptor("not", filter);

			var f = selector(filter);
			if (f.IsConditionless)
				return CreateConditionlessFilterDescriptor("not", filter);

			return this.SetDictionary("not", "filter", f, (d, b) =>
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
			booleanFilter(filter);
			this.SetCacheAndName(filter);
			if (filter.IsConditionless)
				return CreateConditionlessFilterDescriptor("bool", filter);

			return this.New(f => f.BoolFilterDescriptor = filter);

		}
		/// <summary>
		/// Wraps any query to be used as a filter. 
		/// </summary>
		public BaseFilter Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var descriptor = new QueryDescriptor<T>();
			if (querySelector == null)
				return CreateConditionlessFilterDescriptor("query", descriptor);

			var bq = querySelector(descriptor);
			if (bq.IsConditionless)
				return CreateConditionlessFilterDescriptor("query", bq);

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
			if (selector == null)
				return CreateConditionlessFilterDescriptor("nested", filter);

			selector(filter);
			if (filter.IsConditionless)
				return CreateConditionlessFilterDescriptor("nested", filter);

			this.SetCacheAndName(filter);
			return this.New(f=>f.NestedFilter = filter);
		}

		/// <summary>
		///  The regexp filter allows you to use regular expression term queries. 
		/// </summary>
		/// <param name="selector"></param>
		public BaseFilter Regexp(Action<RegexpFilterDescriptor<T>> selector)
		{
			var filter = new RegexpFilterDescriptor<T>();
			if (selector == null)
				return CreateConditionlessFilterDescriptor("regexp", filter);

			selector(filter);
			if (filter.IsConditionless)
				return CreateConditionlessFilterDescriptor("regexp", filter);

			//this.SetCacheAndName(filter);
			return this.SetDictionary("regexp", filter._Field, filter, (d, b) =>
			{
				b.RegexpFilter = d;
			});
		}

	}
}
