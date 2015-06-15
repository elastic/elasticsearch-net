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
