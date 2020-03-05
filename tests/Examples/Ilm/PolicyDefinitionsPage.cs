using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Ilm
{
	public class PolicyDefinitionsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		public void Line34()
		{
			// tag::b53e3314eb39b667a9ba87fb3a286e6b[]
			var response0 = new SearchResponse<object>();
			// end::b53e3314eb39b667a9ba87fb3a286e6b[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""warm"": {
			        ""min_age"": ""1d"",
			        ""actions"": {
			          ""allocate"": {
			            ""number_of_replicas"": 1
			          }
			        }
			      },
			      ""delete"": {
			        ""min_age"": ""30d"",
			        ""actions"": {
			          ""delete"": {}
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line166()
		{
			// tag::1116c769f39f0c7fe86ec2a4871efcd5[]
			var response0 = new SearchResponse<object>();
			// end::1116c769f39f0c7fe86ec2a4871efcd5[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""warm"": {
			        ""actions"": {
			          ""allocate"" : {
			            ""number_of_replicas"" : 2
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line188()
		{
			// tag::0518c673094fb18ecb491a3b78af4695[]
			var response0 = new SearchResponse<object>();
			// end::0518c673094fb18ecb491a3b78af4695[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""warm"": {
			        ""actions"": {
			          ""allocate"" : {
			            ""include"" : {
			              ""box_type"": ""hot,warm""
			            }
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line213()
		{
			// tag::9d461ae140ddc018efd2650559800cd1[]
			var response0 = new SearchResponse<object>();
			// end::9d461ae140ddc018efd2650559800cd1[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""warm"": {
			        ""actions"": {
			          ""allocate"" : {
			            ""number_of_replicas"": 1,
			            ""require"" : {
			              ""box_type"": ""cold""
			            }
			        }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line250()
		{
			// tag::83062a543163370328cf2e21a68c1bd3[]
			var response0 = new SearchResponse<object>();
			// end::83062a543163370328cf2e21a68c1bd3[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""delete"": {
			        ""actions"": {
			          ""wait_for_snapshot"" : {
			            ""policy"": ""slm-policy-name""
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line277()
		{
			// tag::053497b6960f80fd7b005b7c6d54358f[]
			var response0 = new SearchResponse<object>();
			// end::053497b6960f80fd7b005b7c6d54358f[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""delete"": {
			        ""actions"": {
			          ""delete"" : { }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line324()
		{
			// tag::eb5486d2fe4283475bf9e0e09280be16[]
			var response0 = new SearchResponse<object>();
			// end::eb5486d2fe4283475bf9e0e09280be16[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""warm"": {
			        ""actions"": {
			          ""forcemerge"" : {
			            ""max_num_segments"": 1
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line350()
		{
			// tag::0345fbd95c4516a89ac5ad261a16be8f[]
			var response0 = new SearchResponse<object>();
			// end::0345fbd95c4516a89ac5ad261a16be8f[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""cold"": {
			        ""actions"": {
			          ""freeze"" : { }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line384()
		{
			// tag::fc9a1b1173690a911725cff3912e9755[]
			var response0 = new SearchResponse<object>();
			// end::fc9a1b1173690a911725cff3912e9755[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""warm"": {
			        ""actions"": {
			          ""readonly"" : { }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line422()
		{
			// tag::d7e7489b7d176aa854dfc785a12feab3[]
			var response0 = new SearchResponse<object>();
			// end::d7e7489b7d176aa854dfc785a12feab3[]

			response0.MatchesExample(@"PUT my_index-000001
			{
			  ""settings"": {
			    ""index.lifecycle.name"": ""my_policy"",
			    ""index.lifecycle.rollover_alias"": ""my_data""
			  },
			  ""aliases"": {
			    ""my_data"": {
			      ""is_write_index"": true
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line466()
		{
			// tag::19211ccf772f1dee7b500c21f4a9a805[]
			var response0 = new SearchResponse<object>();
			// end::19211ccf772f1dee7b500c21f4a9a805[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""actions"": {
			          ""rollover"" : {
			            ""max_size"": ""100GB""
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line489()
		{
			// tag::cfd4b34f35e531a20739a3b308d57134[]
			var response0 = new SearchResponse<object>();
			// end::cfd4b34f35e531a20739a3b308d57134[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""actions"": {
			          ""rollover"" : {
			            ""max_docs"": 100000000
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line512()
		{
			// tag::d4a41fb74b41b41a0ee114a2311f2815[]
			var response0 = new SearchResponse<object>();
			// end::d4a41fb74b41b41a0ee114a2311f2815[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""actions"": {
			          ""rollover"" : {
			            ""max_age"": ""7d""
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line536()
		{
			// tag::8940f2b911220acc9afef6360b6c13c4[]
			var response0 = new SearchResponse<object>();
			// end::8940f2b911220acc9afef6360b6c13c4[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""actions"": {
			          ""rollover"" : {
			            ""max_age"": ""7d"",
			            ""max_size"": ""100GB""
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line561()
		{
			// tag::f6c79fa1c01bb4539d0cba0bd62c1ce0[]
			var response0 = new SearchResponse<object>();
			// end::f6c79fa1c01bb4539d0cba0bd62c1ce0[]

			response0.MatchesExample(@"PUT /_ilm/policy/rollover_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""actions"": {
			          ""rollover"": {
			            ""max_size"": ""50G""
			          }
			        }
			      },
			      ""delete"": {
			        ""min_age"": ""1d"",
			        ""actions"": {
			          ""delete"": {}
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line612()
		{
			// tag::149a0eea54cdf6ea3052af6dba2d2a63[]
			var response0 = new SearchResponse<object>();
			// end::149a0eea54cdf6ea3052af6dba2d2a63[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""warm"": {
			        ""actions"": {
			          ""set_priority"" : {
			            ""priority"": 50
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line664()
		{
			// tag::f3b4ddce8ff21fc1a76a7c0d9c36650e[]
			var response0 = new SearchResponse<object>();
			// end::f3b4ddce8ff21fc1a76a7c0d9c36650e[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""warm"": {
			        ""actions"": {
			          ""shrink"" : {
			            ""number_of_shards"": 1
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line721()
		{
			// tag::a5a58e8ad66afe831bc295500e3e8739[]
			var response0 = new SearchResponse<object>();
			// end::a5a58e8ad66afe831bc295500e3e8739[]

			response0.MatchesExample(@"PUT _ilm/policy/my_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""actions"": {
			          ""unfollow"" : {}
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		public void Line746()
		{
			// tag::d14a2a6c2a8b084495b8a64708226650[]
			var response0 = new SearchResponse<object>();
			// end::d14a2a6c2a8b084495b8a64708226650[]

			response0.MatchesExample(@"PUT _ilm/policy/full_policy
			{
			  ""policy"": {
			    ""phases"": {
			      ""hot"": {
			        ""actions"": {
			          ""rollover"": {
			            ""max_age"": ""7d"",
			            ""max_size"": ""50G""
			          }
			        }
			      },
			      ""warm"": {
			        ""min_age"": ""30d"",
			        ""actions"": {
			          ""forcemerge"": {
			            ""max_num_segments"": 1
			          },
			          ""shrink"": {
			            ""number_of_shards"": 1
			          },
			          ""allocate"": {
			            ""number_of_replicas"": 2
			          }
			        }
			      },
			      ""cold"": {
			        ""min_age"": ""60d"",
			        ""actions"": {
			          ""allocate"": {
			            ""require"": {
			              ""type"": ""cold""
			            }
			          }
			        }
			      },
			      ""delete"": {
			        ""min_age"": ""90d"",
			        ""actions"": {
			          ""delete"": {}
			        }
			      }
			    }
			  }
			}");
		}
	}
}