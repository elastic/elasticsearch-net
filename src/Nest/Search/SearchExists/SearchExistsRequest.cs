using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface ISearchExistsRequest 
	{
		[JsonProperty(PropertyName = "query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeJsonConverter<QueryContainer>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }

		[JsonIgnore]
		string QueryString { get; set; }
	}
	//TODO if querystring has source || q || this.Query == null do a GET otherwise POST

	public partial class SearchExistsRequest 
	{
		public IQueryContainer Query { get; set; }

		public string QueryString { get; set; }
	}

	//TODO removed typed request variant for now

	[DescriptorFor("IndicesExists")]
	public partial class SearchExistsDescriptor<T> where T : class
	{
		private ISearchExistsRequest Self => this;

		IQueryContainer ISearchExistsRequest.Query { get; set; }

		string ISearchExistsRequest.QueryString { get; set; }

		internal bool _Strict { get; set; }
		
		/// <summary>
		/// When strict is set, conditionless queries are treated as an exception. 
		/// </summary>
		public SearchExistsDescriptor<T> Strict(bool strict = true)
		{
			this._Strict = strict;
			return this;
		}

		/// <summary>
		/// Describe the query to perform using a query descriptor lambda
		/// </summary>
		public SearchExistsDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> query)
		{
			query.ThrowIfNull("query");
			var q = new QueryContainerDescriptor<T>();
			((IQueryContainer)q).IsStrict = this._Strict;
			var bq = query(q);
			return this.Query(bq);
		}

		public SearchExistsDescriptor<T> Query(QueryContainer query)
		{
			return this.Query((IQueryContainer)query);
		}

		/// <summary>
		/// Describe the query to perform using the static Query class
		/// </summary>
		public SearchExistsDescriptor<T> Query(IQueryContainer query)
		{
			if (query == null)
				return this;

			if (this._Strict && query.IsConditionless)
				throw new DslException("Query resulted in a conditionless query:\n{0}".F(JsonConvert.SerializeObject(query, Formatting.Indented)));

			else if (query.IsConditionless)
				return this;
			Self.Query = query;
			return this;

		}

		/// <summary>
		/// Provide a query over the query string under key q. this force the request to be a GET
		/// </summary>
		public SearchExistsDescriptor<T> QueryString(string query)
		{
			Self.RequestParameters.AddQueryString("q", query);
			return this;
		}

		/// <summary>
		/// Describe the query to perform as a raw json string
		/// </summary>
		public SearchExistsDescriptor<T> QueryRaw(string rawQuery)
		{
			Self.Query = new QueryContainerDescriptor<T>().Raw(rawQuery);
			return this;
		}

	}
}
