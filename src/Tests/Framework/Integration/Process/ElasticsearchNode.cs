using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using System.Xml.Linq;

namespace Tests.Framework.Integration
{
	public class ElasticsearchNode : IDisposable
	{
		private static readonly object _lock = new object();
		// <installpath> <> <plugin folder prefix>
		private readonly Dictionary<string, Func<string, string>> SupportedPlugins = new Dictionary<string, Func<string, string>>
		{
			{ "delete-by-query", _ => "delete-by-query" },
			//{ "cloud-azure", _ => "cloud-azure" },
			{ "mapper-attachments", _ => "mapper-attachments" },
			{ "mapper-murmur3", _ => "mapper-murmur3" }
		};

		private readonly bool _doNotSpawnIfAlreadyRunning;
		private ObservableProcess _process;
		private IDisposable _processListener;

		public ElasticsearchVersionInfo VersionInfo { get; }
		public string Binary { get; }

		private string RoamingFolder { get; }
		private string RoamingClusterFolder { get; }

		public bool Started { get; private set; }
		public bool RunningIntegrations { get; private set; }
		public string Prefix { get; set; }
		public string ClusterName { get; }
		public string NodeName { get; }
		public string RepositoryPath { get; private set; }
		public ElasticsearchNodeInfo Info { get; private set; }
		public int Port { get; private set; }

#if DOTNETCORE
		// Investigate  problem with ManualResetEvent on CoreClr
		// Maybe due to .WaitOne() not taking exitContext?
		public class Signal
		{
			private readonly object _lock = new object();
			private bool _notified;

			public Signal(bool initialState)
			{
				_notified = initialState;
			}

			public void Set()
			{
				lock (_lock)
				{
					if (!_notified)
					{
						_notified = true;
						Monitor.Pulse(_lock);
					}
				}
			}

			public bool WaitOne(TimeSpan timeout, bool exitContext)
			{
				lock (_lock)
				{
					bool exit = true;
					if (!_notified)
						exit = Monitor.Wait(_lock, timeout);
					return exit;
				}
			}
		}

		private readonly Subject<Signal> _blockingSubject = new Subject<Signal>();
		public IObservable<Signal> BootstrapWork { get; }
#else
		private readonly Subject<ManualResetEvent> _blockingSubject = new Subject<ManualResetEvent>();
		public IObservable<ManualResetEvent> BootstrapWork { get; }
#endif

		public ElasticsearchNode(
			string elasticsearchVersion,
			bool runningIntegrations,
			bool doNotSpawnIfAlreadyRunning,
			string prefix
			)
		{
			_doNotSpawnIfAlreadyRunning = doNotSpawnIfAlreadyRunning;
			this.VersionInfo = new ElasticsearchVersionInfo(runningIntegrations ? elasticsearchVersion : "0.0.0-unittest");
			this.RunningIntegrations = runningIntegrations;
			this.Prefix = prefix.ToLowerInvariant();
			var suffix = Guid.NewGuid().ToString("N").Substring(0, 6);
			this.ClusterName = $"{this.Prefix}-cluster-{suffix}";
			this.NodeName = $"{this.Prefix}-node-{suffix}";
			this.BootstrapWork = _blockingSubject;
			var appData = GetApplicationDataDirectory();
			this.RoamingFolder = Path.Combine(appData, "NEST", this.VersionInfo.Version + (this.VersionInfo.IsSnapshot ? $"-{VersionInfo.SnapshotIdentifier}" : string.Empty));
			this.RoamingClusterFolder = Path.Combine(this.RoamingFolder, "elasticsearch-" + this.VersionInfo.Version);
            this.RepositoryPath = Path.Combine(RoamingFolder, "repositories");
			this.Binary = Path.Combine(this.RoamingClusterFolder, "bin", "elasticsearch") + ".bat";

			if (!runningIntegrations)
			{
				this.Port = 9200;
				return;
			}

			Console.WriteLine("========> {0}", this.RoamingFolder);
			this.DownloadAndExtractElasticsearch();
		}

		private string GetApplicationDataDirectory()
		{
#if DOTNETCORE
			return Environment.GetEnvironmentVariable("APPDATA");
#else
			return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
#endif
		}

		public IObservable<ElasticsearchMessage> Start(string[] additionalSettings = null)
		{
			if (!this.RunningIntegrations) return Observable.Empty<ElasticsearchMessage>();

			this.Stop();
			var timeout = TimeSpan.FromMinutes(1);

#if DOTNETCORE
			var handle = new Signal(false);
#else
			var handle = new ManualResetEvent(false);
#endif

			if (_doNotSpawnIfAlreadyRunning)
			{
				var client = TestClient.GetClient();
				var alreadyUp = client.RootNodeInfo();
				if (alreadyUp.IsValid)
				{
					var checkPlugins = client.CatPlugins();

					if (checkPlugins.IsValid)
					{
						foreach (var supportedPlugin in SupportedPlugins)
						{
							if (!checkPlugins.Records.Any(r => r.Component.Equals(supportedPlugin.Key)))
								throw new Exception($"Already running elasticsearch does not have supported plugin {supportedPlugin.Key} installed.");
						}

						this.Started = true;
						this.Port = 9200;
						this.Info = new ElasticsearchNodeInfo(alreadyUp.Version.Number, null, alreadyUp.Version.LuceneVersion);
						this._blockingSubject.OnNext(handle);
						if (!handle.WaitOne(timeout, true))
							throw new Exception($"Could not launch tests on already running elasticsearch within {timeout}");

						return Observable.Empty<ElasticsearchMessage>();
					}
				}
			}

			var settingMarker = this.VersionInfo.ParsedVersion.Major >= 5 ? "-E " : "-D";

			var settings = new []
			{
				$"es.cluster.name=\"{this.ClusterName}\"",
				$"es.node.name={this.NodeName}",
				$"es.path.repo=\"{this.RepositoryPath}\"",
				$"es.script.inline=true",
				$"es.script.indexed=true",
				$"es.node.attr.testingcluster=true"
			}.Concat(additionalSettings ?? Enumerable.Empty<string>())
			 .Select(s=> $"{settingMarker}{s}");

			this._process = new ObservableProcess(this.Binary, settings.ToArray());

			var observable = Observable.Using(() => this._process, process => process.Start())
				.Select(consoleLine => new ElasticsearchMessage(consoleLine));
			this._processListener = observable.Subscribe(onNext: s => HandleConsoleMessage(s, handle));

			if (!handle.WaitOne(timeout, true))
			{
				this.Stop();
				throw new Exception($"Could not start elasticsearch within {timeout}");
			}

			return observable;
		}

#if DOTNETCORE
		private void HandleConsoleMessage(ElasticsearchMessage s, Signal handle)
#else
		private void HandleConsoleMessage(ElasticsearchMessage s, ManualResetEvent handle)
#endif
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
				var healthyCluster = this.Client().ClusterHealth(g => g.WaitForStatus(WaitForStatus.Yellow).Timeout(TimeSpan.FromSeconds(30)));
				if (healthyCluster.IsValid)
				{
					this._blockingSubject.OnNext(handle);
					this.Started = true;
				}
				else
				{
					this._blockingSubject.OnError(new Exception("Did not see a healthy cluster after the node started for 30 seconds"));
					handle.Set();
					this.Stop();
				}

			}
			else if (s.TryGetPortNumber(out port))
			{
				this.Port = port;
			}
		}

		private void DownloadAndExtractElasticsearch()
		{
			lock (_lock)
			{
				var localZip = Path.Combine(this.RoamingFolder, this.VersionInfo.Zip);

				Directory.CreateDirectory(this.RoamingFolder);
				if (!File.Exists(localZip))
				{
					Console.WriteLine($"Download elasticsearch: {this.VersionInfo.Version} from {this.VersionInfo.DownloadUrl}");
					new WebClient().DownloadFile(this.VersionInfo.DownloadUrl, localZip);
					Console.WriteLine($"Downloaded elasticsearch: {this.VersionInfo.Version}");
				}

				if (!Directory.Exists(this.RoamingClusterFolder))
				{
					Console.WriteLine($"Unzipping elasticsearch: {this.VersionInfo.Version} ...");
					ZipFile.ExtractToDirectory(localZip, this.RoamingFolder);
				}

				InstallPlugins();

				//hunspell config
				var hunspellFolder = Path.Combine(this.RoamingClusterFolder, "config", "hunspell", "en_US");
				var hunspellPrefix = Path.Combine(hunspellFolder, "en_US");
				if (!File.Exists(hunspellPrefix + ".dic"))
				{
					Directory.CreateDirectory(hunspellFolder);
					//File.Create(hunspellPrefix + ".dic");
					File.WriteAllText(hunspellPrefix + ".dic", "1\r\nabcdegf");
					//File.Create(hunspellPrefix + ".aff");
					File.WriteAllText(hunspellPrefix + ".aff", "SET UTF8\r\nSFX P Y 1\r\nSFX P 0 s");
				}

				var analysFolder = Path.Combine(this.RoamingClusterFolder, "config", "analysis");
				if (!Directory.Exists(analysFolder)) Directory.CreateDirectory(analysFolder);
				var fopXml = Path.Combine(analysFolder, "fop") + ".xml";
				if (!File.Exists(fopXml)) File.WriteAllText(fopXml, "<languages-info />");
				var customStems = Path.Combine(analysFolder, "custom_stems") + ".txt";
				if (!File.Exists(customStems)) File.WriteAllText(customStems, "");
				var stopwords = Path.Combine(analysFolder, "stopwords") + ".txt";
				if (!File.Exists(stopwords)) File.WriteAllText(stopwords, "");
			}
		}

		private void InstallPlugins()
		{
			var pluginCommand = "plugin";
			if (this.VersionInfo.ParsedVersion.Major >= 5) pluginCommand = "elasticsearch-plugin";

			var pluginBat = Path.Combine(this.RoamingClusterFolder, "bin", pluginCommand) + ".bat";
			foreach (var plugin in SupportedPlugins)
			{
				var installPath = plugin.Key;
				var command = plugin.Value(this.VersionInfo.Version);
				var pluginFolder = Path.Combine(this.RoamingClusterFolder, "plugins", installPath);

				if (!Directory.Exists(this.RoamingClusterFolder)) continue;

				// assume plugin already installed
				if (Directory.Exists(pluginFolder)) continue;

				Console.WriteLine($"Installing elasticsearch plugin: {installPath} ...");
				var timeout = TimeSpan.FromSeconds(120);
				var handle = new ManualResetEvent(false);
				Task.Run(() =>
				{
					using (var p = new ObservableProcess(pluginBat, "install", command))
					{
						var o = p.Start();
						Console.WriteLine($"Calling: {pluginBat} install {command}");
						o.Subscribe(e => Console.WriteLine(e),
							(e) =>
							{
								Console.WriteLine($"Failed installing elasticsearch plugin: {command}");
								handle.Set();
								throw e;
							},
							() =>
							{
								Console.WriteLine($"Finished installing elasticsearch plugin: {installPath} exit code: {p.ExitCode}");
								handle.Set();
							});
						if (!handle.WaitOne(timeout, true))
							throw new Exception($"Could not install {command} within {timeout}");
					}
				});
				if (!handle.WaitOne(timeout, true))
					throw new Exception($"Could not install {command} within {timeout}");
			}
		}

		public IElasticClient Client(Func<Uri, IConnectionPool> createPool, Func<ConnectionSettings, ConnectionSettings> settings)
		{
			var port = this.Started ? this.Port : 9200;
			settings = settings ?? (s => s);
			var client = TestClient.GetClient(s => AppendClusterNameToHttpHeaders(settings(s)), port, createPool);
			return client;
		}

		public IElasticClient Client(Func<ConnectionSettings, ConnectionSettings> settings = null)
		{
			var port = this.Started ? this.Port : 9200;
			settings = settings ?? (s => s);
			var client = TestClient.GetClient(s => AppendClusterNameToHttpHeaders(settings(s)), port);
			return client;
		}

		private ConnectionSettings AppendClusterNameToHttpHeaders(ConnectionSettings settings)
		{
			IConnectionConfigurationValues values = settings;
			var headers = values.Headers ?? new NameValueCollection();
			headers.Add("ClusterName", this.ClusterName);
			return settings;
		}

		public void Stop()
		{
			if (!this.RunningIntegrations || !this.Started) return;

			this.Started = false;

			Console.WriteLine($"Stopping... ran integrations: {this.RunningIntegrations}");
			Console.WriteLine($"Node started: {this.Started} on port: {this.Port} using PID: {this.Info?.Pid}");

			this._process?.Dispose();
			this._processListener?.Dispose();

			if (this.Info?.Pid != null)
			{
				var esProcess = Process.GetProcessById(this.Info.Pid.Value);
				Console.WriteLine($"Killing elasticsearch PID {this.Info.Pid}");
				esProcess.Kill();
				esProcess.WaitForExit(5000);
				esProcess.Close();
			}

			if (this._doNotSpawnIfAlreadyRunning) return;
			var dataFolder = Path.Combine(this.RoamingClusterFolder, "data", this.ClusterName);
			if (Directory.Exists(dataFolder))
			{
				Console.WriteLine($"attempting to delete cluster data: {dataFolder}");
				Directory.Delete(dataFolder, true);
			}
			//var logPath = Path.Combine(this.RoamingClusterFolder, "logs");
			//var files = Directory.GetFiles(logPath, this.ClusterName + "*.log");
			//foreach (var f in files)
			//{
			//	Console.WriteLine($"attempting to delete log file: {f}");
			//	File.Delete(f);
			//}
			if (Directory.Exists(this.RepositoryPath))
			{
				Console.WriteLine("attempting to delete repositories");
				Directory.Delete(this.RepositoryPath, true);
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
			Section = match.Groups["section"].Value.Trim().Replace("org.elasticsearch.", "");
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
			new Regex(@"bound_address(es)? {.+\:(?<port>\d+)}");

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
		public string Version { get; }
		public int? Pid { get; }
		public string Build { get; }

		public ElasticsearchNodeInfo(string version, string pid, string build)
		{
			this.Version = version;
			if (!string.IsNullOrEmpty(pid))
				Pid = int.Parse(pid);
			Build = build;
		}

	}

}
