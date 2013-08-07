using System;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	public class AttachmentMappingDescriptor<T>
	{
		internal AttachmentMapping _Mapping = new AttachmentMapping();

		public AttachmentMappingDescriptor<T> Name(string name)
		{
			this._Mapping.TypeNameMarker = name;
			return this;
		}
		public AttachmentMappingDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			var name = new PropertyNameResolver().ResolveToLastToken(objectPath);
			this._Mapping.TypeNameMarker = name;
			return this;
		}

		public AttachmentMappingDescriptor<T> FileField(Func<StringMappingDescriptor<T>, StringMappingDescriptor<T>> stringMapper)
		{
			stringMapper.ThrowIfNull("stringMapper");
			var d = stringMapper(new StringMappingDescriptor<T>());
			d.ThrowIfNull("stringMapper return value");
			
			this._Mapping.Fields[this._Mapping.TypeNameMarker.Name] = d._Mapping;
			return this;
		}

		public AttachmentMappingDescriptor<T> AuthorField(Func<StringMappingDescriptor<T>, StringMappingDescriptor<T>> stringMapper)
		{
			stringMapper.ThrowIfNull("stringMapper");
			var d = stringMapper(new StringMappingDescriptor<T>());
			d.ThrowIfNull("stringMapper return value");

			this._Mapping.Fields["author"] = d._Mapping;
			return this;
		}
		public AttachmentMappingDescriptor<T> DateField(Func<DateMappingDescriptor<T>, DateMappingDescriptor<T>> dateMapper)
		{
			dateMapper.ThrowIfNull("stringMapper");
			var d = dateMapper(new DateMappingDescriptor<T>());
			d.ThrowIfNull("stringMapper return value");

			this._Mapping.Fields["date"] = d._Mapping;
			return this;
		}
		
	}
}
