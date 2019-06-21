using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.PostCalendarEvents(), please update this usage.")]
		public static PostCalendarEventsResponse PostCalendarEvents(this IElasticClient client, Id calendarId,
			Func<PostCalendarEventsDescriptor, IPostCalendarEventsRequest> selector = null
		)
			=> client.MachineLearning.PostCalendarEvents(calendarId, selector);

		[Obsolete("Moved to client.MachineLearning.PostCalendarEvents(), please update this usage.")]
		public static PostCalendarEventsResponse PostCalendarEvents(this IElasticClient client, IPostCalendarEventsRequest request)
			=> client.MachineLearning.PostCalendarEvents(request);

		[Obsolete("Moved to client.MachineLearning.PostCalendarEventsAsync(), please update this usage.")]
		public static Task<PostCalendarEventsResponse> PostCalendarEventsAsync(this IElasticClient client, Id calendarId,
			Func<PostCalendarEventsDescriptor, IPostCalendarEventsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.PostCalendarEventsAsync(calendarId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.PostCalendarEventsAsync(), please update this usage.")]
		public static Task<PostCalendarEventsResponse> PostCalendarEventsAsync(this IElasticClient client, IPostCalendarEventsRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.PostCalendarEventsAsync(request, ct);
	}
}
