using System;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	public interface IBulkIndexOperation<T> : IBulkOperation
	{
		[JsonFormatter(typeof(SourceWriteFormatter<>))]
		T Document { get; set; }

		[DataMember(Name ="_percolate")]
		string Percolate { get; set; }

		[DataMember(Name ="pipeline")]
		string Pipeline { get; set; }

		[JsonProperty("if_seq_no")]
		long? IfSeqNo { get; set; }

		[JsonProperty("if_primary_term")]
		long? IfPrimaryTerm { get; set; }
	}

	public class BulkIndexOperation<T> : BulkOperationBase, IBulkIndexOperation<T>
		where T : class
	{
		public BulkIndexOperation(T document) => Document = document;

		public T Document { get; set; }

		public string Percolate { get; set; }

		public string Pipeline { get; set; }

		public long? IfSeqNo { get; set; }

		public long? IfPrimaryTerm { get; set; }

		protected override Type ClrType => typeof(T);

		protected override string Operation => "index";

		protected override object GetBody() => Document;

		protected override Id GetIdForOperation(Inferrer inferrer) => Id ?? new Id(Document);

		protected override Routing GetRoutingForOperation(Inferrer inferrer) => Routing ?? new Routing(Document);
	}


	public class BulkIndexDescriptor<T> : BulkOperationDescriptorBase<BulkIndexDescriptor<T>, IBulkIndexOperation<T>>, IBulkIndexOperation<T>
		where T : class
	{
		protected override Type BulkOperationClrType => typeof(T);
		protected override string BulkOperationType => "index";
		T IBulkIndexOperation<T>.Document { get; set; }
		string IBulkIndexOperation<T>.Percolate { get; set; }
		string IBulkIndexOperation<T>.Pipeline { get; set; }
		long? IBulkIndexOperation<T>.IfSeqNo { get; set; }
		long? IBulkIndexOperation<T>.IfPrimaryTerm { get; set; }

		protected override object GetBulkOperationBody() => Self.Document;

		protected override Id GetIdForOperation(Inferrer inferrer) => Self.Id ?? new Id(Self.Document);

		protected override Routing GetRoutingForOperation(Inferrer inferrer) => Self.Routing ?? new Routing(Self.Document);

		/// <summary>
		/// The object to index, if id is not manually set it will be inferred from the object
		/// </summary>
		public BulkIndexDescriptor<T> Document(T @object) => Assign(a => a.Document = @object);

		/// <summary>
		/// The pipeline id to preprocess documents with
		/// </summary>
		public BulkIndexDescriptor<T> Pipeline(string pipeline) => Assign(a => a.Pipeline = pipeline);

		public BulkIndexDescriptor<T> Percolate(string percolate) => Assign(a => a.Percolate = percolate);

		public BulkIndexDescriptor<T> IfSeqNo(long? seqNo) => Assign(a => a.IfSeqNo = seqNo);

		public BulkIndexDescriptor<T> IfPrimaryTerm(long? primaryTerm) => Assign(a => a.IfSeqNo = primaryTerm);
	}
}
