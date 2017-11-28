using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Tests.Framework;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit
{
	class TestFrameworkExecutor : XunitTestFrameworkExecutor
	{
		public TestFrameworkExecutor(AssemblyName a, ISourceInformationProvider sip, IMessageSink d) : base(a, sip, d)
		{
		}

		protected override async void RunTestCases(
			IEnumerable<IXunitTestCase> testCases, IMessageSink sink, ITestFrameworkExecutionOptions options
		)
		{
			try
			{
				using (var runner = new TestAssemblyRunner(TestAssembly, testCases, DiagnosticMessageSink, sink, options))
				{
					await runner.RunAsync();
					Console.Out.Flush();
					if (runner.ClusterTotals.Count > 0)
					{
						Console.WriteLine("--------");
						Console.WriteLine("Individual cluster running times:");
						foreach (var kv in runner.ClusterTotals)
							Console.WriteLine($"- {kv.Key}: {kv.Value.Elapsed}");
						Console.WriteLine("--------");
					}

					DumpSeenDeprecations();

					if (runner.FailedCollections.Count > 0)
					{
						Console.WriteLine("--------");
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Failed collections:");
						foreach (var t in runner.FailedCollections.OrderBy(p => p.Item1).ThenBy(t => t.Item2))
						{
							var cluster = t.Item1;
							Console.WriteLine($" - {cluster}: {t.Item2}");
						}
						Console.WriteLine("--------");
					}
					DumpReproduceFilters(runner);
					Console.ResetColor();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		private static void DumpSeenDeprecations()
		{
			if (TestClient.SeenDeprecations.Count == 0) return;

			Console.WriteLine("-------- SEEN DEPRECATIONS");
			foreach (var d in TestClient.SeenDeprecations.Distinct())
				Console.WriteLine(d);
			Console.WriteLine("--------");
		}

		private static void DumpReproduceFilters(TestAssemblyRunner runner)
		{
			var config = TestClient.Configuration;
			var runningIntegrations = config.RunIntegrationTests;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("--------");
			var sb = new StringBuilder("build ")
				.Append($"seed:{config.Seed} ");
			if (config.UsingCustomSourceSerializer)
				sb.Append("source_serialization ");

			if (runningIntegrations)
				sb.Append("integrate ")
					.Append(TestClient.Configuration.ElasticsearchVersion);

			else sb.Append("test");

			if (runningIntegrations && runner.FailedCollections.Count > 0)
			{
                var clusters = string.Join(",", runner.FailedCollections
                    .Select(c => c.Item1.ToLowerInvariant()).Distinct());
                sb.Append(" \"");
				sb.Append(clusters);
                sb.Append("\"");
			}
			if ((!runningIntegrations || (runner.FailedCollections.Count < 30)) && runner.FailedCollections.Count > 0)
			{
				sb.Append(" \"");
				var tests = string.Join(",", runner.FailedCollections
					.OrderBy(t => t.Item2)
					.Select(c => c.Item2.ToLowerInvariant().Split('.').Last()
						.Replace("apitests", "")
						.Replace("usagetests", "")
						.Replace("tests", "")
					));
				sb.Append(tests);
				sb.Append("\"");
			}
			Console.WriteLine(sb.ToString());
			Console.WriteLine("--------");
		}
	}
}
