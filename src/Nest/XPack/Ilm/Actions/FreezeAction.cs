namespace Nest
{
	public interface IFreezeLifecycleAction : ILifecycleAction { }

	public class FreezeLifecycleAction : IFreezeLifecycleAction
	{
	}

	public class FreezeLifecycleActionDescriptor : DescriptorBase<FreezeLifecycleActionDescriptor, IFreezeLifecycleAction>, IFreezeLifecycleAction
	{
	}
}
