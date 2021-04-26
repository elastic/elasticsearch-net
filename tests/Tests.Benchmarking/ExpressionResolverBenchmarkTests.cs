/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
