using System;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(IndexNameMarkerConverter))]
	public class IndexNameMarker : IEquatable<IndexNameMarker>
	{
		public string Name { get; set; }
		public Type Type { get; set; }

		public static implicit operator IndexNameMarker(string typeName)
		{
			if (typeName == null)
				return null;
			return new IndexNameMarker { Name = typeName };
		}
		public static implicit operator IndexNameMarker(Type type)
		{
			if (type == null)
				return null;
			return new IndexNameMarker { Type = type };
		}

		bool IEquatable<IndexNameMarker>.Equals(IndexNameMarker other)
		{
			return Equals(other);
		}

		public override bool Equals(object obj)
		{
			var s = obj as string;
			if (!s.IsNullOrEmpty()) return this.EqualsString(s);
			var pp = obj as IndexNameMarker;
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

		public bool EqualsMarker(IndexNameMarker other)
		{
			return other != null && this.GetHashCode() == other.GetHashCode();
		}
	}
}