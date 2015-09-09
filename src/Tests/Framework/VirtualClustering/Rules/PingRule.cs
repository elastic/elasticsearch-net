using Nest;

namespace Tests.Framework
{
	public class PingRule : RuleBase<PingRule>, IRule
	{
		private IRule Self => this;

		public PingRule Succeeds(Union<TimesHelper.AllTimes, int> times)
		{
			Self.Times = times;
			Self.Succeeds = true;
			return this;
		}
		public PingRule Fails(Union<TimesHelper.AllTimes, int> times)
		{
			Self.Times = times;
			Self.Succeeds = false;
			return this;
		}
		public PingRule SucceedAlways(VirtualCluster cluster = null) => this.Succeeds(TimesHelper.Always);
		public PingRule FailAlways() => this.Fails(TimesHelper.Always);
	}
}