using System;
using System.Collections.Generic;
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
		public RollupJobCrudTests(TimeSeriesCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static string CreateRollupJobName(string s) => $"rollup-{s}";
		private static readonly string CronPeriod = "*/30 * * * * ?";

		protected override bool SupportsUpdates => false;

		protected override LazyResponses Create() => this.Calls<CreateRollupJobDescriptor<Log>, CreateRollupJobRequest, ICreateRollupJobRequest, ICreateRollupJobResponse>(
			this.CreateInitializer,
			this.CreateFluent,
			fluent: (s, c, f) => c.CreateRollupJob(CreateRollupJobName(s), f),
			fluentAsync: (s, c, f) => c.CreateRollupJobAsync(CreateRollupJobName(s), f),
			request: (s, c, r) => c.CreateRollupJob(r),
			requestAsync: (s, c, r) => c.CreateRollupJobAsync(r)
		);

		protected CreateRollupJobRequest CreateInitializer(string role) => new CreateRollupJobRequest(CreateRollupJobName(role))
		{
			PageSize = 1000,
			IndexPattern = TimeSeriesSeeder.IndicesWildCard,
			RollupIndex = CreateRollupJobName(role),
			Cron = CronPeriod,
			Groups = new RollupGroupings
			{
				DateHistogram = new DateHistogramRollupGrouping
				{
					Field = Infer.Field<Log>(p=>p.Timestamp),
					Interval = TimeSpan.FromHours(1)
				},
				Terms = new TermsRollupGrouping
				{
					Fields = Infer.Field<Log>(p=>p.Section).And<Log>(p=>p.Tag)
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
					Field = Infer.Field<Log>(p=>p.Temperature),
					Metrics = new [] {RollupMetric.Average,RollupMetric.Min,RollupMetric.Max}
				},
			}
		};
		protected ICreateRollupJobRequest CreateFluent(string role, CreateRollupJobDescriptor<Log> d) => d
			.PageSize(1000)
			.IndexPattern(TimeSeriesSeeder.IndicesWildCard)
			.RollupIndex(CreateRollupJobName(role))
			.Cron(CronPeriod)
			.Groups(g => g
				.DateHistogram(dh => dh.Field(p => p.Timestamp).Interval(TimeSpan.FromHours(1)))
				.Terms(t => t.Fields(f => f.Field(p => p.Section).Field(p => p.Tag)))
				.Histogram(h => h.Fields(f => f.Field(p => p.Load).Field(p => p.NetIn).Field(p => p.NetOut)).Interval(5))
			)
			.Metrics(m=>m
				.Field(p => p.Temperature, RollupMetric.Average, RollupMetric.Min, RollupMetric.Max)
			);

		protected override LazyResponses Read() => this.Calls<GetRollupJobDescriptor, GetRollupJobRequest, IGetRollupJobRequest, IGetRollupJobResponse>(
			this.ReadInitializer,
			this.ReadFluent,
			fluent: (s, c, f) => c.GetRollupJob(f),
			fluentAsync: (s, c, f) => c.GetRollupJobAsync(f),
			request: (s, c, r) => c.GetRollupJob(r),
			requestAsync: (s, c, r) => c.GetRollupJobAsync(r)
		);

		protected GetRollupJobRequest ReadInitializer(string role) => new GetRollupJobRequest(CreateRollupJobName(role));
		protected IGetRollupJobRequest ReadFluent(string role, GetRollupJobDescriptor d) => d.Id(CreateRollupJobName(role));

		protected override IDictionary<string, Func<LazyResponses>> AfterCreateCalls() => new Dictionary<string, Func<LazyResponses>>
		{
			{ "start", () => this.Calls<StartRollupJobDescriptor, StartRollupJobRequest, IStartRollupJobRequest, IStartRollupJobResponse>(
				this.StartInitializer,
				this.StartFluent,
				fluent: (s, c, f) => c.StartRollupJob(CreateRollupJobName(s), f),
				fluentAsync: (s, c, f) => c.StartRollupJobAsync(CreateRollupJobName(s), f),
				request: (s, c, r) => c.StartRollupJob(r),
				requestAsync: (s, c, r) => c.StartRollupJobAsync(r)
			)},
			{ "stop", () => this.Calls<StopRollupJobDescriptor, StopRollupJobRequest, IStopRollupJobRequest, IStopRollupJobResponse>(
				this.StopInitializer,
				this.StopFluent,
				fluent: (s, c, f) => c.StopRollupJob(CreateRollupJobName(s), f),
				fluentAsync: (s, c, f) => c.StopRollupJobAsync(CreateRollupJobName(s), f),
				request: (s, c, r) => c.StopRollupJob(r),
				requestAsync: (s, c, r) => c.StopRollupJobAsync(r)
			)},
		};
		protected StartRollupJobRequest StartInitializer(string role) => new StartRollupJobRequest(CreateRollupJobName(role));
		protected IStartRollupJobRequest StartFluent(string role, StartRollupJobDescriptor d) => d;

		protected StopRollupJobRequest StopInitializer(string role) => new StopRollupJobRequest(CreateRollupJobName(role));
		protected IStopRollupJobRequest StopFluent(string role, StopRollupJobDescriptor d) => d;

		[I] public async Task StartsJob() =>
			await this.AssertOnAfterCreateResponse<IStartRollupJobResponse>("start", r => r.Started.Should().BeTrue());

		[I] public async Task StopsJob() =>
			await this.AssertOnAfterCreateResponse<IStopRollupJobResponse>("stop", r => r.Stopped.Should().BeTrue());

		// ignored because we mark SupportsUpdates => false
		protected override LazyResponses Update() => null;

		protected override LazyResponses Delete() => this.Calls<DeleteRollupJobDescriptor, DeleteRollupJobRequest, IDeleteRollupJobRequest, IDeleteRollupJobResponse>(
			this.DeleteInitializer,
			this.DeleteFluent,
			fluent: (s, c, f) => c.DeleteRollupJob(CreateRollupJobName(s), f),
			fluentAsync: (s, c, f) => c.DeleteRollupJobAsync(CreateRollupJobName(s), f),
			request: (s, c, r) => c.DeleteRollupJob(r),
			requestAsync: (s, c, r) => c.DeleteRollupJobAsync(r)
		);

		protected DeleteRollupJobRequest DeleteInitializer(string role) => new DeleteRollupJobRequest(CreateRollupJobName(role));
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

		[I, SkipVersion("<6.4.1", "https://github.com/elastic/elasticsearch/issues/34292")]
		protected override async Task GetAfterDeleteIsValid() => await this.AssertOnGetAfterDelete(r => r.ShouldBeValid());
	}
}
