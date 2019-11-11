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
        public static bool IsNullable(this TypeInfo type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static bool IsPublic(this TypeInfo type)
        {
            return type.IsPublic;
        }

        public static bool IsAnonymous(this TypeInfo type)
        {
            return type.GetCustomAttribute<CompilerGeneratedAttribute>() != null
                && type.Name.Contains("AnonymousType")
                && (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"))
                && (type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic;
        }

        private static readonly ThreadsafeTypeKeyHashTable<MethodInfo> ShouldSerializeMethodInfo =
			new ThreadsafeTypeKeyHashTable<MethodInfo>();

		public static MethodInfo GetShouldSerializeMethod(this TypeInfo type)
		{
			return ShouldSerializeMethodInfo.GetOrAdd(type, t =>
			{
				return t.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
					.FirstOrDefault(m => m.Name == "ShouldSerialize"
						&& m.ReturnType == typeof(bool)
						&& m.GetParameters().Length == 1
						&& m.GetParameters()[0].ParameterType == typeof(IJsonFormatterResolver));
			});
		}

        public static IEnumerable<PropertyInfo> GetAllProperties(this Type type)
        {
            return GetAllPropertiesCore(type, new HashSet<string>());
        }

        static IEnumerable<PropertyInfo> GetAllPropertiesCore(Type type, HashSet<string> nameCheck)
        {
            foreach (var item in type.GetRuntimeProperties())
            {
                if (nameCheck.Add(item.Name))
                {
                    yield return item;
                }
            }
            if (type.BaseType != null)
            {
                foreach (var item in GetAllPropertiesCore(type.BaseType, nameCheck))
                {
                    yield return item;
                }
            }
			foreach (var @interface in type.GetInterfaces())
			{
				foreach (var item in GetAllPropertiesCore(@interface, nameCheck))
				{
					yield return item;
				}
			}
        }

        public static IEnumerable<FieldInfo> GetAllFields(this Type type)
        {
            return GetAllFieldsCore(type, new HashSet<string>());
        }

        static IEnumerable<FieldInfo> GetAllFieldsCore(Type type, HashSet<string> nameCheck)
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

        public static bool IsConstructedGenericType(this TypeInfo type)
        {
            return type.AsType().IsConstructedGenericType;
        }

        public static MethodInfo GetGetMethod(this PropertyInfo propInfo)
        {
            return propInfo.GetMethod;
        }

        public static MethodInfo GetSetMethod(this PropertyInfo propInfo)
        {
            return propInfo.SetMethod;
        }
    }
}
