using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Tests.Framework;
using Nest_5_2_0;
using Xunit;

namespace Tests.CommonOptions.AutoExpandReplicas
{
	public class AutoExpandReplicasTests
	{
		[U]
		public void ImplicitConversionFromNullString()
		{
			string nullString = null;
			Nest_5_2_0.AutoExpandReplicas autoExpandReplicas = nullString;
			autoExpandReplicas.Should().BeNull();
		}

		[U]
		public void ImplicitConversionFromMinAndMaxString()
		{
			string minAndMax = "0-5";
			Nest_5_2_0.AutoExpandReplicas autoExpandReplicas = minAndMax;
			autoExpandReplicas.Should().NotBeNull();
			autoExpandReplicas.Enabled.Should().BeTrue();
			autoExpandReplicas.MinReplicas.Should().Be(0);
			autoExpandReplicas.MaxReplicas.Match(i => i.Should().Be(5), s => Assert.True(false, "expecting a match on integer"));

			autoExpandReplicas.ToString().Should().Be(minAndMax);
		}

		[U]
		public void ImplicitConversionFromMinAndAllString()
		{
			string minAndMax = "0-all";
			Nest_5_2_0.AutoExpandReplicas autoExpandReplicas = minAndMax;
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
			var autoExpandReplicas = Nest_5_2_0.AutoExpandReplicas.Create(2, 3);
			autoExpandReplicas.Should().NotBeNull();
			autoExpandReplicas.Enabled.Should().BeTrue();
			autoExpandReplicas.MinReplicas.Should().Be(2);
			autoExpandReplicas.MaxReplicas.Match(i => i.Should().Be(3), s => Assert.True(false, "expecting a match on integer"));

			autoExpandReplicas.ToString().Should().Be("2-3");
		}

		[U]
		public void CreateWithMinAndAll()
		{
			var autoExpandReplicas = Nest_5_2_0.AutoExpandReplicas.Create(0);
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
			var autoExpandReplicas = Nest_5_2_0.AutoExpandReplicas.Disabled;
			autoExpandReplicas.Should().NotBeNull();
			autoExpandReplicas.Enabled.Should().BeFalse();
			autoExpandReplicas.MinReplicas.Should().NotHaveValue();
			autoExpandReplicas.MaxReplicas.Should().BeNull();

			autoExpandReplicas.ToString().Should().Be("false");
		}

		[U]
		public void MinMustBeEqualOrLessThanMax() => Assert.Throws<ArgumentException>(() => Nest_5_2_0.AutoExpandReplicas.Create(2,1));

		[U]
		public void MinMustBeGreaterThanOrEqualToZero() => Assert.Throws<ArgumentException>(() => Nest_5_2_0.AutoExpandReplicas.Create(-1));

		[U]
		public void MinMustBeAnInteger() => Assert.Throws<FormatException>(() => Nest_5_2_0.AutoExpandReplicas.Create("all-all"));

		[U]
		public void MaxMustBeAllOrAnInteger() => Assert.Throws<FormatException>(() => Nest_5_2_0.AutoExpandReplicas.Create("2-boo"));
	}
}
