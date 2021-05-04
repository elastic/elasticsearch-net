// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;

namespace Tests.Reproduce
{
	public class GitHubIssue4818
	{
		[U]
		public void ConnectionSettingsDoesNotThrowNullReferenceException()
		{
			IDisposable settings = new ConnectionSettings();
			Action func = () => settings.Dispose();
			func.Should().NotThrow<NullReferenceException>();
		}
	}
}
