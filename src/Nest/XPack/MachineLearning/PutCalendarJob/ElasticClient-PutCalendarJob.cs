using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Adds a job to a machine learning calendar.
		/// </summary>
		IPutCalendarJobResponse PutCalendarJob(Id calendarId, Id jobId, Func<PutCalendarJobDescriptor, IPutCalendarJobRequest> selector = null);

		/// <inheritdoc cref="PutCalendarJob(Nest.Id,Nest.Id,System.Func{Nest.PutCalendarJobDescriptor,Nest.IPutCalendarJobRequest})" />
		IPutCalendarJobResponse PutCalendarJob(IPutCalendarJobRequest request);

		/// <inheritdoc cref="PutCalendarJob(Nest.Id,Nest.Id,System.Func{Nest.PutCalendarJobDescriptor,Nest.IPutCalendarJobRequest})" />
		Task<IPutCalendarJobResponse> PutCalendarJobAsync(Id calendarId, Id jobId, Func<PutCalendarJobDescriptor, IPutCalendarJobRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="PutCalendarJob(Nest.Id,Nest.Id,System.Func{Nest.PutCalendarJobDescriptor,Nest.IPutCalendarJobRequest})" />
		Task<IPutCalendarJobResponse> PutCalendarJobAsync(IPutCalendarJobRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPutCalendarJobResponse PutCalendarJob(Id calendarId, Id jobId, Func<PutCalendarJobDescriptor, IPutCalendarJobRequest> selector = null)
			=> PutCalendarJob(selector.InvokeOrDefault(new PutCalendarJobDescriptor(calendarId, jobId)));

		/// <inheritdoc />
		public IPutCalendarJobResponse PutCalendarJob(IPutCalendarJobRequest request) =>
			Dispatcher.Dispatch<IPutCalendarJobRequest, PutCalendarJobRequestParameters, PutCalendarJobResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackMlPutCalendarJobDispatch<PutCalendarJobResponse>(p)
			);

		/// <inheritdoc />
		public Task<IPutCalendarJobResponse> PutCalendarJobAsync(Id calendarId, Id jobId, Func<PutCalendarJobDescriptor, IPutCalendarJobRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			PutCalendarJobAsync(selector.InvokeOrDefault(new PutCalendarJobDescriptor(calendarId, jobId)), cancellationToken);

		/// <inheritdoc />
		public Task<IPutCalendarJobResponse> PutCalendarJobAsync(IPutCalendarJobRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IPutCalendarJobRequest, PutCalendarJobRequestParameters, PutCalendarJobResponse, IPutCalendarJobResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.XpackMlPutCalendarJobDispatchAsync<PutCalendarJobResponse>(p, c)
			);
	}
}
