// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;
using Elastic.Transport.Products;
using Elastic.Transport.Products.Elasticsearch;

namespace Nest
{
	public class NestElasticsearchProductConfiguration : ElasticsearchProductRegistration
	{
		public static NestElasticsearchProductConfiguration DefaultForNest { get; } = new NestElasticsearchProductConfiguration();

		/// <summary>
		/// NEST handles 404 in its <see cref="ResponseBase.IsValid"/>, we do not want the low level client throwing exceptions
		/// when <see cref="ITransportConfigurationValues.ThrowExceptions"/> is enabled for 404's. The client is in charge of composing paths
		/// so a 404 never signals a wrong url but a missing entity.
		/// </summary>
		public override bool HttpStatusCodeClassifier(HttpMethod method, int statusCode) =>
			statusCode >= 200 && statusCode < 300
			|| statusCode == 404;


		/// <summary>
		/// Makes the low level transport aware of NEST's <see cref="ResponseBase"/>
		/// So that it can peek in to its exposed error when reporting failures
		/// </summary>
		public override bool TryGetServerErrorReason<TResponse>(TResponse response, out string reason)
		{
			if (response is ResponseBase r)
			{
				reason = r.ServerError?.Error?.ToString();
				return !reason.IsNullOrEmpty();
			}
			return base.TryGetServerErrorReason(response, out reason);
		}
	}
}
