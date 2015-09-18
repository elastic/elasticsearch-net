using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IUpdateRequest<TDocument,TPartialDocument> : IRequest<UpdateRequestParameters>
		where TDocument : class
		where TPartialDocument : class 
	{
		[JsonProperty(PropertyName = "script")]
		string Script { get; set; }

		[JsonProperty(PropertyName = "script_id")]
		string ScriptId { get; set; }

		[JsonProperty(PropertyName = "script_file")]
		string ScriptFile { get; set; }

		[JsonProperty(PropertyName = "lang")]
		string Language { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof (VerbatimDictionaryKeysJsonConverter))]
		Dictionary<string, object> Params { get; set; }

		[JsonProperty(PropertyName = "upsert")]
		TDocument Upsert { get; set; }

		[JsonProperty(PropertyName = "doc_as_upsert")]
		bool? DocAsUpsert { get; set; }

		[JsonProperty(PropertyName = "doc")]
		TPartialDocument Doc { get; set; }
	}

	internal static class UpdateRequestPathInfo
	{
		public static void Update<TDocument, TPartialDocument>(
			IConnectionSettingsValues settings,
			RequestPath pathInfo,
			IUpdateRequest<TDocument, TPartialDocument> self)
			where TDocument : class
			where TPartialDocument : class
		{
			if (pathInfo.Id.IsNullOrEmpty())
			{
				if (self.Doc != null)
					pathInfo.Id = settings.Inferrer.Id(self.Doc);
				else if (self.Upsert != null)
					pathInfo.Id = settings.Inferrer.Id(self.Upsert);
			}

			pathInfo.HttpMethod = HttpMethod.POST;
		}
	}

	public class UpdateRequest<TDocument> : UpdateRequest<TDocument,TDocument>
		where TDocument : class
    {
	}

	public partial class UpdateRequest<TDocument,TPartialDocument> : RequestBase<UpdateRequestParameters>, IUpdateRequest<TDocument, TPartialDocument> 
		where TDocument : class
		where TPartialDocument : class 
	{
		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath pathInfo)
		{
			UpdateRequestPathInfo.Update(settings, pathInfo, this);
		}

		public string Script { get; set; }
		public string ScriptFile { get; set; }
		public string Language { get; set; }
		public Dictionary<string, object> Params { get; set; }
		public TDocument Upsert { get; set; }
		public bool? DocAsUpsert { get; set; }
		public TPartialDocument Doc { get; set; }
	}

	public partial class UpdateDescriptor<TDocument,TPartialDocument> 
		: RequestDescriptorBase<UpdateDescriptor<TDocument, TPartialDocument>, UpdateRequestParameters>
		, IUpdateRequest<TDocument, TPartialDocument> 
		where TDocument : class 
		where TPartialDocument : class
	{

		private IUpdateRequest<TDocument, TPartialDocument> Self => this;

		string IUpdateRequest<TDocument, TPartialDocument>.Script { get; set; }
		
		string IUpdateRequest<TDocument, TPartialDocument>.ScriptId { get; set; }
		
		string IUpdateRequest<TDocument, TPartialDocument>.ScriptFile { get; set; }

		string IUpdateRequest<TDocument, TPartialDocument>.Language { get; set; }

		Dictionary<string, object> IUpdateRequest<TDocument, TPartialDocument>.Params { get; set; }

		TDocument IUpdateRequest<TDocument, TPartialDocument>.Upsert { get; set; }

		bool? IUpdateRequest<TDocument, TPartialDocument>.DocAsUpsert { get; set; }

		TPartialDocument IUpdateRequest<TDocument, TPartialDocument>.Doc { get; set; }

		
		public UpdateDescriptor<TDocument, TPartialDocument> Script(string script)
		{
			script.ThrowIfNull("script");
			Self.Script = script;
			return this;
		}

		public UpdateDescriptor<TDocument, TPartialDocument> ScriptFile(string scriptFile)
		{
			scriptFile.ThrowIfNull("scriptFile");
			Self.ScriptFile = scriptFile;
			return this;
		}

		public UpdateDescriptor<TDocument, TPartialDocument> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			Self.Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}

		public UpdateDescriptor<TDocument, TPartialDocument> Language(string language)
		{
			Self.Language = language;
			return this;
		}

		public UpdateDescriptor<TDocument, TPartialDocument> Id(TDocument document, bool useAsUpsert)
		{
            //TODO: What should this be when we have an Ids type?
			//((IDocumentOptionalPath<UpdateRequestParameters, TDocument>)Self).IdFrom = document;
			if (useAsUpsert)
				return this.Upsert(document);
			return this;
		}


		/// <summary>
		/// The full document to be created if an existing document does not exist for a partial merge.
		/// </summary>
		public UpdateDescriptor<TDocument, TPartialDocument> Upsert(TDocument upsertObject)
		{
			upsertObject.ThrowIfNull("upsertObject");
			Self.Upsert = upsertObject;
			return this;
		}

		/// <summary>
		/// The partial update document to be merged on to the existing object.
		/// </summary>
		public UpdateDescriptor<TDocument, TPartialDocument> Doc(TPartialDocument @object)
		{
			Self.Doc = @object;
			return this;
		}

		public UpdateDescriptor<TDocument, TPartialDocument> DocAsUpsert(bool docAsUpsert = true)
		{
			Self.DocAsUpsert = docAsUpsert;
			return this;
		}

		///<summary>A comma-separated list of fields to return in the response</summary>
		public UpdateDescriptor<TDocument,TPartialDocument> Fields(params string[] fields)
		{
			this.Request.Parameters.AddQueryString("fields", fields);
			return this;
		}
		
			
		///<summary>A comma-separated list of fields to return in the response</summary>
		public UpdateDescriptor<TDocument,TPartialDocument> Fields(params Expression<Func<TPartialDocument, object>>[] typedPathLookups) 
		{
			if (!typedPathLookups.HasAny())
				return this;

			this.Request.Parameters.AddQueryString("fields",typedPathLookups);
			return this;
		}
			
		protected override void UpdateRequestPath(IConnectionSettingsValues settings, RequestPath pathInfo)
		{
			UpdateRequestPathInfo.Update(settings, pathInfo, this);
		}
	}
}
