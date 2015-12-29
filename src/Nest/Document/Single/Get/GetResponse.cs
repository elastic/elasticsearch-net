using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetResponse<T> : IResponse where T : class
	{
		bool Found { get; }
		string Index { get; }
		string Type { get; }
		string Id { get; }
		long Version { get; }
		T Source { get; }
		FieldSelection<T> Fields { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class GetResponse<T> : BaseResponse, IGetResponse<T> where T : class
	{
		private IDictionary<string, object> _fieldValues;

		[JsonProperty(PropertyName = "_index")]
		public string Index { get; private set; }

		[JsonProperty(PropertyName = "_type")]
		public string Type { get; private set; }

		[JsonProperty(PropertyName = "_id")]
		public string Id { get; private set; }

		[JsonProperty(PropertyName = "_version")]
		public long Version { get; private set; }

		[JsonProperty(PropertyName = "found")]
		public bool Found { get; private set; }

		[JsonProperty(PropertyName = "_source")]
		public T Source { get; private set; }


		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		[JsonProperty(PropertyName = "fields")]
		internal IDictionary<string, object> FieldValues
		{
			get { return _fieldValues; }
			set { _fieldValues = value; }
		}

		//private FieldSelection<T> _fields = null; 
		public FieldSelection<T> Fields
		{
			get
			{
				//TODO High Priority: Fix field selections
				throw new NotImplementedException("Fieldselections are broken in 2.0, responses no longer have settings");
				//if (_fields != null)
				//	return _fields;

				//if (this.ApiCall == null)
				//	return null;
				//var realSettings = this.ApiCall.Settings as IConnectionSettingsValues;

				//_fields = new FieldSelection<T>(realSettings, FieldValues);
				//return _fields;
			}
		}

		public K[] FieldValue<TBindTo, K>(Expression<Func<TBindTo, object>> objectPath)
			where TBindTo : class
		{
			if (this.Fields == null) return default(K[]);
			return this.Fields.FieldValues<TBindTo,K>(objectPath);
		}

		public K FieldValue<K>(string path)
		{
			if (this.Fields == null) return default(K);
			return this.Fields.FieldValues<K>(path);
		}

	}
}
