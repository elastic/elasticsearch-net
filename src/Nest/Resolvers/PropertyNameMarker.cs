using System;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest.Resolvers
{

	
	//[JsonConverter(typeof(TypeNameMarkerConverter))]
	/// <summary>
	/// Represents a typed container for property names i.e "property" in "field.nested.property";
	/// </summary>
	public class PropertyNameMarker : IEquatable<PropertyNameMarker>
	{
		public string Name { get; set; }
		public Expression Expression { get; set; }
		public Type Type { get; set; }


		public double? Boost { get; set; }

		public static PropertyNameMarker Create(string path, double? boost = null)
		{
			PropertyNameMarker marker = path;
			marker.Boost = boost;
			return marker;
		}

		public static PropertyNameMarker Create(Expression path, double? boost = null)
		{
			PropertyNameMarker marker = path;
			marker.Boost = boost;
			return marker;
		}
		public static implicit operator PropertyNameMarker(string typeName)
		{
			return typeName == null ? null : new PropertyNameMarker { Name = typeName };
		}

		public static implicit operator PropertyNameMarker(Expression type)
		{
			return type == null ? null : new PropertyNameMarker { Expression = type };
		}
		public static implicit operator PropertyNameMarker(Type type)
		{
			return type == null ? null : new PropertyNameMarker { Type = type };
		}

		public override int GetHashCode()
		{
			if (this.Name != null)
				return this.Name.GetHashCode();
			return this.Expression != null ? this.Expression.GetHashCode() : 0;
		}
		public bool Equals(PropertyNameMarker other)
		{
			return other != null && this.GetHashCode() == other.GetHashCode();
		}
	}
	public static class PropertyNameMarkerExtensions
	{
		internal static bool IsConditionless(this PropertyNameMarker marker)
		{
			return marker == null || (marker.Name.IsNullOrEmpty() && marker.Expression == null && marker.Type == null);
		}
	}
}