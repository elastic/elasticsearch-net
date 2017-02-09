using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Tests.Framework.Profiling
{
	public class ProfiledMethod
	{
		public ProfiledMethod(MethodInfo methodInfo, ProfilingAttribute attribute)
		{
			MethodInfo = methodInfo;
			Attribute = attribute;
		}

		public MethodInfo MethodInfo { get; }

		public ProfilingAttribute Attribute { get; }

		public bool IsAsync => MethodInfo.ReturnType == typeof(Task);

		public Func<Task> CompileAsync(object instance)
		{
			if (MethodInfo.ReturnType != typeof(Task))
			{
				throw new ArgumentException("MethodInfo must return Task");
			}

			var thisParam = Expression.Constant(instance);
			var call = Expression.Call(thisParam, MethodInfo);
			var lambda = Expression.Lambda<Func<Task>>(call);
			return lambda.Compile();
		}

		public Action Compile(object instance)
		{
			if (MethodInfo.ReturnType != typeof(void))
			{
				throw new ArgumentException("MethodInfo must return void");
			}

			var thisParam = Expression.Constant(instance);
			var call = Expression.Call(thisParam, MethodInfo);
			var lambda = Expression.Lambda<Action>(call);
			return lambda.Compile();
		}
	}
}
