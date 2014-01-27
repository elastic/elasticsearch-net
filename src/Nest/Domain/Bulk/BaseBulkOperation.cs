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
		internal IndexNameMarker _Index { get; set; }

		[JsonProperty(PropertyName = "_type")]
		internal TypeNameMarker _Type { get; set; }

		[JsonProperty(PropertyName = "_id")]
		internal string _Id { get; set; }

		[JsonProperty(PropertyName = "_version")]
		internal string _Version { get; set; }

		[JsonProperty(PropertyName = "_version_type")]
		internal string _VersionType { get; set; }

		[JsonProperty(PropertyName = "_routing")]
		internal string _Routing { get; set; }


		[JsonProperty(PropertyName = "_parent")]
		internal string _Parent { get; set; }

		[JsonProperty("_timestamp")]
		internal long? _Timestamp { get; set; }

		[JsonProperty("_ttl")]
		internal string _Ttl { get; set; }



		[JsonProperty("_retry_on_conflict")]
		internal int? _RetriesOnConflict { get; set; }

		internal virtual object _Object { get; set; }

		internal virtual string GetIdForObject(ElasticInferrer inferrer)
		{
			return this._Id;
		}
		/// <summary>
		/// Only used for bulk update operations but in the future might come in handy for other complex bulk ops.
		/// </summary>
		/// <returns></returns>
		internal virtual object GetBody()
		{
			return null;
		}
	}
}