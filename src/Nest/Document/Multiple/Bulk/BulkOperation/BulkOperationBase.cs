// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elasticsearch.Net;

namespace Nest
{
	public abstract class BulkOperationBase : IBulkOperation
	{
		public Id Id { get; set; }
		public IndexName Index { get; set; }

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

		int? IBulkOperation.RetriesOnConflict { get; set; }
		Routing IBulkOperation.Routing { get; set; }
		long? IBulkOperation.Version { get; set; }
		VersionType? IBulkOperation.VersionType { get; set; }

		/// <summary>
		/// Only used for bulk update operations but in the future might come in handy for other complex bulk ops.
		/// </summary>
		object IBulkOperation.GetBody() => GetBulkOperationBody();

		Id IBulkOperation.GetIdForOperation(Inferrer inferrer) => GetIdForOperation(inferrer);

		Routing IBulkOperation.GetRoutingForOperation(Inferrer inferrer) => GetRoutingForOperation(inferrer);

		protected abstract object GetBulkOperationBody();

		protected virtual Id GetIdForOperation(Inferrer inferrer) => Self.Id ?? new Id(GetBulkOperationBody());

		protected virtual Routing GetRoutingForOperation(Inferrer inferrer) => Self.Routing ?? new Routing(GetBulkOperationBody());

		/// <summary>
		/// Manually set the index, default to the default index or the fixed index set on the bulk operation
		/// </summary>
		public TDescriptor Index(IndexName index) => Assign(index, (a, v) => a.Index = v);

		public TDescriptor Index<T>() => Assign(typeof(T), (a, v) => a.Index = v);

		/// <summary>
		/// Manually set the id for the newly created object
		/// </summary>
		public TDescriptor Id(Id id) => Assign(id, (a, v) => a.Id = v);

		public TDescriptor Version(long? version) => Assign(version, (a, v) => a.Version = v);

		public TDescriptor VersionType(VersionType? versionType) => Assign(versionType, (a, v) => a.VersionType = v);

		public TDescriptor Routing(Routing routing) => Assign(routing, (a, v) => a.Routing = v);
	}
}
