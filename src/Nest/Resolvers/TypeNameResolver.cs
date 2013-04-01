using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.Resolvers
{
	public class TypeNameMarker : IEquatable<TypeNameMarker>
	{
		public string Name { get; set; }
		public Type Type { get; set; }

		public string Resolve(IConnectionSettings connectionSettings)
		{
			connectionSettings.ThrowIfNull("connectionSettings");
			string typeName = this.Name;
			if (this.Type == null)
				return this.Name;
			if (connectionSettings.DefaultTypeNames.TryGetValue(this.Type, out typeName))
				return typeName;
			return this.Name;
		}

		public static implicit operator TypeNameMarker(string typeName)
		{
			return new TypeNameMarker {Name = typeName};
		}
		public static implicit operator TypeNameMarker(Type type)
		{
			return new TypeNameMarker { Type = type };
		}

		public override int GetHashCode()
		{
			if (this.Name != null)
				return this.Name.GetHashCode();
			if (this.Type != null)
				return this.Type.GetHashCode();
			return 0;
		}
		public bool Equals(TypeNameMarker other)
		{
			return other != null && this.GetHashCode() == other.GetHashCode();
		}
	}

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

		public TypeNameMarker GetTypeNameForType(Type type)
		{
			var typeName = type.Name;
			var att = new PropertyNameResolver().GetElasticPropertyFor(type);
			if (att != null && !att.TypeNameMarker.IsNullOrEmpty())
				typeName = att.TypeNameMarker.Name;
			else
				typeName = Inflector.MakePlural(type.Name).ToLower();
			return new TypeNameMarker {Name = typeName, Type = type};
		}

		
	}
}
