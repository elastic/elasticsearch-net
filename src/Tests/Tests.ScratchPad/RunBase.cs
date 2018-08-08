using System;
using System.Diagnostics;
using System.Threading;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Nest;
using Tests.Core.Client;

namespace Tests.ScratchPad
{
	[SimpleJob(RunStrategy.Throughput, launchCount: 2, warmupCount: 2, targetCount: 5, id: "BenchmarkRun")]
	[MemoryDiagnoser]
	public abstract class RunBase
	{
		protected virtual int LoopCount => 100_000;
		public bool IsNotBenchmark { get; set; }

		protected virtual IElasticClient Client { get; } = TestClient.DefaultInMemoryClient;
		private Action<IElasticClient> _run;
		private Action<IElasticClient> _runOnce;

		[GlobalSetup]
		public void GlobalSetup()
		{
			var r = this.Routine();
			_run = r.Bind(cacheCreate: false);
			_runOnce = r.Bind(cacheCreate: true);
		}

		protected abstract RoutineBase Routine();

		[Benchmark]
		public void Run() => _run(this.Client);

		[Benchmark]
		public void RunCreateOnce() => _runOnce(this.Client);

		protected LoopRoutine<T> Loop<T>(Func<T> create, Action<IElasticClient, T> act)
		{
			return new LoopRoutine<T>(create, act, this.LoopCount, this.IsNotBenchmark);
		}

		protected class LoopRoutine<T> : RunRoutine<T>
		{
			private readonly int _loopCount;
			private readonly bool _isNotBenchmark;

			public LoopRoutine(Func<T> create, Action<IElasticClient, T> act, int loopCount, bool isNotBenchmark) : base(create, act)
			{
				_loopCount = loopCount;
				_isNotBenchmark = isNotBenchmark;
			}

			public override Action<IElasticClient> Bind(bool cacheCreate)
			{
				var instantiator = !cacheCreate ? this.Create : this.CreateCached;
				var limit = _loopCount * (_isNotBenchmark ? 10 : 1);
				if (!this._isNotBenchmark) // !!
					return c =>
					{
						this.Act(c, instantiator());
						for (var i = 0; i < limit; i++) this.Act(c, instantiator());
					};

				return c =>
				{
					var sw = Stopwatch.StartNew();

					this.Act(c, instantiator());
					for (var i = 0; i < limit; i++) this.Act(c, instantiator());

					var elapsed = sw.Elapsed;
					var perOp = elapsed.TotalMilliseconds / (double) limit + 1;
					var messagesPerSecond = TimeSpan.FromSeconds(1).TotalMilliseconds / perOp;
					Console.WriteLine($"Done {limit:N0} iterations in {elapsed}. ({messagesPerSecond:N2}/s)");
				};
			}
		}


		protected abstract class RunRoutine<T> : RoutineBase
		{
			protected RunRoutine(Func<T> create, Action<IElasticClient, T> act)
			{
				this.Create = create;
				this.Act = act;
				var lazy = new Lazy<T>(this.Create, LazyThreadSafetyMode.ExecutionAndPublication);
				this.CreateCached = () => lazy.Value;
			}
			protected Func<T> Create { get; }
			protected Action<IElasticClient, T> Act { get; }
			protected Func<T> CreateCached { get; }

		}

		protected abstract class RoutineBase
		{
			public abstract Action<IElasticClient> Bind(bool cacheCreate);
		}
	}
}
