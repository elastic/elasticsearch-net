// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Linq;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Elastic.Clients.Elasticsearch.Mapping;
using Tests.Serialization;

namespace Tests.IndexManagement;

public class GetIndexSerializationTests : SerializerTestBase
{
	[U]
	public void GetIndexResponse_DeserializedCorrectly()
	{
		const string json = @"{
  ""catalog-data-2023.01.31"": {
    ""aliases"": {      
    },
    ""mappings"": {
      ""dynamic_templates"": [
        {
          ""strings_as_keyword"": {
            ""match_mapping_type"": ""string"",
            ""mapping"": {
              ""ignore_above"": 1024,
              ""type"": ""keyword""
            }
          }
        }
      ],
      ""date_detection"": false,
      ""properties"": {
        ""@timestamp"": {
          ""type"": ""date""
        },
        ""agent"": {
          ""properties"": {
            ""build"": {
              ""properties"": {
                ""original"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""ephemeral_id"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""id"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""name"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""type"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""version"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        },
        ""client"": {
          ""properties"": {
            ""address"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""as"": {
              ""properties"": {
                ""number"": {
                  ""type"": ""long""
                },
                ""organization"": {
                  ""properties"": {
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024,
                      ""fields"": {
                        ""text"": {
                          ""type"": ""match_only_text""
                        }
                      }
                    }
                  }
                }
              }
            },
            ""bytes"": {
              ""type"": ""long""
            },
            ""domain"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""geo"": {
              ""properties"": {
                ""city_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""continent_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""continent_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""country_iso_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""country_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""location"": {
                  ""type"": ""geo_point""
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""postal_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""region_iso_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""region_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""timezone"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""ip"": {
              ""type"": ""ip""
            },
            ""mac"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""nat"": {
              ""properties"": {
                ""ip"": {
                  ""type"": ""ip""
                },
                ""port"": {
                  ""type"": ""long""
                }
              }
            },
            ""packets"": {
              ""type"": ""long""
            },
            ""port"": {
              ""type"": ""long""
            },
            ""registered_domain"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""subdomain"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""top_level_domain"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""user"": {
              ""properties"": {
                ""domain"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""email"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""full_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""group"": {
                  ""properties"": {
                    ""domain"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""hash"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""roles"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            }
          }
        },
        ""cloud"": {
          ""properties"": {
            ""account"": {
              ""properties"": {
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""availability_zone"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""instance"": {
              ""properties"": {
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""machine"": {
              ""properties"": {
                ""type"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""origin"": {
              ""properties"": {
                ""account"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""availability_zone"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""instance"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""machine"": {
                  ""properties"": {
                    ""type"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""project"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""provider"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""region"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""service"": {
                  ""properties"": {
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                }
              }
            },
            ""project"": {
              ""properties"": {
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""provider"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""region"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""service"": {
              ""properties"": {
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""target"": {
              ""properties"": {
                ""account"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""availability_zone"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""instance"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""machine"": {
                  ""properties"": {
                    ""type"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""project"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""provider"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""region"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""service"": {
                  ""properties"": {
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                }
              }
            }
          }
        },
        ""container"": {
          ""properties"": {
            ""cpu"": {
              ""properties"": {
                ""usage"": {
                  ""type"": ""scaled_float"",
                  ""scaling_factor"": 1000.0
                }
              }
            },
            ""disk"": {
              ""properties"": {
                ""read"": {
                  ""properties"": {
                    ""bytes"": {
                      ""type"": ""long""
                    }
                  }
                },
                ""write"": {
                  ""properties"": {
                    ""bytes"": {
                      ""type"": ""long""
                    }
                  }
                }
              }
            },
            ""id"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""image"": {
              ""properties"": {
                ""hash"": {
                  ""properties"": {
                    ""all"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""tag"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""labels"": {
              ""type"": ""object""
            },
            ""memory"": {
              ""properties"": {
                ""usage"": {
                  ""type"": ""scaled_float"",
                  ""scaling_factor"": 1000.0
                }
              }
            },
            ""name"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""network"": {
              ""properties"": {
                ""egress"": {
                  ""properties"": {
                    ""bytes"": {
                      ""type"": ""long""
                    }
                  }
                },
                ""ingress"": {
                  ""properties"": {
                    ""bytes"": {
                      ""type"": ""long""
                    }
                  }
                }
              }
            },
            ""runtime"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        },
        ""created"": {
          ""type"": ""keyword"",
          ""ignore_above"": 1024
        },
        ""data_stream"": {
          ""properties"": {
            ""dataset"": {
              ""type"": ""constant_keyword""
            },
            ""namespace"": {
              ""type"": ""constant_keyword""
            },
            ""type"": {
              ""type"": ""constant_keyword""
            }
          }
        },
        ""destination"": {
          ""properties"": {
            ""address"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""as"": {
              ""properties"": {
                ""number"": {
                  ""type"": ""long""
                },
                ""organization"": {
                  ""properties"": {
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024,
                      ""fields"": {
                        ""text"": {
                          ""type"": ""match_only_text""
                        }
                      }
                    }
                  }
                }
              }
            },
            ""bytes"": {
              ""type"": ""long""
            },
            ""domain"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""geo"": {
              ""properties"": {
                ""city_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""continent_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""continent_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""country_iso_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""country_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""location"": {
                  ""type"": ""geo_point""
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""postal_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""region_iso_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""region_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""timezone"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""ip"": {
              ""type"": ""ip""
            },
            ""mac"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""nat"": {
              ""properties"": {
                ""ip"": {
                  ""type"": ""ip""
                },
                ""port"": {
                  ""type"": ""long""
                }
              }
            },
            ""packets"": {
              ""type"": ""long""
            },
            ""port"": {
              ""type"": ""long""
            },
            ""registered_domain"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""subdomain"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""top_level_domain"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""user"": {
              ""properties"": {
                ""domain"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""email"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""full_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""group"": {
                  ""properties"": {
                    ""domain"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""hash"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""roles"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            }
          }
        },
        ""dll"": {
          ""properties"": {
            ""code_signature"": {
              ""properties"": {
                ""digest_algorithm"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""exists"": {
                  ""type"": ""boolean""
                },
                ""signing_id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""status"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""subject_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""team_id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""timestamp"": {
                  ""type"": ""date""
                },
                ""trusted"": {
                  ""type"": ""boolean""
                },
                ""valid"": {
                  ""type"": ""boolean""
                }
              }
            },
            ""hash"": {
              ""properties"": {
                ""md5"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""sha1"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""sha256"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""sha384"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""sha512"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""ssdeep"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""tlsh"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""name"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""path"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""pe"": {
              ""properties"": {
                ""architecture"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""company"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""description"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""file_version"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""imphash"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""original_file_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""pehash"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""product"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            }
          }
        },
        ""dns"": {
          ""properties"": {
            ""answers"": {
              ""properties"": {
                ""class"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""data"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""ttl"": {
                  ""type"": ""long""
                },
                ""type"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""header_flags"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""id"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""op_code"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""question"": {
              ""properties"": {
                ""class"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""registered_domain"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""subdomain"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""top_level_domain"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""type"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""resolved_ip"": {
              ""type"": ""ip""
            },
            ""response_code"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""type"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        },
        ""ecs"": {
          ""properties"": {
            ""version"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        },
        ""email"": {
          ""properties"": {
            ""attachments"": {
              ""type"": ""nested"",
              ""properties"": {
                ""file"": {
                  ""properties"": {
                    ""extension"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""hash"": {
                      ""properties"": {
                        ""md5"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""sha1"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""sha256"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""sha384"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""sha512"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""ssdeep"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""tlsh"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        }
                      }
                    },
                    ""mime_type"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""size"": {
                      ""type"": ""long""
                    }
                  }
                }
              }
            },
            ""bcc"": {
              ""properties"": {
                ""address"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""cc"": {
              ""properties"": {
                ""address"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""content_type"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""delivery_timestamp"": {
              ""type"": ""date""
            },
            ""direction"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""from"": {
              ""properties"": {
                ""address"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""local_id"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""message_id"": {
              ""type"": ""wildcard""
            },
            ""origination_timestamp"": {
              ""type"": ""date""
            },
            ""reply_to"": {
              ""properties"": {
                ""address"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""sender"": {
              ""properties"": {
                ""address"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""subject"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024,
              ""fields"": {
                ""text"": {
                  ""type"": ""match_only_text""
                }
              }
            },
            ""to"": {
              ""properties"": {
                ""address"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""x_mailer"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        },
        ""error"": {
          ""properties"": {
            ""code"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""id"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""message"": {
              ""type"": ""match_only_text""
            },
            ""stack_trace"": {
              ""type"": ""wildcard"",
              ""fields"": {
                ""text"": {
                  ""type"": ""match_only_text""
                }
              }
            },
            ""type"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        },
        ""event"": {
          ""properties"": {
            ""action"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""agent_id_status"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""category"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""code"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""created"": {
              ""type"": ""date""
            },
            ""dataset"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""duration"": {
              ""type"": ""long""
            },
            ""end"": {
              ""type"": ""date""
            },
            ""hash"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""id"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""ingested"": {
              ""type"": ""date""
            },
            ""kind"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""module"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""original"": {
              ""type"": ""keyword"",
              ""index"": false,
              ""doc_values"": false
            },
            ""outcome"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""provider"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""reason"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""reference"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""risk_score"": {
              ""type"": ""float""
            },
            ""risk_score_norm"": {
              ""type"": ""float""
            },
            ""sequence"": {
              ""type"": ""long""
            },
            ""severity"": {
              ""type"": ""long""
            },
            ""start"": {
              ""type"": ""date""
            },
            ""timezone"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""type"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""url"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        },
        ""faas"": {
          ""properties"": {
            ""coldstart"": {
              ""type"": ""boolean""
            },
            ""execution"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""id"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""name"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""trigger"": {
              ""type"": ""nested"",
              ""properties"": {
                ""request_id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""type"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""version"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        },
        ""file"": {
          ""properties"": {
            ""accessed"": {
              ""type"": ""date""
            },
            ""attributes"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""code_signature"": {
              ""properties"": {
                ""digest_algorithm"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""exists"": {
                  ""type"": ""boolean""
                },
                ""signing_id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""status"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""subject_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""team_id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""timestamp"": {
                  ""type"": ""date""
                },
                ""trusted"": {
                  ""type"": ""boolean""
                },
                ""valid"": {
                  ""type"": ""boolean""
                }
              }
            },
            ""created"": {
              ""type"": ""date""
            },
            ""ctime"": {
              ""type"": ""date""
            },
            ""device"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""directory"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""drive_letter"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1
            },
            ""elf"": {
              ""properties"": {
                ""architecture"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""byte_order"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""cpu_type"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""creation_date"": {
                  ""type"": ""date""
                },
                ""exports"": {
                  ""type"": ""flattened""
                },
                ""header"": {
                  ""properties"": {
                    ""abi_version"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""class"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""data"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""entrypoint"": {
                      ""type"": ""long""
                    },
                    ""object_version"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""os_abi"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""type"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""version"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""imports"": {
                  ""type"": ""flattened""
                },
                ""sections"": {
                  ""type"": ""nested"",
                  ""properties"": {
                    ""chi2"": {
                      ""type"": ""long""
                    },
                    ""entropy"": {
                      ""type"": ""long""
                    },
                    ""flags"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""physical_offset"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""physical_size"": {
                      ""type"": ""long""
                    },
                    ""type"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""virtual_address"": {
                      ""type"": ""long""
                    },
                    ""virtual_size"": {
                      ""type"": ""long""
                    }
                  }
                },
                ""segments"": {
                  ""type"": ""nested"",
                  ""properties"": {
                    ""sections"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""type"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""shared_libraries"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""telfhash"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""extension"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""fork_name"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""gid"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""group"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""hash"": {
              ""properties"": {
                ""md5"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""sha1"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""sha256"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""sha384"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""sha512"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""ssdeep"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""tlsh"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""inode"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""mime_type"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""mode"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""mtime"": {
              ""type"": ""date""
            },
            ""name"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""owner"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""path"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024,
              ""fields"": {
                ""text"": {
                  ""type"": ""match_only_text""
                }
              }
            },
            ""pe"": {
              ""properties"": {
                ""architecture"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""company"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""description"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""file_version"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""imphash"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""original_file_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""pehash"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""product"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""size"": {
              ""type"": ""long""
            },
            ""target_path"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024,
              ""fields"": {
                ""text"": {
                  ""type"": ""match_only_text""
                }
              }
            },
            ""type"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""uid"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""x509"": {
              ""properties"": {
                ""alternative_names"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""issuer"": {
                  ""properties"": {
                    ""common_name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""country"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""distinguished_name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""locality"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""organization"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""organizational_unit"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""state_or_province"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""not_after"": {
                  ""type"": ""date""
                },
                ""not_before"": {
                  ""type"": ""date""
                },
                ""public_key_algorithm"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""public_key_curve"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""public_key_exponent"": {
                  ""type"": ""long"",
                  ""index"": false,
                  ""doc_values"": false
                },
                ""public_key_size"": {
                  ""type"": ""long""
                },
                ""serial_number"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""signature_algorithm"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""subject"": {
                  ""properties"": {
                    ""common_name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""country"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""distinguished_name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""locality"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""organization"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""organizational_unit"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""state_or_province"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""version_number"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            }
          }
        },
        ""group"": {
          ""properties"": {
            ""domain"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""id"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""name"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        },
        ""host"": {
          ""properties"": {
            ""architecture"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""boot"": {
              ""properties"": {
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""cpu"": {
              ""properties"": {
                ""usage"": {
                  ""type"": ""scaled_float"",
                  ""scaling_factor"": 1000.0
                }
              }
            },
            ""disk"": {
              ""properties"": {
                ""read"": {
                  ""properties"": {
                    ""bytes"": {
                      ""type"": ""long""
                    }
                  }
                },
                ""write"": {
                  ""properties"": {
                    ""bytes"": {
                      ""type"": ""long""
                    }
                  }
                }
              }
            },
            ""domain"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""geo"": {
              ""properties"": {
                ""city_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""continent_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""continent_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""country_iso_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""country_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""location"": {
                  ""type"": ""geo_point""
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""postal_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""region_iso_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""region_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""timezone"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""hostname"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""id"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""ip"": {
              ""type"": ""ip""
            },
            ""mac"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""name"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""network"": {
              ""properties"": {
                ""egress"": {
                  ""properties"": {
                    ""bytes"": {
                      ""type"": ""long""
                    },
                    ""packets"": {
                      ""type"": ""long""
                    }
                  }
                },
                ""ingress"": {
                  ""properties"": {
                    ""bytes"": {
                      ""type"": ""long""
                    },
                    ""packets"": {
                      ""type"": ""long""
                    }
                  }
                }
              }
            },
            ""os"": {
              ""properties"": {
                ""family"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""full"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""kernel"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""platform"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""type"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""version"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""pid_ns_ino"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""type"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""uptime"": {
              ""type"": ""long""
            }
          }
        },
        ""http"": {
          ""properties"": {
            ""request"": {
              ""properties"": {
                ""body"": {
                  ""properties"": {
                    ""bytes"": {
                      ""type"": ""long""
                    },
                    ""content"": {
                      ""type"": ""wildcard"",
                      ""fields"": {
                        ""text"": {
                          ""type"": ""match_only_text""
                        }
                      }
                    }
                  }
                },
                ""bytes"": {
                  ""type"": ""long""
                },
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""method"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""mime_type"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""referrer"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""response"": {
              ""properties"": {
                ""body"": {
                  ""properties"": {
                    ""bytes"": {
                      ""type"": ""long""
                    },
                    ""content"": {
                      ""type"": ""wildcard"",
                      ""fields"": {
                        ""text"": {
                          ""type"": ""match_only_text""
                        }
                      }
                    }
                  }
                },
                ""bytes"": {
                  ""type"": ""long""
                },
                ""mime_type"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""status_code"": {
                  ""type"": ""long""
                }
              }
            },
            ""version"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        },
        ""id"": {
          ""type"": ""keyword"",
          ""ignore_above"": 1024
        },
        ""labels"": {
          ""type"": ""object""
        },
        ""log"": {
          ""properties"": {
            ""file"": {
              ""properties"": {
                ""path"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""level"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""logger"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""origin"": {
              ""properties"": {
                ""file"": {
                  ""properties"": {
                    ""line"": {
                      ""type"": ""long""
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""function"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""syslog"": {
              ""properties"": {
                ""appname"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""facility"": {
                  ""properties"": {
                    ""code"": {
                      ""type"": ""long""
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""hostname"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""msgid"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""priority"": {
                  ""type"": ""long""
                },
                ""procid"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""severity"": {
                  ""properties"": {
                    ""code"": {
                      ""type"": ""long""
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""structured_data"": {
                  ""type"": ""flattened""
                },
                ""version"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            }
          }
        },
        ""message"": {
          ""type"": ""match_only_text""
        },
        ""network"": {
          ""properties"": {
            ""application"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""bytes"": {
              ""type"": ""long""
            },
            ""community_id"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""direction"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""forwarded_ip"": {
              ""type"": ""ip""
            },
            ""iana_number"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""inner"": {
              ""properties"": {
                ""vlan"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                }
              }
            },
            ""name"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""packets"": {
              ""type"": ""long""
            },
            ""protocol"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""transport"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""type"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""vlan"": {
              ""properties"": {
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            }
          }
        },
        ""observer"": {
          ""properties"": {
            ""egress"": {
              ""properties"": {
                ""interface"": {
                  ""properties"": {
                    ""alias"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""vlan"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""zone"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""geo"": {
              ""properties"": {
                ""city_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""continent_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""continent_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""country_iso_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""country_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""location"": {
                  ""type"": ""geo_point""
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""postal_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""region_iso_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""region_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""timezone"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""hostname"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""ingress"": {
              ""properties"": {
                ""interface"": {
                  ""properties"": {
                    ""alias"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""vlan"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""zone"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""ip"": {
              ""type"": ""ip""
            },
            ""mac"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""name"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""os"": {
              ""properties"": {
                ""family"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""full"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""kernel"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""platform"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""type"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""version"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""product"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""serial_number"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""type"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""vendor"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""version"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        },
        ""orchestrator"": {
          ""properties"": {
            ""api_version"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""cluster"": {
              ""properties"": {
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""url"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""version"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""namespace"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""organization"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""resource"": {
              ""properties"": {
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""ip"": {
                  ""type"": ""ip""
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""parent"": {
                  ""properties"": {
                    ""type"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""type"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""type"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        },
        ""organization"": {
          ""properties"": {
            ""id"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""name"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024,
              ""fields"": {
                ""text"": {
                  ""type"": ""match_only_text""
                }
              }
            }
          }
        },
        ""package"": {
          ""properties"": {
            ""architecture"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""build_version"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""checksum"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""description"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""install_scope"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""installed"": {
              ""type"": ""date""
            },
            ""license"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""name"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""path"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""reference"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""size"": {
              ""type"": ""long""
            },
            ""type"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""version"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        },
        ""process"": {
          ""properties"": {
            ""args"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""args_count"": {
              ""type"": ""long""
            },
            ""code_signature"": {
              ""properties"": {
                ""digest_algorithm"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""exists"": {
                  ""type"": ""boolean""
                },
                ""signing_id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""status"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""subject_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""team_id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""timestamp"": {
                  ""type"": ""date""
                },
                ""trusted"": {
                  ""type"": ""boolean""
                },
                ""valid"": {
                  ""type"": ""boolean""
                }
              }
            },
            ""command_line"": {
              ""type"": ""wildcard"",
              ""fields"": {
                ""text"": {
                  ""type"": ""match_only_text""
                }
              }
            },
            ""elf"": {
              ""properties"": {
                ""architecture"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""byte_order"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""cpu_type"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""creation_date"": {
                  ""type"": ""date""
                },
                ""exports"": {
                  ""type"": ""flattened""
                },
                ""header"": {
                  ""properties"": {
                    ""abi_version"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""class"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""data"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""entrypoint"": {
                      ""type"": ""long""
                    },
                    ""object_version"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""os_abi"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""type"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""version"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""imports"": {
                  ""type"": ""flattened""
                },
                ""sections"": {
                  ""type"": ""nested"",
                  ""properties"": {
                    ""chi2"": {
                      ""type"": ""long""
                    },
                    ""entropy"": {
                      ""type"": ""long""
                    },
                    ""flags"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""physical_offset"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""physical_size"": {
                      ""type"": ""long""
                    },
                    ""type"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""virtual_address"": {
                      ""type"": ""long""
                    },
                    ""virtual_size"": {
                      ""type"": ""long""
                    }
                  }
                },
                ""segments"": {
                  ""type"": ""nested"",
                  ""properties"": {
                    ""sections"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""type"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""shared_libraries"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""telfhash"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""end"": {
              ""type"": ""date""
            },
            ""entity_id"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""entry_leader"": {
              ""properties"": {
                ""args"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""args_count"": {
                  ""type"": ""long""
                },
                ""command_line"": {
                  ""type"": ""wildcard"",
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""entity_id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""entry_meta"": {
                  ""properties"": {
                    ""source"": {
                      ""properties"": {
                        ""ip"": {
                          ""type"": ""ip""
                        }
                      }
                    },
                    ""type"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""executable"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""group"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""interactive"": {
                  ""type"": ""boolean""
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""parent"": {
                  ""properties"": {
                    ""entity_id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""pid"": {
                      ""type"": ""long""
                    },
                    ""session_leader"": {
                      ""properties"": {
                        ""entity_id"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""pid"": {
                          ""type"": ""long""
                        },
                        ""start"": {
                          ""type"": ""date""
                        }
                      }
                    },
                    ""start"": {
                      ""type"": ""date""
                    }
                  }
                },
                ""pid"": {
                  ""type"": ""long""
                },
                ""real_group"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""real_user"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024,
                      ""fields"": {
                        ""text"": {
                          ""type"": ""match_only_text""
                        }
                      }
                    }
                  }
                },
                ""same_as_process"": {
                  ""type"": ""boolean""
                },
                ""saved_group"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""saved_user"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024,
                      ""fields"": {
                        ""text"": {
                          ""type"": ""match_only_text""
                        }
                      }
                    }
                  }
                },
                ""start"": {
                  ""type"": ""date""
                },
                ""supplemental_groups"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""tty"": {
                  ""properties"": {
                    ""char_device"": {
                      ""properties"": {
                        ""major"": {
                          ""type"": ""long""
                        },
                        ""minor"": {
                          ""type"": ""long""
                        }
                      }
                    }
                  }
                },
                ""user"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024,
                      ""fields"": {
                        ""text"": {
                          ""type"": ""match_only_text""
                        }
                      }
                    }
                  }
                },
                ""working_directory"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                }
              }
            },
            ""env_vars"": {
              ""type"": ""object""
            },
            ""executable"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024,
              ""fields"": {
                ""text"": {
                  ""type"": ""match_only_text""
                }
              }
            },
            ""exit_code"": {
              ""type"": ""long""
            },
            ""group_leader"": {
              ""properties"": {
                ""args"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""args_count"": {
                  ""type"": ""long""
                },
                ""command_line"": {
                  ""type"": ""wildcard"",
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""entity_id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""executable"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""group"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""interactive"": {
                  ""type"": ""boolean""
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""pid"": {
                  ""type"": ""long""
                },
                ""real_group"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""real_user"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024,
                      ""fields"": {
                        ""text"": {
                          ""type"": ""match_only_text""
                        }
                      }
                    }
                  }
                },
                ""same_as_process"": {
                  ""type"": ""boolean""
                },
                ""saved_group"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""saved_user"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024,
                      ""fields"": {
                        ""text"": {
                          ""type"": ""match_only_text""
                        }
                      }
                    }
                  }
                },
                ""start"": {
                  ""type"": ""date""
                },
                ""supplemental_groups"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""tty"": {
                  ""properties"": {
                    ""char_device"": {
                      ""properties"": {
                        ""major"": {
                          ""type"": ""long""
                        },
                        ""minor"": {
                          ""type"": ""long""
                        }
                      }
                    }
                  }
                },
                ""user"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024,
                      ""fields"": {
                        ""text"": {
                          ""type"": ""match_only_text""
                        }
                      }
                    }
                  }
                },
                ""working_directory"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                }
              }
            },
            ""hash"": {
              ""properties"": {
                ""md5"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""sha1"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""sha256"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""sha384"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""sha512"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""ssdeep"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""tlsh"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""interactive"": {
              ""type"": ""boolean""
            },
            ""name"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024,
              ""fields"": {
                ""text"": {
                  ""type"": ""match_only_text""
                }
              }
            },
            ""parent"": {
              ""properties"": {
                ""args"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""args_count"": {
                  ""type"": ""long""
                },
                ""code_signature"": {
                  ""properties"": {
                    ""digest_algorithm"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""exists"": {
                      ""type"": ""boolean""
                    },
                    ""signing_id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""status"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""subject_name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""team_id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""timestamp"": {
                      ""type"": ""date""
                    },
                    ""trusted"": {
                      ""type"": ""boolean""
                    },
                    ""valid"": {
                      ""type"": ""boolean""
                    }
                  }
                },
                ""command_line"": {
                  ""type"": ""wildcard"",
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""elf"": {
                  ""properties"": {
                    ""architecture"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""byte_order"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""cpu_type"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""creation_date"": {
                      ""type"": ""date""
                    },
                    ""exports"": {
                      ""type"": ""flattened""
                    },
                    ""header"": {
                      ""properties"": {
                        ""abi_version"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""class"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""data"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""entrypoint"": {
                          ""type"": ""long""
                        },
                        ""object_version"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""os_abi"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""type"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""version"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        }
                      }
                    },
                    ""imports"": {
                      ""type"": ""flattened""
                    },
                    ""sections"": {
                      ""type"": ""nested"",
                      ""properties"": {
                        ""chi2"": {
                          ""type"": ""long""
                        },
                        ""entropy"": {
                          ""type"": ""long""
                        },
                        ""flags"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""name"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""physical_offset"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""physical_size"": {
                          ""type"": ""long""
                        },
                        ""type"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""virtual_address"": {
                          ""type"": ""long""
                        },
                        ""virtual_size"": {
                          ""type"": ""long""
                        }
                      }
                    },
                    ""segments"": {
                      ""type"": ""nested"",
                      ""properties"": {
                        ""sections"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""type"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        }
                      }
                    },
                    ""shared_libraries"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""telfhash"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""end"": {
                  ""type"": ""date""
                },
                ""entity_id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""executable"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""exit_code"": {
                  ""type"": ""long""
                },
                ""group"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""group_leader"": {
                  ""properties"": {
                    ""entity_id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""pid"": {
                      ""type"": ""long""
                    },
                    ""start"": {
                      ""type"": ""date""
                    }
                  }
                },
                ""hash"": {
                  ""properties"": {
                    ""md5"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""sha1"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""sha256"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""sha384"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""sha512"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""ssdeep"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""tlsh"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""interactive"": {
                  ""type"": ""boolean""
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""pe"": {
                  ""properties"": {
                    ""architecture"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""company"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""description"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""file_version"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""imphash"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""original_file_name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""pehash"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""product"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""pgid"": {
                  ""type"": ""long""
                },
                ""pid"": {
                  ""type"": ""long""
                },
                ""real_group"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""real_user"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024,
                      ""fields"": {
                        ""text"": {
                          ""type"": ""match_only_text""
                        }
                      }
                    }
                  }
                },
                ""saved_group"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""saved_user"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024,
                      ""fields"": {
                        ""text"": {
                          ""type"": ""match_only_text""
                        }
                      }
                    }
                  }
                },
                ""start"": {
                  ""type"": ""date""
                },
                ""supplemental_groups"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""thread"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""long""
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""title"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""tty"": {
                  ""properties"": {
                    ""char_device"": {
                      ""properties"": {
                        ""major"": {
                          ""type"": ""long""
                        },
                        ""minor"": {
                          ""type"": ""long""
                        }
                      }
                    }
                  }
                },
                ""uptime"": {
                  ""type"": ""long""
                },
                ""user"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024,
                      ""fields"": {
                        ""text"": {
                          ""type"": ""match_only_text""
                        }
                      }
                    }
                  }
                },
                ""working_directory"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                }
              }
            },
            ""pe"": {
              ""properties"": {
                ""architecture"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""company"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""description"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""file_version"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""imphash"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""original_file_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""pehash"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""product"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""pgid"": {
              ""type"": ""long""
            },
            ""pid"": {
              ""type"": ""long""
            },
            ""previous"": {
              ""properties"": {
                ""args"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""args_count"": {
                  ""type"": ""long""
                },
                ""executable"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                }
              }
            },
            ""real_group"": {
              ""properties"": {
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""real_user"": {
              ""properties"": {
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                }
              }
            },
            ""saved_group"": {
              ""properties"": {
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""saved_user"": {
              ""properties"": {
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                }
              }
            },
            ""session_leader"": {
              ""properties"": {
                ""args"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""args_count"": {
                  ""type"": ""long""
                },
                ""command_line"": {
                  ""type"": ""wildcard"",
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""entity_id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""executable"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""group"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""interactive"": {
                  ""type"": ""boolean""
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""parent"": {
                  ""properties"": {
                    ""entity_id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""pid"": {
                      ""type"": ""long""
                    },
                    ""session_leader"": {
                      ""properties"": {
                        ""entity_id"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""pid"": {
                          ""type"": ""long""
                        },
                        ""start"": {
                          ""type"": ""date""
                        }
                      }
                    },
                    ""start"": {
                      ""type"": ""date""
                    }
                  }
                },
                ""pid"": {
                  ""type"": ""long""
                },
                ""real_group"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""real_user"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024,
                      ""fields"": {
                        ""text"": {
                          ""type"": ""match_only_text""
                        }
                      }
                    }
                  }
                },
                ""same_as_process"": {
                  ""type"": ""boolean""
                },
                ""saved_group"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""saved_user"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024,
                      ""fields"": {
                        ""text"": {
                          ""type"": ""match_only_text""
                        }
                      }
                    }
                  }
                },
                ""start"": {
                  ""type"": ""date""
                },
                ""supplemental_groups"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""tty"": {
                  ""properties"": {
                    ""char_device"": {
                      ""properties"": {
                        ""major"": {
                          ""type"": ""long""
                        },
                        ""minor"": {
                          ""type"": ""long""
                        }
                      }
                    }
                  }
                },
                ""user"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024,
                      ""fields"": {
                        ""text"": {
                          ""type"": ""match_only_text""
                        }
                      }
                    }
                  }
                },
                ""working_directory"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                }
              }
            },
            ""start"": {
              ""type"": ""date""
            },
            ""supplemental_groups"": {
              ""properties"": {
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""thread"": {
              ""properties"": {
                ""id"": {
                  ""type"": ""long""
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""title"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024,
              ""fields"": {
                ""text"": {
                  ""type"": ""match_only_text""
                }
              }
            },
            ""tty"": {
              ""properties"": {
                ""char_device"": {
                  ""properties"": {
                    ""major"": {
                      ""type"": ""long""
                    },
                    ""minor"": {
                      ""type"": ""long""
                    }
                  }
                }
              }
            },
            ""uptime"": {
              ""type"": ""long""
            },
            ""user"": {
              ""properties"": {
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                }
              }
            },
            ""working_directory"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024,
              ""fields"": {
                ""text"": {
                  ""type"": ""match_only_text""
                }
              }
            }
          }
        },
        ""registry"": {
          ""properties"": {
            ""data"": {
              ""properties"": {
                ""bytes"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""strings"": {
                  ""type"": ""wildcard""
                },
                ""type"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""hive"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""key"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""path"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""value"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        },
        ""related"": {
          ""properties"": {
            ""hash"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""hosts"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""ip"": {
              ""type"": ""ip""
            },
            ""user"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        },
        ""rule"": {
          ""properties"": {
            ""author"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""category"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""description"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""id"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""license"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""name"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""reference"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""ruleset"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""uuid"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""version"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        },
        ""server"": {
          ""properties"": {
            ""address"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""as"": {
              ""properties"": {
                ""number"": {
                  ""type"": ""long""
                },
                ""organization"": {
                  ""properties"": {
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024,
                      ""fields"": {
                        ""text"": {
                          ""type"": ""match_only_text""
                        }
                      }
                    }
                  }
                }
              }
            },
            ""bytes"": {
              ""type"": ""long""
            },
            ""domain"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""geo"": {
              ""properties"": {
                ""city_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""continent_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""continent_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""country_iso_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""country_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""location"": {
                  ""type"": ""geo_point""
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""postal_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""region_iso_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""region_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""timezone"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""ip"": {
              ""type"": ""ip""
            },
            ""mac"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""nat"": {
              ""properties"": {
                ""ip"": {
                  ""type"": ""ip""
                },
                ""port"": {
                  ""type"": ""long""
                }
              }
            },
            ""packets"": {
              ""type"": ""long""
            },
            ""port"": {
              ""type"": ""long""
            },
            ""registered_domain"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""subdomain"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""top_level_domain"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""user"": {
              ""properties"": {
                ""domain"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""email"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""full_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""group"": {
                  ""properties"": {
                    ""domain"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""hash"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""roles"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            }
          }
        },
        ""service"": {
          ""properties"": {
            ""address"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""environment"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""ephemeral_id"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""id"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""name"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""node"": {
              ""properties"": {
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""role"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""roles"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""origin"": {
              ""properties"": {
                ""address"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""environment"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""ephemeral_id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""node"": {
                  ""properties"": {
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""role"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""roles"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""state"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""type"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""version"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""state"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""target"": {
              ""properties"": {
                ""address"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""environment"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""ephemeral_id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""node"": {
                  ""properties"": {
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""role"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""roles"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""state"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""type"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""version"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""type"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""version"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        },
        ""source"": {
          ""properties"": {
            ""address"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""as"": {
              ""properties"": {
                ""number"": {
                  ""type"": ""long""
                },
                ""organization"": {
                  ""properties"": {
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024,
                      ""fields"": {
                        ""text"": {
                          ""type"": ""match_only_text""
                        }
                      }
                    }
                  }
                }
              }
            },
            ""bytes"": {
              ""type"": ""long""
            },
            ""domain"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""geo"": {
              ""properties"": {
                ""city_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""continent_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""continent_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""country_iso_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""country_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""location"": {
                  ""type"": ""geo_point""
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""postal_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""region_iso_code"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""region_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""timezone"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""ip"": {
              ""type"": ""ip""
            },
            ""mac"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""nat"": {
              ""properties"": {
                ""ip"": {
                  ""type"": ""ip""
                },
                ""port"": {
                  ""type"": ""long""
                }
              }
            },
            ""packets"": {
              ""type"": ""long""
            },
            ""port"": {
              ""type"": ""long""
            },
            ""registered_domain"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""subdomain"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""top_level_domain"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""user"": {
              ""properties"": {
                ""domain"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""email"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""full_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""group"": {
                  ""properties"": {
                    ""domain"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""hash"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""roles"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            }
          }
        },
        ""span"": {
          ""properties"": {
            ""id"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        },
        ""tags"": {
          ""type"": ""keyword"",
          ""ignore_above"": 1024
        },
        ""threat"": {
          ""properties"": {
            ""enrichments"": {
              ""type"": ""nested"",
              ""properties"": {
                ""indicator"": {
                  ""properties"": {
                    ""as"": {
                      ""properties"": {
                        ""number"": {
                          ""type"": ""long""
                        },
                        ""organization"": {
                          ""properties"": {
                            ""name"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024,
                              ""fields"": {
                                ""text"": {
                                  ""type"": ""match_only_text""
                                }
                              }
                            }
                          }
                        }
                      }
                    },
                    ""confidence"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""description"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""email"": {
                      ""properties"": {
                        ""address"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        }
                      }
                    },
                    ""file"": {
                      ""properties"": {
                        ""accessed"": {
                          ""type"": ""date""
                        },
                        ""attributes"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""code_signature"": {
                          ""properties"": {
                            ""digest_algorithm"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""exists"": {
                              ""type"": ""boolean""
                            },
                            ""signing_id"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""status"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""subject_name"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""team_id"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""timestamp"": {
                              ""type"": ""date""
                            },
                            ""trusted"": {
                              ""type"": ""boolean""
                            },
                            ""valid"": {
                              ""type"": ""boolean""
                            }
                          }
                        },
                        ""created"": {
                          ""type"": ""date""
                        },
                        ""ctime"": {
                          ""type"": ""date""
                        },
                        ""device"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""directory"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""drive_letter"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1
                        },
                        ""elf"": {
                          ""properties"": {
                            ""architecture"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""byte_order"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""cpu_type"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""creation_date"": {
                              ""type"": ""date""
                            },
                            ""exports"": {
                              ""type"": ""flattened""
                            },
                            ""header"": {
                              ""properties"": {
                                ""abi_version"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                },
                                ""class"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                },
                                ""data"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                },
                                ""entrypoint"": {
                                  ""type"": ""long""
                                },
                                ""object_version"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                },
                                ""os_abi"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                },
                                ""type"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                },
                                ""version"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                }
                              }
                            },
                            ""imports"": {
                              ""type"": ""flattened""
                            },
                            ""sections"": {
                              ""type"": ""nested"",
                              ""properties"": {
                                ""chi2"": {
                                  ""type"": ""long""
                                },
                                ""entropy"": {
                                  ""type"": ""long""
                                },
                                ""flags"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                },
                                ""name"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                },
                                ""physical_offset"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                },
                                ""physical_size"": {
                                  ""type"": ""long""
                                },
                                ""type"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                },
                                ""virtual_address"": {
                                  ""type"": ""long""
                                },
                                ""virtual_size"": {
                                  ""type"": ""long""
                                }
                              }
                            },
                            ""segments"": {
                              ""type"": ""nested"",
                              ""properties"": {
                                ""sections"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                },
                                ""type"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                }
                              }
                            },
                            ""shared_libraries"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""telfhash"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            }
                          }
                        },
                        ""extension"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""fork_name"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""gid"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""group"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""hash"": {
                          ""properties"": {
                            ""md5"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""sha1"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""sha256"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""sha384"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""sha512"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""ssdeep"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""tlsh"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            }
                          }
                        },
                        ""inode"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""mime_type"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""mode"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""mtime"": {
                          ""type"": ""date""
                        },
                        ""name"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""owner"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""path"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024,
                          ""fields"": {
                            ""text"": {
                              ""type"": ""match_only_text""
                            }
                          }
                        },
                        ""pe"": {
                          ""properties"": {
                            ""architecture"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""company"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""description"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""file_version"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""imphash"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""original_file_name"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""pehash"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""product"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            }
                          }
                        },
                        ""size"": {
                          ""type"": ""long""
                        },
                        ""target_path"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024,
                          ""fields"": {
                            ""text"": {
                              ""type"": ""match_only_text""
                            }
                          }
                        },
                        ""type"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""uid"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""x509"": {
                          ""properties"": {
                            ""alternative_names"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""issuer"": {
                              ""properties"": {
                                ""common_name"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                },
                                ""country"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                },
                                ""distinguished_name"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                },
                                ""locality"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                },
                                ""organization"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                },
                                ""organizational_unit"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                },
                                ""state_or_province"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                }
                              }
                            },
                            ""not_after"": {
                              ""type"": ""date""
                            },
                            ""not_before"": {
                              ""type"": ""date""
                            },
                            ""public_key_algorithm"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""public_key_curve"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""public_key_exponent"": {
                              ""type"": ""long"",
                              ""index"": false,
                              ""doc_values"": false
                            },
                            ""public_key_size"": {
                              ""type"": ""long""
                            },
                            ""serial_number"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""signature_algorithm"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""subject"": {
                              ""properties"": {
                                ""common_name"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                },
                                ""country"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                },
                                ""distinguished_name"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                },
                                ""locality"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                },
                                ""organization"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                },
                                ""organizational_unit"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                },
                                ""state_or_province"": {
                                  ""type"": ""keyword"",
                                  ""ignore_above"": 1024
                                }
                              }
                            },
                            ""version_number"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            }
                          }
                        }
                      }
                    },
                    ""first_seen"": {
                      ""type"": ""date""
                    },
                    ""geo"": {
                      ""properties"": {
                        ""city_name"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""continent_code"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""continent_name"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""country_iso_code"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""country_name"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""location"": {
                          ""type"": ""geo_point""
                        },
                        ""name"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""postal_code"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""region_iso_code"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""region_name"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""timezone"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        }
                      }
                    },
                    ""ip"": {
                      ""type"": ""ip""
                    },
                    ""last_seen"": {
                      ""type"": ""date""
                    },
                    ""marking"": {
                      ""properties"": {
                        ""tlp"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        }
                      }
                    },
                    ""modified_at"": {
                      ""type"": ""date""
                    },
                    ""port"": {
                      ""type"": ""long""
                    },
                    ""provider"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""reference"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""registry"": {
                      ""properties"": {
                        ""data"": {
                          ""properties"": {
                            ""bytes"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""strings"": {
                              ""type"": ""wildcard""
                            },
                            ""type"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            }
                          }
                        },
                        ""hive"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""key"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""path"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""value"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        }
                      }
                    },
                    ""scanner_stats"": {
                      ""type"": ""long""
                    },
                    ""sightings"": {
                      ""type"": ""long""
                    },
                    ""type"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""url"": {
                      ""properties"": {
                        ""domain"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""extension"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""fragment"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""full"": {
                          ""type"": ""wildcard"",
                          ""fields"": {
                            ""text"": {
                              ""type"": ""match_only_text""
                            }
                          }
                        },
                        ""original"": {
                          ""type"": ""wildcard"",
                          ""fields"": {
                            ""text"": {
                              ""type"": ""match_only_text""
                            }
                          }
                        },
                        ""password"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""path"": {
                          ""type"": ""wildcard""
                        },
                        ""port"": {
                          ""type"": ""long""
                        },
                        ""query"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""registered_domain"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""scheme"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""subdomain"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""top_level_domain"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""username"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        }
                      }
                    },
                    ""x509"": {
                      ""properties"": {
                        ""alternative_names"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""issuer"": {
                          ""properties"": {
                            ""common_name"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""country"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""distinguished_name"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""locality"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""organization"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""organizational_unit"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""state_or_province"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            }
                          }
                        },
                        ""not_after"": {
                          ""type"": ""date""
                        },
                        ""not_before"": {
                          ""type"": ""date""
                        },
                        ""public_key_algorithm"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""public_key_curve"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""public_key_exponent"": {
                          ""type"": ""long"",
                          ""index"": false,
                          ""doc_values"": false
                        },
                        ""public_key_size"": {
                          ""type"": ""long""
                        },
                        ""serial_number"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""signature_algorithm"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""subject"": {
                          ""properties"": {
                            ""common_name"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""country"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""distinguished_name"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""locality"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""organization"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""organizational_unit"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""state_or_province"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            }
                          }
                        },
                        ""version_number"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        }
                      }
                    }
                  }
                },
                ""matched"": {
                  ""properties"": {
                    ""atomic"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""field"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""index"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""occurred"": {
                      ""type"": ""date""
                    },
                    ""type"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                }
              }
            },
            ""feed"": {
              ""properties"": {
                ""dashboard_id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""description"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""reference"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""framework"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""group"": {
              ""properties"": {
                ""alias"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""reference"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""indicator"": {
              ""properties"": {
                ""as"": {
                  ""properties"": {
                    ""number"": {
                      ""type"": ""long""
                    },
                    ""organization"": {
                      ""properties"": {
                        ""name"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024,
                          ""fields"": {
                            ""text"": {
                              ""type"": ""match_only_text""
                            }
                          }
                        }
                      }
                    }
                  }
                },
                ""confidence"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""description"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""email"": {
                  ""properties"": {
                    ""address"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""file"": {
                  ""properties"": {
                    ""accessed"": {
                      ""type"": ""date""
                    },
                    ""attributes"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""code_signature"": {
                      ""properties"": {
                        ""digest_algorithm"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""exists"": {
                          ""type"": ""boolean""
                        },
                        ""signing_id"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""status"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""subject_name"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""team_id"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""timestamp"": {
                          ""type"": ""date""
                        },
                        ""trusted"": {
                          ""type"": ""boolean""
                        },
                        ""valid"": {
                          ""type"": ""boolean""
                        }
                      }
                    },
                    ""created"": {
                      ""type"": ""date""
                    },
                    ""ctime"": {
                      ""type"": ""date""
                    },
                    ""device"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""directory"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""drive_letter"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1
                    },
                    ""elf"": {
                      ""properties"": {
                        ""architecture"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""byte_order"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""cpu_type"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""creation_date"": {
                          ""type"": ""date""
                        },
                        ""exports"": {
                          ""type"": ""flattened""
                        },
                        ""header"": {
                          ""properties"": {
                            ""abi_version"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""class"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""data"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""entrypoint"": {
                              ""type"": ""long""
                            },
                            ""object_version"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""os_abi"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""type"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""version"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            }
                          }
                        },
                        ""imports"": {
                          ""type"": ""flattened""
                        },
                        ""sections"": {
                          ""type"": ""nested"",
                          ""properties"": {
                            ""chi2"": {
                              ""type"": ""long""
                            },
                            ""entropy"": {
                              ""type"": ""long""
                            },
                            ""flags"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""name"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""physical_offset"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""physical_size"": {
                              ""type"": ""long""
                            },
                            ""type"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""virtual_address"": {
                              ""type"": ""long""
                            },
                            ""virtual_size"": {
                              ""type"": ""long""
                            }
                          }
                        },
                        ""segments"": {
                          ""type"": ""nested"",
                          ""properties"": {
                            ""sections"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""type"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            }
                          }
                        },
                        ""shared_libraries"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""telfhash"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        }
                      }
                    },
                    ""extension"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""fork_name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""gid"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""group"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""hash"": {
                      ""properties"": {
                        ""md5"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""sha1"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""sha256"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""sha384"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""sha512"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""ssdeep"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""tlsh"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        }
                      }
                    },
                    ""inode"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""mime_type"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""mode"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""mtime"": {
                      ""type"": ""date""
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""owner"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""path"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024,
                      ""fields"": {
                        ""text"": {
                          ""type"": ""match_only_text""
                        }
                      }
                    },
                    ""pe"": {
                      ""properties"": {
                        ""architecture"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""company"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""description"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""file_version"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""imphash"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""original_file_name"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""pehash"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""product"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        }
                      }
                    },
                    ""size"": {
                      ""type"": ""long""
                    },
                    ""target_path"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024,
                      ""fields"": {
                        ""text"": {
                          ""type"": ""match_only_text""
                        }
                      }
                    },
                    ""type"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""uid"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""x509"": {
                      ""properties"": {
                        ""alternative_names"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""issuer"": {
                          ""properties"": {
                            ""common_name"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""country"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""distinguished_name"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""locality"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""organization"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""organizational_unit"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""state_or_province"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            }
                          }
                        },
                        ""not_after"": {
                          ""type"": ""date""
                        },
                        ""not_before"": {
                          ""type"": ""date""
                        },
                        ""public_key_algorithm"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""public_key_curve"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""public_key_exponent"": {
                          ""type"": ""long"",
                          ""index"": false,
                          ""doc_values"": false
                        },
                        ""public_key_size"": {
                          ""type"": ""long""
                        },
                        ""serial_number"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""signature_algorithm"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""subject"": {
                          ""properties"": {
                            ""common_name"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""country"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""distinguished_name"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""locality"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""organization"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""organizational_unit"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            },
                            ""state_or_province"": {
                              ""type"": ""keyword"",
                              ""ignore_above"": 1024
                            }
                          }
                        },
                        ""version_number"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        }
                      }
                    }
                  }
                },
                ""first_seen"": {
                  ""type"": ""date""
                },
                ""geo"": {
                  ""properties"": {
                    ""city_name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""continent_code"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""continent_name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""country_iso_code"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""country_name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""location"": {
                      ""type"": ""geo_point""
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""postal_code"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""region_iso_code"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""region_name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""timezone"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""ip"": {
                  ""type"": ""ip""
                },
                ""last_seen"": {
                  ""type"": ""date""
                },
                ""marking"": {
                  ""properties"": {
                    ""tlp"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""modified_at"": {
                  ""type"": ""date""
                },
                ""port"": {
                  ""type"": ""long""
                },
                ""provider"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""reference"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""registry"": {
                  ""properties"": {
                    ""data"": {
                      ""properties"": {
                        ""bytes"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""strings"": {
                          ""type"": ""wildcard""
                        },
                        ""type"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        }
                      }
                    },
                    ""hive"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""key"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""path"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""value"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""scanner_stats"": {
                  ""type"": ""long""
                },
                ""sightings"": {
                  ""type"": ""long""
                },
                ""type"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""url"": {
                  ""properties"": {
                    ""domain"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""extension"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""fragment"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""full"": {
                      ""type"": ""wildcard"",
                      ""fields"": {
                        ""text"": {
                          ""type"": ""match_only_text""
                        }
                      }
                    },
                    ""original"": {
                      ""type"": ""wildcard"",
                      ""fields"": {
                        ""text"": {
                          ""type"": ""match_only_text""
                        }
                      }
                    },
                    ""password"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""path"": {
                      ""type"": ""wildcard""
                    },
                    ""port"": {
                      ""type"": ""long""
                    },
                    ""query"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""registered_domain"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""scheme"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""subdomain"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""top_level_domain"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""username"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""x509"": {
                  ""properties"": {
                    ""alternative_names"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""issuer"": {
                      ""properties"": {
                        ""common_name"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""country"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""distinguished_name"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""locality"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""organization"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""organizational_unit"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""state_or_province"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        }
                      }
                    },
                    ""not_after"": {
                      ""type"": ""date""
                    },
                    ""not_before"": {
                      ""type"": ""date""
                    },
                    ""public_key_algorithm"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""public_key_curve"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""public_key_exponent"": {
                      ""type"": ""long"",
                      ""index"": false,
                      ""doc_values"": false
                    },
                    ""public_key_size"": {
                      ""type"": ""long""
                    },
                    ""serial_number"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""signature_algorithm"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""subject"": {
                      ""properties"": {
                        ""common_name"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""country"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""distinguished_name"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""locality"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""organization"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""organizational_unit"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""state_or_province"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        }
                      }
                    },
                    ""version_number"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                }
              }
            },
            ""software"": {
              ""properties"": {
                ""alias"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""platforms"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""reference"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""type"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""tactic"": {
              ""properties"": {
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""reference"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""technique"": {
              ""properties"": {
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""reference"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""subtechnique"": {
                  ""properties"": {
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024,
                      ""fields"": {
                        ""text"": {
                          ""type"": ""match_only_text""
                        }
                      }
                    },
                    ""reference"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                }
              }
            }
          }
        },
        ""title"": {
          ""type"": ""keyword"",
          ""ignore_above"": 1024
        },
        ""tls"": {
          ""properties"": {
            ""cipher"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""client"": {
              ""properties"": {
                ""certificate"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""certificate_chain"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""hash"": {
                  ""properties"": {
                    ""md5"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""sha1"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""sha256"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""issuer"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""ja3"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""not_after"": {
                  ""type"": ""date""
                },
                ""not_before"": {
                  ""type"": ""date""
                },
                ""server_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""subject"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""supported_ciphers"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""x509"": {
                  ""properties"": {
                    ""alternative_names"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""issuer"": {
                      ""properties"": {
                        ""common_name"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""country"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""distinguished_name"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""locality"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""organization"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""organizational_unit"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""state_or_province"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        }
                      }
                    },
                    ""not_after"": {
                      ""type"": ""date""
                    },
                    ""not_before"": {
                      ""type"": ""date""
                    },
                    ""public_key_algorithm"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""public_key_curve"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""public_key_exponent"": {
                      ""type"": ""long"",
                      ""index"": false,
                      ""doc_values"": false
                    },
                    ""public_key_size"": {
                      ""type"": ""long""
                    },
                    ""serial_number"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""signature_algorithm"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""subject"": {
                      ""properties"": {
                        ""common_name"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""country"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""distinguished_name"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""locality"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""organization"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""organizational_unit"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""state_or_province"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        }
                      }
                    },
                    ""version_number"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                }
              }
            },
            ""curve"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""established"": {
              ""type"": ""boolean""
            },
            ""next_protocol"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""resumed"": {
              ""type"": ""boolean""
            },
            ""server"": {
              ""properties"": {
                ""certificate"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""certificate_chain"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""hash"": {
                  ""properties"": {
                    ""md5"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""sha1"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""sha256"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""issuer"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""ja3s"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""not_after"": {
                  ""type"": ""date""
                },
                ""not_before"": {
                  ""type"": ""date""
                },
                ""subject"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""x509"": {
                  ""properties"": {
                    ""alternative_names"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""issuer"": {
                      ""properties"": {
                        ""common_name"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""country"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""distinguished_name"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""locality"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""organization"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""organizational_unit"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""state_or_province"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        }
                      }
                    },
                    ""not_after"": {
                      ""type"": ""date""
                    },
                    ""not_before"": {
                      ""type"": ""date""
                    },
                    ""public_key_algorithm"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""public_key_curve"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""public_key_exponent"": {
                      ""type"": ""long"",
                      ""index"": false,
                      ""doc_values"": false
                    },
                    ""public_key_size"": {
                      ""type"": ""long""
                    },
                    ""serial_number"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""signature_algorithm"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""subject"": {
                      ""properties"": {
                        ""common_name"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""country"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""distinguished_name"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""locality"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""organization"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""organizational_unit"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        },
                        ""state_or_province"": {
                          ""type"": ""keyword"",
                          ""ignore_above"": 1024
                        }
                      }
                    },
                    ""version_number"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                }
              }
            },
            ""version"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""version_protocol"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        },
        ""trace"": {
          ""properties"": {
            ""id"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        },
        ""transaction"": {
          ""properties"": {
            ""id"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        },
        ""url"": {
          ""properties"": {
            ""domain"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""extension"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""fragment"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""full"": {
              ""type"": ""wildcard"",
              ""fields"": {
                ""text"": {
                  ""type"": ""match_only_text""
                }
              }
            },
            ""original"": {
              ""type"": ""wildcard"",
              ""fields"": {
                ""text"": {
                  ""type"": ""match_only_text""
                }
              }
            },
            ""password"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""path"": {
              ""type"": ""wildcard""
            },
            ""port"": {
              ""type"": ""long""
            },
            ""query"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""registered_domain"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""scheme"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""subdomain"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""top_level_domain"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""username"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        },
        ""user"": {
          ""properties"": {
            ""changes"": {
              ""properties"": {
                ""domain"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""email"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""full_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""group"": {
                  ""properties"": {
                    ""domain"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""hash"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""roles"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""domain"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""effective"": {
              ""properties"": {
                ""domain"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""email"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""full_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""group"": {
                  ""properties"": {
                    ""domain"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""hash"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""roles"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""email"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""full_name"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024,
              ""fields"": {
                ""text"": {
                  ""type"": ""match_only_text""
                }
              }
            },
            ""group"": {
              ""properties"": {
                ""domain"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""hash"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""id"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""name"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024,
              ""fields"": {
                ""text"": {
                  ""type"": ""match_only_text""
                }
              }
            },
            ""roles"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""target"": {
              ""properties"": {
                ""domain"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""email"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""full_name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""group"": {
                  ""properties"": {
                    ""domain"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""id"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    },
                    ""name"": {
                      ""type"": ""keyword"",
                      ""ignore_above"": 1024
                    }
                  }
                },
                ""hash"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""id"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""roles"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            }
          }
        },
        ""user_agent"": {
          ""properties"": {
            ""device"": {
              ""properties"": {
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""name"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""original"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024,
              ""fields"": {
                ""text"": {
                  ""type"": ""match_only_text""
                }
              }
            },
            ""os"": {
              ""properties"": {
                ""family"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""full"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""kernel"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""name"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024,
                  ""fields"": {
                    ""text"": {
                      ""type"": ""match_only_text""
                    }
                  }
                },
                ""platform"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""type"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                },
                ""version"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""version"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        },
        ""vulnerability"": {
          ""properties"": {
            ""category"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""classification"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""description"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024,
              ""fields"": {
                ""text"": {
                  ""type"": ""match_only_text""
                }
              }
            },
            ""enumeration"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""id"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""reference"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""report_id"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            },
            ""scanner"": {
              ""properties"": {
                ""vendor"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""score"": {
              ""properties"": {
                ""base"": {
                  ""type"": ""float""
                },
                ""environmental"": {
                  ""type"": ""float""
                },
                ""temporal"": {
                  ""type"": ""float""
                },
                ""version"": {
                  ""type"": ""keyword"",
                  ""ignore_above"": 1024
                }
              }
            },
            ""severity"": {
              ""type"": ""keyword"",
              ""ignore_above"": 1024
            }
          }
        }
      }
    },
    ""settings"": {
      ""index"": {
        ""lifecycle"": {
          ""name"": ""7-days-default""
        },
        ""codec"": ""best_compression"",
        ""routing"": {
          ""allocation"": {
            ""include"": {
              ""_tier_preference"": ""data_content""
            }
          }
        },
        ""mapping"": {
          ""total_fields"": {
            ""limit"": ""2000""
          }
        },
        ""number_of_shards"": ""1"",
        ""provided_name"": ""catalog-data-2023.01.31"",
        ""creation_date"": ""1675173789218"",
        ""number_of_replicas"": ""1"",
        ""uuid"": ""lJ64stwrRnaesUtAfZqevA"",
        ""version"": {
          ""created"": ""8030199""
        }
      }
    }
  }
}";

		var response = DeserializeJsonString<GetIndexResponse>(json);
	}

	[U]
	public void GetIndexResponse_DeserializedCorrectly_WhenDynamicTemplateArrayIsPresentInResponse()
	{
		const string json = @"{
  ""catalog-data-2023.01.31"": {
    ""aliases"": {      
    },
    ""mappings"": {
      ""dynamic_templates"": [
        {
          ""strings_as_keyword"": {
            ""match_mapping_type"": ""string"",
            ""mapping"": {
              ""ignore_above"": 1024,
              ""type"": ""keyword""
            }
          }
        }
      ],
      ""properties"": {
        ""@timestamp"": {
          ""type"": ""date""
        }
      }
    },
    ""settings"": {
    }
  }
}";

		var response = DeserializeJsonString<GetIndexResponse>(json);
		VerifyGetIndexResponseDynamicTemplates(response);
	}

	[U]
	public void GetIndexResponse_DeserializedCorrectly_WhenSingleDynamicTemplateIsPresentInResponse()
	{
		const string json = @"{
  ""catalog-data-2023.01.31"": {
    ""aliases"": {      
    },
    ""mappings"": {
      ""dynamic_templates"": 
        {
          ""strings_as_keyword"": {
            ""match_mapping_type"": ""string"",
            ""mapping"": {
              ""ignore_above"": 1024,
              ""type"": ""keyword""
            }
          }
        },
      ""properties"": {
        ""@timestamp"": {
          ""type"": ""date""
        }
      }
    },
    ""settings"": {
    }
  }
}";

		var response = DeserializeJsonString<GetIndexResponse>(json);
		VerifyGetIndexResponseDynamicTemplates(response);
	}

	private static void VerifyGetIndexResponseDynamicTemplates(GetIndexResponse response)
	{
		response.Indices.TryGetValue("catalog-data-2023.01.31", out var indexState).Should().BeTrue();
		indexState.Mappings.DynamicTemplates.Should().HaveCount(1);

		var templateDictionary = indexState.Mappings.DynamicTemplates.Single();
		templateDictionary.TryGetValue("strings_as_keyword", out var dynamicTemplate).Should().BeTrue();
		dynamicTemplate.MatchMappingType.Should().Be("string");

		var keywordProperty = dynamicTemplate.Mapping.Should().BeOfType<KeywordProperty>().Subject;
		keywordProperty.IgnoreAbove.Should().Be(1024);
	}
}
