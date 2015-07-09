using System.Collections.Generic;

namespace Nest.DSL.Visitor
{
	public class MappingWalker
	{
		private readonly IMappingVisitor _visitor;

		public MappingWalker(IMappingVisitor visitor)
		{
			visitor.ThrowIfNull("visitor");
			_visitor = visitor;
		}

		public void Accept(IGetMappingResponse response)
		{
			if (response == null) return;
			this.Accept(response.Mapping);
		}

		public void Accept(RootObjectMapping mapping)
		{
			if (mapping == null) return;
			this._visitor.Visit(mapping);
			this.Accept(mapping.Properties);
		}
			
		public void Accept(IDictionary<FieldName, IElasticType> properties)
		{
			if (properties == null) return;
			foreach (var kv in properties)
			{
				var prop = kv.Key;
				var field = kv.Value;
				var type = field.Type;
				switch (type.Name)
				{
					case "string":
						var s = field as StringMapping;
						if (s == null) continue;
						this._visitor.Visit(s);
						this.Accept(s.Fields);
						break;
					case "float":
					case "double":
					case "byte":
					case "short":
					case "integer":
					case "long":
						var nu = field as NumberMapping;
						if (nu == null) continue;
						this._visitor.Visit(nu);
						this.Accept(nu.Fields);
						break;
					case "date":
						var d = field as DateMapping;
						if (d == null) continue;
						this._visitor.Visit(d);
						this.Accept(d.Fields);
						break;
					case "boolean":
						var bo = field as BooleanMapping;
						if (bo == null) continue;
						this._visitor.Visit(bo);
						this.Accept(bo.Fields);
						break;
					case "binary":
						var bi = field as BinaryMapping;
						if (bi == null) continue;
						this._visitor.Visit(bi);
						this.Accept(bi.Fields);
						break;
					case "object":
						var o = field as ObjectMapping;
						if (o == null) continue;
						this._visitor.Visit(o);
						this._visitor.Depth += 1;
						this.Accept(o.Properties);
						this._visitor.Depth -= 1;
						break;
					case "nested":
						var n = field as NestedObjectMapping;
						if (n == null) continue;
						this._visitor.Visit(n);
						this._visitor.Depth += 1;
						this.Accept(n.Properties);
						this._visitor.Depth -= 1;
						break;
					case "multi_field":
						var m = field as MultiFieldMapping;
						if (m == null) continue;
						this._visitor.Visit(m);
						this.Accept(m.Fields);
						break;
					case "ip":
						var i = field as IPMapping;
						if (i == null) continue;
						this._visitor.Visit(i);
						break;
					case "geo_point":
						var gp = field as GeoPointMapping;
						if (gp == null) continue;
						this._visitor.Visit(gp);
						break;
					case "geo_shape":
						var gs = field as GeoShapeMapping;
						if (gs == null) continue;
						this._visitor.Visit(gs);
						break;
					case "attachment":
						var a = field as AttachmentMapping;
						if (a == null) continue;
						this._visitor.Visit(a);
						break;
				}

			}
		}

		private void Accept(IDictionary<FieldName, IElasticCoreType> properties)
		{
			if (properties == null || properties.Count == 0) return;
			this._visitor.Depth += 1;
			
			var dict = new Dictionary<FieldName, IElasticType>();
			foreach (var kv in properties)
			{
				var t = kv.Value as IElasticType;
				if (t == null) continue;
				dict.Add(kv.Key, t);
			}

			if (dict.Count == 0) return;
			this._visitor.Depth -= 1;
		}
	}
}