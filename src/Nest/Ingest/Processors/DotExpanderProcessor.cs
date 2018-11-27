using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Expands a field with dots into an object field.
	/// This processor allows fields with dots in the name to be accessible by other processors in the pipeline.
	/// Otherwise these fields can’t be accessed by any processor.
	/// </summary>
	[DataContract]
	[JsonConverter(typeof(ProcessorJsonConverter<DotExpanderProcessor>))]
	public interface IDotExpanderProcessor : IProcessor
	{
		/// <summary>
		/// The field to expand into an object field
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// The field that contains the field to expand.
		/// Only required if the field to expand is part another object field,
		/// because the field option can only understand leaf fields.
		/// </summary>
		[DataMember(Name ="path")]
		string Path { get; set; }
	}

	/// <summary>
	/// Expands a field with dots into an object field.
	/// This processor allows fields with dots in the name to be accessible by other processors in the pipeline.
	/// Otherwise these fields can’t be accessed by any processor.
	/// </summary>
	public class DotExpanderProcessor : ProcessorBase, IDotExpanderProcessor
	{
		/// <summary>
		/// The field to expand into an object field
		/// </summary>
		[DataMember(Name ="field")]
		public Field Field { get; set; }

		/// <summary>
		/// The field that contains the field to expand.
		/// Only required if the field to expand is part another object field,
		/// because the field option can only understand leaf fields.
		/// </summary>
		[DataMember(Name ="path")]
		public string Path { get; set; }

		protected override string Name => "dot_expander";
	}

	/// <summary>
	/// Expands a field with dots into an object field.
	/// This processor allows fields with dots in the name to be accessible by other processors in the pipeline.
	/// Otherwise these fields can’t be accessed by any processor.
	/// </summary>
	public class DotExpanderProcessorDescriptor<T>
		: ProcessorDescriptorBase<DotExpanderProcessorDescriptor<T>, IDotExpanderProcessor>, IDotExpanderProcessor
		where T : class
	{
		protected override string Name => "dot_expander";

		Field IDotExpanderProcessor.Field { get; set; }
		string IDotExpanderProcessor.Path { get; set; }

		/// <summary>
		/// The field to expand into an object field
		/// </summary>
		public DotExpanderProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		/// <summary>
		/// The field to expand into an object field
		/// </summary>
		public DotExpanderProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		/// <summary>
		/// The field that contains the field to expand.
		/// Only required if the field to expand is part another object field,
		/// because the field option can only understand leaf fields.
		/// </summary>
		public DotExpanderProcessorDescriptor<T> Path(string path) => Assign(a => a.Path = path);
	}
}
