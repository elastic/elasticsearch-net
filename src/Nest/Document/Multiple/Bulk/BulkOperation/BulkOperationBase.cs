using System;
using Elasticsearch.Net;

namespace Nest
{
	public abstract class BulkOperationBase : IBulkOperation
	{
		public Id Id { get; set; }
		public IndexName Index { get; set; }

		[Obsolete("This property is no longer available in indices created in Elasticsearch 6.x and up")]
		public Id Parent { get; set; }

		public int? RetriesOnConflict { get; set; }
		public Routing Routing { get; set; }
		public long? Version { get; set; }
		public VersionType? VersionType { get; set; }
		protected abstract Type ClrType { get; }
		protected abstract string Operation { get; }

		Type IBulkOperation.ClrType => ClrType;

		string IBulkOperation.Operation => Operation;

		object IBulkOperation.GetBody() => GetBody();

		Id IBulkOperation.GetIdForOperation(Inferrer inferrer) => GetIdForOperation(inferrer);

		Routing IBulkOperation.GetRoutingForOperation(Inferrer inferrer) => GetRoutingForOperation(inferrer);

		protected abstract object GetBody();

		protected virtual Id GetIdForOperation(Inferrer inferrer) => Id ?? new Id(GetBody());

		protected virtual Routing GetRoutingForOperation(Inferrer inferrer) => Routing ?? new Routing(GetBody());
	}

	public abstract class BulkOperationDescriptorBase<TDescriptor, TInterface>
		: DescriptorBase<TDescriptor, TInterface>, IBulkOperation
		where TDescriptor : BulkOperationDescriptorBase<TDescriptor, TInterface>, TInterface, IBulkOperation
		where TInterface : class, IBulkOperation
	{
		protected abstract Type BulkOperationClrType { get; }
		protected abstract string BulkOperationType { get; }

		Type IBulkOperation.ClrType => BulkOperationClrType;
		Id IBulkOperation.Id { get; set; }

		IndexName IBulkOperation.Index { get; set; }
		string IBulkOperation.Operation => BulkOperationType;

		[Obsolete("This feature is no longer supported on indices created in Elasticsearch 6.x and up")]
		Id IBulkOperation.Parent { get; set; }

		int? IBulkOperation.RetriesOnConflict { get; set; }
		Routing IBulkOperation.Routing { get; set; }
		long? IBulkOperation.Version { get; set; }
		VersionType? IBulkOperation.VersionType { get; set; }

		/// <summary>
		/// Only used for bulk update operations but in the future might come in handy for other complex bulk ops.
		/// </summary>
		/// <returns></returns>
		object IBulkOperation.GetBody() => GetBulkOperationBody();

		Id IBulkOperation.GetIdForOperation(Inferrer inferrer) => GetIdForOperation(inferrer);

		Routing IBulkOperation.GetRoutingForOperation(Inferrer inferrer) => GetRoutingForOperation(inferrer);

		protected abstract object GetBulkOperationBody();

		protected virtual Id GetIdForOperation(Inferrer inferrer) => Self.Id ?? new Id(GetBulkOperationBody());

		protected virtual Routing GetRoutingForOperation(Inferrer inferrer) => Self.Routing ?? new Routing(GetBulkOperationBody());

		/// <summary>
		/// Manually set the index, default to the default index or the fixed index set on the bulk operation
		/// </summary>
		public TDescriptor Index(IndexName index) => Assign(a => a.Index = index);

		public TDescriptor Index<T>() => Assign(a => a.Index = typeof(T));

		/// <summary>
		/// Manually set the id for the newly created object
		/// </summary>
		public TDescriptor Id(Id id) => Assign(a => a.Id = id);

		public TDescriptor Version(long? version) => Assign(a => a.Version = version);

		public TDescriptor VersionType(VersionType? versionType) => Assign(a => a.VersionType = versionType);

		public TDescriptor Routing(Routing routing) => Assign(a => a.Routing = routing);

		[Obsolete("This feature is no longer supported on indices created in Elasticsearch 6.x and up")]
		public TDescriptor Parent(Id parent) => Assign(a => a.Parent = parent);
	}
}
