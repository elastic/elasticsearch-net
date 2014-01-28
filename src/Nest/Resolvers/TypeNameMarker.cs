using System;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest.Resolvers
{
	[JsonConverter(typeof(TypeNameMarkerConverter))]
	public class TypeNameMarker : IEquatable<TypeNameMarker>
	{
		public string Name { get; set; }
		public Type Type { get; set; }

		public static TypeNameMarker Create(Type type)
		{
			return GetTypeNameForType(type);
		}

		public static TypeNameMarker Create<T>() where T : class
		{
			return GetTypeNameForType(typeof(T));
		}

		private static TypeNameMarker GetTypeNameForType(Type type)
		{
			if (!type.IsClass && !type.IsInterface)
				throw new ArgumentException("Type is not a class or interface", "type");

			var typeName = type.Name;
			var att = new PropertyNameResolver().GetElasticPropertyFor(type);
			if (att != null && !att.TypeNameMarker.IsNullOrEmpty())
				typeName = att.TypeNameMarker.Name;
			else if (att != null && !string.IsNullOrEmpty(att.Name))
				typeName = att.Name;
			return new TypeNameMarker { Name = typeName, Type = type };
		}

		public bool IsConditionless()
		{
			return this.Name.IsNullOrEmpty() && this.Type == null;
		}

		public static implicit operator TypeNameMarker(string typeName)
		{
			if (typeName == null)
				return null;
			return new TypeNameMarker { Name = typeName };
		}

		public static implicit operator TypeNameMarker(Type type)
		{
			if (type == null)
				return null;
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
}