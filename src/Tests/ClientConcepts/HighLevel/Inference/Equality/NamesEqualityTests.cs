using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.ClientConcepts.HighLevel.Inference.Equality
{
	public class ActionIdsEqualityTests
	{
		[U] public void Eq()
		{
			ActionIds types = "foo,bar";
			ActionIds[] equal = {"foo,bar", "bar,foo", "foo,  bar", "bar,  foo   "};
			foreach (var t in equal)
			{
				(t == types).ShouldBeTrue(t);
				t.Should().Be(types);
			}
		}

		[U] public void NotEq()
		{
			ActionIds types = "foo,bar";
			ActionIds[] notEqual = {"foo,bar,x", "foo" };
			foreach (var t in notEqual)
			{
				(t != types).ShouldBeTrue(t);
				t.Should().NotBe(types);
			}
		}

		[U] public void Null()
		{
			Names value = "foo";
			(value == null).Should().BeFalse();
			(null == value).Should().BeFalse();
		}
	}
}
