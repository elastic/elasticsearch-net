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
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.Serialization;

namespace Tests.Reproduce
{
	public class GithubIssue3356
	{
		[U]
		public void JoinFieldDeserializedCorrectly()
		{
			var doc = new MyDocument
			{
				Join = JoinField.Root("parent")
			};

			var tester = SerializationTester.DefaultWithJsonNetSerializer;
			var response = tester.Client.IndexDocument(doc);

			tester.AssertSerialize(response.ApiCall.RequestBodyInBytes, new { join = "parent" });
			doc = tester.AssertDeserialize<MyDocument>(response.ApiCall.RequestBodyInBytes);

			doc.Join.Match(
				p => { p.Name.Should().Be("parent"); },
				c => throw new InvalidOperationException("should not be called"));
		}

		private class MyDocument
		{
			public JoinField Join { get; set; }
		}
	}
}
