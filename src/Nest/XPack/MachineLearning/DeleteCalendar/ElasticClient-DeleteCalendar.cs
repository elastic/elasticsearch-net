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
			CancellationToken ct = default
		);

		/// <inheritdoc cref="DeleteCalendar(Nest.Id,System.Func{Nest.DeleteCalendarDescriptor,Nest.IDeleteCalendarRequest})" />
		Task<IDeleteCalendarResponse> DeleteCalendarAsync(IDeleteCalendarRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IDeleteCalendarResponse DeleteCalendar(Id calendarId, Func<DeleteCalendarDescriptor, IDeleteCalendarRequest> selector = null) =>
			DeleteCalendar(selector.InvokeOrDefault(new DeleteCalendarDescriptor(calendarId)));

		/// <inheritdoc />
		public IDeleteCalendarResponse DeleteCalendar(IDeleteCalendarRequest request) =>
			Dispatch2<IDeleteCalendarRequest, DeleteCalendarResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IDeleteCalendarResponse> DeleteCalendarAsync(Id calendarId, Func<DeleteCalendarDescriptor, IDeleteCalendarRequest> selector = null,
			CancellationToken ct = default
		) =>
			DeleteCalendarAsync(selector.InvokeOrDefault(new DeleteCalendarDescriptor(calendarId)), ct);

		/// <inheritdoc />
		public Task<IDeleteCalendarResponse> DeleteCalendarAsync(IDeleteCalendarRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IDeleteCalendarRequest, IDeleteCalendarResponse, DeleteCalendarResponse>(request, request.RequestParameters, ct);
	}
}
