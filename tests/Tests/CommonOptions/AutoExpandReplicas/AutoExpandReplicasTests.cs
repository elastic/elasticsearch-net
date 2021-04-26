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
using Xunit;

namespace Tests.CommonOptions.AutoExpandReplicas
{
	public class AutoExpandReplicasTests
	{
		[U]
		public void ImplicitConversionFromNullString()
		{
			string nullString = null;
			Nest.AutoExpandReplicas autoExpandReplicas = nullString;
			autoExpandReplicas.Should().BeNull();
		}

		[U]
		public void ImplicitConversionFromMinAndMaxString()
		{
			var minAndMax = "0-5";
			Nest.AutoExpandReplicas autoExpandReplicas = minAndMax;
			autoExpandReplicas.Should().NotBeNull();
			autoExpandReplicas.Enabled.Should().BeTrue();
			autoExpandReplicas.MinReplicas.Should().Be(0);
			autoExpandReplicas.MaxReplicas.Match(
				i => i.Should().Be(5),
				s => Assert.True(false, "expecting a match on integer"));

			autoExpandReplicas.ToString().Should().Be(minAndMax);
		}

		[U]
		public void ImplicitConversionFromMinAndAllString()
		{
			var minAndMax = "0-all";
			Nest.AutoExpandReplicas autoExpandReplicas = minAndMax;
			autoExpandReplicas.Should().NotBeNull();
			autoExpandReplicas.Enabled.Should().BeTrue();
			autoExpandReplicas.MinReplicas.Should().Be(0);
			autoExpandReplicas.MaxReplicas.Match(
				i => Assert.True(false, "expecting a match on string"),
				s => s.Should().Be("all"));

			autoExpandReplicas.ToString().Should().Be(minAndMax);
		}

		[U]
		public void CreateWithMinAndMax()
		{
			var autoExpandReplicas = Nest.AutoExpandReplicas.Create(2, 3);
			autoExpandReplicas.Should().NotBeNull();
			autoExpandReplicas.Enabled.Should().BeTrue();
			autoExpandReplicas.MinReplicas.Should().Be(2);
			autoExpandReplicas.MaxReplicas.Match(
				i => i.Should().Be(3),
				s => Assert.True(false, "expecting a match on integer"));

			autoExpandReplicas.ToString().Should().Be("2-3");
		}

		[U]
		public void CreateWithMinAndAll()
		{
			var autoExpandReplicas = Nest.AutoExpandReplicas.Create(0);
			autoExpandReplicas.Should().NotBeNull();
			autoExpandReplicas.Enabled.Should().BeTrue();
			autoExpandReplicas.MinReplicas.Should().Be(0);
			autoExpandReplicas.MaxReplicas.Match(
				i => Assert.True(false, "expecting a match on string"),
				s => s.Should().Be("all"));

			autoExpandReplicas.ToString().Should().Be("0-all");
		}

		[U]
		public void CreateWithFalse()
		{
			var autoExpandReplicas = Nest.AutoExpandReplicas.Create("false");
			autoExpandReplicas.Should().NotBeNull();
			autoExpandReplicas.Enabled.Should().BeFalse();
			autoExpandReplicas.MinReplicas.Should().BeNull();
			autoExpandReplicas.MaxReplicas.Should().BeNull();
			autoExpandReplicas.ToString().Should().Be("false");
		}

		[U]
		public void Disabled()
		{
			var autoExpandReplicas = Nest.AutoExpandReplicas.Disabled;
			autoExpandReplicas.Should().NotBeNull();
			autoExpandReplicas.Enabled.Should().BeFalse();
			autoExpandReplicas.MinReplicas.Should().NotHaveValue();
			autoExpandReplicas.MaxReplicas.Should().BeNull();

			autoExpandReplicas.ToString().Should().Be("false");
		}

		[U]
		public void MinMustBeEqualOrLessThanMax() =>
			Assert.Throws<ArgumentException>(() => Nest.AutoExpandReplicas.Create(2, 1));

		[U]
		public void MinMustBeGreaterThanOrEqualToZero() =>
			Assert.Throws<ArgumentException>(() => Nest.AutoExpandReplicas.Create(-1));

		[U]
		public void MinMustBeAnInteger() =>
			Assert.Throws<FormatException>(() => Nest.AutoExpandReplicas.Create("all-all"));

		[U]
		public void MaxMustBeAllOrAnInteger() =>
			Assert.Throws<FormatException>(() => Nest.AutoExpandReplicas.Create("2-boo"));
	}
}
