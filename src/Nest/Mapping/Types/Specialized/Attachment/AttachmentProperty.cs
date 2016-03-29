using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IAttachmentProperty : IProperty
	{
		IDateProperty DateField { get; set; }
		ITextProperty TitleField { get; set; }
		ITextProperty NameField { get; set; }
		ITextProperty FileField { get; set; }
		ITextProperty AuthorField { get; set; }
		ITextProperty KeywordsField { get; set; }
		ITextProperty ContentTypeField { get; set; }
		INumberProperty ContentLengthField { get; set; }
		ITextProperty LanguageField { get; set; }
	}

	public class AttachmentProperty : PropertyBase, IAttachmentProperty
	{
		public AttachmentProperty() : base("attachment") { }

		private IDictionary<PropertyName, IProperty> Dictionary => this.Fields ?? (this.Fields = new Properties());

		public ITextProperty AuthorField
		{
			get { return Dictionary["author"] as ITextProperty; }
			set { Dictionary["author"] = value; }
		}

		public INumberProperty ContentLengthField
		{
			get { return Dictionary["content_length"] as INumberProperty; }
			set { Dictionary["content_length"] = value; }
		}

		public ITextProperty ContentTypeField
		{
			get { return Dictionary["content_type"] as ITextProperty; }
			set { Dictionary["content_type"] = value; }
		}

		public IDateProperty DateField
		{
			get { return Dictionary["date"] as IDateProperty; }
			set { Dictionary["date"] = value; }
		}

		public ITextProperty FileField
		{
			get { return Dictionary["content"] as ITextProperty; }
			set { Dictionary["content"] = value; }
		}

		public ITextProperty KeywordsField
		{
			get { return Dictionary["keywords"] as ITextProperty; }
			set { Dictionary["keywords"] = value; }
		}

		public ITextProperty LanguageField
		{
			get { return Dictionary["language"] as ITextProperty; }
			set { Dictionary["language"] = value; }
		}

		public ITextProperty NameField
		{
			get { return Dictionary["name"] as ITextProperty; }
			set { Dictionary["name"] = value; }
		}

		public ITextProperty TitleField
		{
			get { return Dictionary["title"] as ITextProperty; }
			set { Dictionary["title"] = value; }
		}
	}

	public class AttachmentPropertyDescriptor<T>
		: PropertyDescriptorBase<AttachmentPropertyDescriptor<T>, IAttachmentProperty, T>, IAttachmentProperty
		where T : class
	{
		IDateProperty IAttachmentProperty.DateField { get; set; }
		ITextProperty IAttachmentProperty.TitleField { get; set; }
		ITextProperty IAttachmentProperty.NameField { get; set; }
		ITextProperty IAttachmentProperty.FileField { get; set; }
		ITextProperty IAttachmentProperty.AuthorField { get; set; }
		ITextProperty IAttachmentProperty.KeywordsField { get; set; }
		ITextProperty IAttachmentProperty.ContentTypeField { get; set; }
		INumberProperty IAttachmentProperty.ContentLengthField { get; set; }
		ITextProperty IAttachmentProperty.LanguageField { get; set; }

		private IDictionary<PropertyName, IProperty> Dictionary => Self.Fields ?? (Self.Fields = new Properties());

		public AttachmentPropertyDescriptor() : base("attachment") { }

		private AttachmentPropertyDescriptor<T> SetMetadataField<TDescriptor, TInterface>(Func<TDescriptor, TInterface> selector, string fieldName)
			where TDescriptor : TInterface, new()
			where TInterface : IProperty
		{
			selector.ThrowIfNull(nameof(selector));
			var type = selector(new TDescriptor());
			this.Dictionary[fieldName] = type;
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

		[Obsolete("Use ContentLengthField(Func<NumberPropertyDescriptor<T>, INumberProperty> selector)")]
		public AttachmentPropertyDescriptor<T> ContentLengthField(Func<TextPropertyDescriptor<T>, ITextProperty> selector) =>
			SetMetadataField(selector, "content_length");

		public AttachmentPropertyDescriptor<T> ContentLengthField(Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetMetadataField(selector, "content_length");

		[Obsolete("Use LanguageField(Func<TextPropertyDescriptor<T>, ITextProperty> selector)")]
		public AttachmentPropertyDescriptor<T> LanguageField(Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetMetadataField(selector, "language");

		public AttachmentPropertyDescriptor<T> LanguageField(Func<TextPropertyDescriptor<T>, ITextProperty> selector) =>
			SetMetadataField(selector, "language");
	}
}
