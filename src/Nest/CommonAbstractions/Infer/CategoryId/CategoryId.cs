using System;
using System.Globalization;
using Elasticsearch.Net;

namespace Nest
{
	public class CategoryId : IUrlParameter, IEquatable<CategoryId>
	{
		internal readonly long Value;

		public CategoryId(long value) => Value = value;

		public static implicit operator CategoryId(long categoryId) => new CategoryId(categoryId);
		public static implicit operator long(CategoryId categoryId) => categoryId.Value;

		// ReSharper disable once ImpureMethodCallOnReadonlyValueField
		public string GetString(IConnectionConfigurationValues settings) => Value.ToString(CultureInfo.InvariantCulture);

		public bool Equals(CategoryId other) => this.Value == other.Value;

		public override bool Equals(object obj)
		{
			switch (obj)
			{
				case int l: return this.Value == l;
				case long l: return this.Value == l;
				case CategoryId i: return this.Value == i.Value;
				default: return false;
			}
		}

		public override int GetHashCode() => this.Value.GetHashCode();

		public static bool operator ==(CategoryId left, CategoryId right) => Equals(left, right);

		public static bool operator !=(CategoryId left, CategoryId right) => !Equals(left, right);
	}
}
