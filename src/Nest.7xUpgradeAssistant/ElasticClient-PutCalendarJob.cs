using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.PutCalendarJob(), please update this usage.")]
		public static PutCalendarJobResponse PutCalendarJob(this IElasticClient client, Id calendarId, Id jobId,
			Func<PutCalendarJobDescriptor, IPutCalendarJobRequest> selector = null
		)
			=> client.MachineLearning.PutCalendarJob(calendarId, jobId, selector);

		[Obsolete("Moved to client.MachineLearning.PutCalendarJob(), please update this usage.")]
		public static PutCalendarJobResponse PutCalendarJob(this IElasticClient client, IPutCalendarJobRequest request)
			=> client.MachineLearning.PutCalendarJob(request);

		[Obsolete("Moved to client.MachineLearning.PutCalendarJobAsync(), please update this usage.")]
		public static Task<PutCalendarJobResponse> PutCalendarJobAsync(this IElasticClient client, Id calendarId, Id jobId,
			Func<PutCalendarJobDescriptor, IPutCalendarJobRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.PutCalendarJobAsync(calendarId, jobId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.PutCalendarJobAsync(), please update this usage.")]
		public static Task<PutCalendarJobResponse> PutCalendarJobAsync(this IElasticClient client, IPutCalendarJobRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.PutCalendarJobAsync(request, ct);
	}
}
