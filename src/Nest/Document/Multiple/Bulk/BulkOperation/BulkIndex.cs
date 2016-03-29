using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IIndexOperation<T> : IBulkOperation
	{
		[JsonProperty(PropertyName = "_percolate")]
		string Percolate { get; set; }

		T Document { get; set; }
	}

	public class BulkIndexOperation<T> : BulkOperationBase, IIndexOperation<T>
		where T : class
	{
		public BulkIndexOperation(T document)
		{
			this.Document = document;
		}

		protected override string Operation => "index";

		protected override Type ClrType => typeof(T);

		protected override object GetBody() => this.Document;

		protected override Id GetIdForOperation(Inferrer inferrer) => this.Id ?? new Id(this.Document);

		public string Percolate { get; set; }

		public T Document { get; set; }
	}


	public class BulkIndexDescriptor<T> : BulkOperationDescriptorBase<BulkIndexDescriptor<T>, IIndexOperation<T>>, IIndexOperation<T> 
		where T : class
	{
		protected override string BulkOperationType => "index";
		protected override Type BulkOperationClrType => typeof(T);

		string IIndexOperation<T>.Percolate { get; set; }
		T IIndexOperation<T>.Document { get; set; }

		protected override object GetBulkOperationBody() => Self.Document;

		protected override Id GetIdForOperation(Inferrer inferrer) => Self.Id ?? new Id(Self.Document);

		/// <summary>
		/// The object to index, if id is not manually set it will be inferred from the object
		/// </summary>
		public BulkIndexDescriptor<T> Document(T @object) => Assign(a => a.Document = @object);

	}
}