using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.DeleteCalendarEvent(), please update this usage.")]
		public static DeleteCalendarEventResponse DeleteCalendarEvent(this IElasticClient client, Id calendarId, Id eventId,
			Func<DeleteCalendarEventDescriptor, IDeleteCalendarEventRequest> selector = null
		)
			=> client.MachineLearning.DeleteCalendarEvent(calendarId, eventId, selector);

		[Obsolete("Moved to client.MachineLearning.DeleteCalendarEvent(), please update this usage.")]
		public static DeleteCalendarEventResponse DeleteCalendarEvent(this IElasticClient client, IDeleteCalendarEventRequest request)
			=> client.MachineLearning.DeleteCalendarEvent(request);

		[Obsolete("Moved to client.MachineLearning.DeleteCalendarEventAsync(), please update this usage.")]
		public static Task<DeleteCalendarEventResponse> DeleteCalendarEventAsync(this IElasticClient client, Id calendarId, Id eventId,
			Func<DeleteCalendarEventDescriptor, IDeleteCalendarEventRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteCalendarEventAsync(calendarId, eventId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.DeleteCalendarEventAsync(), please update this usage.")]
		public static Task<DeleteCalendarEventResponse> DeleteCalendarEventAsync(this IElasticClient client, IDeleteCalendarEventRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteCalendarEventAsync(request, ct);
	}
}
