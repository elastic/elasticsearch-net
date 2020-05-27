using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The source of the data for the transform.
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(TransformSource))]
	public interface ITransformSource
	{
		/// <summary>
		/// The source indices for the transform. It can be a single index, an index pattern, an array of indices, or an array of index patterns
		/// </summary>
		[DataMember(Name = "index")]
		[JsonFormatter(typeof(IndicesFormatter))]
		Indices Index { get; set; }

		/// <summary>
		/// A query clause that retrieves a subset of data from the source index
		/// </summary>
		[DataMember(Name = "query")]
		QueryContainer Query { get; set; }
	}

	/// <inheritdoc />
	public class TransformSource
		: ITransformSource
	{
		/// <inheritdoc />
		public Indices Index { get; set; }

		/// <inheritdoc />
		public QueryContainer Query { get; set; }
	}

	/// <inheritdoc cref="ITransformSource" />
	public class TransformSourceDescriptor<T> : DescriptorBase<TransformSourceDescriptor<T>, ITransformSource>, ITransformSource where T : class
	{
		Indices ITransformSource.Index { get; set; }
		QueryContainer ITransformSource.Query { get; set; }

		/// <inheritdoc cref="ITransformSource.Index" />
		public TransformSourceDescriptor<T> Index(Indices indices) => Assign(indices, (a, v) => a.Index = v);

		/// <inheritdoc cref="ITransformSource.Index" />
		public TransformSourceDescriptor<T> Index<TOther>() => Assign(typeof(TOther), (a, v) => a.Index = v);

		/// <inheritdoc cref="ITransformSource.Query" />
		public TransformSourceDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(selector, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<T>()));

	}
}
