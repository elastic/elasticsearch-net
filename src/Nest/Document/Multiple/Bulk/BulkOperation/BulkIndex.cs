using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IBulkIndexOperation<T> : IBulkOperation
	{
		[JsonProperty("_percolate")]
		string Percolate { get; set; }

		[JsonProperty("pipeline")]
		string Pipeline { get; set; }

		[JsonConverter(typeof(SourceConverter))]
		T Document { get; set; }
	}

	public class BulkIndexOperation<T> : BulkOperationBase, IBulkIndexOperation<T>
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
		protected override Routing GetRoutingForOperation(Inferrer inferrer) => this.Routing ?? new Routing(this.Document);

		public string Percolate { get; set; }

		public string Pipeline { get; set; }

		public T Document { get; set; }
	}


	public class BulkIndexDescriptor<T> : BulkOperationDescriptorBase<BulkIndexDescriptor<T>, IBulkIndexOperation<T>>, IBulkIndexOperation<T>
		where T : class
	{
		protected override string BulkOperationType => "index";
		protected override Type BulkOperationClrType => typeof(T);
		string IBulkIndexOperation<T>.Percolate { get; set; }
		string IBulkIndexOperation<T>.Pipeline { get; set; }
		T IBulkIndexOperation<T>.Document { get; set; }

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
	}
}
