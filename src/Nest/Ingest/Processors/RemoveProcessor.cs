using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[JsonConverter(typeof(ProcessorJsonConverter<RemoveProcessor>))]
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
		public RemoveProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		/// <inheritdoc cref="IRemoveProcessor.Field" />
		public RemoveProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) => Assign(a => a.Field = objectPath);

		/// <inheritdoc cref="IRemoveProcessor.IgnoreMissing" />
		public RemoveProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(a => a.IgnoreMissing = ignoreMissing);
	}
}
