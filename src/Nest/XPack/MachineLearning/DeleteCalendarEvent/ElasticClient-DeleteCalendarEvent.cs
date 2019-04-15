using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Deletes scheduled events from a machine learning calendar.
		/// </summary>
		DeleteCalendarEventResponse DeleteCalendarEvent(Id calendarId, Id eventId, Func<DeleteCalendarEventDescriptor, IDeleteCalendarEventRequest> selector = null);

		/// <inheritdoc cref="DeleteCalendarEvent(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarEventDescriptor,Nest.IDeleteCalendarEventRequest})" />
		DeleteCalendarEventResponse DeleteCalendarEvent(IDeleteCalendarEventRequest request);

		/// <inheritdoc cref="DeleteCalendarEvent(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarEventDescriptor,Nest.IDeleteCalendarEventRequest})" />
		Task<DeleteCalendarEventResponse> DeleteCalendarEventAsync(Id calendarId, Id eventId, Func<DeleteCalendarEventDescriptor, IDeleteCalendarEventRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="DeleteCalendarEvent(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarEventDescriptor,Nest.IDeleteCalendarEventRequest})" />
		Task<DeleteCalendarEventResponse> DeleteCalendarEventAsync(IDeleteCalendarEventRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeleteCalendarEventResponse DeleteCalendarEvent(Id calendarId, Id eventId, Func<DeleteCalendarEventDescriptor, IDeleteCalendarEventRequest> selector = null) =>
			DeleteCalendarEvent(selector.InvokeOrDefault(new DeleteCalendarEventDescriptor(calendarId, eventId)));

		/// <inheritdoc />
		public DeleteCalendarEventResponse DeleteCalendarEvent(IDeleteCalendarEventRequest request) =>
			DoRequest<IDeleteCalendarEventRequest, DeleteCalendarEventResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<DeleteCalendarEventResponse> DeleteCalendarEventAsync(
			Id calendarId,
			Id eventId,
			Func<DeleteCalendarEventDescriptor, IDeleteCalendarEventRequest> selector = null,
			CancellationToken ct = default
		) =>
			DeleteCalendarEventAsync(selector.InvokeOrDefault(new DeleteCalendarEventDescriptor(calendarId, eventId)), ct);

		/// <inheritdoc />
		public Task<DeleteCalendarEventResponse> DeleteCalendarEventAsync(IDeleteCalendarEventRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeleteCalendarEventRequest, DeleteCalendarEventResponse, DeleteCalendarEventResponse>(request, request.RequestParameters, ct);
	}
}
