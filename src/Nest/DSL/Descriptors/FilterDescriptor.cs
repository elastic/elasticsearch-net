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

		[JsonProperty(PropertyName = "script")]
		internal ScriptFilterDescriptor ScriptFilter { get; set; }

		public FilterDescriptor()
		{
			
		}

		public void Exists(Expression<Func<T, object>> fieldDescriptor)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.Exists(field);
		}
		public void Exists(string field)
		{
			this.ExistsFilter = new ExistsFilter { Field = field };
			if (this._Cache.HasValue)
				this.ExistsFilter._Cache = this._Cache;
			if (!string.IsNullOrWhiteSpace(this._Name))
				this.ExistsFilter._Name = this._Name;
		}
		public void Missing(Expression<Func<T, object>> fieldDescriptor)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.Missing(field);
		}
		public void Missing(string field)
		{
			this.MissingFilter = new MissingFilter { Field = field };
			if (this._Cache.HasValue)
				this.MissingFilter._Cache = this._Cache;
			if (!string.IsNullOrWhiteSpace(this._Name))
				this.MissingFilter._Name = this._Name;
		}
		public void Ids(IEnumerable<string> values)
		{
			this.IdsFilter = new IdsFilter { Values = values };
			if (this._Cache.HasValue)
				this.IdsFilter._Cache = this._Cache;
			if (!string.IsNullOrWhiteSpace(this._Name))
				this.IdsFilter._Name = this._Name;
		}
		public void Ids(string type, IEnumerable<string> values)
		{
			type.ThrowIfNullOrEmpty("type");
			this.IdsFilter = new IdsFilter { Values = values, Type = new []{ type } };
			if (this._Cache.HasValue)
				this.IdsFilter._Cache = this._Cache;
			if (!string.IsNullOrWhiteSpace(this._Name))
				this.IdsFilter._Name = this._Name;
		}
		public void Ids(IEnumerable<string> types, IEnumerable<string> values)
		{
			this.IdsFilter = new IdsFilter { Values = values, Type = types };
			if (this._Cache.HasValue)
				this.IdsFilter._Cache = this._Cache;
			if (!string.IsNullOrWhiteSpace(this._Name))
				this.IdsFilter._Name = this._Name;
		}

		public void Limit(int limit)
		{
			this.LimitFilter = new LimitFilter { Value = limit };
			if (this._Cache.HasValue)
				this.LimitFilter._Cache = this._Cache;
			if (!string.IsNullOrWhiteSpace(this._Name))
				this.LimitFilter._Name = this._Name;
		}
		public void Type(string type)
		{
			type.ThrowIfNullOrEmpty("type");
			this.TypeFilter = new TypeFilter { Value = type };
			if (this._Cache.HasValue)
				this.TypeFilter._Cache = this._Cache;
			if (!string.IsNullOrWhiteSpace(this._Name))
				this.TypeFilter._Name = this._Name;
		}
		public void MatchAll()
		{
			this.MatchAllFilter = new MatchAllFilter { };
			if (this._Cache.HasValue)
				this.MatchAllFilter._Cache = this._Cache;
			if (!string.IsNullOrWhiteSpace(this._Name))
				this.MatchAllFilter._Name = this._Name;

		}
		public void NumericRange(Action<NumericRangeFilterDescriptor<T>> numericRangeSelector)
		{
			var filter = new NumericRangeFilterDescriptor<T>();
			numericRangeSelector(filter);
			this.NumericRangeFilter = new Dictionary<string, object>();
			this.NumericRangeFilter.Add(filter._Field, filter);
			if (this._Cache.HasValue)
				this.NumericRangeFilter.Add("_cache", this._Cache);
			if (!string.IsNullOrWhiteSpace(this._Name))
				this.NumericRangeFilter.Add("_name", this._Name);
		}
		public void Range(Action<NumericRangeFilterDescriptor<T>> rangeSelector)
		{
			var filter = new NumericRangeFilterDescriptor<T>();
			rangeSelector(filter);
			this.RangeFilter = new Dictionary<string, object>();
			this.RangeFilter.Add(filter._Field, filter);
			if (this._Cache.HasValue)
				this.RangeFilter.Add("_cache", this._Cache);
			if (!string.IsNullOrWhiteSpace(this._Name))
				this.RangeFilter.Add("_name", this._Name);
		}
		public void Script(Action<ScriptFilterDescriptor> scriptSelector)
		{
			var descriptor = new ScriptFilterDescriptor();
			scriptSelector(descriptor);
			this.ScriptFilter = descriptor;
			if (this._Cache.HasValue)
				this.ScriptFilter._Cache = this._Cache;
			if (!string.IsNullOrWhiteSpace(this._Name))
				this.ScriptFilter._Name = this._Name;
		}
		public void Prefix(Expression<Func<T, object>> fieldDescriptor, string prefix)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.Prefix(field, prefix);
		}
		public void Prefix(string field, string prefix)
		{
			this.PrefixFilter = new Dictionary<string, object>();
			this.PrefixFilter.Add(field, prefix);
			if (this._Cache.HasValue)
				this.PrefixFilter.Add("_cache", this._Cache);
			if (!string.IsNullOrWhiteSpace(this._Name))
				this.PrefixFilter.Add("_name", this._Name);
		}
		public void Term(Expression<Func<T, object>> fieldDescriptor, string term)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.Term(field, term);
		}
		public void Term(string field, string term)
		{
			this.TermFilter = new Dictionary<string, object>();
			this.TermFilter.Add(field, term);
			if (this._Cache.HasValue)
				this.TermFilter.Add("_cache", this._Cache);
			if (!string.IsNullOrWhiteSpace(this._Name))
				this.TermFilter.Add("_name", this._Name);
		}
		public void Terms(Expression<Func<T, object>> fieldDescriptor, IEnumerable<string> terms, TermsExecution? Execution = null)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.Terms(field, terms, Execution);
		}
		public void Terms(string field, IEnumerable<string> terms, TermsExecution? Execution = null)
		{
			this.TermsFilter = new Dictionary<string, object>();
			this.TermsFilter.Add(field, terms);
			if (Execution.HasValue)
				this.TermsFilter.Add("execution", Enum.GetName(typeof(TermsExecution), Execution));
			if (this._Cache.HasValue)
				this.TermsFilter.Add("_cache", this._Cache);
			if (!string.IsNullOrWhiteSpace(this._Name))
				this.TermsFilter.Add("_name", this._Name);
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
			this.AndFilter = new Dictionary<string,object>();
			this.AndFilter.Add("filters", descriptors);

			if (this._Cache.HasValue)
				this.AndFilter.Add("_cache", this._Cache);
			if (!string.IsNullOrWhiteSpace(this._Name))
				this.AndFilter.Add("_name", this._Name);
		}


		public void Query(Action<QueryDescriptor<T>> querySelector)
		{
			var descriptor = new QueryDescriptor<T>();
			querySelector(descriptor);

			this.QueryFilter = new Dictionary<string, object>();
			this.QueryFilter.Add("query", descriptor);
			if (this._Cache.HasValue)
				this.QueryFilter.Add("_cache", this._Cache);
			if (!string.IsNullOrWhiteSpace(this._Name))
				this.QueryFilter.Add("_name", this._Name);

		}


	}
}
