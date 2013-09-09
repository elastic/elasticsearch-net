using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	class Program
	{
		static void Main(string[] args)
		{
			RunTest("Wrapped in LongRunning task:",
				() => Task.Factory.StartNew(() => Task.Delay(4000).Wait(), TaskCreationOptions.LongRunning));

			RunTest("Not wrapped:", () => Task.Delay(4000));

			Console.ReadLine();
		}

		private static void RunTest(string description, Func<Task> createTask)
		{
			var tasks = Enumerable.Range(0, 1000).Select(i => createTask()).ToArray();
			var process = Process.GetCurrentProcess();
			Console.WriteLine("Before wait(): {0}", description);
			Console.WriteLine("\tThreads: " + process.Threads.Count);
			Console.WriteLine("\tVirtual memory: " + process.VirtualMemorySize64);

			Task.WhenAll(tasks).Wait();

			Console.WriteLine("After wait(): {0}", description);
			
			Console.WriteLine("\tThreads: " + process.Threads.Count);
			Console.WriteLine("\tVirtual memory: " + process.VirtualMemorySize64);
		}
	}
}
