using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Tests.Profiling.Framework
{
	public class ProfiledMethod
	{
		public ProfiledMethod(MethodInfo methodInfo, ProfilingAttribute attribute)
		{
			this.MethodInfo = methodInfo;
			this.Attribute = attribute;
		}

		public MethodInfo MethodInfo { get; }

		public ProfilingAttribute Attribute { get; }

		public bool IsAsync => this.MethodInfo.ReturnType == typeof(Task);

		public Func<Task> CompileAsync(object instance)
		{
			if (this.MethodInfo.ReturnType != typeof(Task))
			{
				throw new ArgumentException("MethodInfo must return Task");
			}

			var thisParam = Expression.Constant(instance);
			var call = Expression.Call(thisParam, this.MethodInfo);
			var lambda = Expression.Lambda<Func<Task>>(call);
			return lambda.Compile();
		}

		public Action Compile(object instance)
		{
			if (this.MethodInfo.ReturnType != typeof(void))
			{
				throw new ArgumentException("MethodInfo must return void");
			}

			var thisParam = Expression.Constant(instance);
			var call = Expression.Call(thisParam, this.MethodInfo);
			var lambda = Expression.Lambda<Action>(call);
			return lambda.Compile();
		}
	}
}
