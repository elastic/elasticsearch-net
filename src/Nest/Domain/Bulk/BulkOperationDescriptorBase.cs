using System;
using Elasticsearch.Net;

namespace Nest
{
	public abstract class BulkOperationDescriptorBase : IBulkOperation
	{
		private IBulkOperation Self { get { return this; } }

		protected abstract string BulkOperationType { get; }
		string IBulkOperation.Operation { get { return this.BulkOperationType; } }

		protected abstract Type BulkOperationClrType { get; }
		Type IBulkOperation.ClrType { get { return this.BulkOperationClrType;  } }

		IndexNameMarker IBulkOperation.Index { get; set; }

		TypeNameMarker IBulkOperation.Type { get; set; }

		string IBulkOperation.Id { get; set; }

        long IBulkOperation.Version { get; set; }

		VersionType? IBulkOperation.VersionType { get; set; }

		string IBulkOperation.Routing { get; set; }

		string IBulkOperation.Parent { get; set; }

		long? IBulkOperation.Timestamp { get; set; }

		string IBulkOperation.Ttl { get; set; }

		int? IBulkOperation.RetriesOnConflict { get; set; }

		

		protected abstract object GetBulkOperationBody();

		/// <summary>
		/// Only used for bulk update operations but in the future might come in handy for other complex bulk ops.
		/// </summary>
		/// <returns></returns>
		object IBulkOperation.GetBody()
		{
			return this.GetBulkOperationBody();
		}

		string IBulkOperation.GetIdForOperation(ElasticInferrer inferrer)
		{
			return this.GetIdForOperation(inferrer);
		}
		protected virtual string GetIdForOperation(ElasticInferrer inferrer)
		{
			return !Self.Id.IsNullOrEmpty() ? Self.Id : inferrer.Id(this.GetBulkOperationBody());
		}
	}
}