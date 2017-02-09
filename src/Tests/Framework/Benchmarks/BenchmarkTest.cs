using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace Tests.Framework.Benchmarks
{
	[Config(typeof(BenchmarkConfig))]
	public abstract class BenchmarkTestBase
	{
	}
}
