using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.PutCalendar(), please update this usage.")]
		public static PutCalendarResponse PutCalendar(this IElasticClient client, Id calendarId,
			Func<PutCalendarDescriptor, IPutCalendarRequest> selector = null
		)
			=> client.MachineLearning.PutCalendar(calendarId, selector);

		[Obsolete("Moved to client.MachineLearning.PutCalendar(), please update this usage.")]
		public static PutCalendarResponse PutCalendar(this IElasticClient client, IPutCalendarRequest request)
			=> client.MachineLearning.PutCalendar(request);

		[Obsolete("Moved to client.MachineLearning.PutCalendarAsync(), please update this usage.")]
		public static Task<PutCalendarResponse> PutCalendarAsync(this IElasticClient client, Id calendarId,
			Func<PutCalendarDescriptor, IPutCalendarRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.PutCalendarAsync(calendarId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.PutCalendarAsync(), please update this usage.")]
		public static Task<PutCalendarResponse> PutCalendarAsync(this IElasticClient client, IPutCalendarRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.PutCalendarAsync(request, ct);
	}
}
