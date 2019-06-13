using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Adds a job to a machine learning calendar.
		/// </summary>
		public static PutCalendarJobResponse PutCalendarJob(this IElasticClient client,Id calendarId, Id jobId, Func<PutCalendarJobDescriptor, IPutCalendarJobRequest> selector = null);

		/// <inheritdoc cref="PutCalendarJob(Nest.Id,Nest.Id,System.Func{Nest.PutCalendarJobDescriptor,Nest.IPutCalendarJobRequest})" />
		public static PutCalendarJobResponse PutCalendarJob(this IElasticClient client,IPutCalendarJobRequest request);

		/// <inheritdoc cref="PutCalendarJob(Nest.Id,Nest.Id,System.Func{Nest.PutCalendarJobDescriptor,Nest.IPutCalendarJobRequest})" />
		public static Task<PutCalendarJobResponse> PutCalendarJobAsync(this IElasticClient client,Id calendarId, Id jobId, Func<PutCalendarJobDescriptor, IPutCalendarJobRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="PutCalendarJob(Nest.Id,Nest.Id,System.Func{Nest.PutCalendarJobDescriptor,Nest.IPutCalendarJobRequest})" />
		public static Task<PutCalendarJobResponse> PutCalendarJobAsync(this IElasticClient client,IPutCalendarJobRequest request, CancellationToken ct = default);
	}

}
