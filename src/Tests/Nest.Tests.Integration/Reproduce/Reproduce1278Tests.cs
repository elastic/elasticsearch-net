using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce1278Tests
	{
		[Test]
		public void UnmappedPropertiesAreNotIgnored()
		{
			var client = new ElasticClient();
			var index = "issue1278";
			if (client.IndexExists(index).Exists)
				client.DeleteIndex(index);
			var result = client.CreateIndex(index, c => c
				.Settings(s => s
					.Add("analysis.analyzer.folding.tokenizer", "standard")
					.Add("analysis.analyzer.folding.filter.0", "lowercase")
					.Add("analysis.analyzer.folding.filter.1", "asciifolding")
				)
				.AddMapping<ElasticVideo>(m => m
					.Type("video")
					.Properties(props => props
						.String(s => s
							.Name("CountryOfOrigin")
							.Index(FieldIndexOption.NotAnalyzed)
						)
						.String(s => s
							.Name("LanguageOfOrigin")
							.Index(FieldIndexOption.NotAnalyzed)
						)
						.MultiField(s => s
							.Name("Title")
							.Fields(fs => fs
								.String(x => x.Name("Title").Analyzer("english"))
								.String(y => y.Name(t => t.Title.Suffix("folded")).Analyzer("folding"))
								.String(y => y.Name(t => t.Title.Suffix("raw")).Index(FieldIndexOption.NotAnalyzed)))
						)
						.MultiField(s => s
							.Name("TitleEnglish")
							.Fields(fs => fs
								.String(x => x.Name("TitleEnglish").Analyzer("english"))
								.String(y => y.Name(t => t.TitleEnglish.Suffix("folded")).Analyzer("folding")))

						)
						.MultiField(s => s
							.Name("Description")
							.Fields(fs => fs
								.String(x => x.Name("Description").Analyzer("english"))
								.String(y => y.Name(t => t.Description.Suffix("folded")).Analyzer("folding")))

						)

						.MultiField(s => s
							.Name("DescriptionEnglish")
							.Fields(fs => fs
								.String(x => x.Name("DescriptionEnglish").Analyzer("english"))
								.String(y => y.Name(t => t.DescriptionEnglish.Suffix("folded")).Analyzer("folding")))

						)
						.String(s => s
							.Name("VideoImage")
							.Index(FieldIndexOption.No)
						)
						.String(s => s
							.Name("ClientId")
							.Index(FieldIndexOption.NotAnalyzed)
						)
						.Number(s => s
							.Name("ViewCount")
							.Index(NonStringIndexOption.NotAnalyzed)
						)
						.Number(s => s
							.Name("LikesCount")
							.Index(NonStringIndexOption.NotAnalyzed)
						)
						.Number(s => s
							.Name("DislikesCount")
							.Index(NonStringIndexOption.NotAnalyzed)
						)
						.Date(s => s
							.Name("Published")
							.Index(NonStringIndexOption.NotAnalyzed)
						)
						.Date(s => s
							.Name("CreatedDate")
							.Index(NonStringIndexOption.NotAnalyzed)
						)
						.String(s => s
							.Name("WatchLater")
							.Index(FieldIndexOption.NotAnalyzed)
						)
						.String(s => s
							.Name("Favourite")
							.Index(FieldIndexOption.NotAnalyzed)
						)
						.String(s => s
							.Name("Recommendations")
							.Index(FieldIndexOption.NotAnalyzed)
						)
						.Number(s => s
							.Name("TranscriptionStatus")
							.Index(NonStringIndexOption.NotAnalyzed)
						)
						.Number(s => s
							.Name("UploadSource")
							.Index(NonStringIndexOption.NotAnalyzed)
						)
						.Boolean(s => s
							.Name("FromProject")
							.Index(NonStringIndexOption.NotAnalyzed)
						)
						.Boolean(s => s
							.Name("IsSearchable")
							.Index(NonStringIndexOption.NotAnalyzed)
						)
						.MultiField(s => s
							.Name("NativeTranscription")
							.Fields(fs => fs
								.String(x => x.Name("NativeTranscription").Analyzer("english"))
								.String(
									y => y.Name(t => t.NativeTranscription.Suffix("folded")).Analyzer("folding")))

						)
						.MultiField(s => s
							.Name("EnglishTranscription")
							.Fields(fs => fs
								.String(x => x.Name("EnglishTranscription").Analyzer("english"))
								.String(
									y => y.Name(t => t.EnglishTranscription.Suffix("folded")).Analyzer("folding")))

						)
						.MultiField(s => s
							.Name("Tags")
							.Fields(fs => fs
								.String(x => x.Name("Tags").Analyzer("english"))
								.String(y => y.Name(t => t.Tags.Suffix("folded")).Analyzer("folding")))

						)
						.MultiField(s => s
							.Name("Comments")
							.Fields(fs => fs
								.String(x => x.Name("Comments").Analyzer("english"))
								.String(y => y.Name(t => t.Comments.Suffix("folded")).Analyzer("folding")))

						)
						.String(s => s
							.Name("ClientId")
							.Index(FieldIndexOption.NotAnalyzed)
						)
					)
				)
			);

			var getMappingResult = client.GetMapping<ElasticVideo>(m => m
				.Index(index)
				.Type("video")
			);
			getMappingResult.Mapping.Properties.Should().NotContainKey("objectId");
		}
	}

	public class ElasticSearchBaseEntity
	{
		public string ObjectId { get; set; }
	}

	public class ElasticVideo : ElasticSearchBaseEntity
	{
		public string CountryOfOrigin { get; set; }
		public string LanguageOfOrigin { get; set; }
		public string Title { get; set; }
		public string TitleEnglish { get; set; }
		public string Description { get; set; }
		public string DescriptionEnglish { get; set; }
		public string VideoImage { get; set; }
		public int ViewCount { get; set; }
		public int LikesCount { get; set; }
		public int DislikesCount { get; set; }
		public DateTime? Published { get; set; }
		public DateTime? CreatedDate { get; set; }
		public List<string> WatchLater { get; set; }
		public List<string> Favourite { get; set; }
		public bool IsSearchable { get; set; }
		public bool FromProject { get; set; }
		public string NativeTranscription { get; set; }
		public string EnglishTranscription { get; set; }
		public List<string> Tags { get; set; }
		public List<string> Comments { get; set; }
		public List<string> Attributes { get; set; }
		public List<string> Recommendations { get; set; }
		public string ClientId { get; set; }

		public ElasticVideo()
		{
			Tags = new List<string>();
			WatchLater = new List<string>();
			Favourite = new List<string>();
			Comments = new List<string>();
			Attributes = new List<string>();
		}
	}
}
