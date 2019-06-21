using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.GetCalendarEvents(), please update this usage.")]
		public static GetCalendarEventsResponse GetCalendarEvents(this IElasticClient client, Id calendarId,
			Func<GetCalendarEventsDescriptor, IGetCalendarEventsRequest> selector = null
		)
			=> client.MachineLearning.GetCalendarEvents(calendarId, selector);

		[Obsolete("Moved to client.MachineLearning.GetCalendarEvents(), please update this usage.")]
		public static GetCalendarEventsResponse GetCalendarEvents(this IElasticClient client, IGetCalendarEventsRequest request)
			=> client.MachineLearning.GetCalendarEvents(request);

		[Obsolete("Moved to client.MachineLearning.GetCalendarEventsAsync(), please update this usage.")]
		public static Task<GetCalendarEventsResponse> GetCalendarEventsAsync(this IElasticClient client, Id calendarId,
			Func<GetCalendarEventsDescriptor, IGetCalendarEventsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetCalendarEventsAsync(calendarId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.GetCalendarEventsAsync(), please update this usage.")]
		public static Task<GetCalendarEventsResponse> GetCalendarEventsAsync(this IElasticClient client, IGetCalendarEventsRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetCalendarEventsAsync(request, ct);
	}
}
