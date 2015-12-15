using System;
using Nest;

namespace Tests.Framework
{
	public class PingRule : RuleBase<PingRule>, IRule
	{
		private IRule Self => this;

		public PingRule Fails(Union<TimesHelper.AllTimes, int> times, Union<Exception, int> errorState = null)
		{
			Self.Times = times;
			Self.Succeeds = false;
			Self.Return = errorState;
			return this;
		}

		public PingRule Succeeds(Union<TimesHelper.AllTimes, int> times, int? validResponseCode = 200)
		{
			Self.Times = times;
			Self.Succeeds = true;
			Self.Return = validResponseCode;
			return this;
		}

		public PingRule SucceedAlways(int? validResponseCode = 200) => this.Succeeds(TimesHelper.Always, validResponseCode);
		public PingRule FailAlways(Union<Exception, int> errorState = null) => this.Fails(TimesHelper.Always, errorState);
	}
}