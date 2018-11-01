using System;
using System.Globalization;
using Elasticsearch.Net;

namespace Nest
{
	public class CategoryId : IUrlParameter, IEquatable<CategoryId>
	{
		internal readonly long Value;

		public CategoryId(long value) => Value = value;

		public bool Equals(CategoryId other) => Value == other.Value;

		// ReSharper disable once ImpureMethodCallOnReadonlyValueField
		public string GetString(IConnectionConfigurationValues settings) => Value.ToString(CultureInfo.InvariantCulture);

		public override bool Equals(object obj)
		{
			switch (obj)
			{
				case int l: return Value == l;
				case long l: return Value == l;
				case CategoryId i: return Value == i.Value;
				default: return false;
			}
		}

		public override int GetHashCode() => Value.GetHashCode();

		public static bool operator ==(CategoryId left, CategoryId right) => Equals(left, right);

		public static implicit operator CategoryId(long categoryId) => new CategoryId(categoryId);

		public static implicit operator long(CategoryId categoryId) => categoryId.Value;

		public static bool operator !=(CategoryId left, CategoryId right) => !Equals(left, right);
	}
}
