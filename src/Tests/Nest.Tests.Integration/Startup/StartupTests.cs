using System;
using System.Diagnostics;
using System.Runtime.Remoting.Channels;
using System.Threading;
using AppDomainToolkit;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using FluentAssertions;
using Nest;
using Nest.Tests.Integration;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace StartupTests
{
	[TestFixture]
	public class StartupTests : IntegrationTests
	{
		private static readonly AppDomainSetup _setupInfo = new AppDomainSetup()
		{
			ApplicationName = "NEST.Warmup.Tests",
			ApplicationBase = Environment.CurrentDirectory,
			PrivateBinPath = Environment.CurrentDirectory
		};

		[Test]
		public void Calling_RootNodeInfo_TwiceInOneAssembly_IsFast()
		{
			using (var context = AppDomainContext.Create(_setupInfo))
			{
				var result = RemoteFunc.Invoke(context.Domain,
					() => new NoWarmupRoutine(c=>c.RootNodeInfo())
				);

				result.ElapsedMilliseconds.Should().BeGreaterOrEqualTo(100);
				
				result = RemoteFunc.Invoke(context.Domain,
					() => new NoWarmupRoutine(c=>c.RootNodeInfo())
				);

				result.ElapsedMilliseconds.Should().BeLessOrEqualTo(5);
			}
		}
		
		[Test]
		public void Calling_RootNodeInfo_OnceInTwoAsemblies_SlowTwice()
		{
			using (var context = AppDomainContext.Create(_setupInfo))
			{
				var result = RemoteFunc.Invoke(context.Domain,
					() => new NoWarmupRoutine(c=>c.RootNodeInfo())
				);

				result.ElapsedMilliseconds.Should().BeGreaterOrEqualTo(100);
			}
			
			using (var context = AppDomainContext.Create(_setupInfo))
			{
				var result = RemoteFunc.Invoke(context.Domain,
					() => new NoWarmupRoutine(c=>c.RootNodeInfo())
				);

				result.ElapsedMilliseconds.Should().BeGreaterOrEqualTo(100);
			}
		}

		
		
		[Test]
		public void Calling_RootNodeInfo_AfterWarmup_IsFast()
		{
			using (var context = AppDomainContext.Create(_setupInfo))
			{
				var result = RemoteFunc.Invoke(context.Domain,
					() => new WarmupRoutine(c=>c.RootNodeInfo())
				);

				result.ElapsedMilliseconds.Should().BeLessOrEqualTo(20);
			}
		}
		
		[Test]
		public void Calling_Search_TwiceInAppDomainIsFast()
		{
			using (var context = AppDomainContext.Create(_setupInfo))
			{
				var result = RemoteFunc.Invoke(context.Domain,
					() => new NoWarmupRoutine(c=>c.Search<ElasticsearchProject>(s=>s.MatchAll()))
				);

				result.ElapsedMilliseconds.Should().BeGreaterOrEqualTo(100);
				
				Thread.Sleep(2000);

				result = RemoteFunc.Invoke(context.Domain,
					() => new NoWarmupRoutine(c=>c.Search<ElasticsearchProject>(s=>s.MatchAll()))
				);

				result.ElapsedMilliseconds.Should().BeLessOrEqualTo(10);
			}
		}
		
		[Test]
		public void Calling_Search_IsAlsoFaster_AfterWarmup()
		{
			using (var context = AppDomainContext.Create(_setupInfo))
			{
				var result = RemoteFunc.Invoke(context.Domain,
					() => new WarmupRoutine(c=>c.Search<object>(s=>s.MatchAll()))
				);

				result.ElapsedMilliseconds.Should().BeLessOrEqualTo(20);
			}
		}
	}





	[Serializable]
	public class BaseElasticsearchInSeparateDomain : MarshalByRefObject
	{
		protected readonly IElasticClient _client;
		public BaseElasticsearchInSeparateDomain()
		{
			var settings = Settings(9200);
			this._client = new ElasticClient(settings, new InMemoryConnection(settings));
		}

		private ConnectionSettings Settings(int? port = null, Uri hostOverride = null)
		{

			return new ConnectionSettings(hostOverride ?? CreateBaseUri(port), ElasticsearchConfiguration.DefaultIndex)
				.UsePrettyResponses()
				.ExposeRawResponse();
		}

		private Uri CreateBaseUri(int? port = null)
		{
			var host = "localhost";
			if (port == null && Process.GetProcessesByName("fiddler").HasAny())
				host = "ipv4.fiddler";

			var uri = new UriBuilder("http", host, port.GetValueOrDefault(9200)).Uri;
			return uri;
		}
	}


	//This runs in a separate appdomain
	[Serializable]
	public class NoWarmupRoutine : BaseElasticsearchInSeparateDomain
	{
		public long ElapsedMilliseconds { get; set; }

		public NoWarmupRoutine(Action<IElasticClient> call)
		{
			var sw = Stopwatch.StartNew();
			call(_client);
			sw.Stop();
			this.ElapsedMilliseconds = sw.ElapsedMilliseconds;
		}
	}

	//This runs in a separate appdomain
	[Serializable]
	public class WarmupRoutine : BaseElasticsearchInSeparateDomain
	{
		public long ElapsedMilliseconds { get; set; }

		public WarmupRoutine(Action<IElasticClient> call)
		{
			ElasticClient.Warmup();
			var sw = Stopwatch.StartNew();
			call(_client);
			sw.Stop();
			this.ElapsedMilliseconds = sw.ElapsedMilliseconds;
		}

	}
}