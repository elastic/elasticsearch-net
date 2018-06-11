using System;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;

namespace Tests.Reproduce
{
	public class GithubIssue3220
	{
		[U] public void CanExplicitCastTimeFromString()
		{
			var searchRequest = new SearchRequest<Project> { Scroll = "1s" };

			Action getScroll = () => { var scroll = searchRequest.Scroll; };

			getScroll.ShouldNotThrow();
		}
	}
}
