using System;

namespace Nest
{
	public interface IBulkDeleteOperation<T> : IBulkOperation
		where T : class
	{
		T Document { get; set; }
	}

	public class BulkDeleteOperation<T> : BulkOperationBase, IBulkDeleteOperation<T>
		where T : class
	{
		public BulkDeleteOperation(T document)
		{
			this.Document = document;
		} 
		
		public BulkDeleteOperation(Id id) { this.Id = id; }

		protected override string Operation => "delete";

		protected override Type ClrType => typeof(T);

		protected override object GetBody() => null;

		protected override Id GetIdForOperation(Inferrer inferrer) => this.Id ?? new Id(this.Document);

		public T Document { get; set; }
	}
	public class BulkDeleteDescriptor<T> : BulkOperationDescriptorBase<BulkDeleteDescriptor<T>, IBulkDeleteOperation<T>>, IBulkDeleteOperation<T>
		where T : class
	{
		protected override string BulkOperationType => "delete";
		protected override Type BulkOperationClrType => typeof(T);

		T IBulkDeleteOperation<T>.Document { get; set; }

		protected override object GetBulkOperationBody() => null;

		protected override Id GetIdForOperation(Inferrer inferrer) => Self.Id ?? new Id(Self.Document);

		/// <summary>
		/// The object to infer the id off, (if id is not passed using Id())
		/// </summary>
		public BulkDeleteDescriptor<T> Document(T @object) => Assign(a => a.Document = @object);


	}
}