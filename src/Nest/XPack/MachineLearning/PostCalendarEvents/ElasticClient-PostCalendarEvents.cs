using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Creates a machine learning calendar event.
		/// </summary>
		PostCalendarEventsResponse PostCalendarEvents(Id calendarId, Func<PostCalendarEventsDescriptor, IPostCalendarEventsRequest> selector = null);

		/// <inheritdoc cref="PostCalendarEvents(Nest.Id,System.Func{Nest.PostCalendarEventsDescriptor,Nest.IPostCalendarEventsRequest})" />
		PostCalendarEventsResponse PostCalendarEvents(IPostCalendarEventsRequest request);

		/// <inheritdoc cref="PostCalendarEvents(Nest.Id,System.Func{Nest.PostCalendarEventsDescriptor,Nest.IPostCalendarEventsRequest})" />
		Task<PostCalendarEventsResponse> PostCalendarEventsAsync(Id calendarId, Func<PostCalendarEventsDescriptor, IPostCalendarEventsRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="PostCalendarEvents(Nest.Id,System.Func{Nest.PostCalendarEventsDescriptor,Nest.IPostCalendarEventsRequest})" />
		Task<PostCalendarEventsResponse> PostCalendarEventsAsync(IPostCalendarEventsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public PostCalendarEventsResponse PostCalendarEvents(Id calendarId, Func<PostCalendarEventsDescriptor, IPostCalendarEventsRequest> selector = null)
			=> PostCalendarEvents(selector.InvokeOrDefault(new PostCalendarEventsDescriptor(calendarId)));

		/// <inheritdoc />
		public PostCalendarEventsResponse PostCalendarEvents(IPostCalendarEventsRequest request) =>
			DoRequest<IPostCalendarEventsRequest, PostCalendarEventsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<PostCalendarEventsResponse> PostCalendarEventsAsync(
			Id calendarId,
			Func<PostCalendarEventsDescriptor, IPostCalendarEventsRequest> selector = null,
			CancellationToken cancellationToken = default
		) => PostCalendarEventsAsync(selector.InvokeOrDefault(new PostCalendarEventsDescriptor(calendarId)), cancellationToken);

		/// <inheritdoc />
		public Task<PostCalendarEventsResponse> PostCalendarEventsAsync(IPostCalendarEventsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPostCalendarEventsRequest, PostCalendarEventsResponse, PostCalendarEventsResponse>
				(request, request.RequestParameters, ct);
	}
}
