using System;
using Nest.Resolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public abstract class BaseBulkOperation
	{

		internal virtual string _Operation { get; set; }
		internal virtual Type _ClrType { get; set; }

		[JsonProperty(PropertyName = "_index")]
		internal string _Index { get; set; }

		[JsonProperty(PropertyName = "_type")]
		internal string _Type { get; set; }

		[JsonProperty(PropertyName = "_id")]
		internal string _Id { get; set; }

		[JsonProperty(PropertyName = "_version")]
		internal string _Version { get; set; }

		[JsonProperty(PropertyName = "_version_type")]
		internal string _VersionType { get; set; }

		[JsonProperty(PropertyName = "_routing")]
		internal string _Routing { get; set; }

		[JsonProperty(PropertyName = "_percolate")]
		internal string _Percolate { get; set; }

		[JsonProperty(PropertyName = "_parent")]
		internal string _Parent { get; set; }

		[JsonProperty("_timestamp")]
		internal long? _Timestamp { get; set; }

		[JsonProperty("_ttl")]
		internal string _Ttl { get; set; }

		[JsonProperty("consistency")]
		[JsonConverter(typeof(StringEnumConverter))]
		internal Consistency? _Consistency { get; set; }

		[JsonProperty("refresh ")]
		internal bool? _Refresh { get; set; }

		internal virtual object _Object { get; set; }

		internal virtual string GetIdForObject(IdResolver resolver)
		{
			return this._Id;
		}
	}
}