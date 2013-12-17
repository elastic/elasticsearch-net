using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	public partial class UpdateDescriptor<T,K> : DocumentPathDescriptorBase<UpdateDescriptor<T, K>, T, UpdateQueryString> 
		where T : class 
		where K : class
	{

		[JsonProperty(PropertyName = "script")]
		internal string _Script { get; set; }

		[JsonProperty(PropertyName = "params")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		internal Dictionary<string, object> _Params { get; set; }

		[JsonProperty(PropertyName = "upsert")]
		internal object _Upsert { get; set; }

		[JsonProperty(PropertyName = "doc_as_upsert")]
		internal bool? _DocAsUpsert { get; set; }

		[JsonProperty(PropertyName = "doc")]
		internal K _Document { get; set; }

		public UpdateDescriptor<T, K> Script(string script)
		{
			script.ThrowIfNull("script");
			this._Script = script;
			return this;
		}

		public UpdateDescriptor<T, K> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramDictionary)
		{
			paramDictionary.ThrowIfNull("paramDictionary");
			this._Params = paramDictionary(new FluentDictionary<string, object>());
			return this;
		}

		public UpdateDescriptor<T, K> Upsert(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> upsertValues)
		{
			upsertValues.ThrowIfNull("upsertValues");
			this._Upsert = upsertValues(new FluentDictionary<string, object>());
			return this;
		}

		public UpdateDescriptor<T, K> Upsert(K upsertObject)
		{
			upsertObject.ThrowIfNull("upsertObject");
			this._Upsert = upsertObject;
			return this;
		}

		/// <summary>
		/// The partial update document to be merged on to the existing object.
		/// </summary>
		public UpdateDescriptor<T, K> Document(K @object)
		{
			this._Document = @object;
			return this;
		}

		public UpdateDescriptor<T, K> DocAsUpsert(bool docAsUpsert = true)
		{
			this._DocAsUpsert = docAsUpsert;
			return this;
		}

		internal new ElasticSearchPathInfo<UpdateQueryString> ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<UpdateQueryString>(settings);
			pathInfo.QueryString = this._QueryString;
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
			
			return pathInfo;
		}
	}
}
