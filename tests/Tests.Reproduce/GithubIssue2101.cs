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
