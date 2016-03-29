using System;

namespace Nest
{

	public class MultiGetOperation<T> : IMultiGetOperation
	{
		public MultiGetOperation(Id id)
		{
			this.Id = id;
			this.Index = typeof(T);
			this.Type = typeof(T);
		}

		Type IMultiGetOperation.ClrType => typeof(T);

		public IndexName Index { get; set; }

		public TypeName Type { get; set; }

		public Id Id { get; set; }

		public Fields Fields { get; set; }

		public Union<bool, ISourceFilter> Source { get; set; }

		public string Routing { get; set; }

		bool IMultiGetOperation.CanBeFlattened =>
			this.Index == null
			&& this.Type == null
			&& this.Routing == null
			&& this.Source == null
			&& this.Fields == null;


		public object Document { get; set; }
	}

	public class MultiGetOperationDescriptor<T> : DescriptorBase<MultiGetOperationDescriptor<T>, IMultiGetOperation>, IMultiGetOperation
		where T : class
	{
		IndexName IMultiGetOperation.Index { get; set; }
		TypeName IMultiGetOperation.Type { get; set; }
		Id IMultiGetOperation.Id { get; set; }
		string IMultiGetOperation.Routing { get; set; }
		Union<bool, ISourceFilter> IMultiGetOperation.Source { get; set; }
		Fields IMultiGetOperation.Fields { get; set; }
		Type IMultiGetOperation.ClrType => typeof(T);

		bool IMultiGetOperation.CanBeFlattened =>
			Self.Index == null
			&& Self.Type == null
			&& Self.Routing == null
			&& Self.Source == null
			&& Self.Fields == null;

		public MultiGetOperationDescriptor()
		{
			Self.Index = Self.ClrType;
			Self.Type = Self.ClrType;
		}

		/// <summary>
		/// when rest.action.multi.allow_explicit_index is set to false you can use this constructor to generate a multiget operation
		/// with no index and type set
		/// <pre>
		/// See also: https://github.com/elasticsearch/elasticsearch/issues/3636
		/// </pre>
		/// </summary>
		/// <param name="initializeEmpty"></param>
		public MultiGetOperationDescriptor(bool allowExplicitIndex)
			: this()
		{
			if (allowExplicitIndex) return;
			Self.Index = null;
		}

		/// <summary>
		/// Manually set the index, default to the default index or the index set for the type on the connectionsettings.
		/// </summary>
		public MultiGetOperationDescriptor<T> Index(IndexName index) => Assign(a => a.Index = index);

		/// <summary>
		/// Manualy set the type to get the object from, default to whatever
		/// T will be inferred to if not passed.
		/// </summary>
		public MultiGetOperationDescriptor<T> Type(TypeName type) => Assign(a=> a.Type = type);

		public MultiGetOperationDescriptor<T> Id(Id id) => Assign(a => a.Id = id);

		/// <summary>
		/// Control how the document's source is loaded
		/// </summary>
		public MultiGetOperationDescriptor<T> Source(bool? sourceEnabled = true) => Assign(a => a.Source = sourceEnabled);

		/// <summary>
		/// Control how the document's source is loaded
		/// </summary>
		public MultiGetOperationDescriptor<T> Source(Func<SourceFilterDescriptor<T>, ISourceFilter> source) =>
			Assign(a => a.Source = new Union<bool, ISourceFilter>(source(new SourceFilterDescriptor<T>())));

		/// <summary>
		/// Set the routing for the get operation
		/// </summary>
		public MultiGetOperationDescriptor<T> Routing(string routing) => Assign(a => a.Routing = routing);

		/// <summary>
		/// Allows to selectively load specific fields for each document
		/// represented by a search hit. Defaults to load the internal _source field.
		/// </summary>
		public MultiGetOperationDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.Fields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public MultiGetOperationDescriptor<T> Fields(Fields fields) => Assign(a => a.Fields = fields);

	}
}
