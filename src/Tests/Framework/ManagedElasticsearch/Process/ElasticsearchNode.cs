using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Elasticsearch.Net;
using Nest;
using Tests.Framework.Versions;
#if !DOTNETCORE
using XplatManualResetEvent = System.Threading.ManualResetEvent;
#endif

namespace Tests.Framework.Integration
{
	public class ElasticsearchNode : IDisposable
	{
		private static TimeSpan HandleTimeout { get; } = TimeSpan.FromMinutes(1);
		private static TimeSpan ClusterHealthTimeout { get; } = TimeSpan.FromSeconds(20);

		private List<string> DefaultNodeSettings { get; }

		private ObservableProcess _process;
		private IDisposable _processListener;

		private readonly NodeConfiguration _config;

		public ElasticsearchVersion Version => _config.ElasticsearchVersion;
		public INodeFileSystem FileSystem { get; }

		public bool Started { get; private set; }

		public int Port { get; private set; }
		private int? ProcessId { get; set; }

		private readonly Subject<XplatManualResetEvent> _blockingSubject = new Subject<XplatManualResetEvent>();
		public IObservable<XplatManualResetEvent> BootstrapWork => _blockingSubject;

		public ElasticsearchNode(NodeConfiguration config, INodeFileSystem fileSystem)
		{
			this._config = config;
			this.FileSystem = fileSystem;

			var attr = this.Version.Major >= 5 ? "attr." : "";
			var indexedOrStored = this.Version > new ElasticsearchVersion("5.0.0-alpha1") ? "stored" : "indexed";
			var shieldOrSecurity = this.Version > new ElasticsearchVersion("5.0.0-alpha1") ? "security" : "shield";
			var es = this.Version > new ElasticsearchVersion("5.0.0-alpha2") ? "" : "es.";

			this.DefaultNodeSettings = new List<string>
			{
				$"{es}cluster.name={this.FileSystem.ClusterName}",
				$"{es}node.name={this.FileSystem.NodeName}",
				$"{es}path.repo={this.FileSystem.RepositoryPath}",
				$"{es}path.data={Path.Combine(this.FileSystem.DataPath, this.FileSystem.ClusterName)}",
				$"{es}script.inline=true",
				$"{es}script.max_compilations_per_minute=10000",
				$"{es}script.{indexedOrStored}=true",
				$"{es}node.{attr}testingcluster=true"
			};

			if (!this.Version.IsSnapshot)
				this.DefaultNodeSettings.Add($"{es}xpack.{shieldOrSecurity}.enabled={this._config.XPackEnabled.ToString().ToLowerInvariant()}");

			if (this._config.RunIntegrationTests && !this._config.TestAgainstAlreadyRunningElasticsearch) return;
			this.Port = 9200;
		}

		private object _lockGetClient = new object { };
		private IElasticClient _client;
		public IElasticClient Client
		{
			get
			{
				if (!this.Started && TestClient.Configuration.RunIntegrationTests)
					throw new Exception("can not request a client from an ElasticsearchNode if that node hasn't started yet");

				if (this._client != null) return this._client;

				lock (_lockGetClient)
				{
					if (this._client != null) return this._client;

					var port = this.Started ? this.Port : 9200;
					this._client = TestClient.GetClient(ComposeSettings, port);
					return this.Client;
				}
			}
		}

		public IObservable<ElasticsearchConsoleOut> Start(string[] additionalSettings = null)
		{
			if (!this._config.RunIntegrationTests) return Observable.Empty<ElasticsearchConsoleOut>();

			this.Stop();

			var settingMarker = this.Version.Major >= 5 ? "-E " : "-D";
			var settings = DefaultNodeSettings
				.Concat(additionalSettings ?? Enumerable.Empty<string>())
				.Select(s => $"{settingMarker}{s}")
				.ToList();

			this.FileSystem.BeforeStart(settings);

			var handle = new XplatManualResetEvent(false);

			var alreadyRunning = UseAlreadyRunningInstance(handle);
			if (alreadyRunning != null) return alreadyRunning;

			this._process = new ObservableProcess(this.FileSystem.Binary, settings.ToArray());

			var observable = Observable.Using(() => this._process, process => process.Start())
				.Select(c => new ElasticsearchConsoleOut(c.Error, c.Data));

			Console.WriteLine($"Starting: {_process.Binary} {_process.Arguments}");

			this._processListener = observable
				.Subscribe(s => this.HandleConsoleMessage(s, handle), (e) => this.Stop(),  () => { });

			if (!handle.WaitOne(HandleTimeout, true))
			{
				this.Stop();
				throw new Exception($"Could not start elasticsearch within {HandleTimeout}");
			}

			if (this.Exception != null) throw this.Exception;
			return observable;
		}

		private Exception Exception { get; set; }
		private void Fatal(XplatManualResetEvent handle, Exception exception = null)
		{
			if (exception != null) this.Exception = exception;
			handle?.Set();
			this.Stop();
			if (exception != null) this._blockingSubject.OnError(exception);
		}

		private readonly object _stopLock = new object();

		public void Stop()
		{
			lock (_stopLock)
			{
				var hasStarted = this.Started;
				this.Started = false;

				this._processListener?.Dispose();
				this._process?.Dispose();

				if (this.ProcessId != null)
				{
					var esProcess = Process.GetProcesses()
						.FirstOrDefault(p => p.Id == this.ProcessId.Value);
					if (esProcess != null)
					{
						Console.WriteLine($"Killing elasticsearch PID {this.ProcessId}");
						esProcess.Kill();
						esProcess.WaitForExit(5000);
						esProcess.Close();
					}
				}

				this.FileSystem?.Dispose();

				if (!this._config.RunIntegrationTests || !hasStarted) return;
				Console.WriteLine($"Stopping... node has started and ran integrations: {this._config.RunIntegrationTests}");
				Console.WriteLine($"Node started on port: {this.Port} using PID: {this.ProcessId}");
			}
		}

		private IObservable<ElasticsearchConsoleOut> UseAlreadyRunningInstance(XplatManualResetEvent handle)
		{
			if (!this._config.TestAgainstAlreadyRunningElasticsearch)
				return null;

			var client = this.GetPrivateClient(null, false, this.Port);
			if (!this.ValidateRunningVersion(client, handle))
				return null;

			this.ValidatePlugins(client, handle);

			this.Port = 9200;
			this.WaitForClusterBootstrap(client, handle, alreadyRunningInstance: true);

			this.ValidateLicense(client, handle);
			return Observable.Empty<ElasticsearchConsoleOut>();
		}

		private bool ValidateRunningVersion(IElasticClient client, XplatManualResetEvent handle)
		{
			var alreadyUp = client.RootNodeInfo();
			if (!alreadyUp.IsValid) return false;

			var alreadyUpVersion = new ElasticsearchVersion(alreadyUp.Version.Number);
			var alreadyUpSnapshotVersion = new ElasticsearchVersion(alreadyUp.Version.Number + "-SNAPSHOT");
			if (this.Version == alreadyUpVersion ||this.Version == alreadyUpSnapshotVersion)
				return true;
			var e = new Exception($"running elasticsearch is version {alreadyUpVersion} but the test config dictates {this.Version}");
			this.Fatal(handle, e);
			return false;
		}

		private void ValidatePlugins(IElasticClient client, XplatManualResetEvent handle)
		{
			//if the version we are running against is a s snapshot version we do not validate plugins
			//because we can not reliably install plugins against snapshots
			if (this.Version.IsSnapshot) return;

			var requiredMonikers = ElasticsearchPluginCollection.Supported
				.Where(plugin => plugin.IsValid(this.Version) && this._config.RequiredPlugins.Contains(plugin.Plugin))
				.Select(plugin => plugin.Moniker)
				.ToList();

			if (!requiredMonikers.Any()) return;
			var checkPlugins = client.CatPlugins();

			if (!checkPlugins.IsValid)
			{
				this.Fatal(handle, new Exception($"Failed to check plugins: {checkPlugins.DebugInformation}."));
				return;
			}

			var missingPlugins = requiredMonikers
				.Except((checkPlugins.Records ?? Enumerable.Empty<CatPluginsRecord>()).Select(r => r.Component))
				.ToList();
			if (!missingPlugins.Any()) return;

			var e = new Exception($"Already running elasticsearch missed the following plugin(s): {string.Join(", ", missingPlugins)}.");
			this.Fatal(handle, e);
		}

		private void ValidateLicense(IElasticClient client, XplatManualResetEvent handle)
		{
			if (!this._config.XPackEnabled) return;
			var license = client.GetLicense();
			if (license.IsValid && license.License.Status == LicenseStatus.Active) return;

			var exceptionMessageStart = "Server has license plugin installed, ";
#if DOTNETCORE
			//TODO Why is this here hardcoded ?
			var licensePath = @"C:\license.json";
			var licenseFile = File.Exists(licensePath) ? licensePath : string.Empty;
#else
			var licenseFile = Environment.GetEnvironmentVariable("ES_LICENSE_FILE", EnvironmentVariableTarget.Machine);
#endif
			if (!string.IsNullOrWhiteSpace(licenseFile))
			{
				var putLicense = client.PostLicense(new PostLicenseRequest { License = License.LoadFromDisk(licenseFile) });
				if (!putLicense.IsValid)
				{
					this.Fatal(handle, new Exception("Server has invalid license and the ES_LICENSE_FILE failed to register\r\n" + putLicense.DebugInformation));
					return;
				}

				license = client.GetLicense();
				if (license.IsValid && license.License.Status == LicenseStatus.Active) return;
				exceptionMessageStart += " but the installed license is invalid and we attempted to register ES_LICENSE_FILE ";
			}

			Exception exception = null;

			if (!license.IsValid)
			{
				exception = license.ApiCall.HttpStatusCode == 404
					? new Exception($"{exceptionMessageStart} but the license was not found!")
					: new Exception($"{exceptionMessageStart} but a {license.ApiCall.HttpStatusCode} was returned!");
			}
			else if (license.License == null)
				exception = new Exception($"{exceptionMessageStart}  but the license was deleted!");

			else if (license.License.Status == LicenseStatus.Expired)
				exception = new Exception($"{exceptionMessageStart} but the license has expired!");

			else if (license.License.Status == LicenseStatus.Invalid)
				exception = new Exception($"{exceptionMessageStart} but the license is invalid!");

			if (exception != null)
				this.Fatal(handle, exception);
		}

		private void WaitForClusterBootstrap(IElasticClient client, XplatManualResetEvent handle, bool alreadyRunningInstance)
		{
			var healthyCluster = client.ClusterHealth(g => g.WaitForStatus(WaitForStatus.Yellow).Timeout(ClusterHealthTimeout));
			if (healthyCluster.IsValid)
			{
				this.Started = true;
				this._blockingSubject.OnNext(handle);
				if (alreadyRunningInstance && !handle.WaitOne(HandleTimeout, true))
					throw new Exception($"Could not launch tests on already running elasticsearch within {HandleTimeout}");
			}
			else
				this.Fatal(handle, new Exception("Did not see a healthy cluster after the node started for 30 seconds"));
		}

		private void HandleConsoleMessage(ElasticsearchConsoleOut consoleOut, XplatManualResetEvent handle)
		{
			if (consoleOut.Error && !this.Started)
			{
				this.Fatal(handle, new Exception(consoleOut.Data));
				return;
			}

			//no need to snoop for metadata if we already started
			if (!this._config.RunIntegrationTests || this.Started) return;

			string version; int? pid; int port;

			if (this.ProcessId == null && consoleOut.TryParseNodeInfo(out version, out pid))
			{
				var startedVersion = new ElasticsearchVersion(version);
				this.ProcessId = pid;
				if (this.Version != startedVersion)
					this.Fatal(handle, new Exception($"Booted elasticsearch is version {startedVersion} but the test config dictates {this.Version}"));
			}
			else if (consoleOut.TryGetPortNumber(out port))
				this.Port = port;
			else if (consoleOut.TryGetStartedConfirmation())
			{
				var client = this.GetPrivateClient(null, false, this.Port);
				this.ValidatePlugins(client, handle);
				this.ValidateLicense(client, handle);
				this.WaitForClusterBootstrap(client, handle, alreadyRunningInstance: false);
			}
		}

		private ConnectionSettings ClusterSettings(ConnectionSettings s, Func<ConnectionSettings, ConnectionSettings> settings) =>
			AddBasicAuthentication(AppendClusterNameToHttpHeaders(settings(s)));

		private IElasticClient GetPrivateClient(Func<ConnectionSettings, ConnectionSettings> settings, bool forceInMemory, int port)
		{
			settings = settings ?? (s => s);
			var client = forceInMemory
				? TestClient.GetInMemoryClient(s => ClusterSettings(s, settings), port)
				: TestClient.GetClient(s => ClusterSettings(s, settings), port);
			return client;
		}

		private ConnectionSettings ComposeSettings(ConnectionSettings s) => AddBasicAuthentication(AppendClusterNameToHttpHeaders(s));

		private ConnectionSettings AddBasicAuthentication(ConnectionSettings settings) =>
			!this._config.XPackEnabled ? settings : settings.BasicAuthentication("es_admin", "es_admin");

		private ConnectionSettings AppendClusterNameToHttpHeaders(ConnectionSettings settings)
		{
			IConnectionConfigurationValues values = settings;
			var headers = values.Headers ?? new NameValueCollection();
			headers.Add("ClusterName", this.FileSystem.ClusterName);
			return settings;
		}

		public void Dispose() => this.Stop();

	}
}
