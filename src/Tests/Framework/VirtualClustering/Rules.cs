using System;
using Nest;

namespace Tests.Framework
{
	public interface IRule
	{
		Union<TimesHelper.AllTimes, int> Times { get; set; }
		int? OnPort { get; set; }
		bool Succeeds { get; set; }
	}

	public static class TimesHelper
	{
		public class AllTimes { internal AllTimes() { } }

		public static AllTimes Always = new AllTimes();
		public static readonly int Once = 0;
		public static readonly int Twice = 1;
		public static int Times(int n) => Math.Max(0, n -1);
	}

	public interface IClientCallRule : IRule
	{
		/// <summary>
		/// Either a hard exception or soft HTTP error code
		/// </summary>
		Union<Exception, int> Return { get; set; }
	}

	public class ClientCallRule : IClientCallRule
	{
		private IClientCallRule Self => this;
		int? IRule.OnPort { get; set; }
		bool IRule.Succeeds { get; set; }
		Union<TimesHelper.AllTimes, int> IRule.Times { get; set; }
		Union<Exception, int> IClientCallRule.Return { get; set; }

		public ClientCallRule OnPort(int port)
		{
			Self.OnPort = port;
			return this;
		}

		public ClientCallRule Fails(Union<TimesHelper.AllTimes, int> times, Union<Exception, int> errorState = null)
		{
			Self.Times = times;
			Self.Succeeds = false;
			Self.Return = errorState;
			return this;
		}

		public ClientCallRule Succeeds(Union<TimesHelper.AllTimes, int> times,int? validResponseCode = 200)
		{
			Self.Times = times;
			Self.Succeeds = true;
			Self.Return = validResponseCode;
			return this;
		}

		public ClientCallRule SucceedAlways(int? validResponseCode = 200) => this.Succeeds(TimesHelper.Always, validResponseCode);
		public ClientCallRule FailAlways(int? validResponseCode = 200) => this.Fails(TimesHelper.Always, validResponseCode);
	}


	public interface ISniffRule : IRule
	{
		VirtualCluster NewClusterState { get; set; }
	}

	public class SniffRule : ISniffRule
	{
		private ISniffRule Self => this;
		int? IRule.OnPort { get; set; }
		bool IRule.Succeeds { get; set; }
		Union<TimesHelper.AllTimes, int> IRule.Times { get; set; }
		VirtualCluster ISniffRule.NewClusterState { get; set; }

		public SniffRule OnPort(int port)
		{
			Self.OnPort = port;
			return this;
		}

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


	public class PingRule : IRule
	{
		private IRule Self => this;
		int? IRule.OnPort { get; set; }
		bool IRule.Succeeds { get; set; }
		Union<TimesHelper.AllTimes, int> IRule.Times { get; set; }

		public PingRule OnPort(int port)
		{
			Self.OnPort = port;
			return this;
		}

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