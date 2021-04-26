/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.XPack.Transform
{
	[SkipVersion("<7.8.0", "Settings introduced in 7.8.0")]
	public class TransformApiWithSettingsTests
		: ApiIntegrationTestBase<WritableCluster, PreviewTransformResponse<ProjectTransform>, IPreviewTransformRequest, PreviewTransformDescriptor<Project>, PreviewTransformRequest>
	{
		public TransformApiWithSettingsTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
				(client, f) => client.Transform.Preview<Project, ProjectTransform>(f),
				(client, f) => client.Transform.PreviewAsync<Project, ProjectTransform>(f),
				(client, r) => client.Transform.Preview<ProjectTransform>(r),
				(client, r) => client.Transform.PreviewAsync<ProjectTransform>(r));

		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "_transform/_preview";
		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson
		{
			get
			{
				var settings = TestClient.Configuration.InRange("<7.11.0")
					? (dynamic)new { docs_per_second = 200.0, max_page_search_size = 200 }
					: new { docs_per_second = 200.0, max_page_search_size = 200, dates_as_epoch_millis = true };

				return new
				{
					description = CallIsolatedValue,
					frequency = "1s",
					source = new { index = new[] { "project" }, query = new { match_all = new { } } },
					dest = new { index = $"transform-{CallIsolatedValue}" },
					pivot = new
					{
						aggregations = new
						{
							averageCommits = new
							{
								avg = new
								{
									field = "numberOfCommits"
								}
							},
							sumIntoMaster = new
							{
								scripted_metric = new
								{
									combine_script = new
									{
										source = "long sum = 0; for (s in state.masterCommits) { sum += s } return sum"
									},
									init_script = new
									{
										source = "state.masterCommits = []"
									},
									map_script = new
									{
										source = "state.masterCommits.add(doc['branches.keyword'].contains('master')? 1 : 0)"
									},
									reduce_script = new
									{
										source = "long sum = 0; for (s in states) { sum += s } return sum"
									}
								}
							}
						},
						group_by = new
						{
							weekStartedOnMillis = new
							{
								date_histogram = new
								{
									calendar_interval = "week",
									field = "startedOn"
								}
							}
						}
					},
					sync = new
					{
						time = new
						{
							field = "lastActivity"
						}
					},
					settings
				};
			}
		}

		protected override PreviewTransformRequest Initializer
		{
			get
			{
				var settings = new TransformSettings { MaxPageSearchSize = 200, DocsPerSecond = 200 };

				if (TestClient.Configuration.InRange(">=7.11.0"))
					settings.DatesAsEpochMilliseconds = true;

				return new PreviewTransformRequest
				{
					Description = CallIsolatedValue,
					Frequency = "1s",
					Source = new TransformSource { Index = Index<Project>(), Query = new MatchAllQuery() },
					Destination = new TransformDestination { Index = $"transform-{CallIsolatedValue}" },
					Pivot = new TransformPivot
					{
						Aggregations =
							new AverageAggregation("averageCommits", Field<Project>(f => f.NumberOfCommits)) &&
							new ScriptedMetricAggregation("sumIntoMaster")
							{
								InitScript = new InlineScript("state.masterCommits = []"),
								MapScript = new InlineScript("state.masterCommits.add(doc['branches.keyword'].contains('master')? 1 : 0)"),
								CombineScript = new InlineScript("long sum = 0; for (s in state.masterCommits) { sum += s } return sum"),
								ReduceScript = new InlineScript("long sum = 0; for (s in states) { sum += s } return sum")
							},
						GroupBy = new Dictionary<string, ISingleGroupSource>
						{
							{
								"weekStartedOnMillis",
								new DateHistogramGroupSource { Field = Field<Project>(f => f.StartedOn), CalendarInterval = DateInterval.Week }
							}
						}
					},
					Sync = new TransformSyncContainer(new TransformTimeSync { Field = Field<Project>(f => f.LastActivity) }),
					Settings = settings
				};
			}
		}

		protected override Func<PreviewTransformDescriptor<Project>, IPreviewTransformRequest> Fluent => f => f
			.Description(CallIsolatedValue)
			.Frequency(new Time(1, TimeUnit.Second))
			.Source(s => s
				.Index<Project>()
				.Query(q => q.MatchAll())
			)
			.Destination(de => de
				.Index($"transform-{CallIsolatedValue}")
			)
			.Pivot(p => p
				.Aggregations(a => a
					.Average("averageCommits", avg => avg
						.Field(fld => fld.NumberOfCommits)
					)
					.ScriptedMetric("sumIntoMaster", sm => sm
						.InitScript("state.masterCommits = []")
						.MapScript("state.masterCommits.add(doc['branches.keyword'].contains('master')? 1 : 0)")
						.CombineScript("long sum = 0; for (s in state.masterCommits) { sum += s } return sum")
						.ReduceScript("long sum = 0; for (s in states) { sum += s } return sum")
					)
				)
				.GroupBy(g => g
					.DateHistogram("weekStartedOnMillis", dh => dh
						.Field(fld => fld.StartedOn)
						.CalendarInterval(DateInterval.Week)
					)
				)
			)
			.Sync(sy => sy
				.Time(t => t
					.Field(fld => fld.LastActivity)
				)
			).Settings(s =>
				{
					var descriptor = s
						.MaxPageSearchSize(200)
						.DocsPerSecond(200);

					if (TestClient.Configuration.InRange(">=7.11.0"))
						descriptor.DatesAsEpochMilliseconds(true);

					return descriptor;
				});
	}

	[SkipVersion("<7.11.0", "Settings introduced in 7.11.0")]
	public class TransformApiWithSettingsAndNoDatesAsMillisTests
		: ApiIntegrationTestBase<WritableCluster, PreviewTransformResponse<ProjectTransform>, IPreviewTransformRequest, PreviewTransformDescriptor<Project>, PreviewTransformRequest>
	{
		// From 7.11, by default, ISO dates are used for transforms, rather than epoch millis.
		// This test verifies the default behaviour, without configuring the setting, results in a date
		
		public TransformApiWithSettingsAndNoDatesAsMillisTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() =>
			Calls(
				(client, f) => client.Transform.Preview<Project, ProjectTransform>(f),
				(client, f) => client.Transform.PreviewAsync<Project, ProjectTransform>(f),
				(client, r) => client.Transform.Preview<ProjectTransform>(r),
				(client, r) => client.Transform.PreviewAsync<ProjectTransform>(r));

		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "_transform/_preview";
		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson =>
			new
			{
				description = CallIsolatedValue,
				frequency = "1s",
				source = new { index = new[] { "project" }, query = new { match_all = new { } } },
				dest = new { index = $"transform-{CallIsolatedValue}" },
				pivot = new
				{
					aggregations = new
					{
						averageCommits = new { avg = new { field = "numberOfCommits" } },
						sumIntoMaster = new
						{
							scripted_metric = new
							{
								combine_script =
									new { source = "long sum = 0; for (s in state.masterCommits) { sum += s } return sum" },
								init_script = new { source = "state.masterCommits = []" },
								map_script = new { source = "state.masterCommits.add(doc['branches.keyword'].contains('master')? 1 : 0)" },
								reduce_script = new { source = "long sum = 0; for (s in states) { sum += s } return sum" }
							}
						}
					},
					group_by = new { weekStartedOnDate = new { date_histogram = new { calendar_interval = "week", field = "startedOn" } } }
				},
				sync = new { time = new { field = "lastActivity" } },
				settings = new { docs_per_second = 200.0, max_page_search_size = 200 }
			};

		protected override PreviewTransformRequest Initializer => new PreviewTransformRequest
		{
			Description = CallIsolatedValue,
			Frequency = "1s",
			Source = new TransformSource { Index = Index<Project>(), Query = new MatchAllQuery() },
			Destination = new TransformDestination { Index = $"transform-{CallIsolatedValue}" },
			Pivot = new TransformPivot
			{
				Aggregations =
					new AverageAggregation("averageCommits", Field<Project>(f => f.NumberOfCommits)) &&
					new ScriptedMetricAggregation("sumIntoMaster")
					{
						InitScript = new InlineScript("state.masterCommits = []"),
						MapScript = new InlineScript("state.masterCommits.add(doc['branches.keyword'].contains('master')? 1 : 0)"),
						CombineScript = new InlineScript("long sum = 0; for (s in state.masterCommits) { sum += s } return sum"),
						ReduceScript = new InlineScript("long sum = 0; for (s in states) { sum += s } return sum")
					},
				GroupBy = new Dictionary<string, ISingleGroupSource>
				{
					{
						"weekStartedOnDate",
						new DateHistogramGroupSource { Field = Field<Project>(f => f.StartedOn), CalendarInterval = DateInterval.Week }
					}
				}
			},
			Sync = new TransformSyncContainer(new TransformTimeSync { Field = Field<Project>(f => f.LastActivity) }),
			Settings = new TransformSettings { MaxPageSearchSize = 200, DocsPerSecond = 200 }
		};

		protected override Func<PreviewTransformDescriptor<Project>, IPreviewTransformRequest> Fluent => f => f
			.Description(CallIsolatedValue)
			.Frequency(new Time(1, TimeUnit.Second))
			.Source(s => s
				.Index<Project>()
				.Query(q => q.MatchAll())
			)
			.Destination(de => de
				.Index($"transform-{CallIsolatedValue}")
			)
			.Pivot(p => p
				.Aggregations(a => a
					.Average("averageCommits", avg => avg
						.Field(fld => fld.NumberOfCommits)
					)
					.ScriptedMetric("sumIntoMaster", sm => sm
						.InitScript("state.masterCommits = []")
						.MapScript("state.masterCommits.add(doc['branches.keyword'].contains('master')? 1 : 0)")
						.CombineScript("long sum = 0; for (s in state.masterCommits) { sum += s } return sum")
						.ReduceScript("long sum = 0; for (s in states) { sum += s } return sum")
					)
				)
				.GroupBy(g => g
					.DateHistogram("weekStartedOnDate", dh => dh
						.Field(fld => fld.StartedOn)
						.CalendarInterval(DateInterval.Week)
					)
				)
			)
			.Sync(sy => sy
				.Time(t => t
					.Field(fld => fld.LastActivity)
				)
			).Settings(s => s
				.MaxPageSearchSize(200)
				.DocsPerSecond(200));
	}

	[SkipVersion("<7.12.0", "Settings introduced in 7.12.0")]
	public class TransformApiWithRuntimeFieldsTests
		: ApiIntegrationTestBase<WritableCluster, PreviewTransformResponse<ProjectTransform>, IPreviewTransformRequest, PreviewTransformDescriptor<Project>, PreviewTransformRequest>
	{
		private const string RuntimeFieldName = "search_runtime_field";
		private const string RuntimeFieldScript = "if (doc['type'].size() != 0) {emit(doc['type'].value.toUpperCase())}";

		public TransformApiWithRuntimeFieldsTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
				(client, f) => client.Transform.Preview<Project, ProjectTransform>(f),
				(client, f) => client.Transform.PreviewAsync<Project, ProjectTransform>(f),
				(client, r) => client.Transform.Preview<ProjectTransform>(r),
				(client, r) => client.Transform.PreviewAsync<ProjectTransform>(r));

		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "_transform/_preview";
		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson =>
			new
			{
				description = CallIsolatedValue,
				frequency = "1s",
				source = new {
					index = new[] { "project" },
					query = new { match_all = new { } },
					runtime_mappings = new
					{
						search_runtime_field = new
						{
							script = new
							{
								source = RuntimeFieldScript
							},
							type = "keyword"
						}
					}
				},
				dest = new { index = $"transform-{CallIsolatedValue}" },
				pivot = new
				{
					aggregations = new
					{
						averageCommits = new
						{
							avg = new
							{
								field = "numberOfCommits"
							}
						},
						sumIntoMaster = new
						{
							scripted_metric = new
							{
								combine_script = new
								{
									source = "long sum = 0; for (s in state.masterCommits) { sum += s } return sum"
								},
								init_script = new
								{
									source = "state.masterCommits = []"
								},
								map_script = new
								{
									source = "state.masterCommits.add(doc['branches.keyword'].contains('master')? 1 : 0)"
								},
								reduce_script = new
								{
									source = "long sum = 0; for (s in states) { sum += s } return sum"
								}
							}
						}
					},
					group_by = new { type = new { terms = new { field = "search_runtime_field" } } }
				},
				sync = new
				{
					time = new
					{
						field = "lastActivity"
					}
				}
			};

		protected override PreviewTransformRequest Initializer => new()
			{
				Description = CallIsolatedValue,
				Frequency = "1s",
				Source = new TransformSource { Index = Index<Project>(), Query = new MatchAllQuery(),
					RuntimeFields = new RuntimeFields
					{
						{ RuntimeFieldName, new RuntimeField
							{
								Type = FieldType.Keyword,
								Script = new InlineScript(RuntimeFieldScript)
							}
						}
					}
				},
				Destination = new TransformDestination { Index = $"transform-{CallIsolatedValue}" },
				Pivot = new TransformPivot
				{
					Aggregations =
						new AverageAggregation("averageCommits", Field<Project>(f => f.NumberOfCommits)) &&
						new ScriptedMetricAggregation("sumIntoMaster")
						{
							InitScript = new InlineScript("state.masterCommits = []"),
							MapScript = new InlineScript("state.masterCommits.add(doc['branches.keyword'].contains('master')? 1 : 0)"),
							CombineScript = new InlineScript("long sum = 0; for (s in state.masterCommits) { sum += s } return sum"),
							ReduceScript = new InlineScript("long sum = 0; for (s in states) { sum += s } return sum")
						},
					GroupBy = new Dictionary<string, ISingleGroupSource>
					{
						{
							"type",
							new TermsGroupSource { Field = "search_runtime_field"}
						}
					}
					
				},
				Sync = new TransformSyncContainer(new TransformTimeSync { Field = Field<Project>(f => f.LastActivity) })
			};

		protected override Func<PreviewTransformDescriptor<Project>, IPreviewTransformRequest> Fluent => f => f
			.Description(CallIsolatedValue)
			.Frequency(new Time(1, TimeUnit.Second))
			.Source(s => s
				.Index<Project>()
				.Query(q => q.MatchAll())
				.RuntimeFields(rtf => rtf.RuntimeField(RuntimeFieldName, FieldType.Keyword, r => r.Script(RuntimeFieldScript)))
			)
			.Destination(de => de
				.Index($"transform-{CallIsolatedValue}")
			)
			.Pivot(p => p
				.Aggregations(a => a
					.Average("averageCommits", avg => avg
						.Field(fld => fld.NumberOfCommits)
					)
					.ScriptedMetric("sumIntoMaster", sm => sm
						.InitScript("state.masterCommits = []")
						.MapScript("state.masterCommits.add(doc['branches.keyword'].contains('master')? 1 : 0)")
						.CombineScript("long sum = 0; for (s in state.masterCommits) { sum += s } return sum")
						.ReduceScript("long sum = 0; for (s in states) { sum += s } return sum")
					)
				)
				.GroupBy(g => g
					.Terms("type", t => t.Field("search_runtime_field"))
				)
			)
			.Sync(sy => sy
				.Time(t => t
					.Field(fld => fld.LastActivity)
				)
			);
	}
}
