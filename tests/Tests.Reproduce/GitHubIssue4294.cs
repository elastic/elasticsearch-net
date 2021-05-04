// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;

namespace Tests.Reproduce
{
	public class GitHubIssue4294
	{
		[U] public void CanSerializeNullReferenceException()
		{
			var settings = new TransportConfiguration();

			var dic = new Dictionary<string, object>()
			{
				["Exception"] = new NullReferenceException(),
			};

			var data = PostData.Serializable(dic);
			using var ms = new MemoryStream();
			Action write = () => data.Write(ms, settings);

			write.Should().NotThrow();
		}
	}
}
