// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(NumericFielddata))]
	public interface INumericFielddata : IFielddata
	{
		[DataMember(Name ="format")]
		NumericFielddataFormat? Format { get; set; }
	}

	public class NumericFielddata : FielddataBase, INumericFielddata
	{
		public NumericFielddataFormat? Format { get; set; }
	}

	public class NumericFielddataDescriptor
		: FielddataDescriptorBase<NumericFielddataDescriptor, INumericFielddata>, INumericFielddata
	{
		NumericFielddataFormat? INumericFielddata.Format { get; set; }

		public NumericFielddataDescriptor Format(NumericFielddataFormat? format) => Assign(format, (a, v) => a.Format = v);
	}
}
