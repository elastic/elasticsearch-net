using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IBulkOperation
	{
		string Operation { get; }
		Type ClrType { get; }

		[JsonProperty(PropertyName = "_index")]
		IndexNameMarker Index { get; set; }

		[JsonProperty(PropertyName = "_type")]
		TypeNameMarker Type { get; set; }

		[JsonProperty(PropertyName = "_id")]
		string Id { get; set; }

		[JsonProperty(PropertyName = "_version")]
		string Version { get; set; }

		[JsonProperty(PropertyName = "_version_type")]
		VersionTypeOptions? VersionType { get; set; }

		[JsonProperty(PropertyName = "_routing")]
		string Routing { get; set; }

		[JsonProperty(PropertyName = "_parent")]
		string Parent { get; set; }

		[JsonProperty("_timestamp")]
		long? Timestamp { get; set; }

		[JsonProperty("_ttl")]
		string Ttl { get; set; }

		[JsonProperty("_retry_on_conflict")]
		int? RetriesOnConflict { get; set; }

		object GetBody();

		string GetIdForOperation(ElasticInferrer inferrer);
	}

	public abstract class BulkOperationDescriptorBase : IBulkOperation
	{
		private IBulkOperation Self { get { return this; } }

		protected abstract string _Operation { get; }
		string IBulkOperation.Operation { get { return this._Operation; } }

		protected abstract Type _ClrType { get; }
		Type IBulkOperation.ClrType { get { return this._ClrType;  } }

		[JsonProperty(PropertyName = "_index")]
		IndexNameMarker IBulkOperation.Index { get; set; }

		[JsonProperty(PropertyName = "_type")]
		TypeNameMarker IBulkOperation.Type { get; set; }

		[JsonProperty(PropertyName = "_id")]
		string IBulkOperation.Id { get; set; }

		[JsonProperty(PropertyName = "_version")]
		string IBulkOperation.Version { get; set; }

		[JsonProperty(PropertyName = "_version_type")]
		VersionTypeOptions? IBulkOperation.VersionType { get; set; }

		[JsonProperty(PropertyName = "_routing")]
		string IBulkOperation.Routing { get; set; }

		[JsonProperty(PropertyName = "_parent")]
		string IBulkOperation.Parent { get; set; }

		[JsonProperty("_timestamp")]
		long? IBulkOperation.Timestamp { get; set; }

		[JsonProperty("_ttl")]
		string IBulkOperation.Ttl { get; set; }

		[JsonProperty("_retry_on_conflict")]
		int? IBulkOperation.RetriesOnConflict { get; set; }

		

		protected abstract object _GetBody();

		/// <summary>
		/// Only used for bulk update operations but in the future might come in handy for other complex bulk ops.
		/// </summary>
		/// <returns></returns>
		object IBulkOperation.GetBody()
		{
			return this._GetBody();
		}

		string IBulkOperation.GetIdForOperation(ElasticInferrer inferrer)
		{
			return this.GetIdForOperation(inferrer);
		}
		protected virtual string GetIdForOperation(ElasticInferrer inferrer)
		{
			return !Self.Id.IsNullOrEmpty() ? Self.Id : inferrer.Id(this._GetBody());
		}
	}
}