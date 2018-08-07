using BenchmarkDotNet.Running;
using Tests.ScratchPad.Runners.Inferrence;

namespace Tests.ScratchPad
{
	public class Program
	{
		static void Main(string[] args)
		{
			//Use Bench<>() to Benchmark using BanchmarkDotNet
			//Use Run<>() to execture the loop (handy for profilers), this will call create for each itteration
			//Use RunCreateOnce<>() to do the same but call create once before the loop.

			Run<PropertyNameInferenceRunner>();


			//Run(ResolveSomeIds);
			//RunCreateOnce(ResolveSomePropertyNames);
			//Run(ResolveSomeFieldsx);
			//Run(CreateSomeExpressions);
			//Run(DoNothing);
		}

		static void Bench<TBenchmark>() where TBenchmark : RunBase => BenchmarkRunner.Run<TBenchmark>();

		static void Run<TRun>() where TRun : RunBase, new()
		{
			var runner = new TRun { IsNotBenchmark = true };
			runner.GlobalSetup();
			runner.Run();
		}
		static void RunCreateOnce<TRun>() where TRun : RunBase, new()
		{
			var runner = new TRun { IsNotBenchmark = true };
			runner.GlobalSetup();
			runner.RunCreateOnce();
		}


	}
}
