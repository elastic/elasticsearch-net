using System.Reflection;
using BenchmarkDotNet.Attributes;
using Nest;
using Tests.Framework.Benchmarks;
using Tests.Framework.MockData;

namespace Tests.ClientConcepts.HighLevel.Caching
{
	[BenchmarkConfig(100)]
	public class PropertyNameResolverBenchmarkTests
	{
		private FieldResolver _expressionResolver;
		private FieldResolver _propertyResolver;
		private FieldResolver _stringResolver;
		private NoncachingFieldResolver _nonCachingExpressionResolver;
		private NoncachingFieldResolver _nonCachingPropertyResolver;
		private NoncachingFieldResolver _nonCachingStringResolver;
		private static readonly PropertyName InferredPropertyName = Infer.Property<Project>(p => p.Name);
		private static readonly PropertyName PropertyInfoPropertyName = typeof(Project).GetProperty(nameof(Project.Name));
		private static readonly PropertyName StringPropertyName = "Name";

#pragma warning disable 618
		[Setup]
#pragma warning restore 618
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
		public string NonCachedPropertyUsingExpression()
		{
			return _nonCachingExpressionResolver.Resolve(InferredPropertyName);
		}

		[Benchmark]
		public string CachedPropertyUsingExpression()
		{
			return _expressionResolver.Resolve(InferredPropertyName);
		}

		[Benchmark]
		public string NonCachedPropertyUsingPropertyInfo()
		{
			return _nonCachingPropertyResolver.Resolve(PropertyInfoPropertyName);
		}

		[Benchmark]
		public string CachedPropertyUsingPropertyInfo()
		{
			return _propertyResolver.Resolve(PropertyInfoPropertyName);
		}

		[Benchmark(Baseline = true)]
		public string NonCachedPropertyUsingString()
		{
			return _nonCachingStringResolver.Resolve(StringPropertyName);
		}

		[Benchmark]
		public string CachedPropertyUsingString()
		{
			return _stringResolver.Resolve(StringPropertyName);
		}
	}
}
