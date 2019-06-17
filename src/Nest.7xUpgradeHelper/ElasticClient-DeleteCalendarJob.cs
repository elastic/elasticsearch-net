using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.DeleteCalendarJob(), please update this usage.")]
		public static DeleteCalendarJobResponse DeleteCalendarJob(this IElasticClient client, Id calendarId, Id jobId,
			Func<DeleteCalendarJobDescriptor, IDeleteCalendarJobRequest> selector = null
		)
			=> client.MachineLearning.DeleteCalendarJob(calendarId, jobId, selector);

		[Obsolete("Moved to client.MachineLearning.DeleteCalendarJob(), please update this usage.")]
		public static DeleteCalendarJobResponse DeleteCalendarJob(this IElasticClient client, IDeleteCalendarJobRequest request)
			=> client.MachineLearning.DeleteCalendarJob(request);

		[Obsolete("Moved to client.MachineLearning.DeleteCalendarJobAsync(), please update this usage.")]
		public static Task<DeleteCalendarJobResponse> DeleteCalendarJobAsync(this IElasticClient client, Id calendarId, Id jobId,
			Func<DeleteCalendarJobDescriptor, IDeleteCalendarJobRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteCalendarJobAsync(calendarId, jobId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.DeleteCalendarJobAsync(), please update this usage.")]
		public static Task<DeleteCalendarJobResponse> DeleteCalendarJobAsync(this IElasticClient client, IDeleteCalendarJobRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteCalendarJobAsync(request, ct);
	}
}
