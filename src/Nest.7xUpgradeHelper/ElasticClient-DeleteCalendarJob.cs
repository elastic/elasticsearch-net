using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Deletes a machine learning calendar.
		/// Removes all scheduled events from the calendar then deletes the calendar.
		/// </summary>
		public static DeleteCalendarJobResponse DeleteCalendarJob(this IElasticClient client,Id calendarId, Id jobId, Func<DeleteCalendarJobDescriptor, IDeleteCalendarJobRequest> selector = null);

		/// <inheritdoc cref="DeleteCalendarJob(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarJobDescriptor,Nest.IDeleteCalendarJobRequest})" />
		public static DeleteCalendarJobResponse DeleteCalendarJob(this IElasticClient client,IDeleteCalendarJobRequest request);

		/// <inheritdoc cref="DeleteCalendarJob(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarJobDescriptor,Nest.IDeleteCalendarJobRequest})" />
		public static Task<DeleteCalendarJobResponse> DeleteCalendarJobAsync(this IElasticClient client,Id calendarId, Id jobId, Func<DeleteCalendarJobDescriptor, IDeleteCalendarJobRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="DeleteCalendarJob(Nest.Id,Nest.Id,System.Func{Nest.DeleteCalendarJobDescriptor,Nest.IDeleteCalendarJobRequest})" />
		public static Task<DeleteCalendarJobResponse> DeleteCalendarJobAsync(this IElasticClient client,IDeleteCalendarJobRequest request, CancellationToken ct = default);
	}

}
