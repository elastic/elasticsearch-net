using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IRenameProcessor : IProcessor
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="target_field")]
		Field TargetField { get; set; }

		/// <summary>
		/// If <c>true</c> and <see cref="Field" /> does not exist or is null,
		/// the processor quietly exits without modifying the document. Default is <c>false</c>
		/// </summary>
		[DataMember(Name = "ignore_missing")]
		bool? IgnoreMissing { get; set; }
	}

	public class RenameProcessor : ProcessorBase, IRenameProcessor
	{
		public Field Field { get; set; }

		public Field TargetField { get; set; }

		/// <inheritdoc cref="IRenameProcessor.IgnoreMissing" />
		public bool? IgnoreMissing { get; set; }

		protected override string Name => "rename";
	}

	public class RenameProcessorDescriptor<T>
		: ProcessorDescriptorBase<RenameProcessorDescriptor<T>, IRenameProcessor>, IRenameProcessor
		where T : class
	{
		protected override string Name => "rename";
		Field IRenameProcessor.Field { get; set; }
		Field IRenameProcessor.TargetField { get; set; }
		bool? IRenameProcessor.IgnoreMissing { get; set; }

		public RenameProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public RenameProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		public RenameProcessorDescriptor<T> TargetField(Field field) => Assign(a => a.TargetField = field);

		public RenameProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.TargetField = objectPath);


		/// <inheritdoc cref="IRemoveProcessor.IgnoreMissing" />
		public RenameProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(a => a.IgnoreMissing = ignoreMissing);
	}
}
