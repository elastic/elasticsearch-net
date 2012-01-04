using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using ElasticSearch.Client.DSL;
using Newtonsoft.Json.Converters;
using ElasticSearch.Client.Resolvers.Converters;
using System.Linq.Expressions;

namespace ElasticSearch.Client
{
	public class QueryDescriptor<T> where T : class
	{
		[JsonProperty(PropertyName = "match_all")]
		internal MatchAll MatchAllQuery { get; set; }
		[JsonProperty(PropertyName = "term")]
		internal Term TermQuery { get; set; }
		[JsonProperty(PropertyName = "wildcard")]
		internal Wildcard WildcardQuery { get; set; }

		private void ThrowIfNoSlotEmpty(string tried)
		{
			var filled = string.Empty;
			Action<object, string> slot = (o, s) =>
			{
				if (o != null && tried != s)
					filled = s;
			};
			slot(this.MatchAllQuery, "match_all");
			slot(this.TermQuery, "term");
			slot(this.WildcardQuery, "wildcard");
			if (!string.IsNullOrEmpty(filled))
			{
				var message = "Tried to set a {0} query while the descriptor already contains a {1} query".F(tried, filled);
				throw new DslException(message);
			}
		}

		public QueryDescriptor()
		{
			
		}

		public QueryDescriptor<T> MatchAll(double? Boost = null, string NormField = null)
		{
			this.ThrowIfNoSlotEmpty("match_all");

			this.MatchAllQuery = new MatchAll() { NormField = NormField };
			if (Boost.HasValue)
				this.MatchAllQuery.Boost = Boost.Value;

			return this;
		}
		public QueryDescriptor<T> Term(Expression<Func<T, object>> fieldDescriptor
			, string value
			, double? Boost = null)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			return this.Term(field, value, Boost: Boost);
		}
		public QueryDescriptor<T> Term(string field, string value, double? Boost = null)
		{
			this.ThrowIfNoSlotEmpty("term");
			var term = new Term() { Field = field, Value = value };
			if (Boost.HasValue)
				term.Boost = Boost;
			this.TermQuery = term;
			return this;
		}
		public QueryDescriptor<T> Wildcard(Expression<Func<T, object>> fieldDescriptor
			, string value
			, double? Boost = null)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			return this.Wildcard(field, value, Boost: Boost);
		}
		public QueryDescriptor<T> Wildcard(string field, string value, double? Boost = null)
		{
			this.ThrowIfNoSlotEmpty("wildcard");
			var wildcard = new Wildcard() { Field = field, Value = value };
			if (Boost.HasValue)
				wildcard.Boost = Boost;
			this.WildcardQuery = wildcard;
			return this;
		}
	}
}
