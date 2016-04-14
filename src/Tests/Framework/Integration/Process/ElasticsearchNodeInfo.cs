namespace Tests.Framework.Integration
{
	public class ElasticsearchNodeInfo
	{
		public string Version { get; }
		public int? Pid { get; }
		public string Build { get; }

		public ElasticsearchNodeInfo(string version, string pid, string build)
		{
			this.Version = version;
			if (!string.IsNullOrEmpty(pid))
				Pid = int.Parse(pid);
			Build = build;
		}

	}
}
