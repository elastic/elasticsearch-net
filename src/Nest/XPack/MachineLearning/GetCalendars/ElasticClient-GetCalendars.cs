using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieves configuration for machine learning jobs.
		/// </summary>
		IGetCalendarsResponse GetCalendars(Func<GetCalendarsDescriptor, IGetCalendarsRequest> selector = null);


		/// <inheritdoc cref="GetCalendars(System.Func{Nest.GetCalendarsDescriptor,Nest.IGetCalendarsRequest})" />
		IGetCalendarsResponse GetCalendars(IGetCalendarsRequest request);

		/// <inheritdoc cref="GetCalendars(System.Func{Nest.GetCalendarsDescriptor,Nest.IGetCalendarsRequest})" />
		Task<IGetCalendarsResponse> GetCalendarsAsync(Func<GetCalendarsDescriptor, IGetCalendarsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="GetCalendars(System.Func{Nest.GetCalendarsDescriptor,Nest.IGetCalendarsRequest})" />
		Task<IGetCalendarsResponse> GetCalendarsAsync(IGetCalendarsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetCalendarsResponse GetCalendars(Func<GetCalendarsDescriptor, IGetCalendarsRequest> selector = null) =>
			GetCalendars(selector.InvokeOrDefault(new GetCalendarsDescriptor()));

		/// <inheritdoc />
		public IGetCalendarsResponse GetCalendars(IGetCalendarsRequest request) =>
			Dispatcher.Dispatch<IGetCalendarsRequest, GetCalendarsRequestParameters, GetCalendarsResponse>(
				request,
				LowLevelDispatch.XpackMlGetCalendarsDispatch<GetCalendarsResponse>
			);

		/// <inheritdoc />
		public Task<IGetCalendarsResponse> GetCalendarsAsync(Func<GetCalendarsDescriptor, IGetCalendarsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetCalendarsAsync(selector.InvokeOrDefault(new GetCalendarsDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IGetCalendarsResponse> GetCalendarsAsync(IGetCalendarsRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<IGetCalendarsRequest, GetCalendarsRequestParameters, GetCalendarsResponse, IGetCalendarsResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.XpackMlGetCalendarsDispatchAsync<GetCalendarsResponse>
			);
	}
}
