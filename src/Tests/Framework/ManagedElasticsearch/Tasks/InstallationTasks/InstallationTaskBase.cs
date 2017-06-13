using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Tests.Framework.Configuration;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Nodes;
using Tests.Framework.ManagedElasticsearch.Process;
#if !DOTNETCORE
using XplatManualResetEvent = System.Threading.ManualResetEvent;
#endif

namespace Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks
{
	public abstract class InstallationTaskBase
	{
		public abstract void Run(NodeConfiguration config, NodeFileSystem fileSystem);
		
		private static bool IsMono { get; } = Type.GetType("Mono.Runtime") == null;
		protected string BinarySuffix => IsMono || Path.PathSeparator == '/' ? "" : ".bat";

		protected void DownloadFile(string from, string to)
		{
			if (File.Exists(to)) return;
			new WebClient().DownloadFile(from, to);
		}

		protected static void WriteFileIfNotExist(string fileLocation, string contents)
		{
			if (!File.Exists(fileLocation)) File.WriteAllText(fileLocation, contents);
		}

		protected void ExecuteBinary(string binary, string description, params string[] arguments)
		{
			Console.WriteLine($"Preparing to execute: {description} ...");
			var timeout = TimeSpan.FromSeconds(420);
			var handle = new XplatManualResetEvent(false);
			Task.Run(() =>
			{
				using (var p = new ObservableProcess(binary, arguments))
				{
					var o = p.Start();
					Console.WriteLine($"Executing: {binary} {string.Join(" ", arguments)}");
					o.Subscribe(c => Console.WriteLine(c.Data),
						(e) =>
						{
							Console.WriteLine($"Failed executing: {description} {e.Message} {e.StackTrace}");
							handle.Set();
							throw e;
						},
						() =>
						{
							Console.WriteLine($"Finished executing {description} exit code: {p.ExitCode}");
							handle.Set();
						});
					if (!handle.WaitOne(timeout, true))
						throw new Exception($"Timeout while executing {description} exceeded {timeout}");
				}
			});
			if (!handle.WaitOne(timeout, true))
				throw new Exception($"Timeout while executing {description} exceeded {timeout}");
		}
	}
}
