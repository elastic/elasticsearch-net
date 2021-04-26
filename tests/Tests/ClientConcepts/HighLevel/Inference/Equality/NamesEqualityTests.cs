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

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;

namespace Tests.ClientConcepts.HighLevel.Inference.Equality
{
	public class NamesEqualityTests
	{
		[U] public void Eq()
		{
			Names types = "foo,bar";
			Names[] equal = { "foo,bar", "bar,foo", "foo,  bar", "bar,  foo   " };
			foreach (var t in equal)
			{
				(t == types).ShouldBeTrue(t);
				t.Should().Be(types);
			}
		}

		[U] public void NotEq()
		{
			Names types = "foo,bar";
			Names[] notEqual = { "foo,bar,x", "foo" };
			foreach (var t in notEqual)
			{
				(t != types).ShouldBeTrue(t);
				t.Should().NotBe(types);
			}
		}

		[U] public void Null()
		{
			Names types = "foo,bar";
			(types == null).Should().BeFalse();
			(null == types).Should().BeFalse();
		}
	}
}
