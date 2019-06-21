using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.DeleteCalendar(), please update this usage.")]
		public static DeleteCalendarResponse DeleteCalendar(this IElasticClient client, Id calendarId,
			Func<DeleteCalendarDescriptor, IDeleteCalendarRequest> selector = null
		)
			=> client.MachineLearning.DeleteCalendar(calendarId, selector);

		[Obsolete("Moved to client.MachineLearning.DeleteCalendar(), please update this usage.")]
		public static DeleteCalendarResponse DeleteCalendar(this IElasticClient client, IDeleteCalendarRequest request)
			=> client.MachineLearning.DeleteCalendar(request);

		[Obsolete("Moved to client.MachineLearning.DeleteCalendarAsync(), please update this usage.")]
		public static Task<DeleteCalendarResponse> DeleteCalendarAsync(this IElasticClient client, Id calendarId,
			Func<DeleteCalendarDescriptor, IDeleteCalendarRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteCalendarAsync(calendarId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.DeleteCalendarAsync(), please update this usage.")]
		public static Task<DeleteCalendarResponse> DeleteCalendarAsync(this IElasticClient client, IDeleteCalendarRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteCalendarAsync(request, ct);
	}
}
