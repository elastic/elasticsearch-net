using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.ClientConcepts.HighLevel.Inference.Equality
{
	public class CategoryIdEqualityTests
	{
		[U] public void Eq()
		{
			CategoryId types = 2;
			CategoryId[] equal = {2L, 2};
			foreach (var t in equal)
			{
				(t == types).ShouldBeTrue(t);
				t.Should().Be(types);
			}

			CategoryId l1 = 2, l2 = 2;
			(l1 == l2).ShouldBeTrue(l2);
			(l1 == 2).ShouldBeTrue(l1);
			l1.Should().Be(l2);
			l1.Should().Be(2);
		}

		[U] public void NotEq()
		{
			CategoryId types = 3;
			CategoryId[] notEqual = {4L, 4};
			foreach (var t in notEqual)
			{
				(t != types).ShouldBeTrue(t);
				t.Should().NotBe(types);
			}
			CategoryId l1 = 2, l2 = 3;
			(l1 != l2).ShouldBeTrue(l2);
			(l1 != 3).ShouldBeTrue(l1);
			l1.Should().NotBe(l2);
			l1.Should().NotBe(3);
		}

		[U] public void Null()
		{
			CategoryId c = 10;
			(c == null).Should().BeFalse();
			(null == c).Should().BeFalse();
		}
	}
}
