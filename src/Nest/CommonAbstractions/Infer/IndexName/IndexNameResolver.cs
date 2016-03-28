using Elasticsearch.Net;
using System;

namespace Nest
{
	public class IndexNameResolver
	{
		private readonly IConnectionSettingsValues _connectionSettings;

		public IndexNameResolver(IConnectionSettingsValues connectionSettings)
		{
			connectionSettings.ThrowIfNull(nameof(connectionSettings));
			this._connectionSettings = connectionSettings;
		}
		public string Resolve<T>() where T : class => this.Resolve(typeof(T));

		public string Resolve(IndexName i)
		{
			if (i == null || string.IsNullOrEmpty(i.Name))
				return this.Resolve(i.Type);
			return i.Name;
		}

		public string Resolve(Type type)
		{
			var indexName = this._connectionSettings.DefaultIndex;
			var defaultIndices = this._connectionSettings.DefaultIndices;
			if (defaultIndices != null && type != null)
			{
				string value;
				if (defaultIndices.TryGetValue(type, out value) && !string.IsNullOrEmpty(value))
					indexName = value;
			}
			return indexName;
		}
	}
}
