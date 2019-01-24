using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Drops the document without raising any errors. This is useful to prevent the document from getting indexed based on some condition.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<DropProcessor>))]
	public interface IDropProcessor : IProcessor
	{
	}

	/// <inheritdoc cref="IDropProcessor"/>
	public class DropProcessor : ProcessorBase, IDropProcessor
	{
		protected override string Name => "drop";
	}

	/// <inheritdoc cref="IDropProcessor"/>
	public class DropProcessorDescriptor : ProcessorDescriptorBase<DropProcessorDescriptor, IDropProcessor>, IDropProcessor
	{
		protected override string Name => "drop";
	}
}
