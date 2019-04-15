using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieves calendar configuration information for machine learning jobs.
		/// </summary>
		GetCalendarsResponse GetCalendars(Func<GetCalendarsDescriptor, IGetCalendarsRequest> selector = null);


		/// <inheritdoc cref="GetCalendars(System.Func{Nest.GetCalendarsDescriptor,Nest.IGetCalendarsRequest})" />
		GetCalendarsResponse GetCalendars(IGetCalendarsRequest request);

		/// <inheritdoc cref="GetCalendars(System.Func{Nest.GetCalendarsDescriptor,Nest.IGetCalendarsRequest})" />
		Task<GetCalendarsResponse> GetCalendarsAsync(Func<GetCalendarsDescriptor, IGetCalendarsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="GetCalendars(System.Func{Nest.GetCalendarsDescriptor,Nest.IGetCalendarsRequest})" />
		Task<GetCalendarsResponse> GetCalendarsAsync(IGetCalendarsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetCalendarsResponse GetCalendars(Func<GetCalendarsDescriptor, IGetCalendarsRequest> selector = null) =>
			GetCalendars(selector.InvokeOrDefault(new GetCalendarsDescriptor()));

		/// <inheritdoc />
		public GetCalendarsResponse GetCalendars(IGetCalendarsRequest request) =>
			DoRequest<IGetCalendarsRequest, GetCalendarsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetCalendarsResponse> GetCalendarsAsync(
			Func<GetCalendarsDescriptor, IGetCalendarsRequest> selector = null,
			CancellationToken ct = default
		) => GetCalendarsAsync(selector.InvokeOrDefault(new GetCalendarsDescriptor()), ct);

		/// <inheritdoc />
		public Task<GetCalendarsResponse> GetCalendarsAsync(IGetCalendarsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetCalendarsRequest, GetCalendarsResponse>(request, request.RequestParameters, ct);
	}
}
