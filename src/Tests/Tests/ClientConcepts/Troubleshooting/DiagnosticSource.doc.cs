using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using Elastic.Xunit.Sdk;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Elasticsearch.Net.Diagnostics;
using FluentAssertions;
using Nest;
using Tests.Core.Client.Settings;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Xunit;

namespace Tests.ClientConcepts.Troubleshooting
{
	/**
	 * === Diagnostic Source
	 *
	 * Elasticsearch.Net and by proxy NEST ship with support for DiagnosticSource and Activity out of the box.
	 *
	 * To aid with their discover the topics you can subscribe on and the event names they emit are exposed as
	 * strongly typed strings under `Elasticsearch.Net.Diagnostics.DiagnosticSources`
	 * 
	 */
	public class DiagnosticSourceUsageDocumentation : IClusterFixture<ReadOnlyCluster>
	{
		private readonly ReadOnlyCluster _cluster;

		public DiagnosticSourceUsageDocumentation(ReadOnlyCluster cluster) => _cluster = cluster;
		
		
		/**
		 * Subscribing to DiagnosticSources means implementing `IObserver<DiagnosticListener>`
		 * or use `.Subscribe(observer, filter)` to opt in to the correct topic.
		 *
		 * Here we choose the more verbose `IObserver<>` implementation.
		 * 
		 */
		private class ListenerObserver : IObserver<DiagnosticListener>
		{
			private long _messagesWrittenToConsole = 0;
			public long MessagesWrittenToConsole => _messagesWrittenToConsole;
			
			public Exception SeenException { get; private set; }
			public void OnError(Exception error) => SeenException = error;

			public bool Completed { get; private set; }
			public void OnCompleted() => Completed = true;

			private void WriteToConsole<T>(string eventName, T data)
			{
				var a = Activity.Current;
				Console.WriteLine($"{eventName?.PadRight(30)} {a.Id?.PadRight(32)} {a.ParentId?.PadRight(32)} {data?.ToString().PadRight(10)}");
				Interlocked.Increment(ref _messagesWrittenToConsole);
			}
			
			/**
			 * By inspecting the name we selectively subscribe only to topics `Elasticsearch.Net` emits.
			 *
			 * Thanks to `DiagnosticSources` you do not have to guess the topics we emit under.
			 *
			 * `DiagnosticListener.Subscribe` expects an `IObserver<KeyValuePair<string, object>>` which is useful to create
			 * a decoupled messaging contract but as a subscriber you would like to know what `object` is.
			 *
			 * Therefor each topic we ship with has a dedicated `Observer` implementation that takes an `onNext` lambda
			 * which is typed to the context object we actually emit.
			 * 
			 */
			public void OnNext(DiagnosticListener value)
			{
				
				if (value.Name == DiagnosticSources.AuditTrailEvents.SourceName)
					value.Subscribe(new AuditDiagnosticListener(v => WriteToConsole(v.EventName, v.Audit)));
				
				/**
				 * RequestPipeline emits a different context object for the start of the `Activity` then it does
				 * for the end of the `Activity` therefor `RequestPipelineDiagnosticObserver` accepts two `onNext` lambda's.
				 * One for the `.Start` events and one for the `.Stop` events.
				 */
				if (value.Name == DiagnosticSources.RequestPipeline.SourceName)
					value.Subscribe(new RequestPipelineDiagnosticListener(
						v => WriteToConsole(v.EventName, v.RequestData),
						v => WriteToConsole(v.EventName, v.Response))
					);
				
				if (value.Name == DiagnosticSources.HttpConnection.SourceName)
					value.Subscribe(new HttpConnectionDiagnosticListener(
						v => WriteToConsole(v.EventName, v.RequestData),
						v => WriteToConsole(v.EventName, v.StatusCode)
					));
				
				if (value.Name == DiagnosticSources.Serializer.SourceName)
					value.Subscribe(new SerializerDiagnosticListener(v => WriteToConsole(v.EventName, v.Registration)));
			}
		}

		[I] public void SubscribeToTopics()
		{
			/**
			 * Here we hook into all diagnostic sources and use `ListenerObserver` to only listen to the ones
			 * from `Elasticsearch.Net`
			 */
			var listenerObserver = new ListenerObserver();
			using (var subscription = DiagnosticListener.AllListeners.Subscribe(listenerObserver))
			{
				
				/**
				 * We'll use a Sniffing connection pool here since it sniffs on startup and pings before
				 * first usage, so our diagnostics are involved enough to showcase most topics.
				 */
				var pool = new SniffingConnectionPool(new []{ TestConnectionSettings.CreateUri() });
				var connectionSettings = new ConnectionSettings(pool)
					.DefaultMappingFor<Project>(i => i
						.IndexName("project")
					);

				var client = new ElasticClient(connectionSettings);

				/**
				 * After issuing the following request
				 */
				var response = client.Search<Project>(s => s
					.MatchAll()
				);

				listenerObserver.SeenException.Should().BeNull();
				listenerObserver.Completed.Should().BeFalse();
				listenerObserver.MessagesWrittenToConsole.Should().BeGreaterThan(0);
			}
		}
	}
}
