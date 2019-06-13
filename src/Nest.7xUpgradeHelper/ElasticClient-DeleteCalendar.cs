using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Deletes a machine learning calendar.
		/// Removes all scheduled events from the calendar then deletes the calendar.
		/// </summary>
		public static DeleteCalendarResponse DeleteCalendar(this IElasticClient client,Id calendarId, Func<DeleteCalendarDescriptor, IDeleteCalendarRequest> selector = null);

		/// <inheritdoc cref="DeleteCalendar(Nest.Id,System.Func{Nest.DeleteCalendarDescriptor,Nest.IDeleteCalendarRequest})" />
		public static DeleteCalendarResponse DeleteCalendar(this IElasticClient client,IDeleteCalendarRequest request);

		/// <inheritdoc cref="DeleteCalendar(Nest.Id,System.Func{Nest.DeleteCalendarDescriptor,Nest.IDeleteCalendarRequest})" />
		public static Task<DeleteCalendarResponse> DeleteCalendarAsync(this IElasticClient client,Id calendarId, Func<DeleteCalendarDescriptor, IDeleteCalendarRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="DeleteCalendar(Nest.Id,System.Func{Nest.DeleteCalendarDescriptor,Nest.IDeleteCalendarRequest})" />
		public static Task<DeleteCalendarResponse> DeleteCalendarAsync(this IElasticClient client,IDeleteCalendarRequest request, CancellationToken ct = default);
	}

}
