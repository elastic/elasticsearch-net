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
public static DeleteCalendarJobResponse DeleteCalendarJob(this IElasticClient client, Id calendarId, Id jobId,
			Func<DeleteCalendarJobDescriptor, IDeleteCalendarJobRequest> selector = null
		)
			=> client.MachineLearning.DeleteCalendarJob(calendarId, jobId, selector);

		/// <inheritdoc
		///     cref="DeleteCalendarJob(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarJobDescriptor,Nest.IDeleteCalendarJobRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteCalendarJobResponse DeleteCalendarJob(this IElasticClient client, IDeleteCalendarJobRequest request)
			=> client.MachineLearning.DeleteCalendarJob(request);

		/// <inheritdoc
		///     cref="DeleteCalendarJob(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarJobDescriptor,Nest.IDeleteCalendarJobRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteCalendarJobResponse> DeleteCalendarJobAsync(this IElasticClient client, Id calendarId, Id jobId,
			Func<DeleteCalendarJobDescriptor, IDeleteCalendarJobRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteCalendarJobAsync(calendarId, jobId, selector, ct);

		/// <inheritdoc
		///     cref="DeleteCalendarJob(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarJobDescriptor,Nest.IDeleteCalendarJobRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteCalendarJobResponse> DeleteCalendarJobAsync(this IElasticClient client, IDeleteCalendarJobRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.DeleteCalendarJobAsync(request);
	}
}
