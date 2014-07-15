using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest.Resolvers
{
	public class TypeNameResolver
	{
		private readonly IConnectionSettingsValues _connectionSettings;
		private PropertyNameResolver _propertyNameResolver;

		public TypeNameResolver(IConnectionSettingsValues connectionSettings)
		{
			connectionSettings.ThrowIfNull("connectionSettings");
			this._connectionSettings = connectionSettings;
			this._propertyNameResolver = new PropertyNameResolver(this._connectionSettings);
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

			var att = ElasticAttributes.Type(type);
			if (att != null && !att.Name.IsNullOrEmpty())
				typeName = att.Name;
			else
				typeName = _connectionSettings.DefaultTypeNameInferrer(type);
			return typeName;
		}


		internal string GetTypeNameFor(TypeNameMarker t)
		{
			if (t == null) return null;

			return t.Name ?? this.GetTypeNameFor(t.Type);
		}
	}
}
