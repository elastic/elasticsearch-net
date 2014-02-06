using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest.Domain;
using Nest.Resolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public interface IGetResponse<T> : IResponse where T : class
	{
		bool Exists { get; }
		string Index { get; }
		string Type { get; }
		string Id { get; }
		string Version { get; }
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
		public string Version { get; private set; }

		[JsonProperty(PropertyName = "exists")]
		public bool Exists { get; private set; }

		[JsonProperty(PropertyName = "_source")]
		public T Source { get; private set; }


		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		[JsonProperty(PropertyName = "fields")]
		internal IDictionary<string, object> FieldValues
		{
			get { return _fieldValues; }
			set { _fieldValues = value; }
		}

		private FieldSelection<T> _fields = null; 
		public FieldSelection<T> Fields
		{
			get
			{
				if (_fields != null)
					return _fields;

				if (this.ConnectionStatus == null)
					return null;
				_fields = new FieldSelection<T>(this.ConnectionStatus.Infer, FieldValues);
				return _fields;
			}
		}

		public K FieldValue<K>(Expression<Func<T, object>> objectPath)
		{
			if (this.Fields == null) return default(K);
			return this.Fields.FieldValue<K>(objectPath);
		}

		public K FieldValue<K>(string path)
		{
			if (this.Fields == null) return default(K);
			return this.Fields.FieldValue<K>(path);
		}

	}
}
