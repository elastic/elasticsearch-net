using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IAttachmentType : IElasticType
	{
		IDateType DateField { get; set; }
		IStringType TitleField { get; set; }
		IStringType NameField { get; set; }
		IStringType FileField { get; set; }
		IStringType AuthorField { get; set; }
		IStringType KeywordsField { get; set; }
		IStringType ContentTypeField { get; set; }
		INumberType ContentLengthField { get; set; }
		IStringType LanguageField { get; set; }
	}

	public class AttachmentType : ElasticType, IAttachmentType
	{
		public AttachmentType() : base("attachment") { }

		internal AttachmentType(AttachmentAttribute attribute) : base("attachment", attribute) { }

		public IStringType AuthorField
		{
			get { return Fields["author"] as IStringType; }
			set { Fields["author"] = value; }
		}

		public INumberType ContentLengthField
		{
			get { return Fields["content_length"] as INumberType; }
			set { Fields["content_length"] = value; }
		}

		public IStringType ContentTypeField
		{
			get { return Fields["content_type"] as IStringType; }
			set { Fields["content_type"] = value; }
		}

		public IDateType DateField
		{
			get { return Fields["date"] as IDateType; }
			set { Fields["date"] = value; }
		}

		public IStringType FileField
		{
			get { return Fields["file"] as IStringType; }
			set { Fields["file"] = value; }
		}

		public IStringType KeywordsField
		{
			get { return Fields["keywords"] as IStringType; }
			set { Fields["keywords"] = value; }
		}

		public IStringType LanguageField
		{
			get { return Fields["language"] as IStringType; }
			set { Fields["language"] = value; }
		}

		public IStringType NameField
		{
			get { return Fields["name"] as IStringType; }
			set { Fields["name"] = value; }
		}

		public IStringType TitleField
		{
			get { return Fields["title"] as IStringType; }
			set { Fields["title"] = value; }
		}
	}

	public class AttachmentTypeDescriptor<T> 
		: TypeDescriptorBase<AttachmentTypeDescriptor<T>, IAttachmentType, T>, IAttachmentType
		where T : class
	{
		IDateType IAttachmentType.DateField { get; set; }
		IStringType IAttachmentType.TitleField { get; set; }
		IStringType IAttachmentType.NameField { get; set; }
		IStringType IAttachmentType.FileField { get; set; }
		IStringType IAttachmentType.AuthorField { get; set; }
		IStringType IAttachmentType.KeywordsField { get; set; }
		IStringType IAttachmentType.ContentTypeField { get; set; }
		INumberType IAttachmentType.ContentLengthField { get; set; }
		IStringType IAttachmentType.LanguageField { get; set; }

		private IDictionary<FieldName, IElasticType> _Fields { get { return ((IAttachmentType)this).Fields; } }

		private AttachmentTypeDescriptor<T> SetMetadataField<TDescriptor, TInterface>(Func<TDescriptor, TInterface> selector, string fieldName)
			where TDescriptor : TInterface
			where TInterface : IElasticType
		{
			selector.ThrowIfNull(nameof(selector));
			var type = selector(Activator.CreateInstance<TDescriptor>());
			_Fields[fieldName] = type;
			return this;
		}

		public AttachmentTypeDescriptor<T> DateField(Func<DateTypeDescriptor<T>, IDateType> selector) => 
			SetMetadataField(selector, "date");

		public AttachmentTypeDescriptor<T> TitleField(Func<StringTypeDescriptor<T>, IStringType> selector) =>
			SetMetadataField(selector, "title");

		public AttachmentTypeDescriptor<T> NameField(Func<StringTypeDescriptor<T>, IStringType> selector) =>
			SetMetadataField(selector, "name");

		public AttachmentTypeDescriptor<T> FileField(Func<StringTypeDescriptor<T>, IStringType> selector) =>
			SetMetadataField(selector, "file");

		public AttachmentTypeDescriptor<T> AuthorField(Func<StringTypeDescriptor<T>, IStringType> selector) =>
			SetMetadataField(selector, "author");

		public AttachmentTypeDescriptor<T> KeywordsField(Func<StringTypeDescriptor<T>, IStringType> selector) =>
			SetMetadataField(selector, "keywords");

		public AttachmentTypeDescriptor<T> ContentTypeField(Func<StringTypeDescriptor<T>, IStringType> selector) =>
			SetMetadataField(selector, "content_type");

		public AttachmentTypeDescriptor<T> ContentLengthField(Func<StringTypeDescriptor<T>, IStringType> selector) =>
			SetMetadataField(selector, "content_length");

		public AttachmentTypeDescriptor<T> LanguageField(Func<NumberTypeDescriptor<T>, INumberType> selector) =>
			SetMetadataField(selector, "language");
	}
}