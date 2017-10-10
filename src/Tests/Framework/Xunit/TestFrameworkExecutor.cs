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
		public TestFrameworkExecutor(AssemblyName a, ISourceInformationProvider sip, IMessageSink d) : base(a, sip, d) { }

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
					Console.WriteLine("--------");
					Console.WriteLine("Individual cluster running times:");
					foreach (var kv in runner.ClusterTotals)
						Console.WriteLine($"- {kv.Key}: {kv.Value.Elapsed}");
					Console.WriteLine("--------");

					if (TestClient.Configuration.RunIntegrationTests && runner.FailedCollections.Count > 0)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Failed collections:");
                        foreach (var t in runner.FailedCollections)
                        {
                            var cluster = t.Item1;
                            Console.WriteLine($" - {cluster}: {t.Item2}");
                        }
						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.WriteLine("--------");
						var sb = new StringBuilder("build integrate ")
							.Append(TestClient.Configuration.ElasticsearchVersion)
							.Append(" \"");
						var clusters = string.Join(",", runner.FailedCollections
							.Select(c => c.Item1.ToLowerInvariant()).Distinct());
						sb.Append(clusters);
						sb.Append("\"");
						if (runner.FailedCollections.Count < 30)
						{
							sb.Append(" \"");
							var tests = string.Join(",", runner.FailedCollections
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
					Console.ResetColor();
				}

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	}
}
