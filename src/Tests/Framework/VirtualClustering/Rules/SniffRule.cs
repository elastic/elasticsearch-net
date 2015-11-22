using Nest;

namespace Tests.Framework
{
	public interface ISniffRule : IRule
	{
		VirtualCluster NewClusterState { get; set; }
	}

	public class SniffRule : RuleBase<SniffRule>, ISniffRule
	{
		private ISniffRule Self => this;
		VirtualCluster ISniffRule.NewClusterState { get; set; }

		public SniffRule Fails(Union<TimesHelper.AllTimes, int> times)
		{
			Self.Times = times;
			Self.Succeeds = false;
			return this;
		}

		public SniffRule Succeeds(Union<TimesHelper.AllTimes, int> times, VirtualCluster cluster = null)
		{
			Self.Times = times;
			Self.Succeeds = true;
			Self.NewClusterState = cluster;
			return this;
		}
		public SniffRule SucceedAlways(VirtualCluster cluster = null) => this.Succeeds(TimesHelper.Always, cluster);
		public SniffRule FailAlways() => this.Fails(TimesHelper.Always);
	}
}