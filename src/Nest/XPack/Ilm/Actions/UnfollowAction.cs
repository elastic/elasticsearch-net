namespace Nest
{
	public interface IUnfollowLifecycleAction : ILifecycleAction { }

	public class UnfollowLifecycleAction : IUnfollowLifecycleAction
	{
	}

	public class UnfollowLifecycleActionDescriptor : DescriptorBase<UnfollowLifecycleActionDescriptor, IUnfollowLifecycleAction>, IUnfollowLifecycleAction
	{
	}
}
