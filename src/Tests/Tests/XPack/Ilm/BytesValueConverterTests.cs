using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;

namespace Tests.XPack.Ilm
{
	public class BytesValueConverterTests
	{
		[U] public void Test()
		{
			BytesValueConverter.ToBytes("1").Should().Be(1L);
			BytesValueConverter.ToBytes("1b").Should().Be(1L);

			BytesValueConverter.ToBytes("1kb").Should().Be(1024);
			BytesValueConverter.ToBytes("1k").Should().Be(1024);

			BytesValueConverter.ToBytes("1mb").Should().Be(1024 * 1024);
			BytesValueConverter.ToBytes("1m").Should().Be(1024 * 1024);

			BytesValueConverter.ToBytes("1gb").Should().Be(1024 * 1024 * 1024);
			BytesValueConverter.ToBytes("1g").Should().Be(1024 * 1024 * 1024);

			BytesValueConverter.ToBytes("1tb").Should().Be(1024L * 1024 * 1024 * 1024);
			BytesValueConverter.ToBytes("1t").Should().Be(1024L * 1024 * 1024 * 1024);

			BytesValueConverter.ToBytes("1pb").Should().Be(1024L * 1024 * 1024 * 1024 * 1024);
			BytesValueConverter.ToBytes("1p").Should().Be(1024L * 1024 * 1024 * 1024 * 1024);
		}

		[U] public void RolloverLifecycleActionTest()
		{
#pragma warning disable 618
			var rolloverLifecycleAction = new RolloverLifecycleAction();

			rolloverLifecycleAction.MaximumSize = 1;
			rolloverLifecycleAction.MaximumSizeAsString.Should().Be("1b");
			rolloverLifecycleAction.MaximumSize.Should().Be(1);

			rolloverLifecycleAction = new RolloverLifecycleAction();
			rolloverLifecycleAction.MaximumSize = 1024;
			rolloverLifecycleAction.MaximumSizeAsString.Should().Be("1024b");
			rolloverLifecycleAction.MaximumSize.Should().Be(1024);

			rolloverLifecycleAction = new RolloverLifecycleAction();
			rolloverLifecycleAction.MaximumSizeAsString = "1gb";
			rolloverLifecycleAction.MaximumSize.Should().Be(1024 * 1024 * 1024);
#pragma warning restore 618
		}

		[U] public void RolloverLifecycleActionDescriptorTest()
		{
#pragma warning disable 618
			var rolloverLifecycleActionDescriptor = new RolloverLifecycleActionDescriptor();
			rolloverLifecycleActionDescriptor.MaximumSize(1);
			((IRolloverLifecycleAction)rolloverLifecycleActionDescriptor).MaximumSize.Should().Be(1);
			((IRolloverLifecycleAction)rolloverLifecycleActionDescriptor).MaximumSizeAsString.Should().Be("1b");

			rolloverLifecycleActionDescriptor = new RolloverLifecycleActionDescriptor();
			rolloverLifecycleActionDescriptor.MaximumSizeAsString("1gb");
			((IRolloverLifecycleAction)rolloverLifecycleActionDescriptor).MaximumSize.Should().Be(1024 * 1024 * 1024);
			((IRolloverLifecycleAction)rolloverLifecycleActionDescriptor).MaximumSizeAsString.Should().Be("1gb");
#pragma warning restore 618
		}
	}

}
