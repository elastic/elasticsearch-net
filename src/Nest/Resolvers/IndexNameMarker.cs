using System;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest.Resolvers
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


		public override int GetHashCode()
		{
			if (this.Name != null)
				return this.Name.GetHashCode();
			if (this.Type != null)
				return this.Type.GetHashCode();
			return 0;
		}
		public bool Equals(IndexNameMarker other)
		{
			return other != null && this.GetHashCode() == other.GetHashCode();
		}
	}
}