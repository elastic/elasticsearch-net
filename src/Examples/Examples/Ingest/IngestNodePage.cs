using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ingest
{
	public class IngestNodePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line171()
		{
			// tag::841306ff1ac69cceb5bf1c28e2f26dd3[]
			var response0 = new SearchResponse<object>();
			// end::841306ff1ac69cceb5bf1c28e2f26dd3[]

			response0.MatchesExample(@"PUT _ingest/pipeline/drop_guests_network
			{
			  ""processors"": [
			    {
			      ""drop"": {
			        ""if"": ""ctx.network_name == 'Guest'""
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line187()
		{
			// tag::027ee5302d967b530123886906c42a90[]
			var response0 = new SearchResponse<object>();
			// end::027ee5302d967b530123886906c42a90[]

			response0.MatchesExample(@"POST test/_doc/1?pipeline=drop_guests_network
			{
			  ""network_name"" : ""Guest""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line227()
		{
			// tag::9a5f1f590791012d32d29605daf82135[]
			var response0 = new SearchResponse<object>();
			// end::9a5f1f590791012d32d29605daf82135[]

			response0.MatchesExample(@"PUT _ingest/pipeline/drop_guests_network
			{
			  ""processors"": [
			    {
			      ""drop"": {
			        ""if"": ""ctx.network?.name == 'Guest'""
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line243()
		{
			// tag::f8a8b78caaf69d44c71c476ea2a178aa[]
			var response0 = new SearchResponse<object>();
			// end::f8a8b78caaf69d44c71c476ea2a178aa[]

			response0.MatchesExample(@"POST test/_doc/1?pipeline=drop_guests_network
			{
			  ""network"": {
			    ""name"": ""Guest""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line258()
		{
			// tag::3eb75cee4c802d99bb526386349ee36b[]
			var response0 = new SearchResponse<object>();
			// end::3eb75cee4c802d99bb526386349ee36b[]

			response0.MatchesExample(@"POST test/_doc/2?pipeline=drop_guests_network
			{
			  ""foo"" : ""bar""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line318()
		{
			// tag::089ca88d7fd064a474e156d773211bc5[]
			var response0 = new SearchResponse<object>();
			// end::089ca88d7fd064a474e156d773211bc5[]

			response0.MatchesExample(@"PUT _ingest/pipeline/drop_guests_network
			{
			  ""processors"": [
			    {
			      ""dot_expander"": {
			        ""field"": ""network.name""
			      }
			    },
			    {
			      ""drop"": {
			        ""if"": ""ctx.network?.name == 'Guest'""
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line339()
		{
			// tag::8f6cec77f890027ad2e01f06e1290e25[]
			var response0 = new SearchResponse<object>();
			// end::8f6cec77f890027ad2e01f06e1290e25[]

			response0.MatchesExample(@"POST test/_doc/3?pipeline=drop_guests_network
			{
			  ""network.name"": ""Guest""
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line386()
		{
			// tag::3b54be0a1a020edb8943f063f05b5cd7[]
			var response0 = new SearchResponse<object>();
			// end::3b54be0a1a020edb8943f063f05b5cd7[]

			response0.MatchesExample(@"PUT _ingest/pipeline/not_prod_dropper
			{
			  ""processors"": [
			    {
			      ""drop"": {
			        ""if"": ""Collection tags = ctx.tags;if(tags != null){for (String tag : tags) {if (tag.toLowerCase().contains('prod')) { return false;}}} return true;""
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line431()
		{
			// tag::da19607976c3740945300c18e692bc49[]
			var response0 = new SearchResponse<object>();
			// end::da19607976c3740945300c18e692bc49[]

			response0.MatchesExample(@"POST test/_doc/1?pipeline=not_prod_dropper
			{
			  ""tags"": [""application:myapp"", ""env:Stage""]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line446()
		{
			// tag::784dcf96b4970ce6c90d999cdfc2ef0b[]
			var response0 = new SearchResponse<object>();
			// end::784dcf96b4970ce6c90d999cdfc2ef0b[]

			response0.MatchesExample(@"POST test/_doc/2?pipeline=not_prod_dropper
			{
			  ""tags"": [""application:myapp"", ""env:Production""]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line498()
		{
			// tag::7cc6435cb7508e532df1e761934f1683[]
			var response0 = new SearchResponse<object>();
			// end::7cc6435cb7508e532df1e761934f1683[]

			response0.MatchesExample(@"PUT _ingest/pipeline/logs_pipeline
			{
			  ""description"": ""A pipeline of pipelines for log files"",
			  ""version"": 1,
			  ""processors"": [
			    {
			      ""pipeline"": {
			        ""if"": ""ctx.service?.name == 'apache_httpd'"",
			        ""name"": ""httpd_pipeline""
			      }
			    },
			    {
			      ""pipeline"": {
			        ""if"": ""ctx.service?.name == 'syslog'"",
			        ""name"": ""syslog_pipeline""
			      }
			    },
			    {
			      ""fail"": {
			        ""if"": ""ctx.service?.name != 'apache_httpd' && ctx.service?.name != 'syslog'"",
			        ""message"": ""This pipeline requires service.name to be either `syslog` or `apache_httpd`""
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line545()
		{
			// tag::fe2d94eba550076cc27ee21a711fdb5c[]
			var response0 = new SearchResponse<object>();
			// end::fe2d94eba550076cc27ee21a711fdb5c[]

			response0.MatchesExample(@"PUT _ingest/pipeline/check_url
			{
			  ""processors"": [
			    {
			      ""set"": {
			        ""if"": ""ctx.href?.url =~ /^http[^s]/"",
			        ""field"": ""href.insecure"",
			        ""value"": true
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line561()
		{
			// tag::bfc92c930234ada7a3f394263b0deb1e[]
			var response0 = new SearchResponse<object>();
			// end::bfc92c930234ada7a3f394263b0deb1e[]

			response0.MatchesExample(@"POST test/_doc/1?pipeline=check_url
			{
			  ""href"": {
			    ""url"": ""http://www.elastic.co/""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line609()
		{
			// tag::2ad6189aef1ecbb52bf0ddbd4e7a80cb[]
			var response0 = new SearchResponse<object>();
			// end::2ad6189aef1ecbb52bf0ddbd4e7a80cb[]

			response0.MatchesExample(@"PUT _ingest/pipeline/check_url
			{
			  ""processors"": [
			    {
			      ""set"": {
			        ""if"": ""ctx.href?.url != null && ctx.href.url.startsWith('http://')"",
			        ""field"": ""href.insecure"",
			        ""value"": true
			      }
			    }
			  ]
			}");
		}
	}
}