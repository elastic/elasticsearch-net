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
using Nest;

namespace Tests.Analysis.Normalizers
{
	using FuncTokenizer = Func<string, NormalizersDescriptor, IPromise<INormalizers>>;

	public class NormalizerTests
	{
		public class CustomTests : NormalizerAssertionBase<CustomTests>
		{
			public override FuncTokenizer Fluent => (n, an) => an
				.Custom("myCustom", a => a
					.Filters("lowercase", "asciifolding")
				);

			public override INormalizer Initializer => new CustomNormalizer
			{
				Filter = new[] { "lowercase", "asciifolding" },
			};

			public override object Json => new
			{
				type = "custom",
				filter = new[] { "lowercase", "asciifolding" },
			};

			public override string Name => "myCustom";
		}
	}
}
