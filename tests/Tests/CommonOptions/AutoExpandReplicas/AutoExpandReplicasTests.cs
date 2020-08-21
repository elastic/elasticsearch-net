// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
