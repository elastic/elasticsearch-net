using System;
using Elasticsearch.Net;

namespace Nest
{
	public class MultiGetOperation<T> : IMultiGetOperation
	{
		public MultiGetOperation(Id id)
		{
			Id = id;
			Index = typeof(T);
		}


		public object Document { get; set; }

		public Id Id { get; set; }

		public IndexName Index { get; set; }

		public string Routing { get; set; }

		public Union<bool, ISourceFilter> Source { get; set; }

		public Fields StoredFields { get; set; }

		public long? Version { get; set; }

		public VersionType? VersionType { get; set; }

		bool IMultiGetOperation.CanBeFlattened =>
			Index == null
			&& Routing == null
			&& Source == null
			&& StoredFields == null;

		Type IMultiGetOperation.ClrType => typeof(T);
	}

	public class MultiGetOperationDescriptor<T> : DescriptorBase<MultiGetOperationDescriptor<T>, IMultiGetOperation>, IMultiGetOperation
		where T : class
	{
		public MultiGetOperationDescriptor() => Self.Index = Self.ClrType;

		/// <summary>
		/// when rest.action.multi.allow_explicit_index is set to false you can use this constructor to generate a multiget operation
		/// with no index and type set
		/// <pre>
		/// See also: https://github.com/elasticsearch/elasticsearch/issues/3636
		/// </pre>
		/// </summary>
		public MultiGetOperationDescriptor(bool allowExplicitIndex)
			: this()
		{
			if (allowExplicitIndex) return;

			Self.Index = null;
		}

		bool IMultiGetOperation.CanBeFlattened =>
			Self.Index == null
			&& Self.Routing == null
			&& Self.Source == null
			&& Self.StoredFields == null;

		Type IMultiGetOperation.ClrType => typeof(T);
		Id IMultiGetOperation.Id { get; set; }
		IndexName IMultiGetOperation.Index { get; set; }
		string IMultiGetOperation.Routing { get; set; }
		Union<bool, ISourceFilter> IMultiGetOperation.Source { get; set; }
		Fields IMultiGetOperation.StoredFields { get; set; }
		long? IMultiGetOperation.Version { get; set; }
		VersionType? IMultiGetOperation.VersionType { get; set; }

		/// <summary>
		/// Manually set the index, default to the default index or the index set for the type on the connectionsettings.
		/// </summary>
		public MultiGetOperationDescriptor<T> Index(IndexName index) => Assign(a => a.Index = index);

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
		public MultiGetOperationDescriptor<T> StoredFields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.StoredFields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public MultiGetOperationDescriptor<T> StoredFields(Fields fields) => Assign(a => a.StoredFields = fields);

		public MultiGetOperationDescriptor<T> Version(long? version) => Assign(a => a.Version = version);

		public MultiGetOperationDescriptor<T> VersionType(VersionType? versionType) => Assign(a => a.VersionType = versionType);
	}
}
