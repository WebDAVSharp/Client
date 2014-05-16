using System;
using System.IO;
using System.Net;
using System.Text;

namespace WebDAVSharp.Client.WebDav
{
    /// <summary>
    /// The basics for talking to the WebDAV server
    /// </summary>
    internal static class WebDav
    {
        /// <summary>
        /// Get the credentials of the user
        /// </summary>
        /// <param name="requestUri">The request URI</param>
        /// <returns>
        /// The credentials as an instance of <see cref="CredentialCache" />
        /// </returns>
        internal static CredentialCache WebDavCredentialCache(string requestUri)
        {
            var myCredentialCache = new CredentialCache
            {
                {
                    new Uri(requestUri),
                    "Negotiate",
                    new NetworkCredential(WebDavConfig.UserName, WebDavConfig.Password, WebDavConfig.Domain)
                }
            };         

            return myCredentialCache;
        }

        /// <summary>
        /// Creates a basic WebDav Request
        /// </summary>
        /// <param name="requestUri">The requested URI as an <see cref="Uri" /></param>
        /// <param name="method">The <see cref="string" /> defining the WebDAV method to be used</param>
        /// <returns>
        /// The basic WebDav Request as an <see cref="HttpWebRequest" />
        /// </returns>
        internal static HttpWebRequest WebDavRequestBase(string requestUri, string method)
        {
            // Create the HttpWebRequest object.
            var request = (HttpWebRequest)WebRequest.Create(requestUri);

            request.UserAgent = "WebDAV-UnitTestProject/1.0.0";

            // Add the network credentials to the request.
            request.UseDefaultCredentials = true;

            // Specify the WebDAV method.
            request.Method = method;

            return request;
        }

        /// <summary>
        /// Creates the response of the given <see cref="HttpWebRequest" />
        /// </summary>
        /// <param name="request">The <see cref="HttpWebRequest" /></param>
        /// <returns>
        /// The <see cref="WebResponse" /> of the <see cref="HttpWebRequest" />
        /// </returns>
        internal static WebResponse WebDavResponse(HttpWebRequest request)
        {
            try
            {
                // Send the WebDav method request, get the
                // method response from the server.
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                Encoding encode = Encoding.GetEncoding("utf-8");

                // Pipes the stream to a higher level stream reader with the required encoding format. 
                if (stream != null)
                {
                    var readStream = new StreamReader(stream, encode);
                    var read = new Char[256];
                    String str = "";
                    // Reads 256 characters at a time.     
                    int count = readStream.Read(read, 0, 256);
                    while (count > 0)
                    {
                        // Dumps the 256 characters on a string and displays the string to the console.
                        str += new String(read, 0, count);
                        count = readStream.Read(read, 0, 256);
                    }
                    // Releases the resources of the Stream.
                    readStream.Close();
                }

                // Close the HttpWebResponse object.
                response.Close();

                return response;
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Response;
            }
            catch (Exception ex)
            {
                // Catch any exceptions. Any error codes from the WebDAV
                // method request on the server will be caught here, also.
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
