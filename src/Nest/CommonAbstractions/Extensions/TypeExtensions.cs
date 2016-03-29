using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Nest
{
	internal static class TypeExtensions
	{
		private static MethodInfo GetActivatorMethodInfo = typeof(TypeExtensions).GetMethod("GetActivator", BindingFlags.Static | BindingFlags.NonPublic);

		private static ConcurrentDictionary<string, ObjectActivator<object>> _cachedActivators = new ConcurrentDictionary<string, ObjectActivator<object>>();
		private static ConcurrentDictionary<string, Type> _cachedGenericClosedTypes = new ConcurrentDictionary<string, Type>();


		private static ConcurrentDictionary<Type, IList<JsonProperty>> _cachedTypeProperties =
			new ConcurrentDictionary<Type, IList<JsonProperty>>();

		//this contract is only used to resolve properties in class WE OWN.
		//these are not subject to change depending on what the user passes as connectionsettings
		private static ElasticContractResolver _jsonContract = new ElasticContractResolver(new ConnectionSettings(), null);

		public delegate T ObjectActivator<out T>(params object[] args);

		internal static object CreateGenericInstance(this Type t, Type closeOver, params object[] args)
		{
			return t.CreateGenericInstance(new[] {closeOver}, args);
		}

		internal static object CreateGenericInstance(this Type t, Type[] closeOver, params object[] args)
		{
			var argKey = closeOver.Aggregate(new StringBuilder(), (sb, gt) => sb.Append("--" + gt.FullName), sb => sb.ToString());
			var key = t.FullName + argKey;
			Type closedType;
			if (!_cachedGenericClosedTypes.TryGetValue(key, out closedType))
			{
				closedType = t.MakeGenericType(closeOver);
				_cachedGenericClosedTypes.TryAdd(key, closedType);
			}
			return closedType.CreateInstance(args);
		}

		internal static T CreateInstance<T>(this Type t, params object[] args) => (T)t.CreateInstance(args);

		internal static object CreateInstance(this Type t, params object[] args)
		{
			ObjectActivator<object> activator;
			var argLength = args.Count();
			//var argKey = string.Join(",", args.Select(a => a.GetType().Name));
			var argKey = argLength;
			var key = argKey + "--" + t.FullName;
			if (_cachedActivators.TryGetValue(key, out activator))
				return activator(args);
			var generic = GetActivatorMethodInfo.MakeGenericMethod(t);

			var constructors = from c in t.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
							   let p = c.GetParameters()
							   let k = string.Join(",", p.Select(a => a.ParameterType.Name))
							   where p.Count() == argLength //&& k == argKey
							   select c;
			var ctor = constructors.FirstOrDefault();
			if (ctor == null)
				throw new Exception("Cannot create an instance of " + t.FullName
				                    + " because it has no constructor taking " + argLength + " arguments");
			activator = (ObjectActivator<object>)generic.Invoke(null, new[] { ctor });
			_cachedActivators.TryAdd(key, activator);
			return activator(args);
		}

		//do not remove this is referenced through GetActivatorMethod
		private static ObjectActivator<T> GetActivator<T>(ConstructorInfo ctor)
		{
			Type type = ctor.DeclaringType;
			ParameterInfo[] paramsInfo = ctor.GetParameters();

			//create a single param of type object[]
			ParameterExpression param =
				Expression.Parameter(typeof(object[]), "args");

			Expression[] argsExp =
				new Expression[paramsInfo.Length];

			//pick each arg from the params array 
			//and create a typed expression of them
			for (int i = 0; i < paramsInfo.Length; i++)
			{
				Expression index = Expression.Constant(i);
				Type paramType = paramsInfo[i].ParameterType;

				Expression paramAccessorExp =
					Expression.ArrayIndex(param, index);

				Expression paramCastExp =
					Expression.Convert(paramAccessorExp, paramType);

				argsExp[i] = paramCastExp;
			}

			//make a NewExpression that calls the
			//ctor with the args we just created
			NewExpression newExp = Expression.New(ctor, argsExp);

			//create a lambda with the New
			//Expression as body and our param object[] as arg
			LambdaExpression lambda =
				Expression.Lambda(typeof(ObjectActivator<T>), newExp, param);

			//compile it
			ObjectActivator<T> compiled = (ObjectActivator<T>)lambda.Compile();
			return compiled;
		}

		internal static IList<JsonProperty> GetCachedObjectProperties(this Type t, MemberSerialization memberSerialization = MemberSerialization.OptIn)
		{
			IList<JsonProperty> propertyDictionary;
			if (_cachedTypeProperties.TryGetValue(t, out propertyDictionary))
				return propertyDictionary;
			propertyDictionary = _jsonContract.PropertiesOfAll(t, memberSerialization);
			_cachedTypeProperties.TryAdd(t, propertyDictionary);
			return propertyDictionary;
		}

#if DOTNETCORE
		internal static bool IsAssignableFrom(this Type t, Type other) => t.GetTypeInfo().IsAssignableFrom(other.GetTypeInfo());
#endif

		internal static bool IsGeneric(this Type type)
		{
#if DOTNETCORE
			return type.GetTypeInfo().IsGenericType;
#else
			return type.IsGenericType;
#endif
		}

		internal static bool IsValue(this Type type)
		{
#if DOTNETCORE
			return type.GetTypeInfo().IsValueType;
#else
			return type.IsValueType;
#endif
		}

		internal static bool IsEnumType(this Type type)
		{
#if DOTNETCORE
			return type.GetTypeInfo().IsEnum;
#else
			return type.IsEnum;
#endif
		}

#if DOTNETCORE
		internal static IEnumerable<Type> GetInterfaces(this Type type)
		{
			return type.GetTypeInfo().ImplementedInterfaces;
		}
#endif
	}
}