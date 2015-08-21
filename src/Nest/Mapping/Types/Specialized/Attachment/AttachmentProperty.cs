using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IAttachmentProperty : IElasticsearchProperty
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

	public class AttachmentProperty : ElasticsearchProperty, IAttachmentProperty
	{
		public AttachmentProperty() : base("attachment") { }

		internal AttachmentProperty(AttachmentAttribute attribute) : base("attachment", attribute) { }

		public IStringProperty AuthorField
		{
			get { return Fields["author"] as IStringProperty; }
			set { Fields["author"] = value; }
		}

		public INumberProperty ContentLengthField
		{
			get { return Fields["content_length"] as INumberProperty; }
			set { Fields["content_length"] = value; }
		}

		public IStringProperty ContentTypeField
		{
			get { return Fields["content_type"] as IStringProperty; }
			set { Fields["content_type"] = value; }
		}

		public IDateProperty DateField
		{
			get { return Fields["date"] as IDateProperty; }
			set { Fields["date"] = value; }
		}

		public IStringProperty FileField
		{
			get { return Fields["file"] as IStringProperty; }
			set { Fields["file"] = value; }
		}

		public IStringProperty KeywordsField
		{
			get { return Fields["keywords"] as IStringProperty; }
			set { Fields["keywords"] = value; }
		}

		public IStringProperty LanguageField
		{
			get { return Fields["language"] as IStringProperty; }
			set { Fields["language"] = value; }
		}

		public IStringProperty NameField
		{
			get { return Fields["name"] as IStringProperty; }
			set { Fields["name"] = value; }
		}

		public IStringProperty TitleField
		{
			get { return Fields["title"] as IStringProperty; }
			set { Fields["title"] = value; }
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

		private IDictionary<FieldName, IElasticsearchProperty> _Fields { get { return ((IAttachmentProperty)this).Fields; } }

		public AttachmentPropertyDescriptor() : base("attachment") { }

		private AttachmentPropertyDescriptor<T> SetMetadataField<TDescriptor, TInterface>(Func<TDescriptor, TInterface> selector, string fieldName)
			where TDescriptor : TInterface
			where TInterface : IElasticsearchProperty
		{
			selector.ThrowIfNull(nameof(selector));
			var type = selector(Activator.CreateInstance<TDescriptor>());
			_Fields[fieldName] = type;
			return this;
		}

		public AttachmentPropertyDescriptor<T> DateField(Func<DatePropertyDescriptor<T>, IDateProperty> selector) => 
			SetMetadataField(selector, "date");

		public AttachmentPropertyDescriptor<T> TitleField(Func<StringPropertyDescriptor<T>, IStringProperty> selector) =>
			SetMetadataField(selector, "title");

		public AttachmentPropertyDescriptor<T> NameField(Func<StringPropertyDescriptor<T>, IStringProperty> selector) =>
			SetMetadataField(selector, "name");

		public AttachmentPropertyDescriptor<T> FileField(Func<StringPropertyDescriptor<T>, IStringProperty> selector) =>
			SetMetadataField(selector, "file");

		public AttachmentPropertyDescriptor<T> AuthorField(Func<StringPropertyDescriptor<T>, IStringProperty> selector) =>
			SetMetadataField(selector, "author");

		public AttachmentPropertyDescriptor<T> KeywordsField(Func<StringPropertyDescriptor<T>, IStringProperty> selector) =>
			SetMetadataField(selector, "keywords");

		public AttachmentPropertyDescriptor<T> ContentTypeField(Func<StringPropertyDescriptor<T>, IStringProperty> selector) =>
			SetMetadataField(selector, "content_type");

		public AttachmentPropertyDescriptor<T> ContentLengthField(Func<StringPropertyDescriptor<T>, IStringProperty> selector) =>
			SetMetadataField(selector, "content_length");

		public AttachmentPropertyDescriptor<T> LanguageField(Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			SetMetadataField(selector, "language");
	}
}