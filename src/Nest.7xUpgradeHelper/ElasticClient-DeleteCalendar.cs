using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Deletes a machine learning calendar.
		/// Removes all scheduled events from the calendar then deletes the calendar.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteCalendarResponse DeleteCalendar(this IElasticClient client, Id calendarId,
			Func<DeleteCalendarDescriptor, IDeleteCalendarRequest> selector = null
		)
			=> client.MachineLearning.DeleteCalendar(calendarId, selector);

		/// <inheritdoc cref="DeleteCalendar(Nest.Id,System.Func{Nest.DeleteCalendarDescriptor,Nest.IDeleteCalendarRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteCalendarResponse DeleteCalendar(this IElasticClient client, IDeleteCalendarRequest request)
			=> client.MachineLearning.DeleteCalendar(request);

		/// <inheritdoc cref="DeleteCalendar(Nest.Id,System.Func{Nest.DeleteCalendarDescriptor,Nest.IDeleteCalendarRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteCalendarResponse> DeleteCalendarAsync(this IElasticClient client, Id calendarId,
			Func<DeleteCalendarDescriptor, IDeleteCalendarRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteCalendarAsync(calendarId, selector, ct);

		/// <inheritdoc cref="DeleteCalendar(Nest.Id,System.Func{Nest.DeleteCalendarDescriptor,Nest.IDeleteCalendarRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteCalendarResponse> DeleteCalendarAsync(this IElasticClient client, IDeleteCalendarRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteCalendarAsync(request, ct);
	}
}
