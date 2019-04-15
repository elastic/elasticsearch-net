namespace Nest
{
	public interface IReadOnlyLifecycleAction : ILifecycleAction { }

	public class ReadOnlyLifecycleAction : IReadOnlyLifecycleAction
	{
	}

	public class ReadOnlyLifecycleActionDescriptor : DescriptorBase<ReadOnlyLifecycleActionDescriptor, IReadOnlyLifecycleAction>, IReadOnlyLifecycleAction
	{
	}
}
