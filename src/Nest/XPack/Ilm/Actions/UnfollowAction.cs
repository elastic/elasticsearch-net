namespace Nest
{
	public interface IUnfollowAction : ILifecycleAction { }

	public class UnfollowAction : LifecycleActionBase, IUnfollowAction
	{
		public UnfollowAction() : base("unfollow"){ }
	}

	public class UnfollowActionDescriptor : LifecycleActionDescriptorBase<UnfollowActionDescriptor, IUnfollowAction>, IUnfollowAction
	{
		public UnfollowActionDescriptor() : base("unfollow") { }
	}
}
