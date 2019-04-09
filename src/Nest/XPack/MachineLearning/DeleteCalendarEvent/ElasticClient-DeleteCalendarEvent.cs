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
		IDeleteCalendarEventResponse DeleteCalendarEvent(Id calendarId, Id eventId, Func<DeleteCalendarEventDescriptor, IDeleteCalendarEventRequest> selector = null);

		/// <inheritdoc cref="DeleteCalendarEvent(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarEventDescriptor,Nest.IDeleteCalendarEventRequest})" />
		IDeleteCalendarEventResponse DeleteCalendarEvent(IDeleteCalendarEventRequest request);

		/// <inheritdoc cref="DeleteCalendarEvent(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarEventDescriptor,Nest.IDeleteCalendarEventRequest})" />
		Task<IDeleteCalendarEventResponse> DeleteCalendarEventAsync(Id calendarId, Id eventId, Func<DeleteCalendarEventDescriptor, IDeleteCalendarEventRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="DeleteCalendarEvent(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarEventDescriptor,Nest.IDeleteCalendarEventRequest})" />
		Task<IDeleteCalendarEventResponse> DeleteCalendarEventAsync(IDeleteCalendarEventRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteCalendarEventResponse DeleteCalendarEvent(Id calendarId, Id eventId, Func<DeleteCalendarEventDescriptor, IDeleteCalendarEventRequest> selector = null) =>
			DeleteCalendarEvent(selector.InvokeOrDefault(new DeleteCalendarEventDescriptor(calendarId, eventId)));

		/// <inheritdoc />
		public IDeleteCalendarEventResponse DeleteCalendarEvent(IDeleteCalendarEventRequest request) =>
			Dispatch2<IDeleteCalendarEventRequest, DeleteCalendarEventResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IDeleteCalendarEventResponse> DeleteCalendarEventAsync(
			Id calendarId,
			Id eventId,
			Func<DeleteCalendarEventDescriptor, IDeleteCalendarEventRequest> selector = null,
			CancellationToken ct = default
		) =>
			DeleteCalendarEventAsync(selector.InvokeOrDefault(new DeleteCalendarEventDescriptor(calendarId, eventId)), ct);

		/// <inheritdoc />
		public Task<IDeleteCalendarEventResponse> DeleteCalendarEventAsync(IDeleteCalendarEventRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IDeleteCalendarEventRequest, IDeleteCalendarEventResponse, DeleteCalendarEventResponse>(request, request.RequestParameters, ct);
	}
}
