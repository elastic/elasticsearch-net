using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using Xunit;

namespace Tests.Mapping.Types.Specialized.Attachment
{
	public class AttachmentTest
	{
		public string File { get; set; }

		public string Author { get; set; }

		public long ContentLength { get; set; }

		public string ContentType { get; set; }

		public DateTime Date { get; set; }

		public string Keywords { get; set; }

		public string Language { get; set; }

		public string Name { get; set; }

		public string Title { get; set; }
	}

	public class AttachmentMappingTests : TypeMappingTestBase<AttachmentTest>
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

		protected override Func<PropertiesDescriptor<AttachmentTest>, IPromise<IProperties>> FluentProperties => p => p
			.Attachment(a => a
				//.Fields(s => s)
				.Name(n => n.File)
				.AuthorField(d => d
					.Name(n => n.Author)
				)
				.FileField(d => d
					.Name(n => n.File)
				)
				.ContentLengthField((NumberPropertyDescriptor<AttachmentTest> d) => d
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
				.LanguageField((StringPropertyDescriptor<AttachmentTest> d) => d
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

