using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ISearchTemplateRequest : IRequest<SearchTemplateRequestParameters>
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

	public interface ISearchTemplateRequest<T> : ISearchTemplateRequest { }

    public partial class SearchTemplateRequest : RequestBase<SearchTemplateRequestParameters>, ISearchTemplateRequest
    {
        public SearchTemplateRequest() { }
        public SearchTemplateRequest(Indices indices) : base(r => r.Optional(indices)) { }
        public SearchTemplateRequest(Indices indices, Types types) : base(r => r.Optional(indices).Optional(types)) { }

        public string Template { get; set; }
		public string File { get; set; }
		public string Id { get; set; }
		public IDictionary<string, object> Params { get; set; }
		private Type _clrType { get; set; }
		Type ISearchTemplateRequest.ClrType { get { return _clrType; } }
		public Func<dynamic, Hit<dynamic>, Type> TypeSelector { get; set; }
	}
	
	public partial class SearchTemplateRequest<T> : SearchTemplateRequest, ISearchTemplateRequest<T>
		where T : class
	{
        public SearchTemplateRequest() { }
        public SearchTemplateRequest(Indices indices) : base(indices) { }
        public SearchTemplateRequest(Indices indices, Types types) : base(indices, types) { }

		public Type ClrType { get { return typeof(T); } }
	}

	public partial class SearchTemplateDescriptor<T> : RequestDescriptorBase<SearchTemplateDescriptor<T>, SearchTemplateRequestParameters>, ISearchTemplateRequest<T>
		where T : class
	{
		ISearchTemplateRequest<T> Self => this;

		Type ISearchTemplateRequest.ClrType { get { return typeof(T); } }

		/// <summary>
		/// Whether conditionless queries are allowed or not
		/// </summary>
		internal bool _Strict { get; set; }

		string ISearchTemplateRequest.Template { get; set; }

		string ISearchTemplateRequest.File { get; set; }

		string ISearchTemplateRequest.Id { get; set; }

		IDictionary<string, object> ISearchTemplateRequest.Params { get; set; }

		Func<dynamic, Hit<dynamic>, Type> ISearchTemplateRequest.TypeSelector { get; set; }

		public SearchTemplateDescriptor<T> Template(string template)
		{
			this.Self.Template = template;
			return this;
		}

		public SearchTemplateDescriptor<T> File(string file)
		{
			this.Self.File = file;
			return this;
		}

		public SearchTemplateDescriptor<T> Id(string id)
		{
			this.Self.Id = id;
			return this;
		}

		public SearchTemplateDescriptor<T> Params(Dictionary<string, object> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			this.Self.Params = paramDictionary;
			return this;
		}

		public SearchTemplateDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			this.Self.Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}

		public SearchTemplateDescriptor<T> ConcreteTypeSelector(Func<dynamic, Hit<dynamic>, Type> typeSelector)
		{
			Self.TypeSelector = typeSelector;
			return this;
		}
	}
}
