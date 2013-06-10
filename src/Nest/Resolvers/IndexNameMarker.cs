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

		public string Resolve(IConnectionSettings connectionSettings)
		{
			connectionSettings.ThrowIfNull("connectionSettings");

			string typeName = this.Name;
			if (this.Type == null)
				return this.Name;
			return new IndexNameResolver(connectionSettings).GetIndexForType(this.Type);
		}

		public static implicit operator IndexNameMarker(string typeName)
		{
			return new IndexNameMarker { Name = typeName };
		}
		public static implicit operator IndexNameMarker(Type type)
		{
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