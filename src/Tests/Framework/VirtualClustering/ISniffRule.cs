namespace Tests.Framework
{
	public interface ISniffRule
	{
		bool? AllCalls { get; set; }
		int? NthCall { get; set; }
		int? OnPort { get; set; }
		bool Succeeds { get; set; }
		VirtualCluster NewClusterState { get; set; }
	}
}