/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace Nest
{
	public class FieldResolver
	{
		protected readonly ConcurrentDictionary<Field, string> Fields = new ConcurrentDictionary<Field, string>();
		protected readonly ConcurrentDictionary<PropertyName, string> Properties = new ConcurrentDictionary<PropertyName, string>();
		private readonly IConnectionSettingsValues _settings;

		public FieldResolver(IConnectionSettingsValues settings)
		{
			settings.ThrowIfNull(nameof(settings));
			_settings = settings;
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
			if (field.Expression != null && !field.CachableExpression) return Resolve(field.Expression, field.Property);

			if (Fields.TryGetValue(field, out var fieldName))
				return fieldName;

			fieldName = Resolve(field.Expression, field.Property);
			Fields.TryAdd(field, fieldName);
			return fieldName;
		}

		public string Resolve(PropertyName property)
		{
			if (property.IsConditionless()) return null;
			if (!property.Name.IsNullOrEmpty()) return property.Name;

			if (property.Expression != null && !property.CacheableExpression)
				return Resolve(property.Expression, property.Property);

			if (Properties.TryGetValue(property, out var propertyName))
				return propertyName;

			propertyName = Resolve(property.Expression, property.Property, true);
			Properties.TryAdd(property, propertyName);
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
				throw new ArgumentException("Name resolved to null for the given Expression or MemberInfo.");

			return name;
		}
	}
}
