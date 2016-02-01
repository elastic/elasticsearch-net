namespace Nest
{
	public class AttachmentAttribute : ElasticsearchPropertyAttributeBase, IAttachmentProperty
	{
		IStringProperty IAttachmentProperty.AuthorField { get; set; }
		INumberProperty IAttachmentProperty.ContentLengthField { get; set; }
		IStringProperty IAttachmentProperty.ContentTypeField { get; set; }
		IDateProperty IAttachmentProperty.DateField { get; set; }
		IStringProperty IAttachmentProperty.FileField { get; set; }
		IStringProperty IAttachmentProperty.KeywordsField { get; set; }
		IStringProperty IAttachmentProperty.LanguageField { get; set; }
		IStringProperty IAttachmentProperty.NameField { get; set; }
		IStringProperty IAttachmentProperty.TitleField { get; set; }

		public AttachmentAttribute() : base("attachment") { }
	}
}
