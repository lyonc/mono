//
// RecipientInfoEnumerator.cs - System.Security.Cryptography.Pkcs.RecipientInfoEnumerator
//
// Author:
//	Sebastien Pouliot  <sebastien@ximian.com>
//
// (C) 2003 Motus Technologies Inc. (http://www.motus.com)
// Copyright (C) 2004 Novell Inc. (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

#if SECURITY_DEP

using System;
using System.Collections;

namespace System.Security.Cryptography.Pkcs {

	public sealed class RecipientInfoEnumerator : IEnumerator {

		private IEnumerator enumerator;

		// constructors

		internal RecipientInfoEnumerator (IEnumerable enumerable)
		{
			enumerator = enumerable.GetEnumerator ();
		}

		// properties

		public RecipientInfo Current {
			get { return (RecipientInfo) enumerator.Current; }
		}

		object IEnumerator.Current {
			get { return enumerator.Current; }
		}

		// methods

		public bool MoveNext ()
		{
			return enumerator.MoveNext ();
		}

		public void Reset ()
		{
			enumerator.Reset ();
		}
	}
}

#endif
