using BenchmarkDotNet.Attributes;
using Nest;
using Tests.Benchmarking.Framework;
using Tests.Domain;

namespace Tests.Benchmarking
{
	[BenchmarkConfig(100)]
	public class PropertyNameResolverBenchmarkTests
	{
		private static readonly PropertyName InferredPropertyName = Infer.Property<Project>(p => p.Name);
		private static readonly PropertyName PropertyInfoPropertyName = typeof(Project).GetProperty(nameof(Project.Name));
		private static readonly PropertyName StringPropertyName = "Name";
		private FieldResolver _expressionResolver;
		private NoncachingFieldResolver _nonCachingExpressionResolver;
		private NoncachingFieldResolver _nonCachingPropertyResolver;
		private NoncachingFieldResolver _nonCachingStringResolver;
		private FieldResolver _propertyResolver;
		private FieldResolver _stringResolver;

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

		[Benchmark]
		public string NonCachedPropertyUsingExpression() => _nonCachingExpressionResolver.Resolve(InferredPropertyName);

		[Benchmark]
		public string CachedPropertyUsingExpression() => _expressionResolver.Resolve(InferredPropertyName);

		[Benchmark]
		public string NonCachedPropertyUsingPropertyInfo() => _nonCachingPropertyResolver.Resolve(PropertyInfoPropertyName);

		[Benchmark]
		public string CachedPropertyUsingPropertyInfo() => _propertyResolver.Resolve(PropertyInfoPropertyName);

		[Benchmark(Baseline = true)]
		public string NonCachedPropertyUsingString() => _nonCachingStringResolver.Resolve(StringPropertyName);

		[Benchmark]
		public string CachedPropertyUsingString() => _stringResolver.Resolve(StringPropertyName);
	}
}
