using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nest.DSL;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;

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


		public void Exists(Expression<Func<T, object>> fieldDescriptor)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.Exists(field);
		}
		public void Exists(string field)
		{
			this.ExistsFilter = new ExistsFilter { Field = field };
			this.SetCacheAndName(this.ExistsFilter);
		}
		public void Missing(Expression<Func<T, object>> fieldDescriptor)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.Missing(field);
		}
		public void Missing(string field)
		{
			this.MissingFilter = new MissingFilter { Field = field };
			this.SetCacheAndName(this.MissingFilter);
		}
		public void Ids(IEnumerable<string> values)
		{
			this.IdsFilter = new IdsFilter { Values = values };
			this.SetCacheAndName(this.IdsFilter);
		}
		public void Ids(string type, IEnumerable<string> values)
		{
			type.ThrowIfNullOrEmpty("type");
			this.IdsFilter = new IdsFilter { Values = values, Type = new []{ type } };
			this.SetCacheAndName(this.IdsFilter);
		}
		public void Ids(IEnumerable<string> types, IEnumerable<string> values)
		{
			this.IdsFilter = new IdsFilter { Values = values, Type = types };
			this.SetCacheAndName(this.IdsFilter);
		}


		public void GeoBoundingBox(Expression<Func<T, object>> fieldDescriptor, string geoHashTopLeft, string geoHashBottomRight, GeoExecution? Type = null)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.GeoBoundingBox(field, geoHashTopLeft, geoHashBottomRight, Type);
		}
		public void GeoBoundingBox(Expression<Func<T, object>> fieldDescriptor, double topLeftX, double topLeftY, double bottomRightX, double bottomRightY, GeoExecution? Type = null)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.GeoBoundingBox(field, topLeftX, topLeftY, bottomRightX, bottomRightY, Type);
		}
		public void GeoBoundingBox(string fieldName, double topLeftX, double topLeftY, double bottomRightX, double bottomRightY, GeoExecution? Type = null)
		{
			topLeftX.ThrowIfNull("topLeftX");
			topLeftY.ThrowIfNull("topLeftY");
			bottomRightX.ThrowIfNull("bottomRightX");
			bottomRightY.ThrowIfNull("bottomRightY");
			this.GeoBoundingBox(fieldName, "{0}, {1}".F(topLeftX, topLeftY), "{0}, {1}".F(bottomRightX, bottomRightY), Type);
		}

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
		public void GeoDistance(Expression<Func<T, object>> fieldDescriptor, Action<GeoDistanceFilterDescriptor> filterDescriptor)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.GeoDistance(field, filterDescriptor);
		}
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
		public void GeoDistanceRange(Expression<Func<T, object>> fieldDescriptor, Action<GeoDistanceRangeFilterDescriptor> filterDescriptor)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.GeoDistanceRange(field, filterDescriptor);
		}
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

		public void GeoPolygon(Expression<Func<T, object>> fieldDescriptor, IEnumerable<Tuple<double, double>> points)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.GeoPolygon(field, points);
		}
		public void GeoPolygon(string field, IEnumerable<Tuple<double, double>> points)
		{
			this.GeoPolygon(field, points.Select(p=> "{0}, {1}".F(p.Item1, p.Item2)).ToArray());
		}

		public void GeoPolygon(Expression<Func<T, object>> fieldDescriptor, params string[] points)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.GeoPolygon(field, points);
		}
		public void GeoPolygon(string fieldName, params string[] points)
		{
			var filter = new GeoPolygonFilter { Points = points };
			this.SetDictionary(fieldName, filter, d => this.GeoPolygonFilter = d);
		}

		public void Limit(int limit)
		{
			this.LimitFilter = new LimitFilter { Value = limit };
			this.SetCacheAndName(this.LimitFilter);
		}
		public void Type(string type)
		{
			type.ThrowIfNullOrEmpty("type");
			this.TypeFilter = new TypeFilter { Value = type };
			this.SetCacheAndName(this.TypeFilter);
		}
		public void MatchAll()
		{
			this.MatchAllFilter = new MatchAllFilter { };
			this.SetCacheAndName(this.MatchAllFilter);

		}
		public void NumericRange(Action<NumericRangeFilterDescriptor<T>> numericRangeSelector)
		{
			var filter = new NumericRangeFilterDescriptor<T>();
			numericRangeSelector(filter);
			this.SetDictionary(filter._Field, filter, (d) => this.NumericRangeFilter = d);
		}
		public void Range(Action<NumericRangeFilterDescriptor<T>> rangeSelector)
		{
			var filter = new NumericRangeFilterDescriptor<T>();
			rangeSelector(filter);
			this.SetDictionary(filter._Field, filter, (d) => this.RangeFilter = d);			
		}
		public void Script(Action<ScriptFilterDescriptor> scriptSelector)
		{
			var descriptor = new ScriptFilterDescriptor();
			scriptSelector(descriptor);
			this.SetCacheAndName(descriptor);
			this.ScriptFilter = descriptor;
		}
		public void Prefix(Expression<Func<T, object>> fieldDescriptor, string prefix)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.Prefix(field, prefix);
		}
		public void Prefix(string field, string prefix)
		{
			this.SetDictionary(field, prefix, (d) => this.PrefixFilter = d);
		}
		public void Term(Expression<Func<T, object>> fieldDescriptor, string term)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.Term(field, term);
		}
		public void Term(string field, string term)
		{
			this.SetDictionary(field, term, (d) => this.TermFilter = d);
		}
		public void Terms(Expression<Func<T, object>> fieldDescriptor, IEnumerable<string> terms, TermsExecution? Execution = null)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.Terms(field, terms, Execution);
		}
		public void Terms(string field, IEnumerable<string> terms, TermsExecution? Execution = null)
		{
			this.SetDictionary(field, terms, (d) => {
				if (Execution.HasValue)
					d.Add("execution", Enum.GetName(typeof(TermsExecution), Execution));
				this.TermsFilter = d;
			});
		}

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
		public void Not(Action<FilterDescriptor<T>> selector)
		{
			var filter = new FilterDescriptor<T>();
			selector(filter);
			this.SetDictionary("filter", filter, (d) => this.NotFilter = d);
		}

		public void Bool(Action<BoolFilterDescriptor<T>> booleanFilter)
		{
			var filter = new BoolFilterDescriptor<T>();
			booleanFilter(filter);
			this.SetCacheAndName(filter);
			this.BoolFilter = filter;

		}

		public void Query(Action<QueryDescriptor<T>> querySelector)
		{
			var descriptor = new QueryDescriptor<T>();
			querySelector(descriptor);
			this.SetDictionary("query", descriptor, (d) => this.QueryFilter = d);
		}


	}
}
