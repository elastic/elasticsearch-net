using System;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The configured connection settings for the client
		/// </summary>
		IConnectionSettingsValues ConnectionSettings { get; }

		/// <summary>
		/// Access the configured <see cref="IConnectionSettingsValues.SourceSerializer"/>
		/// </summary>
		IElasticsearchSerializer Serializer { get; }

		/// <summary>
		/// An instance of the low level client that uses the serializers from the highlevel client.
		/// </summary>
		IElasticLowLevelClient LowLevel { get; }

		/// <summary>
		/// Access to the <see cref="Inferrer"/> that this instance of the client uses to resolve types to e.g
		/// indices, property, field names
		/// </summary>
		Inferrer Infer { get; }
	}
}
