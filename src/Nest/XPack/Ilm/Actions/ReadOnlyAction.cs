namespace Nest
{
	public interface IReadOnlyAction : ILifecycleAction { }

	public class ReadOnlyAction : LifecycleActionBase, IReadOnlyAction
	{
		public ReadOnlyAction() : base("readonly"){ }
	}

	public class ReadOnlyActionDescriptor : LifecycleActionDescriptorBase<ReadOnlyActionDescriptor, IReadOnlyAction>, IReadOnlyAction
	{
		public ReadOnlyActionDescriptor() : base("readonly") { }
	}
}
