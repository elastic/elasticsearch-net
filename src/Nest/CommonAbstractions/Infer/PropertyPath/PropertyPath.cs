using System;
using System.Linq.Expressions;

namespace Nest
{
	/// <summary>
	/// Represents a typed container for object paths "field.nested.property";
	/// </summary>
	public class PropertyPath : IEquatable<PropertyPath>
	{
		public string Name { get; set; }

		public Expression Expression { get; set; }

		public double? Boost { get; set; }

		public static PropertyPath Create(string path, double? boost = null)
		{
			PropertyPath marker = path;
			marker.Boost = boost;
			return marker;
		}

		public static PropertyPath Create(Expression path, double? boost = null)
		{
			PropertyPath marker = path;
			marker.Boost = boost;
			return marker;
		}

		public static implicit operator PropertyPath(string typeName)
		{
			return typeName == null ? null : new PropertyPath { Name = typeName };
		}

		public static implicit operator PropertyPath(Expression type)
		{
			return type == null ? null : new PropertyPath { Expression = type };
		}

		public override int GetHashCode()
		{
			if (this.Name != null)
				return this.Name.GetHashCode();
			return this.Expression != null ? this.Expression.GetHashCode() : 0;
		}

		bool IEquatable<PropertyPath>.Equals(PropertyPath other)
		{
			return Equals(other);
		}

		public override bool Equals(object obj)
		{
			var s = obj as string;
			if (!s.IsNullOrEmpty()) return this.EqualsString(s);
			var pp = obj as PropertyPath;
			if (pp != null) return this.EqualsMarker(pp);

			return base.Equals(obj);
		}

		public bool EqualsMarker(PropertyPath other)
		{
			if (!this.Name.IsNullOrEmpty() && other != null && !other.Name.IsNullOrEmpty())
				return EqualsString(other.Name);
			if (this.Expression != null && other != null && other.Expression != null)
				return this.GetHashCode() == other.GetHashCode();
			return false;
			
		}
		public bool EqualsString(string other)
		{
			return !other.IsNullOrEmpty() && other == this.Name;
		}
	}

	public static class PropertyPathMarkerExtensions
	{
		internal static bool IsConditionless(this PropertyPath marker)
		{
			return marker == null || (marker.Name.IsNullOrEmpty() && marker.Expression == null);
		}
	}

}