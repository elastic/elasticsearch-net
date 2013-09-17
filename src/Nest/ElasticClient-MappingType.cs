using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Nest.Resolvers.Writers;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <summary>
		/// <para>Automatically map an object based on its attributes, this will also explicitly map strings to strings, datetimes to dates etc even 
		/// if they are not marked with any attributes.</para>
		/// <para>
		/// Type name is the inferred type name for T under the default index
		/// </para>
		/// </summary>
		public IIndicesResponse MapFromAttributes<T>(int maxRecursion = 0) where T : class
		{
			string type = this.GetTypeNameFor<T>();
			return this.MapFromAttributes<T>(this.IndexNameResolver.GetIndexForType<T>(), type, maxRecursion);
		}
		/// <summary>
		/// <para>Automatically map an object based on its attributes, this will also explicitly map strings to strings, datetimes to dates etc even 
		/// if they are not marked with any attributes.</para>
		/// <para>
		/// Type name is the inferred type name for T under the specified index
		/// </para>
		/// </summary>
		public IIndicesResponse MapFromAttributes<T>(string index, int maxRecursion = 0) where T : class
		{
			string type = this.GetTypeNameFor<T>();
			return this.MapFromAttributes<T>(index, type, maxRecursion);
		}
		/// <summary>
		/// <para>Automatically map an object based on its attributes, this will also explicitly map strings to strings, datetimes to dates etc even 
		/// if they are not marked with any attributes.</para>
		/// <para>
		/// Type name is the specified type name under the specified index
		/// </para>
		/// </summary>
		public IIndicesResponse MapFromAttributes<T>(string index, string type, int maxRecursion = 0) where T : class
		{
			string path = this.PathResolver.CreateIndexTypePath(index, type, "_mapping");
			var typeMapping = this.CreateMapFor<T>(type, maxRecursion);
			typeMapping.TypeNameMarker = type;
			return this.Map(typeMapping, index, type, ignoreConflicts: false);
		}

		/// <summary>
		/// <para>Automatically map an object based on its attributes, this will also explicitly map strings to strings, datetimes to dates etc even 
		/// if they are not marked with any attributes.</para>
		/// <para>
		/// Type name is the inferred type name for T under the default index
		/// </para>
		/// </summary>
		public IIndicesResponse MapFromAttributes(Type t, int maxRecursion = 0)
		{
			string type = this.GetTypeNameFor(t);
			return this.MapFromAttributes(t, this.IndexNameResolver.GetIndexForType(t), type, maxRecursion);
		}
		/// <summary>
		/// <para>Automatically map an object based on its attributes, this will also explicitly map strings to strings, datetimes to dates etc even 
		/// if they are not marked with any attributes.</para>
		/// <para>
		/// Type name is the inferred type name for T under the specified index
		/// </para>
		/// </summary>
		public IIndicesResponse MapFromAttributes(Type t, string index, int maxRecursion = 0)
		{
			string type = this.GetTypeNameFor(t);
			return this.MapFromAttributes(t, index, type, maxRecursion);
		}
		/// <summary>
		/// <para>Automatically map an object based on its attributes, this will also explicitly map strings to strings, datetimes to dates etc even 
		/// if they are not marked with any attributes.</para>
		/// <para>
		/// Type name is the specified type name under the specified index
		/// </para>
		/// </summary>
		public IIndicesResponse MapFromAttributes(Type t, string index, string type, int maxRecursion = 0)
		{
			string path = this.PathResolver.CreateIndexTypePath(index, type, "_mapping");
			var typeMapping = this.CreateMapFor(t, type, maxRecursion);
			typeMapping.TypeNameMarker = type;
			return this.Map(typeMapping, index, type, ignoreConflicts: false);
		}

		public IIndicesResponse MapFluent(Func<RootObjectMappingDescriptor<dynamic>, RootObjectMappingDescriptor<dynamic>> typeMappingDescriptor)
		{
			return this.MapFluent<dynamic>(typeMappingDescriptor);
		}

		public IIndicesResponse MapFluent<T>(Func<RootObjectMappingDescriptor<T>, RootObjectMappingDescriptor<T>> typeMappingDescriptor) where T : class
		{
			typeMappingDescriptor.ThrowIfNull("typeMappingDescriptor");
			var d = typeMappingDescriptor(new RootObjectMappingDescriptor<T>(this.Settings));
			var typeMapping = d._Mapping;
			var indexName = d._IndexName;
			if (indexName.IsNullOrEmpty())
				indexName = this.IndexNameResolver.GetIndexForType<T>();

			return this.Map(typeMapping, indexName, this.ResolveTypeName(d._TypeName), d._IgnoreConflicts);

		}

		/// <summary>
		/// Verbosely and explicitly map an object using a TypeMapping object, this gives you exact control over the mapping. Index is the inferred default index
		/// </summary>
		public IIndicesResponse Map(RootObjectMapping typeMapping)
		{
			return this.Map(typeMapping, this.Settings.DefaultIndex);
		}
		/// <summary>
		/// Verbosely and explicitly map an object using a TypeMapping object, this gives you exact control over the mapping.
		/// </summary>
		public IIndicesResponse Map(RootObjectMapping typeMapping, string index, string typeName = null, bool ignoreConflicts = false)
		{
			if (typeName.IsNullOrEmpty())
				typeName = this.ResolveTypeName(typeMapping.TypeNameMarker);

			var mapping = new Dictionary<string, RootObjectMapping>();
			mapping.Add(this.ResolveTypeName(typeMapping.TypeNameMarker), typeMapping);

			string map = JsonConvert.SerializeObject(mapping, Formatting.None, IndexSerializationSettings);
			string path = this.PathResolver.CreateIndexTypePath(index, typeName, "_mapping");
			if (ignoreConflicts)
				path += "?ignore_conflicts=true";
			ConnectionStatus status = this.Connection.PutSync(path, map);

			var r = this.ToParsedResponse<IndicesResponse>(status);
			return r;
        }
       

		private RootObjectMapping CreateMapFor<T>(string type, int maxRecursion = 0) where T : class
		{
			return this.CreateMapFor(typeof(T), type, maxRecursion);
		}
		private RootObjectMapping CreateMapFor(Type t, string type, int maxRecursion = 0)
		{
			var writer = new TypeMappingWriter(t, type, this.Settings, maxRecursion);
			var typeMapping = writer.RootObjectMappingFromAttributes();
			return typeMapping;
		}
	}
}