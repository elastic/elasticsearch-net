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
			CancellationToken ct = default
		);

		/// <inheritdoc cref="PutCalendarJob(Nest.Id,Nest.Id,System.Func{Nest.PutCalendarJobDescriptor,Nest.IPutCalendarJobRequest})" />
		Task<IPutCalendarJobResponse> PutCalendarJobAsync(IPutCalendarJobRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPutCalendarJobResponse PutCalendarJob(Id calendarId, Id jobId, Func<PutCalendarJobDescriptor, IPutCalendarJobRequest> selector = null)
			=> PutCalendarJob(selector.InvokeOrDefault(new PutCalendarJobDescriptor(calendarId, jobId)));

		/// <inheritdoc />
		public IPutCalendarJobResponse PutCalendarJob(IPutCalendarJobRequest request) =>
			DoRequest<IPutCalendarJobRequest, PutCalendarJobResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IPutCalendarJobResponse> PutCalendarJobAsync(
			Id calendarId,
			Id jobId,
			Func<PutCalendarJobDescriptor, IPutCalendarJobRequest> selector = null,
			CancellationToken ct = default
		) => PutCalendarJobAsync(selector.InvokeOrDefault(new PutCalendarJobDescriptor(calendarId, jobId)), ct);

		/// <inheritdoc />
		public Task<IPutCalendarJobResponse> PutCalendarJobAsync(IPutCalendarJobRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPutCalendarJobRequest, IPutCalendarJobResponse, PutCalendarJobResponse>(request, request.RequestParameters, ct);
	}
}
