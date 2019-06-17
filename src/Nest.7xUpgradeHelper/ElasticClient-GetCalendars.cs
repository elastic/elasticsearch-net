using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.GetCalendars(), please update this usage.")]
		public static GetCalendarsResponse GetCalendars(this IElasticClient client, Func<GetCalendarsDescriptor, IGetCalendarsRequest> selector = null
		)
			=> client.MachineLearning.GetCalendars(selector);


		[Obsolete("Moved to client.MachineLearning.GetCalendars(), please update this usage.")]
		public static GetCalendarsResponse GetCalendars(this IElasticClient client, IGetCalendarsRequest request)
			=> client.MachineLearning.GetCalendars(request);

		[Obsolete("Moved to client.MachineLearning.GetCalendarsAsync(), please update this usage.")]
		public static Task<GetCalendarsResponse> GetCalendarsAsync(this IElasticClient client,
			Func<GetCalendarsDescriptor, IGetCalendarsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetCalendarsAsync(selector, ct);

		[Obsolete("Moved to client.MachineLearning.GetCalendarsAsync(), please update this usage.")]
		public static Task<GetCalendarsResponse> GetCalendarsAsync(this IElasticClient client, IGetCalendarsRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetCalendarsAsync(request, ct);
	}
}
