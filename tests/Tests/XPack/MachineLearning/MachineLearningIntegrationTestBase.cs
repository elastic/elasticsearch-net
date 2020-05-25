// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Core.Xunit;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.MachineLearning
{
	[SkipVersion("<5.5.0", "Machine Learning does not exist in previous versions")]
	[SkipOnCi]
	public abstract class MachineLearningIntegrationTestBase<TResponse, TInterface, TDescriptor, TInitializer>
		: ApiIntegrationTestBase<MachineLearningCluster, TResponse, TInterface, TDescriptor, TInitializer>
		where TResponse : class, IResponse
		where TDescriptor : class, TInterface
		where TInitializer : class, TInterface
		where TInterface : class
	{
		protected MachineLearningIntegrationTestBase(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		// https://youtrack.jetbrains.com/issue/RIDER-19912
		[U] protected override Task HitsTheCorrectUrl() => base.HitsTheCorrectUrl();

		[U] protected override Task UsesCorrectHttpMethod() => base.UsesCorrectHttpMethod();

		[U] protected override void SerializesInitializer() => base.SerializesInitializer();

		[U] protected override void SerializesFluent() => base.SerializesFluent();

		[I] public override Task ReturnsExpectedStatusCode() => base.ReturnsExpectedResponse();

		[I] public override Task ReturnsExpectedIsValid() => base.ReturnsExpectedIsValid();

		[I] public override Task ReturnsExpectedResponse() => base.ReturnsExpectedResponse();

		protected PutFilterResponse PutFilter(IElasticClient client, string filterId)
		{
			var putFilterResponse = client.MachineLearning.PutFilter(filterId, f => f
				.Description("A list of safe domains")
				.Items("*.google.com", "wikipedia.org")
			);

			if (!putFilterResponse.IsValid)
				throw new Exception($"Problem putting filter {filterId} for integration test: {putFilterResponse.DebugInformation}");

			return putFilterResponse;
		}

		protected DeleteFilterResponse DeleteFilter(IElasticClient client, string filterId)
		{
			var deleteFilterResponse = client.MachineLearning.DeleteFilter(filterId);

			if (!deleteFilterResponse.IsValid)
				throw new Exception($"Problem deleting filter {filterId} for integration test: {deleteFilterResponse.DebugInformation}");

			return deleteFilterResponse;
		}

		protected PutCalendarResponse PutCalendar(IElasticClient client, string calendarId)
		{
			var putCalendarResponse = client.MachineLearning.PutCalendar(calendarId, f => f
				.Description("Planned outages")
			);

			if (!putCalendarResponse.IsValid)
				throw new Exception($"Problem putting calendar {calendarId} for integration test: {putCalendarResponse.DebugInformation}");

			return putCalendarResponse;
		}
		protected PostCalendarEventsResponse PostCalendarEvent(IElasticClient client, string calendarId)
		{
			var startDate = DateTime.Now.Year;

			var postCalendarEventsResponse = client.MachineLearning.PostCalendarEvents(calendarId, f => f
				.Events(new ScheduledEvent
					{
						StartTime = new DateTimeOffset(startDate, 1, 1, 0, 0, 0, TimeSpan.Zero),
						EndTime = new DateTimeOffset(startDate + 1, 1, 1, 0, 0, 0, TimeSpan.Zero),
						Description = $"Event",
						CalendarId = calendarId
					})
			);

			if (!postCalendarEventsResponse.IsValid)
				throw new Exception($"Problem posting calendar event for calendar {calendarId} for integration test: {postCalendarEventsResponse.DebugInformation}");

			return postCalendarEventsResponse;
		}

		private IEnumerable<ScheduledEvent> GetScheduledEvents(string calendarId)
		{
			var startDate = DateTime.Now.Year;

			for (var i = 0; i < 10; i++)
				yield return new ScheduledEvent
				{
					StartTime = new DateTimeOffset(startDate + i, 1, 1, 0, 0, 0, TimeSpan.Zero),
					EndTime = new DateTimeOffset(startDate + 1 + i, 1, 1, 0, 0, 0, TimeSpan.Zero),
					Description = $"Event {i}",
					CalendarId = calendarId
				};
		}

		protected PostCalendarEventsResponse PostCalendarEvents(IElasticClient client, string calendarId)
		{
			var postCalendarEventsResponse = client.MachineLearning.PostCalendarEvents(calendarId, f => f.Events(GetScheduledEvents(calendarId)));

			if (!postCalendarEventsResponse.IsValid)
				throw new Exception($"Problem posting calendar events {calendarId} for integration test: {postCalendarEventsResponse.DebugInformation}");

			return postCalendarEventsResponse;
		}

		protected PutCalendarJobResponse PutCalendarJob(IElasticClient client, string calendarId, string jobId)
		{
			var putCalendarJobResponse = client.MachineLearning.PutCalendarJob(calendarId, jobId, f => f);

			if (!putCalendarJobResponse.IsValid)
				throw new Exception($"Problem putting calendar job {calendarId} / {jobId} for integration test: {putCalendarJobResponse.DebugInformation}");

			return putCalendarJobResponse;
		}

		protected PutJobResponse PutJob(IElasticClient client, string jobId)
		{
			var putJobResponse = client.MachineLearning.PutJob<Metric>(jobId, f => f
				.Description("Lab 1 - Simple example")
				.AnalysisConfig(a => a
					.BucketSpan("30m")
					.Latency("0s")
					.Detectors(d => d.Sum(c => c.FieldName(r => r.Total)))
				)
				.DataDescription(d => d.TimeField(r => r.Timestamp))
			);

			if (!putJobResponse.IsValid)
				throw new Exception($"Problem putting job {jobId} for integration test: {putJobResponse.DebugInformation}");

			return putJobResponse;
		}

		protected OpenJobResponse OpenJob(IElasticClient client, string jobId)
		{
			var openJobResponse = client.MachineLearning.OpenJob(jobId);
			if (!openJobResponse.IsValid || openJobResponse.Opened == false)
				throw new Exception($"Problem opening job {jobId} for integration test: {openJobResponse.DebugInformation}");

			return openJobResponse;
		}

		protected PostJobDataResponse PostJobData(IElasticClient client, string jobId, int bucketSize, int bucketSpanSeconds)
		{
			var timestamp = 1483228800000L; // 2017-01-01T00:00:00Z
			var data = new List<object>(bucketSize);
			for (var i = 0; i < bucketSize; i++)
			{
				data.Add(new { time = timestamp });
				if (i % 1000 == 0)
					data.AddRange(new[]
					{
						new { time = timestamp },
						new { time = timestamp },
						new { time = timestamp }
					});
				timestamp += bucketSpanSeconds * 1000;
			}

			var postJobDataResponse = client.MachineLearning.PostJobData(jobId, d => d.Data(data));
			if (!postJobDataResponse.IsValid)
				throw new Exception($"Problem posting data for integration test: {postJobDataResponse.DebugInformation}");

			return postJobDataResponse;
		}

		protected FlushJobResponse FlushJob(IElasticClient client, string jobId, bool calculateInterim)
		{
			var flushJobResponse = client.MachineLearning.FlushJob(jobId, f => f.CalculateInterim(calculateInterim));
			if (!flushJobResponse.IsValid || flushJobResponse.Flushed == false)
				throw new Exception($"Problem flushing job {jobId} for integration test: {flushJobResponse.DebugInformation}");

			return flushJobResponse;
		}

		protected CloseJobResponse CloseJob(IElasticClient client, string jobId)
		{
			var closeJobResponse = client.MachineLearning.CloseJob(jobId);
			if (!closeJobResponse.IsValid || closeJobResponse.Closed == false)
				throw new Exception($"Problem closing job {jobId} for integration test: : {closeJobResponse.DebugInformation}");

			return closeJobResponse;
		}

		protected DeleteJobResponse DeleteJob(IElasticClient client, string jobId)
		{
			var deleteJobResponse = client.MachineLearning.DeleteJob(jobId);
			if (!deleteJobResponse.IsValid || deleteJobResponse.Acknowledged == false)
				throw new Exception($"Problem deleting job {jobId} for integration test: : {deleteJobResponse.DebugInformation}");

			return deleteJobResponse;
		}

		protected PutDatafeedResponse PutDatafeed(IElasticClient client, string jobId)
		{
			var putDataFeedResponse = client.MachineLearning.PutDatafeed<Metric>(jobId + "-datafeed", f => f
				.Indices(typeof(Metric)) // TODO: This should be default inferred from T on method
				.JobId(jobId)
				.Query(q => q.MatchAll()));

			if (!putDataFeedResponse.IsValid)
				throw new Exception($"Problem putting datafeed for job {jobId} for integration test: {putDataFeedResponse.DebugInformation}");

			return putDataFeedResponse;
		}

		protected StartDatafeedResponse StartDatafeed(IElasticClient client, string jobId)
		{
			var startDatafeedResponse = client.MachineLearning.StartDatafeed(jobId + "-datafeed");
			if (!startDatafeedResponse.IsValid || startDatafeedResponse.Started == false)
				throw new Exception($"Problem starting datafeed for job {jobId} for integration test: {startDatafeedResponse.DebugInformation}");

			return startDatafeedResponse;
		}

		protected StopDatafeedResponse StopDatafeed(IElasticClient client, string jobId)
		{
			var stopDatafeedResponse = client.MachineLearning.StopDatafeed(jobId + "-datafeed");
			if (!stopDatafeedResponse.IsValid || stopDatafeedResponse.Stopped == false)
				throw new Exception($"Problem stopping datafeed for job {jobId} for integration test: {stopDatafeedResponse.DebugInformation}");

			return stopDatafeedResponse;
		}

		protected void IndexSnapshot(IElasticClient client, string jobId, string snapshotId, string timestamp = "2016-06-02T00:00:00Z")
		{
			var unixTimestamp = (int)DateTime.Parse(timestamp).Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

			client.Index<object>(new
			{
				job_id = jobId,
				snapshot_id = snapshotId,
				timestamp = timestamp,
				description = snapshotId + " description",
				latest_record_time_stamp = timestamp,
				latest_result_time_stamp = timestamp,
				snapshot_doc_count = 1,
				model_size_stats = new
				{
					job_id = jobId,
					model_bytes = 20,
					log_time = timestamp
				},
				quantiles = new
				{
					job_id = jobId,
					timestamp = unixTimestamp,
					quantile_state = "quantiles-2"
				}
			}, i => i.Id(jobId + "_model_snapshot_" + snapshotId)
				.Index(".ml-anomalies-" + jobId)
				.Refresh(Refresh.WaitFor));

			// Index a bucket result, else RevertModelSnapshot throws a null
			client.Index<object>(new
			{
				job_id = jobId,
				result_type = "bucket",
				timestamp = timestamp,
				bucket_span = 1,
			}, i => i.Id(jobId + "_" + unixTimestamp + "_1")
				.Index(".ml-anomalies-" + jobId)
				.Refresh(Refresh.WaitFor));
		}

		protected void IndexAnomalyRecord(IElasticClient client, string jobId, DateTimeOffset timestamp) => client.Index<object>(new
		{
			job_id = jobId,
			result_type = "record",
			timestamp = timestamp.ToString("o"),
			record_score = 80.0,
			bucket_span = 1,
			is_interim = true
		}, i => i.Index(".ml-anomalies-" + jobId).Refresh(Refresh.WaitFor));

		protected void IndexBucket(IElasticClient client, string jobId, DateTimeOffset timestamp) => client.Index<object>(new
		{
			job_id = jobId,
			result_type = "bucket",
			timestamp = timestamp.ToString("o"),
			anomaly_score = 90.0,
			bucket_span = 1,
			is_interim = true
		}, i => i.Index(".ml-anomalies-" + jobId).Refresh(Refresh.WaitFor));

		protected void IndexCategory(IElasticClient client, string jobId) => client.Index<object>(new
		{
			job_id = jobId,
			category_id = "1"
		}, i => i.Index(".ml-anomalies-" + jobId).Refresh(Refresh.WaitFor));

		protected void IndexInfluencer(IElasticClient client, string jobId, DateTimeOffset timestamp) => client.Index<object>(new
		{
			job_id = jobId,
			timestamp = timestamp.ToString("o"),
			influencer_field_name = "foo",
			influencer_field_value = "bar",
			influencer_score = 50,
			result_type = "influencer",
			bucket_span = 1
		}, i => i.Index(".ml-anomalies-" + jobId).Refresh(Refresh.WaitFor));

		protected void IndexForecast(IElasticClient client, string jobId, string forecastId)
		{
			client.Index<object>(new
				{
					job_id =  jobId,
					forecast_id =  forecastId,
					result_type =  "model_forecast",
					bucket_span =  1800,
					detector_index =  0,
					timestamp =  1486591300000,
					model_feature =  "'arithmetic mean value by person'",
					forecast_lower =  5440.502250736747,
					forecast_upper =  6294.296972680027,
					forecast_prediction =  5867.399611708387
				}
				, i => i.Id($"{jobId}_model_forecast_{forecastId}_1486591300000_1800_0_961_0").Index(".ml-anomalies-shared").Refresh(Refresh.WaitFor));

			client.Index<object>(new
				{
					job_id =  jobId,
					result_type =  "model_forecast_request_stats",
					forecast_id =  forecastId,
					processed_record_count =  48,
					forecast_messages =  new object[0],
					timestamp =  1486575000000,
					forecast_start_timestamp =  1486575000000,
					forecast_end_timestamp =  1486661400000,
					forecast_create_timestamp =  1535721789000,
					forecast_expiry_timestamp =  1536931389000,
					forecast_progress =  1,
					processing_time_ms =  3,
					forecast_memory_bytes =  7034,
					forecast_status =  "finished"
				}
				, i => i.Id($"{jobId}_model_forecast_request_stats_{forecastId}").Index(".ml-anomalies-shared").Refresh(Refresh.WaitFor));

		}
	}
}
