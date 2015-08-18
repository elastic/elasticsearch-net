using Elasticsearch.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	internal static class SearchTemplatePathInfo
	{
		public static void Update(ElasticsearchPathInfo<SearchTemplateRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = HttpMethod.POST;
		}

		/// <summary>
		/// Based on the type information present in this descriptor create method that takes
		/// the returned _source and hit and returns the ClrType it should deserialize too.
		/// This is so that Documents[A] can contain actual instances of subclasses B, C as well.
		/// If you specify types using .Types(typeof(B), typeof(C)) then NEST can automagically
		/// create a TypeSelector based on the hits _type property.
		/// </summary>
		public static void CloseOverAutomagicCovariantResultSelector(ElasticInferrer infer, ISearchTemplateRequest self)
		{
			if (infer == null || self == null) return;
			var returnType = self.ClrType;

			if (returnType == null) return;

			var types = (self.Types ?? Enumerable.Empty<TypeName>()).Where(t => t.Type != null).ToList();
			if (self.TypeSelector != null || !types.HasAny(t => t.Type != returnType))
				return;

			var typeDictionary = types.ToDictionary(infer.TypeName, t => t.Type);
			self.TypeSelector = (o, h) =>
			{
				Type t;
				return !typeDictionary.TryGetValue(h.Type, out t) ? returnType : t;
			};
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ISearchTemplateRequest 
		: IQueryPath<SearchTemplateRequestParameters>
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

	public partial class SearchTemplateRequest 
		: QueryPathBase<SearchTemplateRequestParameters>, ISearchTemplateRequest
	{
		public string Template { get; set; }
		public string File { get; set; }
		public string Id { get; set; }
		public IDictionary<string, object> Params { get; set; }
		private Type _clrType { get; set; }
		Type ISearchTemplateRequest.ClrType { get { return _clrType; } }
		public Func<dynamic, Hit<dynamic>, Type> TypeSelector { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SearchTemplateRequestParameters> pathInfo)
		{
			SearchTemplatePathInfo.Update(pathInfo);
		}
	}
	
	public partial class SearchTemplateRequest<T> 
		: QueryPathBase<SearchTemplateRequestParameters, T>, ISearchTemplateRequest
		where T : class
	{
		public string Template { get; set; }
		public string File { get; set; }
		public string Id { get; set; }
		public IDictionary<string, object> Params { get; set; }
		public Type ClrType { get { return typeof(T); } }
		public Func<dynamic, Hit<dynamic>, Type> TypeSelector { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SearchTemplateRequestParameters> pathInfo)
		{
			SearchTemplatePathInfo.Update(pathInfo);
		}
	}

	public partial class SearchTemplateDescriptor<T> 
		: QueryPathDescriptorBase<SearchTemplateDescriptor<T>, SearchTemplateRequestParameters, T>, ISearchTemplateRequest<T>
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

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SearchTemplateRequestParameters> pathInfo)
		{
			SearchTemplatePathInfo.Update(pathInfo);
		}
	}
}
