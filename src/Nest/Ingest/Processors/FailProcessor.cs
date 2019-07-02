using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Raises an exception. This is useful for when you expect a pipeline to
	/// fail and want to relay a specific message to the requester.
	/// </summary>
	[InterfaceDataContract]
	public interface IFailProcessor : IProcessor
	{
		/// <summary>
		/// The error message thrown by the processor. Supports template snippets.
		/// </summary>
		[DataMember(Name ="message")]
		string Message { get; set; }
	}

	/// <inheritdoc cref="IFailProcessor" />
	public class FailProcessor : ProcessorBase, IFailProcessor
	{
		/// <inheritdoc />
		public string Message { get; set; }
		protected override string Name => "fail";
	}

	/// <inheritdoc cref="IFailProcessor" />
	public class FailProcessorDescriptor
		: ProcessorDescriptorBase<FailProcessorDescriptor, IFailProcessor>, IFailProcessor
	{
		protected override string Name => "fail";

		string IFailProcessor.Message { get; set; }

		/// <inheritdoc cref="IFailProcessor.Message" />
		public FailProcessorDescriptor Message(string message) => Assign(message, (a, v) => a.Message = v);
	}
}
