using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.Resolvers
{
	public class TypeNameResolver
	{
		private readonly IConnectionSettings _connectionSettings;
		public TypeNameResolver(IConnectionSettings connectionSettings)
		{
			connectionSettings.ThrowIfNull("hasDefaultIndices");
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

			var att = new PropertyNameResolver().GetElasticPropertyFor(type);
			if (att != null && !att.TypeNameMarker.IsNullOrEmpty())
				typeName = att.TypeNameMarker.Name;
			else if (att != null && !string.IsNullOrEmpty(att.Name))
				typeName = att.Name;
			else
				typeName = _connectionSettings.DefaultTypeNameInferrer(type);
			return typeName;
		}


		internal string GetTypeNameFor(TypeNameMarker t)
		{
			return t.Name ?? this.GetTypeNameFor(t.Type);
		}
	}
}
