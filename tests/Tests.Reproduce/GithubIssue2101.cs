// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;

namespace Tests.Reproduce
{
	public class GithubIssue2101
	{
		[U]
		public void BoolClausesShouldEvaluateOnlyOnce()
		{
			var must = 0;
			var mustNot = 0;
			var should = 0;
			var filter = 0;

			new BoolQueryDescriptor<object>()
				.Must(m =>
				{
					must++;
					return m;
				})
				.MustNot(mn =>
				{
					mustNot++;
					return mn;
				})
				.Should(sh =>
				{
					should++;
					return sh;
				})
				.Filter(f =>
				{
					filter++;
					return f;
				});

			filter.Should().Be(1);
			should.Should().Be(1);
			must.Should().Be(1);
			mustNot.Should().Be(1);
		}
	}
}
