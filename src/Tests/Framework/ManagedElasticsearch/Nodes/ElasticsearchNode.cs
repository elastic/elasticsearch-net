using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Bogus;
using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;
using Tests.Framework.Configuration;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Process;
using Tests.Framework.ManagedElasticsearch.SourceSerializers;
using Tests.Framework.Versions;
#if !DOTNETCORE
using XplatManualResetEvent = System.Threading.ManualResetEvent;

#endif

namespace Tests.Framework.ManagedElasticsearch.Nodes
{
	public class ElasticsearchNode : IDisposable
	{
		private readonly object _lock = new object();

		private CompositeDisposable _composite;
		private int? ProcessId { get; set; }

		private readonly NodeConfiguration _config;

		public ElasticsearchVersion Version => _config.ElasticsearchVersion;

		public NodeFileSystem FileSystem { get; }

		public bool Started { get; private set; }

		public int Port { get; private set; }

		private bool RunningOnCI { get; }

		public bool UsingSourceSerializer { get; }

		public ElasticsearchNode(NodeConfiguration config)
		{
			this._config = config;
			this.FileSystem = config.FileSystem;
			this.RunningOnCI = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TEAMCITY_VERSION"));
			this.Port = config.DesiredPort;
			this.UsingSourceSerializer = config.UsingCustomSourceSerializer ;
		}

		private readonly object _lockGetClient = new object { };
		private volatile IElasticClient _client;

		public IElasticClient Client
		{
			get
			{
				ThrowIfNotStarted();

				if (this._client != null) return this._client;

				lock (_lockGetClient)
				{
					if (this._client != null) return this._client;

					var port = this.Started ? this.Port : 9200;

					this._client = TestClient.GetClient(
						ComposeSettings,
						port,
						forceSsl: this._config.EnableSsl,
						sourceSerializerFactory: (settings, builtin) =>
						{
							var customSourceSerializer = new TestSourceSerializer(builtin);
							return this.UsingSourceSerializer ? customSourceSerializer : null;
						}
					);
				}
				return this._client;
			}
		}

		private void ThrowIfNotStarted()
		{
			if (this.Started || !TestClient.Configuration.RunIntegrationTests) return;
			var logFile = Path.Combine(this.FileSystem.LogsPath, $"{this._config.NodeName}.log");
			throw new Exception($"cannot request a client from an ElasticsearchNode that hasn't started yet. " +
			                    $"Check the log at {logFile} to see if there was an issue starting");
		}

		public void Start(string[] settings, TimeSpan startTimeout)
		{
			if (!this._config.RunIntegrationTests || this.Started) return;
			lock (_lock)
			{
				if (!this._config.RunIntegrationTests || this.Started) return;

				this.FreeResources();

				if (UseAlreadyRunningInstance())
				{
					this.Started = true;
					return;
				}

				var handle = new XplatManualResetEvent(false);
				var booted = false;
				var process = new ObservableProcess(this.FileSystem.Binary, settings);
				this._composite = new CompositeDisposable(process);
				Console.WriteLine($"Starting: {process.Binary} {process.Arguments}");
				try
				{
					var subscription = Observable.Using(() => process, p => p.Start())
						.Select(c => new ElasticsearchConsoleOut(this._config.ElasticsearchVersion, c.Error, c.Data))
						.Subscribe(s => this.HandleConsoleMessage(s, handle), e => throw e, () => handle.Set());
					this._composite.Add(subscription);

					if (!handle.WaitOne(startTimeout, true))
						throw new Exception($"Could not start elasticsearch within {startTimeout}");

					booted = true;
				}
				finally
				{
					if (!booted) this.FreeResources();
				}
			}
		}

		private bool UseAlreadyRunningInstance()
		{
			var client = this.GetPrivateClient(null, false, this.Port);
			return this._config.TestAgainstAlreadyRunningElasticsearch && client.RootNodeInfo().IsValid;
		}

		private void HandleConsoleMessage(ElasticsearchConsoleOut consoleOut, XplatManualResetEvent handle)
		{
			//no need to snoop for metadata if we already started
			if (!this._config.RunIntegrationTests || this.Started) return;
			//if we are running on CI and not started dump elasticsearch stdout/err
			//before the started notification to help debug failures to start
			if (this.RunningOnCI && !this.Started)
			{
				if (consoleOut.Error) Console.Error.WriteLine(consoleOut.Data);
				else Console.WriteLine(consoleOut.Data);
			}

			if (consoleOut.Error && !this.Started && !string.IsNullOrWhiteSpace(consoleOut.Data)) throw new Exception(consoleOut.Data);

			string version;
			int? pid;
			int port;

			if (this.ProcessId == null && consoleOut.TryParseNodeInfo(out version, out pid))
			{
				var startedVersion = ElasticsearchVersion.GetOrAdd(version);
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
			AddClusterSpecificConnectionSettings(AppendClusterNameToHttpHeaders(settings(s)));

		private IElasticClient GetPrivateClient(Func<ConnectionSettings, ConnectionSettings> settings, bool forceInMemory, int port)
		{
			settings = settings ?? (s => s);
			var client = forceInMemory
				? TestClient.GetInMemoryClient(s => ClusterSettings(s, settings), port)
				: TestClient.GetClient(s => ClusterSettings(s, settings), port, forceSsl: this._config.EnableSsl);
			return client;
		}

		private ConnectionSettings ComposeSettings(ConnectionSettings s) =>
			AddClusterSpecificConnectionSettings(AppendClusterNameToHttpHeaders(s));

		private ConnectionSettings AddClusterSpecificConnectionSettings(ConnectionSettings settings) =>
			this._config.ClusterConnectionSettings(settings);

		private ConnectionSettings AppendClusterNameToHttpHeaders(ConnectionSettings settings)
		{
			IConnectionConfigurationValues values = settings;
			var headers = values.Headers ?? new NameValueCollection();
			headers.Add("ClusterName", this._config.ClusterName);
			return settings;
		}

		private void FreeResources()
		{
			var hasStarted = this.Started;
			this.Started = false;

			this._composite?.Dispose();

			var esProcess = this.ProcessId == null
				? null
				: System.Diagnostics.Process.GetProcesses().FirstOrDefault(p => p.Id == this.ProcessId.Value);
			if (esProcess != null)
			{
				Console.WriteLine($"Killing elasticsearch PID {this.ProcessId}");
				esProcess.Kill();
				esProcess.WaitForExit(5000);
				esProcess.Close();
			}

			if (!this._config.RunIntegrationTests || !hasStarted) return;
			Console.WriteLine($"Stopping... node has started and ran integrations: {this._config.RunIntegrationTests}");
			Console.WriteLine($"Node started on port: {this.Port} using PID: {this.ProcessId}");
		}

		public void Stop()
		{
			lock (_lock) this.FreeResources();
		}

		public void Dispose() => this.Stop();
	}
}
