using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieve information about the scheduled events in calendars.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetCalendarEventsResponse GetCalendarEvents(this IElasticClient client, Id calendarId,
			Func<GetCalendarEventsDescriptor, IGetCalendarEventsRequest> selector = null
		)
			=> client.MachineLearning.GetCalendarEvents(calendarId, selector);

		/// <inheritdoc
		///     cref="GetCalendarEvents(Nest.Id,System.Func{Nest.GetCalendarEventsDescriptor,Nest.IGetCalendarEventsRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetCalendarEventsResponse GetCalendarEvents(this IElasticClient client, IGetCalendarEventsRequest request)
			=> client.MachineLearning.GetCalendarEvents(request);

		/// <inheritdoc
		///     cref="GetCalendarEvents(Nest.Id,System.Func{Nest.GetCalendarEventsDescriptor,Nest.IGetCalendarEventsRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetCalendarEventsResponse> GetCalendarEventsAsync(this IElasticClient client, Id calendarId,
			Func<GetCalendarEventsDescriptor, IGetCalendarEventsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetCalendarEventsAsync(calendarId, selector, ct);

		/// <inheritdoc
		///     cref="GetCalendarEvents(Nest.Id,System.Func{Nest.GetCalendarEventsDescriptor,Nest.IGetCalendarEventsRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetCalendarEventsResponse> GetCalendarEventsAsync(this IElasticClient client, IGetCalendarEventsRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetCalendarEventsAsync(request, ct);
	}
}
