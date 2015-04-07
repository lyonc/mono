//
// MonoDefaultTlsProvider.cs
//
// Author:
//       Martin Baulig <martin.baulig@xamarin.com>
//
// Copyright (c) 2015 Xamarin, Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;

namespace Mono.Security.Providers.DotNet
{
	/*
	 * This provider only uses the public .NET APIs from System.dll.
	 * 
	 * It is primarily intended for testing.
	 */
	public class DotNetTlsProvider : MonoTlsProvider
	{
		public override bool SupportsHttps {
			get { return false; }
		}

		public override bool SupportsSslStream {
			get { return true; }
		}

		public override bool SupportsMonoExtensions {
			get { return false; }
		}

		public override bool SupportsTlsContext {
			get { return false; }
		}

		public override bool IsHttpsStream (Stream stream)
		{
			return false;
		}

#pragma warning disable 618

		public override IMonoHttpsStream GetHttpsStream (Stream stream)
		{
			throw new InvalidOperationException ();
		}

		public override IMonoHttpsStream CreateHttpsClientStream (
			Stream innerStream, HttpWebRequest request, byte[] buffer)
		{
			throw new NotSupportedException ("Web API is not supported.");
		}

#pragma warning restore 618

		public override MonoSslStream CreateSslStream (
			Stream innerStream, bool leaveInnerStreamOpen,
			MonoRemoteCertificateValidationCallback userCertificateValidationCallback,
			MonoLocalCertificateSelectionCallback userCertificateSelectionCallback,
			MonoTlsSettings settings = null)
		{
			if (settings != null)
				throw new NotSupportedException ("Mono-specific API Extensions not available.");

			var sslStream = new DotNetSslStreamImpl (
				innerStream, leaveInnerStreamOpen,
				ConvertCallback (userCertificateValidationCallback),
				ConvertCallback (userCertificateSelectionCallback));
			return sslStream;
		}

		public override IMonoTlsContext CreateTlsContext (
			string hostname, bool serverMode, TlsProtocols protocolFlags,
			X509Certificate serverCertificate, X509CertificateCollection clientCertificates,
			bool remoteCertRequired, MonoEncryptionPolicy encryptionPolicy,
			CertificateValidationHelper certificateValidationHelper,
			MonoTlsSettings settings)
		{
			throw new NotSupportedException ();
		}

		internal static RemoteCertificateValidationCallback ConvertCallback (MonoRemoteCertificateValidationCallback callback)
		{
			if (callback == null)
				return null;

			return (s, c, ch, e) => callback (null, c, ch, (MonoSslPolicyErrors)e);
		}

		internal static LocalCertificateSelectionCallback ConvertCallback (MonoLocalCertificateSelectionCallback callback)
		{
			if (callback == null)
				return null;

			return (s, t, lc, rc, ai) => callback (t, lc, rc, ai);
		}

	}
}

