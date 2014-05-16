using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;

namespace WebDAVSharp.Client.WebDav
{
    /// <summary>
    /// The WebDAV methods to use are defined in this class
    /// </summary>
    internal static class WebDavMethod
    {
        /// <summary>
        /// Execute a WebDAV Propfind method on the requested URI
        /// </summary>
        /// <param name="requestUri">The <see cref="Uri" /> for the request</param>
        /// <param name="depth">The <see cref="string" /> for the Depth header</param>
        /// <param name="content">The <see cref="string" /> of the body content</param>
        /// <returns>
        /// The response as a <see cref="WebResponse" />
        /// </returns>
        internal static WebResponse Propfind(string requestUri, string depth = "", string content = "")
        {
            HttpWebRequest request = WebDav.WebDavRequestBase(requestUri, "PROPFIND");
            request.Credentials = WebDav.WebDavCredentialCache(requestUri);

            request.ContentLength = content.Length;
            request.Headers.Add("Depth", depth);

            return WebDav.WebDavResponse(request);
        }

        /// <summary>
        /// Execute a WebDAV MkCol method on the requested URI
        /// </summary>
        /// <param name="requestUri">The <see cref="Uri" /> for the request</param>
        /// <param name="content">The <see cref="string" /> of the body content</param>
        /// <returns>
        /// The response as a <see cref="WebResponse" />
        /// </returns>
        internal static WebResponse MkCol(string requestUri, string content = "")
        {
            HttpWebRequest request = WebDav.WebDavRequestBase(requestUri, "MKCOL");
            request.Credentials = WebDav.WebDavCredentialCache(requestUri);

            request.ContentLength = content.Length;

            return WebDav.WebDavResponse(request);
        }

        /// <summary>
        /// Execute a WebDAV Delete method on the requested URI
        /// </summary>
        /// <param name="requestUri">The <see cref="Uri" /> for the request</param>
        /// <returns>
        /// The response as a <see cref="WebResponse" />
        /// </returns>
        internal static WebResponse Delete(string requestUri)
        {
            HttpWebRequest request = WebDav.WebDavRequestBase(requestUri, "DELETE");
            request.Credentials = WebDav.WebDavCredentialCache(requestUri);

            return WebDav.WebDavResponse(request);
        }

        /// <summary>
        /// Execute a WebDAV Put method on the requested URI
        /// </summary>
        /// <param name="requestUri">The <see cref="Uri" /> for the request</param>
        /// <param name="content">The <see cref="string" /> of the body content</param>
        /// <returns>
        /// The response as a <see cref="WebResponse" />
        /// </returns>
        internal static WebResponse Put(string requestUri, string content = "")
        {
            HttpWebRequest request = WebDav.WebDavRequestBase(requestUri, "PUT");
            request.Credentials = WebDav.WebDavCredentialCache(requestUri);

            request.ContentLength = content.Length;

            return WebDav.WebDavResponse(request);
        }

        /// <summary>
        /// Execute a WebDAV Copy method on the requested URI
        /// </summary>
        /// <param name="requestUri">The <see cref="Uri" /> for the request</param>
        /// <param name="destinationUri">The <see cref="string" /> for the Destination header</param>
        /// <param name="overwrite">The <see cref="bool" /> for the Overwrite header</param>
        /// <returns>
        /// The response as a <see cref="WebResponse" />
        /// </returns>
        internal static WebResponse Copy(string requestUri, string destinationUri, bool overwrite = false)
        {
            HttpWebRequest request = WebDav.WebDavRequestBase(requestUri, "COPY");
            request.Credentials = WebDav.WebDavCredentialCache(requestUri);

            request.Headers.Add("Destination", destinationUri);
            request.Headers.Add("Overwrite", overwrite ? "T" : "F");

            return WebDav.WebDavResponse(request);
        }

        /// <summary>
        /// Execute a WebDAV Move method on the requested URI
        /// </summary>
        /// <param name="requestUri">The <see cref="Uri" /> for the request</param>
        /// <param name="destinationUri">The <see cref="string" /> for the Destination header</param>
        /// <param name="overwrite">The <see cref="bool" /> for the Overwrite header</param>
        /// <returns>
        /// The response as a <see cref="WebResponse" />
        /// </returns>
        internal static WebResponse Move(string requestUri, string destinationUri, bool overwrite = false)
        {
            HttpWebRequest request = WebDav.WebDavRequestBase(requestUri, "MOVE");
            request.Credentials = WebDav.WebDavCredentialCache(requestUri);

            request.Headers.Add("Destination", destinationUri);
            request.Headers.Add("Overwrite", overwrite ? "T" : "F");

            return WebDav.WebDavResponse(request);
        }

        /// <summary>
        /// Execute a WebDAV Lock method on the requested URI
        /// </summary>
        /// <param name="requestUri">The <see cref="Uri" /> for the request</param>
        /// <param name="content">The <see cref="string" /> of the body content</param>
        /// <returns>
        /// The response as a <see cref="WebResponse" />
        /// </returns>
        internal static WebResponse Lock(string requestUri, string content = "")
        {
            HttpWebRequest request = WebDav.WebDavRequestBase(requestUri, "LOCK");
            request.Credentials = WebDav.WebDavCredentialCache(requestUri);

            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
            request.ContentLength = content.Length;
            request.ContentType = "text/xml; charset=\"utf-8\"";
            request.Timeout = 3600;

            request.Headers.Add("Depth", "0"); //can also be infinity
            request.Headers.Add("Pragma", "no-cache");

            // send content
            var encoding = new UTF8Encoding();
            byte[] byte1 = encoding.GetBytes(content);
            Stream newStream = request.GetRequestStream();
            newStream.Write(byte1, 0, byte1.Length);

            return WebDav.WebDavResponse(request);
        }

        /// <summary>
        /// Execute a WebDAV Unlock method on the requested URI
        /// </summary>
        /// <param name="requestUri">The <see cref="Uri" /> for the request</param>
        /// <param name="content">The <see cref="string" /> of the body content</param>
        /// <returns>
        /// The response as a <see cref="WebResponse" />
        /// </returns>
        internal static WebResponse Unlock(string requestUri, string content = "")
        {
            HttpWebRequest request = WebDav.WebDavRequestBase(requestUri, "UNLOCK");
            request.Credentials = WebDav.WebDavCredentialCache(requestUri);

            return WebDav.WebDavResponse(request);
        }

        /// <summary>
        /// Execute a WebDAV Options method on the requested URI
        /// </summary>
        /// <param name="requestUri">The <see cref="Uri" /> for the request</param>
        /// <returns>
        /// The response as a <see cref="WebResponse" />
        /// </returns>
        internal static WebResponse Options(string requestUri)
        {
            HttpWebRequest request = WebDav.WebDavRequestBase(requestUri, "OPTIONS");
            request.Credentials = WebDav.WebDavCredentialCache(requestUri);

            return WebDav.WebDavResponse(request);
        }

        /// <summary>
        /// Execute a WebDAV Head method on the requested URI
        /// </summary>
        /// <param name="requestUri">The <see cref="Uri" /> for the request</param>
        /// <returns>
        /// The response as a <see cref="WebResponse" />
        /// </returns>
        internal static WebResponse Head(string requestUri)
        {
            HttpWebRequest request = WebDav.WebDavRequestBase(requestUri, "HEAD");
            request.Credentials = WebDav.WebDavCredentialCache(requestUri);

            return WebDav.WebDavResponse(request);
        }

        /// <summary>
        /// Execute a WebDAV Proppatch method on the requested URI
        /// </summary>
        /// <param name="requestUri">The <see cref="Uri" /> for the request</param>
        /// <param name="content">The <see cref="string" /> of the body content</param>
        /// <returns>
        /// The response as a <see cref="WebResponse" />
        /// </returns>
        internal static WebResponse Proppatch(string requestUri, string content = "")
        {
            HttpWebRequest request = WebDav.WebDavRequestBase(requestUri, "PROPPATCH");
            request.Credentials = WebDav.WebDavCredentialCache(requestUri);

            request.ContentLength = content.Length;
            request.ContentType = "text/xml; charset=\"utf-8\"";

            // send content
            var encoding = new UTF8Encoding();
            byte[] byte1 = encoding.GetBytes(content);
            Stream newStream = request.GetRequestStream();
            newStream.Write(byte1, 0, byte1.Length);

            return WebDav.WebDavResponse(request);
        }

        /// <summary>
        /// Execute a WebDAV Get method on the requested URI
        /// </summary>
        /// <param name="requestUri">The <see cref="Uri" /> for the request</param>
        /// <returns>
        /// The response as a <see cref="WebResponse" />
        /// </returns>
        internal static WebResponse Get(string requestUri)
        {
            HttpWebRequest request = WebDav.WebDavRequestBase(requestUri, "GET");
            request.Credentials = WebDav.WebDavCredentialCache(requestUri);

            return WebDav.WebDavResponse(request);
        }
    }
}
