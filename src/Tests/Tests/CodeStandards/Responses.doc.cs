using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.CodeStandards
{
	public class Responses
	{
		/**
		* Every collection property on a Response type should be either IReadOnlyCollection or IReadOnlyDictionary
		*/
		[U]
		public void ResponsePropertyCollectionsShouldBeReadOnly()
		{
			var exceptions = new HashSet<PropertyInfo>
			{
				typeof(ITypeMapping).GetProperty(nameof(ITypeMapping.DynamicDateFormats)),
				typeof(ITypeMapping).GetProperty(nameof(ITypeMapping.Meta)),
				typeof(TypeMapping).GetProperty(nameof(TypeMapping.DynamicDateFormats)),
				typeof(TypeMapping).GetProperty(nameof(TypeMapping.Meta)),
				typeof(IBulkResponse).GetProperty(nameof(IBulkResponse.ItemsWithErrors)),
				typeof(IFieldCapabilitiesResponse).GetProperty(nameof(IFieldCapabilitiesResponse.Fields)),
				typeof(IMultiSearchResponse).GetProperty(nameof(IMultiSearchResponse.AllResponses)),
			};

			var responseInterfaceTypes = from t in typeof(IResponse).Assembly().Types()
							    where t.IsInterface() && typeof(IResponse).IsAssignableFrom(t)
							    select t;

			var ruleBreakers = new List<string>();
			var seenTypes = new HashSet<Type>();
			foreach (var responseType in responseInterfaceTypes)
			{
				FindPropertiesBreakingRule(responseType, exceptions, seenTypes, ruleBreakers);
			}

			ruleBreakers.Should().BeEmpty();
		}

		private static readonly Type[] ResponseDictionaries = {typeof(AggregateDictionary)};

		private static void FindPropertiesBreakingRule(Type type, HashSet<PropertyInfo> exceptions, HashSet<Type> seenTypes, List<string> ruleBreakers)
		{
			if (!seenTypes.Add(type)) return;

			var properties = type.GetProperties();
			foreach (var propertyInfo in properties)
			{
				if (exceptions.Contains(propertyInfo))
					continue;

				if (typeof(IDictionary).IsAssignableFrom(propertyInfo.PropertyType) ||
				    typeof(ICollection).IsAssignableFrom(propertyInfo.PropertyType))
				{
					ruleBreakers.Add($"{type.FullName}.{propertyInfo.Name} is of type {propertyInfo.PropertyType.Name}");
				}
				else if (propertyInfo.PropertyType.IsGenericType())
				{
					var genericTypeDefinition = propertyInfo.PropertyType.GetGenericTypeDefinition();
					if (genericTypeDefinition == typeof(IDictionary<,>) ||
					    genericTypeDefinition == typeof(Dictionary<,>) ||
					    genericTypeDefinition == typeof(IEnumerable<>) ||
					    genericTypeDefinition == typeof(IList<>) ||
					    genericTypeDefinition == typeof(ICollection<>))
					{
						ruleBreakers.Add($"{type.FullName}.{propertyInfo.Name} is of type {propertyInfo.PropertyType.Name}");
					}
				}
				else if (propertyInfo.PropertyType.IsClass() &&
				         (propertyInfo.PropertyType.Namespace.StartsWith("Nest") || propertyInfo.PropertyType.Namespace.StartsWith("Elasticsearch.Net"))
				         //Do not traverse known response dictionaries
				         && !ResponseDictionaries.Contains(propertyInfo.PropertyType)
				)
				{
					FindPropertiesBreakingRule(propertyInfo.PropertyType, exceptions, seenTypes, ruleBreakers);
				}
			}
		}
	}
}
