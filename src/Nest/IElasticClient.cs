// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;
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
		/// Access to the <see cref="Inferrer" /> that this instance of the client uses to resolve types to e.g
		/// indices, property, field names
		/// </summary>
		Inferrer Infer { get; }

		/// <summary>
		/// An instance of the low level client that uses the serializers from the highlevel client.
		/// </summary>
		IElasticLowLevelClient LowLevel { get; }

		/// <summary>
		/// Access the configured <see cref="ITransportConfigurationValues.RequestResponseSerializer" />
		/// Out of the box <see cref="SourceSerializer" /> and this point to the same instance
		/// </summary>
		ITransportSerializer RequestResponseSerializer { get; }

		/// <summary>
		/// Access the configured <see cref="IConnectionSettingsValues.SourceSerializer" />
		/// Out of the box <see cref="RequestResponseSerializer" /> and this point to the same instance
		/// </summary>
		ITransportSerializer SourceSerializer { get; }
	}
}
