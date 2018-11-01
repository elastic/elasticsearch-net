using BenchmarkDotNet.Running;
using Tests.ScratchPad.Runners.Inferrence;

namespace Tests.ScratchPad
{
	public class Program
	{
		private static void Bench<TBenchmark>() where TBenchmark : RunBase => BenchmarkRunner.Run<TBenchmark>();

		//Use Bench<>() to Benchmark using BanchmarkDotNet
		//Use Run<>() to execture the loop (handy for profilers), this will call create for each itteration
		//Use RunCreateOnce<>() to do the same but call create once before the loop.
		//Run(ResolveSomeIds);
		//RunCreateOnce(ResolveSomePropertyNames);
		//Run(ResolveSomeFieldsx);
		//Run(CreateSomeExpressions);
		//Run(DoNothing);
		private static void Main(string[] args) => Run<PropertyNameInferenceRunner>();

		private static void Run<TRun>() where TRun : RunBase, new()
		{
			var runner = new TRun { IsNotBenchmark = true };
			runner.GlobalSetup();
			runner.Run();
		}

		private static void RunCreateOnce<TRun>() where TRun : RunBase, new()
		{
			var runner = new TRun { IsNotBenchmark = true };
			runner.GlobalSetup();
			runner.RunCreateOnce();
		}
	}
}
