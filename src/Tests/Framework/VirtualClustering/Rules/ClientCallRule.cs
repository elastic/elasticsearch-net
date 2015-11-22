using System;
using Nest;

namespace Tests.Framework
{
	public interface IClientCallRule : IRule
	{
		/// <summary>
		/// Either a hard exception or soft HTTP error code
		/// </summary>
		Union<Exception, int> Return { get; set; }
	}

	public class ClientCallRule : RuleBase<ClientCallRule>, IClientCallRule
	{
		private IClientCallRule Self => this;
		Union<Exception, int> IClientCallRule.Return { get; set; }

		public ClientCallRule Fails(Union<TimesHelper.AllTimes, int> times, Union<Exception, int> errorState = null)
		{
			Self.Times = times;
			Self.Succeeds = false;
			Self.Return = errorState;
			return this;
		}

		public ClientCallRule Succeeds(Union<TimesHelper.AllTimes, int> times, int? validResponseCode = 200)
		{
			Self.Times = times;
			Self.Succeeds = true;
			Self.Return = validResponseCode;
			return this;
		}

		public ClientCallRule SucceedAlways(int? validResponseCode = 200) => this.Succeeds(TimesHelper.Always, validResponseCode);
		public ClientCallRule FailAlways(int? validResponseCode = 200) => this.Fails(TimesHelper.Always, validResponseCode);
	}
}