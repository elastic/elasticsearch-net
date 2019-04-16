using System;
using System.Linq.Expressions;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Drops the document without raising any errors. This is useful to prevent the document from getting indexed based on some condition.
	/// </summary>
	[InterfaceDataContract]
	public interface IDropProcessor : IProcessor { }

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
