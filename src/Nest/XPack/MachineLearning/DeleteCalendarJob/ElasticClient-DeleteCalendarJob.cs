using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Deletes a machine learning calendar.
		/// Removes all scheduled events from the calendar then deletes the calendar.
		/// </summary>
		DeleteCalendarJobResponse DeleteCalendarJob(Id calendarId, Id jobId, Func<DeleteCalendarJobDescriptor, IDeleteCalendarJobRequest> selector = null);

		/// <inheritdoc cref="DeleteCalendarJob(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarJobDescriptor,Nest.IDeleteCalendarJobRequest})" />
		DeleteCalendarJobResponse DeleteCalendarJob(IDeleteCalendarJobRequest request);

		/// <inheritdoc cref="DeleteCalendarJob(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarJobDescriptor,Nest.IDeleteCalendarJobRequest})" />
		Task<DeleteCalendarJobResponse> DeleteCalendarJobAsync(Id calendarId, Id jobId, Func<DeleteCalendarJobDescriptor, IDeleteCalendarJobRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="DeleteCalendarJob(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarJobDescriptor,Nest.IDeleteCalendarJobRequest})" />
		Task<DeleteCalendarJobResponse> DeleteCalendarJobAsync(IDeleteCalendarJobRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeleteCalendarJobResponse DeleteCalendarJob(Id calendarId, Id jobId, Func<DeleteCalendarJobDescriptor, IDeleteCalendarJobRequest> selector = null) =>
			DeleteCalendarJob(selector.InvokeOrDefault(new DeleteCalendarJobDescriptor(calendarId, jobId)));

		/// <inheritdoc />
		public DeleteCalendarJobResponse DeleteCalendarJob(IDeleteCalendarJobRequest request) =>
			DoRequest<IDeleteCalendarJobRequest, DeleteCalendarJobResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<DeleteCalendarJobResponse> DeleteCalendarJobAsync(
			Id calendarId,
			Id jobId,
			Func<DeleteCalendarJobDescriptor, IDeleteCalendarJobRequest> selector = null,
			CancellationToken ct = default
		) => DeleteCalendarJobAsync(selector.InvokeOrDefault(new DeleteCalendarJobDescriptor(calendarId, jobId)), ct);

		/// <inheritdoc />
		public Task<DeleteCalendarJobResponse> DeleteCalendarJobAsync(IDeleteCalendarJobRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteCalendarJobRequest, DeleteCalendarJobResponse>(request, request.RequestParameters, ct);
	}
}
