using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.XPack.Rollup
{
	public class RollupJobCrudTests
		: CrudTestBase<TimeSeriesCluster, ICreateRollupJobResponse, IGetRollupJobResponse, ICreateRollupJobResponse, IDeleteRollupJobResponse>
	{
		private static readonly string CronPeriod = "*/2 * * * * ?";

		public RollupJobCrudTests(TimeSeriesCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool SupportsUpdates => false;
		protected override bool TestOnlyOneMethod => true;

		private static string CreateRollupName(string s) => $"rollup-logs-{s}";

		protected override LazyResponses Create() =>
			Calls<CreateRollupJobDescriptor<Log>, CreateRollupJobRequest, ICreateRollupJobRequest, ICreateRollupJobResponse>(
				CreateInitializer,
				CreateFluent,
				(s, c, f) => c.CreateRollupJob(CreateRollupName(s), f),
				(s, c, f) => c.CreateRollupJobAsync(CreateRollupName(s), f),
				(s, c, r) => c.CreateRollupJob(r),
				(s, c, r) => c.CreateRollupJobAsync(r)
			);

		protected CreateRollupJobRequest CreateInitializer(string role) => new CreateRollupJobRequest(CreateRollupName(role))
		{
			PageSize = 1000,
			IndexPattern = TimeSeriesSeeder.IndicesWildCard,
			RollupIndex = CreateRollupName(role),
			Cron = CronPeriod,
			Groups = new RollupGroupings
			{
				DateHistogram = new DateHistogramRollupGrouping
				{
					Field = Infer.Field<Log>(p => p.Timestamp),
					Interval = TimeSpan.FromHours(1)
				},
				Terms = new TermsRollupGrouping
				{
					Fields = Infer.Field<Log>(p => p.Section).And<Log>(p => p.Tag)
				},
				Histogram = new HistogramRollupGrouping
				{
					Fields = Infer.Field<Log>(p => p.Load).And<Log>(p => p.NetIn).And<Log>(p => p.NetOut),
					Interval = 5
				}
			},
			Metrics = new List<IRollupFieldMetric>
			{
				new RollupFieldMetric
				{
					Field = Infer.Field<Log>(p => p.Temperature),
					Metrics = new[] { RollupMetric.Average, RollupMetric.Min, RollupMetric.Max }
				},
			}
		};

		protected ICreateRollupJobRequest CreateFluent(string role, CreateRollupJobDescriptor<Log> d) => d
			.PageSize(1000)
			.IndexPattern(TimeSeriesSeeder.IndicesWildCard)
			.RollupIndex(CreateRollupName(role))
			.Cron(CronPeriod)
			.Groups(g => g
				.DateHistogram(dh => dh.Field(p => p.Timestamp).Interval(TimeSpan.FromHours(1)))
				.Terms(t => t.Fields(f => f.Field(p => p.Section).Field(p => p.Tag)))
				.Histogram(h => h.Fields(f => f.Field(p => p.Load).Field(p => p.NetIn).Field(p => p.NetOut)).Interval(5))
			)
			.Metrics(m => m
				.Field(p => p.Temperature, RollupMetric.Average, RollupMetric.Min, RollupMetric.Max)
			);

		protected override LazyResponses Read() => Calls<GetRollupJobDescriptor, GetRollupJobRequest, IGetRollupJobRequest, IGetRollupJobResponse>(
			ReadInitializer,
			ReadFluent,
			(s, c, f) => c.GetRollupJob(f),
			(s, c, f) => c.GetRollupJobAsync(f),
			(s, c, r) => c.GetRollupJob(r),
			(s, c, r) => c.GetRollupJobAsync(r)
		);

		protected GetRollupJobRequest ReadInitializer(string role) => new GetRollupJobRequest(CreateRollupName(role));

		protected IGetRollupJobRequest ReadFluent(string role, GetRollupJobDescriptor d) => d.Id(CreateRollupName(role));

		protected override IDictionary<string, Func<LazyResponses>> AfterCreateCalls() => new Dictionary<string, Func<LazyResponses>>
		{
			{
				"start", () => Calls<StartRollupJobDescriptor, StartRollupJobRequest, IStartRollupJobRequest, IStartRollupJobResponse>(
					StartInitializer,
					StartFluent,
					(s, c, f) => c.StartRollupJob(CreateRollupName(s), f),
					(s, c, f) => c.StartRollupJobAsync(CreateRollupName(s), f),
					(s, c, r) => c.StartRollupJob(r),
					(s, c, r) => c.StartRollupJobAsync(r)
				)
			},
			{ "wait_for_finish", () => Call(WaitForFinish) },
			{
				"rollup_search", () => Calls<RollupSearchDescriptor<Log>, RollupSearchRequest, IRollupSearchRequest, IRollupSearchResponse<Log>>(
					RollupSearchInitializer,
					RollupSearchFluent,
					(s, c, f) => c.RollupSearch(CreateRollupSearchIndices(s), f),
					(s, c, f) => c.RollupSearchAsync(CreateRollupSearchIndices(s), f),
					(s, c, r) => c.RollupSearch<Log>(r),
					(s, c, r) => c.RollupSearchAsync<Log>(r)
				)
			},
			{
				"rollup_caps", () =>
					Calls<GetRollupCapabilitiesDescriptor, GetRollupCapabilitiesRequest, IGetRollupCapabilitiesRequest, IGetRollupCapabilitiesResponse
					>(
						CapsInitializer,
						CapsFluent,
						(s, c, f) => c.GetRollupCapabilities(f),
						(s, c, f) => c.GetRollupCapabilitiesAsync(f),
						(s, c, r) => c.GetRollupCapabilities(r),
						(s, c, r) => c.GetRollupCapabilitiesAsync(r)
					)
			},
			{
				"rollup_index_caps", () =>
					Calls<GetRollupIndexCapabilitiesDescriptor, GetRollupIndexCapabilitiesRequest, IGetRollupIndexCapabilitiesRequest, IGetRollupIndexCapabilitiesResponse
					>(
						IndexCapsInitializer,
						IndexCapsFluent,
						(s, c, f) => c.GetRollupIndexCapabilities(CreateRollupName(s), f),
						(s, c, f) => c.GetRollupIndexCapabilitiesAsync(CreateRollupName(s), f),
						(s, c, r) => c.GetRollupIndexCapabilities(r),
						(s, c, r) => c.GetRollupIndexCapabilitiesAsync(r)
					)
			},
			{
				"stop", () => Calls<StopRollupJobDescriptor, StopRollupJobRequest, IStopRollupJobRequest, IStopRollupJobResponse>(
					StopInitializer,
					StopFluent,
					(s, c, f) => c.StopRollupJob(CreateRollupName(s), f),
					(s, c, f) => c.StopRollupJobAsync(CreateRollupName(s), f),
					(s, c, r) => c.StopRollupJob(r),
					(s, c, r) => c.StopRollupJobAsync(r)
				)
			},
		};

		protected StartRollupJobRequest StartInitializer(string role) => new StartRollupJobRequest(CreateRollupName(role));

		protected IStartRollupJobRequest StartFluent(string role, StartRollupJobDescriptor d) => d;

		protected StopRollupJobRequest StopInitializer(string role) => new StopRollupJobRequest(CreateRollupName(role));

		protected IStopRollupJobRequest StopFluent(string role, StopRollupJobDescriptor d) => d;

		protected GetRollupIndexCapabilitiesRequest IndexCapsInitializer(string role) => new GetRollupIndexCapabilitiesRequest(CreateRollupName(role));

		protected IGetRollupIndexCapabilitiesRequest IndexCapsFluent(string role, GetRollupIndexCapabilitiesDescriptor d) => d;

		protected GetRollupCapabilitiesRequest CapsInitializer(string role) => new GetRollupCapabilitiesRequest(TimeSeriesSeeder.IndicesWildCard);

		protected IGetRollupCapabilitiesRequest CapsFluent(string role, GetRollupCapabilitiesDescriptor d) =>
			d.Index(TimeSeriesSeeder.IndicesWildCard);

		[I] public async Task StartsJob() =>
			await AssertOnAfterCreateResponse<IStartRollupJobResponse>("start", r => r.Started.Should().BeTrue());

		[I] public async Task StopsJob() =>
			await AssertOnAfterCreateResponse<IStopRollupJobResponse>("stop", r => r.Stopped.Should().BeTrue());

		private async Task<IGetRollupJobResponse> WaitForFinish(List<string> allJobs, IElasticClient client)
		{
			var tasks = new List<Task<IGetRollupJobResponse>>(4);
			foreach (var job in allJobs)
				tasks.Add(WaitForFinish(CreateRollupName(job), client));
			await Task.WhenAll(tasks);
			return tasks[0].Result;
		}

		private static async Task<IGetRollupJobResponse> WaitForFinish(string job, IElasticClient client)
		{
			IGetRollupJobResponse response;
			var stillRunning = true;
			long processed = 0;
			do
			{
				//we can do this because we know new data is no longer indexed into these indexes
				response = await client.GetRollupJobAsync(j => j.Id(job));
				var validResponseWithJobs = response.IsValid && response.Jobs.Count > 0;
				if (!validResponseWithJobs) break;

				var processedNow = response.Jobs.First().Stats.DocumentsProcessed;
				if (processed > 0 && processedNow == processed) break;

				stillRunning = response.Jobs.All(j => j.Status.JobState != IndexingJobState.Stopped);
				processed = processedNow;
				await Task.Delay(TimeSpan.FromSeconds(2));
			} while (stillRunning);

			return response;
		}

		//make sure we query a rolled up index and a live index
		protected static Nest.Indices CreateRollupSearchIndices(string rollupIndex) =>
			$"{CreateRollupName(rollupIndex)},logs-{DateTime.UtcNow:yyyy-MM-dd}";

		protected RollupSearchRequest RollupSearchInitializer(string index) => new RollupSearchRequest(CreateRollupSearchIndices(index))
		{
			Size = 0,
			Query = new MatchAllQuery() { },
			Aggregations = new MaxAggregation("max_temp", Infer.Field<Log>(p => p.Temperature))
				&& new AverageAggregation("avg_temp", Infer.Field<Log>(p => p.Temperature))
		};

		protected IRollupSearchRequest RollupSearchFluent(string index, RollupSearchDescriptor<Log> d) => d
			.Size(0)
			.Query(q => q.MatchAll())
			.Aggregations(aggs =>
				aggs.Max("max_temp", m => m.Field(p => p.Temperature))
				&& aggs.Min("avg_temp", m => m.Field(p => p.Temperature))
			);

		[I] public async Task RollupSearchReturnsAggregations() =>
			await AssertOnAfterCreateResponse<IRollupSearchResponse<Log>>("rollup_search", r =>
			{
				r.ShouldBeValid();
				var avg = r.Aggregations.Average("avg_temp");
				avg.Should().NotBeNull();
				avg.Value.Should().HaveValue().And.BeInRange(-10, 45);
				var max = r.Aggregations.Max("max_temp");
				max.Should().NotBeNull();
				max.Value.Should().HaveValue().And.BeInRange(-10, 45);
			});

		[I] public async Task GetRollupCapabilities() =>
			await AssertOnAfterCreateResponse<IGetRollupCapabilitiesResponse>("rollup_caps", r =>
			{
				r.IsValid.Should().BeTrue();
				r.Indices.Should().NotBeEmpty().And.ContainKey(TimeSeriesSeeder.IndicesWildCard);

				var indexCaps = r.Indices[TimeSeriesSeeder.IndicesWildCard];
				indexCaps.RollupJobs.Should().NotBeEmpty();
				var job = indexCaps.RollupJobs.First();
				job.JobId.Should().NotBeNullOrWhiteSpace();
				job.RollupIndex.Should().NotBeNullOrWhiteSpace();
				job.IndexPattern.Should().Be(TimeSeriesSeeder.IndicesWildCard);
				job.Fields.Should().NotBeEmpty();
				var capabilities = job.Fields.Field<Log>(p => p.Temperature);
				capabilities.Should().NotBeEmpty();
				foreach (var c in capabilities)
					c.Should().ContainKey("agg");
			});

		[I] public async Task GetRollupIndexCapabilities() =>
			await AssertOnAfterCreateResponse<IGetRollupIndexCapabilitiesResponse>("rollup_index_caps", r =>
			{
				r.IsValid.Should().BeTrue();
				r.Indices.Should().NotBeEmpty().And.HaveCount(1);

				var indexCaps = r.Indices.First().Value;
				indexCaps.Should().NotBeNull();
				indexCaps.RollupJobs.Should().NotBeEmpty();
				var job = indexCaps.RollupJobs.First();
				job.JobId.Should().NotBeNullOrWhiteSpace();
				job.RollupIndex.Should().NotBeNullOrWhiteSpace();
				job.IndexPattern.Should().Be(TimeSeriesSeeder.IndicesWildCard);
				job.Fields.Should().NotBeEmpty();
				var capabilities = job.Fields.Field<Log>(p => p.Temperature);
				capabilities.Should().NotBeEmpty();
				foreach (var c in capabilities)
					c.Should().ContainKey("agg");
			});



		// ignored because we mark SupportsUpdates => false
		protected override LazyResponses Update() => null;

		protected override LazyResponses Delete() =>
			Calls<DeleteRollupJobDescriptor, DeleteRollupJobRequest, IDeleteRollupJobRequest, IDeleteRollupJobResponse>(
				DeleteInitializer,
				DeleteFluent,
				(s, c, f) => c.DeleteRollupJob(CreateRollupName(s), f),
				(s, c, f) => c.DeleteRollupJobAsync(CreateRollupName(s), f),
				(s, c, r) => c.DeleteRollupJob(r),
				(s, c, r) => c.DeleteRollupJobAsync(r)
			);

		protected DeleteRollupJobRequest DeleteInitializer(string role) => new DeleteRollupJobRequest(CreateRollupName(role));

		protected IDeleteRollupJobRequest DeleteFluent(string role, DeleteRollupJobDescriptor d) => d;

		protected override void ExpectAfterCreate(IGetRollupJobResponse response)
		{
			response.Jobs.Should().NotBeNull().And.NotBeEmpty();
			foreach (var j in response.Jobs)
			{
				j.Config.Should().NotBeNull("job should have config");
				j.Config.Cron.Should().NotBeNullOrWhiteSpace();
				j.Config.Id.Should().NotBeNullOrWhiteSpace();
				j.Config.Timeout.Should().NotBeNull().And.Be("20s");
				j.Config.IndexPattern.Should().NotBeNull().And.Be(TimeSeriesSeeder.IndicesWildCard);
				j.Config.RollupIndex.Should().NotBeNull();
				j.Config.PageSize.Should().Be(1000);
				j.Config.Groups.Should().NotBeNull();
				j.Config.Groups.Terms.Should().NotBeNull();
				j.Config.Groups.Terms.Fields.Should().NotBeNull();
				j.Config.Groups.DateHistogram.Should().NotBeNull();
				j.Config.Groups.DateHistogram.Field.Should().NotBeNull().And.Be("timestamp");
				j.Config.Groups.DateHistogram.Interval.Should().NotBeNull().And.Be("1h");
				j.Config.Groups.DateHistogram.Interval.Should().NotBeNull().And.Be(TimeSpan.FromHours(1));
				j.Config.Groups.Histogram.Should().NotBeNull();
				j.Config.Groups.Histogram.Fields.Should().NotBeNull();
				j.Config.Groups.Histogram.Interval.Should().NotBeNull().And.Be(5);

				j.Config.Metrics.Should().NotBeEmpty("config should have metrics");
				foreach (var m in j.Config.Metrics)
				{
					m.Field.Should().NotBeNull("metric field");
					m.Metrics.Should().NotBeEmpty("metric metrics");
				}

				j.Stats.Should().NotBeNull("job should have stats");
				j.Stats.DocumentsProcessed.Should().Be(0);
				j.Stats.PagesProcessed.Should().Be(0);
				j.Stats.RollupsIndexed.Should().Be(0);
				j.Stats.TriggerCount.Should().Be(0);
				j.Status.Should().NotBeNull("job should have status");
				j.Status.JobState.Should().Be(IndexingJobState.Stopped);
				j.Status.UpgradedDocId.Should().BeTrue("indexing status upgrade doc id");
			}
		}

		[I] [SkipVersion("<6.4.1", "https://github.com/elastic/elasticsearch/issues/34292")]
		protected override async Task GetAfterDeleteIsValid() => await AssertOnGetAfterDelete(r => r.ShouldBeValid());
	}
}
