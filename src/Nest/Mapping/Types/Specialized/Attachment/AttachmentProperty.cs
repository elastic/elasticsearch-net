using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Nest
{
	[Obsolete("Removed in Elasticsearch 6.0, please consider using the ingest-attachment plugin.")]
	[JsonObject(MemberSerialization.OptIn)]
	public interface IAttachmentProperty : IDocValuesProperty
	{
		ITextProperty AuthorField { get; set; }
		ITextProperty ContentField { get; set; }
		INumberProperty ContentLengthField { get; set; }
		ITextProperty ContentTypeField { get; set; }
		IDateProperty DateField { get; set; }
		ITextProperty KeywordsField { get; set; }
		ITextProperty LanguageField { get; set; }
		ITextProperty NameField { get; set; }
		ITextProperty TitleField { get; set; }
	}

	[Obsolete("Removed in Elasticsearch 6.0, please consider using the ingest-attachment plugin.")]
	[DebuggerDisplay("{DebugDisplay}")]
	public class AttachmentProperty : DocValuesPropertyBase, IAttachmentProperty
	{
		public AttachmentProperty() : base(FieldType.Attachment) { }

		public ITextProperty AuthorField
		{
			get => Dictionary["author"] as ITextProperty;
			set => Dictionary["author"] = value;
		}

		public ITextProperty ContentField
		{
			get => Dictionary["content"] as ITextProperty;
			set => Dictionary["content"] = value;
		}

		public INumberProperty ContentLengthField
		{
			get => Dictionary["content_length"] as INumberProperty;
			set => Dictionary["content_length"] = value;
		}

		public ITextProperty ContentTypeField
		{
			get => Dictionary["content_type"] as ITextProperty;
			set => Dictionary["content_type"] = value;
		}

		public IDateProperty DateField
		{
			get => Dictionary["date"] as IDateProperty;
			set => Dictionary["date"] = value;
		}

		public ITextProperty KeywordsField
		{
			get => Dictionary["keywords"] as ITextProperty;
			set => Dictionary["keywords"] = value;
		}

		public ITextProperty LanguageField
		{
			get => Dictionary["language"] as ITextProperty;
			set => Dictionary["language"] = value;
		}

		public ITextProperty NameField
		{
			get => Dictionary["name"] as ITextProperty;
			set => Dictionary["name"] = value;
		}

		public ITextProperty TitleField
		{
			get => Dictionary["title"] as ITextProperty;
			set => Dictionary["title"] = value;
		}

		private IDictionary<PropertyName, IProperty> Dictionary => Fields ?? (Fields = new Properties());
	}

	[Obsolete("Removed in Elasticsearch 6.0, please consider using the ingest-attachment plugin.")]
	[DebuggerDisplay("{DebugDisplay}")]
	public class AttachmentPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<AttachmentPropertyDescriptor<T>, IAttachmentProperty, T>, IAttachmentProperty
		where T : class
	{
		public AttachmentPropertyDescriptor() : base(FieldType.Attachment) { }

		ITextProperty IAttachmentProperty.AuthorField { get; set; }
		ITextProperty IAttachmentProperty.ContentField { get; set; }
		INumberProperty IAttachmentProperty.ContentLengthField { get; set; }
		ITextProperty IAttachmentProperty.ContentTypeField { get; set; }
		IDateProperty IAttachmentProperty.DateField { get; set; }

		private IDictionary<PropertyName, IProperty> Dictionary => Self.Fields ?? (Self.Fields = new Properties());
		ITextProperty IAttachmentProperty.KeywordsField { get; set; }
		ITextProperty IAttachmentProperty.LanguageField { get; set; }
		ITextProperty IAttachmentProperty.NameField { get; set; }
		ITextProperty IAttachmentProperty.TitleField { get; set; }

		private AttachmentPropertyDescriptor<T> SetMetadataField<TDescriptor, TInterface>(Func<TDescriptor, TInterface> selector, string fieldName)
			where TDescriptor : TInterface, new()
			where TInterface : IProperty
		{
			selector.ThrowIfNull(nameof(selector));
			var type = selector(new TDescriptor());
			Dictionary[fieldName] = type;
			return this;
		}

		public AttachmentPropertyDescriptor<T> DateField(Func<DatePropertyDescriptor<T>, IDateProperty> selector) =>
			SetMetadataField(selector, "date");

		public AttachmentPropertyDescriptor<T> TitleField(Func<TextPropertyDescriptor<T>, ITextProperty> selector) =>
			SetMetadataField(selector, "title");

		public AttachmentPropertyDescriptor<T> NameField(Func<TextPropertyDescriptor<T>, ITextProperty> selector) =>
			SetMetadataField(selector, "name");

		public AttachmentPropertyDescriptor<T> FileField(Func<TextPropertyDescriptor<T>, ITextProperty> selector) =>
			SetMetadataField(selector, "content");

		public AttachmentPropertyDescriptor<T> AuthorField(Func<TextPropertyDescriptor<T>, ITextProperty> selector) =>
			SetMetadataField(selector, "author");

		public AttachmentPropertyDescriptor<T> KeywordsField(Func<TextPropertyDescriptor<T>, ITextProperty> selector) =>
			SetMetadataField(selector, "keywords");

		public AttachmentPropertyDescriptor<T> ContentTypeField(Func<TextPropertyDescriptor<T>, ITextProperty> selector) =>
			SetMetadataField(selector, "content_type");

		public AttachmentPropertyDescriptor<T> ContentLengthField(Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetMetadataField(selector, "content_length");

		public AttachmentPropertyDescriptor<T> LanguageField(Func<TextPropertyDescriptor<T>, ITextProperty> selector) =>
			SetMetadataField(selector, "language");
	}
}
