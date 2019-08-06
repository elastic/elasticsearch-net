using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.IndexManagement.OpenCloseIndex.OpenIndex
{
	/*
	 * {
  "error" : {
    "root_cause" : [
      {
        "type" : "illegal_argument_exception",
        "reason" : "request [POST /nest-initializerasync-962199a2/_open] does not support having a body",
"stack_trace" : "[request [POST /nest-initializerasync-962199a2/_open] does not support having a body]; nested: IllegalArgumentException[request [POST /nest-initializerasync-962199a2/_open] does not support having a body];\n\tat org.elasticsearch.ElasticsearchException.guessRootCauses(ElasticsearchException.java:639)\n\tat org.elasticsearch.ElasticsearchException.generateFailureXContent(ElasticsearchException.java:567)\n\tat org.elasticsearch.rest.BytesRestResponse.build(BytesRestResponse.java:138)\n\tat org.elasticsearch.rest.BytesRestResponse.<init>(BytesRestResponse.java:96)\n\tat org.elasticsearch.rest.BytesRestResponse.<init>(BytesRestResponse.java:91)\n\tat org.elasticsearch.rest.RestController.dispatchRequest(RestController.java:243)\n\tat org.elasticsearch.rest.RestController.tryAllHandlers(RestController.java:344)\n\tat org.elasticsearch.rest.RestController.dispatchRequest(RestController.java:174)\n\tat org.elasticsearch.http.AbstractHttpServerTransport.dispatchRequest(AbstractHttpServerTransport.java:322)\n\tat org.elasticsearch.http.AbstractHttpServerTransport.handleIncomingRequest(AbstractHttpServerTransport.java:372)\n\tat org.elasticsearch.http.AbstractHttpServerTransport.incomingRequest(AbstractHttpServerTransport.java:301)\n\tat org.elasticsearch.http.netty4.Netty4HttpRequestHandler.channelRead0(Netty4HttpRequestHandler.java:69)\n\tat org.elasticsearch.http.netty4.Netty4HttpRequestHandler.channelRead0(Netty4HttpRequestHandler.java:31)\n\tat io.netty.channel.SimpleChannelInboundHandler.channelRead(SimpleChannelInboundHandler.java:105)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat org.elasticsearch.http.netty4.Netty4HttpPipeliningHandler.channelRead(Netty4HttpPipeliningHandler.java:58)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.handler.codec.MessageToMessageDecoder.channelRead(MessageToMessageDecoder.java:102)\n\tat io.netty.handler.codec.MessageToMessageCodec.channelRead(MessageToMessageCodec.java:111)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.handler.codec.MessageToMessageDecoder.channelRead(MessageToMessageDecoder.java:102)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.handler.codec.MessageToMessageDecoder.channelRead(MessageToMessageDecoder.java:102)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.handler.codec.ByteToMessageDecoder.fireChannelRead(ByteToMessageDecoder.java:323)\n\tat io.netty.handler.codec.ByteToMessageDecoder.channelRead(ByteToMessageDecoder.java:297)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.handler.timeout.IdleStateHandler.channelRead(IdleStateHandler.java:287)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.channel.DefaultChannelPipeline$HeadContext.channelRead(DefaultChannelPipeline.java:1408)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.DefaultChannelPipeline.fireChannelRead(DefaultChannelPipeline.java:930)\n\tat io.netty.channel.nio.AbstractNioByteChannel$NioByteUnsafe.read(AbstractNioByteChannel.java:163)\n\tat io.netty.channel.nio.NioEventLoop.processSelectedKey(NioEventLoop.java:682)\n\tat io.netty.channel.nio.NioEventLoop.processSelectedKeysPlain(NioEventLoop.java:582)\n\tat io.netty.channel.nio.NioEventLoop.processSelectedKeys(NioEventLoop.java:536)\n\tat io.netty.channel.nio.NioEventLoop.run(NioEventLoop.java:496)\n\tat io.netty.util.concurrent.SingleThreadEventExecutor$5.run(SingleThreadEventExecutor.java:906)\n\tat io.netty.util.internal.ThreadExecutorMap$2.run(ThreadExecutorMap.java:74)\n\tat java.base/java.lang.Thread.run(Thread.java:835)\nCaused by: java.lang.IllegalArgumentException: request [POST /nest-initializerasync-962199a2/_open] does not support having a body\n\tat org.elasticsearch.rest.BaseRestHandler.handleRequest(BaseRestHandler.java:108)\n\tat org.elasticsearch.rest.RestController.dispatchRequest(RestController.java:240)\n\t... 49 more\n"
      }
    ],
    "type" : "illegal_argument_exception",
    "reason" : "request [POST /nest-initializerasync-962199a2/_open] does not support having a body",
"stack_trace" : "java.lang.IllegalArgumentException: request [POST /nest-initializerasync-962199a2/_open] does not support having a body\n\tat org.elasticsearch.rest.BaseRestHandler.handleRequest(BaseRestHandler.java:108)\n\tat org.elasticsearch.rest.RestController.dispatchRequest(RestController.java:240)\n\tat org.elasticsearch.rest.RestController.tryAllHandlers(RestController.java:344)\n\tat org.elasticsearch.rest.RestController.dispatchRequest(RestController.java:174)\n\tat org.elasticsearch.http.AbstractHttpServerTransport.dispatchRequest(AbstractHttpServerTransport.java:322)\n\tat org.elasticsearch.http.AbstractHttpServerTransport.handleIncomingRequest(AbstractHttpServerTransport.java:372)\n\tat org.elasticsearch.http.AbstractHttpServerTransport.incomingRequest(AbstractHttpServerTransport.java:301)\n\tat org.elasticsearch.http.netty4.Netty4HttpRequestHandler.channelRead0(Netty4HttpRequestHandler.java:69)\n\tat org.elasticsearch.http.netty4.Netty4HttpRequestHandler.channelRead0(Netty4HttpRequestHandler.java:31)\n\tat io.netty.channel.SimpleChannelInboundHandler.channelRead(SimpleChannelInboundHandler.java:105)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat org.elasticsearch.http.netty4.Netty4HttpPipeliningHandler.channelRead(Netty4HttpPipeliningHandler.java:58)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.handler.codec.MessageToMessageDecoder.channelRead(MessageToMessageDecoder.java:102)\n\tat io.netty.handler.codec.MessageToMessageCodec.channelRead(MessageToMessageCodec.java:111)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.handler.codec.MessageToMessageDecoder.channelRead(MessageToMessageDecoder.java:102)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.handler.codec.MessageToMessageDecoder.channelRead(MessageToMessageDecoder.java:102)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.handler.codec.ByteToMessageDecoder.fireChannelRead(ByteToMessageDecoder.java:323)\n\tat io.netty.handler.codec.ByteToMessageDecoder.channelRead(ByteToMessageDecoder.java:297)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.handler.timeout.IdleStateHandler.channelRead(IdleStateHandler.java:287)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.channel.DefaultChannelPipeline$HeadContext.channelRead(DefaultChannelPipeline.java:1408)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.DefaultChannelPipeline.fireChannelRead(DefaultChannelPipeline.java:930)\n\tat io.netty.channel.nio.AbstractNioByteChannel$NioByteUnsafe.read(AbstractNioByteChannel.java:163)\n\tat io.netty.channel.nio.NioEventLoop.processSelectedKey(NioEventLoop.java:682)\n\tat io.netty.channel.nio.NioEventLoop.processSelectedKeysPlain(NioEventLoop.java:582)\n\tat io.netty.channel.nio.NioEventLoop.processSelectedKeys(NioEventLoop.java:536)\n\tat io.netty.channel.nio.NioEventLoop.run(NioEventLoop.java:496)\n\tat io.netty.util.concurrent.SingleThreadEventExecutor$5.run(SingleThreadEventExecutor.java:906)\n\tat io.netty.util.internal.ThreadExecutorMap$2.run(ThreadExecutorMap.java:74)\n\tat java.base/java.lang.Thread.run(Thread.java:835)\n"
  },
  "status" : 400
}
	 */
	[SkipVersion(">=8.0.0-SNAPSHOT", "")]
	public class OpenIndexApiTests
		: ApiIntegrationTestBase<WritableCluster, OpenIndexResponse, IOpenIndexRequest, OpenIndexDescriptor, OpenIndexRequest>
	{
		public OpenIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<OpenIndexDescriptor, IOpenIndexRequest> Fluent => d => d
			.IgnoreUnavailable();

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override OpenIndexRequest Initializer => new OpenIndexRequest(CallIsolatedValue)
		{
			IgnoreUnavailable = true
		};

		protected override string UrlPath => $"/{CallIsolatedValue}/_open?ignore_unavailable=true";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values)
			{
				client.Indices.Create(index);
				client.Cluster.Health(index, h => h.WaitForStatus(WaitForStatus.Yellow));
				client.Indices.Close(index);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Open(CallIsolatedValue, f),
			(client, f) => client.Indices.OpenAsync(CallIsolatedValue, f),
			(client, r) => client.Indices.Open(r),
			(client, r) => client.Indices.OpenAsync(r)
		);

		protected override OpenIndexDescriptor NewDescriptor() => new OpenIndexDescriptor(CallIsolatedValue);
	}
}
