using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IFailProcessor : IProcessor
	{
		[DataMember(Name ="message")]
		string Message { get; set; }
	}

	public class FailProcessor : ProcessorBase, IFailProcessor
	{
		public string Message { get; set; }
		protected override string Name => "fail";
	}

	public class FailProcessorDescriptor
		: ProcessorDescriptorBase<FailProcessorDescriptor, IFailProcessor>, IFailProcessor
	{
		protected override string Name => "fail";

		string IFailProcessor.Message { get; set; }

		public FailProcessorDescriptor Message(string message) => Assign(a => a.Message = message);
	}
}
