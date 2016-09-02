using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface ISearchTemplateRequest : ICovariantSearchRequest
	{
		[JsonProperty("params")]
		IDictionary<string, object> Params { get; set; }

		[JsonProperty("inline")]
		string Inline { get; set; }

		[JsonProperty("file")]
		string File { get; set; }

		[JsonProperty("id")]
		string Id { get; set; }

		string Preference { get; }

		string Routing { get; }

		SearchType? SearchType { get; }

		bool? IgnoreUnavalable { get; }
	}

	public partial class SearchTemplateRequest
	{
		public string Inline { get; set; }
		public string File { get; set; }
		public string Id { get; set; }
		public IDictionary<string, object> Params { get; set; }
		protected Type ClrType { get; set; }
		public Func<dynamic, Hit<dynamic>, Type> TypeSelector { get; set; }
		Type ICovariantSearchRequest.ClrType => this.ClrType;
		Types ICovariantSearchRequest.ElasticsearchTypes => ((ISearchTemplateRequest)this).Type;

		SearchType? ISearchTemplateRequest.SearchType => RequestState.RequestParameters?.GetQueryStringValue<SearchType?>("search_type");

		string ISearchTemplateRequest.Preference => RequestState.RequestParameters?.GetQueryStringValue<string>("preference");

		string ISearchTemplateRequest.Routing => RequestState.RequestParameters?.GetQueryStringValue<string[]>("routing") == null
			? null
			: string.Join(",", RequestState.RequestParameters?.GetQueryStringValue<string[]>("routing"));

		bool? ISearchTemplateRequest.IgnoreUnavalable => RequestState.RequestParameters?.GetQueryStringValue<bool?>("ignore_unavailable");
	}

	public class SearchTemplateRequest<T> : SearchTemplateRequest
		where T : class
	{
		public SearchTemplateRequest(Indices indices) : base(indices) { this.ClrType = typeof(T); }
		public SearchTemplateRequest(Indices indices, Types types) : base(indices, types) { this.ClrType = typeof(T); }
	}

	public partial class SearchTemplateDescriptor<T> where T : class
	{
		Type ICovariantSearchRequest.ClrType => typeof(T);
		Types ICovariantSearchRequest.ElasticsearchTypes => ((ISearchTemplateRequest)this).Type;

		Func<dynamic, Hit<dynamic>, Type> ICovariantSearchRequest.TypeSelector { get; set; }
		SearchType? ISearchTemplateRequest.SearchType => RequestState.RequestParameters?.GetQueryStringValue<SearchType?>("search_type");

		string ISearchTemplateRequest.Preference => RequestState.RequestParameters?.GetQueryStringValue<string>("preference");

		string ISearchTemplateRequest.Routing => RequestState.RequestParameters?.GetQueryStringValue<string[]>("routing") == null
			? null
			: string.Join(",", RequestState.RequestParameters?.GetQueryStringValue<string[]>("routing"));

		bool? ISearchTemplateRequest.IgnoreUnavalable => RequestState.RequestParameters?.GetQueryStringValue<bool?>("ignore_unavailable");


		/// <summary>
		/// Whether conditionless queries are allowed or not
		/// </summary>
		internal bool _Strict { get; set; }

		string ISearchTemplateRequest.Inline { get; set; }
		public SearchTemplateDescriptor<T> Inline(string template) => Assign(a => a.Inline = template);

		string ISearchTemplateRequest.File { get; set; }
		public SearchTemplateDescriptor<T> File(string file) => Assign(a => a.File = file);

		string ISearchTemplateRequest.Id { get; set; }
		public SearchTemplateDescriptor<T> Id(string id) => Assign(a => a.Id = id);

		public SearchTemplateDescriptor<T> Params(Dictionary<string, object> paramDictionary) => Assign(a => a.Params = paramDictionary);

		IDictionary<string, object> ISearchTemplateRequest.Params { get; set; }
		public SearchTemplateDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary) =>
			Assign(a => a.Params = paramDictionary?.Invoke(new FluentDictionary<string, object>()));

		public SearchTemplateDescriptor<T> ConcreteTypeSelector(Func<dynamic, Hit<dynamic>, Type> typeSelector) => Assign(a => a.TypeSelector = typeSelector);
	}
}
