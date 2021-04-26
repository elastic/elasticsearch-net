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
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Domain;

namespace Tests.Core.Extensions
{
	public static class ShouldExtensions
	{
		public static void ShouldHaveExpectedIsValid(this IResponse response, bool expectedIsValid) =>
			response.IsValid.Should().Be(expectedIsValid, "{0}", response.DebugInformation);

		public static void ShouldBeValid(this IResponse response) =>
			response.IsValid.Should().BeTrue("{0}", response.DebugInformation);

		public static void ShouldBeValid(this IResponse response, string message) =>
			response.IsValid.Should().BeTrue("{1} {0}", response.DebugInformation, message);

		public static void ShouldNotBeValid(this IResponse response) =>
			response.IsValid.Should().BeFalse("{0}", response.DebugInformation);

		public static void ShouldBeSuccess(this IResponse response) =>
			response.ApiCall.Success.Should().BeTrue("{0}", response.DebugInformation);


		public static void ShouldAdhereToSourceSerializerWhenSet(this Project project)
		{
			var usingSourceSerializer = TestClient.Configuration.Random.SourceSerializer;
			project.Should().NotBeNull();
			if (!usingSourceSerializer)
			{
				project.SourceOnly.Should().BeNull();
				return;
			}
			project.SourceOnly.Should().NotBeNull();
			project.SourceOnly.NotWrittenByDefaultSerializer.Should().Be("written");
			project.SourceOnly.NotReadByDefaultSerializer.Should().Be("read");
		}

		public static void ShouldBeTrue(this bool b, IUrlParameter p) =>
			b.Should().BeTrue(p?.GetString(TestClient.DefaultInMemoryClient.ConnectionSettings) ?? "NULL");
	}
}
