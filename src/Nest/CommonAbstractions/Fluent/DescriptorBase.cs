using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Nest
{
	public abstract class DescriptorBase<TDescriptor, TInterface>
		where TDescriptor : DescriptorBase<TDescriptor, TInterface>, TInterface
		where TInterface : class
	{
		protected TDescriptor Assign(Action<TInterface> assigner) => Fluent.Assign((TDescriptor)this, assigner);

		/// <summary>
		/// Hides the <see cref="Equals"/> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj) => base.Equals(obj);

		/// <summary>
		/// Hides the <see cref="GetHashCode"/> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode() => base.GetHashCode();

		/// <summary>
		/// Hides the <see cref="ToString"/> method.
		/// </summary>
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString() => base.ToString();
	}
}
