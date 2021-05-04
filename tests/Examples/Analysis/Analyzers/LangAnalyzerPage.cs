// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Analyzers
{
	public class LangAnalyzerPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:85")]
		public void Line85()
		{
			// tag::137c62a4443bdd7d5b95a15022a9dc30[]
			var response0 = new SearchResponse<object>();
			// end::137c62a4443bdd7d5b95a15022a9dc30[]

			response0.MatchesExample(@"PUT /arabic_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""arabic_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_arabic_"" \<1>
			        },
			        ""arabic_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""مثال""] \<2>
			        },
			        ""arabic_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""arabic""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_arabic"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""decimal_digit"",
			            ""arabic_stop"",
			            ""arabic_normalization"",
			            ""arabic_keywords"",
			            ""arabic_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:135")]
		public void Line135()
		{
			// tag::f7dc2fed08e57abda2c3e8a14f8eb098[]
			var response0 = new SearchResponse<object>();
			// end::f7dc2fed08e57abda2c3e8a14f8eb098[]

			response0.MatchesExample(@"PUT /armenian_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""armenian_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_armenian_"" \<1>
			        },
			        ""armenian_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""օրինակ""] \<2>
			        },
			        ""armenian_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""armenian""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_armenian"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""armenian_stop"",
			            ""armenian_keywords"",
			            ""armenian_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:183")]
		public void Line183()
		{
			// tag::01f50acf7998b24969f451e922d145eb[]
			var response0 = new SearchResponse<object>();
			// end::01f50acf7998b24969f451e922d145eb[]

			response0.MatchesExample(@"PUT /basque_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""basque_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_basque_"" \<1>
			        },
			        ""basque_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""Adibidez""] \<2>
			        },
			        ""basque_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""basque""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_basque"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""basque_stop"",
			            ""basque_keywords"",
			            ""basque_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:231")]
		public void Line231()
		{
			// tag::496d35c89dc991a1509f7e8fb93ade45[]
			var response0 = new SearchResponse<object>();
			// end::496d35c89dc991a1509f7e8fb93ade45[]

			response0.MatchesExample(@"PUT /bengali_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""bengali_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_bengali_"" \<1>
			        },
			        ""bengali_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""উদাহরণ""] \<2>
			        },
			        ""bengali_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""bengali""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_bengali"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""decimal_digit"",
			            ""bengali_keywords"",
			            ""indic_normalization"",
			            ""bengali_normalization"",
			            ""bengali_stop"",
			            ""bengali_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:282")]
		public void Line282()
		{
			// tag::13670d1534125831c2059eebd86d840c[]
			var response0 = new SearchResponse<object>();
			// end::13670d1534125831c2059eebd86d840c[]

			response0.MatchesExample(@"PUT /brazilian_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""brazilian_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_brazilian_"" \<1>
			        },
			        ""brazilian_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""exemplo""] \<2>
			        },
			        ""brazilian_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""brazilian""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_brazilian"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""brazilian_stop"",
			            ""brazilian_keywords"",
			            ""brazilian_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:330")]
		public void Line330()
		{
			// tag::d0378fe5e3aad05a2fd2e6e81213374f[]
			var response0 = new SearchResponse<object>();
			// end::d0378fe5e3aad05a2fd2e6e81213374f[]

			response0.MatchesExample(@"PUT /bulgarian_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""bulgarian_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_bulgarian_"" \<1>
			        },
			        ""bulgarian_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""пример""] \<2>
			        },
			        ""bulgarian_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""bulgarian""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_bulgarian"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""bulgarian_stop"",
			            ""bulgarian_keywords"",
			            ""bulgarian_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:378")]
		public void Line378()
		{
			// tag::7ab968a61bb0783f563dd6d29b253901[]
			var response0 = new SearchResponse<object>();
			// end::7ab968a61bb0783f563dd6d29b253901[]

			response0.MatchesExample(@"PUT /catalan_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""catalan_elision"": {
			          ""type"":       ""elision"",
			          ""articles"":   [ ""d"", ""l"", ""m"", ""n"", ""s"", ""t""],
			          ""articles_case"": true
			        },
			        ""catalan_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_catalan_"" <1>
			        },
			        ""catalan_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""example""] <2>
			        },
			        ""catalan_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""catalan""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_catalan"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""catalan_elision"",
			            ""lowercase"",
			            ""catalan_stop"",
			            ""catalan_keywords"",
			            ""catalan_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:435")]
		public void Line435()
		{
			// tag::d305110a8cabfbebd1e38d85559d1023[]
			var response0 = new SearchResponse<object>();
			// end::d305110a8cabfbebd1e38d85559d1023[]

			response0.MatchesExample(@"PUT /cjk_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""english_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  [ \<1>
			            ""a"", ""and"", ""are"", ""as"", ""at"", ""be"", ""but"", ""by"", ""for"",
			            ""if"", ""in"", ""into"", ""is"", ""it"", ""no"", ""not"", ""of"", ""on"",
			            ""or"", ""s"", ""such"", ""t"", ""that"", ""the"", ""their"", ""then"",
			            ""there"", ""these"", ""they"", ""this"", ""to"", ""was"", ""will"",
			            ""with"", ""www""
			          ]
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_cjk"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""cjk_width"",
			            ""lowercase"",
			            ""cjk_bigram"",
			            ""english_stop""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:481")]
		public void Line481()
		{
			// tag::a28111cdd9b5aaea96c779cbfbf38780[]
			var response0 = new SearchResponse<object>();
			// end::a28111cdd9b5aaea96c779cbfbf38780[]

			response0.MatchesExample(@"PUT /czech_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""czech_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_czech_"" \<1>
			        },
			        ""czech_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""příklad""] \<2>
			        },
			        ""czech_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""czech""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_czech"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""czech_stop"",
			            ""czech_keywords"",
			            ""czech_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:529")]
		public void Line529()
		{
			// tag::ed85ed833bec7286a0dfbe64077c5715[]
			var response0 = new SearchResponse<object>();
			// end::ed85ed833bec7286a0dfbe64077c5715[]

			response0.MatchesExample(@"PUT /danish_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""danish_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_danish_"" \<1>
			        },
			        ""danish_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""eksempel""] \<2>
			        },
			        ""danish_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""danish""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_danish"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""danish_stop"",
			            ""danish_keywords"",
			            ""danish_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:577")]
		public void Line577()
		{
			// tag::10d8b17e73d31dcd907de67327ed78a2[]
			var response0 = new SearchResponse<object>();
			// end::10d8b17e73d31dcd907de67327ed78a2[]

			response0.MatchesExample(@"PUT /dutch_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""dutch_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_dutch_"" \<1>
			        },
			        ""dutch_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""voorbeeld""] \<2>
			        },
			        ""dutch_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""dutch""
			        },
			        ""dutch_override"": {
			          ""type"":       ""stemmer_override"",
			          ""rules"": [
			            ""fiets=>fiets"",
			            ""bromfiets=>bromfiets"",
			            ""ei=>eier"",
			            ""kind=>kinder""
			          ]
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_dutch"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""dutch_stop"",
			            ""dutch_keywords"",
			            ""dutch_override"",
			            ""dutch_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:635")]
		public void Line635()
		{
			// tag::81c7a392efd505b686eed978fb7d9d17[]
			var response0 = new SearchResponse<object>();
			// end::81c7a392efd505b686eed978fb7d9d17[]

			response0.MatchesExample(@"PUT /english_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""english_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_english_"" \<1>
			        },
			        ""english_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""example""] \<2>
			        },
			        ""english_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""english""
			        },
			        ""english_possessive_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""possessive_english""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_english"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""english_possessive_stemmer"",
			            ""lowercase"",
			            ""english_stop"",
			            ""english_keywords"",
			            ""english_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:688")]
		public void Line688()
		{
			// tag::2f4e28c81db47547ad39d0926babab12[]
			var response0 = new SearchResponse<object>();
			// end::2f4e28c81db47547ad39d0926babab12[]

			response0.MatchesExample(@"PUT /estonian_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""estonian_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_estonian_"" <1>
			        },
			        ""estonian_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""näide""] <2>
			        },
			        ""estonian_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""estonian""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_estonian"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""estonian_stop"",
			            ""estonian_keywords"",
			            ""estonian_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:736")]
		public void Line736()
		{
			// tag::85f0e5e8ab91ceab63c21dbedd9f4037[]
			var response0 = new SearchResponse<object>();
			// end::85f0e5e8ab91ceab63c21dbedd9f4037[]

			response0.MatchesExample(@"PUT /finnish_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""finnish_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_finnish_"" \<1>
			        },
			        ""finnish_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""esimerkki""] \<2>
			        },
			        ""finnish_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""finnish""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_finnish"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""finnish_stop"",
			            ""finnish_keywords"",
			            ""finnish_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:784")]
		public void Line784()
		{
			// tag::f545bb95214769aca993c1632a71ad2c[]
			var response0 = new SearchResponse<object>();
			// end::f545bb95214769aca993c1632a71ad2c[]

			response0.MatchesExample(@"PUT /french_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""french_elision"": {
			          ""type"":         ""elision"",
			          ""articles_case"": true,
			          ""articles"": [
			              ""l"", ""m"", ""t"", ""qu"", ""n"", ""s"",
			              ""j"", ""d"", ""c"", ""jusqu"", ""quoiqu"",
			              ""lorsqu"", ""puisqu""
			            ]
			        },
			        ""french_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_french_"" <1>
			        },
			        ""french_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""Example""] <2>
			        },
			        ""french_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""light_french""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_french"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""french_elision"",
			            ""lowercase"",
			            ""french_stop"",
			            ""french_keywords"",
			            ""french_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:842")]
		public void Line842()
		{
			// tag::9606c271921cb800d5ea395b16d6ceaf[]
			var response0 = new SearchResponse<object>();
			// end::9606c271921cb800d5ea395b16d6ceaf[]

			response0.MatchesExample(@"PUT /galician_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""galician_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_galician_"" \<1>
			        },
			        ""galician_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""exemplo""] \<2>
			        },
			        ""galician_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""galician""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_galician"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""galician_stop"",
			            ""galician_keywords"",
			            ""galician_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:890")]
		public void Line890()
		{
			// tag::187e8786e0a90f1f6278cf89b670de0a[]
			var response0 = new SearchResponse<object>();
			// end::187e8786e0a90f1f6278cf89b670de0a[]

			response0.MatchesExample(@"PUT /german_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""german_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_german_"" \<1>
			        },
			        ""german_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""Beispiel""] \<2>
			        },
			        ""german_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""light_german""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_german"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""german_stop"",
			            ""german_keywords"",
			            ""german_normalization"",
			            ""german_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:939")]
		public void Line939()
		{
			// tag::1f00e73c144603e97f6c14ab15fa1913[]
			var response0 = new SearchResponse<object>();
			// end::1f00e73c144603e97f6c14ab15fa1913[]

			response0.MatchesExample(@"PUT /greek_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""greek_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_greek_"" \<1>
			        },
			        ""greek_lowercase"": {
			          ""type"":       ""lowercase"",
			          ""language"":   ""greek""
			        },
			        ""greek_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""παράδειγμα""] \<2>
			        },
			        ""greek_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""greek""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_greek"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""greek_lowercase"",
			            ""greek_stop"",
			            ""greek_keywords"",
			            ""greek_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:991")]
		public void Line991()
		{
			// tag::af00a58d9171d32f6efe52d94e51e526[]
			var response0 = new SearchResponse<object>();
			// end::af00a58d9171d32f6efe52d94e51e526[]

			response0.MatchesExample(@"PUT /hindi_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""hindi_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_hindi_"" \<1>
			        },
			        ""hindi_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""उदाहरण""] \<2>
			        },
			        ""hindi_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""hindi""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_hindi"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""decimal_digit"",
			            ""hindi_keywords"",
			            ""indic_normalization"",
			            ""hindi_normalization"",
			            ""hindi_stop"",
			            ""hindi_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:1042")]
		public void Line1042()
		{
			// tag::84108653e9e03b4edacd878ec870df77[]
			var response0 = new SearchResponse<object>();
			// end::84108653e9e03b4edacd878ec870df77[]

			response0.MatchesExample(@"PUT /hungarian_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""hungarian_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_hungarian_"" \<1>
			        },
			        ""hungarian_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""példa""] \<2>
			        },
			        ""hungarian_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""hungarian""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_hungarian"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""hungarian_stop"",
			            ""hungarian_keywords"",
			            ""hungarian_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:1091")]
		public void Line1091()
		{
			// tag::eb5987b58dae90c3a8a1609410be0570[]
			var response0 = new SearchResponse<object>();
			// end::eb5987b58dae90c3a8a1609410be0570[]

			response0.MatchesExample(@"PUT /indonesian_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""indonesian_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_indonesian_"" \<1>
			        },
			        ""indonesian_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""contoh""] \<2>
			        },
			        ""indonesian_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""indonesian""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_indonesian"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""indonesian_stop"",
			            ""indonesian_keywords"",
			            ""indonesian_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:1139")]
		public void Line1139()
		{
			// tag::160f39a50847bad0be4be1529a95e4ce[]
			var response0 = new SearchResponse<object>();
			// end::160f39a50847bad0be4be1529a95e4ce[]

			response0.MatchesExample(@"PUT /irish_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""irish_hyphenation"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  [ ""h"", ""n"", ""t"" ],
			          ""ignore_case"": true
			        },
			        ""irish_elision"": {
			          ""type"":       ""elision"",
			          ""articles"":   [ ""d"", ""m"", ""b"" ],
			          ""articles_case"": true
			        },
			        ""irish_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_irish_"" \<1>
			        },
			        ""irish_lowercase"": {
			          ""type"":       ""lowercase"",
			          ""language"":   ""irish""
			        },
			        ""irish_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""sampla""] \<2>
			        },
			        ""irish_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""irish""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_irish"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""irish_hyphenation"",
			            ""irish_elision"",
			            ""irish_lowercase"",
			            ""irish_stop"",
			            ""irish_keywords"",
			            ""irish_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:1203")]
		public void Line1203()
		{
			// tag::00e0c964c79fcc1876ab957da2ffce82[]
			var response0 = new SearchResponse<object>();
			// end::00e0c964c79fcc1876ab957da2ffce82[]

			response0.MatchesExample(@"PUT /italian_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""italian_elision"": {
			          ""type"": ""elision"",
			          ""articles"": [
			                ""c"", ""l"", ""all"", ""dall"", ""dell"",
			                ""nell"", ""sull"", ""coll"", ""pell"",
			                ""gl"", ""agl"", ""dagl"", ""degl"", ""negl"",
			                ""sugl"", ""un"", ""m"", ""t"", ""s"", ""v"", ""d""
			          ],
			          ""articles_case"": true
			        },
			        ""italian_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_italian_"" \<1>
			        },
			        ""italian_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""esempio""] \<2>
			        },
			        ""italian_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""light_italian""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_italian"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""italian_elision"",
			            ""lowercase"",
			            ""italian_stop"",
			            ""italian_keywords"",
			            ""italian_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:1262")]
		public void Line1262()
		{
			// tag::d983c1ea730eeabac9e914656d7c9be2[]
			var response0 = new SearchResponse<object>();
			// end::d983c1ea730eeabac9e914656d7c9be2[]

			response0.MatchesExample(@"PUT /latvian_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""latvian_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_latvian_"" \<1>
			        },
			        ""latvian_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""piemērs""] \<2>
			        },
			        ""latvian_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""latvian""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_latvian"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""latvian_stop"",
			            ""latvian_keywords"",
			            ""latvian_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:1310")]
		public void Line1310()
		{
			// tag::bb067c049331cc850a77b18bdfff81b5[]
			var response0 = new SearchResponse<object>();
			// end::bb067c049331cc850a77b18bdfff81b5[]

			response0.MatchesExample(@"PUT /lithuanian_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""lithuanian_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_lithuanian_"" \<1>
			        },
			        ""lithuanian_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""pavyzdys""] \<2>
			        },
			        ""lithuanian_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""lithuanian""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_lithuanian"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""lithuanian_stop"",
			            ""lithuanian_keywords"",
			            ""lithuanian_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:1358")]
		public void Line1358()
		{
			// tag::2731a8577ad734a732d784c5dcb1225d[]
			var response0 = new SearchResponse<object>();
			// end::2731a8577ad734a732d784c5dcb1225d[]

			response0.MatchesExample(@"PUT /norwegian_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""norwegian_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_norwegian_"" \<1>
			        },
			        ""norwegian_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""eksempel""] \<2>
			        },
			        ""norwegian_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""norwegian""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_norwegian"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""norwegian_stop"",
			            ""norwegian_keywords"",
			            ""norwegian_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:1406")]
		public void Line1406()
		{
			// tag::d1a285aa244ec461d68f13e7078a33c0[]
			var response0 = new SearchResponse<object>();
			// end::d1a285aa244ec461d68f13e7078a33c0[]

			response0.MatchesExample(@"PUT /persian_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""char_filter"": {
			        ""zero_width_spaces"": {
			            ""type"":       ""mapping"",
			            ""mappings"": [ ""\\u200C=>\\u0020""] \<1>
			        }
			      },
			      ""filter"": {
			        ""persian_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_persian_"" \<2>
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_persian"": {
			          ""tokenizer"":     ""standard"",
			          ""char_filter"": [ ""zero_width_spaces"" ],
			          ""filter"": [
			            ""lowercase"",
			            ""decimal_digit"",
			            ""arabic_normalization"",
			            ""persian_normalization"",
			            ""persian_stop""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:1452")]
		public void Line1452()
		{
			// tag::584f502cf840134f2db5f39e2483ced1[]
			var response0 = new SearchResponse<object>();
			// end::584f502cf840134f2db5f39e2483ced1[]

			response0.MatchesExample(@"PUT /portuguese_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""portuguese_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_portuguese_"" \<1>
			        },
			        ""portuguese_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""exemplo""] \<2>
			        },
			        ""portuguese_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""light_portuguese""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_portuguese"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""portuguese_stop"",
			            ""portuguese_keywords"",
			            ""portuguese_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:1500")]
		public void Line1500()
		{
			// tag::1ba7afe23a26fe9ac7856d8c5bc1059d[]
			var response0 = new SearchResponse<object>();
			// end::1ba7afe23a26fe9ac7856d8c5bc1059d[]

			response0.MatchesExample(@"PUT /romanian_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""romanian_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_romanian_"" \<1>
			        },
			        ""romanian_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""exemplu""] \<2>
			        },
			        ""romanian_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""romanian""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_romanian"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""romanian_stop"",
			            ""romanian_keywords"",
			            ""romanian_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:1549")]
		public void Line1549()
		{
			// tag::d260225cf97e068ead2a8a6bb5aefd90[]
			var response0 = new SearchResponse<object>();
			// end::d260225cf97e068ead2a8a6bb5aefd90[]

			response0.MatchesExample(@"PUT /russian_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""russian_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_russian_"" \<1>
			        },
			        ""russian_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""пример""] \<2>
			        },
			        ""russian_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""russian""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_russian"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""russian_stop"",
			            ""russian_keywords"",
			            ""russian_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:1597")]
		public void Line1597()
		{
			// tag::320645d771e952af2a67bb7445c3688d[]
			var response0 = new SearchResponse<object>();
			// end::320645d771e952af2a67bb7445c3688d[]

			response0.MatchesExample(@"PUT /sorani_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""sorani_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_sorani_"" \<1>
			        },
			        ""sorani_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""mînak""] \<2>
			        },
			        ""sorani_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""sorani""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_sorani"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""sorani_normalization"",
			            ""lowercase"",
			            ""decimal_digit"",
			            ""sorani_stop"",
			            ""sorani_keywords"",
			            ""sorani_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:1647")]
		public void Line1647()
		{
			// tag::327466380bcd55361973b4a96c6dccb2[]
			var response0 = new SearchResponse<object>();
			// end::327466380bcd55361973b4a96c6dccb2[]

			response0.MatchesExample(@"PUT /spanish_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""spanish_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_spanish_"" \<1>
			        },
			        ""spanish_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""ejemplo""] \<2>
			        },
			        ""spanish_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""light_spanish""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_spanish"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""spanish_stop"",
			            ""spanish_keywords"",
			            ""spanish_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:1695")]
		public void Line1695()
		{
			// tag::f097c02541056f3c0fc855e7bbeef8a8[]
			var response0 = new SearchResponse<object>();
			// end::f097c02541056f3c0fc855e7bbeef8a8[]

			response0.MatchesExample(@"PUT /swedish_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""swedish_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_swedish_"" \<1>
			        },
			        ""swedish_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""exempel""] \<2>
			        },
			        ""swedish_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""swedish""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_swedish"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""lowercase"",
			            ""swedish_stop"",
			            ""swedish_keywords"",
			            ""swedish_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:1743")]
		public void Line1743()
		{
			// tag::103296e16b4233926ad1f07360385606[]
			var response0 = new SearchResponse<object>();
			// end::103296e16b4233926ad1f07360385606[]

			response0.MatchesExample(@"PUT /turkish_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""turkish_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_turkish_"" \<1>
			        },
			        ""turkish_lowercase"": {
			          ""type"":       ""lowercase"",
			          ""language"":   ""turkish""
			        },
			        ""turkish_keywords"": {
			          ""type"":       ""keyword_marker"",
			          ""keywords"":   [""örnek""] \<2>
			        },
			        ""turkish_stemmer"": {
			          ""type"":       ""stemmer"",
			          ""language"":   ""turkish""
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_turkish"": {
			          ""tokenizer"":  ""standard"",
			          ""filter"": [
			            ""apostrophe"",
			            ""turkish_lowercase"",
			            ""turkish_stop"",
			            ""turkish_keywords"",
			            ""turkish_stemmer""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/analyzers/lang-analyzer.asciidoc:1796")]
		public void Line1796()
		{
			// tag::346f28d82acb5427c304aa574fea0008[]
			var response0 = new SearchResponse<object>();
			// end::346f28d82acb5427c304aa574fea0008[]

			response0.MatchesExample(@"PUT /thai_example
			{
			  ""settings"": {
			    ""analysis"": {
			      ""filter"": {
			        ""thai_stop"": {
			          ""type"":       ""stop"",
			          ""stopwords"":  ""_thai_"" \<1>
			        }
			      },
			      ""analyzer"": {
			        ""rebuilt_thai"": {
			          ""tokenizer"":  ""thai"",
			          ""filter"": [
			            ""lowercase"",
			            ""decimal_digit"",
			            ""thai_stop""
			          ]
			        }
			      }
			    }
			  }
			}");
		}
	}
}
