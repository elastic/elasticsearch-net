using System;
using System.Diagnostics;
using System.Linq;
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

		public static TypeName Create(Type type) => GetTypeNameForType(type);

		public static TypeName Create<T>() where T : class => GetTypeNameForType(typeof(T));

		private static TypeName GetTypeNameForType(Type type) => new TypeName { Type = type };

		public static implicit operator TypeName(string typeName) =>
			typeName == null ? null : new TypeName { Name = typeName };

		public static implicit operator TypeName(Type type) => type == null ? null : new TypeName { Type = type };

		public override int GetHashCode()
		{
			if (this.Name != null)
				return this.Name.GetHashCode();
			return this.Type?.GetHashCode() ?? 0;
		}

		bool IEquatable<TypeName>.Equals(TypeName other) => Equals(other);

		public override bool Equals(object obj)
		{
			var s = obj as string;
			if (!s.IsNullOrEmpty()) return this.EqualsString(s);
			var pp = obj as TypeName;
			if (pp != null) return this.EqualsMarker(pp);

			return base.Equals(obj);
		}

		public override string ToString()
		{
			if (!this.Name.IsNullOrEmpty())
				return this.Name;
			if (this.Type != null)
				return this.Type.Name;
			return string.Empty;
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

		string IUrlParameter.GetString(IConnectionConfigurationValues settings)
		{
			var nestSettings = settings as IConnectionSettingsValues;
			if (nestSettings == null)
				throw new Exception("Tried to pass type name on querystring but it could not be resolved because no nest settings are available");

			return nestSettings.Inferrer.TypeName(this);
		}

		public static TypeName From<T>() => typeof(T);

		public Types And<T>() => new Types(new TypeName[] { this, typeof(T)});
		public Types And(TypeName type) => new Types(new TypeName[] { this, type });
		public Types And(TypeName[] types) => new Types(new TypeName[] { this }.Concat(types));
	}
}
