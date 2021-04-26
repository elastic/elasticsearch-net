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
	public class FieldsEqualityTests
	{
		[U] public void Eq()
		{
			Fields name = "foo, bar";
			Fields[] equal = { "foo, bar", "  foo,   bar", "bar, foo", "   bar  ,   foo  " };
			foreach (var t in equal)
			{
				(t == name).ShouldBeTrue(t);
				t.Should().BeEquivalentTo(name);
			}
		}

		[U] public void NotEq()
		{
			Fields name = "foo, bar";
			Fields[] notEqual = { "bar", "foo, bar, x", "", "   ", " foo ", Infer.Field<Project>(p => p.Name) };
			foreach (var t in notEqual)
			{
				(t != name).ShouldBeTrue(t);
				if (t != null) t.Should().NotBeEquivalentTo(name);
			}
		}

		[U] public void TypedEq()
		{
			Fields t1 = Infer.Field<Project>(p => p.Name), t2 = Infer.Field<Project>(p => p.Name);
			(t1 == t2).ShouldBeTrue(t2);
			t1.Should().BeEquivalentTo(t2);

			t1 = Infer.Field<Project>(p => p.Name, 1.1);
			t2 = Infer.Field<Project>(p => p.Name, 1.1);
			(t1 == t2).ShouldBeTrue(t2);
			t1.Should().BeEquivalentTo(t2);

			// boost factor is not taken into account when comparing fields
			t1 = Infer.Field<Project>(p => p.Name, 2.1);
			t2 = Infer.Field<Project>(p => p.Name, 1.1);
			(t1 == t2).ShouldBeTrue(t2);
			t1.Should().BeEquivalentTo(t2);

			// boost factor is not taken into account when comparing fields
			t1 = Infer.Field<Project>(p => p.Name);
			t2 = Infer.Field<Project>(p => p.Name, 1.1);
			(t1 == t2).ShouldBeTrue(t2);
			t1.Should().BeEquivalentTo(t2);
		}

		[U] public void TypedNotEq()
		{
			Fields t1 = Infer.Field<Developer>(p => p.Id), t2 = Infer.Field<CommitActivity>(p => p.Id);
			(t1 != t2).ShouldBeTrue(t2);
			t1.Should().NotBeEquivalentTo(t2);

			t1 = Infer.Field<Project>(p => p.Name, 1.0);
			t2 = Infer.Field<Project>(p => p.LocationPoint, 1.0);
			(t1 != t2).ShouldBeTrue(t2);
			t1.Should().NotBeEquivalentTo(t2);
		}

		[U] public void ReflectedEq()
		{
			Fields t1 = typeof(Project).GetProperty(nameof(Project.Name)),
				t2 = typeof(Project).GetProperty(nameof(Project.Name));
			(t1 == t2).ShouldBeTrue(t2);
			t1.Should().BeEquivalentTo(t2);
		}

		[U] public void ReflectedNotEq()
		{
			Fields t1 = typeof(CommitActivity).GetProperty(nameof(CommitActivity.Id)),
				t2 = typeof(Developer).GetProperty(nameof(Developer.Id));
			(t1 != t2).ShouldBeTrue(t2);
			t1.Should().NotBeEquivalentTo(t2);
		}

		[U] public void Null()
		{
			Fields value = "foo";
			(value == null).Should().BeFalse();
			(null == value).Should().BeFalse();
		}
	}
}
