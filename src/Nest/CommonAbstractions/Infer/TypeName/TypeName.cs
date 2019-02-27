using System;
using System.Diagnostics;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	[Obsolete("Types have been removed from 7.x")]
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class TypeName : IEquatable<TypeName>, IUrlParameter
	{
		private TypeName(string type) => Name = type;

		private TypeName(Type type) => Type = type;

		public string Name { get; }
		public Type Type { get; }

		internal string DebugDisplay => Type == null ? Name : $"{nameof(TypeName)} for typeof: {Type?.Name}";
		private static int TypeHashCode { get; } = typeof(TypeName).GetHashCode();

		public bool Equals(TypeName other) => EqualsMarker(other);

		string IUrlParameter.GetString(IConnectionConfigurationValues settings) => throw new NotImplementedException();

		public static TypeName Create(Type type) => GetTypeNameForType(type);

		public static TypeName Create<T>() where T : class => GetTypeNameForType(typeof(T));

		private static TypeName GetTypeNameForType(Type type) => new TypeName(type);

		public static implicit operator TypeName(string typeName) => typeName.IsNullOrEmpty() ? null : new TypeName(typeName);

		public static implicit operator TypeName(Type type) => type == null ? null : new TypeName(type);

		public override int GetHashCode()
		{
			unchecked
			{
				var result = TypeHashCode;
				result = (result * 397) ^ (Name?.GetHashCode() ?? Type?.GetHashCode() ?? 0);
				return result;
			}
		}

		public static bool operator ==(TypeName left, TypeName right) => Equals(left, right);

		public static bool operator !=(TypeName left, TypeName right) => !Equals(left, right);

		public override bool Equals(object obj)
		{
			var s = obj as string;
			if (!s.IsNullOrEmpty()) return EqualsString(s);

			var pp = obj as TypeName;
			return EqualsMarker(pp);
		}

		public override string ToString()
		{
			if (!Name.IsNullOrEmpty())
				return Name;

			return Type != null ? Type.Name : string.Empty;
		}

		private bool EqualsMarker(TypeName other)
		{
			if (!Name.IsNullOrEmpty() && other != null && !other.Name.IsNullOrEmpty())
				return EqualsString(other.Name);
			if (Type != null && other?.Type != null)
				return Type == other.Type;

			return false;
		}

		private bool EqualsString(string other) => !other.IsNullOrEmpty() && other == Name;

		public static TypeName From<T>() => typeof(T);

		public Types And<T>() => new Types(new TypeName[] { this, typeof(T) });

		public Types And(TypeName type) => new Types(new TypeName[] { this, type });

		public Types And(TypeName[] types) => new Types(new TypeName[] { this }.Concat(types));
	}
}
