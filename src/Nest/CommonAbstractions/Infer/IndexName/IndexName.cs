using System;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(IndexNameJsonConverter))]
	public class IndexName : IEquatable<IndexName>
	{
		public string Name { get; set; }
		public Type Type { get; set; }

		public static implicit operator IndexName(string typeName)
		{
			if (typeName == null)
				return null;
			return new IndexName { Name = typeName };
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
	}
}