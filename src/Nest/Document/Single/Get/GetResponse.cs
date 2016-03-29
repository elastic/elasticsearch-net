using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;
using System.Linq;

namespace Nest
{
	public interface IGetResponse<T> : IResponse where T : class
	{
		[JsonProperty(PropertyName = "_index")]
		string Index { get; }

		[JsonProperty(PropertyName = "_type")]
		string Type { get; }

		[JsonProperty(PropertyName = "_id")]
		string Id { get; }

		[JsonProperty(PropertyName = "_version")]
		long Version { get; }

		[JsonProperty(PropertyName = "found")]
		bool Found { get; }

		[JsonProperty(PropertyName = "_source")]
		T Source { get; }

		[JsonProperty(PropertyName = "fields")]
		FieldValues Fields { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class GetResponse<T> : ResponseBase, IGetResponse<T> where T : class
	{
		public string Index { get; private set; }
		public string Type { get; private set; }
		public string Id { get; private set; }
		public long Version { get; private set; }
		public bool Found { get; private set; }
		public T Source { get; private set; }
		public FieldValues Fields { get; private set; }
	}
}
