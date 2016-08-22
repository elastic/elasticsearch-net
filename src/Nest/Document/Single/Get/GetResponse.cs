using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;
using System.Linq;

namespace Nest
{
	public interface IGetResponse<T> : IResponse where T : class
	{
		[JsonProperty("_index")]
		string Index { get; }

		[JsonProperty("_type")]
		string Type { get; }

		[JsonProperty("_id")]
		string Id { get; }

		[JsonProperty("_version")]
		long Version { get; }

		[JsonProperty("found")]
		bool Found { get; }

		[JsonProperty("_source")]
		T Source { get; }

		[JsonProperty("fields")]
		FieldValues Fields { get; }

		[JsonProperty("_parent")]
		string Parent { get; }

		[JsonProperty("_routing")]
		string Routing { get; }

		[JsonProperty("_timestamp")]
		long? Timestamp { get; }

		[JsonProperty("_ttl")]
		long? Ttl { get; }
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
		public string Parent { get; private set; }
		public string Routing { get; private set; }
		public long? Timestamp { get; private set; }
		public long? Ttl { get; private set; }
	}
}
