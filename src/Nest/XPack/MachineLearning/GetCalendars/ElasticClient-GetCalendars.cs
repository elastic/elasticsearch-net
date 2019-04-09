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
		IGetCalendarsResponse GetCalendars(Func<GetCalendarsDescriptor, IGetCalendarsRequest> selector = null);


		/// <inheritdoc cref="GetCalendars(System.Func{Nest.GetCalendarsDescriptor,Nest.IGetCalendarsRequest})" />
		IGetCalendarsResponse GetCalendars(IGetCalendarsRequest request);

		/// <inheritdoc cref="GetCalendars(System.Func{Nest.GetCalendarsDescriptor,Nest.IGetCalendarsRequest})" />
		Task<IGetCalendarsResponse> GetCalendarsAsync(Func<GetCalendarsDescriptor, IGetCalendarsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="GetCalendars(System.Func{Nest.GetCalendarsDescriptor,Nest.IGetCalendarsRequest})" />
		Task<IGetCalendarsResponse> GetCalendarsAsync(IGetCalendarsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetCalendarsResponse GetCalendars(Func<GetCalendarsDescriptor, IGetCalendarsRequest> selector = null) =>
			GetCalendars(selector.InvokeOrDefault(new GetCalendarsDescriptor()));

		/// <inheritdoc />
		public IGetCalendarsResponse GetCalendars(IGetCalendarsRequest request) =>
			Dispatch2<IGetCalendarsRequest, GetCalendarsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetCalendarsResponse> GetCalendarsAsync(
			Func<GetCalendarsDescriptor, IGetCalendarsRequest> selector = null,
			CancellationToken ct = default
		) => GetCalendarsAsync(selector.InvokeOrDefault(new GetCalendarsDescriptor()), ct);

		/// <inheritdoc />
		public Task<IGetCalendarsResponse> GetCalendarsAsync(IGetCalendarsRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IGetCalendarsRequest, IGetCalendarsResponse, GetCalendarsResponse>(request, request.RequestParameters, ct);
	}
}
