using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Elasticsearch.Net;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IUpdateRequest<TUpsert,TDocument> : IDocumentOptionalPath<UpdateRequestParameters>
		where TUpsert : class
		where TDocument : class 
	{
		[JsonProperty(PropertyName = "script")]
		string Script { get; set; }

		[JsonProperty(PropertyName = "lang")]
		string Language { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		Dictionary<string, object> Params { get; set; }

		[JsonProperty(PropertyName = "upsert")]
		TUpsert Upsert { get; set; }

		[JsonProperty(PropertyName = "doc_as_upsert")]
		bool? DocAsUpsert { get; set; }

		[JsonProperty(PropertyName = "doc")]
		TDocument Document { get; set; }
	}

	public class UpdateRequest<TDocument> : UpdateRequest<TDocument, TDocument>
		where TDocument : class 
	{
		
	}

	public partial class UpdateRequest<TUpsert, TDocument> : DocumentOptionalPathBase<UpdateRequestParameters, TUpsert>, IUpdateRequest<TUpsert, TDocument> 
		where TUpsert : class
		where TDocument : class 
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<UpdateRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
		

		public string Script { get; set; }
		public string Language { get; set; }
		public Dictionary<string, object> Params { get; set; }
		public TUpsert Upsert { get; set; }
		public bool? DocAsUpsert { get; set; }
		public TDocument Document { get; set; }
	}


	public partial class UpdateDescriptor<TUpsert,TDocument> 
		: DocumentPathDescriptor<UpdateDescriptor<TUpsert, TDocument>, UpdateRequestParameters, TUpsert>
		, IUpdateRequest<TUpsert, TDocument> 
		where TUpsert : class 
		where TDocument : class
	{

		private IUpdateRequest<TUpsert, TDocument> Self { get { return this; } }

		string IUpdateRequest<TUpsert, TDocument>.Script { get; set; }

		string IUpdateRequest<TUpsert, TDocument>.Language { get; set; }

		Dictionary<string, object> IUpdateRequest<TUpsert, TDocument>.Params { get; set; }

		TUpsert IUpdateRequest<TUpsert, TDocument>.Upsert { get; set; }

		bool? IUpdateRequest<TUpsert, TDocument>.DocAsUpsert { get; set; }

		TDocument IUpdateRequest<TUpsert, TDocument>.Document { get; set; }

		
		public UpdateDescriptor<TUpsert, TDocument> Script(string script)
		{
			script.ThrowIfNull("script");
			Self.Script = script;
			return this;
		}

		public UpdateDescriptor<TUpsert, TDocument> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			Self.Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}

		/// <summary>
		/// The full document to be created if an existing document does not exist for a partial merge.
		/// </summary>
		public UpdateDescriptor<TUpsert, TDocument> Upsert(TUpsert upsertObject)
		{
			upsertObject.ThrowIfNull("upsertObject");
			Self.Upsert = upsertObject;
			return this;
		}

		/// <summary>
		/// The partial update document to be merged on to the existing object.
		/// </summary>
		public UpdateDescriptor<TUpsert, TDocument> Document(TDocument @object)
		{
			Self.Document = @object;
			return this;
		}

		public UpdateDescriptor<TUpsert, TDocument> DocAsUpsert(bool docAsUpsert = true)
		{
			Self.DocAsUpsert = docAsUpsert;
			return this;
		}

		///<summary>A comma-separated list of fields to return in the response</summary>
		public UpdateDescriptor<TUpsert,TDocument> Fields(params string[] fields)
		{
			this.Request.RequestParameters.AddQueryString("fields", fields);
			return this;
		}
		
			
		///<summary>A comma-separated list of fields to return in the response</summary>
		public UpdateDescriptor<TUpsert,TDocument> Fields(params Expression<Func<TDocument, object>>[] typedPathLookups) 
		{
			if (!typedPathLookups.HasAny())
				return this;

			this.Request.RequestParameters.AddQueryString("fields",typedPathLookups);
			return this;
		}
			
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<UpdateRequestParameters> pathInfo)
		{
			if (Self.Document != null && Self.Id == null)
				Self.Id = new ElasticInferrer(settings).Id(Self.Document);

			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
}
