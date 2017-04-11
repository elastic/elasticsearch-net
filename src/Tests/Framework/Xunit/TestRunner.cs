using System;
using System.Threading;
using Xunit.Runners;
using System.Reflection;
using Tests.Framework;
using Xunit;
using System.Collections.ObjectModel;
using System.Collections.Concurrent;

namespace Tests
{
	class TestRunner
	{
		// We use consoleLock because messages can arrive in parallel, so we want to make sure we get
		// consistent console output.
		static object consoleLock = new object();

		// Use an event to know when we're done
		static ManualResetEvent finished = new ManualResetEvent(false);

		// Start out assuming success; we'll set this to 1 if we get a failed test
		static int _result = 0;

		private static readonly ConcurrentBag<TestFailedInfo> FailedTests = new ConcurrentBag<TestFailedInfo>();

		public static int Run(string[] args)
		{

			var testAssembly = typeof(TestRunner).Assembly();
			var clrVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(typeof(int).Assembly().Location).ProductVersion;
			Console.WriteLine($"Running tests from {testAssembly.Location} using CLR: {clrVersion}");
			try
			{
				return RunTests(testAssembly);
			}
			catch (InvalidOperationException e) when (e.Message.Contains("runner when it's not idle"))
			{
				Console.WriteLine(e);
			}
			return _result;
		}

		private static int RunTests(Assembly testAssembly)
		{
#if DOTNETCORE
			using (var runner = AssemblyRunner.WithoutAppDomain(testAssembly.Location))
#else
			using (var runner = AssemblyRunner.WithAppDomain(testAssembly.Location))
#endif
			{
				runner.OnDiscoveryComplete = OnDiscoveryComplete;
				runner.OnExecutionComplete = OnExecutionComplete;
				runner.OnTestFailed = OnTestFailed;
				runner.OnTestSkipped = OnTestSkipped;

				Console.WriteLine("Discovering...");
				runner.Start();

				finished.WaitOne();
				finished.Dispose();
				return _result;
			}
		}

		static void OnDiscoveryComplete(DiscoveryCompleteInfo info)
		{
			lock (consoleLock)
				Console.WriteLine($"Running {info.TestCasesToRun} of {info.TestCasesDiscovered} tests...");
		}

		static void OnExecutionComplete(ExecutionCompleteInfo info)
		{
			lock (consoleLock)
			{
				foreach (var t in FailedTests)
				{
					Console.ForegroundColor = ConsoleColor.Red;

					Console.Write("[FAIL] {0}: ", t.TestDisplayName);
					Console.ForegroundColor = ConsoleColor.Blue;
					Console.WriteLine(t.ExceptionMessage);
					Console.ResetColor();
				}

				Console.WriteLine($"Finished: {info.TotalTests} tests in {Math.Round(info.ExecutionTime, 3)}s ({info.TestsFailed} failed, {info.TestsSkipped} skipped)");
			}

			finished.Set();
		}

		static void OnTestFailed(TestFailedInfo info)
		{
			FailedTests.Add(info);
			lock (consoleLock)
			{
				Console.ForegroundColor = ConsoleColor.Red;

				Console.Write("[FAIL] {0}: ", info.TestDisplayName);
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.WriteLine(info.ExceptionMessage);
				if (info.ExceptionStackTrace != null)
				{
					Console.ForegroundColor = ConsoleColor.DarkGray;
					Console.WriteLine(info.ExceptionStackTrace);
				}

				Console.ResetColor();
			}

			_result = 1;
		}

		static void OnTestSkipped(TestSkippedInfo info)
		{
			lock (consoleLock)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine("[SKIP] {0}: {1}", info.TestDisplayName, info.SkipReason);
				Console.ResetColor();
			}
		}
	}
}
