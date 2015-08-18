using System;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(TypeNameJsonConverter))]
	public class TypeName : IEquatable<TypeName>
	{
		public string Name { get; set; }
		public Type Type { get; set; }

		public static TypeName Create(Type type)
		{
			return GetTypeNameForType(type);
		}

		public static TypeName Create<T>() where T : class
		{
			return GetTypeNameForType(typeof(T));
		}

		private static TypeName GetTypeNameForType(Type type)
		{
			return new TypeName { Type = type };
		}

		public static implicit operator TypeName(string typeName)
		{
			return typeName == null ? null : new TypeName { Name = typeName };
		}

		public static implicit operator TypeName(Type type)
		{
			return type == null ? null : new TypeName { Type = type };
		}

		public override int GetHashCode()
		{
			if (this.Name != null)
				return this.Name.GetHashCode();
			return this.Type != null ? this.Type.GetHashCode() : 0;
		}

		bool IEquatable<TypeName>.Equals(TypeName other)
		{
			return Equals(other);
		}

		public override bool Equals(object obj)
		{
			var s = obj as string;
			if (!s.IsNullOrEmpty()) return this.EqualsString(s);
			var pp = obj as TypeName;
			if (pp != null) return this.EqualsMarker(pp);

			return base.Equals(obj);
		}

		public bool EqualsMarker(TypeName other)
		{
			if (!this.Name.IsNullOrEmpty() && other != null && !other.Name.IsNullOrEmpty())
				return EqualsString(other.Name);
			if (this.Type != null && other != null && other.Type != null)
				return this.GetHashCode() == other.GetHashCode();
			return false;
		}

		public bool EqualsString(string other)
		{
			return !other.IsNullOrEmpty() && other == this.Name;
		}
	}
}