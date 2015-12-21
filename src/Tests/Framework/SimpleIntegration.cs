using System;
using Nest;
using Tests.Framework.Integration;

namespace Tests.Framework
{
	public abstract class SimpleIntegration
	{
		readonly IIntegrationCluster _cluster;

		protected virtual ConnectionSettings GetConnectionSettings(ConnectionSettings settings) => settings;
		protected virtual IElasticClient Client => this._cluster.Client(GetConnectionSettings);

		public SimpleIntegration(IIntegrationCluster cluster)
		{
			this._cluster = cluster;
		}

		public string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);
	}
}
