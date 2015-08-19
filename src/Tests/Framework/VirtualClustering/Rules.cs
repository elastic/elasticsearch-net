namespace Tests.Framework
{
	public interface IRule
	{
		bool? AllCalls { get; set; }
		int? NthCall { get; set; }
		int? OnPort { get; set; }
		bool Succeeds { get; set; }
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
		bool? IRule.AllCalls { get; set; }
		int? IRule.NthCall { get; set; }
		VirtualCluster ISniffRule.NewClusterState { get; set; }

		public SniffRule OnPort(int port)
		{
			Self.OnPort = port;
			return this;
		}

		public SniffRule FailsOn(int call)
		{
			Self.NthCall = call;
			Self.Succeeds = false;
			return this;
		}

		public SniffRule SucceedsOn(int call, VirtualCluster cluster = null)
		{
			Self.NthCall = call;
			Self.Succeeds = true;
			Self.NewClusterState = cluster;
			return this;
		}

		public SniffRule FailAlways()
		{
			Self.AllCalls = true;
			Self.Succeeds = false;
			return this;
		}

		public SniffRule SucceedAlways(VirtualCluster cluster = null)
		{
			Self.AllCalls = true;
			Self.Succeeds = true;
			Self.NewClusterState = cluster;
			return this;
		}

	}


	public class PingRule : IRule
	{
		private IRule Self => this;
		int? IRule.OnPort { get; set; }
		bool IRule.Succeeds { get; set; }
		bool? IRule.AllCalls { get; set; }
		int? IRule.NthCall { get; set; }

		public PingRule OnPort(int port)
		{
			Self.OnPort = port;
			return this;
		}

		public PingRule FailsOn(int call)
		{
			Self.NthCall = call;
			Self.Succeeds = false;
			return this;
		}

		public PingRule SucceedsOn(int call)
		{
			Self.NthCall = call;
			Self.Succeeds = true;
			return this;
		}

		public PingRule FailAlways()
		{
			Self.AllCalls = true;
			Self.Succeeds = false;
			return this;
		}

		public PingRule SucceedAlways()
		{
			Self.AllCalls = true;
			Self.Succeeds = true;
			return this;
		}

	}
}