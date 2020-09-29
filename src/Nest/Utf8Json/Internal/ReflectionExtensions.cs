#region Utf8Json License https://github.com/neuecc/Utf8Json/blob/master/LICENSE
// MIT License
//
// Copyright (c) 2017 Yoshifumi Kawai
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Elasticsearch.Net.Utf8Json.Internal
{
    internal static class ReflectionExtensions
    {
		private static readonly ThreadsafeTypeKeyHashTable<MethodInfo> ShouldSerializeMethodInfo =
			new ThreadsafeTypeKeyHashTable<MethodInfo>();

		/// <summary>
		/// Gets all the properties for a type, including base type and interface properties
		/// </summary>
		public static IEnumerable<PropertyInfo> GetAllProperties(this Type type)
		{
			var properties = new Dictionary<string, PropertyInfo>();
			GetAllPropertiesCore(type, properties);
			return properties.Values;
		}

		private static void GetAllPropertiesCore(Type type, Dictionary<string, PropertyInfo> collectedProperties)
		{
			foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
			{
				if (collectedProperties.TryGetValue(property.Name, out var existingProperty))
				{
					if (IsHidingMember(property))
						collectedProperties[property.Name] = property;
					else if (!type.IsInterface && property.IsVirtual())
					{
						var propertyDeclaringType = property.GetDeclaringType();

						if (!(existingProperty.IsVirtual() && existingProperty.GetDeclaringType().IsAssignableFrom(propertyDeclaringType)))
						{
							collectedProperties[property.Name] = property;
						}
					}
				}
				else
					collectedProperties.Add(property.Name, property);
			}

			if (type.BaseType != null)
				GetAllPropertiesCore(type.BaseType, collectedProperties);

			foreach (var @interface in type.GetInterfaces())
				GetAllPropertiesCore(@interface, collectedProperties);
		}

		public static IEnumerable<FieldInfo> GetAllFields(this Type type) => GetAllFieldsCore(type, new HashSet<string>());

		private static IEnumerable<FieldInfo> GetAllFieldsCore(Type type, HashSet<string> nameCheck)
        {
            foreach (var item in type.GetRuntimeFields())
            {
                if (nameCheck.Add(item.Name))
                {
                    yield return item;
                }
            }
            if (type.BaseType != null)
            {
                foreach (var item in GetAllFieldsCore(type.BaseType, nameCheck))
                {
                    yield return item;
                }
            }
        }

		private static Type GetDeclaringType(this PropertyInfo propertyInfo) =>
			propertyInfo.GetBaseDefinition()?.DeclaringType ?? propertyInfo.DeclaringType;

		private static MethodInfo GetBaseDefinition(this PropertyInfo propertyInfo)
		{
			var m = propertyInfo.GetMethod;
			return m != null
				? m.GetBaseDefinition()
				: propertyInfo.SetMethod?.GetBaseDefinition();
		}

		public static MethodInfo GetShouldSerializeMethod(this Type type) =>
			ShouldSerializeMethodInfo.GetOrAdd(type, t =>
			{
				return t.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
					.FirstOrDefault(m => m.Name == "ShouldSerialize"
						&& m.ReturnType == typeof(bool)
						&& m.GetParameters().Length == 1
						&& m.GetParameters()[0].ParameterType == typeof(IJsonFormatterResolver));
			});

		/// <summary>
		/// Determines if a type is an anonymous type
		/// </summary>
		public static bool IsAnonymous(this Type type) =>
			type.GetCustomAttribute<CompilerGeneratedAttribute>() != null
			&& type.Name.Contains("AnonymousType")
			&& (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"))
			&& (type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic;

		/// <summary>
		/// Determines if a <see cref="PropertyInfo"/> is hiding/shadowing a
		/// <see cref="PropertyInfo"/> from a base type
		/// </summary>
		private static bool IsHidingMember(PropertyInfo propertyInfo)
		{
			var baseType = propertyInfo.DeclaringType?.BaseType;
			var baseProperty = baseType?.GetProperty(propertyInfo.Name);
			if (baseProperty == null)
				return false;

			var derivedGetMethod = propertyInfo.GetBaseDefinition();
			return derivedGetMethod?.ReturnType != propertyInfo.PropertyType;
		}

		/// <summary>
		/// Determines if the type is a nullable type
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNullable(this Type type) =>
			type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

		/// <summary>
		/// Determines if a <see cref="PropertyInfo"/> is virtual
		/// </summary>
		private static bool IsVirtual(this PropertyInfo propertyInfo)
		{
			var methodInfo = propertyInfo.GetMethod;
			if (methodInfo != null && methodInfo.IsVirtual)
				return true;

			methodInfo = propertyInfo.SetMethod;
			return methodInfo != null && methodInfo.IsVirtual;
		}

		/// <summary>
		/// Gets the methods for a type matching the given name
		/// </summary>
		public static IEnumerable<MethodInfo> GetDeclaredMethods(this Type type, string name)
	    {
			var methods = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			for (var index = 0; index < methods.Length; ++index)
			{
				var method = methods[index];
				if (method.Name == name)
					yield return method;
			}
	    }

		/// <summary>
		/// Gets all the constructors for a type
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ConstructorInfo[] GetDeclaredConstructors(this Type type) =>
			type.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
	}
}
