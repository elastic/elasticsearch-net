using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Creates a machine learning calendar.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PutCalendarResponse PutCalendar(this IElasticClient client, Id calendarId,
			Func<PutCalendarDescriptor, IPutCalendarRequest> selector = null
		)
			=> client.MachineLearning.PutCalendar(calendarId, selector);

		/// <inheritdoc cref="PutCalendar(Nest.Id,System.Func{Nest.PutCalendarDescriptor,Nest.IPutCalendarRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PutCalendarResponse PutCalendar(this IElasticClient client, IPutCalendarRequest request)
			=> client.MachineLearning.PutCalendar(request);

		/// <inheritdoc cref="PutCalendar(Nest.Id,System.Func{Nest.PutCalendarDescriptor,Nest.IPutCalendarRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PutCalendarResponse> PutCalendarAsync(this IElasticClient client, Id calendarId,
			Func<PutCalendarDescriptor, IPutCalendarRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.PutCalendarAsync(calendarId, selector, ct);

		/// <inheritdoc cref="PutCalendar(Nest.Id,System.Func{Nest.PutCalendarDescriptor,Nest.IPutCalendarRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PutCalendarResponse> PutCalendarAsync(this IElasticClient client, IPutCalendarRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.PutCalendarAsync(request, ct);
	}
}
