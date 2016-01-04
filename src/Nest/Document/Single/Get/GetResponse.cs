using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;
using System.Linq;

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
		internal ElasticInferrer Inferrer { get; set; }

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

		[JsonProperty(PropertyName = "fields")]
#pragma warning disable 649
		private IDictionary<string, object> _rawFields;
#pragma warning restore 649

		private FieldSelection<T> _fields = null; 
		public FieldSelection<T> Fields
		{
			get
			{
				if (_fields != null)
					return _fields;

				_fields = new FieldSelection<T>(Inferrer, _rawFields);
				return _fields;
			}
		}
	}
}
