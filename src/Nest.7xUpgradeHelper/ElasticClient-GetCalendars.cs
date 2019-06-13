using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieves calendar configuration information for machine learning jobs.
		/// </summary>
		public static GetCalendarsResponse GetCalendars(this IElasticClient client,Func<GetCalendarsDescriptor, IGetCalendarsRequest> selector = null);


		/// <inheritdoc cref="GetCalendars(System.Func{Nest.GetCalendarsDescriptor,Nest.IGetCalendarsRequest})" />
		public static GetCalendarsResponse GetCalendars(this IElasticClient client,IGetCalendarsRequest request);

		/// <inheritdoc cref="GetCalendars(System.Func{Nest.GetCalendarsDescriptor,Nest.IGetCalendarsRequest})" />
		public static Task<GetCalendarsResponse> GetCalendarsAsync(this IElasticClient client,Func<GetCalendarsDescriptor, IGetCalendarsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="GetCalendars(System.Func{Nest.GetCalendarsDescriptor,Nest.IGetCalendarsRequest})" />
		public static Task<GetCalendarsResponse> GetCalendarsAsync(this IElasticClient client,IGetCalendarsRequest request, CancellationToken ct = default);
	}

}
