using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface ISearchTemplateRequest : ICovariantSearchRequest
	{
		[Obsolete("Removed in NEST 6.x.")]
		[JsonProperty("file")]
		string File { get; set; }

		[JsonProperty("id")]
		string Id { get; set; }

		bool? IgnoreUnavalable { get; }

		[JsonProperty("inline")]
		string Inline { get; set; }

		[JsonProperty("params")]
		IDictionary<string, object> Params { get; set; }

		string Preference { get; }

		string Routing { get; }

		SearchType? SearchType { get; }
	}

	public partial class SearchTemplateRequest
	{
		[Obsolete("Removed in NEST 6.x.")]
		public string File { get; set; }

		public string Id { get; set; }
		public string Inline { get; set; }
		public IDictionary<string, object> Params { get; set; }
		public Func<dynamic, Hit<dynamic>, Type> TypeSelector { get; set; }
		protected Type ClrType { get; set; }
		Type ICovariantSearchRequest.ClrType => ClrType;
		Types ICovariantSearchRequest.ElasticsearchTypes => ((ISearchTemplateRequest)this).Type;

		bool? ISearchTemplateRequest.IgnoreUnavalable => RequestState.RequestParameters?.GetQueryStringValue<bool?>("ignore_unavailable");

		string ISearchTemplateRequest.Preference => RequestState.RequestParameters?.GetQueryStringValue<string>("preference");

		string ISearchTemplateRequest.Routing => RequestState.RequestParameters?.GetQueryStringValue<string[]>("routing") == null
			? null
			: string.Join(",", RequestState.RequestParameters?.GetQueryStringValue<string[]>("routing"));

		SearchType? ISearchTemplateRequest.SearchType => RequestState.RequestParameters?.GetQueryStringValue<SearchType?>("search_type");
	}

	public class SearchTemplateRequest<T> : SearchTemplateRequest
		where T : class
	{
		public SearchTemplateRequest(Indices indices) : base(indices) => ClrType = typeof(T);

		public SearchTemplateRequest(Indices indices, Types types) : base(indices, types) => ClrType = typeof(T);
	}

	public partial class SearchTemplateDescriptor<T> where T : class
	{
		/// <summary>
		/// Whether conditionless queries are allowed or not
		/// </summary>
		internal bool _Strict { get; set; }

		Type ICovariantSearchRequest.ClrType => typeof(T);
		Types ICovariantSearchRequest.ElasticsearchTypes => ((ISearchTemplateRequest)this).Type;


		[Obsolete("Removed in NEST 6.x.")]
		string ISearchTemplateRequest.File { get; set; }

		string ISearchTemplateRequest.Id { get; set; }

		bool? ISearchTemplateRequest.IgnoreUnavalable => RequestState.RequestParameters?.GetQueryStringValue<bool?>("ignore_unavailable");

		string ISearchTemplateRequest.Inline { get; set; }

		IDictionary<string, object> ISearchTemplateRequest.Params { get; set; }

		string ISearchTemplateRequest.Preference => RequestState.RequestParameters?.GetQueryStringValue<string>("preference");

		string ISearchTemplateRequest.Routing => RequestState.RequestParameters?.GetQueryStringValue<string[]>("routing") == null
			? null
			: string.Join(",", RequestState.RequestParameters?.GetQueryStringValue<string[]>("routing"));

		SearchType? ISearchTemplateRequest.SearchType => RequestState.RequestParameters?.GetQueryStringValue<SearchType?>("search_type");

		Func<dynamic, Hit<dynamic>, Type> ICovariantSearchRequest.TypeSelector { get; set; }

		public SearchTemplateDescriptor<T> Inline(string template) => Assign(a => a.Inline = template);

		[Obsolete("Removed in NEST 6.x.")]
		public SearchTemplateDescriptor<T> File(string file) => Assign(a => a.File = file);

		public SearchTemplateDescriptor<T> Id(string id) => Assign(a => a.Id = id);

		public SearchTemplateDescriptor<T> Params(Dictionary<string, object> paramDictionary) => Assign(a => a.Params = paramDictionary);

		public SearchTemplateDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary) =>
			Assign(a => a.Params = paramDictionary?.Invoke(new FluentDictionary<string, object>()));

		public SearchTemplateDescriptor<T> ConcreteTypeSelector(Func<dynamic, Hit<dynamic>, Type> typeSelector) =>
			Assign(a => a.TypeSelector = typeSelector);
	}
}
