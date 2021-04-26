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
using Tests.Domain;

namespace Tests.ClientConcepts.HighLevel.Inference.Equality
{
	public class RelationNameEqualityTests
	{
		[U] public void Eq()
		{
			RelationName name = "foo";
			RelationName[] equal = { "foo" };
			foreach (var t in equal)
			{
				(t == name).ShouldBeTrue(t);
				t.Should().Be(name);
			}
		}

		[U] public void NotEq()
		{
			RelationName name = "foo";
			RelationName[] notEqual = { "bar", "foo  ", "  foo   ", "x", "", "   ", typeof(Project) };
			foreach (var t in notEqual)
			{
				(t != name).ShouldBeTrue(t);
				t.Should().NotBe(name);
			}
		}

		[U] public void TypedEq()
		{
			RelationName t1 = typeof(Project), t2 = typeof(Project);
			(t1 == t2).ShouldBeTrue(t2);
		}

		[U] public void TypedNotEq()
		{
			RelationName t1 = typeof(Project), t2 = typeof(CommitActivity);
			(t1 != t2).ShouldBeTrue(t2);
		}

		[U] public void Null()
		{
			RelationName value = "foo";
			(value == null).Should().BeFalse();
			(null == value).Should().BeFalse();
		}
	}
}
