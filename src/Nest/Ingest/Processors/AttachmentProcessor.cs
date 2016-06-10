using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<AttachmentProcessor>))]
	public interface IAttachmentProcessor : IProcessor
	{
		/// <summary>
		/// The field to get the base64 encoded field from
		/// </summary>
		[JsonProperty("field")]
		Field Field { get; set; }

		/// <summary>
		/// The field that will hold the attachment information
		/// </summary>
		[JsonProperty("target_field")]
		Field TargetField { get; set; }

		/// <summary>
		/// Properties to select to be stored. Can be content, title, name, author,
		/// keywords, date, content_type, content_length, language. Defaults to all
		/// </summary>
		[JsonProperty("properties")]
		IEnumerable<string> Properties { get; set; }

		/// <summary>
		/// The number of chars being used for extraction to prevent huge fields. Use -1 for no limit.
		/// Defaults to 100000.
		/// </summary>
		[JsonProperty("indexed_chars")]
		long? IndexedCharacters { get; set; }
	}

	public class AttachmentProcessor : ProcessorBase, IAttachmentProcessor
	{
		protected override string Name => "attachment";

		public Field Field { get; set; }

		public Field TargetField { get; set; }

		public IEnumerable<string> Properties { get; set; }

		public long? IndexedCharacters { get; set; }
	}

	public class AttachmentProcessorDescriptor<T>
: ProcessorDescriptorBase<AttachmentProcessorDescriptor<T>, IAttachmentProcessor>, IAttachmentProcessor
where T : class
	{
		protected override string Name => "attachment";

		Field IAttachmentProcessor.Field { get; set; }
		Field IAttachmentProcessor.TargetField { get; set; }
		IEnumerable<string> IAttachmentProcessor.Properties { get; set; }
		long? IAttachmentProcessor.IndexedCharacters { get; set; }

		public AttachmentProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public AttachmentProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		public AttachmentProcessorDescriptor<T> TargetField(Field field) => Assign(a => a.TargetField = field);

		public AttachmentProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.TargetField = objectPath);

		public AttachmentProcessorDescriptor<T> IndexedCharacters(long indexedCharacters) => Assign(a => a.IndexedCharacters = indexedCharacters);

		public AttachmentProcessorDescriptor<T> Properties(IEnumerable<string> properties) => Assign(a => a.Properties = properties);

		public AttachmentProcessorDescriptor<T> Properties(params string[] properties) => Assign(a => a.Properties = properties);
	}
}
