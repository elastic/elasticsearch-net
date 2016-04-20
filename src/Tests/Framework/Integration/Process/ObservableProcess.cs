using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Tests.Framework.Integration
{
	public class ObservableProcess : IDisposable
	{
		public ObservableProcess(string bin, params string[] args)
		{
			this.Binary = bin;
			this.Arguments = string.Join(" ", args);
			this.Process = new Process
			{
				EnableRaisingEvents = true,
				StartInfo =
				{
					FileName = this.Binary,
					Arguments = this.Arguments,
					CreateNoWindow = true,
					UseShellExecute = false,
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					RedirectStandardInput = false
				}
			};
		}

		private bool Started { get; set; }

		public int? ExitCode { get; set; }

		public string Binary { get; private set; }

		public Process Process { get; private set; }

		public string Arguments { get; private set; }

		public IObservable<string> Start()
		{
			return Observable.Create<string>(observer =>
			{
				// listen to stdout and stderr
				var stdOut = this.Process.CreateStandardOutputObservable();
				var stdErr = this.Process.CreateStandardErrorObservable();

				var stdOutSubscription = stdOut.Subscribe(observer);
				var stdErrSubscription = stdErr.Subscribe(observer);

				var processExited = Observable.FromEventPattern(h => this.Process.Exited += h, h => this.Process.Exited -= h);
				var processError = CreateProcessExitSubscription(this.Process, processExited, observer);

				if (!this.Process.Start())
					throw new Exception($"Failed to start observable process: {this.Binary}");

				this.Process.BeginOutputReadLine();
				this.Process.BeginErrorReadLine();
				this.Started = true;

				return new CompositeDisposable(stdOutSubscription, stdErrSubscription, processError);
			});
		}

		private IDisposable CreateProcessExitSubscription(Process process, IObservable<EventPattern<object>> processExited, IObserver<string> observer)
		{
			return processExited.Subscribe(args =>
			{
				try
				{
					this.ExitCode = process?.ExitCode;
					if (process?.ExitCode > 0)
					{
						observer.OnError(new Exception(
							$"Process '{process.StartInfo.FileName}' terminated with error code {process.ExitCode}"));
					}
					else observer.OnCompleted();
				}
				finally
				{
					this.Started = false;
					process?.Close();
				}
			});
		}

		public void Stop()
		{
			if (this.Started)
			{
				try
				{
					this.Process?.Kill();
					this.Process?.WaitForExit(2000);
					this.Process?.Close();
				}
				catch (Exception)
				{
				}
			}
			this.Started = false;
		}

		public void Dispose() => this.Stop();
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
