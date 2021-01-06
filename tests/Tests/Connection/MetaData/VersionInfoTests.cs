// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;

namespace Tests.Core.Connection.MetaData
{
	public class VersionInfoTests
	{
		[U] public void ToString_ReturnsExpectedValue_ForNonPrerelease()
		{
			var sut = new TestVersionInfo("1.2.3", false);
			sut.ToString().Should().Be("1.2.3");
		}

		[U] public void ToString_ReturnsExpectedValue_ForPrerelease()
		{
			var sut = new TestVersionInfo("1.2.3", true);
			sut.ToString().Should().Be("1.2.3p");
		}

		private class TestVersionInfo : VersionInfo
		{
			public TestVersionInfo(string version, bool isPrerelease)
			{
				Version = new Version(version);
				IsPrerelease = isPrerelease;
			}
		}
	}
}
