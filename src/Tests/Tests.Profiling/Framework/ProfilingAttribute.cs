using System;

namespace Tests.Profiling.Framework
{
	[AttributeUsage(AttributeTargets.Method)]
	public abstract class ProfilingAttribute : Attribute
	{
		public int Iterations { get; set; } = 1;
	}
}
