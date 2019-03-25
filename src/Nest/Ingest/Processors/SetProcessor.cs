using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Sets one field and associates it with the specified value.
	/// If the field already exists, its value will be replaced with the provided one.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<SetProcessor>))]
	public interface ISetProcessor : IProcessor
	{
		/// <summary>
		/// The field to insert, upsert, or update. Supports template snippets.
		/// </summary>
		[JsonProperty("field")]
		Field Field { get; set; }

		/// <summary>
		/// The value to be set for the field. Supports template snippets.
		/// </summary>
		[JsonProperty("value")]
		[JsonConverter(typeof(SourceValueWriteConverter))]
		object Value { get; set; }

		/// <summary>
		/// If processor will update fields with pre-existing non-null-valued field.
		/// When set to false, such fields will not be touched.
		/// Default is <c>true</c>
		/// </summary>
		[JsonProperty("override")]
		bool? Override { get; set; }
	}

	/// <inheritdoc cref="ISetProcessor" />
	public class SetProcessor : ProcessorBase, ISetProcessor
	{
		/// <inheritdoc />
		public Field Field { get; set; }
		/// <inheritdoc />
		public object Value { get; set; }
		/// <inheritdoc />
		public bool? Override { get; set; }
		protected override string Name => "set";
	}

	/// <inheritdoc cref="ISetProcessor" />
	public class SetProcessorDescriptor<T> : ProcessorDescriptorBase<SetProcessorDescriptor<T>, ISetProcessor>, ISetProcessor
		where T : class
	{
		protected override string Name => "set";
		Field ISetProcessor.Field { get; set; }
		object ISetProcessor.Value { get; set; }
		bool? ISetProcessor.Override { get; set; }

		/// <inheritdoc cref="ISetProcessor.Field"/>
		public SetProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		/// <inheritdoc cref="ISetProcessor.Field"/>
		public SetProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		/// <inheritdoc cref="ISetProcessor.Value"/>
		public SetProcessorDescriptor<T> Value<TValue>(TValue value) => Assign(a => a.Value = value);

		/// <inheritdoc cref="ISetProcessor.Override"/>
		public SetProcessorDescriptor<T> Override(bool? @override = true) => Assign(a => a.Override = @override);
	}
}
