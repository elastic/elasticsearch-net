using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Raises an exception. This is useful for when you expect a pipeline to
	/// fail and want to relay a specific message to the requester.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<FailProcessor>))]
	public interface IFailProcessor : IProcessor
	{
		/// <summary>
		/// The error message thrown by the processor. Supports template snippets.
		/// </summary>
		[JsonProperty("message")]
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
		public FailProcessorDescriptor Message(string message) => Assign(a => a.Message = message);
	}
}
