using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Creates a machine learning calendar event.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PostCalendarEventsResponse PostCalendarEvents(this IElasticClient client, Id calendarId,
			Func<PostCalendarEventsDescriptor, IPostCalendarEventsRequest> selector = null
		)
			=> client.MachineLearning.PostCalendarEvents(calendarId, selector);

		/// <inheritdoc
		///     cref="PostCalendarEvents(Nest.Id,System.Func{Nest.PostCalendarEventsDescriptor,Nest.IPostCalendarEventsRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PostCalendarEventsResponse PostCalendarEvents(this IElasticClient client, IPostCalendarEventsRequest request)
			=> client.MachineLearning.PostCalendarEvents(request);

		/// <inheritdoc
		///     cref="PostCalendarEvents(Nest.Id,System.Func{Nest.PostCalendarEventsDescriptor,Nest.IPostCalendarEventsRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PostCalendarEventsResponse> PostCalendarEventsAsync(this IElasticClient client, Id calendarId,
			Func<PostCalendarEventsDescriptor, IPostCalendarEventsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.PostCalendarEventsAsync(calendarId, selector, ct);

		/// <inheritdoc
		///     cref="PostCalendarEvents(Nest.Id,System.Func{Nest.PostCalendarEventsDescriptor,Nest.IPostCalendarEventsRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PostCalendarEventsResponse> PostCalendarEventsAsync(this IElasticClient client, IPostCalendarEventsRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.PostCalendarEventsAsync(request, ct);
	}
}
