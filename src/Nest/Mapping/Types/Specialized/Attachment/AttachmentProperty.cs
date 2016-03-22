using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IAttachmentProperty : IProperty
	{
		IDateProperty DateField { get; set; }
		IStringProperty TitleField { get; set; }
		IStringProperty NameField { get; set; }
		IStringProperty FileField { get; set; }
		IStringProperty AuthorField { get; set; }
		IStringProperty KeywordsField { get; set; }
		IStringProperty ContentTypeField { get; set; }
		INumberProperty ContentLengthField { get; set; }
		IStringProperty LanguageField { get; set; }
	}

	public class AttachmentProperty : PropertyBase, IAttachmentProperty
	{
		public AttachmentProperty() : base("attachment") { }

		private IDictionary<PropertyName, IProperty> Dictionary => this.Fields ?? (this.Fields = new Properties());

		public IStringProperty AuthorField
		{
			get { return Dictionary["author"] as IStringProperty; }
			set { Dictionary["author"] = value; }
		}

		public INumberProperty ContentLengthField
		{
			get { return Dictionary["content_length"] as INumberProperty; }
			set { Dictionary["content_length"] = value; }
		}

		public IStringProperty ContentTypeField
		{
			get { return Dictionary["content_type"] as IStringProperty; }
			set { Dictionary["content_type"] = value; }
		}

		public IDateProperty DateField
		{
			get { return Dictionary["date"] as IDateProperty; }
			set { Dictionary["date"] = value; }
		}

		public IStringProperty FileField
		{
			get { return Dictionary["content"] as IStringProperty; }
			set { Dictionary["content"] = value; }
		}

		public IStringProperty KeywordsField
		{
			get { return Dictionary["keywords"] as IStringProperty; }
			set { Dictionary["keywords"] = value; }
		}

		public IStringProperty LanguageField
		{
			get { return Dictionary["language"] as IStringProperty; }
			set { Dictionary["language"] = value; }
		}

		public IStringProperty NameField
		{
			get { return Dictionary["name"] as IStringProperty; }
			set { Dictionary["name"] = value; }
		}

		public IStringProperty TitleField
		{
			get { return Dictionary["title"] as IStringProperty; }
			set { Dictionary["title"] = value; }
		}
	}

	public class AttachmentPropertyDescriptor<T> 
		: PropertyDescriptorBase<AttachmentPropertyDescriptor<T>, IAttachmentProperty, T>, IAttachmentProperty
		where T : class
	{
		IDateProperty IAttachmentProperty.DateField { get; set; }
		IStringProperty IAttachmentProperty.TitleField { get; set; }
		IStringProperty IAttachmentProperty.NameField { get; set; }
		IStringProperty IAttachmentProperty.FileField { get; set; }
		IStringProperty IAttachmentProperty.AuthorField { get; set; }
		IStringProperty IAttachmentProperty.KeywordsField { get; set; }
		IStringProperty IAttachmentProperty.ContentTypeField { get; set; }
		INumberProperty IAttachmentProperty.ContentLengthField { get; set; }
		IStringProperty IAttachmentProperty.LanguageField { get; set; }

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

		public AttachmentPropertyDescriptor<T> TitleField(Func<StringPropertyDescriptor<T>, IStringProperty> selector) =>
			SetMetadataField(selector, "title");

		public AttachmentPropertyDescriptor<T> NameField(Func<StringPropertyDescriptor<T>, IStringProperty> selector) =>
			SetMetadataField(selector, "name");

		public AttachmentPropertyDescriptor<T> FileField(Func<StringPropertyDescriptor<T>, IStringProperty> selector) =>
			SetMetadataField(selector, "content");

		public AttachmentPropertyDescriptor<T> AuthorField(Func<StringPropertyDescriptor<T>, IStringProperty> selector) =>
			SetMetadataField(selector, "author");

		public AttachmentPropertyDescriptor<T> KeywordsField(Func<StringPropertyDescriptor<T>, IStringProperty> selector) =>
			SetMetadataField(selector, "keywords");

		public AttachmentPropertyDescriptor<T> ContentTypeField(Func<StringPropertyDescriptor<T>, IStringProperty> selector) =>
			SetMetadataField(selector, "content_type");

		[Obsolete("Use ContentLengthField(Func<NumberPropertyDescriptor<T>, INumberProperty> selector)")]
		public AttachmentPropertyDescriptor<T> ContentLengthField(Func<StringPropertyDescriptor<T>, IStringProperty> selector) =>
			SetMetadataField(selector, "content_length");

		public AttachmentPropertyDescriptor<T> ContentLengthField(Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetMetadataField(selector, "content_length");

		[Obsolete("Use LanguageField(Func<StringPropertyDescriptor<T>, IStringProperty> selector)")]
		public AttachmentPropertyDescriptor<T> LanguageField(Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetMetadataField(selector, "language");

		public AttachmentPropertyDescriptor<T> LanguageField(Func<StringPropertyDescriptor<T>, IStringProperty> selector) =>
			SetMetadataField(selector, "language");
	}
}