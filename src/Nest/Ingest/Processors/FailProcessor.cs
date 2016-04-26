using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IFailProcessor : IProcessor
	{
		[JsonProperty("message")]
		string Message { get; set; }
	}

	public class FailProcessor : ProcessorBase, IFailProcessor
	{
		protected override string Name => "fail";
		public string Message { get; set; }
	}

	public class FailProcessorDescriptor<T>
		: ProcessorDescriptorBase<FailProcessorDescriptor<T>, IFailProcessor>, IFailProcessor
		where T : class
	{
		protected override string Name => "fail";

		string IFailProcessor.Message { get; set; }

		public FailProcessorDescriptor<T> Message(string message) => Assign(a => a.Message = message);
	}
}
