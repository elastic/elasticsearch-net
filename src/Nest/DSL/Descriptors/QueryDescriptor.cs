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
	public class QueryDescriptor<T> where T : class
	{
		[JsonProperty(PropertyName = "match_all")]
		internal MatchAll MatchAllQuery { get; set; }
		[JsonProperty(PropertyName = "term")]
		internal Term TermQuery { get; set; }
		[JsonProperty(PropertyName = "wildcard")]
		internal Wildcard WildcardQuery { get; set; }
		[JsonProperty(PropertyName = "prefix")]
		internal Prefix PrefixQuery { get; set; }
		[JsonProperty(PropertyName = "bool")]
		internal BoolQueryDescriptor<T> BoolQueryDescriptor { get; set; }
		[JsonProperty(PropertyName = "boosting")]
		internal BoostingQueryDescriptor<T> BoostingQueryDescriptor { get; set; }
		[JsonProperty(PropertyName = "ids")]
		internal IdsQuery IdsQuery { get; set; }
		[JsonProperty(PropertyName = "custom_score")]
		internal CustomScoreQueryDescriptor<T> CustomScoreQueryDescriptor { get; set; }
		[JsonProperty(PropertyName = "custom_boost_factor")]
		internal CustomBoostFactorQueryDescriptor<T> CustomBoostFactorQueryDescriptor { get; set; }
		[JsonProperty(PropertyName = "constant_score")]
		internal ConstantScoreQueryDescriptor<T> ConstantScoreQueryDescriptor { get; set; }
		[JsonProperty(PropertyName = "dis_max")]
		internal DismaxQueryDescriptor<T> DismaxQueryDescriptor { get; set; }
		[JsonProperty(PropertyName = "filtered")]
		internal FilteredQueryDescriptor<T> FilteredQueryDescriptor { get; set; }

		public QueryDescriptor()
		{
			
		}
		public void Filtered(Action<FilteredQueryDescriptor<T>> selector)
		{
			var query = new FilteredQueryDescriptor<T>();
			selector(query);
			this.FilteredQueryDescriptor = query;
		}
		public void Dismax(Action<DismaxQueryDescriptor<T>> selector)
		{
			var query = new DismaxQueryDescriptor<T>();
			selector(query);
			this.DismaxQueryDescriptor = query;
		}
		public void ConstantScore(Action<ConstantScoreQueryDescriptor<T>> selector)
		{
			var query = new ConstantScoreQueryDescriptor<T>();
			selector(query);
			this.ConstantScoreQueryDescriptor = query;
		}
		public void CustomBoostFactor(Action<CustomBoostFactorQueryDescriptor<T>> selector)
		{
			var query = new CustomBoostFactorQueryDescriptor<T>();
			selector(query);
			this.CustomBoostFactorQueryDescriptor = query;
		}
		public void CustomScore(Action<CustomScoreQueryDescriptor<T>> customScoreQuery)
		{
			var query = new CustomScoreQueryDescriptor<T>();
			customScoreQuery(query);
			this.CustomScoreQueryDescriptor = query;
		}
		public void Bool(Action<BoolQueryDescriptor<T>> booleanQuery)
		{
			var query = new BoolQueryDescriptor<T>();
			booleanQuery(query);
			this.BoolQueryDescriptor = query;
		}
		public void Boosting(Action<BoostingQueryDescriptor<T>> boostingQuery)
		{
			var query = new BoostingQueryDescriptor<T>();
			boostingQuery(query);
			this.BoostingQueryDescriptor = query;
		}
		public void MatchAll(double? Boost = null, string NormField = null)
		{
			this.MatchAllQuery = new MatchAll() { NormField = NormField };
			if (Boost.HasValue)
				this.MatchAllQuery.Boost = Boost.Value;
		}
		public void Term(Expression<Func<T, object>> fieldDescriptor
			, string value
			, double? Boost = null)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.Term(field, value, Boost: Boost);
		}
		public void Term(string field, string value, double? Boost = null)
		{
			var term = new Term() { Field = field, Value = value };
			if (Boost.HasValue)
				term.Boost = Boost;
			this.TermQuery = term;
		}
		public void Wildcard(Expression<Func<T, object>> fieldDescriptor
			, string value
			, double? Boost = null)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.Wildcard(field, value, Boost: Boost);
		}
		public void Wildcard(string field, string value, double? Boost = null)
		{
			var wildcard = new Wildcard() { Field = field, Value = value };
			if (Boost.HasValue)
				wildcard.Boost = Boost;
			this.WildcardQuery = wildcard;
		}
		public void Prefix(Expression<Func<T, object>> fieldDescriptor
			, string value
			, double? Boost = null)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			this.Prefix(field, value, Boost: Boost);
		}
		public void Prefix(string field, string value, double? Boost = null)
		{
			var prefix = new Prefix() { Field = field, Value = value };
			if (Boost.HasValue)
				prefix.Boost = Boost;
			this.PrefixQuery = prefix;
		}
		public void Ids(IEnumerable<string> values)
		{
			this.IdsQuery = new IdsQuery { Values = values };
		}
		public void Ids(string type, IEnumerable<string> values)
		{
			type.ThrowIfNullOrEmpty("type");
			this.IdsQuery = new IdsQuery { Values = values, Type = new[] { type } };
		}
		public void Ids(IEnumerable<string> types, IEnumerable<string> values)
		{
			this.IdsQuery = new IdsQuery { Values = values, Type = types };
		}
	}
}
