namespace Nest
{
	public interface IDeleteLifecycleAction : ILifecycleAction { }

	public class DeleteLifecycleAction : IDeleteLifecycleAction
	{
	}

	public class DeleteLifecycleActionDescriptor : DescriptorBase<DeleteLifecycleActionDescriptor, IDeleteLifecycleAction>, IDeleteLifecycleAction
	{
	}
}
