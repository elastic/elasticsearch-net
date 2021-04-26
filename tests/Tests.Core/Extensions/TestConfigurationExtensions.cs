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

using Elasticsearch.Net;
using Tests.Configuration;

namespace Tests.Core.Extensions
{
	public static class TestConfigurationExtensions
	{
		public static IConnection CreateConnection(this TestConfigurationBase configuration, bool forceInMemory = false, byte[] response = null) =>
			forceInMemory
				? new InMemoryConnection(response)
				: configuration.RunIntegrationTests
					? (IConnection)new HttpConnection()
					: new InMemoryConnection(response);

		public static bool InRange(this TestConfigurationBase configuration, string range) =>
			configuration.ElasticsearchVersion.InRange(range);
	}
}
