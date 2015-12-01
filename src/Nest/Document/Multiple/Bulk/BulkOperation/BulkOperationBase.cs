using System;
using Elasticsearch.Net;

namespace Nest
{
	public abstract class BulkOperationBase : IBulkOperation
	{
		public abstract string Operation { get; }
		public abstract Type ClrType { get; }
		public IndexName Index { get; set; }
		public TypeName Type { get; set; }
		public string Id { get; set; }
        public long? Version { get; set; }
		public VersionType? VersionType { get; set; }
		public string Routing { get; set; }
		public string Parent { get; set; }
		public long? Timestamp { get; set; }
		public string Ttl { get; set; }
		public int? RetriesOnConflict { get; set; }
		public abstract object GetBody();

		public virtual string GetIdForOperation(ElasticInferrer inferrer)
		{
			return !this.Id.IsNullOrEmpty() ? this.Id : inferrer.Id(this.GetBody());
		}
	}

	public abstract class BulkOperationDescriptorBase : DescriptorBase<BulkOperationDescriptorBase, IBulkOperation>, IBulkOperation
	{
		protected abstract string BulkOperationType { get; }
		string IBulkOperation.Operation => this.BulkOperationType;

		protected abstract Type BulkOperationClrType { get; }
		Type IBulkOperation.ClrType => this.BulkOperationClrType;

		IndexName IBulkOperation.Index { get; set; }

		TypeName IBulkOperation.Type { get; set; }

		string IBulkOperation.Id { get; set; }

		long? IBulkOperation.Version { get; set; }

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