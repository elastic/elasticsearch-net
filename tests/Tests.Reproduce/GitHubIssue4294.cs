using System;
using System.Collections.Generic;
using System.IO;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;

namespace Tests.Reproduce
{
	public class GitHubIssue4294
	{
		[U] public void CanSerializeNullReferenceException()
		{
			var settings = new ConnectionConfiguration();

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
