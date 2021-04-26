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

namespace Tests.QueryDsl.BoolDsl.Operators
{
	public class CombinationUsageTests : OperatorUsageBase
	{
		[U] public void DoesNotJoinTwoShouldsUsingAnd() => ReturnsBool(
			(Query || Query) && (Query || Query),
			q => (q.Query() || q.Query()) && (q.Query() || q.Query()),
			b =>
			{
				b.Must.Should().NotBeEmpty().And.HaveCount(2);
				b.Should.Should().BeNull();
				b.MustNot.Should().BeNull();
				b.Filter.Should().BeNull();
			});

		[U] public void DoesJoinTwoShouldsUsingOr() => ReturnsBool(
			Query || Query || (Query || Query),
			q => q.Query() || q.Query() || (q.Query() || q.Query()),
			b =>
			{
				b.Should.Should().NotBeEmpty().And.HaveCount(4);
				b.Must.Should().BeNull();
				b.MustNot.Should().BeNull();
				b.Filter.Should().BeNull();
			});

		[U] public void DoesNotJoinTwoMustsUsingOr() => ReturnsBool(
			Query && Query || Query && Query,
			q => q.Query() && q.Query() || q.Query() && q.Query(),
			b =>
			{
				b.Should.Should().NotBeEmpty().And.HaveCount(2);
				b.Must.Should().BeNull();
				b.MustNot.Should().BeNull();
				b.Filter.Should().BeNull();
			});

		[U] public void DoesJoinTwoMustsUsingAnd() => ReturnsBool(
			Query && Query && (Query && Query),
			q => q.Query() && q.Query() && (q.Query() && q.Query()),
			b =>
			{
				b.Must.Should().NotBeEmpty().And.HaveCount(4);
				b.Should.Should().BeNull();
				b.MustNot.Should().BeNull();
				b.Filter.Should().BeNull();
			});

		[U] public void AndJoinsMustNot() => ReturnsBool(
			Query && !Query,
			q => q.Query() && !q.Query(),
			b =>
			{
				b.Must.Should().NotBeEmpty().And.HaveCount(1);
				b.MustNot.Should().NotBeEmpty().And.HaveCount(1);
			});

		[U] public void OrDoesNotJoinMustNot() => ReturnsBool(
			Query || !Query,
			q => q.Query() || !q.Query(),
			b => { b.Should.Should().NotBeEmpty().And.HaveCount(2); });

		[U] public void OrDoesNotJoinFilter() => ReturnsBool(
			Query || !Query,
			q => q.Query() || +q.Query(),
			b =>
			{
				b.Should.Should().NotBeEmpty().And.HaveCount(2);
				b.Filter.Should().BeNull();
			});

		[U] public void AndJoinsFilter() => ReturnsBool(
			Query && +Query,
			q => q.Query() && +q.Query(),
			b =>
			{
				b.Must.Should().NotBeEmpty().And.HaveCount(1);
				b.Filter.Should().NotBeEmpty().And.HaveCount(1);
			});
	}
}
