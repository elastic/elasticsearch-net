using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Nest
{
	public static class TypeExtensions
	{
		private static MethodInfo GetActivatorMethodInfo = typeof(TypeExtensions).GetMethod("GetActivator", BindingFlags.Static | BindingFlags.NonPublic);

		private static ConcurrentDictionary<Type, ObjectActivator<object>> _cachedActivators = new ConcurrentDictionary<Type, ObjectActivator<object>>(); 

		public delegate T ObjectActivator<out T>(params object[] args);
		

		public static object CreateInstance(this Type t)
		{
			ObjectActivator<object> activator;
			if (!_cachedActivators.TryGetValue(t, out activator))
			{
				var generic = GetActivatorMethodInfo.MakeGenericMethod(t);
				ConstructorInfo ctor = t.GetConstructors().First();
				activator = (ObjectActivator<object>)generic.Invoke(null, new[] { ctor });
				_cachedActivators.TryAdd(t, activator);
			}
			return activator();
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
