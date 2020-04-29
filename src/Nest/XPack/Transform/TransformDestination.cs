using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The destination for a transform.
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(TransformDestination))]
	public interface ITransformDestination
	{
		/// <summary>
		/// The destination index for the transform.
		/// </summary>
		[DataMember(Name = "index")]
		public IndexName Index { get; set; }

		/// <summary>
		/// The unique identifier for a pipeline.
		/// </summary>
		[DataMember(Name = "pipeline")]
		public string Pipeline { get; set; }
	}

	/// <inheritdoc />
	public class TransformDestination
		: ITransformDestination
	{
		/// <inheritdoc />
		public IndexName Index { get; set; }

		/// <inheritdoc />
		public string Pipeline { get; set; }
	}

	/// <inheritdoc cref="ITransformDestination"/>
	public class TransformDestinationDescriptor : DescriptorBase<TransformDestinationDescriptor, ITransformDestination>, ITransformDestination
	{
		IndexName ITransformDestination.Index { get; set; }
		string ITransformDestination.Pipeline { get; set; }

		/// <inheritdoc cref="ITransformDestination.Index"/>
		public TransformDestinationDescriptor Index(IndexName index) => Assign(index, (a, v) => a.Index = v);

		/// <inheritdoc cref="ITransformDestination.Index"/>
		public TransformDestinationDescriptor Index<T>() => Assign(typeof(T), (a, v) => a.Index = v);

		/// <inheritdoc cref="ITransformDestination.Pipeline"/>
		public TransformDestinationDescriptor Pipeline(string pipeline) => Assign(pipeline, (a, v) => a.Pipeline = v);
	}
}
