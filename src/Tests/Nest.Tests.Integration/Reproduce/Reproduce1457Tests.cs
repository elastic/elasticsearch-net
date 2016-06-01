using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Nest.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce1457Tests : IntegrationTests
	{
		[Test]
		[SkipVersion("0 - 1.3.9", "Analyzer type required on ES <= 1.3")]
		public void AnalyzerMissingType_ShouldNotThrow_And_DeserializeAsCustom()
		{
			var indexName = "issue1457-repro";

			if (this.Client.IndexExists(indexName).Exists)
				this.Client.DeleteIndex(indexName);

			var putTemplate = this.Client.PutTemplate("issue1457template", t => t
				.Template("issue1457-*")
				.Settings(s => s
					.Add("analysis", BuildAnalysisSettings())
				)
			);

			putTemplate.IsValid.Should().BeTrue();

			var createIndex = this.Client.CreateIndex(indexName);
			createIndex.IsValid.Should().BeTrue();

			var getIndex = this.Client.GetIndex(g => g.Index(indexName));
			getIndex.Indices.Should().NotBeNull();
			var index = getIndex.Indices[indexName];
			index.Analysis.Should().NotBeNull();
			var analyzers = index.Analysis.Analyzers;
			analyzers.Should().NotBeNull();
			analyzers["standardplus"].Type.Should().Be("custom");
			analyzers["typename"].Type.Should().Be("custom");
			analyzers["whitespace_lower"].Type.Should().Be("custom");
			analyzers["version_search"].Type.Should().Be("custom");
			analyzers["version_index"].Type.Should().Be("custom");
			analyzers["email"].Type.Should().Be("custom");
			analyzers["comma_whitespace"].Type.Should().Be("pattern");
			getIndex.IsValid.Should().BeTrue();
		}

		private object BuildAnalysisSettings()
		{
			return new
			{
				filter = new {
					version_pad2 = new {
						pattern = "(\\.|^)(\\d{2})(?=\\.|$)",
						type = "pattern_replace",
						replacement = "$1000$2"
					},
					version_pad3 = new {
						type = "pattern_replace",
						pattern = "(\\.|^)(\\d{3})(?=\\.|$)",
						replacement = "$100$2"
					},
					version_pad1 = new {
						pattern = "(\\.|^)(\\d{1})(?=\\.|$)",
						type = "pattern_replace",
						replacement = "$10000$2"
					},
					version_pad4 = new {
						type = "pattern_replace",
						pattern = "(\\.|^)(\\d{4})(?=\\.|$)",
						replacement = "$10$2"
					},
					version = new {
						type = "pattern_capture",
						patterns = new[] {
							"^(\\d+)\\.",
							"^(\\d+\\.\\d+)",
							"^(\\d+\\.\\d+\\.\\d+)"
						}
					},
					typename = new {
						type = "pattern_capture",
						patterns = new[] { 
							"\\.(\\w+)"
						}
					},
					email = new {
						type = "pattern_capture",
						patterns = new[] {
							"(\\w+)",
							"(\\p{L}+)",
							"(\\d+)",
							"(.+)@",
							"@(.+)"
						}
					}
				},
				analyzer = new
				{
					comma_whitespace = new
					{
						type = "pattern",
						pattern = @"[,\s]+"
					},
					email = new
					{
						tokenizer = "keyword",
						filter = new[] {
                            "email",
                            "lowercase",
                            "unique"
                        }
					},
					version_index = new
					{
						tokenizer = "whitespace",
						filter = new[] {
                            "version_pad1",
                            "version_pad2",
                            "version_pad3",
                            "version_pad4",
                            "version",
                            "lowercase",
                            "unique"
                        }
					},
					version_search = new
					{
						tokenizer = "whitespace",
						filter = new[] {
                            "version_pad1",
                            "version_pad2",
                            "version_pad3",
                            "version_pad4",
                            "lowercase"
                        }
					},
					whitespace_lower = new
					{
						tokenizer = "whitespace",
						filter = new[] { "lowercase" }
					},
					typename = new
					{
						tokenizer = "whitespace",
						filter = new[] {
                            "typename",
                            "lowercase",
                            "unique"
                        }
					},
					standardplus = new
					{
						tokenizer = "whitespace",
						filter = new[] {
                            "standard",
                            "typename",
                            "lowercase",
                            "stop",
                            "unique"
                        }
					}
				}
			};
		}
	}
}
