using System;
using Elasticsearch.Net;

namespace Nest
{

	[ContractJsonConverter(typeof(IndexNameJsonConverter))]
	public class IndexName : IEquatable<IndexName>, IUrlParameter
	{
		public string Name { get; set; }
		public Type Type { get; set; }

		public static implicit operator IndexName(string typeName)
		{
			if (typeName.IsNullOrEmpty())
				return null;
			return new IndexName { Name = typeName.Trim() };
		}
		public static implicit operator IndexName(Type type)
		{
			if (type == null)
				return null;
			return new IndexName { Type = type };
		}

		bool IEquatable<IndexName>.Equals(IndexName other)
		{
			return Equals(other);
		}

		public override bool Equals(object obj)
		{
			var s = obj as string;
			if (!s.IsNullOrEmpty()) return this.EqualsString(s);
			var pp = obj as IndexName;
			if (pp != null) return this.EqualsMarker(pp);
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			if (this.Name != null)
				return this.Name.GetHashCode();
			if (this.Type != null)
				return this.Type.GetHashCode();
			return 0;
		}

		public override string ToString()
		{
			if (!this.Name.IsNullOrEmpty())
				return this.Name;
			if (this.Type != null)
				return this.Type.Name;
			return string.Empty;
		}

		public bool EqualsString(string other)
		{
			return !other.IsNullOrEmpty() && other == this.Name;
		}

		public bool EqualsMarker(IndexName other)
		{
			if (!this.Name.IsNullOrEmpty() && other != null && !other.Name.IsNullOrEmpty())
				return EqualsString(other.Name);
			if (this.Type != null && other != null && other.Type != null)
				return this.GetHashCode() == other.GetHashCode();
			return false;
		}

		public string GetString(IConnectionConfigurationValues settings) => ((IUrlParameter)(Indices)(Indices.Index(this))).GetString(settings);

		public static IndexName From<T>() => typeof(T);

		public Indices And<T>() => new Indices(new IndexName[] { this, typeof(T) });
		public Indices And(IndexName index) => new Indices(new IndexName[] { this, index });
	}
}