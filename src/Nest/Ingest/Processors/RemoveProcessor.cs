using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IRemoveProcessor : IProcessor
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// If <c>true</c> and <see cref="Field" /> does not exist or is null,
		/// the processor quietly exits without modifying the document. Default is <c>false</c>
		/// </summary>
		[DataMember(Name ="ignore_missing")]
		bool? IgnoreMissing { get; set; }
	}

	/// <inheritdoc cref="IRemoveProcessor" />
	public class RemoveProcessor : ProcessorBase, IRemoveProcessor
	{
		/// <inheritdoc cref="IRemoveProcessor.Field" />
		public Field Field { get; set; }

		/// <inheritdoc cref="IRemoveProcessor.IgnoreMissing" />
		public bool? IgnoreMissing { get; set; }

		protected override string Name => "remove";
	}

	/// <inheritdoc cref="IRemoveProcessor" />
	public class RemoveProcessorDescriptor<T>
		: ProcessorDescriptorBase<RemoveProcessorDescriptor<T>, IRemoveProcessor>, IRemoveProcessor
		where T : class
	{
		protected override string Name => "remove";

		Field IRemoveProcessor.Field { get; set; }
		bool? IRemoveProcessor.IgnoreMissing { get; set; }

		/// <inheritdoc cref="IRemoveProcessor.Field" />
		public RemoveProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IRemoveProcessor.Field" />
		public RemoveProcessorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="IRemoveProcessor.IgnoreMissing" />
		public RemoveProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(ignoreMissing, (a, v) => a.IgnoreMissing = v);
	}
}
