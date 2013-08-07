using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.Resolvers
{
	public class TypeNameResolver
	{
		public TypeNameMarker GetTypeNameFor(Type type)
		{
			if (!type.IsClass && !type.IsInterface)
				throw new ArgumentException("Type is not a class or interface", "type");
			return this.GetTypeNameForType(type);
		}


		public TypeNameMarker GetTypeNameFor<T>() where T : class
		{
			return this.GetTypeNameForType(typeof(T));
		}

		public IEnumerable<TypeNameMarker> GetTypeNamesFor(IEnumerable<Type> types)
		{
			return types.Select(GetTypeNameFor).ToArray();
		}

		public TypeNameMarker GetTypeNameForType(Type type)
		{
			var typeName = type.Name;
			var att = new PropertyNameResolver().GetElasticPropertyFor(type);
			if (att != null && !att.TypeNameMarker.IsNullOrEmpty())
				typeName = att.TypeNameMarker.Name;
			else if (att != null && !string.IsNullOrEmpty(att.Name))
				typeName = att.Name;
			return new TypeNameMarker {Name = typeName, Type = type};
		}

		
	}
}
