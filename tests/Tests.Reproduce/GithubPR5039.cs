// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using static Tests.Core.Serialization.SerializationTestHelper;

namespace Tests.Reproduce
{
	public class GithubPR5039
	{
		public class MyCustomTokenizer : ITokenizer
		{
			public string Type => "my_custom_tok";
			public string Version { get; set; }

			public string Y { get; set; }
		}

		[U]
		public void CustomTokenizer()
		{
			var tokenizer = Object(new MyCustomTokenizer() { Version = "x", Y = "z" })
				.RoundTrips(new { type = "my_custom_tok", version = "x", y = "z" });
			tokenizer.Type.Should().Be("my_custom_tok");
			tokenizer.Version.Should().Be("x");
			tokenizer.Y.Should().Be("z");
		}

		public class DynamicSynonymTokenFilter : ITokenFilter
		{
			public bool? Expand { get; set; }
			public SynonymFormat? Format { get; set; }
			public bool? Lenient { get; set; }
			public IEnumerable<string> Synonyms { get; set; }

			[DataMember(Name = "synonyms_path")]
			public string SynonymsPath { get; set; }

			public string Tokenizer { get; set; }
			public bool? Updateable { get; set; }
			public string Type { get; } = "dynamic_synonym";
			public string Version { get; set; }
			public int? Interval { get; set; }
		}

		[U]
		public void CustomTokenFilter()
		{
			var tokenizer = Object(new DynamicSynonymTokenFilter() { Version = "x", SynonymsPath = "/root/access" })
				.RoundTrips(new { type = "dynamic_synonym", version = "x", synonyms_path = "/root/access" });
			tokenizer.Type.Should().Be("dynamic_synonym");
			tokenizer.Version.Should().Be("x");
			tokenizer.SynonymsPath.Should().Be("/root/access");
		}

		[U]
		public void CreateIndex()
		{
			var client = TestClient.DefaultInMemoryClient;

			var response = client.Indices.Create("my-index", i => i
				.Settings(s => s
					.Analysis(a => a
						.TokenFilters(t => t
							.UserDefined("mytf",
								new DynamicSynonymTokenFilter
								{
									SynonymsPath = "https://my-synonym-server-url-that-not-is-relevant",
									Updateable = true,
									Lenient = true,
									Interval = 60
								})
						)
						.Tokenizers(t => t
							.UserDefined("myt", new MyCustomTokenizer { Y = "yy" })
						)
					)
				)
			);

			Expect(new
				{
					settings = new
					{
						analysis = new
						{
							filter = new
							{
								mytf = new
								{
									lenient = true,
									synonyms_path = "https://my-synonym-server-url-that-not-is-relevant",
									updateable = true,
									type = "dynamic_synonym",
									interval = 60
								}
							},
							tokenizer = new { myt = new { type = "my_custom_tok", y = "yy" } }
						}
					}
				})
				.NoRoundTrip()
				.FromRequest(response);
		}
	}
}
