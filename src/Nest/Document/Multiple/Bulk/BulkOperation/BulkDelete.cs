using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IBulkDeleteOperation<T> : IBulkOperation
		where T : class
	{
		T Document { get; set; }

		[DataMember(Name = "if_seq_no")]
		long? IfSeqNo { get; set; }

		[DataMember(Name = "if_primary_term")]
		long? IfPrimaryTerm { get; set; }
	}

	[DataContract]
	public class BulkDeleteOperation<T> : BulkOperationBase, IBulkDeleteOperation<T>
		where T : class
	{
		public BulkDeleteOperation(T document) => Document = document;

		public BulkDeleteOperation(Id id) => Id = id;

		public T Document { get; set; }

		public long? IfSeqNo { get; set; }

		public long? IfPrimaryTerm { get; set; }

		protected override Type ClrType => typeof(T);

		protected override string Operation => "delete";

		protected override object GetBody() => null;

		protected override Id GetIdForOperation(Inferrer inferrer) => Id ?? new Id(Document);

		protected override Routing GetRoutingForOperation(Inferrer inferrer) => Routing ?? new Routing(Document);
	}

	[DataContract]
	public class BulkDeleteDescriptor<T> : BulkOperationDescriptorBase<BulkDeleteDescriptor<T>, IBulkDeleteOperation<T>>, IBulkDeleteOperation<T>
		where T : class
	{
		protected override Type BulkOperationClrType => typeof(T);
		protected override string BulkOperationType => "delete";
		long? IBulkDeleteOperation<T>.IfSeqNo { get; set; }
		long? IBulkDeleteOperation<T>.IfPrimaryTerm { get; set; }

		T IBulkDeleteOperation<T>.Document { get; set; }

		protected override object GetBulkOperationBody() => null;

		protected override Id GetIdForOperation(Inferrer inferrer) => Self.Id ?? new Id(Self.Document);

		protected override Routing GetRoutingForOperation(Inferrer inferrer) => Self.Routing ?? new Routing(Self.Document);

		/// <summary>
		/// The object to infer the id off, (if id is not passed using Id())
		/// </summary>
		public BulkDeleteDescriptor<T> Document(T @object) => Assign(a => a.Document = @object);

		public BulkDeleteDescriptor<T> IfSeqNo(long? seqNo) => Assign(a => a.IfSeqNo = seqNo);

		public BulkDeleteDescriptor<T> IfPrimaryTerm(long? primaryTerm) => Assign(a => a.IfSeqNo = primaryTerm);
	}
}
