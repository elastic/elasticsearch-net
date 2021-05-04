// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;

namespace Tests.Connection.MetaData
{
	public class VersionInfoTests
	{
		[U] public void ToString_ReturnsExpectedValue_ForNonPrerelease()
		{
			var sut = new TestableVersionInfo("1.2.3");
			sut.ToString().Should().Be("1.2.3");
		}

		[U] public void ToString_ReturnsExpectedValue_ForPrerelease()
		{
			var sut = new TestableVersionInfo("1.2.3-beta-1");
			sut.ToString().Should().Be("1.2.3p");
		}

		[U]
		public void ToString_ReturnsExpectedValue_ForNullVersion()
		{
			var sut = new TestableVersionInfo(null);
			sut.ToString().Should().Be("0.0.0");
		}

		[U]
		public void ToString_ReturnsExpectedValue_ForInvalidVersion()
		{
			var sut = new TestableVersionInfo("NOT-A-VERSION-NUMBER");
			sut.ToString().Should().Be("0.0.0");
		}

		private class TestVersionInfo : VersionInfo
		{
			public TestVersionInfo(string version, bool isPrerelease)
			{
				Version = new Version(version);
				IsPrerelease = isPrerelease;
			}
		}

		private class TestableVersionInfo : VersionInfo
		{
			public TestableVersionInfo(string version) => StoreVersion(version);
		}
	}
}
