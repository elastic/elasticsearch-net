// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using BenchmarkDotNet.Attributes;
using Nest;
using Tests.Benchmarking.Framework;
using Tests.Core.Client;
using Tests.Domain;

namespace Tests.Benchmarking
{
	[BenchmarkConfig]
	public class ExpressionResolverBenchmarkTests
	{

		[GlobalSetup]
		public void Setup() => Client = TestClient.DefaultInMemoryClient;

		public IElasticClient Client { get; set; }

		[Benchmark(Description = "Boxed Expression", OperationsPerInvoke = 1000)]
		public void ResolveBoxedExpressionToString() => ResolveBoxedExpressionToStringImp<Project>(p => p.Suggest.Weight);

		[Benchmark(Description = "Unboxed Expression", OperationsPerInvoke = 1000)]
		public void ResolvedUnboxedExpressionToString() => ResolvedUnboxedExpressionToStringImp<Project, int?>(p => p.Suggest.Weight);

		[Benchmark(Description = "Field Resolver", OperationsPerInvoke = 1000)]
		public void FieldResolver() => FieldResolverImp<Project>(p => p.Suggest.Weight);

		[Benchmark(Description = "Field Resolver Unboxed", OperationsPerInvoke = 1000)]
		public void FieldResolverUnboxed() => FieldResolverUnboxedImp<Project, int?>(p => p.Suggest.Weight);

		// ReSharper disable once UnusedMethodReturnValue.Local
		private string ResolveBoxedExpressionToStringImp<T>(Expression<Func<T, object>> expression) => expression.ToString();

		// ReSharper disable once UnusedMethodReturnValue.Local
		private string ResolvedUnboxedExpressionToStringImp<T, TValue>(Expression<Func<T, TValue>> expression) => expression.ToString();

		// ReSharper disable once UnusedMethodReturnValue.Local
		private string FieldResolverImp<T>(Expression<Func<T, object>> expression) => Client.Infer.Field(expression);

		// ReSharper disable once UnusedMethodReturnValue.Local
		private string FieldResolverUnboxedImp<T, TValue>(Expression<Func<T, TValue>> expression) => Client.Infer.Field(expression);
	}
}
