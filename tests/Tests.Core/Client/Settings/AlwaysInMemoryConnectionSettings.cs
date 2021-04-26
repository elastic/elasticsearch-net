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

		public AlwaysInMemoryConnectionSettings(byte[] bytes) : base(forceInMemory: true, response: bytes) { }

		public AlwaysInMemoryConnectionSettings(
			Func<ICollection<Uri>, IConnectionPool> createPool = null,
			SourceSerializerFactory sourceSerializerFactory = null,
			IPropertyMappingProvider propertyMappingProvider = null,
			int port = 9200
		)
			: base(
				createPool,
				sourceSerializerFactory,
				propertyMappingProvider,
				forceInMemory: true,
				port
			) { }
	}
}
