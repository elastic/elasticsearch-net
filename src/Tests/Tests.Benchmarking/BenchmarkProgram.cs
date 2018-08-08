using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Tests.Benchmarking
{
	public static class Program
	{
		public static int Main(string[] arguments)
		{
			Console.WriteLine("Running Benchmarking.");
			if (arguments.Count() >= 1 && arguments[0].Equals("non-interactive", StringComparison.OrdinalIgnoreCase))
			{
				Console.WriteLine("Running in Non-Interactive mode.");
				foreach (var benchmarkType in GetBenchmarkTypes())
				{
					BenchmarkRunner.Run(benchmarkType);
				}

				return 0;
			}

			Console.WriteLine("Running in Interactive mode.");
			var benchmarkSwitcher = new BenchmarkSwitcher(GetBenchmarkTypes());
			benchmarkSwitcher.Run(arguments);
			return 0;
		}


		private static Type[] GetBenchmarkTypes()
		{
			IEnumerable<Type> types;

			try
			{
				types = typeof(Program).GetTypeInfo().Assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException e)
			{
				types = e.Types.Where(t => t != null);
			}

			return types
				.Where(t => t.GetMethods(BindingFlags.Instance | BindingFlags.Public)
					.Any(m => m.GetCustomAttributes(typeof(BenchmarkAttribute), false).Any()))
				.OrderBy(t => t.Namespace)
				.ThenBy(t => t.Name)
				.ToArray();
		}
	}
}
