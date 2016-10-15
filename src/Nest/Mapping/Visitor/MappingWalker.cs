using System.Collections.Generic;

namespace Nest
{
	public class MappingWalker
	{
		private readonly IMappingVisitor _visitor;

		public MappingWalker(IMappingVisitor visitor)
		{
			visitor.ThrowIfNull(nameof(visitor));
			_visitor = visitor;
		}

		public void Accept(IGetMappingResponse response)
		{
			if (response == null) return;

			foreach (var indexMapping in response.Mappings)
			foreach (var typeMapping in indexMapping.Value)
			{
				this.Accept(typeMapping.Value);
			}
		}

		public void Accept(ITypeMapping mapping)
		{
			if (mapping == null) return;
			this._visitor.Visit(mapping);
			this.Accept(mapping.Properties);
		}

		public void Accept(IProperties properties)
		{
			if (properties == null) return;
			foreach (var kv in (IEnumerable<KeyValuePair<PropertyName, IProperty>>)properties)
			{
				var prop = kv.Key;
				var field = kv.Value;
				var type = field.Type;
				switch (type.Name)
				{
					case "text":
						var t = field as ITextProperty;
						if (t == null) continue;
						this._visitor.Visit(t);
						this.Accept(t.Fields);
						break;
					case "keyword":
						var k = field as IKeywordProperty;
						if (k == null) continue;
						this._visitor.Visit(k);
						this.Accept(k.Fields);
						break;
					case "string":
#pragma warning disable 618
						var s = field as IStringProperty;
#pragma warning restore 618
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
						var nu = field as INumberProperty;
						if (nu == null) continue;
						this._visitor.Visit(nu);
						this.Accept(nu.Fields);
						break;
					case "date":
						var d = field as IDateProperty;
						if (d == null) continue;
						this._visitor.Visit(d);
						this.Accept(d.Fields);
						break;
					case "boolean":
						var bo = field as IBooleanProperty;
						if (bo == null) continue;
						this._visitor.Visit(bo);
						this.Accept(bo.Fields);
						break;
					case "binary":
						var bi = field as IBinaryProperty;
						if (bi == null) continue;
						this._visitor.Visit(bi);
						this.Accept(bi.Fields);
						break;
					case "object":
						var o = field as IObjectProperty;
						if (o == null) continue;
						this._visitor.Visit(o);
						this._visitor.Depth += 1;
						this.Accept(o.Properties);
						this._visitor.Depth -= 1;
						break;
					case "nested":
						var n = field as INestedProperty;
						if (n == null) continue;
						this._visitor.Visit(n);
						this._visitor.Depth += 1;
						this.Accept(n.Properties);
						this._visitor.Depth -= 1;
						break;
					case "ip":
						var i = field as IIpProperty;
						if (i == null) continue;
						this._visitor.Visit(i);
						this.Accept(i.Fields);
						break;
					case "geo_point":
						var gp = field as IGeoPointProperty;
						if (gp == null) continue;
						this._visitor.Visit(gp);
						this.Accept(gp.Fields);
						break;
					case "geo_shape":
						var gs = field as IGeoShapeProperty;
						if (gs == null) continue;
						this._visitor.Visit(gs);
						this.Accept(gs.Fields);
						break;
					case "attachment":
						var a = field as IAttachmentProperty;
						if (a == null) continue;
						this._visitor.Visit(a);
						this.Accept(a.Fields);
						break;
					case "completion":
						var c = field as ICompletionProperty;
						if (c == null) continue;
						this._visitor.Visit(c);
						this.Accept(c.Fields);
						break;
					case "murmur3":
						var mm = field as IMurmur3HashProperty;
						if (mm == null) continue;
						this._visitor.Visit(mm);
						this.Accept(mm.Fields);
						break;
					case "token_count":
						var tc = field as ITokenCountProperty;
						if (tc == null) continue;
						this._visitor.Visit(tc);
						this.Accept(tc.Fields);
						break;
				}
			}
		}
	}
}
