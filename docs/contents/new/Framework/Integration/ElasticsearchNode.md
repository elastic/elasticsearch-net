---
template: layout.jade
title: x
menusection: concepts
menuitem: breaking-changes
---
```
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
var stdOut = process.CreateStandardOutputObservable();
var stdErr = process.CreateStandardErrorObservable();
var stdOutSubscription = stdOut.Subscribe(observer);
var stdErrSubscription = stdErr.Subscribe(observer);
var processExited = Observable.FromEventPattern(h => process.Exited += h, h => process.Exited -= h);
var processError = CreateProcessExitSubscription(process, processExited, observer);
process.Start();
process.BeginOutputReadLine();
process.BeginErrorReadLine();
observer.OnError(new Exception(
							$"Process '{process.StartInfo.FileName}' terminated with error code {process.ExitCode}"));
observer.OnCompleted();
process?.Close();
ElasticsearchNodeInfo info;
int port;
this.Info = info;
var seeder = new Seeder(this.Port);
seeder.SeedNode();
handle.Set();
this.Started = true;
this.Port = port;
var a = string.Join(" ", arguments);
Console.WriteLine(a);
var zip = $
"elasticsearch-{this.Version}.zip";
var downloadUrl = $
"https://download.elastic.co/elasticsearch/elasticsearch/{zip}";
var localZip = Path.Combine(this.RoamingFolder, zip);
Directory.CreateDirectory(this.RoamingFolder);
new WebClient().DownloadFile(downloadUrl, localZip);
ZipFile.ExtractToDirectory(localZip, this.RoamingFolder);
Console.WriteLine($"Stopping... ran integrations: {this.RunningIntegrations}");
Console.WriteLine($"Node started: {this.Started} on port: {this.Port} using PID: {this.Info?.Pid}");
this.Started = false;
var esProcess = Process.GetProcessById(this.Info.Pid);
Console.WriteLine($"Killing elasticsearch PID {this.Info.Pid}");
esProcess.Kill();
esProcess.WaitForExit(5000);
esProcess.Close();
this._process?.Kill();
this._process?.WaitForExit(2000);
this._process?.Close();
this._processListener?.Dispose();
var dataFolder = Path.Combine(this.RoamingClusterFolder, "data", this.ClusterName);
Console.WriteLine($"attempting to delete cluster data: {dataFolder}");
Directory.Delete(dataFolder, true);
var logPath = Path.Combine(this.RoamingClusterFolder, "logs");
var files = Directory.GetFiles(logPath, this.ClusterName + "*.log");
Console.WriteLine($"attempting to delete log file: {f}");
File.Delete(f);
this.Stop();
nodeInfo = null;
var match = InfoParser.Match(this.Message);
var version = match.Groups["version"].Value.Trim();
var pid = match.Groups["pid"].Value.Trim();
var build = match.Groups["build"].Value.Trim();
nodeInfo = new ElasticsearchNodeInfo(version, pid, build);
port = 0;
var match = PortParser.Match(this.Message);
var portString = match.Groups["port"].Value.Trim();
port = int.Parse(portString);
var receivedStdErr =
				Observable.FromEventPattern<DataReceivedEventHandler, DataReceivedEventArgs>
					(h => process.ErrorDataReceived += h, h => process.ErrorDataReceived -= h)
				.Select(e => new ElasticsearchMessage(e.EventArgs.Data));
var cancel = Disposable.Create(process.CancelErrorRead);
var receivedStdOut =
				Observable.FromEventPattern<DataReceivedEventHandler, DataReceivedEventArgs>
					(h => process.OutputDataReceived += h, h => process.OutputDataReceived -= h)
				.Select(e => new ElasticsearchMessage(e.EventArgs.Data));
var cancel = Disposable.Create(process.CancelOutputRead);
```
