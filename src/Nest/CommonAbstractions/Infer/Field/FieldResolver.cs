using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{
	public class FieldResolver
	{
		private readonly IConnectionSettingsValues _settings;

		protected readonly ConcurrentDictionary<Field, string> Fields = new ConcurrentDictionary<Field, string>();
		protected readonly ConcurrentDictionary<PropertyName, string> Properties = new ConcurrentDictionary<PropertyName, string>();

		public FieldResolver(IConnectionSettingsValues settings)
		{
			settings.ThrowIfNull(nameof(settings));
			this._settings = settings;
		}

		public string Resolve(Field field)
		{
			var name = ResolveFieldName(field);
			if (field.Boost.HasValue) name += $"^{field.Boost.Value.ToString(CultureInfo.InvariantCulture)}";
			return name;
		}

		private string ResolveFieldName(Field field)
		{
			if (field.IsConditionless()) return null;
			if (!field.Name.IsNullOrEmpty()) return field.Name;
			if (field.Expression != null && !field.CachableExpression)
			{
				return this.Resolve(field.Expression, field.Property);
			}

			string fieldName;
			if (this.Fields.TryGetValue(field, out fieldName))
				return fieldName;

			fieldName = this.Resolve(field.Expression, field.Property);
			this.Fields.TryAdd(field, fieldName);
			return fieldName;
		}

		public string Resolve(PropertyName property)
		{
			if (property.IsConditionless()) return null;
			if (!property.Name.IsNullOrEmpty()) return property.Name;

			if (property.Expression != null && !property.CacheableExpression)
			{
				return this.Resolve(property.Expression, property.Property);
			}

			string propertyName;
			if (this.Properties.TryGetValue(property, out propertyName))
				return propertyName;

			propertyName = this.Resolve(property.Expression, property.Property, true);
			this.Properties.TryAdd(property, propertyName);
			return propertyName;
		}

		private string Resolve(Expression expression, MemberInfo member, bool toLastToken = false)
		{
			var visitor = new FieldExpressionVisitor(_settings);
			var name = expression != null
				? visitor.Resolve(expression, toLastToken)
				: member != null
					? visitor.Resolve(member)
					: null;

			if (name == null)
				throw new ResolveException("Name resolved to null for the given Expression or MemberInfo.");

			return name;
		}
	}
}
