using Xunit;

namespace Tests.Framework.Integration
{
	/// <summary>
	/// Use this cluster for heavy API's, either on ES's side or the client (intricate setup etc)
	/// </summary>
	public class IntrusiveOperationCluster : ClusterBase
	{
		public override int MaxConcurrency => 1;
	}
}
