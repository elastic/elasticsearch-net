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

using System;
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Domain;

namespace Tests.QueryDsl.BoolDsl.Operators
{
	//hide
	public class AndAssignManyManualBoolsTests : OperatorUsageBase
	{
		private static readonly int Iterations = 10000;

		private static QueryContainer ATermQuery(QueryContainerDescriptor<Project> must) => must.Term(p => p.Name, "foo");

		private void AssertBoolQuery(QueryContainer q, Action<IBoolQuery> assert) =>
			assert(((IQueryContainer)q).Bool);

		[U] public void AndAssigningManyBoolShouldQueries()
		{
			var q = Query<Project>.Bool(b => b.Should(ATermQuery));
			var container = AndAssignManyBoolQueries(q);
			DefaultMustAssert(container);
			container.Bool.Must.Cast<IQueryContainer>().Should().OnlyContain(s => s.Bool != null && s.Bool.Should != null);
			AssertBoolQuery(q, b => b.Should.Should().NotBeNullOrEmpty());
		}

		[U] public void AndAssigningManyBoolMustNotQueries()
		{
			var q = Query<Project>.Bool(b => b.MustNot(ATermQuery));
			var container = AndAssignManyBoolQueries(q);
			container.Bool.MustNot.Should().NotBeEmpty().And.HaveCount(Iterations);
			container.Bool.MustNot.Cast<IQueryContainer>().Should().OnlyContain(s => s.Term != null);
			AssertBoolQuery(q, b => b.MustNot.Should().NotBeNullOrEmpty());
		}

		[U] public void AndAssigningBoolMustQueries()
		{
			var q = Query<Project>.Bool(b => b.Must(ATermQuery));
			var container = AndAssignManyBoolQueries(q);
			DefaultMustAssert(container);
			container.Bool.Must.Cast<IQueryContainer>().Should().OnlyContain(s => s.Term != null);
			AssertBoolQuery(q, b => b.Must.Should().NotBeNullOrEmpty());
		}

		[U] public void AndAssigningBoolShouldQueriesWithMustClauses()
		{
			var q = Query<Project>.Bool(b => b.Should(ATermQuery).Must(ATermQuery));
			var container = AndAssignManyBoolQueries(q);
			DefaultMustAssert(container);
			container.Bool.Must.Cast<IQueryContainer>()
				.Should()
				.OnlyContain(s => s.Bool != null && s.Bool.Should != null && s.Bool.Must != null);
			AssertBoolQuery(q, b =>
			{
				b.Should.Should().NotBeNullOrEmpty();
				b.Must.Should().NotBeNullOrEmpty();
			});
		}

		[U] public void AndAssigningBoolShouldQueriesWithMustNotClauses()
		{
			var q = Query<Project>.Bool(b => b.Should(ATermQuery).MustNot(ATermQuery));
			var container = AndAssignManyBoolQueries(q);
			DefaultMustAssert(container);
			container.Bool.Must.Cast<IQueryContainer>()
				.Should()
				.OnlyContain(s => s.Bool != null && s.Bool.Should != null && s.Bool.MustNot != null);
			AssertBoolQuery(q, b =>
			{
				b.Should.Should().NotBeNullOrEmpty();
				b.MustNot.Should().NotBeNullOrEmpty();
			});
		}

		[U] public void AndAssigningBoolMustQueriesWithMustNotClauses()
		{
			var q = Query<Project>.Bool(b => b.Must(ATermQuery).MustNot(ATermQuery));
			var container = AndAssignManyBoolQueries(q);
			DefaultMustAssert(container);
			container.Bool.MustNot.Should().NotBeEmpty().And.HaveCount(Iterations);
			container.Bool.Must.Cast<IQueryContainer>().Should().OnlyContain(s => s.Term != null);
			AssertBoolQuery(q, b =>
			{
				b.Must.Should().NotBeNullOrEmpty();
				b.MustNot.Should().NotBeNullOrEmpty();
			});
		}

		[U] public void AndAssigningNamedBoolMustQueries()
		{
			var q = Query<Project>.Bool(b => b.Must(ATermQuery).Name("name"));
			var container = AndAssignManyBoolQueries(q);
			DefaultMustAssert(container);
			container.Bool.Must.Cast<IQueryContainer>()
				.Should()
				.OnlyContain(s => s.Bool != null && s.Bool.Must != null && s.Bool.Name == "name");
			AssertBoolQuery(q, b =>
			{
				b.Must.Should().NotBeNullOrEmpty();
				b.Name.Should().NotBeNullOrEmpty();
			});
		}

		private IQueryContainer AndAssignManyBoolQueries(QueryContainer q)
		{
			var container = new QueryContainer();
			Action act = () =>
			{
				for (var i = 0; i < Iterations; i++) container &= q;
			};
			act.Should().NotThrow();
			return container;
		}

		private void DefaultMustAssert(IQueryContainer lotsOfAnds)
		{
			lotsOfAnds.Should().NotBeNull();
			lotsOfAnds.Bool.Should().NotBeNull();
			lotsOfAnds.Bool.Must.Should().NotBeEmpty().And.HaveCount(Iterations);
		}
	}
}
