using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Docs
{
	public class UpdateByQueryPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line10()
		{
			// tag::a4a396cd07657b3977713fb3a742c41b[]
			var response0 = new SearchResponse<object>();
			// end::a4a396cd07657b3977713fb3a742c41b[]

			response0.MatchesExample(@"POST twitter/_update_by_query?conflicts=proceed");
		}

		[U]
		[SkipExample]
		public void Line80()
		{
			// tag::52a87b81e4e0b6b11e23e85db1602a63[]
			var response0 = new SearchResponse<object>();
			// end::52a87b81e4e0b6b11e23e85db1602a63[]

			response0.MatchesExample(@"POST twitter/_update_by_query?conflicts=proceed
			{
			  ""query"": { \<1>
			    ""term"": {
			      ""user"": ""kimchy""
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line104()
		{
			// tag::2fd69fb0538e4f36ac69a8b8f8bf5ae8[]
			var response0 = new SearchResponse<object>();
			// end::2fd69fb0538e4f36ac69a8b8f8bf5ae8[]

			response0.MatchesExample(@"POST twitter/_update_by_query
			{
			  ""script"": {
			    ""source"": ""ctx._source.likes++"",
			    ""lang"": ""painless""
			  },
			  ""query"": {
			    ""term"": {
			      ""user"": ""kimchy""
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line152()
		{
			// tag::cde4dddae5c06e7f1d38c9d933dbc7ac[]
			var response0 = new SearchResponse<object>();
			// end::cde4dddae5c06e7f1d38c9d933dbc7ac[]

			response0.MatchesExample(@"POST twitter,blog/_update_by_query");
		}

		[U]
		[SkipExample]
		public void Line162()
		{
			// tag::d8b115341da772a628a024e7d1644e73[]
			var response0 = new SearchResponse<object>();
			// end::d8b115341da772a628a024e7d1644e73[]

			response0.MatchesExample(@"POST twitter/_update_by_query?routing=1");
		}

		[U]
		[SkipExample]
		public void Line172()
		{
			// tag::54a770f053f3225ea0d1e34334232411[]
			var response0 = new SearchResponse<object>();
			// end::54a770f053f3225ea0d1e34334232411[]

			response0.MatchesExample(@"POST twitter/_update_by_query?scroll_size=100");
		}

		[U]
		[SkipExample]
		public void Line182()
		{
			// tag::c4b278ba293abd0d02a0b5ad1a99f84a[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::c4b278ba293abd0d02a0b5ad1a99f84a[]

			response0.MatchesExample(@"PUT _ingest/pipeline/set-foo
			{
			  ""description"" : ""sets foo"",
			  ""processors"" : [ {
			      ""set"" : {
			        ""field"": ""foo"",
			        ""value"": ""bar""
			      }
			  } ]
			}");

			response1.MatchesExample(@"POST twitter/_update_by_query?pipeline=set-foo");
		}

		[U]
		[SkipExample]
		public void Line360()
		{
			// tag::7df191cc7f814e410a4ac7261065e6ef[]
			var response0 = new SearchResponse<object>();
			// end::7df191cc7f814e410a4ac7261065e6ef[]

			response0.MatchesExample(@"GET _tasks?detailed=true&actions=*byquery");
		}

		[U]
		[SkipExample]
		public void Line420()
		{
			// tag::be3a6431d01846950dc1a39a7a6a1faa[]
			var response0 = new SearchResponse<object>();
			// end::be3a6431d01846950dc1a39a7a6a1faa[]

			response0.MatchesExample(@"GET /_tasks/r1A2WoRbTwKZ516z6NEs5A:36619");
		}

		[U]
		[SkipExample]
		public void Line441()
		{
			// tag::18ddb7e7a4bcafd449df956e828ed7a8[]
			var response0 = new SearchResponse<object>();
			// end::18ddb7e7a4bcafd449df956e828ed7a8[]

			response0.MatchesExample(@"POST _tasks/r1A2WoRbTwKZ516z6NEs5A:36619/_cancel");
		}

		[U]
		[SkipExample]
		public void Line461()
		{
			// tag::bdb30dd52d32f50994008f4f9c0da5f0[]
			var response0 = new SearchResponse<object>();
			// end::bdb30dd52d32f50994008f4f9c0da5f0[]

			response0.MatchesExample(@"POST _update_by_query/r1A2WoRbTwKZ516z6NEs5A:36619/_rethrottle?requests_per_second=-1");
		}

		[U]
		[SkipExample]
		public void Line490()
		{
			// tag::0d664883151008b1051ef2c9ab2d0373[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::0d664883151008b1051ef2c9ab2d0373[]

			response0.MatchesExample(@"POST twitter/_update_by_query
			{
			  ""slice"": {
			    ""id"": 0,
			    ""max"": 2
			  },
			  ""script"": {
			    ""source"": ""ctx._source['extra'] = 'test'""
			  }
			}");

			response1.MatchesExample(@"POST twitter/_update_by_query
			{
			  ""slice"": {
			    ""id"": 1,
			    ""max"": 2
			  },
			  ""script"": {
			    ""source"": ""ctx._source['extra'] = 'test'""
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line518()
		{
			// tag::4acf902c2598b2558f34f20c1744c433[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::4acf902c2598b2558f34f20c1744c433[]

			response0.MatchesExample(@"GET _refresh");

			response1.MatchesExample(@"POST twitter/_search?size=0&q=extra:test&filter_path=hits.total");
		}

		[U]
		[SkipExample]
		public void Line549()
		{
			// tag::ea02de2dbe05091fcb0dac72c8ba5f83[]
			var response0 = new SearchResponse<object>();
			// end::ea02de2dbe05091fcb0dac72c8ba5f83[]

			response0.MatchesExample(@"POST twitter/_update_by_query?refresh&slices=5
			{
			  ""script"": {
			    ""source"": ""ctx._source['extra'] = 'test'""
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line563()
		{
			// tag::025b54db0edc50c24ea48a2bd94366ad[]
			var response0 = new SearchResponse<object>();
			// end::025b54db0edc50c24ea48a2bd94366ad[]

			response0.MatchesExample(@"POST twitter/_search?size=0&q=extra:test&filter_path=hits.total");
		}

		[U]
		[SkipExample]
		public void Line641()
		{
			// tag::2fe28d9a91b3081a9ec4601af8fb7b1c[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::2fe28d9a91b3081a9ec4601af8fb7b1c[]

			response0.MatchesExample(@"PUT test
			{
			  ""mappings"": {
			    ""dynamic"": false,   \<1>
			    ""properties"": {
			      ""text"": {""type"": ""text""}
			    }
			  }
			}");

			response1.MatchesExample(@"POST test/_doc?refresh
			{
			  ""text"": ""words words"",
			  ""flag"": ""bar""
			}");

			response2.MatchesExample(@"POST test/_doc?refresh
			{
			  ""text"": ""words words"",
			  ""flag"": ""foo""
			}");

			response3.MatchesExample(@"PUT test/_mapping   \<2>
			{
			  ""properties"": {
			    ""text"": {""type"": ""text""},
			    ""flag"": {""type"": ""text"", ""analyzer"": ""keyword""}
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line680()
		{
			// tag::abd4fc3ce7784413a56fe2dcfe2809b5[]
			var response0 = new SearchResponse<object>();
			// end::abd4fc3ce7784413a56fe2dcfe2809b5[]

			response0.MatchesExample(@"POST test/_search?filter_path=hits.total
			{
			  ""query"": {
			    ""match"": {
			      ""flag"": ""foo""
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line709()
		{
			// tag::97babc8d19ef0866774576716eb6d19e[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::97babc8d19ef0866774576716eb6d19e[]

			response0.MatchesExample(@"POST test/_update_by_query?refresh&conflicts=proceed");

			response1.MatchesExample(@"POST test/_search?filter_path=hits.total
			{
			  ""query"": {
			    ""match"": {
			      ""flag"": ""foo""
			    }
			  }
			}");
		}
	}
}