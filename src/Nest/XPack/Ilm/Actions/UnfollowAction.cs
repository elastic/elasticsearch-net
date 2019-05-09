namespace Nest
{
	/// <summary>
	/// This action turns a ccr follower index into a regular index. This can be desired when moving follower indices into the
	/// next phase. Also certain actions like shrink and rollover can then be performed safely on follower indices.
	/// </summary>
	public interface IUnfollowLifecycleAction : ILifecycleAction { }

	public class UnfollowLifecycleAction : IUnfollowLifecycleAction { }

	public class UnfollowLifecycleActionDescriptor
		: DescriptorBase<UnfollowLifecycleActionDescriptor, IUnfollowLifecycleAction>, IUnfollowLifecycleAction { }
}
