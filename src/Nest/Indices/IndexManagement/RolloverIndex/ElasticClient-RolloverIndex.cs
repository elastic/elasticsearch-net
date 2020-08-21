// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public partial interface IElasticClient
	{
		RolloverIndexResponse RolloverIndex(Name alias, Func<RolloverIndexDescriptor, IRolloverIndexRequest> selector = null);

		RolloverIndexResponse RolloverIndex(IRolloverIndexRequest request);

		Task<RolloverIndexResponse> RolloverIndexAsync(
			Name alias,
			Func<RolloverIndexDescriptor, IRolloverIndexRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		Task<RolloverIndexResponse> RolloverIndexAsync(IRolloverIndexRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		public RolloverIndexResponse RolloverIndex(Name alias, Func<RolloverIndexDescriptor, IRolloverIndexRequest> selector = null) =>
			RolloverIndex(selector.InvokeOrDefault(new RolloverIndexDescriptor(alias)));

		public RolloverIndexResponse RolloverIndex(IRolloverIndexRequest request) =>
			DoRequest<IRolloverIndexRequest, RolloverIndexResponse>(request, request.RequestParameters);

		public Task<RolloverIndexResponse> RolloverIndexAsync(
			Name alias,
			Func<RolloverIndexDescriptor, IRolloverIndexRequest> selector = null,
			CancellationToken cancellationToken = default
		) => RolloverIndexAsync(selector.InvokeOrDefault(new RolloverIndexDescriptor(alias)));

		public Task<RolloverIndexResponse> RolloverIndexAsync(IRolloverIndexRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IRolloverIndexRequest, RolloverIndexResponse>(request, request.RequestParameters, ct);
	}
}
