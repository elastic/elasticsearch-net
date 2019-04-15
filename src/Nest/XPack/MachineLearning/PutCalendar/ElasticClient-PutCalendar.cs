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
		PutCalendarResponse PutCalendar(Id calendarId, Func<PutCalendarDescriptor, IPutCalendarRequest> selector = null);

		/// <inheritdoc cref="PutCalendar(Nest.Id,System.Func{Nest.PutCalendarDescriptor,Nest.IPutCalendarRequest})" />
		PutCalendarResponse PutCalendar(IPutCalendarRequest request);

		/// <inheritdoc cref="PutCalendar(Nest.Id,System.Func{Nest.PutCalendarDescriptor,Nest.IPutCalendarRequest})" />
		Task<PutCalendarResponse> PutCalendarAsync(Id calendarId, Func<PutCalendarDescriptor, IPutCalendarRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="PutCalendar(Nest.Id,System.Func{Nest.PutCalendarDescriptor,Nest.IPutCalendarRequest})" />
		Task<PutCalendarResponse> PutCalendarAsync(IPutCalendarRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public PutCalendarResponse PutCalendar(Id calendarId, Func<PutCalendarDescriptor, IPutCalendarRequest> selector = null)
			=> PutCalendar(selector.InvokeOrDefault(new PutCalendarDescriptor(calendarId)));

		/// <inheritdoc />
		public PutCalendarResponse PutCalendar(IPutCalendarRequest request) =>
			DoRequest<IPutCalendarRequest, PutCalendarResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<PutCalendarResponse> PutCalendarAsync(
			Id calendarId,
			Func<PutCalendarDescriptor, IPutCalendarRequest> selector = null,
			CancellationToken ct = default
		) => PutCalendarAsync(selector.InvokeOrDefault(new PutCalendarDescriptor(calendarId)), ct);

		/// <inheritdoc />
		public Task<PutCalendarResponse> PutCalendarAsync(IPutCalendarRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPutCalendarRequest, PutCalendarResponse>(request, request.RequestParameters, ct);
	}
}
