// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;

namespace Nest
{
	/// <summary>
	/// ElasticClient is NEST's strongly typed client which exposes fully mapped Elasticsearch endpoints.
	/// </summary>
	/// <remarks>
	/// It is generally expected to be used as a singleton instance for the lifetime of the host application.
	/// </remarks>
	public partial interface IElasticClient
	{
		/// <summary>
		/// The configured connection settings for the client.
		/// </summary>
		IConnectionSettingsValues ConnectionSettings { get; }

		/// <summary>
		/// Access to the <see cref="Inferrer" /> that this instance of the client uses to resolve types to e.g
		/// indices, property, field names.
		/// </summary>
		Inferrer Infer { get; }
		
		/// <summary>
		/// Access the configured <see cref="ITransportConfiguration.RequestResponseSerializer" />
		/// Out of the box <see cref="SourceSerializer" /> and this point to the same instance.
		/// </summary>
		ITransportSerializer RequestResponseSerializer { get; }

		/// <summary>
		/// Access the configured <see cref="IConnectionSettingsValues.SourceSerializer" />
		/// Out of the box <see cref="RequestResponseSerializer" /> and this point to the same instance.
		/// </summary>
		ITransportSerializer SourceSerializer { get; }
	}
}
