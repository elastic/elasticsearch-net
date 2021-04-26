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
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using System.Collections.Generic;
using System.Reflection;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net.Utf8Json;

namespace Tests.CodeStandards.Serialization
{
	public class Formatters
	{
		[U]
		public void CustomFormattersShouldBeInternal()
		{
			var formatters = typeof(IElasticClient).Assembly.GetTypes()
				.Concat(typeof(IElasticLowLevelClient).Assembly.GetTypes())
				.Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IJsonFormatter<>)))
				.ToList();
			var visible = new List<string>();
			foreach (var formatter in formatters)
			{
				if (formatter.IsVisible)
					visible.Add(formatter.Name);
			}
			visible.Should().BeEmpty();
		}

		[U]
		public void TypesAndPropertiesShouldBeAttributedWithCorrectlyTypedJsonFormatter()
		{
			Type GetFormatterTargetType(Type t)
			{
				var attribute = t.GetCustomAttribute<JsonFormatterAttribute>();

				if (attribute == null)
					return null;

				var formatterType = attribute.FormatterType;

				if (formatterType.IsGenericType && !formatterType.IsConstructedGenericType)
					return null;

				return formatterType.GetInterfaces()
					.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IJsonFormatter<>))
					.Select(i => i.GetGenericArguments()[0])
					.Single();
			}

			var typesAndProperties =
				from t in typeof(IElasticClient).Assembly.GetTypes().Concat(typeof(IElasticLowLevelClient).Assembly.GetTypes())
				let p = t.GetProperties()
				let typeHasFormatter = t.GetCustomAttribute<JsonFormatterAttribute>(false) != null
				let propertiesHaveFormatter = p.Any(pp => pp.GetCustomAttribute<JsonFormatterAttribute>(false) != null)
				where typeHasFormatter || propertiesHaveFormatter
				select new { Type = t, TypeHasFormatter = typeHasFormatter, Properties = p, PropertiesHaveFormatter = propertiesHaveFormatter };

			var invalid = new List<string>();

			foreach (var typeAndProperties in typesAndProperties)
			{
				if (typeAndProperties.TypeHasFormatter)
				{
					var t = typeAndProperties.Type;
					var f = GetFormatterTargetType(t);

					if (f != null && t != f)
						invalid.Add($"{t.FullName} has IJsonFormatter<{f.FullName}>");
				}

				if (typeAndProperties.PropertiesHaveFormatter)
				{
					foreach (var property in typeAndProperties.Properties)
					{
						var t = property.PropertyType;
						var f = GetFormatterTargetType(t);

						if (f != null && t != f)
							invalid.Add($"property {property.Name} on {typeAndProperties.Type.FullName} has IJsonFormatter<{f.FullName}>");
					}
				}
			}

			invalid.Should().BeEmpty();
		}
	}
}
