using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.ClientConcepts.HighLevel.Inference.Equality
{
	public class NameEqualityTests
	{
		[U] public void Eq()
		{
			Name types = "foo";
			Name[] equal = {"foo", "  foo", "foo   "};
			foreach (var t in equal)
			{
				(t == types).ShouldBeTrue(t);
				t.Should().Be(types);
			}
		}

		[U] public void NotEq()
		{
			Name types = "foo";
			Name[] notEqual = {"bar", "" };
			foreach (var t in notEqual)
			{
				(t != types).ShouldBeTrue(t);
				t.Should().NotBe(types);
			}
		}

		[U] public void Null()
		{
			Name value = "foo";
			(value == null).Should().BeFalse();
			(null == value).Should().BeFalse();
		}
	}
}
