// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Nest
{
	internal static class TypeExtensions
	{
		internal static readonly MethodInfo GetActivatorMethodInfo =
			typeof(TypeExtensions).GetMethod(nameof(GetActivator), BindingFlags.Static | BindingFlags.NonPublic);

		private static readonly ConcurrentDictionary<string, ObjectActivator<object>> CachedActivators =
			new ConcurrentDictionary<string, ObjectActivator<object>>();

		private static readonly ConcurrentDictionary<string, Type> CachedGenericClosedTypes =
			new ConcurrentDictionary<string, Type>();

		private static readonly ConcurrentDictionary<Type, List<PropertyInfo>> CachedTypePropertyInfos =
			new ConcurrentDictionary<Type, List<PropertyInfo>>();

		internal static object CreateGenericInstance(this Type t, Type closeOver, params object[] args) =>
			t.CreateGenericInstance(new[] { closeOver }, args);

		internal static object CreateGenericInstance(this Type t, Type[] closeOver, params object[] args)
		{
			var key = closeOver.Aggregate(new StringBuilder(t.FullName), (sb, gt) =>
			{
				sb.Append("--");
				return sb.Append(gt.FullName);
			}, sb => sb.ToString());
			if (!CachedGenericClosedTypes.TryGetValue(key, out var closedType))
			{
				closedType = t.MakeGenericType(closeOver);
				CachedGenericClosedTypes.TryAdd(key, closedType);
			}
			return closedType.CreateInstance(args);
		}

		internal static T CreateInstance<T>(this Type t, params object[] args) => (T)t.CreateInstance(args);

		internal static object CreateInstance(this Type t, params object[] args)
		{
			var key = t.FullName;
			var argKey = args.Length;
			if (args.Length > 0)
				key = argKey + "--" + key;
			if (CachedActivators.TryGetValue(key, out var activator))
				return activator(args);

			var generic = GetActivatorMethodInfo.MakeGenericMethod(t);
			var constructors = from c in t.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
				let p = c.GetParameters()
				where p.Length == args.Length
				select c;

			var ctor = constructors.FirstOrDefault();
			if (ctor == null)
				throw new Exception($"Cannot create an instance of {t.FullName} because it has no constructor taking {args.Length} arguments");

			activator = (ObjectActivator<object>)generic.Invoke(null, new object[] { ctor });
			CachedActivators.TryAdd(key, activator);
			return activator(args);
		}

		//do not remove this is referenced through GetActivatorMethod
		internal static ObjectActivator<T> GetActivator<T>(ConstructorInfo ctor)
		{
			var paramsInfo = ctor.GetParameters();

			//create a single param of type object[]
			var param = Expression.Parameter(typeof(object[]), "args");

			var argsExp = new Expression[paramsInfo.Length];

			//pick each arg from the params array
			//and create a typed expression of them
			for (var i = 0; i < paramsInfo.Length; i++)
			{
				var index = Expression.Constant(i);
				var paramType = paramsInfo[i].ParameterType;
				var paramAccessorExp = Expression.ArrayIndex(param, index);
				var paramCastExp = Expression.Convert(paramAccessorExp, paramType);
				argsExp[i] = paramCastExp;
			}

			//make a NewExpression that calls the
			//ctor with the args we just created
			var newExp = Expression.New(ctor, argsExp);

			//create a lambda with the New
			//Expression as body and our param object[] as arg
			var lambda = Expression.Lambda(typeof(ObjectActivator<T>), newExp, param);

			//compile it
			var compiled = (ObjectActivator<T>)lambda.Compile();
			return compiled;
		}

		internal static List<PropertyInfo> GetAllProperties(this Type type)
		{
			if (CachedTypePropertyInfos.TryGetValue(type, out var propertyInfos))
				return propertyInfos;

			var properties = new Dictionary<string, PropertyInfo>();
			GetAllPropertiesCore(type, properties);
			propertyInfos = properties.Values.ToList();
			CachedTypePropertyInfos.TryAdd(type, propertyInfos);
			return propertyInfos;
		}

		/// <summary>
		/// Returns inherited properties with reflectedType set to base type
		/// </summary>
		private static void GetAllPropertiesCore(Type type, Dictionary<string, PropertyInfo> collectedProperties)
		{
			foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
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
		}

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

		private static Type GetDeclaringType(this PropertyInfo propertyInfo) =>
			propertyInfo.GetBaseDefinition()?.DeclaringType ?? propertyInfo.DeclaringType;

		private static MethodInfo GetBaseDefinition(this PropertyInfo propertyInfo)
		{
			var m = propertyInfo.GetMethod;
			return m != null
				? m.GetBaseDefinition()
				: propertyInfo.SetMethod?.GetBaseDefinition();
		}

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

		internal delegate T ObjectActivator<out T>(params object[] args);

		private static readonly Assembly NestAssembly = typeof(TypeExtensions).Assembly;

		public static bool IsNestType(this Type type) => type.Assembly == NestAssembly;
	}
}
