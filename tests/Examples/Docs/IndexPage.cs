// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Examples.Models;
using System.ComponentModel;

namespace Examples.Docs
{
	public class IndexPage : ExampleBase
	{

		[U]
		[Description("docs/index_.asciidoc:146")]
		public void Line146()
		{
			// tag::804a97ff4d0613e6568e4efb19c52021[]
			var putSettingsResponse = client.Cluster.PutSettings(s => s
				.Persistent(p => p
					.Add("action.auto_create_index", "twitter,index10,-index1*,+ind*")
				)
			);

			var putSettingsResponse2 = client.Cluster.PutSettings(s => s
				.Persistent(p => p
					.Add("action.auto_create_index", "false")
				)
			);

			var putSettingsResponse3 = client.Cluster.PutSettings(s => s
				.Persistent(p => p
					.Add("action.auto_create_index", "true")
				)
			);
			// end::804a97ff4d0613e6568e4efb19c52021[]

			putSettingsResponse.MatchesExample(@"PUT _cluster/settings
			{
			    ""persistent"": {
			        ""action.auto_create_index"": ""twitter,index10,-index1*,+ind*"" \<1>
			    }
			}");

			putSettingsResponse2.MatchesExample(@"PUT _cluster/settings
			{
			    ""persistent"": {
			        ""action.auto_create_index"": ""false"" \<2>
			    }
			}");

			putSettingsResponse3.MatchesExample(@"PUT _cluster/settings
			{
			    ""persistent"": {
			        ""action.auto_create_index"": ""true"" \<3>
			    }
			}");
		}

		[U]
		[Description("docs/index_.asciidoc:195")]
		public void Line195()
		{
			// tag::36818c6d9f434d387819c30bd9addb14[]
			var indexResponse = client.Index(new Tweet
			{
				User = "kimchy",
				PostDate = new DateTime(2009, 11, 15, 14, 12, 12),
				Message = "trying out Elasticsearch"
			},
				i => i
					.Index("twitter")
			);
			// end::36818c6d9f434d387819c30bd9addb14[]

			indexResponse.MatchesExample(@"POST twitter/_doc/
			{
			    ""user"" : ""kimchy"",
			    ""post_date"" : ""2009-11-15T14:12:12"",
			    ""message"" : ""trying out Elasticsearch""
			}");
		}

		[U]
		[Description("docs/index_.asciidoc:244")]
		public void Line244()
		{
			// tag::625dc94df1f9affb49a082fd99d41620[]
			var indexResponse = client.Index(new Tweet
			{
				User = "kimchy",
				PostDate = new DateTime(2009, 11, 15, 14, 12, 12),
				Message = "trying out Elasticsearch"
			},
				i => i
					.Index("twitter")
					.Routing("kimchy")
			);
			// end::625dc94df1f9affb49a082fd99d41620[]

			indexResponse.MatchesExample(@"POST twitter/_doc?routing=kimchy
			{
			    ""user"" : ""kimchy"",
			    ""post_date"" : ""2009-11-15T14:12:12"",
			    ""message"" : ""trying out Elasticsearch""
			}");
		}

		[U]
		[Description("docs/index_.asciidoc:366")]
		public void Line366()
		{
			// tag::b918d6b798da673a33e49b94f61dcdc0[]
			var indexResponse = client.Index(new Tweet
			{
				User = "kimchy",
				PostDate = new DateTime(2009, 11, 15, 14, 12, 12),
				Message = "trying out Elasticsearch"
			},
				i => i
					.Index("twitter")
					.Id(1)
					.Timeout("5m")
			);
			// end::b918d6b798da673a33e49b94f61dcdc0[]

			indexResponse.MatchesExample(@"PUT twitter/_doc/1?timeout=5m
			{
			    ""user"" : ""kimchy"",
			    ""post_date"" : ""2009-11-15T14:12:12"",
			    ""message"" : ""trying out Elasticsearch""
			}");
		}

		[U]
		[Description("docs/index_.asciidoc:395")]
		public void Line395()
		{
			// tag::1f336ecc62480c1d56351cc2f82d0d08[]
			var indexResponse = client.Index(new Tweet
			{
				Message = "elasticsearch now has versioning support, double cool!"
			},
				i => i
					.Index("twitter")
					.Id(1)
					.Version(2)
					.VersionType(VersionType.External)
			);
			// end::1f336ecc62480c1d56351cc2f82d0d08[]

			indexResponse.MatchesExample(@"PUT twitter/_doc/1?version=2&version_type=external
			{
			    ""message"" : ""elasticsearch now has versioning support, double cool!""
			}");
		}
		[U]
		[Description("docs/index_.asciidoc:453")]
		public void Line453()
		{
			// tag::bb143628fd04070683eeeadc9406d9cc[]
			var indexResponse = client.Index(new Tweet
			{
				User = "kimchy",
				PostDate = new DateTime(2009, 11, 15, 14, 12, 12),
				Message = "trying out Elasticsearch"
			},
			i => i.Index("twitter").Id(1));
			// end::bb143628fd04070683eeeadc9406d9cc[]

			indexResponse.MatchesExample(@"PUT twitter/_doc/1
			{
			    ""user"" : ""kimchy"",
			    ""post_date"" : ""2009-11-15T14:12:12"",
			    ""message"" : ""trying out Elasticsearch""
			}");
		}

		[U]
		[Description("docs/index_.asciidoc:486")]
		public void Line486()
		{
			// tag::048d8abd42d094bbdcf4452a58ccb35b[]
			var createResponse = client.Create(new Tweet
			{
				User = "kimchy",
				PostDate = new DateTime(2009, 11, 15, 14, 12, 12),
				Message = "trying out Elasticsearch"
			},
			i => i
				.Index("twitter")
				.Id(1)
			);
			// end::048d8abd42d094bbdcf4452a58ccb35b[]

			createResponse.MatchesExample(@"PUT twitter/_create/1
			{
			    ""user"" : ""kimchy"",
			    ""post_date"" : ""2009-11-15T14:12:12"",
			    ""message"" : ""trying out Elasticsearch""
			}");
		}

		[U]
		[Description("docs/index_.asciidoc:499")]
		public void Line499()
		{
			// tag::d718b63cf1b6591a1d59a0cf4fd995eb[]
			var indexResponse = client.Index(new Tweet
			{
				User = "kimchy",
				PostDate = new DateTime(2009, 11, 15, 14, 12, 12),
				Message = "trying out Elasticsearch"
			},
				i => i
					.Index("twitter")
					.Id(1)
					.OpType(OpType.Create)
				);
			// end::d718b63cf1b6591a1d59a0cf4fd995eb[]

			indexResponse.MatchesExample(@"PUT twitter/_doc/1?op_type=create
			{
			    ""user"" : ""kimchy"",
			    ""post_date"" : ""2009-11-15T14:12:12"",
			    ""message"" : ""trying out Elasticsearch""
			}");
		}
	}
}
