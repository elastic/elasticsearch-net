using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticSearch.Client;

namespace ElasticSearch.Tests
{
	public class BaseElasticSearchTests
	{
		private IConnectionSettings _settings;
		protected IConnectionSettings Settings
		{
			get
			{
				if (this._settings != null)
					return this._settings;

				this._settings = new ConnectionSettings(Test.Default.Host, Test.Default.Port)
								.SetDefaultIndex(Test.Default.DefaultIndex)
								.SetMaximumAsyncConnections(Test.Default.MaximumAsyncConnections);
				return this._settings;
			}
		}
		private ElasticClient _connectedClient;
		protected ElasticClient ConnectedClient
		{
			get 
			{
				if (this._connectedClient != null)
					return this._connectedClient;

				var client = new ElasticClient(this.Settings);
				if (client.IsValid)
				{ 
					this._connectedClient = client;
					return this._connectedClient;
				}
				return null;
			}
		}
		protected ElasticClient CreateClient()
		{
			return new ElasticClient(this.Settings);
		}

	}
}
