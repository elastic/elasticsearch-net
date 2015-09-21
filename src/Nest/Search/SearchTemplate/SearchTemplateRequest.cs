using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	public partial interface ISearchTemplateRequest 
	{
		[JsonProperty("params")]
		IDictionary<string, object> Params { get; set; }

		Type ClrType { get; }

		[JsonProperty(PropertyName = "template")]
		string Template { get; set; }

		[JsonProperty("file")]
		string File { get; set; }

		[JsonProperty("id")]
		string Id { get; set; }

		Func<dynamic, Hit<dynamic>, Type> TypeSelector { get; set; }
	}

	public partial class SearchTemplateRequest 
	{
		public string Template { get; set; }
		public string File { get; set; }
		public string Id { get; set; }
		public IDictionary<string, object> Params { get; set; }
		private Type _clrType { get; set; }
		Type ISearchTemplateRequest.ClrType { get { return _clrType; } }
		public Func<dynamic, Hit<dynamic>, Type> TypeSelector { get; set; }
	}

	public class SearchTemplateRequest<T> : SearchTemplateRequest
		where T : class
	{
		public SearchTemplateRequest() { }
		public SearchTemplateRequest(Indices indices) : base(indices) { }
		public SearchTemplateRequest(Indices indices, Types types) : base(indices, types) { }

		public Type ClrType => typeof(T);
	}

	public partial class SearchTemplateDescriptor<T> where T : class
	{
		Type ISearchTemplateRequest.ClrType => typeof(T);

		/// <summary>
		/// Whether conditionless queries are allowed or not
		/// </summary>
		internal bool _Strict { get; set; }

		string ISearchTemplateRequest.Template { get; set; }
		public SearchTemplateDescriptor<T> Template(string template) => Assign(a => a.Template = template);

		string ISearchTemplateRequest.File { get; set; }
		public SearchTemplateDescriptor<T> File(string file) => Assign(a => a.File = file);

		string ISearchTemplateRequest.Id { get; set; }
		public SearchTemplateDescriptor<T> Id(string id) => Assign(a => a.Id = id);

		public SearchTemplateDescriptor<T> Params(Dictionary<string, object> paramDictionary) => Assign(a => a.Params = paramDictionary);

		IDictionary<string, object> ISearchTemplateRequest.Params { get; set; }
		public SearchTemplateDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary) =>
			Assign(a => a.Params = paramDictionary?.Invoke(new FluentDictionary<string, object>()));

		Func<dynamic, Hit<dynamic>, Type> ISearchTemplateRequest.TypeSelector { get; set; }
		public SearchTemplateDescriptor<T> ConcreteTypeSelector(Func<dynamic, Hit<dynamic>, Type> typeSelector) => Assign(a => a.TypeSelector = typeSelector);
	}
}
