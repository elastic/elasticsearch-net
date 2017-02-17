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
using Tests.Framework.Integration;
using Tests.Framework.Versions;
#if !DOTNETCORE
using XplatManualResetEvent = System.Threading.ManualResetEvent;

#endif

namespace Tests.Framework.ManagedElasticsearch
{
	public class ElasticsearchNode : IDisposable
	{
		private static TimeSpan HandleTimeout { get; } = TimeSpan.FromMinutes(1);
		private static TimeSpan ClusterHealthTimeout { get; } = TimeSpan.FromSeconds(20);

		private ObservableProcess _process;
		private IDisposable _processListener;

		private readonly NodeConfiguration _config;

		public ElasticsearchVersion Version => _config.ElasticsearchVersion;
		public INodeFileSystem FileSystem { get; }

		public bool Started { get; private set; }

		public int Port { get; private set; } = 9200;
		private int? ProcessId { get; set; }

		public ElasticsearchNode(NodeConfiguration config)
		{
			this._config = config;
			this.FileSystem = config.FileSystem;

			if (this._config.RunIntegrationTests && !this._config.TestAgainstAlreadyRunningElasticsearch) return;
		}

		private readonly object _lockGetClient = new object { };
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

		public void Start(string[] settings, Action onNodeStarted)
		{
			if (!this._config.RunIntegrationTests) return;
			this.Stop();

			var client = this.GetPrivateClient(null, false, this.Port);
			if (UseAlreadyRunningInstance(client, onNodeStarted))
				return;

			var handle = new XplatManualResetEvent(false);
			var booted = false;
			this._process = new ObservableProcess(this.FileSystem.Binary, settings);
			Console.WriteLine($"Starting: {_process.Binary} {_process.Arguments}");
			try
			{
				var observable = Observable.Using(() => this._process, process => process.Start())
					.Select(c => new ElasticsearchConsoleOut(c.Error, c.Data));

				this._processListener = observable
					.Subscribe(s => this.HandleConsoleMessage(s, handle), e => { throw e; }, () => handle.Set());

				if (!handle.WaitOne(HandleTimeout, true))
					throw new Exception($"Could not start elasticsearch within {HandleTimeout}");

				this.ValidateCluster(client);
				onNodeStarted?.Invoke();
				booted = true;
			}
			finally
			{
				if (!booted) this.Stop();
			}
		}

		private void ValidateCluster(IElasticClient client)
		{
			this.ValidatePlugins(client);
			this.ValidateLicense(client);
			var healthyCluster = client.ClusterHealth(g => g.WaitForStatus(WaitForStatus.Yellow).Timeout(ClusterHealthTimeout));
			if(!healthyCluster.IsValid)
				throw new Exception($"Did not see a healhty cluster before calling onNodeStarted handler." + healthyCluster.DebugInformation);
		}

		private bool UseAlreadyRunningInstance(IElasticClient client, Action onNodeStarted)
		{
			if (!this._config.TestAgainstAlreadyRunningElasticsearch) return false;
			if (!this.ValidateRunningVersion(client)) return false;

			this.ValidateCluster(client);
			onNodeStarted?.Invoke();
			return true;
		}

		private bool ValidateRunningVersion(IElasticClient client)
		{
			var alreadyUp = client.RootNodeInfo();
			if (!alreadyUp.IsValid) return false;

			var alreadyUpVersion = new ElasticsearchVersion(alreadyUp.Version.Number);
			var alreadyUpSnapshotVersion = new ElasticsearchVersion(alreadyUp.Version.Number + "-SNAPSHOT");
			if (this.Version == alreadyUpVersion || this.Version == alreadyUpSnapshotVersion)
				return true;
			throw new Exception($"running elasticsearch is version {alreadyUpVersion} but the test config dictates {this.Version}");
		}

		private void ValidatePlugins(IElasticClient client)
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
				throw new Exception($"Failed to check plugins: {checkPlugins.DebugInformation}.");

			var missingPlugins = requiredMonikers
				.Except((checkPlugins.Records ?? Enumerable.Empty<CatPluginsRecord>()).Select(r => r.Component))
				.ToList();
			if (!missingPlugins.Any()) return;

			var pluginsString = string.Join(", ", missingPlugins);
			throw new Exception($"Already running elasticsearch missed the following plugin(s): {pluginsString}.");
		}

		private void ValidateLicense(IElasticClient client)
		{
			if (!this._config.XPackEnabled) return;
			var license = client.GetLicense();
			if (license.IsValid && license.License.Status == LicenseStatus.Active) return;

			var exceptionMessageStart = "Server has license plugin installed, ";
#if DOTNETCORE
			var licenseFile = Environment.GetEnvironmentVariable("ES_LICENSE_FILE");
#else
			var licenseFile = Environment.GetEnvironmentVariable("ES_LICENSE_FILE", EnvironmentVariableTarget.Machine);
#endif
			if (!string.IsNullOrWhiteSpace(licenseFile))
			{
				var putLicense = client.PostLicense(new PostLicenseRequest {License = License.LoadFromDisk(licenseFile)});
				if (!putLicense.IsValid)
					throw new Exception("Server has invalid license and the ES_LICENSE_FILE failed to register\r\n" + putLicense.DebugInformation);

				license = client.GetLicense();
				if (license.IsValid && license.License.Status == LicenseStatus.Active) return;
				exceptionMessageStart += " but the installed license is invalid and we attempted to register ES_LICENSE_FILE ";
			}

			Exception exception = null;
			if (!license.IsValid)
			{
				exception = license.ApiCall.HttpStatusCode == 404
					? new Exception($"{exceptionMessageStart} but the license was not found! Details: {license.DebugInformation}")
					: new Exception($"{exceptionMessageStart} but a {license.ApiCall.HttpStatusCode} was returned! Details: {license.DebugInformation}");
			}
			else if (license.License == null)
				exception = new Exception($"{exceptionMessageStart}  but the license was deleted!");

			else if (license.License.Status == LicenseStatus.Expired)
				exception = new Exception($"{exceptionMessageStart} but the license has expired!");

			else if (license.License.Status == LicenseStatus.Invalid)
				exception = new Exception($"{exceptionMessageStart} but the license is invalid!");

			if (exception != null)
				throw exception;
		}

		private void HandleConsoleMessage(ElasticsearchConsoleOut consoleOut, XplatManualResetEvent handle)
		{
			//no need to snoop for metadata if we already started
			if (!this._config.RunIntegrationTests || this.Started) return;

			if (consoleOut.Error && !this.Started) throw new Exception(consoleOut.Data);

			string version;
			int? pid;
			int port;

			if (this.ProcessId == null && consoleOut.TryParseNodeInfo(out version, out pid))
			{
				var startedVersion = new ElasticsearchVersion(version);
				this.ProcessId = pid;
				if (this.Version != startedVersion)
					throw new Exception($"Booted elasticsearch is version {startedVersion} but the test config dictates {this.Version}");
			}
			else if (consoleOut.TryGetPortNumber(out port))
				this.Port = port;
			else if (consoleOut.TryGetStartedConfirmation())
			{
				this.Started = true;
				handle.Set();
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
					var esProcess = Process.GetProcesses().FirstOrDefault(p => p.Id == this.ProcessId.Value);
					if (esProcess != null)
					{
						Console.WriteLine($"Killing elasticsearch PID {this.ProcessId}");
						esProcess.Kill();
						esProcess.WaitForExit(5000);
						esProcess.Close();
					}
				}

				if (!this._config.RunIntegrationTests || !hasStarted) return;
				Console.WriteLine($"Stopping... node has started and ran integrations: {this._config.RunIntegrationTests}");
				Console.WriteLine($"Node started on port: {this.Port} using PID: {this.ProcessId}");
			}
		}

		public void Dispose() => this.Stop();
	}
}
