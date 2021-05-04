// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;
using System.Linq;
using Examples.Models;

namespace Examples.QueryDsl
{
	public class NestedQueryPage : ExampleBase
	{
		[U]
		[Description("query-dsl/nested-query.asciidoc:23")]
		public void Line23()
		{
			// tag::c612d93e7f682a0d731e385edf9f5d56[]
			var createIndexResponse = client.Indices.Create("my_index", c => c
				.Map(m => m
					.Properties(p => p
						.Nested<object>(n => n
							.Name("obj1")
						)
					)
				)
			);
			// end::c612d93e7f682a0d731e385edf9f5d56[]

			createIndexResponse.MatchesExample(@"PUT /my_index
			{
			    ""mappings"" : {
			        ""properties"" : {
			            ""obj1"" : {
			                ""type"" : ""nested""
			            }
			        }
			    }
			}");
		}

		[U]
		[Description("query-dsl/nested-query.asciidoc:41")]
		public void Line41()
		{
			// tag::6be70810d6ebd6f09d8a49f9df847765[]
			var searchResponse = client.Search<object>(s => s
				.Index("my_index")
				.Query(q => q
					.Nested(n => n
						.Path("obj1")
						.Query(nq => nq
							.Match(m => m
								.Field("obj1.name")
								.Query("blue")
							) && q
							.LongRange(r => r
								.Field("obj1.count")
								.GreaterThan(5)
							)
						)
						.ScoreMode(NestedScoreMode.Average)
					)
				)
			);
			// end::6be70810d6ebd6f09d8a49f9df847765[]

			searchResponse.MatchesExample(@"GET /my_index/_search
			{
			    ""query"":  {
			        ""nested"" : {
			            ""path"" : ""obj1"",
			            ""query"" : {
			                ""bool"" : {
			                    ""must"" : [
			                    { ""match"" : {""obj1.name"" : ""blue""} },
			                    { ""range"" : {""obj1.count"" : {""gt"" : 5}} }
			                    ]
			                }
			            },
			            ""score_mode"" : ""avg""
			        }
			    }
			}", e => e.ApplyBodyChanges(json =>
			{
				json["query"]["nested"]["query"]["bool"]["must"][0]["match"]["obj1.name"].ToLongFormQuery();
			}));
		}

		[U]
		[Description("query-dsl/nested-query.asciidoc:133")]
		public void Line133()
		{
			// tag::54092c8c646133f5dbbc047990dd458d[]
			var createIndexResponse = client.Indices.Create("drivers", c => c
				.Map<DriverDocument>(m => m
					.Properties(p => p
						.Nested<Driver>(n => n
							.Name(nn => nn.Driver)
							.Properties(props => props
								.Text(t => t
									.Name(name => name.LastName)
								)
								.Nested<Vehicle>(nested => nested
									.Name(nn => nn.Vehicle)
									.Properties(pp => pp
										.Text(t => t
											.Name(nn => nn.Make)
										)
										.Text(t => t
											.Name(nn => nn.Model)
										)
									)
								)
							)
						)
					)
				)
			);
			// end::54092c8c646133f5dbbc047990dd458d[]

			createIndexResponse.MatchesExample(@"PUT /drivers
			{
			    ""mappings"" : {
			        ""properties"" : {
			            ""driver"" : {
			                ""type"" : ""nested"",
			                ""properties"" : {
			                    ""last_name"" : {
			                        ""type"" : ""text""
			                    },
			                    ""vehicle"" : {
			                        ""type"" : ""nested"",
			                        ""properties"" : {
			                            ""make"" : {
			                                ""type"" : ""text""
			                            },
			                            ""model"" : {
			                                ""type"" : ""text""
			                            }
			                        }
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U]
		[Description("query-dsl/nested-query.asciidoc:165")]
		public void Line165()
		{
			// tag::873fbbc6ab81409058591385fd602736[]
			var indexResponse = client.Index(new DriverDocument
			{
				Driver = new Driver
				{
					LastName = "McQueen",
					Vehicle = new[]
					{
						new Vehicle
						{
							Make = "Powell Motors",
							Model = "Canyonero"
						},
						new Vehicle
						{
							Make = "Miller-Meteor",
							Model = "Ecto-1"
						}
					}
				}
			}, i => i.Id(1).Index("drivers"));

			var indexResponse2 = client.Index(new DriverDocument
			{
				Driver = new Driver
				{
					LastName = "Hudson",
					Vehicle = new[]
					{
						new Vehicle
						{
							Make = "Mifune",
							Model = "Mach Five"
						},
						new Vehicle
						{
							Make = "Miller-Meteor",
							Model = "Ecto-1"
						}
					}
				}
			}, i => i.Id(2).Index("drivers"));
			// end::873fbbc6ab81409058591385fd602736[]

			indexResponse.MatchesExample(@"PUT /drivers/_doc/1
			{
			  ""driver"" : {
			        ""last_name"" : ""McQueen"",
			        ""vehicle"" : [
			            {
			                ""make"" : ""Powell Motors"",
			                ""model"" : ""Canyonero""
			            },
			            {
			                ""make"" : ""Miller-Meteor"",
			                ""model"" : ""Ecto-1""
			            }
			        ]
			    }
			}");

			indexResponse2.MatchesExample(@"PUT /drivers/_doc/2?refresh
			{
			  ""driver"" : {
			        ""last_name"" : ""Hudson"",
			        ""vehicle"" : [
			            {
			                ""make"" : ""Mifune"",
			                ""model"" : ""Mach Five""
			            },
			            {
			                ""make"" : ""Miller-Meteor"",
			                ""model"" : ""Ecto-1""
			            }
			        ]
			    }
			}");
		}

		[U]
		[Description("query-dsl/nested-query.asciidoc:206")]
		public void Line206()
		{
			// tag::0bd3923424a20a4ba860b0774b9991b1[]
			var searchResponse = client.Search<DriverDocument>(s => s
				.Index("drivers")
				.Query(q => q
					.Nested(nq => nq
						.Path(p => p.Driver)
						.Query(qq => qq
							.Nested(nnq => nnq
								.Path(p => p.Driver.Vehicle)
								.Query(qqq => qqq
										.Match(m => m
											.Field(f => f.Driver.Vehicle.First().Make)
											.Query("Powell Motors")
										) && q
										.Match(m => m
											.Field(f => f.Driver.Vehicle.First().Model)
											.Query("Canyonero")
										)
								)
							)
						)
					)
				)
			);
			// end::0bd3923424a20a4ba860b0774b9991b1[]

			searchResponse.MatchesExample(@"GET /drivers/_search
			{
			    ""query"" : {
			        ""nested"" : {
			            ""path"" : ""driver"",
			            ""query"" : {
			                ""nested"" : {
			                    ""path"" :  ""driver.vehicle"",
			                    ""query"" :  {
			                        ""bool"" : {
			                            ""must"" : [
			                                { ""match"" : { ""driver.vehicle.make"" : ""Powell Motors"" } },
			                                { ""match"" : { ""driver.vehicle.model"" : ""Canyonero"" } }
			                            ]
			                        }
			                    }
			                }
			            }
			        }
			    }
			}", e => e.ApplyBodyChanges(json =>
			{
				json["query"]["nested"]["query"]["nested"]["query"]["bool"]["must"][0]["match"]["driver.vehicle.make"].ToLongFormQuery();
				json["query"]["nested"]["query"]["nested"]["query"]["bool"]["must"][1]["match"]["driver.vehicle.model"].ToLongFormQuery();
			}));
		}
	}
}
