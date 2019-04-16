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
		IPostCalendarEventsResponse PostCalendarEvents(Id calendarId, Func<PostCalendarEventsDescriptor, IPostCalendarEventsRequest> selector = null);

		/// <inheritdoc cref="PostCalendarEvents(Nest.Id,System.Func{Nest.PostCalendarEventsDescriptor,Nest.IPostCalendarEventsRequest})" />
		IPostCalendarEventsResponse PostCalendarEvents(IPostCalendarEventsRequest request);

		/// <inheritdoc cref="PostCalendarEvents(Nest.Id,System.Func{Nest.PostCalendarEventsDescriptor,Nest.IPostCalendarEventsRequest})" />
		Task<IPostCalendarEventsResponse> PostCalendarEventsAsync(Id calendarId, Func<PostCalendarEventsDescriptor, IPostCalendarEventsRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="PostCalendarEvents(Nest.Id,System.Func{Nest.PostCalendarEventsDescriptor,Nest.IPostCalendarEventsRequest})" />
		Task<IPostCalendarEventsResponse> PostCalendarEventsAsync(IPostCalendarEventsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPostCalendarEventsResponse PostCalendarEvents(Id calendarId, Func<PostCalendarEventsDescriptor, IPostCalendarEventsRequest> selector = null)
			=> PostCalendarEvents(selector.InvokeOrDefault(new PostCalendarEventsDescriptor(calendarId)));

		/// <inheritdoc />
		public IPostCalendarEventsResponse PostCalendarEvents(IPostCalendarEventsRequest request) =>
			DoRequest<IPostCalendarEventsRequest, PostCalendarEventsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IPostCalendarEventsResponse> PostCalendarEventsAsync(
			Id calendarId,
			Func<PostCalendarEventsDescriptor, IPostCalendarEventsRequest> selector = null,
			CancellationToken cancellationToken = default
		) => PostCalendarEventsAsync(selector.InvokeOrDefault(new PostCalendarEventsDescriptor(calendarId)), cancellationToken);

		/// <inheritdoc />
		public Task<IPostCalendarEventsResponse> PostCalendarEventsAsync(IPostCalendarEventsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPostCalendarEventsRequest, IPostCalendarEventsResponse, PostCalendarEventsResponse>
				(request, request.RequestParameters, ct);
	}
}
