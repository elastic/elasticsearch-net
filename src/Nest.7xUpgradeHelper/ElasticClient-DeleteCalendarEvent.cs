using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Deletes scheduled events from a machine learning calendar.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteCalendarEventResponse DeleteCalendarEvent(this IElasticClient client, Id calendarId, Id eventId,
			Func<DeleteCalendarEventDescriptor, IDeleteCalendarEventRequest> selector = null
		)
			=> client.MachineLearning.DeleteCalendarEvent(calendarId, eventId, selector);

		/// <inheritdoc
		///     cref="DeleteCalendarEvent(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarEventDescriptor,Nest.IDeleteCalendarEventRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteCalendarEventResponse DeleteCalendarEvent(this IElasticClient client, IDeleteCalendarEventRequest request)
			=> client.MachineLearning.DeleteCalendarEvent(request);

		/// <inheritdoc
		///     cref="DeleteCalendarEvent(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarEventDescriptor,Nest.IDeleteCalendarEventRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteCalendarEventResponse> DeleteCalendarEventAsync(this IElasticClient client, Id calendarId, Id eventId,
			Func<DeleteCalendarEventDescriptor, IDeleteCalendarEventRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteCalendarEventAsync(calendarId, eventId, selector, ct);

		/// <inheritdoc
		///     cref="DeleteCalendarEvent(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarEventDescriptor,Nest.IDeleteCalendarEventRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteCalendarEventResponse> DeleteCalendarEventAsync(this IElasticClient client, IDeleteCalendarEventRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteCalendarEventAsync(request, ct);
	}
}
