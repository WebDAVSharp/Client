using System;
using System.Net;
using WebDAVSharp.Client.WebDav;

namespace WebDAVSharp.Client
{
    /// <summary>
    /// This program will do a PROPFIND request to the WebDAV server.
    /// The statuscode and statusdescription will be written to the console.
    /// </summary>
    class Program
    {
        // change according to your configuration
        private const string Url = "http://localhost:8880/";

        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            Console.WriteLine("PROPFIND " + Url);

            // do the request and get the WebResponse
            WebResponse response = WebDavMethod.Propfind(Url);

            // cast to an HttpWebResponse
            HttpWebResponse httpWebResponse = response as HttpWebResponse;

            // if not null, write statuscodes to the console
            if (httpWebResponse != null)
            {
                Console.WriteLine((int)httpWebResponse.StatusCode + " " + httpWebResponse.StatusDescription);
            }
            else
            {
                Console.WriteLine("HttpWebResponse was null.");
            }
            
        }
    }
}
