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
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Domain;

namespace Tests.ClientConcepts.HighLevel.Inference.Equality
{
	public class IdEqualityTests
	{
		[U] public void Eq()
		{
			Id types = "foo";
			Id[] equal = { "foo" };
			foreach (var t in equal)
			{
				(t == types).ShouldBeTrue(t);
				t.Should().Be(types);
			}

			Id l1 = 2, l2 = 2;
			(l1 == l2).ShouldBeTrue(l2);
			(l1 == 2).ShouldBeTrue(l1);
			(l1 == "2").ShouldBeTrue(l1);
			l1.Should().Be(l2);
			l1.Should().Be("2");
			l1.Should().Be(2);

			var g = Guid.NewGuid();
			l1 = g;
			l2 = g;
			(l1 == l2).ShouldBeTrue(l2);
			l1.Should().Be(l2);

			var project = new Project { Name = "x" };
			l1 = Id.From(project);
			l2 = Id.From(project);
			(l1 == l2).ShouldBeTrue(l2);
			l1.Should().Be(l2);
		}

		[U] public void NotEq()
		{
			Id types = "foo";
			Id[] notEqual = { "bar", "", "foo  " };
			foreach (var t in notEqual)
			{
				(t != types).ShouldBeTrue(t);
				t.Should().NotBe(types);
			}
			Id l1 = 2, l2 = 3;
			(l1 != l2).ShouldBeTrue(l2);
			(l1 != 3).ShouldBeTrue(l1);
			(l1 != "3").ShouldBeTrue(l1);
			l1.Should().NotBe(l2);
			l1.Should().NotBe(3);
			l1.Should().NotBe("3");

			l1 = Guid.NewGuid();
			l2 = Guid.NewGuid();
			(l1 != l2).ShouldBeTrue(l2);
			l1.Should().NotBe(l2);

			//when comparing objects we can only do referential equality. Project does not have its own Equals implementation.
			l1 = Id.From(new Project { Name = "x" });
			l2 = Id.From(new Project { Name = "x" });
			(l1 != l2).ShouldBeTrue(l2);
			l1.Should().NotBe(l2);
		}

		[U] public void Null()
		{
			Id value = "foo";
			(value == null).Should().BeFalse();
			(null == value).Should().BeFalse();
		}
	}
}
