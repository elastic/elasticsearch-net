// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.QueryDsl.FullText.Match
{
	/*
	 * {
  "error" : {
    "root_cause" : [
      {
        "type" : "parsing_exception",
        "reason" : "[match] query does not support [cutoff_frequency]",
        "line" : 1,
        "col" : 120,
        "stack_trace" : "ParsingException[[match] query does not support [cutoff_frequency]]\n\tat org.elasticsearch.index.query.MatchQueryBuilder.fromXContent(MatchQueryBuilder.java:480)\n\tat org.elasticsearch.search.SearchModule.lambda$registerQuery$11(SearchModule.java:813)\n\tat org.elasticsearch.common.xcontent.NamedXContentRegistry.parseNamedObject(NamedXContentRegistry.java:141)\n\tat org.elasticsearch.common.xcontent.support.AbstractXContentParser.namedObject(AbstractXContentParser.java:385)\n\tat org.elasticsearch.index.query.AbstractQueryBuilder.parseInnerQueryBuilder(AbstractQueryBuilder.java:314)\n\tat org.elasticsearch.search.builder.SearchSourceBuilder.parseXContent(SearchSourceBuilder.java:1065)\n\tat org.elasticsearch.rest.action.search.RestSearchAction.parseSearchRequest(RestSearchAction.java:121)\n\tat org.elasticsearch.rest.action.search.RestSearchAction.lambda$prepareRequest$1(RestSearchAction.java:100)\n\tat org.elasticsearch.rest.RestRequest.withContentOrSourceParamParserOrNull(RestRequest.java:449)\n\tat org.elasticsearch.rest.action.search.RestSearchAction.prepareRequest(RestSearchAction.java:99)\n\tat org.elasticsearch.rest.BaseRestHandler.handleRequest(BaseRestHandler.java:92)\n\tat org.elasticsearch.rest.RestController.dispatchRequest(RestController.java:240)\n\tat org.elasticsearch.rest.RestController.tryAllHandlers(RestController.java:344)\n\tat org.elasticsearch.rest.RestController.dispatchRequest(RestController.java:174)\n\tat org.elasticsearch.http.AbstractHttpServerTransport.dispatchRequest(AbstractHttpServerTransport.java:322)\n\tat org.elasticsearch.http.AbstractHttpServerTransport.handleIncomingRequest(AbstractHttpServerTransport.java:372)\n\tat org.elasticsearch.http.AbstractHttpServerTransport.incomingRequest(AbstractHttpServerTransport.java:301)\n\tat org.elasticsearch.http.netty4.Netty4HttpRequestHandler.channelRead0(Netty4HttpRequestHandler.java:69)\n\tat org.elasticsearch.http.netty4.Netty4HttpRequestHandler.channelRead0(Netty4HttpRequestHandler.java:31)\n\tat io.netty.channel.SimpleChannelInboundHandler.channelRead(SimpleChannelInboundHandler.java:105)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat org.elasticsearch.http.netty4.Netty4HttpPipeliningHandler.channelRead(Netty4HttpPipeliningHandler.java:58)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.handler.codec.MessageToMessageDecoder.channelRead(MessageToMessageDecoder.java:102)\n\tat io.netty.handler.codec.MessageToMessageCodec.channelRead(MessageToMessageCodec.java:111)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.handler.codec.MessageToMessageDecoder.channelRead(MessageToMessageDecoder.java:102)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.handler.codec.MessageToMessageDecoder.channelRead(MessageToMessageDecoder.java:102)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.handler.codec.ByteToMessageDecoder.fireChannelRead(ByteToMessageDecoder.java:323)\n\tat io.netty.handler.codec.ByteToMessageDecoder.channelRead(ByteToMessageDecoder.java:297)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.handler.timeout.IdleStateHandler.channelRead(IdleStateHandler.java:287)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.channel.DefaultChannelPipeline$HeadContext.channelRead(DefaultChannelPipeline.java:1408)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.DefaultChannelPipeline.fireChannelRead(DefaultChannelPipeline.java:930)\n\tat io.netty.channel.nio.AbstractNioByteChannel$NioByteUnsafe.read(AbstractNioByteChannel.java:163)\n\tat io.netty.channel.nio.NioEventLoop.processSelectedKey(NioEventLoop.java:682)\n\tat io.netty.channel.nio.NioEventLoop.processSelectedKeysPlain(NioEventLoop.java:582)\n\tat io.netty.channel.nio.NioEventLoop.processSelectedKeys(NioEventLoop.java:536)\n\tat io.netty.channel.nio.NioEventLoop.run(NioEventLoop.java:496)\n\tat io.netty.util.concurrent.SingleThreadEventExecutor$5.run(SingleThreadEventExecutor.java:906)\n\tat io.netty.util.internal.ThreadExecutorMap$2.run(ThreadExecutorMap.java:74)\n\tat java.base/java.lang.Thread.run(Thread.java:835)\n"
      }
    ],
    "type" : "parsing_exception",
    "reason" : "[match] query does not support [cutoff_frequency]",
    "line" : 1,
    "col" : 120,
    "stack_trace" : "ParsingException[[match] query does not support [cutoff_frequency]]\n\tat org.elasticsearch.index.query.MatchQueryBuilder.fromXContent(MatchQueryBuilder.java:480)\n\tat org.elasticsearch.search.SearchModule.lambda$registerQuery$11(SearchModule.java:813)\n\tat org.elasticsearch.common.xcontent.NamedXContentRegistry.parseNamedObject(NamedXContentRegistry.java:141)\n\tat org.elasticsearch.common.xcontent.support.AbstractXContentParser.namedObject(AbstractXContentParser.java:385)\n\tat org.elasticsearch.index.query.AbstractQueryBuilder.parseInnerQueryBuilder(AbstractQueryBuilder.java:314)\n\tat org.elasticsearch.search.builder.SearchSourceBuilder.parseXContent(SearchSourceBuilder.java:1065)\n\tat org.elasticsearch.rest.action.search.RestSearchAction.parseSearchRequest(RestSearchAction.java:121)\n\tat org.elasticsearch.rest.action.search.RestSearchAction.lambda$prepareRequest$1(RestSearchAction.java:100)\n\tat org.elasticsearch.rest.RestRequest.withContentOrSourceParamParserOrNull(RestRequest.java:449)\n\tat org.elasticsearch.rest.action.search.RestSearchAction.prepareRequest(RestSearchAction.java:99)\n\tat org.elasticsearch.rest.BaseRestHandler.handleRequest(BaseRestHandler.java:92)\n\tat org.elasticsearch.rest.RestController.dispatchRequest(RestController.java:240)\n\tat org.elasticsearch.rest.RestController.tryAllHandlers(RestController.java:344)\n\tat org.elasticsearch.rest.RestController.dispatchRequest(RestController.java:174)\n\tat org.elasticsearch.http.AbstractHttpServerTransport.dispatchRequest(AbstractHttpServerTransport.java:322)\n\tat org.elasticsearch.http.AbstractHttpServerTransport.handleIncomingRequest(AbstractHttpServerTransport.java:372)\n\tat org.elasticsearch.http.AbstractHttpServerTransport.incomingRequest(AbstractHttpServerTransport.java:301)\n\tat org.elasticsearch.http.netty4.Netty4HttpRequestHandler.channelRead0(Netty4HttpRequestHandler.java:69)\n\tat org.elasticsearch.http.netty4.Netty4HttpRequestHandler.channelRead0(Netty4HttpRequestHandler.java:31)\n\tat io.netty.channel.SimpleChannelInboundHandler.channelRead(SimpleChannelInboundHandler.java:105)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat org.elasticsearch.http.netty4.Netty4HttpPipeliningHandler.channelRead(Netty4HttpPipeliningHandler.java:58)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.handler.codec.MessageToMessageDecoder.channelRead(MessageToMessageDecoder.java:102)\n\tat io.netty.handler.codec.MessageToMessageCodec.channelRead(MessageToMessageCodec.java:111)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.handler.codec.MessageToMessageDecoder.channelRead(MessageToMessageDecoder.java:102)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.handler.codec.MessageToMessageDecoder.channelRead(MessageToMessageDecoder.java:102)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.handler.codec.ByteToMessageDecoder.fireChannelRead(ByteToMessageDecoder.java:323)\n\tat io.netty.handler.codec.ByteToMessageDecoder.channelRead(ByteToMessageDecoder.java:297)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.handler.timeout.IdleStateHandler.channelRead(IdleStateHandler.java:287)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.AbstractChannelHandlerContext.fireChannelRead(AbstractChannelHandlerContext.java:352)\n\tat io.netty.channel.DefaultChannelPipeline$HeadContext.channelRead(DefaultChannelPipeline.java:1408)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:374)\n\tat io.netty.channel.AbstractChannelHandlerContext.invokeChannelRead(AbstractChannelHandlerContext.java:360)\n\tat io.netty.channel.DefaultChannelPipeline.fireChannelRead(DefaultChannelPipeline.java:930)\n\tat io.netty.channel.nio.AbstractNioByteChannel$NioByteUnsafe.read(AbstractNioByteChannel.java:163)\n\tat io.netty.channel.nio.NioEventLoop.processSelectedKey(NioEventLoop.java:682)\n\tat io.netty.channel.nio.NioEventLoop.processSelectedKeysPlain(NioEventLoop.java:582)\n\tat io.netty.channel.nio.NioEventLoop.processSelectedKeys(NioEventLoop.java:536)\n\tat io.netty.channel.nio.NioEventLoop.run(NioEventLoop.java:496)\n\tat io.netty.util.concurrent.SingleThreadEventExecutor$5.run(SingleThreadEventExecutor.java:906)\n\tat io.netty.util.internal.ThreadExecutorMap$2.run(ThreadExecutorMap.java:74)\n\tat java.base/java.lang.Thread.run(Thread.java:835)\n"
  },
  "status" : 400
}
	 */
	[SkipVersion(">=8.0.0-SNAPSHOT", "TODO broken in snapshot")]
	public class MatchUsageTests : QueryDslUsageTestsBase
	{
		public MatchUsageTests(ReadOnlyCluster i, EndpointUsage usage) : base(i, usage) { }

		protected override ConditionlessWhen ConditionlessWhen => new ConditionlessWhen<IMatchQuery>(a => a.Match)
		{
			q => q.Query = null,
			q => q.Query = string.Empty,
			q => q.Field = null
		};

		protected override QueryContainer QueryInitializer => new MatchQuery
		{
			Field = Field<Project>(p => p.Description),
			Analyzer = "standard",
			Boost = 1.1,
			Name = "named_query",
			Query = "hello world",
			Fuzziness = Fuzziness.AutoLength(3, 6),
			FuzzyTranspositions = true,
			MinimumShouldMatch = 2,
			FuzzyRewrite = MultiTermQueryRewrite.TopTermsBlendedFreqs(10),
			Lenient = true,
			Operator = Operator.Or,
			AutoGenerateSynonymsPhraseQuery = false
		};

		protected override object QueryJson => new
		{
			match = new
			{
				description = new
				{
					_name = "named_query",
					boost = 1.1,
					query = "hello world",
					analyzer = "standard",
					fuzzy_rewrite = "top_terms_blended_freqs_10",
					fuzziness = "AUTO:3,6",
					fuzzy_transpositions = true,
					lenient = true,
					minimum_should_match = 2,
					@operator = "or",
					auto_generate_synonyms_phrase_query = false
				}
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Match(c => c
				.Field(p => p.Description)
				.Analyzer("standard")
				.Boost(1.1)
				.Query("hello world")
				.Fuzziness(Fuzziness.AutoLength(3, 6))
				.Lenient()
				.FuzzyTranspositions()
				.MinimumShouldMatch(2)
				.Operator(Operator.Or)
				.FuzzyRewrite(MultiTermQueryRewrite.TopTermsBlendedFreqs(10))
				.Name("named_query")
				.AutoGenerateSynonymsPhraseQuery(false)
			);

		//hide
		[U] public void DeserializeShortForm()
		{
			using var stream = new MemoryStream(ShortFormQuery);
			var query = Client.RequestResponseSerializer.Deserialize<IMatchQuery>(stream);
			query.Should().NotBeNull();
			query.Field.Should().Be(new Field("description"));
			query.Query.Should().Be("project description");
		}
	}
}
