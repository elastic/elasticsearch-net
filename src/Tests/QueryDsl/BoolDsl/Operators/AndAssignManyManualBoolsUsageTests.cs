using System;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;

namespace Tests.QueryDsl.BoolDsl.Operators
{
	public class AndAssignManyManualBoolsUsageTests : OperatorUsageBase
	{
		private static QueryContainer ATermQuery(QueryContainerDescriptor<Project> must) => must.Term(p => p.Name, "foo");

		private static int Iterations = 10000;

		[U] public void AndAssigningManyBoolShouldQueries()
		{
			var q = Query<Project>.Bool(b => b.Should(ATermQuery));
			var container = AndAssignManyBoolQueries(q);
			DefaultMustAssert(container);
			container.Bool.Must.Cast<IQueryContainer>().Should().OnlyContain(s => s.Bool != null && s.Bool.Should != null);
		}

		[U] public void AndAssigningManyBoolMustNotQueries()
		{
			var q = Query<Project>.Bool(b => b.MustNot(ATermQuery));
			var container = AndAssignManyBoolQueries(q);
			container.Bool.MustNot.Should().NotBeEmpty().And.HaveCount(Iterations);
			container.Bool.MustNot.Cast<IQueryContainer>().Should().OnlyContain(s => s.Term != null);
		}

		[U] public void AndAssigningBoolMustQueries()
		{
			var q = Query<Project>.Bool(b => b.Must(ATermQuery));
			var container = AndAssignManyBoolQueries(q);
			DefaultMustAssert(container);
			container.Bool.Must.Cast<IQueryContainer>().Should().OnlyContain(s => s.Term != null);
		}

		[U] public void AndAssigningBoolShouldQueriesWithMustClauses()
		{
			var q = Query<Project>.Bool(b => b.Should(ATermQuery).Must(ATermQuery));
			var container = AndAssignManyBoolQueries(q);
			DefaultMustAssert(container);
			container.Bool.Must.Cast<IQueryContainer>().Should()
				.OnlyContain(s => s.Bool != null && s.Bool.Should != null && s.Bool.Must != null);
		}

		[U] public void AndAssigningBoolShouldQueriesWithMustNotClauses()
		{
			var q = Query<Project>.Bool(b => b.Should(ATermQuery).MustNot(ATermQuery));
			var container = AndAssignManyBoolQueries(q);
			DefaultMustAssert(container);
			container.Bool.Must.Cast<IQueryContainer>().Should()
				.OnlyContain(s => s.Bool != null && s.Bool.Should != null && s.Bool.MustNot != null);
		}

		[U] public void AndAssigningBoolMustQueriesWithMustNotClauses()
		{
			var q = Query<Project>.Bool(b => b.Must(ATermQuery).MustNot(ATermQuery));
			var container = AndAssignManyBoolQueries(q);
			DefaultMustAssert(container);
			container.Bool.MustNot.Should().NotBeEmpty().And.HaveCount(Iterations);
			container.Bool.Must.Cast<IQueryContainer>().Should().OnlyContain(s => s.Term != null);
		}

		[U] public void AndAssigningNamedBoolMustQueries()
		{
			var q = Query<Project>.Bool(b => b.Must(ATermQuery).Name("name"));
			var container = AndAssignManyBoolQueries(q);
			DefaultMustAssert(container);
			container.Bool.Must.Cast<IQueryContainer>().Should()
				.OnlyContain(s => s.Bool != null && s.Bool.Must != null && s.Bool.Name == "name");
		}

		private IQueryContainer AndAssignManyBoolQueries(QueryContainer q)
		{
			var container = new QueryContainer();
			Action act = () =>
			{
				for (int i = 0; i < Iterations; i++) container &= q;
			};
			act.ShouldNotThrow();
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
