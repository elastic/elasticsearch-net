using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Creates a machine learning calendar.
		/// </summary>
		IPutCalendarResponse PutCalendar(Id calendarId, Func<PutCalendarDescriptor, IPutCalendarRequest> selector = null);

		/// <inheritdoc cref="PutCalendar(Nest.Id,System.Func{Nest.PutCalendarDescriptor,Nest.IPutCalendarRequest})" />
		IPutCalendarResponse PutCalendar(IPutCalendarRequest request);

		/// <inheritdoc cref="PutCalendar(Nest.Id,System.Func{Nest.PutCalendarDescriptor,Nest.IPutCalendarRequest})" />
		Task<IPutCalendarResponse> PutCalendarAsync(Id calendarId, Func<PutCalendarDescriptor, IPutCalendarRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="PutCalendar(Nest.Id,System.Func{Nest.PutCalendarDescriptor,Nest.IPutCalendarRequest})" />
		Task<IPutCalendarResponse> PutCalendarAsync(IPutCalendarRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPutCalendarResponse PutCalendar(Id calendarId, Func<PutCalendarDescriptor, IPutCalendarRequest> selector = null)
			=> PutCalendar(selector.InvokeOrDefault(new PutCalendarDescriptor(calendarId)));

		/// <inheritdoc />
		public IPutCalendarResponse PutCalendar(IPutCalendarRequest request) =>
			Dispatcher.Dispatch<IPutCalendarRequest, PutCalendarRequestParameters, PutCalendarResponse>(
				request,
				LowLevelDispatch.MlPutCalendarDispatch<PutCalendarResponse>
			);

		/// <inheritdoc />
		public Task<IPutCalendarResponse> PutCalendarAsync(Id calendarId, Func<PutCalendarDescriptor, IPutCalendarRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			PutCalendarAsync(selector.InvokeOrDefault(new PutCalendarDescriptor(calendarId)), cancellationToken);

		/// <inheritdoc />
		public Task<IPutCalendarResponse> PutCalendarAsync(IPutCalendarRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IPutCalendarRequest, PutCalendarRequestParameters, PutCalendarResponse, IPutCalendarResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.MlPutCalendarDispatchAsync<PutCalendarResponse>
			);
	}
}
