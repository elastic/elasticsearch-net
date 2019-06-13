using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieve information about the scheduled events in calendars.
		/// </summary>
		public static GetCalendarEventsResponse GetCalendarEvents(this IElasticClient client,Id calendarId, Func<GetCalendarEventsDescriptor, IGetCalendarEventsRequest> selector = null);

		/// <inheritdoc cref="GetCalendarEvents(Nest.Id,System.Func{Nest.GetCalendarEventsDescriptor,Nest.IGetCalendarEventsRequest})" />
		public static GetCalendarEventsResponse GetCalendarEvents(this IElasticClient client,IGetCalendarEventsRequest request);

		/// <inheritdoc cref="GetCalendarEvents(Nest.Id,System.Func{Nest.GetCalendarEventsDescriptor,Nest.IGetCalendarEventsRequest})" />
		public static Task<GetCalendarEventsResponse> GetCalendarEventsAsync(this IElasticClient client,Id calendarId, Func<GetCalendarEventsDescriptor, IGetCalendarEventsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="GetCalendarEvents(Nest.Id,System.Func{Nest.GetCalendarEventsDescriptor,Nest.IGetCalendarEventsRequest})" />
		public static Task<GetCalendarEventsResponse> GetCalendarEventsAsync(this IElasticClient client,IGetCalendarEventsRequest request, CancellationToken ct = default);
	}

}
