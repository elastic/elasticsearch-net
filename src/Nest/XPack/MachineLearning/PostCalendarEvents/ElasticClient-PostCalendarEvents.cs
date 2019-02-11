using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Instantiates a machine learning calendar.
		/// </summary>
		IPostCalendarEventsResponse PostCalendarEvents(Id calendarId, Func<PostCalendarEventsDescriptor, IPostCalendarEventsRequest> selector = null);

		/// <inheritdoc cref="PostCalendarEvents(Nest.Id,System.Func{Nest.PostCalendarEventsDescriptor,Nest.IPostCalendarEventsRequest})" />
		IPostCalendarEventsResponse PostCalendarEvents(IPostCalendarEventsRequest request);

		/// <inheritdoc cref="PostCalendarEvents(Nest.Id,System.Func{Nest.PostCalendarEventsDescriptor,Nest.IPostCalendarEventsRequest})" />
		Task<IPostCalendarEventsResponse> PostCalendarEventsAsync(Id calendarId, Func<PostCalendarEventsDescriptor, IPostCalendarEventsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="PostCalendarEvents(Nest.Id,System.Func{Nest.PostCalendarEventsDescriptor,Nest.IPostCalendarEventsRequest})" />
		Task<IPostCalendarEventsResponse> PostCalendarEventsAsync(IPostCalendarEventsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPostCalendarEventsResponse PostCalendarEvents(Id calendarId, Func<PostCalendarEventsDescriptor, IPostCalendarEventsRequest> selector = null)
			=> PostCalendarEvents(selector.InvokeOrDefault(new PostCalendarEventsDescriptor(calendarId)));

		/// <inheritdoc />
		public IPostCalendarEventsResponse PostCalendarEvents(IPostCalendarEventsRequest request) =>
			Dispatcher.Dispatch<IPostCalendarEventsRequest, PostCalendarEventsRequestParameters, PostCalendarEventsResponse>(
				request,
				LowLevelDispatch.XpackMlPostCalendarEventsDispatch<PostCalendarEventsResponse>
			);

		/// <inheritdoc />
		public Task<IPostCalendarEventsResponse> PostCalendarEventsAsync(Id calendarId, Func<PostCalendarEventsDescriptor, IPostCalendarEventsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			PostCalendarEventsAsync(selector.InvokeOrDefault(new PostCalendarEventsDescriptor(calendarId)), cancellationToken);

		/// <inheritdoc />
		public Task<IPostCalendarEventsResponse> PostCalendarEventsAsync(IPostCalendarEventsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IPostCalendarEventsRequest, PostCalendarEventsRequestParameters, PostCalendarEventsResponse, IPostCalendarEventsResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.XpackMlPostCalendarEventsDispatchAsync<PostCalendarEventsResponse>
			);
	}
}
