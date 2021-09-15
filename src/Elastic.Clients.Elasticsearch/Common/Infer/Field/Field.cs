using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	// TODO - This is a prototype
	public struct FieldSettings
	{
		public double? Boost { get; set; }
		public string? Format { get; set; }
	}

	// TODO - Make struct
	[DebuggerDisplay("{" + nameof(DebugDisplay) + ",nq}")]
	public class Field : IEquatable<Field>, IUrlParameter
	{
		private readonly object _comparisonValue;
		private readonly Type _type;

		// TODO - Future idea
		public Field(string name, FieldSettings settings)
		{
			name.ThrowIfNullOrEmpty(nameof(name));

			// todo  -throw is no settings
			Name = ParseFieldName(name, out var b);
			Boost = b ?? settings.Boost;
			Format = settings.Format;
			_comparisonValue = Name;
		}

		public Field(string name) : this(name, null, null) { }

		public Field(string name, double boost) : this(name, boost, null) { }

		public Field(string name, string format) : this(name, null, format) { }

		public Field(string name, double? boost, string? format)
		{
			name.ThrowIfNullOrEmpty(nameof(name));
			Name = ParseFieldName(name, out var b);
			Boost = b ?? boost;
			Format = format;
			_comparisonValue = Name;
		}

		public Field(Expression expression, double? boost = null, string format = null)
		{
			Expression = expression ?? throw new ArgumentNullException(nameof(expression));
			Boost = boost;
			Format = format;
			_comparisonValue = expression.ComparisonValueFromExpression(out var type, out var cachable);
			_type = type;
			CachableExpression = cachable;
		}

		public Field(PropertyInfo property, double? boost = null, string format = null)
		{
			Property = property ?? throw new ArgumentNullException(nameof(property));
			Boost = boost;
			Format = format;
			_comparisonValue = property;
			_type = property.DeclaringType;
		}

		/// <summary>
		///     A boost to apply to the field
		/// </summary>
		public double? Boost { get; set; }

		/// <summary>
		///     A format to apply to the field.
		/// </summary>
		/// <remarks>
		///     Can be used only for Doc Value Fields Elasticsearch 6.4.0+
		/// </remarks>
		public string? Format { get; set; }

		public bool CachableExpression { get; }

		/// <summary>
		///     An expression from which the name of the field can be inferred
		/// </summary>
		public Expression Expression { get; }

		/// <summary>
		///     The name of the field
		/// </summary>
		public string Name { get; }

		/// <summary>
		///     A property from which the name of the field can be inferred
		/// </summary>
		public PropertyInfo Property { get; }

		internal string DebugDisplay =>
			$"{Expression?.ToString() ?? PropertyDebug ?? Name}{(Boost.HasValue ? "^" + Boost.Value : string.Empty)}"
			+ $"{(!string.IsNullOrEmpty(Format) ? " format: " + Format : string.Empty)}"
			+ $"{(_type == null ? string.Empty : " typeof: " + _type.Name)}";

		private string PropertyDebug => Property == null ? null : $"PropertyInfo: {Property.Name}";

		public bool Equals(Field other) => _type != null
			? other != null && _type == other._type && _comparisonValue.Equals(other._comparisonValue)
			: other != null && _comparisonValue.Equals(other._comparisonValue);

		string IUrlParameter.GetString(ITransportConfiguration settings)
		{
			if (!(settings is IElasticsearchClientSettings ElasticsearchSettings))
			{
				throw new ArgumentNullException(nameof(settings),
					$"Can not resolve {nameof(Field)} if no {nameof(IElasticsearchClientSettings)} is provided");
			}

			return string.Empty;
			//return ElasticsearchSettings.Inferrer.Field(this);
		}

		public override string ToString() => DebugDisplay;

		public Fields And(Field field) => new(new[] { this, field });

		public Fields And<T, TValue>(Expression<Func<T, TValue>> field, double? boost = null, string format = null)
			where T : class =>
			new(new[] { this, new Field(field, boost, format) });

		public Fields And<T>(Expression<Func<T, object>> field, double? boost = null, string format = null)
			where T : class =>
			new(new[] { this, new Field(field, boost, format) });

		public Fields And(string field, double? boost = null, string format = null) =>
			new(new[] { this, new Field(field, boost, format) });

		public Fields And(PropertyInfo property, double? boost = null, string format = null) =>
			new(new[] { this, new Field(property, boost, format) });

		private static string ParseFieldName(string name, out double? boost)
		{
			boost = null;
			if (name == null)
				return null;

			var caretIndex = name.IndexOf('^');
			if (caretIndex == -1)
				return name;

			var parts = name.Split(new[] { '^' }, 2, StringSplitOptions.RemoveEmptyEntries);
			name = parts[0];
			boost = double.Parse(parts[1], CultureInfo.InvariantCulture);
			return name;
		}

		public static implicit operator Field(string name) => name.IsNullOrEmpty() ? null : new Field(name);

		public static implicit operator Field(Expression expression) =>
			expression == null ? null : new Field(expression);

		public static implicit operator Field(PropertyInfo property) => property == null ? null : new Field(property);

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = _comparisonValue?.GetHashCode() ?? 0;
				hashCode = (hashCode * 397) ^ (_type?.GetHashCode() ?? 0);
				return hashCode;
			}
		}

		public override bool Equals(object obj)
		{
			switch (obj)
			{
				case string s:
					return Equals(s);
				case PropertyInfo p:
					return Equals(p);
				case Field f:
					return Equals(f);
				default:
					return false;
			}
		}

		public static bool operator ==(Field x, Field y) => Equals(x, y);

		public static bool operator !=(Field x, Field y) => !Equals(x, y);
	}
}
