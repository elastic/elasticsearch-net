using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
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
	[SkipVersion("<7.9.0", "Geotile grid group by introduced in 7.9.0")]
	public class TransformApiTests : CoordinatedIntegrationTestBase<WritableCluster>
	{
		private const string PutTransformStep = nameof(PutTransformStep);
		private const string GetTransformStep = nameof(GetTransformStep);
		private const string StartTransformStep = nameof(StartTransformStep);
		private const string PreviewTransformStep = nameof(PreviewTransformStep);
		private const string UpdateTransformStep = nameof(UpdateTransformStep);
		private const string GetTransformStatsStep = nameof(GetTransformStatsStep);
		private const string StopTransformStep = nameof(StopTransformStep);
		private const string DeleteTransformStep = nameof(DeleteTransformStep);

		public TransformApiTests(WritableCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage)
		{
			{
				PutTransformStep, u =>
					u.Calls<PutTransformDescriptor<Project>, PutTransformRequest, IPutTransformRequest, PutTransformResponse>(
						v => new PutTransformRequest(v)
						{
							Description = v,
							Frequency = "1s",
							Source = new TransformSource
							{
								Index = Index<Project>(),
								Query = new MatchAllQuery()
							},
							Destination = new TransformDestination
							{
								Index = $"transform-{v}"
							},
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
									{ "weekStartedOn", new DateHistogramGroupSource()
										{
											Field = Field<Project>(f => f.StartedOn),
											CalendarInterval = DateInterval.Week
										}
									},
									{
										"geotile", new GeoTileGridGroupSource
										{
											Field = Field<Project>(f => f.LocationPoint),
											Precision = GeoTilePrecision.Precision6,
											Bounds = new BoundingBox
											{
												TopLeft = new GeoLocation(-90, 180),
												BottomRight = new GeoLocation(90, -180)
											}
										}
									}
								}
							}
						},
						(v, d) => d
							.Description(v)
							.Frequency(new Time(1, TimeUnit.Second))
							.Source(s => s
								.Index<Project>()
								.Query(q => q.MatchAll())
							)
							.Destination(de => de
								.Index($"transform-{v}")
							)
							.Pivot(p => p
								.Aggregations(a => a
									.Average("averageCommits", avg => avg
										.Field(f => f.NumberOfCommits)
									)
									.ScriptedMetric("sumIntoMaster", sm => sm
										.InitScript("state.masterCommits = []")
										.MapScript("state.masterCommits.add(doc['branches.keyword'].contains('master')? 1 : 0)")
										.CombineScript("long sum = 0; for (s in state.masterCommits) { sum += s } return sum")
										.ReduceScript("long sum = 0; for (s in states) { sum += s } return sum")
									)
								)
								.GroupBy(g => g
									.DateHistogram("weekStartedOn", dh => dh
										.Field(f => f.StartedOn)
										.CalendarInterval(DateInterval.Week)
									)
									.GeoTileGrid("geotile", gtg => gtg
										.Field(f => f.LocationPoint)
										.Precision(GeoTilePrecision.Precision6)
										.Bounds(b => b
											.TopLeft(-90, 180)
											.BottomRight(90, -180)
										)
									)
								)
							),
						(v, c, f) => c.Transform.Put(v, f),
						(v, c, f) => c.Transform.PutAsync(v, f),
						(v, c, r) => c.Transform.Put(r),
						(v, c, r) => c.Transform.PutAsync(r)
					)
			},
			{
				GetTransformStep, u =>
					u.Calls<GetTransformDescriptor, GetTransformRequest, IGetTransformRequest, GetTransformResponse>(
						v => new GetTransformRequest(v),
						(v, d) => d.TransformId(v),
						(v, c, f) => c.Transform.Get(f),
						(v, c, f) => c.Transform.GetAsync(f),
						(v, c, r) => c.Transform.Get(r),
						(v, c, r) => c.Transform.GetAsync(r)
					)
			},
			{
				StartTransformStep, u =>
					u.Calls<StartTransformDescriptor, StartTransformRequest, IStartTransformRequest, StartTransformResponse>(
						v => new StartTransformRequest(v) { Timeout = "2m" },
						(v, d) => d.Timeout("2m"),
						(v, c, f) => c.Transform.Start(v, f),
						(v, c, f) => c.Transform.StartAsync(v, f),
						(v, c, r) => c.Transform.Start(r),
						(v, c, r) => c.Transform.StartAsync(r)
					)
			},
			{
				PreviewTransformStep, u =>
					u.Calls<PreviewTransformDescriptor<Project>, PreviewTransformRequest, IPreviewTransformRequest, PreviewTransformResponse<ProjectTransform>>(
						v => new PreviewTransformRequest
						{
							Description = v,
							Frequency = "1s",
							Source = new TransformSource
							{
								Index = Index<Project>(),
								Query = new MatchAllQuery()
							},
							Destination = new TransformDestination
							{
								Index = $"transform-{v}"
							},
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
									{ TestClient.Configuration.InRange("<7.11.0") ? "weekStartedOnMillis" : "weekStartedOnDate", new DateHistogramGroupSource
										{
											Field = Field<Project>(f => f.StartedOn),
											CalendarInterval = DateInterval.Week
										}
									}
								}
							},
							Sync = new TransformSyncContainer(new TransformTimeSync
							{
								Field = Field<Project>(f => f.LastActivity)
							})
						},
						(v, d) => d
							.Description(v)
							.Frequency(new Time(1, TimeUnit.Second))
							.Source(s => s
								.Index<Project>()
								.Query(q => q.MatchAll())
							)
							.Destination(de => de
								.Index($"transform-{v}")
							)
							.Pivot(p => p
								.Aggregations(a => a
									.Average("averageCommits", avg => avg
										.Field(f => f.NumberOfCommits)
									)
									.ScriptedMetric("sumIntoMaster", sm => sm
										.InitScript("state.masterCommits = []")
										.MapScript("state.masterCommits.add(doc['branches.keyword'].contains('master')? 1 : 0)")
										.CombineScript("long sum = 0; for (s in state.masterCommits) { sum += s } return sum")
										.ReduceScript("long sum = 0; for (s in states) { sum += s } return sum")
									)
								)
								.GroupBy(g => g
									.DateHistogram(TestClient.Configuration.InRange("<7.11.0") ? "weekStartedOnMillis" : "weekStartedOnDate", dh => dh
										.Field(f => f.StartedOn)
										.CalendarInterval(DateInterval.Week)
									)
								)
							)
							.Sync(sy => sy
								.Time(t => t
									.Field(f => f.LastActivity)
								)
							),
						(v, c, f) => c.Transform.Preview<Project, ProjectTransform>(f),
						(v, c, f) => c.Transform.PreviewAsync<Project, ProjectTransform>(f),
						(v, c, r) => c.Transform.Preview<ProjectTransform>(r),
						(v, c, r) => c.Transform.PreviewAsync<ProjectTransform>(r)
					)
			},
			{
				UpdateTransformStep, u =>
					u.Calls<UpdateTransformDescriptor<Project>, UpdateTransformRequest, IUpdateTransformRequest, UpdateTransformResponse>(
						v => new UpdateTransformRequest(v)
						{
							Frequency = "2s"
						},
						(v, d) => d
							.Frequency("2s"),
						(v, c, f) => c.Transform.Update(v, f),
						(v, c, f) => c.Transform.UpdateAsync(v, f),
						(v, c, r) => c.Transform.Update(r),
						(v, c, r) => c.Transform.UpdateAsync(r)
					)
			},
			{
				GetTransformStatsStep, u =>
					u.Calls<GetTransformStatsDescriptor, GetTransformStatsRequest, IGetTransformStatsRequest, GetTransformStatsResponse>(
						v => new GetTransformStatsRequest(v),
						(v, d) => d,
						(v, c, f) => c.Transform.GetStats(v, f),
						(v, c, f) => c.Transform.GetStatsAsync(v, f),
						(v, c, r) => c.Transform.GetStats(r),
						(v, c, r) => c.Transform.GetStatsAsync(r)
					)
			},
			{
				StopTransformStep, u =>
					u.Calls<StopTransformDescriptor, StopTransformRequest, IStopTransformRequest, StopTransformResponse>(
						v => new StopTransformRequest(v) { Force = true, WaitForCompletion = true, Timeout = "2m"},
						(v, d) => d.Force().WaitForCompletion().Timeout("2m"),
						(v, c, f) => c.Transform.Stop(v, f),
						(v, c, f) => c.Transform.StopAsync(v, f),
						(v, c, r) => c.Transform.Stop(r),
						(v, c, r) => c.Transform.StopAsync(r)
					)
			},
			{
				DeleteTransformStep, u =>
					u.Calls<DeleteTransformDescriptor, DeleteTransformRequest, IDeleteTransformRequest, DeleteTransformResponse>(
						v => new DeleteTransformRequest(v) { Force = true },
						(v, d) => d.Force(),
						(v, c, f) => c.Transform.Delete(v, f),
						(v, c, f) => c.Transform.DeleteAsync(v, f),
						(v, c, r) => c.Transform.Delete(r),
						(v, c, r) => c.Transform.DeleteAsync(r)
					)
			},
		}) { }

		[I] public async Task PutTransformResponse() => await Assert<PutTransformResponse>(PutTransformStep, (v, r) =>
		{
			r.ShouldBeValid();
			r.Acknowledged.Should().BeTrue();
		});

		[I] public async Task GetTransformResponse() => await Assert<GetTransformResponse>(GetTransformStep, (v, r) =>
		{
			r.ShouldBeValid();
			r.Count.Should().Be(1);
			var transform = r.Transforms.First();

			transform.Id.Should().Be(v);
			transform.Description.Should().Be(v);
			transform.Frequency.Should().Be(new Time(1, TimeUnit.Second));
			transform.Destination.Should().NotBeNull();
			transform.Destination.Index.Should().Be($"transform-{v}");

			Nest.Indices indices = "project";
			transform.Source.Index.Should().Be(indices);
			((IQueryContainer)transform.Source.Query).MatchAll.Should().NotBeNull();

			transform.Pivot.Should().NotBeNull();
			transform.Pivot.GroupBy.Should().ContainKey("weekStartedOn");
			transform.Pivot.Aggregations["averageCommits"].Should().NotBeNull();
			transform.Pivot.Aggregations["sumIntoMaster"].Should().NotBeNull();
		});

		[I] public async Task StartTransformResponse() => await Assert<StartTransformResponse>(StartTransformStep, (v, r) =>
		{
			r.ShouldBeValid();
			r.Acknowledged.Should().BeTrue();
		});

		[I] public async Task PreviewTransformResponse() => await Assert<PreviewTransformResponse<ProjectTransform>>(PreviewTransformStep, (v, r) =>
		{
			r.ShouldBeValid();

			r.Preview.Should().NotBeNull().And.HaveCountGreaterThan(0);
			r.GeneratedDestinationIndex.Should().NotBeNull();
			r.GeneratedDestinationIndex.Mappings.Should().NotBeNull();
			r.GeneratedDestinationIndex.Settings.Should().NotBeNull();
			r.GeneratedDestinationIndex.Aliases.Should().NotBeNull();

			if (TestClient.Configuration.InRange("<7.11.0"))
				r.Preview.First().WeekStartedOnMillis.Should().BeGreaterOrEqualTo(1);
			else
				r.Preview.First().WeekStartedOnDate.Should().NotBe(DateTime.MinValue);
		});

		[I] public async Task UpdateTransformResponse() => await Assert<UpdateTransformResponse>(UpdateTransformStep, (v, r) =>
		{
			r.ShouldBeValid();
			r.Frequency.Should().Be("2s");
		});

		[I] public async Task GetTransformStatsResponse() => await Assert<GetTransformStatsResponse>(GetTransformStatsStep, (v, r) =>
		{
			r.ShouldBeValid();
			r.Count.Should().Be(1);
			var stats = r.Transforms.First();

			stats.Id.Should().Be(v);
			stats.State.Should().NotBeNullOrEmpty();
			stats.Stats.Should().NotBeNull();
			stats.Checkpointing.Should().NotBeNull();
		});

		[I] public async Task StopTransformResponse() => await Assert<StopTransformResponse>(StopTransformStep, (v, r) =>
		{
			r.ShouldBeValid();
			r.Acknowledged.Should().BeTrue();
		});

		[I] public async Task DeleteTransformResponse() => await Assert<DeleteTransformResponse>(DeleteTransformStep, (v, r) =>
		{
			r.ShouldBeValid();
			r.Acknowledged.Should().BeTrue();
		});
	}
}
