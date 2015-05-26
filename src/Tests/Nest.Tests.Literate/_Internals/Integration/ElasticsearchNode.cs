using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nest.Tests.Literate._Internals.Integration
{
	public class ElasticsearchNode : IDisposable
	{
		private static object _lock = new object();
		private Process _process;
		private IDisposable _processListener;

		public string Version { get; }
		public string Binary { get; }

		private string RoamingFolder { get; }
		private string RoamingClusterFolder { get; }

		public ElasticsearchNode(string elasticsearchVersion)
		{
			this.Version = elasticsearchVersion;

			var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			this.RoamingFolder = Path.Combine(appdata, "NEST", this.Version);
			this.RoamingClusterFolder = Path.Combine(this.RoamingFolder, "elasticsearch-" + elasticsearchVersion);
			this.Binary = Path.Combine(this.RoamingClusterFolder, "bin", "elasticsearch") + ".bat";
			
			this.DownloadAndExtractElasticsearch();
		}


		public IObservable<string> Start()
		{
			var handle  = new ManualResetEvent(false);
			this.Stop();

			this._process = this.CreateProcess(new[] { ""});

			var observable = Observable.Using(() => _process,
				process =>
				{
					return Observable.Create<string>(observer =>
					{
						// listen to stdout and stderr
						var stdOut = process.CreateStandardOutputObservable();
						var stdErr = process.CreateStandardErrorObservable();

						var stdOutSubscription = stdOut.Subscribe(observer);
						var stdErrSubscription = stdErr.Subscribe(observer);

						var processExited = Observable.FromEventPattern(h => process.Exited += h, h => process.Exited -= h);

						var processError = processExited.Subscribe(args =>
						{
							process.WaitForExit();
							try
							{
								if (process.ExitCode != 0)
								{
									observer.OnError(new Exception(
										string.Format("Process '{0}' terminated with error code {1}",
										process.StartInfo.FileName, process.ExitCode)));
								}
								else
								{
									observer.OnCompleted();
								}
							}
							finally
							{
								process.Close();
							}
						});

						process.Start();
						process.BeginOutputReadLine();
						process.BeginErrorReadLine();

						return new CompositeDisposable(stdOutSubscription, stdErrSubscription, processError);
					});
				});

			this._processListener = observable.Subscribe(onNext: s =>
			{
				if (s.Contains("started"))
					handle.Set();
			});

			var timeout = TimeSpan.FromSeconds(60);
			if (!handle.WaitOne(timeout, true))
				throw new ApplicationException($"Could not start elasticsearch within {timeout}");

			return observable;
		}

		private Process CreateProcess(IEnumerable<string> arguments)
		{
			return new Process
			{
				EnableRaisingEvents = true,
				StartInfo =
				{
					FileName = this.Binary,
					Arguments = string.Join(" ", arguments),
					CreateNoWindow = true,
					UseShellExecute = false,
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					RedirectStandardInput = false
				}
			};

		}

		private void DownloadAndExtractElasticsearch()
		{
			lock (_lock)
			{
				var zip = $"elasticsearch-{this.Version}.zip";
				var downloadUrl = $"https://download.elastic.co/elasticsearch/elasticsearch/{zip}";
				var localZip = Path.Combine(this.RoamingFolder, zip);

				Directory.CreateDirectory(this.RoamingFolder);
				if (!File.Exists(localZip))
				{
					//TODO write progress on console optionally
					new WebClient().DownloadFile(downloadUrl, localZip);
				}

				if (!Directory.Exists(this.RoamingClusterFolder))
				{
					//TODO write progress on console optionally
					ZipFile.ExtractToDirectory(localZip, this.RoamingFolder);
				}
			}

		}

		public void Stop()
		{
			this._process?.Kill();
			this._processListener?.Dispose();
		}

		public void Dispose()
		{
			this.Stop();
		}
	}
	public static class RxProcessUtilities
	{
		public static IObservable<string> CreateStandardErrorObservable(this Process process)
		{
			var receivedStdErr =
				Observable.FromEventPattern<DataReceivedEventHandler, DataReceivedEventArgs>
					(h => process.ErrorDataReceived += h, h => process.ErrorDataReceived -= h)
				.Select(e => e.EventArgs.Data);

			return Observable.Create<string>(observer =>
			{
				var cancel = Disposable.Create(process.CancelErrorRead);
				return new CompositeDisposable(cancel, receivedStdErr.Subscribe(observer));
			});
		}

		public static IObservable<string> CreateStandardOutputObservable(this Process process)
		{
			var receivedStdOut =
				Observable.FromEventPattern<DataReceivedEventHandler, DataReceivedEventArgs>
					(h => process.OutputDataReceived += h, h => process.OutputDataReceived -= h)
				.Select(e => e.EventArgs.Data);

			return Observable.Create<string>(observer =>
			{
				var cancel = Disposable.Create(process.CancelOutputRead);
				return new CompositeDisposable(cancel, receivedStdOut.Subscribe(observer));
			});
		}
	}
}
