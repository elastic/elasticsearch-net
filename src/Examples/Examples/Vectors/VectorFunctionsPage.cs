using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Vectors
{
	public class VectorFunctionsPage : ExampleBase
	{
		[U]
		[SkipExample]
		public void Line21()
		{
			// tag::d5fe26f952e93d08d427678ffdfdd2cd[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::d5fe26f952e93d08d427678ffdfdd2cd[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""my_dense_vector"": {
			        ""type"": ""dense_vector"",
			        ""dims"": 3
			      },
			      ""my_sparse_vector"" : {
			        ""type"" : ""sparse_vector""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""my_dense_vector"": [0.5, 10, 6],
			  ""my_sparse_vector"": {""2"": 1.5, ""15"" : 2, ""50"": -1.1, ""4545"": 1.1}
			}");

			response2.MatchesExample(@"PUT my_index/_doc/2
			{
			  ""my_dense_vector"": [-0.5, 10, 10],
			  ""my_sparse_vector"": {""2"": 2.5, ""10"" : 1.3, ""55"": -2.3, ""113"": 1.6}
			}");

			response3.MatchesExample(@"");
		}

		[U]
		[SkipExample]
		public void Line57()
		{
			// tag::5ed03b6c95b31d2915c584aacd782eb6[]
			var response0 = new SearchResponse<object>();
			// end::5ed03b6c95b31d2915c584aacd782eb6[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""script_score"": {
			      ""query"": {
			        ""match_all"": {}
			      },
			      ""script"": {
			        ""source"": ""cosineSimilarity(params.query_vector, doc['my_dense_vector']) + 1.0"", \<1>
			        ""params"": {
			          ""query_vector"": [4, 3.4, -0.2]  \<2>
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line86()
		{
			// tag::84502fcc20d08a68002cb004be7a2b20[]
			var response0 = new SearchResponse<object>();
			// end::84502fcc20d08a68002cb004be7a2b20[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""script_score"": {
			      ""query"": {
			        ""match_all"": {}
			      },
			      ""script"": {
			        ""source"": ""cosineSimilaritySparse(params.query_vector, doc['my_sparse_vector']) + 1.0"",
			        ""params"": {
			          ""query_vector"": {""2"": 0.5, ""10"" : 111.3, ""50"": -1.3, ""113"": 14.8, ""4545"": 156.0}
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line110()
		{
			// tag::52ade18507911d36cb875daf9726412c[]
			var response0 = new SearchResponse<object>();
			// end::52ade18507911d36cb875daf9726412c[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""script_score"": {
			      ""query"": {
			        ""match_all"": {}
			      },
			      ""script"": {
			        ""source"": """"""
			          double value = dotProduct(params.query_vector, doc['my_dense_vector']);
			          return sigmoid(1, Math.E, -value); \<1>
			        """""",
			        ""params"": {
			          ""query_vector"": [4, 3.4, -0.2]
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line139()
		{
			// tag::a33958dc12dfd4364d75c499652be433[]
			var response0 = new SearchResponse<object>();
			// end::a33958dc12dfd4364d75c499652be433[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""script_score"": {
			      ""query"": {
			        ""match_all"": {}
			      },
			      ""script"": {
			        ""source"": """"""
			          double value = dotProductSparse(params.query_vector, doc['my_sparse_vector']);
			          return sigmoid(1, Math.E, -value);
			        """""",
			         ""params"": {
			          ""query_vector"": {""2"": 0.5, ""10"" : 111.3, ""50"": -1.3, ""113"": 14.8, ""4545"": 156.0}
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line167()
		{
			// tag::0bb1457dfc484885e8809fc02536b523[]
			var response0 = new SearchResponse<object>();
			// end::0bb1457dfc484885e8809fc02536b523[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""script_score"": {
			      ""query"": {
			        ""match_all"": {}
			      },
			      ""script"": {
			        ""source"": ""1 / (1 + l1norm(params.queryVector, doc['my_dense_vector']))"", \<1>
			        ""params"": {
			          ""queryVector"": [4, 3.4, -0.2]
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line200()
		{
			// tag::08843af9fc77104ef77d8c51a2b7c296[]
			var response0 = new SearchResponse<object>();
			// end::08843af9fc77104ef77d8c51a2b7c296[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""script_score"": {
			      ""query"": {
			        ""match_all"": {}
			      },
			      ""script"": {
			        ""source"": ""1 / (1 + l1normSparse(params.queryVector, doc['my_sparse_vector']))"",
			        ""params"": {
			          ""queryVector"": {""2"": 0.5, ""10"" : 111.3, ""50"": -1.3, ""113"": 14.8, ""4545"": 156.0}
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line225()
		{
			// tag::24b552802661be085433cf389ce80a40[]
			var response0 = new SearchResponse<object>();
			// end::24b552802661be085433cf389ce80a40[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""script_score"": {
			      ""query"": {
			        ""match_all"": {}
			      },
			      ""script"": {
			        ""source"": ""1 / (1 + l2norm(params.queryVector, doc['my_dense_vector']))"",
			        ""params"": {
			          ""queryVector"": [4, 3.4, -0.2]
			        }
			      }
			    }
			  }
			}");
		}

		[U]
		[SkipExample]
		public void Line249()
		{
			// tag::d9e8b9435e3a07b5d154b842a90c3d85[]
			var response0 = new SearchResponse<object>();
			// end::d9e8b9435e3a07b5d154b842a90c3d85[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""script_score"": {
			      ""query"": {
			        ""match_all"": {}
			      },
			      ""script"": {
			        ""source"": ""1 / (1 + l2normSparse(params.queryVector, doc['my_sparse_vector']))"",
			        ""params"": {
			          ""queryVector"": {""2"": 0.5, ""10"" : 111.3, ""50"": -1.3, ""113"": 14.8, ""4545"": 156.0}
			        }
			      }
			    }
			  }
			}");
		}
	}
}