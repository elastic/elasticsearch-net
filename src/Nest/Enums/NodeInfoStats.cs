using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[Flags]
	public enum NodeInfoStats
	{
		None = 0,
		Indices = 1 << 1,
		OS = 1 << 2,
		Process = 1 << 3,
		JVM = 1 << 4,
		ThreadPool = 1 << 5,
		Network = 1 << 6,
		Transport = 1 << 7,
		HTTP = 1 << 8,
		FileSystem = 1 << 9,
		All = Indices | OS | Process | JVM | ThreadPool | Network | Transport | HTTP | FileSystem
	}
}
