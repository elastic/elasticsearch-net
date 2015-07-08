using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	public class FieldName : IEquatable<FieldName>
	{
		public string Name { get; set; }
		public Expression Expression { get; set; }
		public Type Type { get; set; }
		public double? Boost { get; set; }

		public static FieldName Create(string field, double? boost = null)
		{
			FieldName fieldName = field;
			fieldName.Boost = boost;
			return fieldName;
		}

		public static FieldName Create(Expression field, double? boost = null)
		{
			FieldName fieldName = field;
			fieldName.Boost = boost;
			return fieldName;
		}

		public static implicit operator FieldName(string typeName)
		{
			return typeName == null ? null : new FieldName { Name = typeName };
		}

		public static implicit operator FieldName(Expression type)
		{
			return type == null ? null : new FieldName { Expression = type };
		}

		public static implicit operator FieldName(Type type)
		{
			return type == null ? null : new FieldName { Type = type };
		}

		public override int GetHashCode()
		{
			if (Name != null)
				return Name.GetHashCode();
			return Expression != null ? Expression.GetHashCode() : 0;
		}

		bool IEquatable<FieldName>.Equals(FieldName other)
		{
			return Equals(other);
		}

		public override bool Equals(object obj)
		{
			var s = obj as string;
			if (!s.IsNullOrEmpty()) return EqualsString(s);
			var pp = obj as FieldName;
			if (pp != null) return EqualsMarker(pp);
			return base.Equals(obj);
		}

		public bool EqualsMarker(FieldName other)
		{
			if (!Name.IsNullOrEmpty() && other != null && !other.Name.IsNullOrEmpty())
				return EqualsString(other.Name);
			if (Expression != null && other != null && other.Expression != null)
				return GetHashCode() == other.GetHashCode();
			if (Type != null && other != null && other.Type != null)
				return GetHashCode() == other.GetHashCode();
			return false;
		}

		public bool EqualsString(string other)
		{
			return !other.IsNullOrEmpty() && other == Name;
		}
	}
}
