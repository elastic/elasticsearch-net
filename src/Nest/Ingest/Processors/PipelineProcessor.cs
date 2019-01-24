using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary> Executes another pipeline.</summary>
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<PipelineProcessor>))]
	public interface IPipelineProcessor : IProcessor
	{
		//TODO 7.x: this property clashes with the Name property on the IProcessor, need to rename base in master
		/// <summary>The name of the pipeline to execute. </summary>
		[JsonProperty("name")]
		string ProcessorName { get; set; }
	}

	/// <inheritdoc cref="IPipelineProcessor" />
	public class PipelineProcessor : ProcessorBase, IPipelineProcessor
	{
		/// <inheritdoc cref="IPipelineProcessor.ProcessorName"/>
		[JsonProperty("name")]
		public string ProcessorName { get; set; }

		internal const string ProcessorTypeName = "pipeline";
		protected override string Name => ProcessorTypeName;
	}

	/// <inheritdoc cref="IPipelineProcessor" />
	public class PipelineProcessorDescriptor
		: ProcessorDescriptorBase<PipelineProcessorDescriptor, IPipelineProcessor>, IPipelineProcessor
	{
		protected override string Name => PipelineProcessor.ProcessorTypeName;
		string IPipelineProcessor.ProcessorName { get; set; }

		/// <inheritdoc cref="IPipelineProcessor.ProcessorName"/>
		public PipelineProcessorDescriptor ProcessorName(string processorName) => Assign(a => a.ProcessorName = processorName);
	}
}
