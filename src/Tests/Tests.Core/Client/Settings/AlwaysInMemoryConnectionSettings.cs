using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;

namespace Tests.Core.Client.Settings
{
	/// <summary>
	/// ConnectionSettings sub class that is always in memory no matter if we are running in integration or unit test mode.
	/// Only use this if you expect a test to run in integration mode and you want to forcefully still use an in memory connection
	/// </summary>
	public class AlwaysInMemoryConnectionSettings : TestConnectionSettings
	{
		public AlwaysInMemoryConnectionSettings() : base(forceInMemory: true) { }

		public AlwaysInMemoryConnectionSettings(
			Func<ICollection<Uri>, IConnectionPool> createPool = null,
			SourceSerializerFactory sourceSerializerFactory = null,
			IPropertyMappingProvider propertyMappingProvider = null,
			int port = 9200)
			: base(
				createPool,
				sourceSerializerFactory,
				propertyMappingProvider,
				forceInMemory: true,
				port: port
			) { }
	}
}