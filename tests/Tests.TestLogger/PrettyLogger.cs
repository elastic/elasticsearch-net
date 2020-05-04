// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

namespace Tests.Core.VsTest
{
	[FriendlyName(FriendlyName)]
	[ExtensionUri(ExtensionUri)]
	public class PrettyLogger : ITestLogger
	{
		private static readonly ConsoleColor DefaultBg = Console.BackgroundColor;
		private int _writtenPassed = 0;
		public const string ExtensionUri = "logger://Microsoft/TestPlatform/PrettyLogger/v1";
		public const string FriendlyName = "pretty";
		private readonly List<string> _disableSkipNamespaces = new List<string>();
		public static Uri RootUri { get; } = new Uri(Environment.CurrentDirectory + Path.DirectorySeparatorChar, UriKind.Absolute);

		private readonly ConcurrentQueue<TestResult> _failedTests = new ConcurrentQueue<TestResult>();

		public void Initialize(TestLoggerEvents events, string testRunDirectory)
		{
			events.TestResult += (s, e) =>
			{
				if (e.Result.Outcome != TestOutcome.Failed) return;
				_failedTests.Enqueue(e.Result);
			};

			//events.TestResult += TestResultHandler;
			events.TestRunComplete += TestRunCompleteHandler;
			events.TestRunStart += (sender, args) =>
			{
				var settingsXml = args.TestRunCriteria.TestRunSettings;
				if (string.IsNullOrWhiteSpace(settingsXml)) return;

				var settings = XDocument.Parse(settingsXml);
				var parameters = settings.Root?.XPathSelectElements("//TestRunParameters/Parameter");
				if (parameters == null) return;

				foreach (var para in parameters)
				{
					var name = para.Attribute("name")?.Value;
					var value = para.Attribute("value")?.Value;
					if (name == "DisableFullSkipMessages" && !string.IsNullOrWhiteSpace(value))
						_disableSkipNamespaces.AddRange(value
							.Split(';')
							.Select(s => s.Trim())
							.Where(s => !string.IsNullOrWhiteSpace(s))
						);
				}
			};
		}

		public void TestResultHandler(object sender, TestResultEventArgs e)
		{
			var testCase = e.Result.TestCase;
			var skipSkips = _disableSkipNamespaces.Any(n => testCase.FullyQualifiedName.StartsWith(n));
			var takingTooLong = e.Result.Duration > TimeSpan.FromSeconds(2);
			switch (e.Result.Outcome)
			{
				//case TestOutcome.Passed when !takingTooLong && !(isExamples || isReproduce): break;
				case TestOutcome.Skipped when skipSkips: break;
				case TestOutcome.Passed when !takingTooLong:

					Console.BackgroundColor = DefaultBg;
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write(".");
					Console.ResetColor();
					_writtenPassed++;
					if (_writtenPassed > Console.WindowWidth / 2)
					{
						Console.WriteLine();
						_writtenPassed = 0;
					}

					break;
				default:
					WriteTestResult(e.Result);

					break;
			}
		}

		private void WriteTestResult(TestResult result, bool longForm = true)
		{
			if (_writtenPassed > 0)
			{
				Console.WriteLine();
				_writtenPassed = 0;
			}
			var testCase = result.TestCase;
			PrintTestOutcomeHeader(result.Outcome, result.TestCase.FullyQualifiedName);
			switch (result.Outcome)
			{
				case TestOutcome.NotFound: break;
				case TestOutcome.None: break;
				case TestOutcome.Passed:
					PrintLocation(testCase);
					PrintDuration(result.Duration);
					break;
				case TestOutcome.Skipped:
					foreach (var p in result.Messages)
						p.Text.WriteWordWrapped();

					break;
				case TestOutcome.Failed:
					PrintLocation(testCase);
					PrintDuration(result.Duration);
					result.ErrorMessage.WriteWordWrapped(WordWrapper.WriteWithExceptionHighlighted, longForm);
					if (longForm)
						PrintStackTrace(result.ErrorStackTrace);
					break;
			}
		}

		private static int _slowTests = 0;
		private static void PrintDuration(TimeSpan duration)
		{
			var takingTooLong = duration > TimeSpan.FromSeconds(2);
			if (!takingTooLong) return;
			_slowTests++;
			var d = ToStringFromMilliseconds(duration.TotalMilliseconds);
			Console.ForegroundColor = ConsoleColor.Red;
			$"{nameof(TestResult.Duration)}: {d} is flagged as taking too long.".WriteWordWrapped();
			Console.ResetColor();
		}

		private static int _prettiedTraces = 0;

		private static void PrintStackTrace(string stackTrace)
		{
			if (string.IsNullOrWhiteSpace(stackTrace)) return;

			//If a huge amount of test fail, dont bother doing all this work.
			_prettiedTraces++;
			if (_prettiedTraces > 100)
			{
				Console.WriteLine(stackTrace);
				return;
			}
			Console.WriteLine();
			foreach (var line in stackTrace.Split(new[] { '\r', '\n' }))
			{
				if (!line.StartsWith("   at"))
				{
					Console.WriteLine(line);
					continue;
				}
				var atIn = line.Split(new[] { ") in " }, StringSplitOptions.RemoveEmptyEntries);
				var at = atIn[0] + ")";
				Console.WriteLine(at);
				if (atIn.Length <= 1) continue;

				var @in = atIn[1].Split(':');
				var file = @in[0];
				var lineNumber = @in[1];
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.Write("     in ");
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.Write(lineNumber);
				Console.Write(" ");
				Console.ForegroundColor = ConsoleColor.DarkGray;
				Console.WriteLine(file.CreateRelativePath());
				Console.ResetColor();
			}
			Console.WriteLine();
		}

		private static void PrintLocation(TestCase testCase)
		{
			if (testCase.LineNumber <= -1 || string.IsNullOrEmpty(testCase.CodeFilePath)) return;

			var relativeFile = testCase.CodeFilePath.CreateRelativePath();
			Console.ForegroundColor = ConsoleColor.Blue;
			$"line: {testCase.LineNumber} {relativeFile}".WriteWordWrapped();
			Console.ResetColor();
		}

		private void TestRunCompleteHandler(object sender, TestRunCompleteEventArgs e)
		{
			static void WriteBox(string boxString, ConsoleColor boxColor, string metric)
			{
				boxString = " " + boxString.PadRight(5);
				Console.ForegroundColor = ConsoleColor.White;
				Console.BackgroundColor = boxColor;
				Console.Write(boxString);
				Console.BackgroundColor = ConsoleColor.White;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.Write(' ');
				Console.Write(metric.PadRight(18));
				Console.ResetColor();
				Console.WriteLine();
			}


			//Reprint first 20 test failures at the bottom for convenience
			Announce($"SEEN {_failedTests.Count} FAILURE{(_failedTests.Count > 1 ? "S" : "")}");

			for (var expanded = 0; _failedTests.TryDequeue(out var testResult); expanded++)
			{
				WriteTestResult(testResult, expanded <= 20);
			}


			Announce(" 🌈 SUMMARY RESULTS 🌈 ");

			WriteBox("ALL", ConsoleColor.DarkGray, e.TestRunStatistics.ExecutedTests.ToString());

			foreach (var kv in e.TestRunStatistics.Stats)
			{
				var (boxString, color) = kv.Key switch
				{
					TestOutcome.Passed => ("PASS", ConsoleColor.Green),
					TestOutcome.Failed => ("FAIL", ConsoleColor.Red),
					TestOutcome.None => ("NONE", ConsoleColor.Gray),
					TestOutcome.NotFound => ("MISS", ConsoleColor.Gray),
					TestOutcome.Skipped => ("SKIP", ConsoleColor.Yellow),
					_ => ("UNKN", ConsoleColor.Cyan)
				};
				WriteBox(boxString, color, kv.Value.ToString());
			}

			Console.WriteLine();
			WriteBox("SLOW", _slowTests > 0 ? ConsoleColor.Red : ConsoleColor.Green, _slowTests.ToString());
			WriteBox("TIME", ConsoleColor.Gray, ToStringFromMilliseconds(e.ElapsedTimeInRunningTests.TotalMilliseconds));

			Console.WriteLine();
			Console.WriteLine();
		}

		private static void Announce(string text)
		{
			Console.WriteLine();
			var padding = new string(' ', text.Length + 4);
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Write(padding);
			Console.ResetColor();
			Console.WriteLine();
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Write($"  {text}  ");
			Console.ResetColor();
			Console.WriteLine();
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Write(padding);
			Console.ResetColor();
			Console.WriteLine();
			Console.WriteLine();
		}


		public static void PrintTestOutcomeHeader(TestOutcome testOutcome, string testCaseFullyQualifiedName)
		{
			Console.ForegroundColor = ConsoleColor.White;
			switch (testOutcome)
			{
				case TestOutcome.Passed:
					Console.BackgroundColor = ConsoleColor.Green;
					Console.Write(" PASS ");
					break;
				case TestOutcome.Failed:
					Console.BackgroundColor = ConsoleColor.Red;
					Console.Write("[FAIL]");
					break;
				case TestOutcome.None:
					Console.BackgroundColor = ConsoleColor.Gray;
					Console.Write(" NONE ");
					break;
				case TestOutcome.NotFound:
					Console.BackgroundColor = ConsoleColor.Gray;
					Console.Write(" MISS ");
					break;
				case TestOutcome.Skipped:
					Console.BackgroundColor = ConsoleColor.DarkYellow;
					Console.Write(" SKIP ");
					break;
			}
			var bg = Console.BackgroundColor;
			Console.ResetColor();
			Console.ForegroundColor = bg;
			Console.WriteLine($" {testCaseFullyQualifiedName}");
			Console.ResetColor();
		}
		//Taken from https://github.com/adamralph/bullseye/
		private static readonly IFormatProvider Provider = CultureInfo.InvariantCulture;

		private static string ToStringFromMilliseconds(double milliseconds, bool @fixed = false)
		{
			// less than one millisecond
			if (milliseconds < 1D)
			{
				return "<1 ms";
			}

			// milliseconds
			if (milliseconds < 1_000D)
			{
				return milliseconds.ToString(@fixed ? "F0" : "G3", Provider) + " ms";
			}

			// seconds
			if (milliseconds < 60_000D)
			{
				return (milliseconds / 1_000D).ToString(@fixed ? "F2" : "G3", Provider) + " s";
			}

			// minutes and seconds
			if (milliseconds < 3_600_000D)
			{
				var minutes = Math.Floor(milliseconds / 60_000D).ToString("F0", Provider);
#pragma warning disable IDE0047 // Remove unnecessary parentheses
				var seconds = ((milliseconds % 60_000D) / 1_000D).ToString("F0", Provider);
#pragma warning restore IDE0047 // Remove unnecessary parentheses
				return seconds == "0"
					? $"{minutes} m"
					: $"{minutes} m {seconds} s";
			}

			// minutes
			return (milliseconds / 60_000d).ToString("N0", Provider) + " m";
		}

	}
}
