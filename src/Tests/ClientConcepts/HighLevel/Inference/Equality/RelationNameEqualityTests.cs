using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;

namespace Tests.ClientConcepts.HighLevel.Inference.Equality
{
	public class RelationNameEqualityTests
	{
		[U] public void Eq()
		{
			RelationName name = "foo";
			RelationName[] equal = {"foo"};
			foreach (var t in equal)
			{
				(t == name).ShouldBeTrue(t);
				t.Should().Be(name);
			}
		}

		[U] public void NotEq()
		{
			RelationName name = "foo";
			RelationName[] notEqual = {"bar", "foo  ", "  foo   ", "x", "", "   ", typeof(Project)};
			foreach (var t in notEqual)
			{
				(t != name).ShouldBeTrue(t);
				t.Should().NotBe(name);
			}
		}

		[U] public void TypedEq()
		{
			RelationName t1 = typeof(Project), t2 = typeof(Project);
			(t1 == t2).ShouldBeTrue(t2);
		}

		[U] public void TypedNotEq()
		{
			RelationName t1 = typeof(Project), t2 = typeof(CommitActivity);
			(t1 != t2).ShouldBeTrue(t2);
		}
		[U] public void Null()
		{
			RelationName value = "foo";
			(value == null).Should().BeFalse();
			(null == value).Should().BeFalse();
		}
	}
}
