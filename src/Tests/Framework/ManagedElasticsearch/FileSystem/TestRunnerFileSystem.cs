using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Tests.Framework.Versions;
using Tests.XPack.Security;

namespace Tests.Framework.Integration
{
	public class TestRunnerFileSystem : INodeFileSystem
	{
		private static readonly object Lock = new object();

		private readonly string _name;
		private readonly NodeConfiguration _config;
		private ElasticsearchVersion Version => _config.ElasticsearchVersion;

		private string TypeOfCluster => _name.ToLowerInvariant();
		public string ClusterName { get; }
		public string NodeName { get; }

		private string RoamingFolder { get; }

		public string ElasticsearchHome { get; }
		public string Binary => Path.Combine(this.ElasticsearchHome, "bin", "elasticsearch") + ".bat";
		public string PluginBinary =>
			Path.Combine(this.ElasticsearchHome, "bin", (this.Version.Major >= 5 ? "elasticsearch-" : "" ) +"plugin") + ".bat";
		public string ConfigPath => Path.Combine(ElasticsearchHome, "config");
		public string DataPath => Path.Combine(ElasticsearchHome, "data");
		public string LogsPath => Path.Combine(ElasticsearchHome, "logs");
		public string RepositoryPath => Path.Combine(RoamingFolder, "repositories");

		private string AnalysisFolder => Path.Combine(this.ConfigPath, "analysis");
		private string DownloadZipLocation => Path.Combine(this.RoamingFolder, this.Version.Zip);

		public TestRunnerFileSystem(NodeConfiguration config)
		{
			this._config = config;
			this._name = config.TypeOfCluster;

			var prefix = this._name.ToLowerInvariant();
			var suffix = Guid.NewGuid().ToString("N").Substring(0, 6);
			this.ClusterName = $"{prefix}-cluster-{suffix}";
			this.NodeName = $"{prefix}-node-{suffix}";

			var appData = GetApplicationDataDirectory();
			this.RoamingFolder = Path.Combine(appData, "NEST", this.Version.FullyQualifiedVersion);
			this.ElasticsearchHome = Path.Combine(this.RoamingFolder, this.Version.FolderInZip);

			if (config.RunIntegrationTests)
				this.DownloadAndExtractElasticsearch();
		}

		private void DownloadAndExtractElasticsearch()
		{
			lock (TestRunnerFileSystem.Lock)
			{
				if (!Directory.Exists(this.RoamingFolder))
					Directory.CreateDirectory(this.RoamingFolder);

				EnsureJavaHome();
				DownloadDistributionZip();
				UnzipDistribution();
				CreateHelperBatFile();

				if (this.Version.IsSnapshot) return;

				InstallPlugins();

				if (!Directory.Exists(this.AnalysisFolder))
					Directory.CreateDirectory(this.AnalysisFolder);

				SetupHunspellFiles();
				SetupCompoundTokenFilterFopFile();
				SetupCustomStemming();
				SetupStopwordsFile();

				EnsureShieldAdmin();
				EnsureWatcherActionConfigurations();
			}
		}

		private static void WriteFileIfNotExist(string fileLocation, string contents)
		{
			if (!File.Exists(fileLocation)) File.WriteAllText(fileLocation, contents);
		}

		private void SetupStopwordsFile() =>
			WriteFileIfNotExist(Path.Combine(this.AnalysisFolder, "stopwords") + ".txt", "");

		private void SetupCustomStemming() =>
			WriteFileIfNotExist(Path.Combine(this.AnalysisFolder, "custom_stems") + ".txt", "");

		private void SetupCompoundTokenFilterFopFile() =>
			WriteFileIfNotExist(Path.Combine(this.AnalysisFolder, "fop") + ".xml", "<languages-info />");

		private void CreateHelperBatFile() =>
			WriteFileIfNotExist(Path.Combine(this.ElasticsearchHome, "run.bat"), @"bin\elasticsearch.bat");

		private void SetupHunspellFiles()
		{
			var hunspellFolder = Path.Combine(this.ConfigPath, "hunspell", "en_US");
			var hunspellPrefix = Path.Combine(hunspellFolder, "en_US");
			if (File.Exists(hunspellPrefix + ".dic")) return;
			Directory.CreateDirectory(hunspellFolder);
			File.WriteAllText(hunspellPrefix + ".dic", "1\r\nabcdegf");
			File.WriteAllText(hunspellPrefix + ".aff", "SET UTF8\r\nSFX P Y 1\r\nSFX P 0 s");
		}

		private void UnzipDistribution()
		{
			if (Directory.Exists(this.ElasticsearchHome)) return;
			Console.WriteLine($"Unzipping elasticsearch: {this.Version.Version} ...");
			ZipFile.ExtractToDirectory(DownloadZipLocation, this.RoamingFolder);
		}

		private void DownloadDistributionZip()
		{
			if (File.Exists(this.DownloadZipLocation)) return;
			Console.WriteLine($"Download elasticsearch: {this.Version.Version} from {this.Version.DownloadUrl}");
			new WebClient().DownloadFile(this.Version.DownloadUrl, DownloadZipLocation);
			Console.WriteLine($"Downloaded elasticsearch: {this.Version.Version}");
		}

		private void InstallPlugins()
		{
			foreach (var plugin in ElasticsearchPluginCollection.Supported.Where(plugin => plugin.IsValid(this.Version)))
			{
				var installParameter = plugin.InstallParamater(this.Version);
				var folder = plugin.FolderName;
				var pluginFolder = Path.Combine(this.ElasticsearchHome, "plugins", folder);

				if (!Directory.Exists(this.ElasticsearchHome)) continue;

				// assume plugin already installed
				if (Directory.Exists(pluginFolder)) continue;

				Console.WriteLine($"Installing elasticsearch plugin: {plugin.Moniker} ...");
				var timeout = TimeSpan.FromSeconds(120);
				var handle = new ManualResetEvent(false);
				Task.Run(() =>
				{
					using (var p = new ObservableProcess(this.PluginBinary, "install", installParameter))
					{
						var o = p.Start();
						Console.WriteLine($"Calling: {this.PluginBinary} install {installParameter}");
						o.Subscribe(c=>Console.WriteLine(c.Data),
							(e) =>
							{
								Console.WriteLine($"Failed installing elasticsearch plugin: {plugin.Moniker}");
								handle.Set();
								throw e;
							},
							() =>
							{
								Console.WriteLine($"Finished installing elasticsearch plugin: {plugin.Moniker} exit code: {p.ExitCode}");
								handle.Set();
							});
						if (!handle.WaitOne(timeout, true))
							throw new Exception($"Could not install {plugin.Moniker} within {timeout}");
					}
				});
				if (!handle.WaitOne(timeout, true))
					throw new Exception($"Could not install {plugin.Moniker} within {timeout}");
			}
		}

		public void BeforeStart(IEnumerable<string> settings)
		{
			var easyRunBat = Path.Combine(this.RoamingFolder, $"run-{this.TypeOfCluster}.bat");
			if (File.Exists(easyRunBat)) return;
			var badSettings = new[] {"node.name", "cluster.name"};
			var batSettings = string.Join(" ", settings.Where(s => !badSettings.Any(s.Contains)));
			File.WriteAllText(easyRunBat, $@"elasticsearch-{this.Version.Version}\bin\elasticsearch.bat {batSettings}");
		}

		private void EnsureWatcherActionConfigurations()
		{
			if (!this._config.XPackEnabled) return;

			var rolesConfig = Path.Combine(this.ElasticsearchHome, "config", "elasticsearch.yml");
			var lines = File.ReadAllLines(rolesConfig).ToList();
			var saveFile = false;

			// set up for Watcher HipChat action
			if (!lines.Any(line => line.StartsWith("xpack.notification.hipchat:")))
			{
				lines.AddRange(new[]
				{
					string.Empty,
					"xpack.notification.hipchat:",
					"  account:",
					"    notify-monitoring:",
					"      profile: user",
					"      user: watcher-user@example.com",
					"      auth_token: hipchat_auth_token",
					string.Empty
				});

				saveFile = true;
			}

			// set up for Watcher Slack action
			if (!lines.Any(line => line.StartsWith("xpack.notification.slack:")))
			{
				lines.AddRange(new[]
				{
					string.Empty,
					"xpack.notification.slack:",
					"  account:",
					"    monitoring:",
					"      url: https://hooks.slack.com/services/foo/bar/baz",
					string.Empty
				});

				saveFile = true;
			}

			// set up for Watcher PagerDuty action
			if (!lines.Any(line => line.StartsWith("xpack.notification.pagerduty:")))
			{
				lines.AddRange(new[]
				{
					string.Empty,
					"xpack.notification.pagerduty:",
					"  account:",
					"    my_pagerduty_account:",
					"      service_api_key: pager_duty_service_api_key",
					string.Empty
				});

				saveFile = true;
			}

			if (saveFile) File.WriteAllLines(rolesConfig, lines);
		}

		private void EnsureJavaHome()
		{
#if DOTNETCORE
			var javaHome = Environment.GetEnvironmentVariable("JAVA_HOME");
#else
			var javaHome = Environment.GetEnvironmentVariable("JAVA_HOME", EnvironmentVariableTarget.Machine)
				?? Environment.GetEnvironmentVariable("JAVA_HOME", EnvironmentVariableTarget.User);
#endif
			if (string.IsNullOrWhiteSpace(javaHome))
				throw new Exception("The elasticsearch bat files are resillient to JAVA_HOME not being set, however the shield tooling is not");
		}

		private void EnsureShieldAdmin()
		{
			if (!this._config.XPackEnabled) return;


			var folder = this.Version.Major >= 5 ? "x-pack" : "shield";
			var plugin = this.Version.Major >= 5 ? "users" : "esusers";

			EnsureRoles(folder);
			var timeout = TimeSpan.FromMinutes(1);

			var pluginBat = Path.Combine(this.ElasticsearchHome, "bin", folder, plugin) + ".bat";
			foreach (var cred in ShieldInformation.AllUsers)
			{
				var handle = new ManualResetEvent(false);
				Task.Run(() =>
				{
					using (var p = new ObservableProcess(pluginBat, $"useradd {cred.Username} -p {cred.Password} -r {cred.Role}"))
					{
						var o = p.Start();
						Console.WriteLine($"Calling: {pluginBat} useradd {cred.Username}");
						o.Subscribe(
							//c=>Console.WriteLine(c.Data),
							c => { },
							(e) =>
							{
								handle.Set();
								throw e;
							},
							() =>
							{
								handle.Set();
							});
						if (!handle.WaitOne(timeout, true))
							throw new Exception($"Could not add user {cred.Username} within {timeout}");
					}
				});
				if (!handle.WaitOne(timeout, true))
					throw new Exception($"Could not add user {cred.Username} within {timeout}");
			}
		}

		private void EnsureRoles(string securityFolder)
		{
			var rolesConfig = Path.Combine(this.ElasticsearchHome, "config", securityFolder, "roles.yml");
			var lines = File.ReadAllLines(rolesConfig).ToList();
			var saveFile = false;

			if (!lines.Any(line => line.StartsWith("user:")))
			{
				lines.InsertRange(0, new []
				{
					"# Read-only operations on indices",
					"user:",
					"  indices:",
					"    - names: '*'",
					"      privileges:",
					"        - read",
					string.Empty
				});

				saveFile = true;
			}

			if (!lines.Any(line => line.StartsWith("power_user:")))
			{
				lines.InsertRange(0, new []
				{
					"# monitoring cluster privileges",
					"# All operations on all indices",
					"power_user:",
					"  cluster:",
					"    - monitor",
					"  indices:",
					"    - names: '*'",
					"      privileges:",
					"        - all",
					string.Empty
				});

				saveFile = true;
			}

			if (!lines.Any(line => line.StartsWith("admin:")))
			{
				lines.InsertRange(0, new []
				{
					"# All cluster rights",
					"# All operations on all indices",
					"admin:",
					"  cluster:",
					"    - all",
					"  indices:",
					"    - names: '*'",
					"      privileges:",
					"        - all",
					string.Empty
				});

				saveFile = true;
			}

			if (saveFile) File.WriteAllLines(rolesConfig, lines);
		}

		private string GetApplicationDataDirectory()
		{
#if DOTNETCORE
			return Environment.GetEnvironmentVariable("APPDATA");
#else
			return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
#endif
		}

		public void Dispose()
		{
			var dataFolder = Path.Combine(this.DataPath, this.ClusterName);
			if (Directory.Exists(dataFolder))
			{
				Console.WriteLine($"attempting to delete cluster data: {dataFolder}");
				Directory.Delete(dataFolder, true);
			}

			if (Directory.Exists(this.LogsPath))
			{
				var files = Directory.GetFiles(this.LogsPath, this.ClusterName + "*.log");
				foreach (var f in files)
				{
					Console.WriteLine($"attempting to delete log file: {f}");
					File.Delete(f);
				}
			}

			if (Directory.Exists(this.RepositoryPath))
			{
				Console.WriteLine("attempting to delete repositories");
				Directory.Delete(this.RepositoryPath, true);
			}
		}
	}
}
