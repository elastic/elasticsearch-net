/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
		/// Access the configured <see cref="ITransportConfiguration.RequestResponseSerializer" />
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
