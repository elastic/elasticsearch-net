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

		public string GetIndexForType(Type type)
		{
			var defaultIndices = this._connectionSettings.DefaultIndices;

			if (defaultIndices == null)
				return this._connectionSettings.DefaultIndex;

			if (type == null)
				return this._connectionSettings.DefaultIndex;

			string value;
			if (defaultIndices.TryGetValue(type, out value) && !string.IsNullOrWhiteSpace(value))
				return value;
			return this._connectionSettings.DefaultIndex;
		}


		internal string GetIndexForType(IndexName i)
		{
			if (i == null) return this.GetIndexForType((Type)null);

			return i.Name ?? this.GetIndexForType(i.Type);
		}
	}
}
