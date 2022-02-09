// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace Tests.Core.Client.Settings
{
	/// <summary>
	///     ConnectionSettings sub class that is always in memory no matter if we are running in integration or unit test mode.
	///     Only use this if you expect a test to run in integration mode and you want to forcefully still use an in memory
	///     connection
	/// </summary>
	public class AlwaysInMemoryElasticsearchClientSettings : TestElasticsearchClientSettings
	{
		public AlwaysInMemoryElasticsearchClientSettings() : base(forceInMemory: true) { }

		public AlwaysInMemoryElasticsearchClientSettings(byte[] bytes) : base(forceInMemory: true, response: bytes) { }

		public AlwaysInMemoryElasticsearchClientSettings(
			Func<ICollection<Uri>, NodePool> createPool = null,
			SourceSerializerFactory sourceSerializerFactory = null,
			IPropertyMappingProvider propertyMappingProvider = null,
			int port = 9200
		)
			: base(
				createPool,
				sourceSerializerFactory,
				propertyMappingProvider,
				true,
				port
			)
		{
		}
	}
}
