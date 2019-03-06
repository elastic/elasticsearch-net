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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="DeleteCalendarEvent(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarEventDescriptor,Nest.IDeleteCalendarEventRequest})" />
		Task<IDeleteCalendarEventResponse> DeleteCalendarEventAsync(IDeleteCalendarEventRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteCalendarEventResponse DeleteCalendarEvent(Id calendarId, Id eventId, Func<DeleteCalendarEventDescriptor, IDeleteCalendarEventRequest> selector = null) =>
			DeleteCalendarEvent(selector.InvokeOrDefault(new DeleteCalendarEventDescriptor(calendarId, eventId)));

		/// <inheritdoc />
		public IDeleteCalendarEventResponse DeleteCalendarEvent(IDeleteCalendarEventRequest request) =>
			Dispatcher.Dispatch<IDeleteCalendarEventRequest, DeleteCalendarEventRequestParameters, DeleteCalendarEventResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackMlDeleteCalendarEventDispatch<DeleteCalendarEventResponse>(p)
			);

		/// <inheritdoc />
		public Task<IDeleteCalendarEventResponse> DeleteCalendarEventAsync(Id calendarId, Id eventId, Func<DeleteCalendarEventDescriptor, IDeleteCalendarEventRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DeleteCalendarEventAsync(selector.InvokeOrDefault(new DeleteCalendarEventDescriptor(calendarId, eventId)), cancellationToken);

		/// <inheritdoc />
		public Task<IDeleteCalendarEventResponse> DeleteCalendarEventAsync(IDeleteCalendarEventRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IDeleteCalendarEventRequest, DeleteCalendarEventRequestParameters, DeleteCalendarEventResponse, IDeleteCalendarEventResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.XpackMlDeleteCalendarEventDispatchAsync<DeleteCalendarEventResponse>(p, c)
			);
	}
}
