#region Utf8Json License https://github.com/neuecc/Utf8Json/blob/master/LICENSE
// MIT License
//
// Copyright (c) 2017 Yoshifumi Kawai
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Elasticsearch.Net.Utf8Json.Internal.Emit
{
	internal class DynamicAssembly
	{
		private static readonly byte[] PublicKey = Assembly.GetExecutingAssembly().GetName().GetPublicKey();

        private readonly ModuleBuilder _moduleBuilder;
		private readonly object _gate = new object();

		public DynamicAssembly(string moduleName)
        {
			var assemblyName = new AssemblyName(moduleName);
			assemblyName.SetPublicKey(PublicKey);
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            _moduleBuilder = assemblyBuilder.DefineDynamicModule(moduleName);
        }

		// requires lock on mono environment. see: https://github.com/neuecc/MessagePack-CSharp/issues/161

        public TypeBuilder DefineType(string name, TypeAttributes attr)
        {
            lock (_gate)
				return _moduleBuilder.DefineType(name, attr);
		}

        public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent)
        {
            lock (_gate)
				return _moduleBuilder.DefineType(name, attr, parent);
		}

        public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, Type[] interfaces)
        {
            lock (_gate)
				return _moduleBuilder.DefineType(name, attr, parent, interfaces);
		}
    }
}
