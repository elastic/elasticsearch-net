using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Converts a JSON string into a structured JSON object.
	/// </summary>
	[DataContract]
	[JsonConverter(typeof(ProcessorJsonConverter<JsonProcessor>))]
	public interface IJsonProcessor : IProcessor
	{
		/// <summary>
		/// Flag that forces the serialized json to be injected into the top level of the document.
		/// <see cref="TargetField" /> must not be set when this option is chosen.
		/// </summary>
		[DataMember(Name ="add_to_root")]
		bool? AddToRoot { get; set; }

		/// <summary>
		/// Field holding json as a string
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// The field to insert the converted structured object into
		/// </summary>
		[DataMember(Name ="target_field")]
		Field TargetField { get; set; }
	}

	/// <inheritdoc />
	public class JsonProcessor : ProcessorBase, IJsonProcessor
	{
		/// <inheritdoc />
		public bool? AddToRoot { get; set; }

		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public Field TargetField { get; set; }

		protected override string Name => "json";
	}

	/// <inheritdoc />
	public class JsonProcessorDescriptor<T>
		: ProcessorDescriptorBase<JsonProcessorDescriptor<T>, IJsonProcessor>, IJsonProcessor
		where T : class
	{
		protected override string Name => "json";
		bool? IJsonProcessor.AddToRoot { get; set; }

		Field IJsonProcessor.Field { get; set; }
		Field IJsonProcessor.TargetField { get; set; }

		/// <inheritdoc />
		public JsonProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		/// <inheritdoc />
		public JsonProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		/// <inheritdoc />
		public JsonProcessorDescriptor<T> TargetField(Field field) => Assign(a => a.TargetField = field);

		/// <inheritdoc />
		public JsonProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.TargetField = objectPath);

		/// <inheritdoc />
		public JsonProcessorDescriptor<T> AddToRoot(bool? addToRoot = true) => Assign(a => a.AddToRoot = addToRoot);
	}
}
