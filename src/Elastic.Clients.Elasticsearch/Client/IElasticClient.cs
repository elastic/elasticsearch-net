using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	/// <summary>
	///     ElasticClient is Elastic.Clients.Elasticsearch's strongly typed client which exposes fully mapped Elasticsearch endpoints.
	/// </summary>
	/// <remarks>
	///     It is generally expected to be used as a singleton instance for the lifetime of the host application.
	/// </remarks>
	public partial interface IElasticClient
	{
		/// <summary>
		///     The configured connection settings for the client.
		/// </summary>
		IElasticsearchClientSettings ElasticsearchClientSettings { get; }

		/// <summary>
		///     Access to the <see cref="Inferrer" /> that this instance of the client uses to resolve types to e.g
		///     indices, property, field names.
		/// </summary>
		Inferrer Infer { get; }

		/// <summary>
		///     Access the configured <see cref="ITransportConfiguration.RequestResponseSerializer" />
		///     Out of the box <see cref="SourceSerializer" /> and this point to the same instance.
		/// </summary>
		Serializer RequestResponseSerializer { get; }

		/// <summary>
		///     Access the configured <see cref="IElasticsearchClientSettings.SourceSerializer" />
		///     Out of the box <see cref="RequestResponseSerializer" /> and this point to the same instance.
		/// </summary>
		Serializer SourceSerializer { get; }
	}
}
