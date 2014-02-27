using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Elasticsearch.Net.Connection;

namespace Elasticsearch.Net
{
	internal static class TypeExtensions
	{
		private static MethodInfo GetActivatorMethodInfo = typeof(TypeExtensions).GetMethod("GetActivator", BindingFlags.Static | BindingFlags.NonPublic);

		private static ConcurrentDictionary<string, ObjectActivator<object>> _cachedActivators = new ConcurrentDictionary<string, ObjectActivator<object>>(); 
		private static ConcurrentDictionary<string, Type> _cachedGenericClosedTypes = new ConcurrentDictionary<string, Type>();

		public delegate T ObjectActivator<out T>(params object[] args);
		

		internal static object CreateGenericInstance(this Type t, Type closeOver, params object[] args)
		{
			var key = t.FullName + "--" + closeOver.FullName;
			Type closedType;
			if (!_cachedGenericClosedTypes.TryGetValue(key, out closedType))
			{
				closedType = t.MakeGenericType(closeOver);
				_cachedGenericClosedTypes.TryAdd(key, closedType);
			}
			return closedType.CreateInstance(args);
		}

		internal static object CreateInstance(this Type t, params object[] args)
		{
			ObjectActivator<object> activator;
			var argLength = args.Count();
			var key = argLength + "--" + t.FullName;
			if (!_cachedActivators.TryGetValue(key, out activator))
			{
				var generic = GetActivatorMethodInfo.MakeGenericMethod(t);

				ConstructorInfo ctor = t.GetConstructors().FirstOrDefault(c=>c.GetParameters().Count() == argLength);
				if (ctor == null)
					throw new Exception("Cannot create an instance of " + t.FullName 
						+ "because it has no constructor taking " + argLength + " arguments");
				activator = (ObjectActivator<object>)generic.Invoke(null, new[] { ctor });
				_cachedActivators.TryAdd(key, activator);
			}
			return activator(args);
		}

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
	}
}
