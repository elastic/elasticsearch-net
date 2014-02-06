using System;

namespace Nest
{
	/// <summary>
	/// DescriptorFor is a marker to rename unintuitive generated elasticsearch operation names
	/// </summary>
	[AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
	public class DescriptorForAttribute : Attribute
	{
// ReSharper disable once UnusedParameter.Local
		public DescriptorForAttribute (string operation)
		{
		}
	}
}
