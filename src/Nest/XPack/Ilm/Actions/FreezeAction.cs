namespace Nest
{
	public interface IFreezeAction : ILifecycleAction { }

	public class FreezeAction : LifecycleActionBase, IFreezeAction
	{
		public FreezeAction() : base("freeze"){ }
	}

	public class FreezeActionDescriptor : LifecycleActionDescriptorBase<FreezeActionDescriptor, IFreezeAction>, IFreezeAction
	{
		public FreezeActionDescriptor() : base("freeze") { }
	}
}
