using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.ClientConcepts.HighLevel.Inference.Equality
{
	public class RoutingEqualityTests
	{
		[U] public void Eq()
		{
			Routing types = "foo";
			Routing[] equal = {"foo"};
			foreach (var t in equal)
			{
				(t == types).ShouldBeTrue(t);
				t.Should().Be(types);
			}

			Routing l1 = 2, l2 = 2;
			(l1 == l2).ShouldBeTrue(l2);
			(l1 == 2).ShouldBeTrue(l1);
			(l1 == "2").ShouldBeTrue(l1);
			l1.Should().Be(l2);
			l1.Should().Be("2");
			l1.Should().Be(2);

			l1 = "foo, bar"; l2 = "bar,   foo";
			(l1 == l2).ShouldBeTrue(l2);
			l1.Should().Be(l2);

		}

		[U] public void NotEq()
		{
			Routing types = "foo";
			Routing[] notEqual = {"bar", "" , "foo  "};
			foreach (var t in notEqual)
			{
				(t != types).ShouldBeTrue(t);
				t.Should().NotBe(types);
			}
			Routing l1 = 2, l2 = 3;
			(l1 != l2).ShouldBeTrue(l2);
			(l1 != 3).ShouldBeTrue(l1);
			(l1 != "3").ShouldBeTrue(l1);
			l1.Should().NotBe(l2);
			l1.Should().NotBe(3);
			l1.Should().NotBe("3");

			l1 = "foo, bar"; l2 = "bar,   foo, x";
			(l1 != l2).ShouldBeTrue(l2);
			l1.Should().NotBe(l2);

			l1 = "foo, bar"; l2 = "bar";
			(l1 != l2).ShouldBeTrue(l2);
			l1.Should().NotBe(l2);
		}

		[U] public void Null()
		{
			Routing value = "foo";
			(value == null).Should().BeFalse();
			(null == value).Should().BeFalse();
		}
	}
}
