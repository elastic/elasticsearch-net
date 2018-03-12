using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;

namespace Tests.ClientConcepts.HighLevel.Inference.Equality
{
	public class IndexNameEqualityTests
	{
		[U] public void Eq()
		{
			IndexName types = "foo";
			IndexName[] equal = {"foo"};
			foreach (var t in equal)
			{
				(t == types).ShouldBeTrue(t);
				t.Should().Be(types);
			}
		}

		[U] public void NotEq()
		{
			IndexName types = "foo";
			IndexName[] notEqual = {"bar", "foo  ", "  foo   ", "x", "", "   ", typeof(Project)};
			foreach (var t in notEqual)
			{
				(t != types).ShouldBeTrue(t);
				t.Should().NotBe(types);
			}
		}
		[U] public void EqCluster()
		{
			IndexName types = "c:foo";
			IndexName[] equal = {"c:foo"};
			foreach (var t in equal)
			{
				(t == types).ShouldBeTrue(t);
				t.Should().Be(types);
			}
		}

		[U] public void NotEqCluster()
		{
			IndexName types = "c:foo";
			IndexName[] notEqual = {"c1:bar", "c:foo  ", "  c:foo   ", "c:foo1", "x", "", "   ", typeof(Project) };
			foreach (var t in notEqual)
			{
				(t != types).ShouldBeTrue(t);
				t.Should().NotBe(types);
			}
		}

		[U] public void IndexdEq()
		{
			IndexName t1 = typeof(Project), t2 = typeof(Project);
			(t1 == t2).ShouldBeTrue(t2);
		}

		[U] public void IndexdNotEq()
		{
			IndexName t1 = typeof(Project), t2 = typeof(CommitActivity);
			(t1 != t2).ShouldBeTrue(t2);
		}

		[U] public void Null()
		{
			IndexName value = typeof(Project);
			(value == null).Should().BeFalse();
			(null == value).Should().BeFalse();
		}
	}
}
