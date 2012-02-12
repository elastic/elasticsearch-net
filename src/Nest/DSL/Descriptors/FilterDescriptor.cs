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
		[JsonProperty(PropertyName = "exists")]
		internal ExistsFilter ExistsFilter { get; set; }

		[JsonProperty(PropertyName = "missing")]
		internal MissingFilter MissingFilter { get; set; }

		[JsonProperty(PropertyName = "ids")]
		internal IdsFilter IdsFilter { get; set; }

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

		[JsonProperty(PropertyName = "fquery")]
		internal Dictionary<string, object> QueryFilter { get; set; }

		[JsonProperty(PropertyName = "script")]
		internal ScriptFilterDescriptor ScriptFilter { get; set; }

		public FilterDescriptor()
		{
			
		}

		public FilterDescriptor<T> Exists(Expression<Func<T, object>> fieldDescriptor)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			return this.Exists(field);
		}
		public FilterDescriptor<T> Exists(string field)
		{
			this.ExistsFilter = new ExistsFilter { Field = field };
			return this;
		}
		public FilterDescriptor<T> Missing(Expression<Func<T, object>> fieldDescriptor)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			return this.Missing(field);
		}
		public FilterDescriptor<T> Missing(string field)
		{
			this.MissingFilter = new MissingFilter { Field = field };
			return this;
		}


		public FilterDescriptor<T> Ids(IEnumerable<string> values)
		{
			this.IdsFilter = new IdsFilter { Values = values };
			return this;
		}
		public FilterDescriptor<T> Ids(string type, IEnumerable<string> values)
		{
			type.ThrowIfNullOrEmpty("type");
			this.IdsFilter = new IdsFilter { Values = values, Type = new []{ type } };
			return this;
		}
		public FilterDescriptor<T> Ids(IEnumerable<string> types, IEnumerable<string> values)
		{
			this.IdsFilter = new IdsFilter { Values = values, Type = types };
			return this;
		}

		public FilterDescriptor<T> Limit(int limit)
		{
			this.LimitFilter = new LimitFilter { Value = limit };
			return this;
		}
		public FilterDescriptor<T> Type(string type)
		{
			type.ThrowIfNullOrEmpty("type");
			this.TypeFilter = new TypeFilter { Value = type };
			return this;
		}
		public FilterDescriptor<T> MatchAll()
		{
			this.MatchAllFilter = new MatchAllFilter { };
			return this;
		}
		public FilterDescriptor<T> NumericRange(Func<NumericRangeFilterDescriptor<T>, NumericRangeFilterDescriptor<T>> numericRangeSelector)
		{
			var descriptor = new NumericRangeFilterDescriptor<T>();
			var filter = numericRangeSelector(descriptor);
			this.NumericRangeFilter = new Dictionary<string, object>();
			this.NumericRangeFilter.Add(filter._Field, filter);
			if (filter._Cache.HasValue)
				this.NumericRangeFilter.Add("_cache", filter._Cache);
			return this;
		}
		public FilterDescriptor<T> Range(Func<NumericRangeFilterDescriptor<T>, NumericRangeFilterDescriptor<T>> rangeSelector)
		{
			var descriptor = new NumericRangeFilterDescriptor<T>();
			var filter = rangeSelector(descriptor);
			this.RangeFilter = new Dictionary<string, object>();
			this.RangeFilter.Add(filter._Field, filter);
			if (filter._Cache.HasValue)
				this.RangeFilter.Add("_cache", filter._Cache);
			return this;
		}
		public FilterDescriptor<T> Script(Func<ScriptFilterDescriptor, ScriptFilterDescriptor> scriptSelector)
		{
			var descriptor = new ScriptFilterDescriptor();
			var script = scriptSelector(descriptor);
			this.ScriptFilter = script;
			return this;
		}
		public FilterDescriptor<T> Prefix(Expression<Func<T, object>> fieldDescriptor, string prefix, bool? Cache = null)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			return this.Prefix(field, prefix, Cache);
		}
		public FilterDescriptor<T> Prefix(string field, string prefix, bool? Cache = null)
		{
			this.PrefixFilter = new Dictionary<string, object>();
			this.PrefixFilter.Add(field, prefix);
			if (Cache.HasValue)
				this.PrefixFilter.Add("_cache", Cache);
			return this;
		}
		public FilterDescriptor<T> Term(Expression<Func<T, object>> fieldDescriptor, string prefix, bool? Cache = null)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			return this.Term(field, prefix, Cache);
		}
		public FilterDescriptor<T> Term(string field, string prefix, bool? Cache = null)
		{
			this.TermFilter = new Dictionary<string, object>();
			this.TermFilter.Add(field, prefix);
			if (Cache.HasValue)
				this.TermFilter.Add("_cache", Cache);
			return this;
		}

		public FilterDescriptor<T> Query(Action<QueryDescriptor<T>> querySelector, bool? Cache = null)
		{
			var descriptor = new QueryDescriptor<T>();
			querySelector(descriptor);

			this.QueryFilter = new Dictionary<string, object>();
			this.QueryFilter.Add("query", descriptor);
			if (Cache.HasValue)
				this.QueryFilter.Add("_cache", Cache);
			return this;
		}


	}
}
