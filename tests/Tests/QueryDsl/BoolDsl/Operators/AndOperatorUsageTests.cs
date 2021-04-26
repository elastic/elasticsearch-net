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

using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;

namespace Tests.QueryDsl.BoolDsl.Operators
{
	public class AndOperatorUsageTests : OperatorUsageBase
	{
		[U] public void And()
		{
			ReturnsBool(Query && Query, q => q.Query() && q.Query(), b =>
			{
				b.Must.Should().NotBeEmpty().And.HaveCount(2);
				b.Should.Should().BeNull();
				b.MustNot.Should().BeNull();
				b.Filter.Should().BeNull();
			});

			ReturnsBool(Query && Query && ConditionlessQuery, q => q.Query() && q.Query() && q.ConditionlessQuery(), b =>
			{
				b.Must.Should().NotBeEmpty().And.HaveCount(2);
				b.Should.Should().BeNull();
				b.MustNot.Should().BeNull();
				b.Filter.Should().BeNull();
			});

			ReturnsSingleQuery(Query && ConditionlessQuery, q => q.Query() && q.ConditionlessQuery(),
				c => c.Term.Value.Should().NotBeNull());

			ReturnsSingleQuery(ConditionlessQuery && Query, q => q.ConditionlessQuery() && q.Query(),
				c => c.Term.Value.Should().NotBeNull());

			ReturnsSingleQuery(Query && NullQuery, q => q.Query() && q.NullQuery(),
				c => c.Term.Value.Should().NotBeNull());

			ReturnsSingleQuery(NullQuery && Query, q => q.NullQuery() && q.Query(),
				c => c.Term.Value.Should().NotBeNull());

			ReturnsSingleQuery(ConditionlessQuery && ConditionlessQuery && ConditionlessQuery && Query,
				q => q.ConditionlessQuery() && q.ConditionlessQuery() && q.ConditionlessQuery() && q.Query(),
				c => c.Term.Value.Should().NotBeNull());

			ReturnsSingleQuery(
				NullQuery && NullQuery && ConditionlessQuery && Query,
				q => q.NullQuery() && q.NullQuery() && q.ConditionlessQuery() && q.Query(),
				c => c.Term.Value.Should().NotBeNull());

			ReturnsNull(NullQuery && ConditionlessQuery, q => q.NullQuery() && q.ConditionlessQuery());
			ReturnsNull(ConditionlessQuery && NullQuery, q => q.ConditionlessQuery() && q.NullQuery());
			ReturnsNull(ConditionlessQuery && ConditionlessQuery, q => q.ConditionlessQuery() && q.ConditionlessQuery());
			ReturnsNull(
				ConditionlessQuery && ConditionlessQuery && ConditionlessQuery && ConditionlessQuery,
				q => q.ConditionlessQuery() && q.ConditionlessQuery() && q.ConditionlessQuery() && q.ConditionlessQuery()
			);
			ReturnsNull(
				NullQuery && ConditionlessQuery && ConditionlessQuery && ConditionlessQuery,
				q => q.NullQuery() && q.ConditionlessQuery() && q.ConditionlessQuery() && q.ConditionlessQuery()
			);
		}

		[U] public void CombiningManyUsingAggregate()
		{
			var lotsOfAnds = Enumerable.Range(0, 100).Aggregate(new QueryContainer(), (q, c) => q && Query, q => q);
			LotsOfAnds(lotsOfAnds);
		}

		[U] public void CombiningManyUsingForeachInitializingWithNull()
		{
			QueryContainer container = null;
			foreach (var i in Enumerable.Range(0, 100))
				container &= Query;
			LotsOfAnds(container);
		}

		[U] public void CombiningManyUsingForeachInitializingWithDefault()
		{
			var container = new QueryContainer();
			foreach (var i in Enumerable.Range(0, 100))
				container &= Query;
			LotsOfAnds(container);
		}

		private void LotsOfAnds(IQueryContainer lotsOfAnds, int iterations = 100)
		{
			lotsOfAnds.Should().NotBeNull();
			lotsOfAnds.Bool.Should().NotBeNull();
			lotsOfAnds.Bool.Must.Should().NotBeEmpty().And.HaveCount(iterations);
		}
	}
}
