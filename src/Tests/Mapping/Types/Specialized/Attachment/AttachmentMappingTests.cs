using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using Xunit;
using Tests.Framework.MockData;

namespace Tests.Mapping.Types.Specialized.Attachment
{
	public class AttachmentMappingTests : TypeMappingTestBase<Tests.Framework.MockData.Attachment>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				file = new
				{
					type = "attachment",
					fields = new
					{
						author = new
						{
							type = "string"
						},
						content = new
						{
							type = "string"
						},
						content_length = new
						{
							type = "double"
						},
						content_type = new
						{
							type = "string"
						},
						date = new
						{
							type = "date"
						},
						keywords = new
						{
							type = "string"
						},
						language = new
						{
							type = "string",
							doc_values = true,
							index = "not_analyzed"
						},
						name = new
						{
							type = "string"
						},
						title = new
						{
							type = "string"
						}
					}
				}
			}
		};

		protected override void AttributeBasedSerializes()
		{
			// TODO: Implement
		}

		protected override Func<PropertiesDescriptor<Framework.MockData.Attachment>, IPromise<IProperties>> FluentProperties => p => p
			.Attachment(a => a
				//.Fields(s => s)
				.Name(n => n.File)
				.AuthorField(d => d
					.Name(n => n.Author)
				)
				.FileField(d => d
					.Name(n => n.File)
				)
				.ContentLengthField((NumberPropertyDescriptor<Framework.MockData.Attachment> d) => d
					.Name(n => n.ContentLength)
				)
				.ContentTypeField(d => d
					.Name(n => n.ContentType)
				)
				.DateField(d => d
					.Name(n => n.Date)
				)
				.KeywordsField(d => d
					.Name(n => n.Keywords)
				)
				.LanguageField((StringPropertyDescriptor<Framework.MockData.Attachment> d) => d
					.Name(n => n.Language)
					.DocValues()
					.NotAnalyzed()
				)
				.NameField(d => d
					.Name(n => n.Name)
				)
				.TitleField(d => d
					.Name(n => n.Title)
				)
			);
	}
}

