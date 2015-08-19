using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Elasticsearch.Net;

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
