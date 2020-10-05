// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;

namespace Tests.Reproduce
{
	public class GithubIssue3745
	{
		[U]
		public void CanDeserializeCompositeAggs()
		{
			var json = @"{
	""took"": 28,
	""timed_out"": false,
	""_shards"": {
		""total"": 5,
		""successful"": 5,
		""skipped"": 0,
		""failed"": 0
	},
	""hits"": {
		""total"": {
			""value"": 97,
			""relation"": ""eq""
		},
		""max_score"": null,
		""hits"": []
	},
	""aggregations"": {
		""composite#FieldNamecomposite"": {
			""after_key"": {
				""FieldNamesub"": 1088442000000
			},
			""buckets"": [{
					""key"": {
						""FieldNamesub"": 1081270800000
					},
					""doc_count"": 2
				}, {
					""key"": {
						""FieldNamesub"": 1081702800000
					},
					""doc_count"": 2
				}, {
					""key"": {
						""FieldNamesub"": 1082307600000
					},
					""doc_count"": 2
				}, {
					""key"": {
						""FieldNamesub"": 1084294800000
					},
					""doc_count"": 3
				}, {
					""key"": {
						""FieldNamesub"": 1084467600000
					},
					""doc_count"": 2
				}, {
					""key"": {
						""FieldNamesub"": 1084813200000
					},
					""doc_count"": 3
				}, {
					""key"": {
						""FieldNamesub"": 1085504400000
					},
					""doc_count"": 1
				}, {
					""key"": {
						""FieldNamesub"": 1087232400000
					},
					""doc_count"": 3
				}, {
					""key"": {
						""FieldNamesub"": 1087750800000
					},
					""doc_count"": 1
				}, {
					""key"": {
						""FieldNamesub"": 1088442000000
					},
					""doc_count"": 4
				}
			]
		}
	}
}";

			var bytes = Encoding.UTF8.GetBytes(json);

			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(pool, new InMemoryConnection(bytes)).DefaultIndex("default_index");
			var client = new ElasticClient(connectionSettings);

			var response = client.Search<object>(s => s);
			var compositeAgg = response.Aggregations.Composite("FieldNamecomposite");
			compositeAgg.Should().NotBeNull();
		}

		[U]
		public void CanDeserializeCompositeAggsWithSubs()
		{
			var json = @"{
  ""took"" : 172,
  ""timed_out"" : false,
  ""_shards"" : {
    ""total"" : 2,
    ""successful"" : 2,
    ""skipped"" : 0,
    ""failed"" : 0
  },
  ""hits"" : {
    ""total"" : {
      ""value"" : 100,
      ""relation"" : ""eq""
    },
    ""max_score"" : null,
    ""hits"" : [ ]
  },
  ""aggregations"" : {
    ""composite#my_buckets"" : {
      ""after_key"" : {
        ""branches"" : ""dev"",
        ""started"" : 1535760000000,
        ""branch_count"" : 2.0
      },
      ""buckets"" : [
        {
          ""key"" : {
            ""branches"" : ""dev"",
            ""started"" : 1525132800000,
            ""branch_count"" : 3.0
          },
          ""doc_count"" : 1,
          ""nested#project_tags"" : {
            ""doc_count"" : 28,
            ""sterms#tags"" : {
              ""doc_count_error_upper_bound"" : 0,
              ""sum_other_doc_count"" : 14,
              ""buckets"" : [
                {
                  ""key"" : ""adipisci"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""facilis"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""nulla"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""voluptatem"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""aliquam"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""animi"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""architecto"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""assumenda"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""beatae"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""blanditiis"",
                  ""doc_count"" : 1
                }
              ]
            }
          }
        },
        {
          ""key"" : {
            ""branches"" : ""dev"",
            ""started"" : 1525132800000,
            ""branch_count"" : 4.0
          },
          ""doc_count"" : 1,
          ""nested#project_tags"" : {
            ""doc_count"" : 33,
            ""sterms#tags"" : {
              ""doc_count_error_upper_bound"" : 0,
              ""sum_other_doc_count"" : 19,
              ""buckets"" : [
                {
                  ""key"" : ""enim"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""rerum"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""sit"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""ut"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""aliquam"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""architecto"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""asperiores"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""blanditiis"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""commodi"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""consectetur"",
                  ""doc_count"" : 1
                }
              ]
            }
          }
        },
        {
          ""key"" : {
            ""branches"" : ""dev"",
            ""started"" : 1527811200000,
            ""branch_count"" : 2.0
          },
          ""doc_count"" : 3,
          ""nested#project_tags"" : {
            ""doc_count"" : 77,
            ""sterms#tags"" : {
              ""doc_count_error_upper_bound"" : 2,
              ""sum_other_doc_count"" : 52,
              ""buckets"" : [
                {
                  ""key"" : ""et"",
                  ""doc_count"" : 7
                },
                {
                  ""key"" : ""aut"",
                  ""doc_count"" : 4
                },
                {
                  ""key"" : ""autem"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""iste"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""molestias"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""neque"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""quas"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""qui"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""alias"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""amet"",
                  ""doc_count"" : 1
                }
              ]
            }
          }
        },
        {
          ""key"" : {
            ""branches"" : ""dev"",
            ""started"" : 1527811200000,
            ""branch_count"" : 3.0
          },
          ""doc_count"" : 4,
          ""nested#project_tags"" : {
            ""doc_count"" : 126,
            ""sterms#tags"" : {
              ""doc_count_error_upper_bound"" : 2,
              ""sum_other_doc_count"" : 88,
              ""buckets"" : [
                {
                  ""key"" : ""et"",
                  ""doc_count"" : 6
                },
                {
                  ""key"" : ""consequatur"",
                  ""doc_count"" : 5
                },
                {
                  ""key"" : ""non"",
                  ""doc_count"" : 5
                },
                {
                  ""key"" : ""in"",
                  ""doc_count"" : 4
                },
                {
                  ""key"" : ""animi"",
                  ""doc_count"" : 3
                },
                {
                  ""key"" : ""autem"",
                  ""doc_count"" : 3
                },
                {
                  ""key"" : ""doloribus"",
                  ""doc_count"" : 3
                },
                {
                  ""key"" : ""qui"",
                  ""doc_count"" : 3
                },
                {
                  ""key"" : ""rerum"",
                  ""doc_count"" : 3
                },
                {
                  ""key"" : ""ut"",
                  ""doc_count"" : 3
                }
              ]
            }
          }
        },
        {
          ""key"" : {
            ""branches"" : ""dev"",
            ""started"" : 1527811200000,
            ""branch_count"" : 4.0
          },
          ""doc_count"" : 4,
          ""nested#project_tags"" : {
            ""doc_count"" : 118,
            ""sterms#tags"" : {
              ""doc_count_error_upper_bound"" : 1,
              ""sum_other_doc_count"" : 89,
              ""buckets"" : [
                {
                  ""key"" : ""est"",
                  ""doc_count"" : 5
                },
                {
                  ""key"" : ""et"",
                  ""doc_count"" : 4
                },
                {
                  ""key"" : ""aut"",
                  ""doc_count"" : 3
                },
                {
                  ""key"" : ""expedita"",
                  ""doc_count"" : 3
                },
                {
                  ""key"" : ""qui"",
                  ""doc_count"" : 3
                },
                {
                  ""key"" : ""quia"",
                  ""doc_count"" : 3
                },
                {
                  ""key"" : ""asperiores"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""consequatur"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""cumque"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""dolor"",
                  ""doc_count"" : 2
                }
              ]
            }
          }
        },
        {
          ""key"" : {
            ""branches"" : ""dev"",
            ""started"" : 1530403200000,
            ""branch_count"" : 3.0
          },
          ""doc_count"" : 2,
          ""nested#project_tags"" : {
            ""doc_count"" : 17,
            ""sterms#tags"" : {
              ""doc_count_error_upper_bound"" : 0,
              ""sum_other_doc_count"" : 4,
              ""buckets"" : [
                {
                  ""key"" : ""qui"",
                  ""doc_count"" : 3
                },
                {
                  ""key"" : ""ipsum"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""assumenda"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""aut"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""corrupti"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""dolor"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""hic"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""laborum"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""necessitatibus"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""nobis"",
                  ""doc_count"" : 1
                }
              ]
            }
          }
        },
        {
          ""key"" : {
            ""branches"" : ""dev"",
            ""started"" : 1530403200000,
            ""branch_count"" : 4.0
          },
          ""doc_count"" : 1,
          ""nested#project_tags"" : {
            ""doc_count"" : 45,
            ""sterms#tags"" : {
              ""doc_count_error_upper_bound"" : 0,
              ""sum_other_doc_count"" : 30,
              ""buckets"" : [
                {
                  ""key"" : ""maxime"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""omnis"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""quibusdam"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""saepe"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""velit"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""adipisci"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""architecto"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""assumenda"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""aut"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""blanditiis"",
                  ""doc_count"" : 1
                }
              ]
            }
          }
        },
        {
          ""key"" : {
            ""branches"" : ""dev"",
            ""started"" : 1533081600000,
            ""branch_count"" : 2.0
          },
          ""doc_count"" : 2,
          ""nested#project_tags"" : {
            ""doc_count"" : 72,
            ""sterms#tags"" : {
              ""doc_count_error_upper_bound"" : 0,
              ""sum_other_doc_count"" : 47,
              ""buckets"" : [
                {
                  ""key"" : ""et"",
                  ""doc_count"" : 5
                },
                {
                  ""key"" : ""est"",
                  ""doc_count"" : 3
                },
                {
                  ""key"" : ""numquam"",
                  ""doc_count"" : 3
                },
                {
                  ""key"" : ""velit"",
                  ""doc_count"" : 3
                },
                {
                  ""key"" : ""aut"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""enim"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""quod"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""rerum"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""ut"",
                  ""doc_count"" : 2
                },
                {
                  ""key"" : ""alias"",
                  ""doc_count"" : 1
                }
              ]
            }
          }
        },
        {
          ""key"" : {
            ""branches"" : ""dev"",
            ""started"" : 1533081600000,
            ""branch_count"" : 4.0
          },
          ""doc_count"" : 1,
          ""nested#project_tags"" : {
            ""doc_count"" : 8,
            ""sterms#tags"" : {
              ""doc_count_error_upper_bound"" : 0,
              ""sum_other_doc_count"" : 0,
              ""buckets"" : [
                {
                  ""key"" : ""corporis"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""ea"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""ipsa"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""nemo"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""odit"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""sequi"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""sunt"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""voluptas"",
                  ""doc_count"" : 1
                }
              ]
            }
          }
        },
        {
          ""key"" : {
            ""branches"" : ""dev"",
            ""started"" : 1535760000000,
            ""branch_count"" : 2.0
          },
          ""doc_count"" : 1,
          ""nested#project_tags"" : {
            ""doc_count"" : 6,
            ""sterms#tags"" : {
              ""doc_count_error_upper_bound"" : 0,
              ""sum_other_doc_count"" : 0,
              ""buckets"" : [
                {
                  ""key"" : ""doloribus"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""eius"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""fuga"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""in"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""nam"",
                  ""doc_count"" : 1
                },
                {
                  ""key"" : ""provident"",
                  ""doc_count"" : 1
                }
              ]
            }
          }
        }
      ]
    }
  }
}
";

			var bytes = Encoding.UTF8.GetBytes(json);

			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(pool, new InMemoryConnection(bytes)).DefaultIndex("default_index");
			var client = new ElasticClient(connectionSettings);

			var response = client.Search<object>(s => s);
			var compositeAgg = response.Aggregations.Composite("my_buckets");
			compositeAgg.Should().NotBeNull();
		}
	}
}
