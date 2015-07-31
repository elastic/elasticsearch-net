using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace Nest
{
	public interface IAttachmentType : IElasticType
	{

	}

	[JsonObject(MemberSerialization.OptIn)]
	public class AttachmentType : ElasticType, IAttachmentType
	{
		public AttachmentType() : base("attachment") { }
	}

	public class AttachmentTypeDescriptor<T> where T : class
	{
		internal AttachmentType _Mapping = new AttachmentType();

		public AttachmentTypeDescriptor<T> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}
		public AttachmentTypeDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}

		public AttachmentTypeDescriptor<T> FileField(Func<StringTypeDescriptor<T>, StringTypeDescriptor<T>> stringMapper)
		{
			stringMapper.ThrowIfNull("stringMapper");
			var d = stringMapper(new StringTypeDescriptor<T>());
			d.ThrowIfNull("stringMapper return value");
			
			this._Mapping.Fields[this._Mapping.Name] = d._Mapping;
			return this;
		}

		public AttachmentTypeDescriptor<T> AuthorField(Func<StringTypeDescriptor<T>, StringTypeDescriptor<T>> stringMapper)
		{
			stringMapper.ThrowIfNull("stringMapper");
			var d = stringMapper(new StringTypeDescriptor<T>());
			d.ThrowIfNull("attachment author field mapping");

			this._Mapping.Fields["author"] = d._Mapping;
			return this;
		}
		public AttachmentTypeDescriptor<T> TitleField(Func<StringTypeDescriptor<T>, StringTypeDescriptor<T>> stringMapper)
		{
			stringMapper.ThrowIfNull("stringMapper");
			var d = stringMapper(new StringTypeDescriptor<T>());
			d.ThrowIfNull("attachment title field");

			this._Mapping.Fields["title"] = d._Mapping;
			return this;
		}
		public AttachmentTypeDescriptor<T> MetadataField(string metadataFieldName, Func<StringTypeDescriptor<T>, StringTypeDescriptor<T>> stringMapper)
		{
			metadataFieldName.ThrowIfNullOrEmpty("metadataFieldName");
			stringMapper.ThrowIfNull("stringMapper");
			var d = stringMapper(new StringTypeDescriptor<T>());
			d.ThrowIfNull("attachment metadata field");

			this._Mapping.Fields[metadataFieldName] = d._Mapping;
			return this;
		}
		public AttachmentTypeDescriptor<T> DateField(Func<DateTypeDescriptor<T>, DateTypeDescriptor<T>> dateMapper)
		{
			dateMapper.ThrowIfNull("stringMapper");
			var d = dateMapper(new DateTypeDescriptor<T>());
			d.ThrowIfNull("stringMapper return value");

			this._Mapping.Fields["date"] = d._Mapping;
			return this;
		}
		
	}
}