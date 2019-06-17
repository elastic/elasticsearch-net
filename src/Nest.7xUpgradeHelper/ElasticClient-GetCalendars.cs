using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieves calendar configuration information for machine learning jobs.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetCalendarsResponse GetCalendars(this IElasticClient client, Func<GetCalendarsDescriptor, IGetCalendarsRequest> selector = null
		)
			=> client.MachineLearning.GetCalendars(selector);


		/// <inheritdoc cref="GetCalendars(System.Func{Nest.GetCalendarsDescriptor,Nest.IGetCalendarsRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetCalendarsResponse GetCalendars(this IElasticClient client, IGetCalendarsRequest request)
			=> client.MachineLearning.GetCalendars(request);

		/// <inheritdoc cref="GetCalendars(System.Func{Nest.GetCalendarsDescriptor,Nest.IGetCalendarsRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetCalendarsResponse> GetCalendarsAsync(this IElasticClient client,
			Func<GetCalendarsDescriptor, IGetCalendarsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetCalendarsAsync(selector, ct);

		/// <inheritdoc cref="GetCalendars(System.Func{Nest.GetCalendarsDescriptor,Nest.IGetCalendarsRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetCalendarsResponse> GetCalendarsAsync(this IElasticClient client, IGetCalendarsRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetCalendarsAsync(request, ct);
	}
}
