using System;

// declares the profiling attrbutes in the event that the jetBrains self host dlls have not yet been bootstrapped by our fake build

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
