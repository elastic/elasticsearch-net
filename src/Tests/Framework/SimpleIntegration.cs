using System;
using Nest;
using Tests.Framework.Integration;

namespace Tests.Framework
{
	public abstract class SimpleIntegration
	{
		readonly ClusterBase _cluster;

		protected virtual IElasticClient Client => this._cluster.Client;

		public SimpleIntegration(ClusterBase cluster)
		{
			this._cluster = cluster;
		}

		public static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);
	}
}
