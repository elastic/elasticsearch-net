using System;

#if !FEATURE_PROFILING
namespace Tests.Framework.Profiling.Performance
{
	public class PerformanceAttribute : Attribute
	{
		public int Iterations { get; set; }
	}
}
namespace Tests.Framework.Profiling.Timeline
{
	public class TimelineAttribute : Attribute
	{
		public int Iterations { get; set; }
	}
}

namespace Tests.Framework.Profiling.Memory
{
	public class MemoryAttribute : Attribute { }
}

namespace Tests.Framework.Profiling
{
	public class ProfilingSetupAttribute : Attribute { }
}
#endif
