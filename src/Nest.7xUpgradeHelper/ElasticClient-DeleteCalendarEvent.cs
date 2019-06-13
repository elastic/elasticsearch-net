using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Deletes scheduled events from a machine learning calendar.
		/// </summary>
		public static DeleteCalendarEventResponse DeleteCalendarEvent(this IElasticClient client,Id calendarId, Id eventId, Func<DeleteCalendarEventDescriptor, IDeleteCalendarEventRequest> selector = null);

		/// <inheritdoc cref="DeleteCalendarEvent(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarEventDescriptor,Nest.IDeleteCalendarEventRequest})" />
		public static DeleteCalendarEventResponse DeleteCalendarEvent(this IElasticClient client,IDeleteCalendarEventRequest request);

		/// <inheritdoc cref="DeleteCalendarEvent(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarEventDescriptor,Nest.IDeleteCalendarEventRequest})" />
		public static Task<DeleteCalendarEventResponse> DeleteCalendarEventAsync(this IElasticClient client,Id calendarId, Id eventId, Func<DeleteCalendarEventDescriptor, IDeleteCalendarEventRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="DeleteCalendarEvent(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarEventDescriptor,Nest.IDeleteCalendarEventRequest})" />
		public static Task<DeleteCalendarEventResponse> DeleteCalendarEventAsync(this IElasticClient client,IDeleteCalendarEventRequest request, CancellationToken ct = default);
	}

}
