using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest.Resolvers
{
	public class TypeNameResolver
	{
		private readonly IConnectionSettingsValues _connectionSettings;

		public TypeNameResolver(IConnectionSettingsValues connectionSettings)
		{
			connectionSettings.ThrowIfNull(nameof(connectionSettings));
			this._connectionSettings = connectionSettings;
		}

		public string GetTypeNameFor<T>()
		{
			return this.GetTypeNameFor(typeof(T));
		}

		public string GetTypeNameFor(Type type)
		{
			if (type == null) return null;
			string typeName;

			if (_connectionSettings.DefaultTypeNames.TryGetValue(type, out typeName))
				return typeName;

			var att = ElasticsearchTypeAttribute.From(type);
			if (att != null && !att.Name.IsNullOrEmpty())
				typeName = att.Name;
			else
				typeName = _connectionSettings.DefaultTypeNameInferrer(type);
			return typeName;
		}


		internal string GetTypeNameFor(TypeName t)
		{
			if (t == null) return null;

			return t.Name ?? this.GetTypeNameFor(t.Type);
		}
	}
}
