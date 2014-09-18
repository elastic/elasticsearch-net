using Elasticsearch.Net;
using Nest.DSL.Descriptors;
using Nest.Resolvers.Converters;
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
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
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

			var types = (self.Types ?? Enumerable.Empty<TypeNameMarker>()).Where(t => t.Type != null).ToList();
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

		[JsonProperty(PropertyName = "sort")]
		[JsonConverter(typeof(SortCollectionConverter))]
		IList<KeyValuePair<PropertyPathMarker, ISort>> Sort { get; set; }

		[JsonProperty(PropertyName = "template")]
		SearchTemplate Template { get; set; }

		Func<dynamic, Hit<dynamic>, Type> TypeSelector { get; set; }
	}

	public interface ISearchTemplateRequest<T> : ISearchTemplateRequest { }

	public partial class SearchTemplateRequest 
		: QueryPathBase<SearchTemplateRequestParameters>, ISearchTemplateRequest
	{
		public SearchTemplate Template { get; set; }
		public IDictionary<string, object> Params { get; set; }
		private Type _clrType { get; set; }
		Type ISearchTemplateRequest.ClrType { get { return _clrType; } }
		public IList<KeyValuePair<PropertyPathMarker, ISort>> Sort { get; set; }
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
		public SearchTemplate Template { get; set; }
		public IDictionary<string, object> Params { get; set; }
		public Type ClrType { get { return typeof(T); } }
		public IList<KeyValuePair<PropertyPathMarker, ISort>> Sort { get; set; }
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
		ISearchTemplateRequest<T> Self { get { return this; } }

		Type ISearchTemplateRequest.ClrType { get { return typeof(T); } }

		/// <summary>
		/// Whether conditionless queries are allowed or not
		/// </summary>
		internal bool _Strict { get; set; }

		SearchTemplate ISearchTemplateRequest.Template { get; set; }

		IDictionary<string, object> ISearchTemplateRequest.Params { get; set; }

		IList<KeyValuePair<PropertyPathMarker, ISort>> ISearchTemplateRequest.Sort { get; set; }

		Func<dynamic, Hit<dynamic>, Type> ISearchTemplateRequest.TypeSelector { get; set; }

		public SearchTemplateDescriptor<T> Template(Func<TemplateDescriptor, TemplateDescriptor> templateSelector)
		{
			templateSelector.ThrowIfNull("templateSelector");
			var descriptor = templateSelector(new TemplateDescriptor());
			this.Self.Template = descriptor.Template;
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

		/// <summary>
		/// <para>Sort() allows you to fully describe your sort unlike the SortAscending and SortDescending aliases.
		/// </para>
		/// </summary>
		public SearchTemplateDescriptor<T> Sort(Func<SortFieldDescriptor<T>, IFieldSort> sortSelector)
		{
			if (Self.Sort == null)
				Self.Sort = new List<KeyValuePair<PropertyPathMarker, ISort>>();

			sortSelector.ThrowIfNull("sortSelector");
			var descriptor = sortSelector(new SortFieldDescriptor<T>());
			Self.Sort.Add(new KeyValuePair<PropertyPathMarker, ISort>(descriptor.Field, descriptor));
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

	[JsonObject]
	public class SearchTemplate
	{
		[JsonProperty("file")]
		public string File { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("query")]
		public IQueryContainer Query { get; set; }
	}

	public class TemplateDescriptor
	{
		public SearchTemplate Template = new SearchTemplate();

		public TemplateDescriptor File(string file)
		{
			Template.File = file;
			return this;
		}

		public TemplateDescriptor Id(string id)
		{
			Template.Id = id;
			return this;
		}

		public TemplateDescriptor Query<T>(Func<QueryDescriptor<T>, QueryContainer> query) where T : class
		{
			query.ThrowIfNull("query");
			var q = new QueryDescriptor<T>();
			((IQueryContainer)q).IsStrict = true;
			Template.Query = query(q);
			return this;
		}
	}
}
