using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest_5_2_0;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Document.Single.Attachment
{
	public class Document
	{
		static Document()
		{
			using (var stream = typeof(Document).Assembly().GetManifestResourceStream("Tests.Document.Single.Attachment.Attachment_Test_Document.pdf"))
			{
				using (var memoryStream = new MemoryStream())
				{
					stream.CopyTo(memoryStream);
					TestPdfDocument = Convert.ToBase64String(memoryStream.ToArray());
				}
			}
		}

		public Nest_5_2_0.Attachment Attachment { get; set; }

		// Base 64 encoded version of Attachment_Test_Document.pdf
		public static string TestPdfDocument { get; }
	}

	public abstract class AttachmentApiTestsBase :
		ApiIntegrationTestBase<WritableCluster, IIndexResponse, IIndexRequest<Document>, IndexDescriptor<Document>, IndexRequest<Document>>
	{
		protected virtual Document Document => new Document { Attachment = new Nest_5_2_0.Attachment { Content = Attachment.Document.TestPdfDocument } };

		protected AttachmentApiTestsBase(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
		}

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var index = callUniqueValue.Value;
				var indexResponse = client.CreateIndex(index, c => c
					.Mappings(m => m
						.Map<Document>(mm => mm
							.Properties(p => p
								.Attachment(a => a
									.Name(n => n.Attachment)
									.AuthorField(d => d
										.Name(n => n.Attachment.Author)
										.Store()
									)
									.FileField(d => d
										.Name(n => n.Attachment.Content)
										.Store()
									)
									.ContentLengthField(d => d
										.Name(n => n.Attachment.ContentLength)
										.Store()
									)
									.ContentTypeField(d => d
										.Name(n => n.Attachment.ContentType)
										.Store()
									)
									.DateField(d => d
										.Name(n => n.Attachment.Date)
										.Store()
									)
									.KeywordsField(d => d
										.Name(n => n.Attachment.Keywords)
										.Store()
									)
									.LanguageField(d => d
										.Name(n => n.Attachment.Language)
										.Store()
									)
									.NameField(d => d
										.Name(n => n.Attachment.Name)
										.Store()
									)
									.TitleField(d => d
										.Name(n => n.Attachment.Title)
										.Store()
									)
								)
							)
						)
					)
				);

				if (!indexResponse.IsValid)
				{
					throw new Exception("Could not set up attachment index for test");
				}
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Index(this.Document, f),
			fluentAsync: (client, f) => client.IndexAsync(this.Document, f),
			request: (client, r) => client.Index(r),
			requestAsync: (client, r) => client.IndexAsync(r)
			);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 201;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override string UrlPath
			=> $"/{CallIsolatedValue}/document/{CallIsolatedValue}?refresh=true";

		protected override bool SupportsDeserialization => false;

		protected override IndexDescriptor<Document> NewDescriptor() => new IndexDescriptor<Document>(this.Document);

		protected override Func<IndexDescriptor<Document>, IIndexRequest<Document>> Fluent => s => s
			.Index(CallIsolatedValue)
			.Id(CallIsolatedValue)
			.Refresh(Refresh.True);

		protected override IndexRequest<Document> Initializer =>
			new IndexRequest<Document>(this.Document, CallIsolatedValue, id: CallIsolatedValue)
			{
				Refresh = Refresh.True,
			};

		protected override void ExpectResponse(IIndexResponse response)
		{
			response.ShouldBeValid();

			var searchResponse = Client.Search<Document>(s => s
				.Index(CallIsolatedValue)
				.Query(q => q
					.Match(m => m
						.Field(a => a.Attachment.Content)
						.Query("NEST mapper")
					)
				)
			);

			searchResponse.ShouldBeValid();
			searchResponse.Documents.Count().Should().Be(1);
		}
	}

	public class AttachmentApiTests : AttachmentApiTestsBase
	{

		public AttachmentApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
		}

		protected override object ExpectJson =>
			new
			{
				attachment = Document.TestPdfDocument
			};

		protected override void ExpectResponse(IIndexResponse response)
		{
			response.ShouldBeValid();

			var searchResponse = Client.Search<Document>(s => s
				.Index(CallIsolatedValue)
				.Query(q => q
					.Match(m => m
						.Field(a => a.Attachment.Content)
						.Query("NEST mapper")
					)
				)
			);

			searchResponse.ShouldBeValid();
			searchResponse.Documents.Count().Should().Be(1);

			searchResponse = Client.Search<Document>(s => s
				.Index(CallIsolatedValue)
				.StoredFields(f => f.Field(d => d.Attachment.ContentType))
				.Query(q => q
					.MatchAll()
				)
			);

			searchResponse.ShouldBeValid();
			searchResponse.Documents.Count().Should().Be(1);

			// mapper attachment extracts content type
			searchResponse.Hits.First()
				.Fields
				.ValueOf<Document, string>(d => d.Attachment.ContentType)
				.Should().Be("application/pdf");
		}
	}

	public class AttachmentExplicitWithMetadataApiTests : AttachmentApiTestsBase
	{
		protected override Document Document =>
			new Document
			{
				Attachment = new Nest_5_2_0.Attachment
				{
					ContentType = "application/pdf",
					Content = Document.TestPdfDocument,
					Name = "content name",
					Language = "en",
					DetectLanguage = true
				}
			};

		public AttachmentExplicitWithMetadataApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
		}

		protected override object ExpectJson =>
			new
			{
				attachment = new
				{
					_content = Document.TestPdfDocument,
					_content_type = "application/pdf",
					_name = "content name",
					_language = "en",
					_detect_language = true
				}
			};

		protected override void ExpectResponse(IIndexResponse response)
		{
			response.ShouldBeValid();

			// search on attachment name
			var searchResponse = Client.Search<Document>(s => s
				.Index(CallIsolatedValue)
				.Query(q => q
					.Match(m => m
						.Field(a => a.Attachment.Name)
						.Query("name")
					)
				)
			);

			searchResponse.ShouldBeValid();
			searchResponse.Documents.Count().Should().Be(1);

			// search on content type
			searchResponse = Client.Search<Document>(s => s
				.Index(CallIsolatedValue)
				.Query(q => q
					.Match(m => m
						.Field(a => a.Attachment.ContentType)
						.Query("pdf")
					)
				)
			);

			searchResponse.ShouldBeValid();
			searchResponse.Documents.Count().Should().Be(1);

			// search on language
			searchResponse = Client.Search<Document>(s => s
				.Index(CallIsolatedValue)
				.Query(q => q
					.Match(m => m
						.Field(a => a.Attachment.Language)
						.Query("en")
					)
				)
			);

			searchResponse.ShouldBeValid();
			searchResponse.Documents.Count().Should().Be(1);
		}
	}

	public class AttachmentDetectLanguageApiTests : AttachmentApiTestsBase
	{
		protected override Document Document =>
			new Document
			{
				Attachment = new Nest_5_2_0.Attachment
				{
					Content = Document.TestPdfDocument,
					DetectLanguage = true
				}
			};

		public AttachmentDetectLanguageApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
		}

		protected override object ExpectJson =>
			new
			{
				attachment = new
				{
					_content = Document.TestPdfDocument,
					_detect_language = true
				}
			};

		protected override void ExpectResponse(IIndexResponse response)
		{
			response.ShouldBeValid();

			// search on language (document is detected as French)
			var searchResponse = Client.Search<Document>(s => s
				.Index(CallIsolatedValue)
				.Query(q => q
					.Match(m => m
						.Field(a => a.Attachment.Language)
						.Query("fr")
					)
				)
			);

			searchResponse.ShouldBeValid();
			searchResponse.Documents.Count().Should().Be(1);
		}
	}

	public class AttachmentPopulatingFromHitApiTests : AttachmentApiTestsBase
	{
		protected override Document Document =>
			new Document
			{
				Attachment = new Nest_5_2_0.Attachment
				{
					Content = Document.TestPdfDocument,
					DetectLanguage = true
				}
			};

		public AttachmentPopulatingFromHitApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage)
		{
		}

		protected override object ExpectJson =>
			new
			{
				attachment = new
				{
					_content = Document.TestPdfDocument,
					_detect_language = true
				}
			};

		protected override void ExpectResponse(IIndexResponse response)
		{
			response.ShouldBeValid();

			// search on language (document is detected as French)
			var searchResponse = Client.Search<Document>(s => s
				.StoredFields(f => f
					.Field(d => d.Attachment.Name)
					.Field(d => d.Attachment.Author)
					.Field(d => d.Attachment.Content)
					.Field(d => d.Attachment.ContentLength)
					.Field(d => d.Attachment.ContentType)
					.Field(d => d.Attachment.Date)
					.Field(d => d.Attachment.Keywords)
					.Field(d => d.Attachment.Language)
					.Field(d => d.Attachment.Title)
				)
				.Index(CallIsolatedValue)
				.Query(q => q
					.MatchAll()
				)
			);

			var documents = new List<Document>();
			// try to set all the metadata
			foreach (var hit in searchResponse.Hits)
			{
				var document = new Document { Attachment = new Nest_5_2_0.Attachment() };
				document.Attachment.Name = hit.Fields.ValueOf<Document, string>(d => d.Attachment.Name);
				document.Attachment.Author = hit.Fields.ValueOf<Document, string>(d => d.Attachment.Author);
				document.Attachment.Content = hit.Fields.ValueOf<Document, string>(d => d.Attachment.Content);
				document.Attachment.ContentLength = hit.Fields.ValueOf<Document, long?>(d => d.Attachment.ContentLength);
				document.Attachment.ContentType = hit.Fields.ValueOf<Document, string>(d => d.Attachment.ContentType);
				document.Attachment.Date = hit.Fields.ValueOf<Document, DateTime?>(d => d.Attachment.Date);
				document.Attachment.Keywords = hit.Fields.ValueOf<Document, string>(d => d.Attachment.Keywords);
				document.Attachment.Language = hit.Fields.ValueOf<Document, string>(d => d.Attachment.Language);
				document.Attachment.Title = hit.Fields.ValueOf<Document, string>(d => d.Attachment.Title);
				documents.Add(document);
			}

			documents.Should().NotBeEmpty();
			documents.Count.Should().Be(1);

			var firstDocument = documents[0];

			// This is the only metadata that can be extracted from the sample doc
			firstDocument.Attachment.Title.Should().Be("Attachment Test Document");
			firstDocument.Attachment.ContentType.Should().Be("application/pdf");
			firstDocument.Attachment.Author.Should().Be("Russ Cam");
			firstDocument.Attachment.Keywords.Should().Be("nest,test,document");
			firstDocument.Attachment.Date.Should().Be(new DateTime(2016, 12, 08, 3, 5, 13, DateTimeKind.Utc));
			firstDocument.Attachment.ContentLength.Should().Be(315745);
			firstDocument.Attachment.Content.Should().Contain("mapper-attachment support");
			firstDocument.Attachment.Language.Should().Be("fr");
		}
	}
}
