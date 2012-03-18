using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Nest
{
	internal class PropertyDescriptor<T>
	{
		internal Expression<Func<T, object>> Expression { get; set; }
		internal IElasticProperty Property { get; set; }
	}

	public class Map<T> where T :  class
	{
		private IElasticType ElasticType { get; set; }
		private List<PropertyDescriptor<T>> PropertyDescriptors { get; set; }
		private Map()
		{
			this.ElasticType = null;
			this.PropertyDescriptors = new List<PropertyDescriptor<T>>();
		}
		private Map(Expression<Func<T, object>> expression, IElasticProperty elasticProperty) : this()
		{
			this.MapField(expression, elasticProperty);
		}
		private Map(IElasticType elasticType) : this()
		{
			this.MapType(elasticType);
		}

		private void MapField(Expression<Func<T, object>> expression, IElasticProperty elasticProperty)
		{
			var descriptor = new PropertyDescriptor<T> {
				Expression = expression, 
				Property = elasticProperty
			};
			this.PropertyDescriptors.Add(descriptor);
		}
		private void MapType(IElasticType elasticType)
		{
			this.ElasticType = elasticType;
		}
		public Map<T> AddField(Expression<Func<T, object>> expression, IElasticProperty elasticProperty)
		{
			this.MapField(expression, elasticProperty);
			return this;
		}
		public static Map<T> Type()
		{
			return new Map<T>(null);
		}
		public static Map<T> Type(IElasticType elasticType)
		{
			return new Map<T>(elasticType);
		}
	}
}
