using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.XPack.MachineLearning
{
	[SkipVersion("<5.5.0", "Machine Learning does not exist in previous versions")]
	public abstract class MachineLearningIntegrationTestBase<TResponse, TInterface, TDescriptor, TInitializer>
		: ApiIntegrationTestBase<MachineLearningCluster, TResponse, TInterface, TDescriptor, TInitializer>
		where TResponse : class, IResponse
		where TDescriptor : class, TInterface
		where TInitializer : class, TInterface
		where TInterface : class
	{
		protected MachineLearningIntegrationTestBase(MachineLearningCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected IPutJobResponse PutJob(IElasticClient client, string jobId)
		{
			var putJobResponse = client.PutJob<Metric>(jobId, f => f
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

		protected IOpenJobResponse OpenJob(IElasticClient client, string jobId)
		{
			var openJobResponse = client.OpenJob(jobId);
			if (!openJobResponse.IsValid || openJobResponse.Opened == false)
				throw new Exception($"Problem opening job {jobId} for integration test: {openJobResponse.DebugInformation}");
			return openJobResponse;
		}

		protected IFlushJobResponse FlushJob(IElasticClient client, string jobId, bool calculateInterim)
		{
			var flushJobResponse = client.FlushJob(jobId, f => f.CalculateInterim(calculateInterim));
			if (!flushJobResponse.IsValid || flushJobResponse.Flushed == false)
				throw new Exception($"Problem flushing job {jobId} for integration test: {flushJobResponse.DebugInformation}");
			return flushJobResponse;
		}

		protected ICloseJobResponse CloseJob(IElasticClient client, string jobId)
		{
			var closeJobResponse = client.CloseJob(jobId);
			if (!closeJobResponse.IsValid || closeJobResponse.Closed == false)
				throw new Exception($"Problem closing job {jobId} for integration test: : {closeJobResponse.DebugInformation}");
			return closeJobResponse;
		}

		protected IDeleteJobResponse DeleteJob(IElasticClient client, string jobId)
		{
			var deleteJobResponse = client.DeleteJob(jobId);
			if (!deleteJobResponse.IsValid || deleteJobResponse.Acknowledged == false)
				throw new Exception($"Problem deleting job {jobId} for integration test: : {deleteJobResponse.DebugInformation}");
			return deleteJobResponse;
		}

		protected IPutDatafeedResponse PutDatafeed(IElasticClient client, string jobId)
		{
			var putDataFeedResponse = client.PutDatafeed<Metric>(jobId + "-datafeed", f => f
				.JobId(jobId)
				.Query(q => q.MatchAll()));

			if (!putDataFeedResponse.IsValid)
				throw new Exception($"Problem putting datafeed for job {jobId} for integration test: {putDataFeedResponse.DebugInformation}");

			return putDataFeedResponse;
		}

		protected IStartDatafeedResponse StartDatafeed(IElasticClient client, string jobId)
		{
			var startDatafeedResponse = client.StartDatafeed(jobId + "-datafeed");
			if (!startDatafeedResponse.IsValid || startDatafeedResponse.Started == false)
				throw new Exception($"Problem starting datafeed for job {jobId} for integration test: {startDatafeedResponse.DebugInformation}");
			return startDatafeedResponse;
		}

		protected IStopDatafeedResponse StopDatafeed(IElasticClient client, string jobId)
		{
			var stopDatafeedResponse = client.StopDatafeed(jobId + "-datafeed");
			if (!stopDatafeedResponse.IsValid || stopDatafeedResponse.Stopped == false)
				throw new Exception($"Problem stopping datafeed for job {jobId} for integration test: {stopDatafeedResponse.DebugInformation}");
			return stopDatafeedResponse;
		}

		protected void IndexSnapshot(IElasticClient client, string jobId, string snapshotId, string timestamp = "2016-06-02T00:00:00Z")
		{
			var unixTimestamp = (int)(DateTime.Parse(timestamp).Subtract(new DateTime(1970, 1, 1)).TotalSeconds);

			client.Index<object>(new
			{
				job_id = jobId,
				snapshot_id = snapshotId,
				timestamp = timestamp,
				description = snapshotId + " description",
				latest_record_time_stamp = timestamp,
				latest_result_time_stamp = timestamp,
				snapshot_doc_count = 1,
				model_size_stats = new {
					job_id = jobId,
					model_bytes = 20,
					log_time = timestamp
				},
				quantiles = new {
					job_id = jobId,
					timestamp = unixTimestamp,
					quantile_state = "quantiles-2"
				}
			}, i => i.Id(jobId + "_model_snapshot_" + snapshotId)
					 .Type("doc")
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
					  .Type("doc")
				      .Index(".ml-anomalies-" + jobId)
					  .Refresh(Refresh.WaitFor));
		}

		protected void IndexAnomalyRecord(IElasticClient client, string jobId, DateTimeOffset timestamp)
		{
			client.Index<object>(new
			{
				job_id = jobId,
				result_type = "record",
				timestamp = timestamp.ToString("o"),
				record_score = 80.0,
				bucket_span = 1,
				is_interim = true
			}, i => i.Type("doc").Index(".ml-anomalies-" + jobId).Refresh(Refresh.WaitFor));
		}

		protected void IndexBucket(IElasticClient client, string jobId, DateTimeOffset timestamp)
		{
			client.Index<object>(new
			{
				job_id = jobId,
				result_type = "bucket",
				timestamp = timestamp.ToString("o"),
				anomaly_score = 90.0,
				bucket_span = 1,
				is_interim = true
			}, i => i.Type("doc").Index(".ml-anomalies-" + jobId).Refresh(Refresh.WaitFor));
		}

		protected void IndexCategory(IElasticClient client, string jobId)
		{
			client.Index<object>(new
			{
				job_id = jobId,
				category_id = "1"
			}, i => i.Type("doc").Index(".ml-anomalies-" + jobId).Refresh(Refresh.WaitFor));
		}

		protected void IndexInfluencer(IElasticClient client, string jobId, DateTimeOffset timestamp)
		{
			client.Index<object>(new
			{
				job_id = jobId,
				timestamp = timestamp.ToString("o"),
				influencer_field_name = "foo",
				influencer_field_value = "bar",
				influencer_score = 50,
				result_type = "influencer",
				bucket_span = 1
			}, i => i.Type("doc").Index(".ml-anomalies-" + jobId).Refresh(Refresh.WaitFor));
		}
	}
}
