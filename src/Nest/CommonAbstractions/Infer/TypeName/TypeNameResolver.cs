using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.Serialization;

namespace Nest
{
	public class TypeNameResolver
	{
		private readonly IConnectionSettingsValues _connectionSettings;
		private readonly ConcurrentDictionary<Type, string> _typeNames = new ConcurrentDictionary<Type, string>();

		public TypeNameResolver(IConnectionSettingsValues connectionSettings)
		{
			connectionSettings.ThrowIfNull(nameof(connectionSettings));
			this._connectionSettings = connectionSettings;
		}

		public string Resolve<T>() where T : class => this.Resolve(typeof(T));

		public string Resolve(TypeName t) => t?.Name ?? this.ResolveType(t?.Type);

		private string ResolveType(Type type)
		{
			if (type == null) return null;

			if (_typeNames.TryGetValue(type, out var typeName)) return typeName;

			if (_connectionSettings.DefaultTypeNames.TryGetValue(type, out typeName))
			{
				_typeNames.TryAdd(type, typeName);
				return typeName;
			}

			var att = ElasticsearchTypeAttribute.From(type);
			if (att != null && !att.Name.IsNullOrEmpty())
				typeName = att.Name;
			else
			{
				var dataContract = type.GetAttributes<DataContractAttribute>().FirstOrDefault();
				if (dataContract != null) typeName = dataContract.Name;
				else
				{
					var inferredType =_connectionSettings.DefaultTypeNameInferrer(type);
					typeName = !inferredType.IsNullOrEmpty() ? inferredType : _connectionSettings.DefaultTypeName;
				}
			}
			if (typeName.IsNullOrEmpty()) throw new ArgumentNullException($"{type.FullName} resolved to an empty string or null");

			_typeNames.TryAdd(type, typeName);
			return typeName;
		}
	}
}
