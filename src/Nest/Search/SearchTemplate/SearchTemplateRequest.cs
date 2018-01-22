using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SearchTemplateRequest>))]
	public partial interface ISearchTemplateRequest : ICovariantSearchRequest
	{
		[JsonProperty("params")]
		IDictionary<string, object> Params { get; set; }

		[JsonProperty("source")]
		string Source { get; set; }

		[Obsolete("Inline is being deprecated for Source and will be removed in Elasticsearch 7.0")]
		[JsonIgnore]
		string Inline { get; set; }

		[JsonProperty("id")]
		string Id { get; set; }
	}

	public partial class SearchTemplateRequest
	{
		protected sealed override void Initialize() => this.TypedKeys = true;

		public string Source { get; set; }
		[Obsolete("Inline is being deprecated for Source and will be removed in Elasticsearch 7.0")]
		public string Inline { get => this.Source; set => this.Source = value; }
		public string Id { get; set; }
		public IDictionary<string, object> Params { get; set; }
		protected Type ClrType { get; set; }
		public Func<dynamic, Hit<dynamic>, Type> TypeSelector { get; set; }
		Type ICovariantSearchRequest.ClrType => this.ClrType;

	}

	public class SearchTemplateRequest<T> : SearchTemplateRequest
		where T : class
	{
		public SearchTemplateRequest() : base(typeof(T)) { this.ClrType = typeof(T); }
		public SearchTemplateRequest(Indices indices) : base(indices) { this.ClrType = typeof(T); }
		public SearchTemplateRequest(Indices indices, Types types) : base(indices, types) { this.ClrType = typeof(T); }
	}

	public partial class SearchTemplateDescriptor<T> where T : class
	{
		protected sealed override void Initialize() => this.TypedKeys();

		Type ICovariantSearchRequest.ClrType => typeof(T);

		/// <summary>
		/// Whether conditionless queries are allowed or not
		/// </summary>
		internal bool _Strict { get; set; }

		string ISearchTemplateRequest.Inline { get => Self.Source; set => Self.Source = value; }
		[Obsolete("Inline is being deprecated for Source and will be removed in Elasticsearch 7.0")]
		public SearchTemplateDescriptor<T> Inline(string template) => Assign(a => a.Inline = template);

		string ISearchTemplateRequest.Source { get; set; }
		public SearchTemplateDescriptor<T> Source(string template) => Assign(a => a.Source = template);

		string ISearchTemplateRequest.Id { get; set; }
		public SearchTemplateDescriptor<T> Id(string id) => Assign(a => a.Id = id);

		public SearchTemplateDescriptor<T> Params(Dictionary<string, object> paramDictionary) => Assign(a => a.Params = paramDictionary);

		IDictionary<string, object> ISearchTemplateRequest.Params { get; set; }
		public SearchTemplateDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary) =>
			Assign(a => a.Params = paramDictionary?.Invoke(new FluentDictionary<string, object>()));
	}
}
