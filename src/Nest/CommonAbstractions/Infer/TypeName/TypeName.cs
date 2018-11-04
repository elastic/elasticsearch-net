using System;
using System.Diagnostics;
using Elasticsearch.Net;

namespace Nest
{
	[ContractJsonConverter(typeof(TypeNameJsonConverter))]
	[DebuggerDisplay("{DebugDisplay,nq}")]
	public class TypeName : IEquatable<TypeName>, IUrlParameter
	{
		public string Name { get; set; }
		public Type Type { get; set; }

		internal string DebugDisplay => Type == null ? Name : $"{nameof(TypeName)} for typeof: {Type?.Name}";

		bool IEquatable<TypeName>.Equals(TypeName other) => Equals(other);

		string IUrlParameter.GetString(IConnectionConfigurationValues settings)
		{
			var nestSettings = settings as IConnectionSettingsValues;
			if (nestSettings == null)
				throw new Exception("Tried to pass type name on querystring but it could not be resolved because no nest settings are available");

			return nestSettings.Inferrer.TypeName(this);
		}

		public static TypeName Create(Type type) => GetTypeNameForType(type);

		public static TypeName Create<T>() where T : class => GetTypeNameForType(typeof(T));

		private static TypeName GetTypeNameForType(Type type) => new TypeName { Type = type };

		public static implicit operator TypeName(string typeName) => typeName == null ? null : new TypeName { Name = typeName };

		public static implicit operator TypeName(Type type) => type == null ? null : new TypeName { Type = type };

		public override int GetHashCode()
		{
			if (Name != null)
				return Name.GetHashCode();

			return Type?.GetHashCode() ?? 0;
		}

		public override bool Equals(object obj)
		{
			var s = obj as string;
			if (!s.IsNullOrEmpty()) return EqualsString(s);

			var pp = obj as TypeName;
			if (pp != null) return EqualsMarker(pp);

			return base.Equals(obj);
		}

		public override string ToString()
		{
			if (!Name.IsNullOrEmpty())
				return Name;
			if (Type != null)
				return Type.Name;

			return string.Empty;
		}

		public bool EqualsMarker(TypeName other)
		{
			if (!Name.IsNullOrEmpty() && other != null && !other.Name.IsNullOrEmpty())
				return EqualsString(other.Name);
			if (Type != null && other?.Type != null)
				return Type == other.Type;

			return false;
		}

		public bool EqualsString(string other) => !other.IsNullOrEmpty() && other == Name;

		public static TypeName From<T>() => typeof(T);

		public Types And<T>() => new Types(new TypeName[] { this, typeof(T) });

		public Types And(TypeName type) => new Types(new TypeName[] { this, type });
	}
}
