using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieve information about the scheduled events in calendars.
		/// </summary>
		GetCalendarEventsResponse GetCalendarEvents(Id calendarId, Func<GetCalendarEventsDescriptor, IGetCalendarEventsRequest> selector = null);

		/// <inheritdoc cref="GetCalendarEvents(Nest.Id,System.Func{Nest.GetCalendarEventsDescriptor,Nest.IGetCalendarEventsRequest})" />
		GetCalendarEventsResponse GetCalendarEvents(IGetCalendarEventsRequest request);

		/// <inheritdoc cref="GetCalendarEvents(Nest.Id,System.Func{Nest.GetCalendarEventsDescriptor,Nest.IGetCalendarEventsRequest})" />
		Task<GetCalendarEventsResponse> GetCalendarEventsAsync(Id calendarId, Func<GetCalendarEventsDescriptor, IGetCalendarEventsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="GetCalendarEvents(Nest.Id,System.Func{Nest.GetCalendarEventsDescriptor,Nest.IGetCalendarEventsRequest})" />
		Task<GetCalendarEventsResponse> GetCalendarEventsAsync(IGetCalendarEventsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="IElasticClient.GetCalendarEvents(Nest.Id,System.Func{Nest.GetCalendarEventsDescriptor,Nest.IGetCalendarEventsRequest})" />
		public GetCalendarEventsResponse GetCalendarEvents(Id calendarId, Func<GetCalendarEventsDescriptor, IGetCalendarEventsRequest> selector = null) =>
			GetCalendarEvents(selector.InvokeOrDefault(new GetCalendarEventsDescriptor(calendarId)));

		/// <inheritdoc cref="IElasticClient.GetCalendarEvents(Nest.Id,System.Func{Nest.GetCalendarEventsDescriptor,Nest.IGetCalendarEventsRequest})" />
		public GetCalendarEventsResponse GetCalendarEvents(IGetCalendarEventsRequest request) =>
			DoRequest<IGetCalendarEventsRequest, GetCalendarEventsResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="IElasticClient.GetCalendarEvents(Nest.Id,System.Func{Nest.GetCalendarEventsDescriptor,Nest.IGetCalendarEventsRequest})" />
		public Task<GetCalendarEventsResponse> GetCalendarEventsAsync(
			Id calendarId,
			Func<GetCalendarEventsDescriptor, IGetCalendarEventsRequest> selector = null,
			CancellationToken ct = default
		) => GetCalendarEventsAsync(selector.InvokeOrDefault(new GetCalendarEventsDescriptor(calendarId)), ct);

		/// <inheritdoc cref="IElasticClient.GetCalendarEvents(Nest.Id,System.Func{Nest.GetCalendarEventsDescriptor,Nest.IGetCalendarEventsRequest})" />
		public Task<GetCalendarEventsResponse> GetCalendarEventsAsync(IGetCalendarEventsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetCalendarEventsRequest, GetCalendarEventsResponse, GetCalendarEventsResponse>(request, request.RequestParameters, ct);
	}
}
