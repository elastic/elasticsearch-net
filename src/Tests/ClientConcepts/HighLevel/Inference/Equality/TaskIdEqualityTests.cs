using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.ClientConcepts.HighLevel.Inference.Equality
{
	public class TaskIdEqualityTests
	{
		[U] public void Eq()
		{
			TaskId types = "node:1337";
			TaskId[] equal = {"node:1337"};
			foreach (var t in equal)
			{
				(t == types).ShouldBeTrue(t);
				t.Should().Be(types);
			}
		}

		[U] public void NotEq()
		{
			TaskId types = "node:1337";
			TaskId[] notEqual = {"node:1338", "  node:1337", "node:1337   ", "node:133", "node2:1337"};
			foreach (var t in notEqual)
			{
				(t != types).ShouldBeTrue(t);
				t.Should().NotBe(types);
			}
		}

		[U] public void Null()
		{
			TaskId value = "node:1339";
			(value == null).Should().BeFalse();
			(null == value).Should().BeFalse();
		}
	}
}
