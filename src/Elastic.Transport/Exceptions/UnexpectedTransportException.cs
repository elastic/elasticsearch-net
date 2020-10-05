// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;

namespace Elastic.Transport
{

	public class UnexpectedTransportException : TransportException
	{
		public UnexpectedTransportException(Exception killerException, IReadOnlyCollection<PipelineException> seenExceptions)
			: base(PipelineFailure.Unexpected, killerException?.Message ?? "An unexpected exception occurred.", killerException) =>
			SeenExceptions = seenExceptions;

		public IReadOnlyCollection<PipelineException> SeenExceptions { get; }
	}


}
