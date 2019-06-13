using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Creates a machine learning calendar.
		/// </summary>
		public static PutCalendarResponse PutCalendar(this IElasticClient client,Id calendarId, Func<PutCalendarDescriptor, IPutCalendarRequest> selector = null);

		/// <inheritdoc cref="PutCalendar(Nest.Id,System.Func{Nest.PutCalendarDescriptor,Nest.IPutCalendarRequest})" />
		public static PutCalendarResponse PutCalendar(this IElasticClient client,IPutCalendarRequest request);

		/// <inheritdoc cref="PutCalendar(Nest.Id,System.Func{Nest.PutCalendarDescriptor,Nest.IPutCalendarRequest})" />
		public static Task<PutCalendarResponse> PutCalendarAsync(this IElasticClient client,Id calendarId, Func<PutCalendarDescriptor, IPutCalendarRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="PutCalendar(Nest.Id,System.Func{Nest.PutCalendarDescriptor,Nest.IPutCalendarRequest})" />
		public static Task<PutCalendarResponse> PutCalendarAsync(this IElasticClient client,IPutCalendarRequest request, CancellationToken ct = default);
	}

}
