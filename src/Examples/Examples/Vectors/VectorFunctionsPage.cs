using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Vectors
{
	public class VectorFunctionsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line21()
		{
			// tag::0f621a396f26e1a8d1a724329260af07[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();
			// end::0f621a396f26e1a8d1a724329260af07[]

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
			      },
			      ""status"" : {
			        ""type"" : ""keyword""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""my_dense_vector"": [0.5, 10, 6],
			  ""my_sparse_vector"": {""2"": 1.5, ""15"" : 2, ""50"": -1.1, ""4545"": 1.1},
			  ""status"" : ""published""
			}");

			response2.MatchesExample(@"PUT my_index/_doc/2
			{
			  ""my_dense_vector"": [-0.5, 10, 10],
			  ""my_sparse_vector"": {""2"": 2.5, ""10"" : 1.3, ""55"": -2.3, ""113"": 1.6},
			  ""status"" : ""published""
			}");

			response3.MatchesExample(@"");
		}

		[U(Skip = "Example not implemented")]
		public void Line62()
		{
			// tag::e077e86607f272568cff9cd950c21bb6[]
			var response0 = new SearchResponse<object>();
			// end::e077e86607f272568cff9cd950c21bb6[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""script_score"": {
			      ""query"" : {
			        ""bool"" : {
			          ""filter"" : {
			            ""term"" : {
			              ""status"" : ""published"" \<1>
			            }
			          }
			        }
			      },
			      ""script"": {
			        ""source"": ""cosineSimilarity(params.query_vector, doc['my_dense_vector']) + 1.0"", \<2>
			        ""params"": {
			          ""query_vector"": [4, 3.4, -0.2]  \<3>
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line98()
		{
			// tag::d6b15235dd3238e8b94caa42d0c0c32e[]
			var response0 = new SearchResponse<object>();
			// end::d6b15235dd3238e8b94caa42d0c0c32e[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""script_score"": {
			      ""query"" : {
			        ""bool"" : {
			          ""filter"" : {
			            ""term"" : {
			              ""status"" : ""published""
			            }
			          }
			        }
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

		[U(Skip = "Example not implemented")]
		public void Line128()
		{
			// tag::3ab88f9b42d28940835c6a6cd91f50fd[]
			var response0 = new SearchResponse<object>();
			// end::3ab88f9b42d28940835c6a6cd91f50fd[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""script_score"": {
			      ""query"" : {
			        ""bool"" : {
			          ""filter"" : {
			            ""term"" : {
			              ""status"" : ""published""
			            }
			          }
			        }
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

		[U(Skip = "Example not implemented")]
		public void Line163()
		{
			// tag::a7ac82b206e859c187678c62681ba380[]
			var response0 = new SearchResponse<object>();
			// end::a7ac82b206e859c187678c62681ba380[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""script_score"": {
			      ""query"" : {
			        ""bool"" : {
			          ""filter"" : {
			            ""term"" : {
			              ""status"" : ""published""
			            }
			          }
			        }
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

		[U(Skip = "Example not implemented")]
		public void Line197()
		{
			// tag::36fc6170ce7ff17b719f988ae03a50c9[]
			var response0 = new SearchResponse<object>();
			// end::36fc6170ce7ff17b719f988ae03a50c9[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""script_score"": {
			      ""query"" : {
			        ""bool"" : {
			          ""filter"" : {
			            ""term"" : {
			              ""status"" : ""published""
			            }
			          }
			        }
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

		[U(Skip = "Example not implemented")]
		public void Line236()
		{
			// tag::ab123056145692c7e7e0d7a95aa7ea72[]
			var response0 = new SearchResponse<object>();
			// end::ab123056145692c7e7e0d7a95aa7ea72[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""script_score"": {
			      ""query"" : {
			        ""bool"" : {
			          ""filter"" : {
			            ""term"" : {
			              ""status"" : ""published""
			            }
			          }
			        }
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

		[U(Skip = "Example not implemented")]
		public void Line267()
		{
			// tag::b0e60ffce9edecba49a8b0cce869a85d[]
			var response0 = new SearchResponse<object>();
			// end::b0e60ffce9edecba49a8b0cce869a85d[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""script_score"": {
			      ""query"" : {
			        ""bool"" : {
			          ""filter"" : {
			            ""term"" : {
			              ""status"" : ""published""
			            }
			          }
			        }
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

		[U(Skip = "Example not implemented")]
		public void Line297()
		{
			// tag::20a6db9d4b71d551d6864e95d5b93c4f[]
			var response0 = new SearchResponse<object>();
			// end::20a6db9d4b71d551d6864e95d5b93c4f[]

			response0.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""script_score"": {
			      ""query"" : {
			        ""bool"" : {
			          ""filter"" : {
			            ""term"" : {
			              ""status"" : ""published""
			            }
			          }
			        }
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