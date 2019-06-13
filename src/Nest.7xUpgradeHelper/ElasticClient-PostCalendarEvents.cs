using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Creates a machine learning calendar event.
		/// </summary>
		public static PostCalendarEventsResponse PostCalendarEvents(this IElasticClient client,Id calendarId, Func<PostCalendarEventsDescriptor, IPostCalendarEventsRequest> selector = null);

		/// <inheritdoc cref="PostCalendarEvents(Nest.Id,System.Func{Nest.PostCalendarEventsDescriptor,Nest.IPostCalendarEventsRequest})" />
		public static PostCalendarEventsResponse PostCalendarEvents(this IElasticClient client,IPostCalendarEventsRequest request);

		/// <inheritdoc cref="PostCalendarEvents(Nest.Id,System.Func{Nest.PostCalendarEventsDescriptor,Nest.IPostCalendarEventsRequest})" />
		public static Task<PostCalendarEventsResponse> PostCalendarEventsAsync(this IElasticClient client,Id calendarId, Func<PostCalendarEventsDescriptor, IPostCalendarEventsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="PostCalendarEvents(Nest.Id,System.Func{Nest.PostCalendarEventsDescriptor,Nest.IPostCalendarEventsRequest})" />
		public static Task<PostCalendarEventsResponse> PostCalendarEventsAsync(this IElasticClient client,IPostCalendarEventsRequest request, CancellationToken ct = default);
	}

}
