using System.Reflection;
using BenchmarkDotNet.Attributes;
using Nest;
using Tests.Framework.Benchmarks;
using Tests.Framework.MockData;

namespace Tests.ClientConcepts.HighLevel.Caching
{
	[BenchmarkConfig(100)]
	public class FieldResolverBenchmarkTests
	{
		private FieldResolver _expressionResolver;
		private FieldResolver _propertyResolver;
		private FieldResolver _stringResolver;
		private NoncachingFieldResolver _nonCachingExpressionResolver;
		private NoncachingFieldResolver _nonCachingPropertyResolver;
		private NoncachingFieldResolver _nonCachingStringResolver;
		private static readonly Field StringField = "Name";
		private static readonly Field PropertyField = typeof(Project).GetProperty(nameof(Project.Name));
		private static readonly Field InferredField = Infer.Field<Project>(p => p.Name);

#if NET45
		[Setup]
#else
		[GlobalSetup]
#endif
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
		public string NonCachedFieldUsingExpression()
		{
			return _nonCachingExpressionResolver.Resolve(InferredField);
		}

		[Benchmark]
		public string CachedFieldUsingExpression()
		{
			return _expressionResolver.Resolve(InferredField);
		}

		[Benchmark]
		public string NonCachedFieldUsingPropertyInfo()
		{
			return _nonCachingPropertyResolver.Resolve(PropertyField);
		}

		[Benchmark]
		public string CachedFieldUsingPropertyInfo()
		{
			return _propertyResolver.Resolve(PropertyField);
		}

		[Benchmark(Baseline = true)]
		public string NonCachedFieldUsingString()
		{
			return _nonCachingStringResolver.Resolve(StringField);
		}

		[Benchmark]
		public string CachedFieldUsingString()
		{
			return _stringResolver.Resolve(StringField);
		}
	}
}
