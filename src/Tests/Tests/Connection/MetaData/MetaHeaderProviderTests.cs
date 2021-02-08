// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Text.RegularExpressions;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;

namespace Tests.Core.Connection.MetaData
{
	public class MetaHeaderProviderTests
	{
		private readonly Regex _validHeaderRegex = new Regex(@"^[a-z]{1,}=[a-z0-9\.\-]{1,}(?:,[a-z]{1,}=[a-z0-9\.\-]+)*$");
		private readonly Regex _validVersionRegex = new Regex(@"^[0-9]{1,2}\.[0-9]{1,2}(?:\.[0-9]{1,3})?p?$");
		private readonly Regex _validHttpClientPart = new Regex(@"^[a-z]{2,3}=[0-9]{1,2}\.[0-9]{1,2}(?:\.[0-9]{1,3})?p?$");

		[U] public void HeaderName_ReturnsExpectedValue()
		{
			var sut = new MetaHeaderProvider();
			sut.HeaderName.Should().Be("x-elastic-client-meta");
		}

		[U] public void HeaderName_ReturnsNullWhenDisabled()
		{
			var sut = new MetaHeaderProvider();

			var connectionSettings = new ConnectionSettings()
				.DisableMetaHeader(true);

			var requestData = new RequestData(HttpMethod.POST, "/_search", "{}", connectionSettings,
				new SearchRequestParameters(),
				new RecyclableMemoryStreamFactory());

			sut.ProduceHeaderValue(requestData).Should().BeNull();
		}

		[U] public void HeaderName_ReturnsExpectedValue_ForSyncRequest_WhenNotDisabled()
		{
			var sut = new MetaHeaderProvider();

			var connectionSettings = new ConnectionSettings();

			var requestData = new RequestData(HttpMethod.POST, "/_search", "{}", connectionSettings,
				new SearchRequestParameters(),
				new RecyclableMemoryStreamFactory())
			{
				IsAsync = false
			};

			var result = sut.ProduceHeaderValue(requestData);

			_validHeaderRegex.Match(result).Success.Should().BeTrue();

			var parts = result.Split(',');
			parts.Length.Should().Be(4);

			parts[0].Should().StartWith("es=");
			var clientVersion = parts[0].Substring(3);
			_validVersionRegex.Match(clientVersion).Success.Should().BeTrue();

			parts[1].Should().Be("a=0");

			parts[2].Should().StartWith("net=");
			var runtimeVersion = parts[2].Substring(4);
			_validVersionRegex.Match(runtimeVersion).Success.Should().BeTrue();

			_validHttpClientPart.Match(parts[3]).Success.Should().BeTrue();
		}

		[U] public void HeaderName_ReturnsExpectedValue_ForAsyncRequest_WhenNotDisabled()
		{
			var sut = new MetaHeaderProvider();

			var connectionSettings = new ConnectionSettings();

			var requestData = new RequestData(HttpMethod.POST, "/_search", "{}", connectionSettings,
				new SearchRequestParameters(),
				new RecyclableMemoryStreamFactory())
			{
				IsAsync = true
			};

			var result = sut.ProduceHeaderValue(requestData);

			_validHeaderRegex.Match(result).Success.Should().BeTrue();

			var parts = result.Split(',');
			parts.Length.Should().Be(4);

			parts[0].Should().StartWith("es=");
			var clientVersion = parts[0].Substring(3);
			_validVersionRegex.Match(clientVersion).Success.Should().BeTrue();

			parts[1].Should().Be("a=1");

			parts[2].Should().StartWith("net=");
			var runtimeVersion = parts[2].Substring(4);
			_validVersionRegex.Match(runtimeVersion).Success.Should().BeTrue();

			_validHttpClientPart.Match(parts[3]).Success.Should().BeTrue();
		}
	}
}