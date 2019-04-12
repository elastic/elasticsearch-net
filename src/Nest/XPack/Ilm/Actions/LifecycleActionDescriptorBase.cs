using System;

namespace Nest
{
	public abstract class LifecycleActionDescriptorBase<TDescriptor, TInterface>
		: DescriptorBase<TDescriptor, TInterface>, ILifecycleAction
		where TDescriptor : DescriptorBase<TDescriptor, TInterface>, TInterface
		where TInterface : class, ILifecycleAction
	{
		private string _name;

		protected LifecycleActionDescriptorBase(string name)
		{
			if (name == null) throw new ArgumentNullException(nameof(name));
			if (name.Length == 0) throw new ArgumentException("cannot be empty");

			_name = name;
		}

		string ILifecycleAction.Name
		{
			get => _name;
			set => _name = value;
		}
	}
}
