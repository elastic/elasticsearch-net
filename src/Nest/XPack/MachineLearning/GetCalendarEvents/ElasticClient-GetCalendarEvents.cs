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
		IGetCalendarEventsResponse GetCalendarEvents(Id calendarId, Func<GetCalendarEventsDescriptor, IGetCalendarEventsRequest> selector = null);

		/// <inheritdoc cref="GetCalendarEvents(Nest.Id,System.Func{Nest.GetCalendarEventsDescriptor,Nest.IGetCalendarEventsRequest})" />
		IGetCalendarEventsResponse GetCalendarEvents(IGetCalendarEventsRequest request);

		/// <inheritdoc cref="GetCalendarEvents(Nest.Id,System.Func{Nest.GetCalendarEventsDescriptor,Nest.IGetCalendarEventsRequest})" />
		Task<IGetCalendarEventsResponse> GetCalendarEventsAsync(Id calendarId, Func<GetCalendarEventsDescriptor, IGetCalendarEventsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="GetCalendarEvents(Nest.Id,System.Func{Nest.GetCalendarEventsDescriptor,Nest.IGetCalendarEventsRequest})" />
		Task<IGetCalendarEventsResponse> GetCalendarEventsAsync(IGetCalendarEventsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="IElasticClient.GetCalendarEvents(Nest.Id,System.Func{Nest.GetCalendarEventsDescriptor,Nest.IGetCalendarEventsRequest})" />
		public IGetCalendarEventsResponse GetCalendarEvents(Id calendarId, Func<GetCalendarEventsDescriptor, IGetCalendarEventsRequest> selector = null) =>
			GetCalendarEvents(selector.InvokeOrDefault(new GetCalendarEventsDescriptor(calendarId)));

		/// <inheritdoc cref="IElasticClient.GetCalendarEvents(Nest.Id,System.Func{Nest.GetCalendarEventsDescriptor,Nest.IGetCalendarEventsRequest})" />
		public IGetCalendarEventsResponse GetCalendarEvents(IGetCalendarEventsRequest request) =>
			Dispatcher.Dispatch<IGetCalendarEventsRequest, GetCalendarEventsRequestParameters, GetCalendarEventsResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackMlGetCalendarEventsDispatch<GetCalendarEventsResponse>(p)
			);

		/// <inheritdoc cref="IElasticClient.GetCalendarEvents(Nest.Id,System.Func{Nest.GetCalendarEventsDescriptor,Nest.IGetCalendarEventsRequest})" />
		public Task<IGetCalendarEventsResponse> GetCalendarEventsAsync(Id calendarId, Func<GetCalendarEventsDescriptor, IGetCalendarEventsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => GetCalendarEventsAsync(selector.InvokeOrDefault(new GetCalendarEventsDescriptor(calendarId)), cancellationToken);

		/// <inheritdoc cref="IElasticClient.GetCalendarEvents(Nest.Id,System.Func{Nest.GetCalendarEventsDescriptor,Nest.IGetCalendarEventsRequest})" />
		public Task<IGetCalendarEventsResponse> GetCalendarEventsAsync(IGetCalendarEventsRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IGetCalendarEventsRequest, GetCalendarEventsRequestParameters, GetCalendarEventsResponse, IGetCalendarEventsResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.XpackMlGetCalendarEventsDispatchAsync<GetCalendarEventsResponse>(p, c)
			);
	}
}
