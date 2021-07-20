using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch;

namespace Nest
{
	internal sealed class NestElasticsearchProductRegistration : ElasticsearchProductRegistration
	{
		public static NestElasticsearchProductRegistration DefaultForNest { get; } = new();

		/// <summary>
		///     NEST handles 404 in its <see cref="ResponseBase.IsValid" />, we do not want the low level client throwing
		///     exceptions
		///     when <see cref="ITransportConfiguration.ThrowExceptions" /> is enabled for 404's. The client is in charge of
		///     composing paths
		///     so a 404 never signals a wrong URL but a missing entity.
		/// </summary>
		public override bool HttpStatusCodeClassifier(HttpMethod method, int statusCode) =>
			statusCode is >= 200 and < 300 or 404;

		/// <summary>
		///     Makes the low level transport aware of NEST's <see cref="ResponseBase" />
		///     so that it can peek in to its exposed error when reporting failures.
		/// </summary>
		public override bool TryGetServerErrorReason<TResponse>(TResponse response, out string? reason)
		{
			if (response is not ResponseBase r) return base.TryGetServerErrorReason(response, out reason);
			reason = r.ServerError?.Error?.ToString();
			return !string.IsNullOrEmpty(reason);
		}
	}
}
