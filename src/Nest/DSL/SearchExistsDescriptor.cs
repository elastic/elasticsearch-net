using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ISearchExistsRequest : IQueryPath<SearchExistsRequestParameters>
	{
		[JsonProperty(PropertyName = "query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryContainer>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }

		[JsonIgnore]
		string QueryString { get; set; }
	}
	public interface ISearchExistsRequest<T> : ISearchExistsRequest { }

	internal static class SearchExistsPathInfo
	{
		public static void Update(ElasticsearchPathInfo<SearchExistsRequestParameters> pathInfo, ISearchExistsRequest request)
		{
			if (request.RequestParameters.ContainsKey("source") || request.RequestParameters.ContainsKey("q"))
				pathInfo.HttpMethod = PathInfoHttpMethod.GET;
			else
				pathInfo.HttpMethod = request.Query != null ? PathInfoHttpMethod.POST : PathInfoHttpMethod.GET;
		}
	}
	
	public partial class SearchExistsRequest : QueryPathBase<SearchExistsRequestParameters>, ISearchExistsRequest
	{
		public IQueryContainer Query { get; set; }

		public string QueryString { get; set; }

		protected SearchExistsRequest() : base() { }

		protected SearchExistsRequest(IndexNameMarker index, TypeNameMarker type = null)
			: base(index, type) { }


		protected SearchExistsRequest(IEnumerable<IndexNameMarker> indices, IEnumerable<TypeNameMarker> types = null)
			: base(indices, types) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SearchExistsRequestParameters> pathInfo)
		{
			SearchExistsPathInfo.Update(pathInfo, this);
		}

	}

	public partial class SearchExistsRequest<T> : QueryPathBase<SearchExistsRequestParameters, T>, ISearchExistsRequest<T>
		where T : class
	{
		public IQueryContainer Query { get; set; }

		public string QueryString { get; set; }

		protected SearchExistsRequest() : base() { }

		protected SearchExistsRequest(IndexNameMarker index, TypeNameMarker type = null)
			: base(index, type) { }

		protected SearchExistsRequest(IEnumerable<IndexNameMarker> indices, IEnumerable<TypeNameMarker> types = null)
			: base(indices, types) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SearchExistsRequestParameters> pathInfo)
		{
			SearchExistsPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("IndicesExists")]
	public partial class SearchExistsDescriptor<T> : QueryPathDescriptorBase<SearchExistsDescriptor<T>, SearchExistsRequestParameters, T>, ISearchExistsRequest
		where T : class
	{
		private ISearchExistsRequest Self { get { return this; } }

		IQueryContainer ISearchExistsRequest.Query { get; set; }

		string ISearchExistsRequest.QueryString { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SearchExistsRequestParameters> pathInfo)
		{
			SearchExistsPathInfo.Update(pathInfo, this);
		}

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
		public SearchExistsDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> query)
		{
			query.ThrowIfNull("query");
			var q = new QueryDescriptor<T>();
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
			Self.Query = new QueryDescriptor<T>().Raw(rawQuery);
			return this;
		}

	}
}
