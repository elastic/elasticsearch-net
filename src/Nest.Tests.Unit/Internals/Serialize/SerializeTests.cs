using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using Newtonsoft.Json;

namespace Nest.Tests.Unit.Internals.Serialize
{
	[TestFixture]
	public class SerializeTests : BaseJsonTests
	{
		public class SimpleClass
		{
			public int Id { get; set; }
			public string Name { get; set; }
		}

		public class ClassWithCollections
		{
			public int Id { get; set; }
			private ICollection<SimpleClass> _productVariants;
			public virtual ICollection<SimpleClass> ProductVariants
			{
				get { return _productVariants ?? (_productVariants = new List<SimpleClass>()); }
				set { _productVariants = value; }
			}
		}

		// <summary>
		/// https://github.com/Mpdreamz/NEST/issues/204
		/// 
		/// Reported problems with ICollections
		/// </summary>
		[Test]
		public void ClassWithCollectionSerializes()
		{
			var col = new ClassWithCollections
			{
				Id = 2,
				ProductVariants = new List<SimpleClass>
				{
					new SimpleClass {Id = 1, Name = "class 1"},
					new SimpleClass {Id = 1, Name = "class 1"},
				}
			};
			var json = this._client.Serialize(col);
			this.JsonEquals(json, MethodInfo.GetCurrentMethod());
		}
	}
}
