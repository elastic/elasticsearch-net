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
			ValidateIndexName(i.Name);
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
			ValidateIndexName(indexName, type);
			return indexName;
		}

		private void ValidateIndexName(string indexName, Type type = null)
		{
			if (string.IsNullOrWhiteSpace(indexName))
				throw new ResolveException(
					"Index name is null for the given type and no default index is set. "
					+ "Map an index name using ConnectionSettings.MapDefaultTypeIndices() "
					+ "or set a default index using ConnectionSettings.DefaultIndex()."
				);

			if (indexName.HasAny(c => char.IsUpper(c)))
				throw new ResolveException($"Index names cannot contain uppercase characters: {indexName}.");
		}
	}
}
