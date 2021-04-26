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

using System.Collections.Generic;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Tests.Core.Client;
using Tests.Core.Extensions;

namespace Tests.Ingest.GrokProcessorPatterns
{
	public class GrokProcessorPatternsUnitTests
	{
		[U]
		public void ShouldDeserialize()
		{
			var fixedResponse = new
			{
				patterns = new Dictionary<string, object>
				{
					{ "BACULA_CAPACITY", "%{INT}{1,3}(,%{INT}{3})*" },
					{ "PATH", "(?:%{UNIXPATH}|%{WINPATH})" }
				}
			};

			var client = FixedResponseClient.Create(fixedResponse);

			//warmup
			var response = client.Ingest.GrokProcessorPatterns();
			response.ShouldBeValid();

			response.Patterns.Should().NotBeNull();
			response.Patterns.Should().HaveCount(2);
			response.Patterns.Should().ContainKey("BACULA_CAPACITY");
			response.Patterns.Should().ContainKey("PATH");
		}
	}
}
