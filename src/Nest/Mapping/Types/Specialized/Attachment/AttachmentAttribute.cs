namespace Nest
{
	// TODO validate if these mappings still apply to 5.0
	public class AttachmentAttribute : ElasticsearchPropertyAttributeBase, IAttachmentProperty
	{
		ITextProperty IAttachmentProperty.AuthorField { get; set; }
		INumberProperty IAttachmentProperty.ContentLengthField { get; set; }
		ITextProperty IAttachmentProperty.ContentTypeField { get; set; }
		IDateProperty IAttachmentProperty.DateField { get; set; }
		ITextProperty IAttachmentProperty.FileField { get; set; }
		ITextProperty IAttachmentProperty.KeywordsField { get; set; }
		ITextProperty IAttachmentProperty.LanguageField { get; set; }
		ITextProperty IAttachmentProperty.NameField { get; set; }
		ITextProperty IAttachmentProperty.TitleField { get; set; }

		public AttachmentAttribute() : base("attachment") { }
	}
}
