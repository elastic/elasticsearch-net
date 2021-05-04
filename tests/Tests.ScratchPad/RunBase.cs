// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Diagnostics;
using System.Threading;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Nest;
using Tests.Core.Client;

namespace Tests.ScratchPad
{
	[SimpleJob(RunStrategy.Throughput, 2, 2, 5, id: "BenchmarkRun")]
	[MemoryDiagnoser]
	public abstract class RunBase
	{
		private Action<IElasticClient> _run;
		private Action<IElasticClient> _runOnce;
		public bool IsNotBenchmark { get; set; }

		protected virtual IElasticClient Client { get; } = TestClient.DefaultInMemoryClient;
		protected virtual int LoopCount => 100_000;

		[GlobalSetup]
		public void GlobalSetup()
		{
			var r = Routine();
			_run = r.Bind(false);
			_runOnce = r.Bind(true);
		}

		protected abstract RoutineBase Routine();

		[Benchmark]
		public void Run() => _run(Client);

		[Benchmark]
		public void RunCreateOnce() => _runOnce(Client);

		protected LoopRoutine<T> Loop<T>(Func<T> create, Action<IElasticClient, T> act) => new LoopRoutine<T>(create, act, LoopCount, IsNotBenchmark);

		protected class LoopRoutine<T> : RunRoutine<T>
		{
			private readonly bool _isNotBenchmark;
			private readonly int _loopCount;

			public LoopRoutine(Func<T> create, Action<IElasticClient, T> act, int loopCount, bool isNotBenchmark) : base(create, act)
			{
				_loopCount = loopCount;
				_isNotBenchmark = isNotBenchmark;
			}

			public override Action<IElasticClient> Bind(bool cacheCreate)
			{
				var instantiator = !cacheCreate ? Create : CreateCached;
				var limit = _loopCount * (_isNotBenchmark ? 10 : 1);
				if (!_isNotBenchmark) // !!
				{
					return c =>
					{
						Act(c, instantiator());
						for (var i = 0; i < limit; i++) Act(c, instantiator());
					};
				}

				return c =>
				{
					var sw = Stopwatch.StartNew();

					Act(c, instantiator());
					for (var i = 0; i < limit; i++) Act(c, instantiator());

					var elapsed = sw.Elapsed;
					var perOp = elapsed.TotalMilliseconds / limit + 1;
					var messagesPerSecond = TimeSpan.FromSeconds(1).TotalMilliseconds / perOp;
					Console.WriteLine($"Done {limit:N0} iterations in {elapsed}. ({messagesPerSecond:N2}/s)");
				};
			}
		}


		protected abstract class RunRoutine<T> : RoutineBase
		{
			protected RunRoutine(Func<T> create, Action<IElasticClient, T> act)
			{
				Create = create;
				Act = act;
				var lazy = new Lazy<T>(Create, LazyThreadSafetyMode.ExecutionAndPublication);
				CreateCached = () => lazy.Value;
			}

			protected Action<IElasticClient, T> Act { get; }
			protected Func<T> Create { get; }
			protected Func<T> CreateCached { get; }
		}

		protected abstract class RoutineBase
		{
			public abstract Action<IElasticClient> Bind(bool cacheCreate);
		}
	}
}
