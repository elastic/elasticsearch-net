using System;
using System.Collections.Generic;
using System.IO;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Document.Single.Index
{
	public class TestDocument
	{
		static TestDocument()
		{
			using (var stream = typeof(TestDocument).Assembly().GetManifestResourceStream("Tests.Document.Single.Index.Attachment_Test_Document.pdf"))
			{
				using (var memoryStream = new MemoryStream())
				{
					stream.CopyTo(memoryStream);
					TestPdfDocument = Convert.ToBase64String(memoryStream.ToArray());
				}
			}
		}

		// Base 64 encoded version of Attachment_Test_Document.pdf
		public static string TestPdfDocument { get; }
	}
	public class IndexIngestAttachmentApiTests :
		ApiIntegrationTestBase<IntrusiveOperationCluster, IIndexResponse,
			IIndexRequest<IndexIngestAttachmentApiTests.IngestedAttachment>,
			IndexDescriptor<IndexIngestAttachmentApiTests.IngestedAttachment>,
			IndexRequest<IndexIngestAttachmentApiTests.IngestedAttachment>>
	{
		public class IngestedAttachment
		{
			public int Id { get; set; }
			public string Content { get; set; }
			public Attachment Attachment { get; set; }
		}

		private static string Content => TestDocument.TestPdfDocument;

		private static string PipelineId { get; } = "pipeline-" + Guid.NewGuid().ToString("N").Substring(0, 8);

		public IndexIngestAttachmentApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var value in values)
			{
				var index = value.Value;

				client.CreateIndex(index, c => c
					.Mappings(m => m
						.Map<IngestedAttachment>(mm => mm
							.Properties(p => p
								.Text(s => s
									.Name(f => f.Content)
								)
								.Object<Attachment>(o => o
									.Name(f => f.Attachment)
								)
							)
						)
					)
				);
			}

			client.PutPipeline(new PutPipelineRequest(PipelineId)
			{
				Description = "Attachment pipeline test",
				Processors = new List<IProcessor>
				{
					new AttachmentProcessor
					{
						Field = "content",
						TargetField = "attachment"
					}
				}
			});
		}

		private IngestedAttachment Document => new IngestedAttachment
		{
			Id = 1,
			Content = Content
		};

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Index<IngestedAttachment>(this.Document, f),
			fluentAsync: (client, f) => client.IndexAsync<IngestedAttachment>(this.Document, f),
			request: (client, r) => client.Index(r),
			requestAsync: (client, r) => client.IndexAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 201;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override string UrlPath
			=> $"/{CallIsolatedValue}/ingestedattachment/1?refresh=true&pipeline={PipelineId}";

		protected override bool SupportsDeserialization => false;

		protected override bool NoClientSerializeOfExpected => (bool)Dependant(false, true);

		protected override object ExpectJson =>
			Dependant(
				new {id = 1, content = Content},
				new {attachment = (object) null, id = 1, content = Content}
			);


		protected override IndexDescriptor<IngestedAttachment> NewDescriptor() => new IndexDescriptor<IngestedAttachment>(this.Document);

		protected override Func<IndexDescriptor<IngestedAttachment>, IIndexRequest<IngestedAttachment>> Fluent => s => s
			.Index(CallIsolatedValue)
			.Refresh(Refresh.True)
			.Pipeline(PipelineId);

		protected override IndexRequest<IngestedAttachment> Initializer =>
			new IndexRequest<IngestedAttachment>(this.Document, CallIsolatedValue)
			{
				Refresh = Refresh.True,
				Pipeline = PipelineId
			};

		protected override void ExpectResponse(IIndexResponse response)
		{
			response.ShouldBeValid();

			var getResponse = this.Client.Get<IngestedAttachment>(response.Id, g => g.Index(CallIsolatedValue));

			getResponse.ShouldBeValid();
			getResponse.Source.Should().NotBeNull();

			getResponse.Source.Attachment.Should().NotBeNull();

			var attachment = getResponse.Source.Attachment;

			attachment.Title.Should().Be("Attachment Test Document");
			attachment.Keywords.Should().Be("nest,test,document");
			attachment.Date.Should().Be(new DateTime(2016, 12, 08, 3, 5, 13, DateTimeKind.Utc));
			attachment.ContentType.Should().Be("application/pdf");
			attachment.Author.Should().Be("Russ Cam");
			attachment.Language.Should().Be("fr");
			attachment.ContentLength.Should().Be(96);
			attachment.Content.Should().Contain("mapper-attachment support");
		}
	}
}
