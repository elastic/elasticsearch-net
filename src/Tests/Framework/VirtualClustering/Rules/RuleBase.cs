using System;
using Nest;

namespace Tests.Framework
{
	public interface IRule
	{
		Union<TimesHelper.AllTimes, int> Times { get; set; }
		int? OnPort { get; set; }
		bool Succeeds { get; set; }
		Func<DateTime, DateTime> Takes { get; set; }
	}

	public abstract class RuleBase<TRule> : IRule
		where TRule : RuleBase<TRule>, IRule
	{
		private IRule Self => this;
		int? IRule.OnPort { get; set; }
		bool IRule.Succeeds { get; set; }
		Func<DateTime, DateTime> IRule.Takes { get; set; }
		Union<TimesHelper.AllTimes, int> IRule.Times { get; set; }

		public TRule OnPort(int port)
		{
			Self.OnPort = port;
			return (TRule)this;
		}

		public TRule Takes(TimeSpan span)
		{
			Self.Takes = (d) => d.Add(span);
			return (TRule)this;
		}

		public TRule Takes(Func<DateTime, DateTime> takes)
		{
			Self.Takes = takes;
			return (TRule)this;
		}
	}

}