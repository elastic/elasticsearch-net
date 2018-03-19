using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;

namespace Tests.ClientConcepts.HighLevel.Inference.Equality
{
	public class TypeNameEqualityTests
	{
		[U] public void Eq()
		{
			TypeName types = "foo";
			TypeName[] equal = {"foo"};
			foreach (var t in equal)
			{
				(t == types).ShouldBeTrue(t);
				t.Should().Be(types);
			}
		}

		[U] public void NotEq()
		{
			TypeName types = "foo";
			TypeName[] notEqual = {"bar", "foo  ", "  foo   ", "x", "", "   ", typeof(Project)};
			foreach (var t in notEqual)
			{
				(t != types).ShouldBeTrue(t);
				t.Should().NotBe(types);
			}
		}

		[U] public void TypedEq()
		{
			TypeName t1 = typeof(Project), t2 = typeof(Project);
			(t1 == t2).ShouldBeTrue(t2);
		}

		[U] public void TypedNotEq()
		{
			TypeName t1 = typeof(Project), t2 = typeof(CommitActivity);
			(t1 != t2).ShouldBeTrue(t2);
		}

		[U] public void Null()
		{
			TypeName value = "foo";
			(value == null).Should().BeFalse();
			(null == value).Should().BeFalse();
		}
	}
}
