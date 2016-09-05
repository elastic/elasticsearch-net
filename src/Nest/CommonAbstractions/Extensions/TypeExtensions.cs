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
		private static readonly MethodInfo GetActivatorMethodInfo =
			typeof(TypeExtensions).GetMethod(nameof(GetActivator), BindingFlags.Static | BindingFlags.NonPublic);

		private static readonly ConcurrentDictionary<string, ObjectActivator<object>> CachedActivators =
			new ConcurrentDictionary<string, ObjectActivator<object>>();

		private static readonly ConcurrentDictionary<string, Type> CachedGenericClosedTypes =
			new ConcurrentDictionary<string, Type>();

		private static readonly ConcurrentDictionary<Type, IList<JsonProperty>> CachedTypeProperties =
			new ConcurrentDictionary<Type, IList<JsonProperty>>();

		//this contract is only used to resolve properties in class WE OWN.
		//these are not subject to change depending on what the user passes as connectionsettings
		private static readonly ElasticContractResolver JsonContract = new ElasticContractResolver(new ConnectionSettings(), null);

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
			if (!CachedGenericClosedTypes.TryGetValue(key, out closedType))
			{
				closedType = t.MakeGenericType(closeOver);
				CachedGenericClosedTypes.TryAdd(key, closedType);
			}
			return closedType.CreateInstance(args);
		}

		internal static T CreateInstance<T>(this Type t, params object[] args) => (T)t.CreateInstance(args);

		internal static object CreateInstance(this Type t, params object[] args)
		{
			ObjectActivator<object> activator;
			var argKey = args.Length;
			var key = argKey + "--" + t.FullName;
			if (CachedActivators.TryGetValue(key, out activator))
				return activator(args);

			var generic = GetActivatorMethodInfo.MakeGenericMethod(t);
			var constructors = from c in t.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
							   let p = c.GetParameters()
							   let k = string.Join(",", p.Select(a => a.ParameterType.Name))
							   where p.Length == args.Length
							   select c;

			var ctor = constructors.FirstOrDefault();
			if (ctor == null)
				throw new Exception($"Cannot create an instance of {t.FullName} because it has no constructor taking {args.Length} arguments");
			activator = (ObjectActivator<object>)generic.Invoke(null, new[] { ctor });
			CachedActivators.TryAdd(key, activator);
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
			if (CachedTypeProperties.TryGetValue(t, out propertyDictionary))
				return propertyDictionary;
			propertyDictionary = JsonContract.PropertiesOfAll(t, memberSerialization);
			CachedTypeProperties.TryAdd(t, propertyDictionary);
			return propertyDictionary;
		}
	}
}
