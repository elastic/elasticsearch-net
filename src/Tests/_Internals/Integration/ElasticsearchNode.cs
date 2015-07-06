using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace Tests._Internals.Integration
{
	public class ElasticsearchNode : IDisposable
	{
		private static readonly object _lock = new object();
		private Process _process;
		private IDisposable _processListener;

		public string Version { get; }
		public string Binary { get; }

		private string RoamingFolder { get; }
		private string RoamingClusterFolder { get; }

		public bool Started { get; private set; }
		public bool RunningIntegrations { get; private set; }
		public string ClusterName { get; } = Guid.NewGuid().ToString("N").Substring(0, 6);
		public string NodeName { get; } = Guid.NewGuid().ToString("N").Substring(0, 6);

		public ElasticsearchNodeInfo Info { get; private set; }
		public int Port { get; private set; }

		public ElasticsearchNode(string elasticsearchVersion, bool runningIntegrations)
		{
			this.Version = elasticsearchVersion;
			this.RunningIntegrations = runningIntegrations;

			if (!runningIntegrations)
			{
				this.Port = 9200;
				return;
			}

			var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			this.RoamingFolder = Path.Combine(appdata, "NEST", this.Version);
			this.RoamingClusterFolder = Path.Combine(this.RoamingFolder, "elasticsearch-" + elasticsearchVersion);
			this.Binary = Path.Combine(this.RoamingClusterFolder, "bin", "elasticsearch") + ".bat";

			Console.WriteLine("========> {0}", this.RoamingFolder);
			this.DownloadAndExtractElasticsearch();
		}

		public IObservable<ElasticsearchMessage> Start()
		{
			if (!this.RunningIntegrations) return Observable.Empty<ElasticsearchMessage>();

			var handle  = new ManualResetEvent(false);
			this.Stop();

			this._process = this.CreateProcess(
				$"-Des.cluster.name={this.ClusterName}",
				$"-Des.node.name={this.NodeName}",
				"-Des.discovery.zen.ping.multicast.enabled=false"
			);

			var observable = Observable.Using(() => _process, process => StartObservableProcess(process));
			this._processListener = observable.Subscribe(onNext: s => HandleConsoleMessage(s, handle));

			var timeout = TimeSpan.FromSeconds(60);
			if (!handle.WaitOne(timeout, true))
				throw new ApplicationException($"Could not start elasticsearch within {timeout}");

			return observable;
		}

		private static IObservable<ElasticsearchMessage> StartObservableProcess(Process process)
		{
			return Observable.Create<ElasticsearchMessage>(observer =>
			{
				// listen to stdout and stderr
				var stdOut = process.CreateStandardOutputObservable();
				var stdErr = process.CreateStandardErrorObservable();

				var stdOutSubscription = stdOut.Subscribe(observer);
				var stdErrSubscription = stdErr.Subscribe(observer);

				var processExited = Observable.FromEventPattern(h => process.Exited += h, h => process.Exited -= h);
				var processError = CreateProcessExitSubscription(process, processExited, observer);

				process.Start();
				process.BeginOutputReadLine();
				process.BeginErrorReadLine();

				return new CompositeDisposable(stdOutSubscription, stdErrSubscription, processError);
			});
		}

		private static IDisposable CreateProcessExitSubscription(Process process, IObservable<EventPattern<object>> processExited, IObserver<ElasticsearchMessage> observer)
		{
			return processExited.Subscribe(args =>
			{
				try
				{
					if (process?.ExitCode > 0)
					{
						observer.OnError(new Exception(
							$"Process '{process.StartInfo.FileName}' terminated with error code {process.ExitCode}"));
					}
					else
					{
						observer.OnCompleted();
					}
				}
				finally
				{
					process?.Close();
				}
			});
		}

		private void HandleConsoleMessage(ElasticsearchMessage s, ManualResetEvent handle)
		{
			//no need to snoop for metadata if we already started
			if (!this.RunningIntegrations || this.Started) return;

			ElasticsearchNodeInfo info;
			int port;

			if (s.TryParseNodeInfo(out info))
			{
				this.Info = info;
			}
			else if (s.TryGetStartedConfirmation())
			{
				handle.Set();
				this.Started = true;
			}
			else if (s.TryGetPortNumber(out port))
			{
				this.Port = port;
			}
		}

		private Process CreateProcess(params string[] arguments)
		{
			var a = string.Join(" ", arguments);
			Console.WriteLine(a);
			return new Process
			{
				EnableRaisingEvents = true,
				StartInfo =
				{
					FileName = this.Binary,
					Arguments = a,
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
			Console.WriteLine($"Stopping... ran integrations: {this.RunningIntegrations}");
			Console.WriteLine($"Node started: {this.Started} on port: {this.Port} using PID: {this.Info?.Pid}");

			if (!this.RunningIntegrations || !this.Started) return;

			this.Started = false;
			if (this.Info != null)
			{
				var esProcess = Process.GetProcessById(this.Info.Pid);
				Console.WriteLine($"Killing elasticsearch PID {this.Info.Pid}");
				esProcess.Kill();
				esProcess.WaitForExit(5000);
				esProcess.Close();
			}

			this._process?.Kill();
			this._process?.WaitForExit(2000);
			this._process?.Close();
			this._processListener?.Dispose();

			var dataFolder = Path.Combine(this.RoamingClusterFolder, "data", this.ClusterName);
			if (Directory.Exists(dataFolder))
			{
				Console.WriteLine($"attempting to delete cluster data: {dataFolder}");
				Directory.Delete(dataFolder, true);
			}
			var logPath = Path.Combine(this.RoamingClusterFolder, "logs");
			var files = Directory.GetFiles(logPath, this.ClusterName + "*.log");
			foreach (var f in files)
			{
				Console.WriteLine($"attempting to delete log file: {f}");
				File.Delete(f);
			}
		}

		public void Dispose()
		{
			this.Stop();
		}
	}

	public class ElasticsearchMessage
	{
		/*
[2015-05-26 20:05:07,681][INFO ][node                     ] [Nick Fury] version[1.5.2], pid[7704], build[62ff986/2015-04-27T09:21:06Z]
[2015-05-26 20:05:07,681][INFO ][node                     ] [Nick Fury] initializing ...
[2015-05-26 20:05:07,681][INFO ][plugins                  ] [Nick Fury] loaded [], sites []
[2015-05-26 20:05:10,790][INFO ][node                     ] [Nick Fury] initialized
[2015-05-26 20:05:10,821][INFO ][node                     ] [Nick Fury] starting ...
[2015-05-26 20:05:11,041][INFO ][transport                ] [Nick Fury] bound_address {inet[/0:0:0:0:0:0:0:0:9300]}, publish_address {inet[/192.168.194.146:9300]}
[2015-05-26 20:05:11,056][INFO ][discovery                ] [Nick Fury] elasticsearch-martijnl/yuiyXva3Si6sQE5tY_9CHg
[2015-05-26 20:05:14,103][INFO ][cluster.service          ] [Nick Fury] new_master [Nick Fury][yuiyXva3Si6sQE5tY_9CHg][WIN-DK60SLEMH8C][inet[/192.168.194.146:9300]], reason: zen-disco-join (elected_as_master)
[2015-05-26 20:05:14,134][INFO ][gateway                  ] [Nick Fury] recovered [0] indices into cluster_state
[2015-05-26 20:05:14,150][INFO ][http                     ] [Nick Fury] bound_address {inet[/0:0:0:0:0:0:0:0:9200]}, publish_address {inet[/192.168.194.146:9200]}
[2015-05-26 20:05:14,150][INFO ][node                     ] [Nick Fury] started
*/

		public DateTime Date { get; }
		public string Level { get; }
		public string Section { get; }
		public string Node { get; }
		public string Message { get; }


		private static readonly Regex ConsoleLineParser =
			new Regex(@"\[(?<date>.*?)\]\[(?<level>.*?)\]\[(?<section>.*?)\] \[(?<node>.*?)\] (?<message>.+)");

		public ElasticsearchMessage(string consoleLine)
		{
			Console.WriteLine(consoleLine);
			if (string.IsNullOrEmpty(consoleLine)) return;
			var match = ConsoleLineParser.Match(consoleLine);
			if (!match.Success) return;
			var dateString = match.Groups["date"].Value.Trim();
			Date = DateTime.ParseExact(dateString, "yyyy-MM-dd HH:mm:ss,fff", CultureInfo.CurrentCulture);
			Level = match.Groups["level"].Value.Trim();
			Section = match.Groups["section"].Value.Trim();
			Node = match.Groups["node"].Value.Trim();
			Message = match.Groups["message"].Value.Trim();
		}
		
		private static readonly Regex InfoParser = 
			new Regex(@"version\[(?<version>.*)\], pid\[(?<pid>.*)\], build\[(?<build>.+)\]");

		public bool TryParseNodeInfo(out ElasticsearchNodeInfo nodeInfo)
		{
			nodeInfo = null;
			if (this.Section != "node") return false;

			var match = InfoParser.Match(this.Message);
			if (!match.Success) return false;

			var version = match.Groups["version"].Value.Trim();
			var pid = match.Groups["pid"].Value.Trim();
			var build = match.Groups["build"].Value.Trim();
			nodeInfo = new ElasticsearchNodeInfo(version, pid, build);
			return true;


		}

		public bool TryGetStartedConfirmation()
		{
			if (this.Section != "node") return false;
			return this.Message == "started";
		}

		private static readonly Regex PortParser = 
			new Regex(@"{inet\[.+\:(?<port>\d+)\]");

		public bool TryGetPortNumber(out int port)
		{
			port = 0;
			if (this.Section != "http") return false;

			var match = PortParser.Match(this.Message);
			if (!match.Success) return false;

			var portString = match.Groups["port"].Value.Trim();
			port = int.Parse(portString);
			return true;
		}
	}

	public class ElasticsearchNodeInfo
	{
		public string Version { get;  }
		public int Pid { get;  }
		public string Build { get;  }

		public ElasticsearchNodeInfo(string version, string pid, string build)
		{
			this.Version = version;
			Pid = int.Parse(pid);
			Build = build;
		}

	}

	public static class RxProcessUtilities
	{
		public static IObservable<ElasticsearchMessage> CreateStandardErrorObservable(this Process process)
		{
			var receivedStdErr =
				Observable.FromEventPattern<DataReceivedEventHandler, DataReceivedEventArgs>
					(h => process.ErrorDataReceived += h, h => process.ErrorDataReceived -= h)
				.Select(e => new ElasticsearchMessage(e.EventArgs.Data));

			return Observable.Create<ElasticsearchMessage>(observer =>
			{
				var cancel = Disposable.Create(process.CancelErrorRead);
				return new CompositeDisposable(cancel, receivedStdErr.Subscribe(observer));
			});
		}

		public static IObservable<ElasticsearchMessage> CreateStandardOutputObservable(this Process process)
		{
			var receivedStdOut =
				Observable.FromEventPattern<DataReceivedEventHandler, DataReceivedEventArgs>
					(h => process.OutputDataReceived += h, h => process.OutputDataReceived -= h)
				.Select(e => new ElasticsearchMessage(e.EventArgs.Data));

			return Observable.Create<ElasticsearchMessage>(observer =>
			{
				var cancel = Disposable.Create(process.CancelOutputRead);
				return new CompositeDisposable(cancel, receivedStdOut.Subscribe(observer));
			});
		}
	}
}
