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
	public class GithubIssue3715
	{
		[U]
		public void CanDeserializeExtendedStats()
		{
			var json = @"{
    ""took"": 99,
    ""timed_out"": false,
    ""_shards"": {
        ""total"": 3,
        ""successful"": 3,
        ""skipped"": 0,
        ""failed"": 0
    },
    ""hits"": {
        ""total"": {
            ""value"": 10000,
            ""relation"": ""gte""
        },
        ""max_score"": null,
        ""hits"": []
    },
    ""aggregations"": {
        ""filter#filter"": {
            ""doc_count"": 285903,
            ""lterms#d"": {
                ""doc_count_error_upper_bound"": 0,
                ""sum_other_doc_count"": 0,
                ""buckets"": [
                    {
                        ""key"": 101,
                        ""doc_count"": 95068,
                        ""tdigest_percentiles#pp"": {
                            ""values"": {
                                ""50.0"": 0.0
                            }
                        },
                        ""extended_stats#qs"": {
                            ""count"": 16110,
                            ""min"": 0.0,
                            ""max"": 1801447.0,
                            ""avg"": 31503.353134698944,
                            ""sum"": 5.07519019E8,
                            ""sum_of_squares"": 1.68025874794985E14,
                            ""variance"": 9.437450274168343E9,
                            ""std_deviation"": 97146.54020688715,
                            ""std_deviation_bounds"": {
                                ""upper"": 225796.43354847323,
                                ""lower"": -162789.72727907536
                            }
                        },
                        ""extended_stats#ps"": {
                            ""count"": 95068,
                            ""min"": 0.0,
                            ""max"": 5958503.0,
                            ""avg"": 4801.276076071865,
                            ""sum"": 4.56447714E8,
                            ""sum_of_squares"": 1.2068715728964E14,
                            ""variance"": 1.2464301952332447E9,
                            ""std_deviation"": 35304.81830052726,
                            ""std_deviation_bounds"": {
                                ""upper"": 75410.91267712638,
                                ""lower"": -65808.36052498265
                            }
                        },
                        ""tdigest_percentiles#tp"": {
                            ""values"": {
                                ""50.0"": 271.5856925670251
                            }
                        },
                        ""tdigest_percentiles#dp"": {
                            ""values"": {
                                ""50.0"": 648.049116497329
                            }
                        },
                        ""tdigest_percentiles#qp"": {
                            ""values"": {
                                ""50.0"": 768.7588339657976
                            }
                        },
                        ""extended_stats#ts"": {
                            ""count"": 95068,
                            ""min"": 0.0,
                            ""max"": 36017.0,
                            ""avg"": 315.96469895232883,
                            ""sum"": 3.0038132E7,
                            ""sum_of_squares"": 1.3990669014E10,
                            ""variance"": 47331.17010486898,
                            ""std_deviation"": 217.55728005486046,
                            ""std_deviation_bounds"": {
                                ""upper"": 751.0792590620497,
                                ""lower"": -119.14986115739208
                            }
                        },
                        ""extended_stats#ds"": {
                            ""count"": 90792,
                            ""min"": 0.0,
                            ""max"": 3.5378698E7,
                            ""avg"": 5667.729502599348,
                            ""sum"": 5.14584497E8,
                            ""sum_of_squares"": 6.808428300863287E15,
                            ""variance"": 7.495717436699335E10,
                            ""std_deviation"": 273783.07903702406,
                            ""std_deviation_bounds"": {
                                ""upper"": 553233.8875766475,
                                ""lower"": -541898.4285714488
                            }
                        }
                    },
                    {
                        ""key"": 102,
                        ""doc_count"": 77252,
                        ""tdigest_percentiles#pp"": {
                            ""values"": {
                                ""50.0"": 0.04755484258284208
                            }
                        },
                        ""extended_stats#qs"": {
                            ""count"": 25515,
                            ""min"": 0.0,
                            ""max"": 2584845.0,
                            ""avg"": 36470.966607877715,
                            ""sum"": 9.30556713E8,
                            ""sum_of_squares"": 3.16949960993977E14,
                            ""variance"": 1.1091971710265238E10,
                            ""std_deviation"": 105318.4300598202,
                            ""std_deviation_bounds"": {
                                ""upper"": 247107.8267275181,
                                ""lower"": -174165.89351176267
                            }
                        },
                        ""extended_stats#ps"": {
                            ""count"": 77252,
                            ""min"": 0.0,
                            ""max"": 2673561.0,
                            ""avg"": 6659.815991818982,
                            ""sum"": 5.14484105E8,
                            ""sum_of_squares"": 1.04305457220823E14,
                            ""variance"": 1.3058443503185332E9,
                            ""std_deviation"": 36136.46842621084,
                            ""std_deviation_bounds"": {
                                ""upper"": 78932.75284424066,
                                ""lower"": -65613.1208606027
                            }
                        },
                        ""tdigest_percentiles#tp"": {
                            ""values"": {
                                ""50.0"": 281.59719021322684
                            }
                        },
                        ""tdigest_percentiles#dp"": {
                            ""values"": {
                                ""50.0"": 670.2614773996437
                            }
                        },
                        ""tdigest_percentiles#qp"": {
                            ""values"": {
                                ""50.0"": 1183.019605734767
                            }
                        },
                        ""extended_stats#ts"": {
                            ""count"": 77252,
                            ""min"": 0.0,
                            ""max"": 6341.0,
                            ""avg"": 326.32038005488533,
                            ""sum"": 2.5208902E7,
                            ""sum_of_squares"": 1.1218474478E10,
                            ""variance"": 38734.22042916224,
                            ""std_deviation"": 196.81011261914932,
                            ""std_deviation_bounds"": {
                                ""upper"": 719.940605293184,
                                ""lower"": -67.29984518341331
                            }
                        },
                        ""extended_stats#ds"": {
                            ""count"": 73793,
                            ""min"": 0.0,
                            ""max"": 6639782.0,
                            ""avg"": 2608.6924640548564,
                            ""sum"": 1.92503243E8,
                            ""sum_of_squares"": 1.25256075857975E14,
                            ""variance"": 1.6905925236628783E9,
                            ""std_deviation"": 41116.81558271358,
                            ""std_deviation_bounds"": {
                                ""upper"": 84842.32362948201,
                                ""lower"": -79624.9387013723
                            }
                        }
                    },
                    {
                        ""key"": 90,
                        ""doc_count"": 52153,
                        ""tdigest_percentiles#pp"": {
                            ""values"": {
                                ""50.0"": 0.0
                            }
                        },
                        ""extended_stats#qs"": {
                            ""count"": 8922,
                            ""min"": 0.0,
                            ""max"": 1913557.0,
                            ""avg"": 36307.94776955839,
                            ""sum"": 3.2393951E8,
                            ""sum_of_squares"": 1.22221158323898E14,
                            ""variance"": 1.2380585016175932E10,
                            ""std_deviation"": 111268.07725567982,
                            ""std_deviation_bounds"": {
                                ""upper"": 258844.10228091804,
                                ""lower"": -186228.20674180123
                            }
                        },
                        ""extended_stats#ps"": {
                            ""count"": 52153,
                            ""min"": 0.0,
                            ""max"": 6557177.0,
                            ""avg"": 15631.252602918337,
                            ""sum"": 8.15216717E8,
                            ""sum_of_squares"": 5.75629496296623E14,
                            ""variance"": 1.0792986747973734E10,
                            ""std_deviation"": 103889.30044991993,
                            ""std_deviation_bounds"": {
                                ""upper"": 223409.8535027582,
                                ""lower"": -192147.34829692153
                            }
                        },
                        ""tdigest_percentiles#tp"": {
                            ""values"": {
                                ""50.0"": 74.2732817039577
                            }
                        },
                        ""tdigest_percentiles#dp"": {
                            ""values"": {
                                ""50.0"": 1.0
                            }
                        },
                        ""tdigest_percentiles#qp"": {
                            ""values"": {
                                ""50.0"": 817.6689997218875
                            }
                        },
                        ""extended_stats#ts"": {
                            ""count"": 52153,
                            ""min"": 0.0,
                            ""max"": 1.27188995E8,
                            ""avg"": 14529.932487105249,
                            ""sum"": 7.57779569E8,
                            ""sum_of_squares"": 7.3151332914453952E16,
                            ""variance"": 1.4024183158874133E12,
                            ""std_deviation"": 1184237.4406711743,
                            ""std_deviation_bounds"": {
                                ""upper"": 2383004.8138294537,
                                ""lower"": -2353944.9488552436
                            }
                        },
                        ""extended_stats#ds"": {
                            ""count"": 51611,
                            ""min"": 0.0,
                            ""max"": 602528.0,
                            ""avg"": 19.16610800023251,
                            ""sum"": 989182.0,
                            ""sum_of_squares"": 3.99775785168E11,
                            ""variance"": 7745574.129525801,
                            ""std_deviation"": 2783.087158090059,
                            ""std_deviation_bounds"": {
                                ""upper"": 5585.34042418035,
                                ""lower"": -5547.0082081798855
                            }
                        }
                    },
                    {
                        ""key"": 103,
                        ""doc_count"": 25540,
                        ""tdigest_percentiles#pp"": {
                            ""values"": {
                                ""50.0"": 3826.72732033258
                            }
                        },
                        ""extended_stats#qs"": {
                            ""count"": 17071,
                            ""min"": 0.0,
                            ""max"": 259133.0,
                            ""avg"": 7321.023783023842,
                            ""sum"": 1.24977197E8,
                            ""sum_of_squares"": 1.3199933702703E13,
                            ""variance"": 7.196398963816029E8,
                            ""std_deviation"": 26826.104756032004,
                            ""std_deviation_bounds"": {
                                ""upper"": 60973.23329508785,
                                ""lower"": -46331.18572904017
                            }
                        },
                        ""extended_stats#ps"": {
                            ""count"": 25540,
                            ""min"": 0.0,
                            ""max"": 520280.0,
                            ""avg"": 12038.269968676586,
                            ""sum"": 3.07457415E8,
                            ""sum_of_squares"": 2.8396697501261E13,
                            ""variance"": 9.669319551926221E8,
                            ""std_deviation"": 31095.52950494045,
                            ""std_deviation_bounds"": {
                                ""upper"": 74229.32897855749,
                                ""lower"": -50152.78904120431
                            }
                        },
                        ""tdigest_percentiles#tp"": {
                            ""values"": {
                                ""50.0"": 155.8047619047619
                            }
                        },
                        ""tdigest_percentiles#dp"": {
                            ""values"": {
                                ""50.0"": 5641.342239159542
                            }
                        },
                        ""tdigest_percentiles#qp"": {
                            ""values"": {
                                ""50.0"": 19.0
                            }
                        },
                        ""extended_stats#ts"": {
                            ""count"": 25540,
                            ""min"": 0.0,
                            ""max"": 240193.0,
                            ""avg"": 225.48598277212216,
                            ""sum"": 5758912.0,
                            ""sum_of_squares"": 1.8248033644E11,
                            ""variance"": 7094040.0355513645,
                            ""std_deviation"": 2663.4639166978336,
                            ""std_deviation_bounds"": {
                                ""upper"": 5552.413816167789,
                                ""lower"": -5101.441850623545
                            }
                        },
                        ""extended_stats#ds"": {
                            ""count"": 11278,
                            ""min"": 0.0,
                            ""max"": 2.9021871E7,
                            ""avg"": 64464.69347402022,
                            ""sum"": 7.27032813E8,
                            ""sum_of_squares"": 4.728208731426765E15,
                            ""variance"": 4.150860776725629E11,
                            ""std_deviation"": 644271.7421030996,
                            ""std_deviation_bounds"": {
                                ""upper"": 1353008.1776802193,
                                ""lower"": -1224078.790732179
                            }
                        }
                    },
                    {
                        ""key"": 80,
                        ""doc_count"": 20927,
                        ""tdigest_percentiles#pp"": {
                            ""values"": {
                                ""50.0"": 2949.387118501673
                            }
                        },
                        ""extended_stats#qs"": {
                            ""count"": 12031,
                            ""min"": 0.0,
                            ""max"": 1608730.0,
                            ""avg"": 61025.894190009145,
                            ""sum"": 7.34202533E8,
                            ""sum_of_squares"": 1.63417993399929E14,
                            ""variance"": 9.858916740672789E9,
                            ""std_deviation"": 99292.07793511418,
                            ""std_deviation_bounds"": {
                                ""upper"": 259610.05006023753,
                                ""lower"": -137558.2616802192
                            }
                        },
                        ""extended_stats#ps"": {
                            ""count"": 20926,
                            ""min"": 0.0,
                            ""max"": 5354898.0,
                            ""avg"": 39613.42693300201,
                            ""sum"": 8.28950572E8,
                            ""sum_of_squares"": 1.4363900903777E14,
                            ""variance"": 5.294917142443744E9,
                            ""std_deviation"": 72766.18131002714,
                            ""std_deviation_bounds"": {
                                ""upper"": 185145.7895530563,
                                ""lower"": -105918.93568705226
                            }
                        },
                        ""tdigest_percentiles#tp"": {
                            ""values"": {
                                ""50.0"": 82.77833096254149
                            }
                        },
                        ""tdigest_percentiles#dp"": {
                            ""values"": {
                                ""50.0"": 219.2937254901961
                            }
                        },
                        ""tdigest_percentiles#qp"": {
                            ""values"": {
                                ""50.0"": 13835.860802283652
                            }
                        },
                        ""extended_stats#ts"": {
                            ""count"": 20926,
                            ""min"": 7.0,
                            ""max"": 3701.0,
                            ""avg"": 121.0365573927172,
                            ""sum"": 2532811.0,
                            ""sum_of_squares"": 7.00794875E8,
                            ""variance"": 18839.345839319245,
                            ""std_deviation"": 137.25649652864976,
                            ""std_deviation_bounds"": {
                                ""upper"": 395.54955045001674,
                                ""lower"": -153.4764356645823
                            }
                        },
                        ""extended_stats#ds"": {
                            ""count"": 20488,
                            ""min"": 0.0,
                            ""max"": 1834889.0,
                            ""avg"": 1459.575898086685,
                            ""sum"": 2.9903791E7,
                            ""sum_of_squares"": 2.0237617597161E13,
                            ""variance"": 9.856487087346728E8,
                            ""std_deviation"": 31395.042741405414,
                            ""std_deviation_bounds"": {
                                ""upper"": 64249.66138089751,
                                ""lower"": -61330.50958472415
                            }
                        }
                    },
                    {
                        ""key"": 91,
                        ""doc_count"": 12829,
                        ""tdigest_percentiles#pp"": {
                            ""values"": {
                                ""50.0"": 0.0
                            }
                        },
                        ""extended_stats#qs"": {
                            ""count"": 0,
                            ""min"": null,
                            ""max"": null,
                            ""avg"": null,
                            ""sum"": 0.0,
                            ""sum_of_squares"": null,
                            ""variance"": null,
                            ""std_deviation"": null,
                            ""std_deviation_bounds"": {
                                ""upper"": null,
                                ""lower"": null
                            }
                        },
                        ""extended_stats#ps"": {
                            ""count"": 12829,
                            ""min"": 0.0,
                            ""max"": 0.0,
                            ""avg"": 0.0,
                            ""sum"": 0.0,
                            ""sum_of_squares"": 0.0,
                            ""variance"": 0.0,
                            ""std_deviation"": 0.0,
                            ""std_deviation_bounds"": {
                                ""upper"": 0.0,
                                ""lower"": 0.0
                            }
                        },
                        ""tdigest_percentiles#tp"": {
                            ""values"": {
                                ""50.0"": 36.0
                            }
                        },
                        ""tdigest_percentiles#dp"": {
                            ""values"": {
                                ""50.0"": 2.0
                            }
                        },
                        ""tdigest_percentiles#qp"": {
                            ""values"": {
                                ""50.0"": null
                            }
                        },
                        ""extended_stats#ts"": {
                            ""count"": 12829,
                            ""min"": 4.0,
                            ""max"": 331713.0,
                            ""avg"": 168.68773871696936,
                            ""sum"": 2164095.0,
                            ""sum_of_squares"": 2.42915170229E11,
                            ""variance"": 1.8906392855022315E7,
                            ""std_deviation"": 4348.148209873062,
                            ""std_deviation_bounds"": {
                                ""upper"": 8864.984158463094,
                                ""lower"": -8527.608681029154
                            }
                        },
                        ""extended_stats#ds"": {
                            ""count"": 12823,
                            ""min"": 0.0,
                            ""max"": 4490.0,
                            ""avg"": 2.810106839273181,
                            ""sum"": 36034.0,
                            ""sum_of_squares"": 4.5201418E7,
                            ""variance"": 3517.130048362601,
                            ""std_deviation"": 59.30539645228418,
                            ""std_deviation_bounds"": {
                                ""upper"": 121.42089974384153,
                                ""lower"": -115.80068606529518
                            }
                        }
                    },
                    {
                        ""key"": 92,
                        ""doc_count"": 2134,
                        ""tdigest_percentiles#pp"": {
                            ""values"": {
                                ""50.0"": 0.0
                            }
                        },
                        ""extended_stats#qs"": {
                            ""count"": 0,
                            ""min"": null,
                            ""max"": null,
                            ""avg"": null,
                            ""sum"": 0.0,
                            ""sum_of_squares"": null,
                            ""variance"": null,
                            ""std_deviation"": null,
                            ""std_deviation_bounds"": {
                                ""upper"": null,
                                ""lower"": null
                            }
                        },
                        ""extended_stats#ps"": {
                            ""count"": 2134,
                            ""min"": 0.0,
                            ""max"": 0.0,
                            ""avg"": 0.0,
                            ""sum"": 0.0,
                            ""sum_of_squares"": 0.0,
                            ""variance"": 0.0,
                            ""std_deviation"": 0.0,
                            ""std_deviation_bounds"": {
                                ""upper"": 0.0,
                                ""lower"": 0.0
                            }
                        },
                        ""tdigest_percentiles#tp"": {
                            ""values"": {
                                ""50.0"": 46.44117647058824
                            }
                        },
                        ""tdigest_percentiles#dp"": {
                            ""values"": {
                                ""50.0"": 2.0
                            }
                        },
                        ""tdigest_percentiles#qp"": {
                            ""values"": {
                                ""50.0"": null
                            }
                        },
                        ""extended_stats#ts"": {
                            ""count"": 2134,
                            ""min"": 5.0,
                            ""max"": 83459.0,
                            ""avg"": 445.2825679475164,
                            ""sum"": 950233.0,
                            ""sum_of_squares"": 2.5478409473E10,
                            ""variance"": 1.1740996852207838E7,
                            ""std_deviation"": 3426.5138044677183,
                            ""std_deviation_bounds"": {
                                ""upper"": 7298.310176882953,
                                ""lower"": -6407.74504098792
                            }
                        },
                        ""extended_stats#ds"": {
                            ""count"": 2132,
                            ""min"": 1.0,
                            ""max"": 27701.0,
                            ""avg"": 85.66369606003752,
                            ""sum"": 182635.0,
                            ""sum_of_squares"": 4.393040829E9,
                            ""variance"": 2053187.448344313,
                            ""std_deviation"": 1432.8947792299032,
                            ""std_deviation_bounds"": {
                                ""upper"": 2951.453254519844,
                                ""lower"": -2780.1258623997687
                            }
                        }
                    }
                ]
            }
        }
    }
}";

			var bytes = Encoding.UTF8.GetBytes(json);

			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(pool, new InMemoryConnection(bytes)).DefaultIndex("default_index");
			var client = new ElasticClient(connectionSettings);

			var response = client.Search<object>(s => s);
			var filterAgg = response.Aggregations.Filter("filter");
			filterAgg.Should().NotBeNull();
			filterAgg.DocCount.Should().Be(285903);

			var termsAggregate = filterAgg.Terms("d");
			termsAggregate.Should().NotBeNull();
			termsAggregate.DocCountErrorUpperBound.Should().Be(0);
			termsAggregate.SumOtherDocCount.Should().Be(0);

			foreach (var bucket in termsAggregate.Buckets)
			{
				bucket.Key.Should().NotBeNullOrEmpty();
				bucket.DocCount.Should().BeGreaterThan(0);

				AssertPercentilesAggregate(bucket, "pp");
				AssertPercentilesAggregate(bucket, "tp");
				AssertPercentilesAggregate(bucket, "dp");
				AssertPercentilesAggregate(bucket, "qp");

				AssertExtendedStats(bucket, "qs");
				AssertExtendedStats(bucket, "ps");
				AssertExtendedStats(bucket, "ts");
				AssertExtendedStats(bucket, "ds");
			}
		}

		private static void AssertExtendedStats(AggregateDictionary bucket, string name)
		{
			var extendedStatsAggregate = bucket.ExtendedStats(name);
			extendedStatsAggregate.Should().NotBeNull();
			extendedStatsAggregate.Count.Should().BeGreaterOrEqualTo(0);
			extendedStatsAggregate.StdDeviationBounds.Should().NotBeNull();
		}

		private static void AssertPercentilesAggregate(AggregateDictionary bucket, string name)
		{
			var percentilesAggregate = bucket.Percentiles(name);
			percentilesAggregate.Should().NotBeNull();
			percentilesAggregate.Items.Should().HaveCount(1).And.Contain(p => Math.Abs(p.Percentile - 50d) < double.Epsilon);
		}
	}
}
