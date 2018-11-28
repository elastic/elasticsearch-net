using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	/// <summary>
	/// The ingest attachment plugin lets Elasticsearch extract file attachments in common formats
	/// (such as PPT, XLS, and PDF) by using the Apache text extraction library Tika.
	/// You can use the ingest attachment plugin as a replacement for the mapper attachment plugin.
	/// </summary>
	/// <remarks>
	/// Requires the Ingest Attachment Processor Plugin to be installed on the cluster.
	/// </remarks>
	[InterfaceDataContract]
	[JsonFormatter(typeof(ProcessorFormatter<AttachmentProcessor>))]
	public interface IAttachmentProcessor : IProcessor
	{
		/// <summary> The field to get the base64 encoded field from </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }


		/// <summary> If `true` and `field` does not exist, the processor quietly exits without modifying the document </summary>
		[DataMember(Name ="ignore_missing")]
		bool? IgnoreMissing { get; set; }

		/// <summary>
		/// The number of chars being used for extraction to prevent huge fields. Use -1 for no limit.
		/// Defaults to 100000.
		/// </summary>
		[DataMember(Name ="indexed_chars")]
		long? IndexedCharacters { get; set; }

		/// <summary> Field name from which you can overwrite the number of chars being used for extraction. </summary>
		[DataMember(Name ="indexed_chars_field")]
		Field IndexedCharactersField { get; set; }

		/// <summary>
		/// Properties to select to be stored. Can be content, title, name, author,
		/// keywords, date, content_type, content_length, language. Defaults to all
		/// </summary>
		[DataMember(Name ="properties")]
		IEnumerable<string> Properties { get; set; }

		/// <summary> The field that will hold the attachment information </summary>
		[DataMember(Name ="target_field")]
		Field TargetField { get; set; }
	}

	/// <inheritdoc cref="IAttachmentProcessor" />
	public class AttachmentProcessor : ProcessorBase, IAttachmentProcessor
	{
		/// <inheritdoc cref="IAttachmentProcessor.Field" />
		public Field Field { get; set; }

		/// <inheritdoc />
		/// <inheritdoc cref="IAttachmentProcessor.IgnoreMissing" />
		public bool? IgnoreMissing { get; set; }

		/// <inheritdoc cref="IAttachmentProcessor.IndexedCharacters" />
		public long? IndexedCharacters { get; set; }

		/// <inheritdoc cref="IAttachmentProcessor.IndexedCharactersField" />
		public Field IndexedCharactersField { get; set; }

		/// <inheritdoc cref="IAttachmentProcessor.Properties" />
		public IEnumerable<string> Properties { get; set; }

		/// <inheritdoc cref="IAttachmentProcessor.TargetField" />
		public Field TargetField { get; set; }

		protected override string Name => "attachment";
	}

	/// <inheritdoc cref="IAttachmentProcessor" />
	public class AttachmentProcessorDescriptor<T>
		: ProcessorDescriptorBase<AttachmentProcessorDescriptor<T>, IAttachmentProcessor>, IAttachmentProcessor
		where T : class
	{
		protected override string Name => "attachment";

		Field IAttachmentProcessor.Field { get; set; }
		bool? IAttachmentProcessor.IgnoreMissing { get; set; }
		long? IAttachmentProcessor.IndexedCharacters { get; set; }
		Field IAttachmentProcessor.IndexedCharactersField { get; set; }
		IEnumerable<string> IAttachmentProcessor.Properties { get; set; }
		Field IAttachmentProcessor.TargetField { get; set; }

		/// <inheritdoc cref="IAttachmentProcessor.Field" />
		public AttachmentProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		/// <inheritdoc cref="IAttachmentProcessor.Field" />
		public AttachmentProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) => Assign(a => a.Field = objectPath);

		/// <inheritdoc cref="IAttachmentProcessor.TargetField" />
		public AttachmentProcessorDescriptor<T> TargetField(Field field) => Assign(a => a.TargetField = field);

		/// <inheritdoc cref="IAttachmentProcessor.TargetField" />
		public AttachmentProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) => Assign(a => a.TargetField = objectPath);

		/// <inheritdoc cref="IAttachmentProcessor.IndexedCharacters" />
		public AttachmentProcessorDescriptor<T> IndexedCharacters(long? indexedCharacters) => Assign(a => a.IndexedCharacters = indexedCharacters);

		/// <inheritdoc cref="IAttachmentProcessor.IndexedCharactersField" />
		public AttachmentProcessorDescriptor<T> IndexedCharactersField(Field field) => Assign(a => a.IndexedCharactersField = field);

		/// <inheritdoc cref="IAttachmentProcessor.IndexedCharactersField" />
		public AttachmentProcessorDescriptor<T> IndexedCharactersField(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.IndexedCharactersField = objectPath);

		/// <inheritdoc cref="IAttachmentProcessor.IgnoreMissing" />
		public AttachmentProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(a => a.IgnoreMissing = ignoreMissing);

		/// <inheritdoc cref="IAttachmentProcessor.Properties" />
		public AttachmentProcessorDescriptor<T> Properties(IEnumerable<string> properties) => Assign(a => a.Properties = properties);

		/// <inheritdoc cref="IAttachmentProcessor.Properties" />
		public AttachmentProcessorDescriptor<T> Properties(params string[] properties) => Assign(a => a.Properties = properties);
	}
}
