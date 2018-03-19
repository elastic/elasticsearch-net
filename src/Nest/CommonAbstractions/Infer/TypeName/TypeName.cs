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
		private static int TypeHashCode { get; } = typeof(TypeName).GetHashCode();

		public string Name { get; }
		public Type Type { get; }

		private TypeName(string type) => this.Name = type;

		private TypeName(Type type) => this.Type = type;

		internal string DebugDisplay => Type == null ? Name : $"{nameof(TypeName)} for typeof: {Type?.Name}";

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
				result = (result * 397) ^ (this.Name?.GetHashCode() ?? this.Type?.GetHashCode() ?? 0);
				return result;
			}
		}
		public static bool operator ==(TypeName left, TypeName right) => Equals(left, right);

		public static bool operator !=(TypeName left, TypeName right) => !Equals(left, right);

		public bool Equals(TypeName other) => EqualsMarker(other);

		public override bool Equals(object obj)
		{
			var s = obj as string;
			if (!s.IsNullOrEmpty()) return this.EqualsString(s);
			var pp = obj as TypeName;
			return this.EqualsMarker(pp);
		}

		public override string ToString()
		{
			if (!this.Name.IsNullOrEmpty())
				return this.Name;
			return this.Type != null ? this.Type.Name : string.Empty;
		}

		private bool EqualsMarker(TypeName other)
		{
			if (!this.Name.IsNullOrEmpty() && other != null && !other.Name.IsNullOrEmpty())
				return EqualsString(other.Name);
			if (this.Type != null && other?.Type != null)
				return this.Type == other.Type;
			return false;
		}

		private bool EqualsString(string other)
		{
			return !other.IsNullOrEmpty() && other == this.Name;
		}

		string IUrlParameter.GetString(IConnectionConfigurationValues settings)
		{
			if (!(settings is IConnectionSettingsValues nestSettings))
				throw new ArgumentNullException(nameof(settings), $"Can not resolve {nameof(TypeName)} if no {nameof(IConnectionSettingsValues)} is provided");

			return nestSettings.Inferrer.TypeName(this);
		}

		public static TypeName From<T>() => typeof(T);

		public Types And<T>() => new Types(new TypeName[] { this, typeof(T)});
		public Types And(TypeName type) => new Types(new TypeName[] { this, type });
		public Types And(TypeName[] types) => new Types(new TypeName[] { this }.Concat(types));
	}
}
