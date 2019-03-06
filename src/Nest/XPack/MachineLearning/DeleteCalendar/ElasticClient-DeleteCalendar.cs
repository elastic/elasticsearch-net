using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Deletes a machine learning calendar.
		/// Removes all scheduled events from the calendar then deletes the calendar.
		/// </summary>
		IDeleteCalendarResponse DeleteCalendar(Id calendarId, Func<DeleteCalendarDescriptor, IDeleteCalendarRequest> selector = null);

		/// <inheritdoc cref="DeleteCalendar(Nest.Id,System.Func{Nest.DeleteCalendarDescriptor,Nest.IDeleteCalendarRequest})" />
		IDeleteCalendarResponse DeleteCalendar(IDeleteCalendarRequest request);

		/// <inheritdoc cref="DeleteCalendar(Nest.Id,System.Func{Nest.DeleteCalendarDescriptor,Nest.IDeleteCalendarRequest})" />
		Task<IDeleteCalendarResponse> DeleteCalendarAsync(Id calendarId, Func<DeleteCalendarDescriptor, IDeleteCalendarRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="DeleteCalendar(Nest.Id,System.Func{Nest.DeleteCalendarDescriptor,Nest.IDeleteCalendarRequest})" />
		Task<IDeleteCalendarResponse> DeleteCalendarAsync(IDeleteCalendarRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteCalendarResponse DeleteCalendar(Id calendarId, Func<DeleteCalendarDescriptor, IDeleteCalendarRequest> selector = null) =>
			DeleteCalendar(selector.InvokeOrDefault(new DeleteCalendarDescriptor(calendarId)));

		/// <inheritdoc />
		public IDeleteCalendarResponse DeleteCalendar(IDeleteCalendarRequest request) =>
			Dispatcher.Dispatch<IDeleteCalendarRequest, DeleteCalendarRequestParameters, DeleteCalendarResponse>(
				request,
				(p, d) => LowLevelDispatch.MlDeleteCalendarDispatch<DeleteCalendarResponse>(p)
			);

		/// <inheritdoc />
		public Task<IDeleteCalendarResponse> DeleteCalendarAsync(Id calendarId, Func<DeleteCalendarDescriptor, IDeleteCalendarRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DeleteCalendarAsync(selector.InvokeOrDefault(new DeleteCalendarDescriptor(calendarId)), cancellationToken);

		/// <inheritdoc />
		public Task<IDeleteCalendarResponse> DeleteCalendarAsync(IDeleteCalendarRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IDeleteCalendarRequest, DeleteCalendarRequestParameters, DeleteCalendarResponse, IDeleteCalendarResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.MlDeleteCalendarDispatchAsync<DeleteCalendarResponse>(p, c)
			);
	}
}
