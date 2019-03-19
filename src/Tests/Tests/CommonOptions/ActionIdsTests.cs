using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;

namespace Tests.CommonOptions
{
	public class ActionIdsTests
	{
		[U] public void Equal()
		{
			var actionIds1 = new ActionIds("1,2,3");
			var actionIds2 = new ActionIds(new [] { "3", "2", "1" });

			actionIds1.Should().Be(actionIds2);
			actionIds1.GetHashCode().Should().Be(actionIds2.GetHashCode());
		}

		[U] public void NotEqual()
		{
			var actionIds1 = new ActionIds("1,2,3,3");
			var actionIds2 = new ActionIds(new [] { "3", "2", "1" });

			actionIds1.Should().NotBe(actionIds2);
			actionIds1.GetHashCode().Should().NotBe(actionIds2.GetHashCode());
		}
	}
}
