using System;

namespace Tests.Framework.Profiling
{
	[AttributeUsage(AttributeTargets.Method)]
	public abstract class ProfilingAttribute : Attribute
	{
		public int Iterations { get; set; } = 1;
	}
}
