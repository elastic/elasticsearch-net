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
			CancellationToken ct = default
		);

		/// <inheritdoc cref="PutCalendar(Nest.Id,System.Func{Nest.PutCalendarDescriptor,Nest.IPutCalendarRequest})" />
		Task<IPutCalendarResponse> PutCalendarAsync(IPutCalendarRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPutCalendarResponse PutCalendar(Id calendarId, Func<PutCalendarDescriptor, IPutCalendarRequest> selector = null)
			=> PutCalendar(selector.InvokeOrDefault(new PutCalendarDescriptor(calendarId)));

		/// <inheritdoc />
		public IPutCalendarResponse PutCalendar(IPutCalendarRequest request) =>
			Dispatch2<IPutCalendarRequest, PutCalendarResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IPutCalendarResponse> PutCalendarAsync(
			Id calendarId,
			Func<PutCalendarDescriptor, IPutCalendarRequest> selector = null,
			CancellationToken ct = default
		) => PutCalendarAsync(selector.InvokeOrDefault(new PutCalendarDescriptor(calendarId)), ct);

		/// <inheritdoc />
		public Task<IPutCalendarResponse> PutCalendarAsync(IPutCalendarRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IPutCalendarRequest, IPutCalendarResponse, PutCalendarResponse>(request, request.RequestParameters, ct);
	}
}
