namespace Nest
{
	public class AttachmentAttribute : ElasticsearchDocValuesPropertyAttributeBase, IAttachmentProperty
	{
		public AttachmentAttribute() : base(FieldType.Attachment) { }

		ITextProperty IAttachmentProperty.AuthorField { get; set; }
		INumberProperty IAttachmentProperty.ContentLengthField { get; set; }
		ITextProperty IAttachmentProperty.ContentTypeField { get; set; }
		IDateProperty IAttachmentProperty.DateField { get; set; }
		ITextProperty IAttachmentProperty.ContentField { get; set; }
		ITextProperty IAttachmentProperty.KeywordsField { get; set; }
		ITextProperty IAttachmentProperty.LanguageField { get; set; }
		ITextProperty IAttachmentProperty.NameField { get; set; }
		ITextProperty IAttachmentProperty.TitleField { get; set; }
	}
}
