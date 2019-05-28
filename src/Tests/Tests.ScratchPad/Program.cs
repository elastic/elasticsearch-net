using System;
using BenchmarkDotNet.Running;
using Elasticsearch.Net;
using Nest;
using Tests.ScratchPad.Runners.Inferrence;

namespace Tests.ScratchPad
{
	public static class ElasticClientExtensions
	{
		[Obsolete()]
		public static void CreateIndex(this IElasticClient client, string index) => client.Indices.Create(index);
	}
	
	public class Program
	{
		private static void Main(string[] args)
		{
			var lowLevel = new ElasticLowLevelClient();
			var info = lowLevel.MachineLearning.Info<StringResponse>();
			var rootInfo = lowLevel.RootNodeInfo<StringResponse>();
			var xpack = lowLevel.XPack.Info<StringResponse>();
			
			//lowLevel.Indices.Index
			//lowLevel.Xpack
			//lowLevel.CrossClusterReplication.
			//lowLevel.IndexLifecycleManagement.
			//lowLevel.XPack.XPackInfo();
			
			//lowLevel.Indices.Create<String>()
			//lowLevel.Security.
			
			
			highLevel.IndexLifecycleManagement.PutLifecycle(new PutLifecycleRequest("policy")
			{
				Policy = new Policy()
			});

		}

		private static void Bench<TBenchmark>() where TBenchmark : RunBase => BenchmarkRunner.Run<TBenchmark>();

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
