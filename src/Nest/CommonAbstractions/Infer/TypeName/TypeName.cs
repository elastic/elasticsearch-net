using System;
using Elasticsearch.Net;

namespace Nest
{
	[ContractJsonConverter(typeof(TypeNameJsonConverter))]
	public class TypeName : IEquatable<TypeName> , IUrlParameter
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
			return this.Type?.GetHashCode() ?? 0;
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

		string IUrlParameter.GetString(IConnectionConfigurationValues settings) => ((IUrlParameter)(Types)(Types.Type(this))).GetString(settings);

		public static TypeName From<T>() => typeof(T);

		public Types And<T>() => new Types(new TypeName[] { this, typeof(T)});
		public Types And(TypeName type) => new Types(new TypeName[] { this, type });
	}
}