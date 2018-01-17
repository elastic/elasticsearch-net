using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

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

		/// <summary>
		/// If `true` and `field` does not exist, the processor quietly exits without modifying the document
		/// </summary>
		[JsonProperty("ignore_missing")]
		bool? IgnoreMissing { get; set; }
	}

	/// <summary>
	/// The ingest attachment plugin lets Elasticsearch extract file attachments in common formats
	/// (such as PPT, XLS, and PDF) by using the Apache text extraction library Tika.
	/// You can use the ingest attachment plugin as a replacement for the mapper attachment plugin.
	/// </summary>
	/// <remarks>
	/// Requires the Ingest Attachment Processor Plugin to be installed on the cluster.
	/// </remarks>
	public class AttachmentProcessor : ProcessorBase, IAttachmentProcessor
	{
		protected override string Name => "attachment";

		/// <summary>
		/// The field to get the base64 encoded field from
		/// </summary>
		public Field Field { get; set; }

		/// <summary>
		/// The field that will hold the attachment information
		/// </summary>
		public Field TargetField { get; set; }

		/// <summary>
		/// Properties to select to be stored. Can be content, title, name, author,
		/// keywords, date, content_type, content_length, language. Defaults to all
		/// </summary>
		public IEnumerable<string> Properties { get; set; }

		/// <summary>
		/// The number of chars being used for extraction to prevent huge fields. Use -1 for no limit.
		/// Defaults to 100000.
		/// </summary>
		public long? IndexedCharacters { get; set; }

		/// <inheritdoc/>
		public bool? IgnoreMissing { get; set; }
	}

	/// <summary>
	/// The ingest attachment plugin lets Elasticsearch extract file attachments in common formats
	/// (such as PPT, XLS, and PDF) by using the Apache text extraction library Tika.
	/// You can use the ingest attachment plugin as a replacement for the mapper attachment plugin.
	/// </summary>
	/// <remarks>
	/// Requires the Ingest Attachment Processor Plugin to be installed on the cluster.
	/// </remarks>
	public class AttachmentProcessorDescriptor<T>
		: ProcessorDescriptorBase<AttachmentProcessorDescriptor<T>, IAttachmentProcessor>, IAttachmentProcessor
		where T : class
	{
		protected override string Name => "attachment";

		Field IAttachmentProcessor.Field { get; set; }
		Field IAttachmentProcessor.TargetField { get; set; }
		IEnumerable<string> IAttachmentProcessor.Properties { get; set; }
		long? IAttachmentProcessor.IndexedCharacters { get; set; }
		bool? IAttachmentProcessor.IgnoreMissing { get; set; }

		/// <summary>
		/// The field to get the base64 encoded field from
		/// </summary>
		public AttachmentProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		/// <summary>
		/// The field to get the base64 encoded field from
		/// </summary>
		public AttachmentProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		/// <summary>
		/// The field that will hold the attachment information
		/// </summary>
		public AttachmentProcessorDescriptor<T> TargetField(Field field) => Assign(a => a.TargetField = field);

		/// <summary>
		/// The field that will hold the attachment information
		/// </summary>
		public AttachmentProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.TargetField = objectPath);

		/// <summary>
		/// The number of chars being used for extraction to prevent huge fields. Use -1 for no limit.
		/// Defaults to 100000.
		/// </summary>
		public AttachmentProcessorDescriptor<T> IndexedCharacters(long? indexedCharacters) => Assign(a => a.IndexedCharacters = indexedCharacters);

		/// <inheritdoc/>
		public AttachmentProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(a => a.IgnoreMissing = ignoreMissing);

		/// <summary>
		/// Properties to select to be stored. Can be content, title, name, author,
		/// keywords, date, content_type, content_length, language. Defaults to all
		/// </summary>
		public AttachmentProcessorDescriptor<T> Properties(IEnumerable<string> properties) => Assign(a => a.Properties = properties);

		/// <summary>
		/// Properties to select to be stored. Can be content, title, name, author,
		/// keywords, date, content_type, content_length, language. Defaults to all
		/// </summary>
		public AttachmentProcessorDescriptor<T> Properties(params string[] properties) => Assign(a => a.Properties = properties);
	}
}
