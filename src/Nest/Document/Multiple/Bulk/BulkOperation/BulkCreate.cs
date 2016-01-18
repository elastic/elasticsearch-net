using System;

namespace Nest
{
	public interface IBulkCreateOperation<T> : IBulkOperation
		where T : class
	{
		T Document { get; set; }
	}

	public class BulkCreateOperation<T> : BulkOperationBase, IBulkCreateOperation<T>
		where T : class
	{
		public T Document { get; set; }

		public BulkCreateOperation(T document)
		{
			this.Document = document;
		}

		protected override string Operation => "create";

		protected override Type ClrType => typeof(T);

		protected override object GetBody() => this.Document;

		protected override Id GetIdForOperation(Inferrer inferrer) => this.Id ?? new Id(this.Document);
	}


	public class BulkCreateDescriptor<T> : BulkOperationDescriptorBase<BulkCreateDescriptor<T>, IBulkCreateOperation<T>>, IBulkCreateOperation<T> 
		where T : class
	{
		protected override string BulkOperationType => "create";
		protected override Type BulkOperationClrType => typeof(T);

		protected override object GetBulkOperationBody() => Self.Document;

		protected override Id GetIdForOperation(Inferrer inferrer) => Self.Id ?? new Id(Self.Document);

		T IBulkCreateOperation<T>.Document { get; set; }

		/// <summary>
		/// The object to update, if id is not manually set it will be inferred from the object
		/// </summary>
		public BulkCreateDescriptor<T> Document(T @object) => Assign(a => a.Document = @object);
	}
}