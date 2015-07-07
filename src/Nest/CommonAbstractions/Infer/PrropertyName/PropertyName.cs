using System;
using System.Linq.Expressions;

namespace Nest
{

	
	/// <summary>
	/// Represents a typed container for property names i.e "property" in "field.nested.property";
	/// </summary>
	public class PropertyName : IEquatable<PropertyName>
	{
		public string Name { get; set; }
		public Expression Expression { get; set; }
		public Type Type { get; set; }


		public double? Boost { get; set; }

		public static PropertyName Create(string path, double? boost = null)
		{
			PropertyName marker = path;
			marker.Boost = boost;
			return marker;
		}

		public static PropertyName Create(Expression path, double? boost = null)
		{
			PropertyName marker = path;
			marker.Boost = boost;
			return marker;
		}
		public static implicit operator PropertyName(string typeName)
		{
			return typeName == null ? null : new PropertyName { Name = typeName };
		}

		public static implicit operator PropertyName(Expression type)
		{
			return type == null ? null : new PropertyName { Expression = type };
		}
		public static implicit operator PropertyName(Type type)
		{
			return type == null ? null : new PropertyName { Type = type };
		}
		
		bool IEquatable<PropertyName>.Equals(PropertyName other)
		{
			return Equals(other);
		}
		public override bool Equals(object obj)
		{
			var s = obj as string;
			if (!s.IsNullOrEmpty()) return this.EqualsString(s);
			var pp = obj as PropertyName;
			if (pp != null) return this.EqualsMarker(pp);
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			if (this.Name != null)
				return this.Name.GetHashCode();
			return this.Expression != null ? this.Expression.GetHashCode() : 0;
		}
		public bool EqualsMarker(PropertyName other)
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
	}
	public static class PropertyNameMarkerExtensions
	{
		internal static bool IsConditionless(this PropertyName marker)
		{
			return marker == null || (marker.Name.IsNullOrEmpty() && marker.Expression == null && marker.Type == null);
		}
	}
}