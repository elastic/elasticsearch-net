using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net.Specification.MachineLearningApi;

// ReSharper disable once CheckNamespace
namespace Nest.Specification.MachineLearningApi
{
	///<summary>
	/// Logically groups all MachineLearning API's together so that they may be discovered more naturally.
	/// <para>Not intended to be instantiated directly please defer to the <see cref = "IElasticClient.MachineLearning"/> property
	/// on <see cref = "IElasticClient"/>.
	///</para>
	///</summary>
	public class MachineLearningNamespace : NamespacedClientProxy
	{
		internal MachineLearningNamespace(ElasticClient client): base(client)
		{
		}

		///<inheritdoc cref = "ICloseJobRequest"/>
		public CloseJobResponse CloseJob(Id jobId, Func<CloseJobDescriptor, ICloseJobRequest> selector = null) => CloseJob(selector.InvokeOrDefault(new CloseJobDescriptor(jobId: jobId)));
		///<inheritdoc cref = "ICloseJobRequest"/>
		public Task<CloseJobResponse> CloseJobAsync(Id jobId, Func<CloseJobDescriptor, ICloseJobRequest> selector = null, CancellationToken ct = default) => CloseJobAsync(selector.InvokeOrDefault(new CloseJobDescriptor(jobId: jobId)), ct);
		///<inheritdoc cref = "ICloseJobRequest"/>
		public CloseJobResponse CloseJob(ICloseJobRequest request) => DoRequest<ICloseJobRequest, CloseJobResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "ICloseJobRequest"/>
		public Task<CloseJobResponse> CloseJobAsync(ICloseJobRequest request, CancellationToken ct = default) => DoRequestAsync<ICloseJobRequest, CloseJobResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IDeleteCalendarRequest"/>
		public DeleteCalendarResponse DeleteCalendar(Id calendarId, Func<DeleteCalendarDescriptor, IDeleteCalendarRequest> selector = null) => DeleteCalendar(selector.InvokeOrDefault(new DeleteCalendarDescriptor(calendarId: calendarId)));
		///<inheritdoc cref = "IDeleteCalendarRequest"/>
		public Task<DeleteCalendarResponse> DeleteCalendarAsync(Id calendarId, Func<DeleteCalendarDescriptor, IDeleteCalendarRequest> selector = null, CancellationToken ct = default) => DeleteCalendarAsync(selector.InvokeOrDefault(new DeleteCalendarDescriptor(calendarId: calendarId)), ct);
		///<inheritdoc cref = "IDeleteCalendarRequest"/>
		public DeleteCalendarResponse DeleteCalendar(IDeleteCalendarRequest request) => DoRequest<IDeleteCalendarRequest, DeleteCalendarResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IDeleteCalendarRequest"/>
		public Task<DeleteCalendarResponse> DeleteCalendarAsync(IDeleteCalendarRequest request, CancellationToken ct = default) => DoRequestAsync<IDeleteCalendarRequest, DeleteCalendarResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IDeleteCalendarEventRequest"/>
		public DeleteCalendarEventResponse DeleteCalendarEvent(Id calendarId, Id eventId, Func<DeleteCalendarEventDescriptor, IDeleteCalendarEventRequest> selector = null) => DeleteCalendarEvent(selector.InvokeOrDefault(new DeleteCalendarEventDescriptor(calendarId: calendarId, eventId: eventId)));
		///<inheritdoc cref = "IDeleteCalendarEventRequest"/>
		public Task<DeleteCalendarEventResponse> DeleteCalendarEventAsync(Id calendarId, Id eventId, Func<DeleteCalendarEventDescriptor, IDeleteCalendarEventRequest> selector = null, CancellationToken ct = default) => DeleteCalendarEventAsync(selector.InvokeOrDefault(new DeleteCalendarEventDescriptor(calendarId: calendarId, eventId: eventId)), ct);
		///<inheritdoc cref = "IDeleteCalendarEventRequest"/>
		public DeleteCalendarEventResponse DeleteCalendarEvent(IDeleteCalendarEventRequest request) => DoRequest<IDeleteCalendarEventRequest, DeleteCalendarEventResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IDeleteCalendarEventRequest"/>
		public Task<DeleteCalendarEventResponse> DeleteCalendarEventAsync(IDeleteCalendarEventRequest request, CancellationToken ct = default) => DoRequestAsync<IDeleteCalendarEventRequest, DeleteCalendarEventResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IDeleteCalendarJobRequest"/>
		public DeleteCalendarJobResponse DeleteCalendarJob(Id calendarId, Id jobId, Func<DeleteCalendarJobDescriptor, IDeleteCalendarJobRequest> selector = null) => DeleteCalendarJob(selector.InvokeOrDefault(new DeleteCalendarJobDescriptor(calendarId: calendarId, jobId: jobId)));
		///<inheritdoc cref = "IDeleteCalendarJobRequest"/>
		public Task<DeleteCalendarJobResponse> DeleteCalendarJobAsync(Id calendarId, Id jobId, Func<DeleteCalendarJobDescriptor, IDeleteCalendarJobRequest> selector = null, CancellationToken ct = default) => DeleteCalendarJobAsync(selector.InvokeOrDefault(new DeleteCalendarJobDescriptor(calendarId: calendarId, jobId: jobId)), ct);
		///<inheritdoc cref = "IDeleteCalendarJobRequest"/>
		public DeleteCalendarJobResponse DeleteCalendarJob(IDeleteCalendarJobRequest request) => DoRequest<IDeleteCalendarJobRequest, DeleteCalendarJobResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IDeleteCalendarJobRequest"/>
		public Task<DeleteCalendarJobResponse> DeleteCalendarJobAsync(IDeleteCalendarJobRequest request, CancellationToken ct = default) => DoRequestAsync<IDeleteCalendarJobRequest, DeleteCalendarJobResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IDeleteDatafeedRequest"/>
		public DeleteDatafeedResponse DeleteDatafeed(Id datafeedId, Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> selector = null) => DeleteDatafeed(selector.InvokeOrDefault(new DeleteDatafeedDescriptor(datafeedId: datafeedId)));
		///<inheritdoc cref = "IDeleteDatafeedRequest"/>
		public Task<DeleteDatafeedResponse> DeleteDatafeedAsync(Id datafeedId, Func<DeleteDatafeedDescriptor, IDeleteDatafeedRequest> selector = null, CancellationToken ct = default) => DeleteDatafeedAsync(selector.InvokeOrDefault(new DeleteDatafeedDescriptor(datafeedId: datafeedId)), ct);
		///<inheritdoc cref = "IDeleteDatafeedRequest"/>
		public DeleteDatafeedResponse DeleteDatafeed(IDeleteDatafeedRequest request) => DoRequest<IDeleteDatafeedRequest, DeleteDatafeedResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IDeleteDatafeedRequest"/>
		public Task<DeleteDatafeedResponse> DeleteDatafeedAsync(IDeleteDatafeedRequest request, CancellationToken ct = default) => DoRequestAsync<IDeleteDatafeedRequest, DeleteDatafeedResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IDeleteExpiredDataRequest"/>
		public DeleteExpiredDataResponse DeleteExpiredData(Func<DeleteExpiredDataDescriptor, IDeleteExpiredDataRequest> selector = null) => DeleteExpiredData(selector.InvokeOrDefault(new DeleteExpiredDataDescriptor()));
		///<inheritdoc cref = "IDeleteExpiredDataRequest"/>
		public Task<DeleteExpiredDataResponse> DeleteExpiredDataAsync(Func<DeleteExpiredDataDescriptor, IDeleteExpiredDataRequest> selector = null, CancellationToken ct = default) => DeleteExpiredDataAsync(selector.InvokeOrDefault(new DeleteExpiredDataDescriptor()), ct);
		///<inheritdoc cref = "IDeleteExpiredDataRequest"/>
		public DeleteExpiredDataResponse DeleteExpiredData(IDeleteExpiredDataRequest request) => DoRequest<IDeleteExpiredDataRequest, DeleteExpiredDataResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IDeleteExpiredDataRequest"/>
		public Task<DeleteExpiredDataResponse> DeleteExpiredDataAsync(IDeleteExpiredDataRequest request, CancellationToken ct = default) => DoRequestAsync<IDeleteExpiredDataRequest, DeleteExpiredDataResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IDeleteFilterRequest"/>
		public DeleteFilterResponse DeleteFilter(Id filterId, Func<DeleteFilterDescriptor, IDeleteFilterRequest> selector = null) => DeleteFilter(selector.InvokeOrDefault(new DeleteFilterDescriptor(filterId: filterId)));
		///<inheritdoc cref = "IDeleteFilterRequest"/>
		public Task<DeleteFilterResponse> DeleteFilterAsync(Id filterId, Func<DeleteFilterDescriptor, IDeleteFilterRequest> selector = null, CancellationToken ct = default) => DeleteFilterAsync(selector.InvokeOrDefault(new DeleteFilterDescriptor(filterId: filterId)), ct);
		///<inheritdoc cref = "IDeleteFilterRequest"/>
		public DeleteFilterResponse DeleteFilter(IDeleteFilterRequest request) => DoRequest<IDeleteFilterRequest, DeleteFilterResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IDeleteFilterRequest"/>
		public Task<DeleteFilterResponse> DeleteFilterAsync(IDeleteFilterRequest request, CancellationToken ct = default) => DoRequestAsync<IDeleteFilterRequest, DeleteFilterResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IDeleteForecastRequest"/>
		public DeleteForecastResponse DeleteForecast(Id jobId, ForecastIds forecastId, Func<DeleteForecastDescriptor, IDeleteForecastRequest> selector = null) => DeleteForecast(selector.InvokeOrDefault(new DeleteForecastDescriptor(jobId: jobId, forecastId: forecastId)));
		///<inheritdoc cref = "IDeleteForecastRequest"/>
		public Task<DeleteForecastResponse> DeleteForecastAsync(Id jobId, ForecastIds forecastId, Func<DeleteForecastDescriptor, IDeleteForecastRequest> selector = null, CancellationToken ct = default) => DeleteForecastAsync(selector.InvokeOrDefault(new DeleteForecastDescriptor(jobId: jobId, forecastId: forecastId)), ct);
		///<inheritdoc cref = "IDeleteForecastRequest"/>
		public DeleteForecastResponse DeleteForecast(IDeleteForecastRequest request) => DoRequest<IDeleteForecastRequest, DeleteForecastResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IDeleteForecastRequest"/>
		public Task<DeleteForecastResponse> DeleteForecastAsync(IDeleteForecastRequest request, CancellationToken ct = default) => DoRequestAsync<IDeleteForecastRequest, DeleteForecastResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IDeleteJobRequest"/>
		public DeleteJobResponse DeleteJob(Id jobId, Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null) => DeleteJob(selector.InvokeOrDefault(new DeleteJobDescriptor(jobId: jobId)));
		///<inheritdoc cref = "IDeleteJobRequest"/>
		public Task<DeleteJobResponse> DeleteJobAsync(Id jobId, Func<DeleteJobDescriptor, IDeleteJobRequest> selector = null, CancellationToken ct = default) => DeleteJobAsync(selector.InvokeOrDefault(new DeleteJobDescriptor(jobId: jobId)), ct);
		///<inheritdoc cref = "IDeleteJobRequest"/>
		public DeleteJobResponse DeleteJob(IDeleteJobRequest request) => DoRequest<IDeleteJobRequest, DeleteJobResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IDeleteJobRequest"/>
		public Task<DeleteJobResponse> DeleteJobAsync(IDeleteJobRequest request, CancellationToken ct = default) => DoRequestAsync<IDeleteJobRequest, DeleteJobResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IDeleteModelSnapshotRequest"/>
		public DeleteModelSnapshotResponse DeleteModelSnapshot(Id jobId, Id snapshotId, Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null) => DeleteModelSnapshot(selector.InvokeOrDefault(new DeleteModelSnapshotDescriptor(jobId: jobId, snapshotId: snapshotId)));
		///<inheritdoc cref = "IDeleteModelSnapshotRequest"/>
		public Task<DeleteModelSnapshotResponse> DeleteModelSnapshotAsync(Id jobId, Id snapshotId, Func<DeleteModelSnapshotDescriptor, IDeleteModelSnapshotRequest> selector = null, CancellationToken ct = default) => DeleteModelSnapshotAsync(selector.InvokeOrDefault(new DeleteModelSnapshotDescriptor(jobId: jobId, snapshotId: snapshotId)), ct);
		///<inheritdoc cref = "IDeleteModelSnapshotRequest"/>
		public DeleteModelSnapshotResponse DeleteModelSnapshot(IDeleteModelSnapshotRequest request) => DoRequest<IDeleteModelSnapshotRequest, DeleteModelSnapshotResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IDeleteModelSnapshotRequest"/>
		public Task<DeleteModelSnapshotResponse> DeleteModelSnapshotAsync(IDeleteModelSnapshotRequest request, CancellationToken ct = default) => DoRequestAsync<IDeleteModelSnapshotRequest, DeleteModelSnapshotResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IFlushJobRequest"/>
		public FlushJobResponse FlushJob(Id jobId, Func<FlushJobDescriptor, IFlushJobRequest> selector = null) => FlushJob(selector.InvokeOrDefault(new FlushJobDescriptor(jobId: jobId)));
		///<inheritdoc cref = "IFlushJobRequest"/>
		public Task<FlushJobResponse> FlushJobAsync(Id jobId, Func<FlushJobDescriptor, IFlushJobRequest> selector = null, CancellationToken ct = default) => FlushJobAsync(selector.InvokeOrDefault(new FlushJobDescriptor(jobId: jobId)), ct);
		///<inheritdoc cref = "IFlushJobRequest"/>
		public FlushJobResponse FlushJob(IFlushJobRequest request) => DoRequest<IFlushJobRequest, FlushJobResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IFlushJobRequest"/>
		public Task<FlushJobResponse> FlushJobAsync(IFlushJobRequest request, CancellationToken ct = default) => DoRequestAsync<IFlushJobRequest, FlushJobResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IForecastJobRequest"/>
		public ForecastJobResponse ForecastJob(Id jobId, Func<ForecastJobDescriptor, IForecastJobRequest> selector = null) => ForecastJob(selector.InvokeOrDefault(new ForecastJobDescriptor(jobId: jobId)));
		///<inheritdoc cref = "IForecastJobRequest"/>
		public Task<ForecastJobResponse> ForecastJobAsync(Id jobId, Func<ForecastJobDescriptor, IForecastJobRequest> selector = null, CancellationToken ct = default) => ForecastJobAsync(selector.InvokeOrDefault(new ForecastJobDescriptor(jobId: jobId)), ct);
		///<inheritdoc cref = "IForecastJobRequest"/>
		public ForecastJobResponse ForecastJob(IForecastJobRequest request) => DoRequest<IForecastJobRequest, ForecastJobResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IForecastJobRequest"/>
		public Task<ForecastJobResponse> ForecastJobAsync(IForecastJobRequest request, CancellationToken ct = default) => DoRequestAsync<IForecastJobRequest, ForecastJobResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IGetBucketsRequest"/>
		public GetBucketsResponse GetBuckets(Id jobId, Func<GetBucketsDescriptor, IGetBucketsRequest> selector = null) => GetBuckets(selector.InvokeOrDefault(new GetBucketsDescriptor(jobId: jobId)));
		///<inheritdoc cref = "IGetBucketsRequest"/>
		public Task<GetBucketsResponse> GetBucketsAsync(Id jobId, Func<GetBucketsDescriptor, IGetBucketsRequest> selector = null, CancellationToken ct = default) => GetBucketsAsync(selector.InvokeOrDefault(new GetBucketsDescriptor(jobId: jobId)), ct);
		///<inheritdoc cref = "IGetBucketsRequest"/>
		public GetBucketsResponse GetBuckets(IGetBucketsRequest request) => DoRequest<IGetBucketsRequest, GetBucketsResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IGetBucketsRequest"/>
		public Task<GetBucketsResponse> GetBucketsAsync(IGetBucketsRequest request, CancellationToken ct = default) => DoRequestAsync<IGetBucketsRequest, GetBucketsResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IGetCalendarEventsRequest"/>
		public GetCalendarEventsResponse GetCalendarEvents(Id calendarId, Func<GetCalendarEventsDescriptor, IGetCalendarEventsRequest> selector = null) => GetCalendarEvents(selector.InvokeOrDefault(new GetCalendarEventsDescriptor(calendarId: calendarId)));
		///<inheritdoc cref = "IGetCalendarEventsRequest"/>
		public Task<GetCalendarEventsResponse> GetCalendarEventsAsync(Id calendarId, Func<GetCalendarEventsDescriptor, IGetCalendarEventsRequest> selector = null, CancellationToken ct = default) => GetCalendarEventsAsync(selector.InvokeOrDefault(new GetCalendarEventsDescriptor(calendarId: calendarId)), ct);
		///<inheritdoc cref = "IGetCalendarEventsRequest"/>
		public GetCalendarEventsResponse GetCalendarEvents(IGetCalendarEventsRequest request) => DoRequest<IGetCalendarEventsRequest, GetCalendarEventsResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IGetCalendarEventsRequest"/>
		public Task<GetCalendarEventsResponse> GetCalendarEventsAsync(IGetCalendarEventsRequest request, CancellationToken ct = default) => DoRequestAsync<IGetCalendarEventsRequest, GetCalendarEventsResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IGetCalendarsRequest"/>
		public GetCalendarsResponse GetCalendars(Func<GetCalendarsDescriptor, IGetCalendarsRequest> selector = null) => GetCalendars(selector.InvokeOrDefault(new GetCalendarsDescriptor()));
		///<inheritdoc cref = "IGetCalendarsRequest"/>
		public Task<GetCalendarsResponse> GetCalendarsAsync(Func<GetCalendarsDescriptor, IGetCalendarsRequest> selector = null, CancellationToken ct = default) => GetCalendarsAsync(selector.InvokeOrDefault(new GetCalendarsDescriptor()), ct);
		///<inheritdoc cref = "IGetCalendarsRequest"/>
		public GetCalendarsResponse GetCalendars(IGetCalendarsRequest request) => DoRequest<IGetCalendarsRequest, GetCalendarsResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IGetCalendarsRequest"/>
		public Task<GetCalendarsResponse> GetCalendarsAsync(IGetCalendarsRequest request, CancellationToken ct = default) => DoRequestAsync<IGetCalendarsRequest, GetCalendarsResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IGetCategoriesRequest"/>
		public GetCategoriesResponse GetCategories(Id jobId, Func<GetCategoriesDescriptor, IGetCategoriesRequest> selector = null) => GetCategories(selector.InvokeOrDefault(new GetCategoriesDescriptor(jobId: jobId)));
		///<inheritdoc cref = "IGetCategoriesRequest"/>
		public Task<GetCategoriesResponse> GetCategoriesAsync(Id jobId, Func<GetCategoriesDescriptor, IGetCategoriesRequest> selector = null, CancellationToken ct = default) => GetCategoriesAsync(selector.InvokeOrDefault(new GetCategoriesDescriptor(jobId: jobId)), ct);
		///<inheritdoc cref = "IGetCategoriesRequest"/>
		public GetCategoriesResponse GetCategories(IGetCategoriesRequest request) => DoRequest<IGetCategoriesRequest, GetCategoriesResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IGetCategoriesRequest"/>
		public Task<GetCategoriesResponse> GetCategoriesAsync(IGetCategoriesRequest request, CancellationToken ct = default) => DoRequestAsync<IGetCategoriesRequest, GetCategoriesResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IGetDatafeedStatsRequest"/>
		public GetDatafeedStatsResponse GetDatafeedStats(Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> selector = null) => GetDatafeedStats(selector.InvokeOrDefault(new GetDatafeedStatsDescriptor()));
		///<inheritdoc cref = "IGetDatafeedStatsRequest"/>
		public Task<GetDatafeedStatsResponse> GetDatafeedStatsAsync(Func<GetDatafeedStatsDescriptor, IGetDatafeedStatsRequest> selector = null, CancellationToken ct = default) => GetDatafeedStatsAsync(selector.InvokeOrDefault(new GetDatafeedStatsDescriptor()), ct);
		///<inheritdoc cref = "IGetDatafeedStatsRequest"/>
		public GetDatafeedStatsResponse GetDatafeedStats(IGetDatafeedStatsRequest request) => DoRequest<IGetDatafeedStatsRequest, GetDatafeedStatsResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IGetDatafeedStatsRequest"/>
		public Task<GetDatafeedStatsResponse> GetDatafeedStatsAsync(IGetDatafeedStatsRequest request, CancellationToken ct = default) => DoRequestAsync<IGetDatafeedStatsRequest, GetDatafeedStatsResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IGetDatafeedsRequest"/>
		public GetDatafeedsResponse GetDatafeeds(Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> selector = null) => GetDatafeeds(selector.InvokeOrDefault(new GetDatafeedsDescriptor()));
		///<inheritdoc cref = "IGetDatafeedsRequest"/>
		public Task<GetDatafeedsResponse> GetDatafeedsAsync(Func<GetDatafeedsDescriptor, IGetDatafeedsRequest> selector = null, CancellationToken ct = default) => GetDatafeedsAsync(selector.InvokeOrDefault(new GetDatafeedsDescriptor()), ct);
		///<inheritdoc cref = "IGetDatafeedsRequest"/>
		public GetDatafeedsResponse GetDatafeeds(IGetDatafeedsRequest request) => DoRequest<IGetDatafeedsRequest, GetDatafeedsResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IGetDatafeedsRequest"/>
		public Task<GetDatafeedsResponse> GetDatafeedsAsync(IGetDatafeedsRequest request, CancellationToken ct = default) => DoRequestAsync<IGetDatafeedsRequest, GetDatafeedsResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IGetFiltersRequest"/>
		public GetFiltersResponse GetFilters(Func<GetFiltersDescriptor, IGetFiltersRequest> selector = null) => GetFilters(selector.InvokeOrDefault(new GetFiltersDescriptor()));
		///<inheritdoc cref = "IGetFiltersRequest"/>
		public Task<GetFiltersResponse> GetFiltersAsync(Func<GetFiltersDescriptor, IGetFiltersRequest> selector = null, CancellationToken ct = default) => GetFiltersAsync(selector.InvokeOrDefault(new GetFiltersDescriptor()), ct);
		///<inheritdoc cref = "IGetFiltersRequest"/>
		public GetFiltersResponse GetFilters(IGetFiltersRequest request) => DoRequest<IGetFiltersRequest, GetFiltersResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IGetFiltersRequest"/>
		public Task<GetFiltersResponse> GetFiltersAsync(IGetFiltersRequest request, CancellationToken ct = default) => DoRequestAsync<IGetFiltersRequest, GetFiltersResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IGetInfluencersRequest"/>
		public GetInfluencersResponse GetInfluencers(Id jobId, Func<GetInfluencersDescriptor, IGetInfluencersRequest> selector = null) => GetInfluencers(selector.InvokeOrDefault(new GetInfluencersDescriptor(jobId: jobId)));
		///<inheritdoc cref = "IGetInfluencersRequest"/>
		public Task<GetInfluencersResponse> GetInfluencersAsync(Id jobId, Func<GetInfluencersDescriptor, IGetInfluencersRequest> selector = null, CancellationToken ct = default) => GetInfluencersAsync(selector.InvokeOrDefault(new GetInfluencersDescriptor(jobId: jobId)), ct);
		///<inheritdoc cref = "IGetInfluencersRequest"/>
		public GetInfluencersResponse GetInfluencers(IGetInfluencersRequest request) => DoRequest<IGetInfluencersRequest, GetInfluencersResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IGetInfluencersRequest"/>
		public Task<GetInfluencersResponse> GetInfluencersAsync(IGetInfluencersRequest request, CancellationToken ct = default) => DoRequestAsync<IGetInfluencersRequest, GetInfluencersResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IGetJobStatsRequest"/>
		public GetJobStatsResponse GetJobStats(Func<GetJobStatsDescriptor, IGetJobStatsRequest> selector = null) => GetJobStats(selector.InvokeOrDefault(new GetJobStatsDescriptor()));
		///<inheritdoc cref = "IGetJobStatsRequest"/>
		public Task<GetJobStatsResponse> GetJobStatsAsync(Func<GetJobStatsDescriptor, IGetJobStatsRequest> selector = null, CancellationToken ct = default) => GetJobStatsAsync(selector.InvokeOrDefault(new GetJobStatsDescriptor()), ct);
		///<inheritdoc cref = "IGetJobStatsRequest"/>
		public GetJobStatsResponse GetJobStats(IGetJobStatsRequest request) => DoRequest<IGetJobStatsRequest, GetJobStatsResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IGetJobStatsRequest"/>
		public Task<GetJobStatsResponse> GetJobStatsAsync(IGetJobStatsRequest request, CancellationToken ct = default) => DoRequestAsync<IGetJobStatsRequest, GetJobStatsResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IGetJobsRequest"/>
		public GetJobsResponse GetJobs(Func<GetJobsDescriptor, IGetJobsRequest> selector = null) => GetJobs(selector.InvokeOrDefault(new GetJobsDescriptor()));
		///<inheritdoc cref = "IGetJobsRequest"/>
		public Task<GetJobsResponse> GetJobsAsync(Func<GetJobsDescriptor, IGetJobsRequest> selector = null, CancellationToken ct = default) => GetJobsAsync(selector.InvokeOrDefault(new GetJobsDescriptor()), ct);
		///<inheritdoc cref = "IGetJobsRequest"/>
		public GetJobsResponse GetJobs(IGetJobsRequest request) => DoRequest<IGetJobsRequest, GetJobsResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IGetJobsRequest"/>
		public Task<GetJobsResponse> GetJobsAsync(IGetJobsRequest request, CancellationToken ct = default) => DoRequestAsync<IGetJobsRequest, GetJobsResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IGetModelSnapshotsRequest"/>
		public GetModelSnapshotsResponse GetModelSnapshots(Id jobId, Func<GetModelSnapshotsDescriptor, IGetModelSnapshotsRequest> selector = null) => GetModelSnapshots(selector.InvokeOrDefault(new GetModelSnapshotsDescriptor(jobId: jobId)));
		///<inheritdoc cref = "IGetModelSnapshotsRequest"/>
		public Task<GetModelSnapshotsResponse> GetModelSnapshotsAsync(Id jobId, Func<GetModelSnapshotsDescriptor, IGetModelSnapshotsRequest> selector = null, CancellationToken ct = default) => GetModelSnapshotsAsync(selector.InvokeOrDefault(new GetModelSnapshotsDescriptor(jobId: jobId)), ct);
		///<inheritdoc cref = "IGetModelSnapshotsRequest"/>
		public GetModelSnapshotsResponse GetModelSnapshots(IGetModelSnapshotsRequest request) => DoRequest<IGetModelSnapshotsRequest, GetModelSnapshotsResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IGetModelSnapshotsRequest"/>
		public Task<GetModelSnapshotsResponse> GetModelSnapshotsAsync(IGetModelSnapshotsRequest request, CancellationToken ct = default) => DoRequestAsync<IGetModelSnapshotsRequest, GetModelSnapshotsResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IGetOverallBucketsRequest"/>
		public GetOverallBucketsResponse GetOverallBuckets(Id jobId, Func<GetOverallBucketsDescriptor, IGetOverallBucketsRequest> selector = null) => GetOverallBuckets(selector.InvokeOrDefault(new GetOverallBucketsDescriptor(jobId: jobId)));
		///<inheritdoc cref = "IGetOverallBucketsRequest"/>
		public Task<GetOverallBucketsResponse> GetOverallBucketsAsync(Id jobId, Func<GetOverallBucketsDescriptor, IGetOverallBucketsRequest> selector = null, CancellationToken ct = default) => GetOverallBucketsAsync(selector.InvokeOrDefault(new GetOverallBucketsDescriptor(jobId: jobId)), ct);
		///<inheritdoc cref = "IGetOverallBucketsRequest"/>
		public GetOverallBucketsResponse GetOverallBuckets(IGetOverallBucketsRequest request) => DoRequest<IGetOverallBucketsRequest, GetOverallBucketsResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IGetOverallBucketsRequest"/>
		public Task<GetOverallBucketsResponse> GetOverallBucketsAsync(IGetOverallBucketsRequest request, CancellationToken ct = default) => DoRequestAsync<IGetOverallBucketsRequest, GetOverallBucketsResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IGetAnomalyRecordsRequest"/>
		public GetAnomalyRecordsResponse GetAnomalyRecords(Id jobId, Func<GetAnomalyRecordsDescriptor, IGetAnomalyRecordsRequest> selector = null) => GetAnomalyRecords(selector.InvokeOrDefault(new GetAnomalyRecordsDescriptor(jobId: jobId)));
		///<inheritdoc cref = "IGetAnomalyRecordsRequest"/>
		public Task<GetAnomalyRecordsResponse> GetAnomalyRecordsAsync(Id jobId, Func<GetAnomalyRecordsDescriptor, IGetAnomalyRecordsRequest> selector = null, CancellationToken ct = default) => GetAnomalyRecordsAsync(selector.InvokeOrDefault(new GetAnomalyRecordsDescriptor(jobId: jobId)), ct);
		///<inheritdoc cref = "IGetAnomalyRecordsRequest"/>
		public GetAnomalyRecordsResponse GetAnomalyRecords(IGetAnomalyRecordsRequest request) => DoRequest<IGetAnomalyRecordsRequest, GetAnomalyRecordsResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IGetAnomalyRecordsRequest"/>
		public Task<GetAnomalyRecordsResponse> GetAnomalyRecordsAsync(IGetAnomalyRecordsRequest request, CancellationToken ct = default) => DoRequestAsync<IGetAnomalyRecordsRequest, GetAnomalyRecordsResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IMachineLearningInfoRequest"/>
		public MachineLearningInfoResponse Info(Func<MachineLearningInfoDescriptor, IMachineLearningInfoRequest> selector = null) => Info(selector.InvokeOrDefault(new MachineLearningInfoDescriptor()));
		///<inheritdoc cref = "IMachineLearningInfoRequest"/>
		public Task<MachineLearningInfoResponse> InfoAsync(Func<MachineLearningInfoDescriptor, IMachineLearningInfoRequest> selector = null, CancellationToken ct = default) => InfoAsync(selector.InvokeOrDefault(new MachineLearningInfoDescriptor()), ct);
		///<inheritdoc cref = "IMachineLearningInfoRequest"/>
		public MachineLearningInfoResponse Info(IMachineLearningInfoRequest request) => DoRequest<IMachineLearningInfoRequest, MachineLearningInfoResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IMachineLearningInfoRequest"/>
		public Task<MachineLearningInfoResponse> InfoAsync(IMachineLearningInfoRequest request, CancellationToken ct = default) => DoRequestAsync<IMachineLearningInfoRequest, MachineLearningInfoResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IOpenJobRequest"/>
		public OpenJobResponse OpenJob(Id jobId, Func<OpenJobDescriptor, IOpenJobRequest> selector = null) => OpenJob(selector.InvokeOrDefault(new OpenJobDescriptor(jobId: jobId)));
		///<inheritdoc cref = "IOpenJobRequest"/>
		public Task<OpenJobResponse> OpenJobAsync(Id jobId, Func<OpenJobDescriptor, IOpenJobRequest> selector = null, CancellationToken ct = default) => OpenJobAsync(selector.InvokeOrDefault(new OpenJobDescriptor(jobId: jobId)), ct);
		///<inheritdoc cref = "IOpenJobRequest"/>
		public OpenJobResponse OpenJob(IOpenJobRequest request) => DoRequest<IOpenJobRequest, OpenJobResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IOpenJobRequest"/>
		public Task<OpenJobResponse> OpenJobAsync(IOpenJobRequest request, CancellationToken ct = default) => DoRequestAsync<IOpenJobRequest, OpenJobResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IPostCalendarEventsRequest"/>
		public PostCalendarEventsResponse PostCalendarEvents(Id calendarId, Func<PostCalendarEventsDescriptor, IPostCalendarEventsRequest> selector) => PostCalendarEvents(selector.InvokeOrDefault(new PostCalendarEventsDescriptor(calendarId: calendarId)));
		///<inheritdoc cref = "IPostCalendarEventsRequest"/>
		public Task<PostCalendarEventsResponse> PostCalendarEventsAsync(Id calendarId, Func<PostCalendarEventsDescriptor, IPostCalendarEventsRequest> selector, CancellationToken ct = default) => PostCalendarEventsAsync(selector.InvokeOrDefault(new PostCalendarEventsDescriptor(calendarId: calendarId)), ct);
		///<inheritdoc cref = "IPostCalendarEventsRequest"/>
		public PostCalendarEventsResponse PostCalendarEvents(IPostCalendarEventsRequest request) => DoRequest<IPostCalendarEventsRequest, PostCalendarEventsResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IPostCalendarEventsRequest"/>
		public Task<PostCalendarEventsResponse> PostCalendarEventsAsync(IPostCalendarEventsRequest request, CancellationToken ct = default) => DoRequestAsync<IPostCalendarEventsRequest, PostCalendarEventsResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IPostJobDataRequest"/>
		public PostJobDataResponse PostJobData(Id jobId, Func<PostJobDataDescriptor, IPostJobDataRequest> selector) => PostJobData(selector.InvokeOrDefault(new PostJobDataDescriptor(jobId: jobId)));
		///<inheritdoc cref = "IPostJobDataRequest"/>
		public Task<PostJobDataResponse> PostJobDataAsync(Id jobId, Func<PostJobDataDescriptor, IPostJobDataRequest> selector, CancellationToken ct = default) => PostJobDataAsync(selector.InvokeOrDefault(new PostJobDataDescriptor(jobId: jobId)), ct);
		///<inheritdoc cref = "IPostJobDataRequest"/>
		public PostJobDataResponse PostJobData(IPostJobDataRequest request) => DoRequest<IPostJobDataRequest, PostJobDataResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IPostJobDataRequest"/>
		public Task<PostJobDataResponse> PostJobDataAsync(IPostJobDataRequest request, CancellationToken ct = default) => DoRequestAsync<IPostJobDataRequest, PostJobDataResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IPreviewDatafeedRequest"/>
		public PreviewDatafeedResponse<TResult> PreviewDatafeed<TResult>(Id datafeedId, Func<PreviewDatafeedDescriptor, IPreviewDatafeedRequest> selector = null) => PreviewDatafeed<TResult>(selector.InvokeOrDefault(new PreviewDatafeedDescriptor(datafeedId: datafeedId)));
		///<inheritdoc cref = "IPreviewDatafeedRequest"/>
		public Task<PreviewDatafeedResponse<TResult>> PreviewDatafeedAsync<TResult>(Id datafeedId, Func<PreviewDatafeedDescriptor, IPreviewDatafeedRequest> selector = null, CancellationToken ct = default) => PreviewDatafeedAsync<TResult>(selector.InvokeOrDefault(new PreviewDatafeedDescriptor(datafeedId: datafeedId)), ct);
		///<inheritdoc cref = "IPreviewDatafeedRequest"/>
		public PreviewDatafeedResponse<TResult> PreviewDatafeed<TResult>(IPreviewDatafeedRequest request) => DoRequest<IPreviewDatafeedRequest, PreviewDatafeedResponse<TResult>>(request, request.RequestParameters);
		///<inheritdoc cref = "IPreviewDatafeedRequest"/>
		public Task<PreviewDatafeedResponse<TResult>> PreviewDatafeedAsync<TResult>(IPreviewDatafeedRequest request, CancellationToken ct = default) => DoRequestAsync<IPreviewDatafeedRequest, PreviewDatafeedResponse<TResult>>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IPutCalendarRequest"/>
		public PutCalendarResponse PutCalendar(Id calendarId, Func<PutCalendarDescriptor, IPutCalendarRequest> selector = null) => PutCalendar(selector.InvokeOrDefault(new PutCalendarDescriptor(calendarId: calendarId)));
		///<inheritdoc cref = "IPutCalendarRequest"/>
		public Task<PutCalendarResponse> PutCalendarAsync(Id calendarId, Func<PutCalendarDescriptor, IPutCalendarRequest> selector = null, CancellationToken ct = default) => PutCalendarAsync(selector.InvokeOrDefault(new PutCalendarDescriptor(calendarId: calendarId)), ct);
		///<inheritdoc cref = "IPutCalendarRequest"/>
		public PutCalendarResponse PutCalendar(IPutCalendarRequest request) => DoRequest<IPutCalendarRequest, PutCalendarResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IPutCalendarRequest"/>
		public Task<PutCalendarResponse> PutCalendarAsync(IPutCalendarRequest request, CancellationToken ct = default) => DoRequestAsync<IPutCalendarRequest, PutCalendarResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IPutCalendarJobRequest"/>
		public PutCalendarJobResponse PutCalendarJob(Id calendarId, Id jobId, Func<PutCalendarJobDescriptor, IPutCalendarJobRequest> selector = null) => PutCalendarJob(selector.InvokeOrDefault(new PutCalendarJobDescriptor(calendarId: calendarId, jobId: jobId)));
		///<inheritdoc cref = "IPutCalendarJobRequest"/>
		public Task<PutCalendarJobResponse> PutCalendarJobAsync(Id calendarId, Id jobId, Func<PutCalendarJobDescriptor, IPutCalendarJobRequest> selector = null, CancellationToken ct = default) => PutCalendarJobAsync(selector.InvokeOrDefault(new PutCalendarJobDescriptor(calendarId: calendarId, jobId: jobId)), ct);
		///<inheritdoc cref = "IPutCalendarJobRequest"/>
		public PutCalendarJobResponse PutCalendarJob(IPutCalendarJobRequest request) => DoRequest<IPutCalendarJobRequest, PutCalendarJobResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IPutCalendarJobRequest"/>
		public Task<PutCalendarJobResponse> PutCalendarJobAsync(IPutCalendarJobRequest request, CancellationToken ct = default) => DoRequestAsync<IPutCalendarJobRequest, PutCalendarJobResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IPutDatafeedRequest"/>
		public PutDatafeedResponse PutDatafeed<TDocument>(Id datafeedId, Func<PutDatafeedDescriptor<TDocument>, IPutDatafeedRequest> selector)
			where TDocument : class => PutDatafeed(selector.InvokeOrDefault(new PutDatafeedDescriptor<TDocument>(datafeedId: datafeedId)));
		///<inheritdoc cref = "IPutDatafeedRequest"/>
		public Task<PutDatafeedResponse> PutDatafeedAsync<TDocument>(Id datafeedId, Func<PutDatafeedDescriptor<TDocument>, IPutDatafeedRequest> selector, CancellationToken ct = default)
			where TDocument : class => PutDatafeedAsync(selector.InvokeOrDefault(new PutDatafeedDescriptor<TDocument>(datafeedId: datafeedId)), ct);
		///<inheritdoc cref = "IPutDatafeedRequest"/>
		public PutDatafeedResponse PutDatafeed(IPutDatafeedRequest request) => DoRequest<IPutDatafeedRequest, PutDatafeedResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IPutDatafeedRequest"/>
		public Task<PutDatafeedResponse> PutDatafeedAsync(IPutDatafeedRequest request, CancellationToken ct = default) => DoRequestAsync<IPutDatafeedRequest, PutDatafeedResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IPutFilterRequest"/>
		public PutFilterResponse PutFilter(Id filterId, Func<PutFilterDescriptor, IPutFilterRequest> selector) => PutFilter(selector.InvokeOrDefault(new PutFilterDescriptor(filterId: filterId)));
		///<inheritdoc cref = "IPutFilterRequest"/>
		public Task<PutFilterResponse> PutFilterAsync(Id filterId, Func<PutFilterDescriptor, IPutFilterRequest> selector, CancellationToken ct = default) => PutFilterAsync(selector.InvokeOrDefault(new PutFilterDescriptor(filterId: filterId)), ct);
		///<inheritdoc cref = "IPutFilterRequest"/>
		public PutFilterResponse PutFilter(IPutFilterRequest request) => DoRequest<IPutFilterRequest, PutFilterResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IPutFilterRequest"/>
		public Task<PutFilterResponse> PutFilterAsync(IPutFilterRequest request, CancellationToken ct = default) => DoRequestAsync<IPutFilterRequest, PutFilterResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IPutJobRequest"/>
		public PutJobResponse PutJob<TDocument>(Id jobId, Func<PutJobDescriptor<TDocument>, IPutJobRequest> selector)
			where TDocument : class => PutJob(selector.InvokeOrDefault(new PutJobDescriptor<TDocument>(jobId: jobId)));
		///<inheritdoc cref = "IPutJobRequest"/>
		public Task<PutJobResponse> PutJobAsync<TDocument>(Id jobId, Func<PutJobDescriptor<TDocument>, IPutJobRequest> selector, CancellationToken ct = default)
			where TDocument : class => PutJobAsync(selector.InvokeOrDefault(new PutJobDescriptor<TDocument>(jobId: jobId)), ct);
		///<inheritdoc cref = "IPutJobRequest"/>
		public PutJobResponse PutJob(IPutJobRequest request) => DoRequest<IPutJobRequest, PutJobResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IPutJobRequest"/>
		public Task<PutJobResponse> PutJobAsync(IPutJobRequest request, CancellationToken ct = default) => DoRequestAsync<IPutJobRequest, PutJobResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IRevertModelSnapshotRequest"/>
		public RevertModelSnapshotResponse RevertModelSnapshot(Id jobId, Id snapshotId, Func<RevertModelSnapshotDescriptor, IRevertModelSnapshotRequest> selector = null) => RevertModelSnapshot(selector.InvokeOrDefault(new RevertModelSnapshotDescriptor(jobId: jobId, snapshotId: snapshotId)));
		///<inheritdoc cref = "IRevertModelSnapshotRequest"/>
		public Task<RevertModelSnapshotResponse> RevertModelSnapshotAsync(Id jobId, Id snapshotId, Func<RevertModelSnapshotDescriptor, IRevertModelSnapshotRequest> selector = null, CancellationToken ct = default) => RevertModelSnapshotAsync(selector.InvokeOrDefault(new RevertModelSnapshotDescriptor(jobId: jobId, snapshotId: snapshotId)), ct);
		///<inheritdoc cref = "IRevertModelSnapshotRequest"/>
		public RevertModelSnapshotResponse RevertModelSnapshot(IRevertModelSnapshotRequest request) => DoRequest<IRevertModelSnapshotRequest, RevertModelSnapshotResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IRevertModelSnapshotRequest"/>
		public Task<RevertModelSnapshotResponse> RevertModelSnapshotAsync(IRevertModelSnapshotRequest request, CancellationToken ct = default) => DoRequestAsync<IRevertModelSnapshotRequest, RevertModelSnapshotResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IStartDatafeedRequest"/>
		public StartDatafeedResponse StartDatafeed(Id datafeedId, Func<StartDatafeedDescriptor, IStartDatafeedRequest> selector = null) => StartDatafeed(selector.InvokeOrDefault(new StartDatafeedDescriptor(datafeedId: datafeedId)));
		///<inheritdoc cref = "IStartDatafeedRequest"/>
		public Task<StartDatafeedResponse> StartDatafeedAsync(Id datafeedId, Func<StartDatafeedDescriptor, IStartDatafeedRequest> selector = null, CancellationToken ct = default) => StartDatafeedAsync(selector.InvokeOrDefault(new StartDatafeedDescriptor(datafeedId: datafeedId)), ct);
		///<inheritdoc cref = "IStartDatafeedRequest"/>
		public StartDatafeedResponse StartDatafeed(IStartDatafeedRequest request) => DoRequest<IStartDatafeedRequest, StartDatafeedResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IStartDatafeedRequest"/>
		public Task<StartDatafeedResponse> StartDatafeedAsync(IStartDatafeedRequest request, CancellationToken ct = default) => DoRequestAsync<IStartDatafeedRequest, StartDatafeedResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IStopDatafeedRequest"/>
		public StopDatafeedResponse StopDatafeed(Id datafeedId, Func<StopDatafeedDescriptor, IStopDatafeedRequest> selector = null) => StopDatafeed(selector.InvokeOrDefault(new StopDatafeedDescriptor(datafeedId: datafeedId)));
		///<inheritdoc cref = "IStopDatafeedRequest"/>
		public Task<StopDatafeedResponse> StopDatafeedAsync(Id datafeedId, Func<StopDatafeedDescriptor, IStopDatafeedRequest> selector = null, CancellationToken ct = default) => StopDatafeedAsync(selector.InvokeOrDefault(new StopDatafeedDescriptor(datafeedId: datafeedId)), ct);
		///<inheritdoc cref = "IStopDatafeedRequest"/>
		public StopDatafeedResponse StopDatafeed(IStopDatafeedRequest request) => DoRequest<IStopDatafeedRequest, StopDatafeedResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IStopDatafeedRequest"/>
		public Task<StopDatafeedResponse> StopDatafeedAsync(IStopDatafeedRequest request, CancellationToken ct = default) => DoRequestAsync<IStopDatafeedRequest, StopDatafeedResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IUpdateDatafeedRequest"/>
		public UpdateDatafeedResponse UpdateDatafeed<TDocument>(Id datafeedId, Func<UpdateDatafeedDescriptor<TDocument>, IUpdateDatafeedRequest> selector)
			where TDocument : class => UpdateDatafeed(selector.InvokeOrDefault(new UpdateDatafeedDescriptor<TDocument>(datafeedId: datafeedId)));
		///<inheritdoc cref = "IUpdateDatafeedRequest"/>
		public Task<UpdateDatafeedResponse> UpdateDatafeedAsync<TDocument>(Id datafeedId, Func<UpdateDatafeedDescriptor<TDocument>, IUpdateDatafeedRequest> selector, CancellationToken ct = default)
			where TDocument : class => UpdateDatafeedAsync(selector.InvokeOrDefault(new UpdateDatafeedDescriptor<TDocument>(datafeedId: datafeedId)), ct);
		///<inheritdoc cref = "IUpdateDatafeedRequest"/>
		public UpdateDatafeedResponse UpdateDatafeed(IUpdateDatafeedRequest request) => DoRequest<IUpdateDatafeedRequest, UpdateDatafeedResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IUpdateDatafeedRequest"/>
		public Task<UpdateDatafeedResponse> UpdateDatafeedAsync(IUpdateDatafeedRequest request, CancellationToken ct = default) => DoRequestAsync<IUpdateDatafeedRequest, UpdateDatafeedResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IUpdateFilterRequest"/>
		public UpdateFilterResponse UpdateFilter(Id filterId, Func<UpdateFilterDescriptor, IUpdateFilterRequest> selector) => UpdateFilter(selector.InvokeOrDefault(new UpdateFilterDescriptor(filterId: filterId)));
		///<inheritdoc cref = "IUpdateFilterRequest"/>
		public Task<UpdateFilterResponse> UpdateFilterAsync(Id filterId, Func<UpdateFilterDescriptor, IUpdateFilterRequest> selector, CancellationToken ct = default) => UpdateFilterAsync(selector.InvokeOrDefault(new UpdateFilterDescriptor(filterId: filterId)), ct);
		///<inheritdoc cref = "IUpdateFilterRequest"/>
		public UpdateFilterResponse UpdateFilter(IUpdateFilterRequest request) => DoRequest<IUpdateFilterRequest, UpdateFilterResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IUpdateFilterRequest"/>
		public Task<UpdateFilterResponse> UpdateFilterAsync(IUpdateFilterRequest request, CancellationToken ct = default) => DoRequestAsync<IUpdateFilterRequest, UpdateFilterResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IUpdateJobRequest"/>
		public UpdateJobResponse UpdateJob<TDocument>(Id jobId, Func<UpdateJobDescriptor<TDocument>, IUpdateJobRequest> selector)
			where TDocument : class => UpdateJob(selector.InvokeOrDefault(new UpdateJobDescriptor<TDocument>(jobId: jobId)));
		///<inheritdoc cref = "IUpdateJobRequest"/>
		public Task<UpdateJobResponse> UpdateJobAsync<TDocument>(Id jobId, Func<UpdateJobDescriptor<TDocument>, IUpdateJobRequest> selector, CancellationToken ct = default)
			where TDocument : class => UpdateJobAsync(selector.InvokeOrDefault(new UpdateJobDescriptor<TDocument>(jobId: jobId)), ct);
		///<inheritdoc cref = "IUpdateJobRequest"/>
		public UpdateJobResponse UpdateJob(IUpdateJobRequest request) => DoRequest<IUpdateJobRequest, UpdateJobResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IUpdateJobRequest"/>
		public Task<UpdateJobResponse> UpdateJobAsync(IUpdateJobRequest request, CancellationToken ct = default) => DoRequestAsync<IUpdateJobRequest, UpdateJobResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IUpdateModelSnapshotRequest"/>
		public UpdateModelSnapshotResponse UpdateModelSnapshot(Id jobId, Id snapshotId, Func<UpdateModelSnapshotDescriptor, IUpdateModelSnapshotRequest> selector) => UpdateModelSnapshot(selector.InvokeOrDefault(new UpdateModelSnapshotDescriptor(jobId: jobId, snapshotId: snapshotId)));
		///<inheritdoc cref = "IUpdateModelSnapshotRequest"/>
		public Task<UpdateModelSnapshotResponse> UpdateModelSnapshotAsync(Id jobId, Id snapshotId, Func<UpdateModelSnapshotDescriptor, IUpdateModelSnapshotRequest> selector, CancellationToken ct = default) => UpdateModelSnapshotAsync(selector.InvokeOrDefault(new UpdateModelSnapshotDescriptor(jobId: jobId, snapshotId: snapshotId)), ct);
		///<inheritdoc cref = "IUpdateModelSnapshotRequest"/>
		public UpdateModelSnapshotResponse UpdateModelSnapshot(IUpdateModelSnapshotRequest request) => DoRequest<IUpdateModelSnapshotRequest, UpdateModelSnapshotResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IUpdateModelSnapshotRequest"/>
		public Task<UpdateModelSnapshotResponse> UpdateModelSnapshotAsync(IUpdateModelSnapshotRequest request, CancellationToken ct = default) => DoRequestAsync<IUpdateModelSnapshotRequest, UpdateModelSnapshotResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IValidateJobRequest"/>
		public ValidateJobResponse ValidateJob<TDocument>(Func<ValidateJobDescriptor<TDocument>, IValidateJobRequest> selector)
			where TDocument : class => ValidateJob(selector.InvokeOrDefault(new ValidateJobDescriptor<TDocument>()));
		///<inheritdoc cref = "IValidateJobRequest"/>
		public Task<ValidateJobResponse> ValidateJobAsync<TDocument>(Func<ValidateJobDescriptor<TDocument>, IValidateJobRequest> selector, CancellationToken ct = default)
			where TDocument : class => ValidateJobAsync(selector.InvokeOrDefault(new ValidateJobDescriptor<TDocument>()), ct);
		///<inheritdoc cref = "IValidateJobRequest"/>
		public ValidateJobResponse ValidateJob(IValidateJobRequest request) => DoRequest<IValidateJobRequest, ValidateJobResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IValidateJobRequest"/>
		public Task<ValidateJobResponse> ValidateJobAsync(IValidateJobRequest request, CancellationToken ct = default) => DoRequestAsync<IValidateJobRequest, ValidateJobResponse>(request, request.RequestParameters, ct);
		///<inheritdoc cref = "IValidateDetectorRequest"/>
		public ValidateDetectorResponse ValidateDetector<TDocument>(Func<ValidateDetectorDescriptor<TDocument>, IValidateDetectorRequest> selector)
			where TDocument : class => ValidateDetector(selector.InvokeOrDefault(new ValidateDetectorDescriptor<TDocument>()));
		///<inheritdoc cref = "IValidateDetectorRequest"/>
		public Task<ValidateDetectorResponse> ValidateDetectorAsync<TDocument>(Func<ValidateDetectorDescriptor<TDocument>, IValidateDetectorRequest> selector, CancellationToken ct = default)
			where TDocument : class => ValidateDetectorAsync(selector.InvokeOrDefault(new ValidateDetectorDescriptor<TDocument>()), ct);
		///<inheritdoc cref = "IValidateDetectorRequest"/>
		public ValidateDetectorResponse ValidateDetector(IValidateDetectorRequest request) => DoRequest<IValidateDetectorRequest, ValidateDetectorResponse>(request, request.RequestParameters);
		///<inheritdoc cref = "IValidateDetectorRequest"/>
		public Task<ValidateDetectorResponse> ValidateDetectorAsync(IValidateDetectorRequest request, CancellationToken ct = default) => DoRequestAsync<IValidateDetectorRequest, ValidateDetectorResponse>(request, request.RequestParameters, ct);
	}
}