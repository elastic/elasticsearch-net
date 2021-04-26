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

using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Newtonsoft.Json;
using Tests.Core.Serialization;

namespace Tests.Reproduce
{
	public class GithubIssue3743
	{
		[U]
		public void SerializesUnicodeEscapeSequences()
		{
			var doc = new { value = new string(Enumerable.Range(0, 9727).Select(i => (char)i).ToArray()) };

			var internalJson = SerializationTester.Default.Client.SourceSerializer.SerializeToString(doc, formatting: SerializationFormatting.None);
			var jsonNet = JsonConvert.SerializeObject(doc, Formatting.None);

			// json.net lowercases unicode escape sequences, utf8json uppercases them (faster op). Both are valid and accepted
			internalJson.Should().BeEquivalentTo(jsonNet);
		}
	}
}
