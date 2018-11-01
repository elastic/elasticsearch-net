using BenchmarkDotNet.Attributes;
using Nest;
using Tests.Benchmarking.Framework;
using Tests.Domain;

namespace Tests.Benchmarking
{
	[BenchmarkConfig(100)]
	public class FieldResolverBenchmarkTests
	{
		private static readonly Field InferredField = Infer.Field<Project>(p => p.Name);
		private static readonly Field PropertyField = typeof(Project).GetProperty(nameof(Project.Name));
		private static readonly Field StringField = "Name";
		private FieldResolver _expressionResolver;
		private NoncachingFieldResolver _nonCachingExpressionResolver;
		private NoncachingFieldResolver _nonCachingPropertyResolver;
		private NoncachingFieldResolver _nonCachingStringResolver;
		private FieldResolver _propertyResolver;
		private FieldResolver _stringResolver;

		[Benchmark]
		public string CachedFieldUsingExpression() => _expressionResolver.Resolve(InferredField);

		[Benchmark]
		public string CachedFieldUsingPropertyInfo() => _propertyResolver.Resolve(PropertyField);

		[Benchmark]
		public string CachedFieldUsingString() => _stringResolver.Resolve(StringField);

		[Benchmark]
		public string NonCachedFieldUsingExpression() => _nonCachingExpressionResolver.Resolve(InferredField);

		[Benchmark]
		public string NonCachedFieldUsingPropertyInfo() => _nonCachingPropertyResolver.Resolve(PropertyField);

		[Benchmark(Baseline = true)]
		public string NonCachedFieldUsingString() => _nonCachingStringResolver.Resolve(StringField);

		[GlobalSetup]
		public void Setup()
		{
			_expressionResolver = new FieldResolver(new ConnectionSettings());
			_propertyResolver = new FieldResolver(new ConnectionSettings());
			_stringResolver = new FieldResolver(new ConnectionSettings());
			_nonCachingExpressionResolver = new NoncachingFieldResolver(new ConnectionSettings());
			_nonCachingPropertyResolver = new NoncachingFieldResolver(new ConnectionSettings());
			_nonCachingStringResolver = new NoncachingFieldResolver(new ConnectionSettings());
		}
	}
}
