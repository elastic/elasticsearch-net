using System;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IBulkCreateOperation<T> : IBulkOperation
		where T : class
	{
		T Document { get; set; }

		[DataMember(Name ="pipeline")]
		string Pipeline { get; set; }
	}

	public class BulkCreateOperation<T> : BulkOperationBase, IBulkCreateOperation<T>
		where T : class
	{
		public BulkCreateOperation(T document) => Document = document;

		public T Document { get; set; }

		public string Pipeline { get; set; }

		protected override Type ClrType => typeof(T);

		protected override string Operation => "create";

		protected override object GetBody() => Document;

		protected override Id GetIdForOperation(Inferrer inferrer) => Id ?? new Id(Document);

		protected override Routing GetRoutingForOperation(Inferrer inferrer) => Routing ?? new Routing(Document);
	}


	public class BulkCreateDescriptor<T> : BulkOperationDescriptorBase<BulkCreateDescriptor<T>, IBulkCreateOperation<T>>, IBulkCreateOperation<T>
		where T : class
	{
		protected override Type BulkOperationClrType => typeof(T);
		protected override string BulkOperationType => "create";

		T IBulkCreateOperation<T>.Document { get; set; }

		string IBulkCreateOperation<T>.Pipeline { get; set; }

		protected override object GetBulkOperationBody() => Self.Document;

		protected override Id GetIdForOperation(Inferrer inferrer) => Self.Id ?? new Id(Self.Document);

		protected override Routing GetRoutingForOperation(Inferrer inferrer) => Self.Routing ?? new Routing(Self.Document);

		/// <summary>
		/// The object to update, if id is not manually set it will be inferred from the object
		/// </summary>
		public BulkCreateDescriptor<T> Document(T @object) => Assign(a => a.Document = @object);

		/// <summary>
		/// The pipeline id to preprocess documents with
		/// </summary>
		public BulkCreateDescriptor<T> Pipeline(string pipeline) => Assign(a => a.Pipeline = pipeline);
	}
}
