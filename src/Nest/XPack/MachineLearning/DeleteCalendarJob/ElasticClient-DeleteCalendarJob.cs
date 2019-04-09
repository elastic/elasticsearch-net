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
		IDeleteCalendarJobResponse DeleteCalendarJob(Id calendarId, Id jobId, Func<DeleteCalendarJobDescriptor, IDeleteCalendarJobRequest> selector = null);

		/// <inheritdoc cref="DeleteCalendarJob(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarJobDescriptor,Nest.IDeleteCalendarJobRequest})" />
		IDeleteCalendarJobResponse DeleteCalendarJob(IDeleteCalendarJobRequest request);

		/// <inheritdoc cref="DeleteCalendarJob(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarJobDescriptor,Nest.IDeleteCalendarJobRequest})" />
		Task<IDeleteCalendarJobResponse> DeleteCalendarJobAsync(Id calendarId, Id jobId, Func<DeleteCalendarJobDescriptor, IDeleteCalendarJobRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="DeleteCalendarJob(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarJobDescriptor,Nest.IDeleteCalendarJobRequest})" />
		Task<IDeleteCalendarJobResponse> DeleteCalendarJobAsync(IDeleteCalendarJobRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteCalendarJobResponse DeleteCalendarJob(Id calendarId, Id jobId, Func<DeleteCalendarJobDescriptor, IDeleteCalendarJobRequest> selector = null) =>
			DeleteCalendarJob(selector.InvokeOrDefault(new DeleteCalendarJobDescriptor(calendarId, jobId)));

		/// <inheritdoc />
		public IDeleteCalendarJobResponse DeleteCalendarJob(IDeleteCalendarJobRequest request) =>
			Dispatch2<IDeleteCalendarJobRequest, DeleteCalendarJobResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IDeleteCalendarJobResponse> DeleteCalendarJobAsync(
			Id calendarId,
			Id jobId,
			Func<DeleteCalendarJobDescriptor, IDeleteCalendarJobRequest> selector = null,
			CancellationToken ct = default
		) => DeleteCalendarJobAsync(selector.InvokeOrDefault(new DeleteCalendarJobDescriptor(calendarId, jobId)), ct);

		/// <inheritdoc />
		public Task<IDeleteCalendarJobResponse> DeleteCalendarJobAsync(IDeleteCalendarJobRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IDeleteCalendarJobRequest, IDeleteCalendarJobResponse, DeleteCalendarJobResponse>(request, request.RequestParameters, ct);
	}
}
