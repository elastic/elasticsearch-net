using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Adds a job to a machine learning calendar.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PutCalendarJobResponse PutCalendarJob(this IElasticClient client, Id calendarId, Id jobId,
			Func<PutCalendarJobDescriptor, IPutCalendarJobRequest> selector = null
		)
			=> client.MachineLearning.PutCalendarJob(calendarId, jobId, selector);

		/// <inheritdoc
		///     cref="PutCalendarJob(Nest.Id,Nest.Id,System.Func{Nest.PutCalendarJobDescriptor,Nest.IPutCalendarJobRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PutCalendarJobResponse PutCalendarJob(this IElasticClient client, IPutCalendarJobRequest request)
			=> client.MachineLearning.PutCalendarJob(request);

		/// <inheritdoc
		///     cref="PutCalendarJob(Nest.Id,Nest.Id,System.Func{Nest.PutCalendarJobDescriptor,Nest.IPutCalendarJobRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PutCalendarJobResponse> PutCalendarJobAsync(this IElasticClient client, Id calendarId, Id jobId,
			Func<PutCalendarJobDescriptor, IPutCalendarJobRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.PutCalendarJobAsync(calendarId, jobId, selector, ct);

		/// <inheritdoc
		///     cref="PutCalendarJob(Nest.Id,Nest.Id,System.Func{Nest.PutCalendarJobDescriptor,Nest.IPutCalendarJobRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PutCalendarJobResponse> PutCalendarJobAsync(this IElasticClient client, IPutCalendarJobRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.PutCalendarJobAsync(request, ct);
	}
}
