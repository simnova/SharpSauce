using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SauceLabs
{
    /// <summary>
    /// Forked from https://github.com/binken/SauceREST-DotNet/blob/master/SauceREST.cs
    /// </summary>
    public class SauceRest
    {
        private readonly string _username;
        private readonly string _accessKey;
        private const string RestUrl = "https://saucelabs.com/rest/v1/{0}";
        private const string RestUrlDonload = "https://saucelabs.com/rest/{0}/jobs/{1}";
        private const string UserResultFormat = RestUrl + "/{1}";
        private const string JobResultFormat = RestUrl + "/jobs/{1}";
        private const string DownloadVideoFormat = RestUrlDonload + "/results/video.flv";
        private const string DownloadLogFormat = RestUrlDonload + "/results/selenium-server.log";

        public SauceRest(string username, string accessKey)
        {
            _username = username;
            _accessKey = accessKey;
        }

        /// <summary>
        /// Marks a Sauce Job as 'passed'.
        /// </summary>
        /// <param name="jobId">the Sauce Job Id, typically equal to the Selenium/WebDriver sessionId</param>
        /// <exception cref="IOException">thrown if an error occurs invoking the REST request</exception>
        public void JobPassed(string jobId)
        {
            IDictionary<String, Object> updates = new Dictionary<String, Object>();
            updates.Add("passed", true);
            UpdateJobInfo(jobId, updates);
        }

        /// <summary>
        /// Marks a Sauce Job as 'failed'.
        /// </summary>
        /// <param name="jobId">the Sauce Job Id, typically equal to the Selenium/WebDriver sessionId</param>
        /// <exception cref="IOException">thrown if an error occurs invoking the REST request</exception>
        public void JobFailed(string jobId)
        {
            IDictionary<String, Object> updates = new Dictionary<String, Object>();
            updates.Add("passed", false);
            UpdateJobInfo(jobId, updates);
        }

        public void blah() { }
        /// <summary>
        /// Downloads the video for a Sauce Job to the filesystem.  The file will be stored in
        /// a directory specified by the <code>location</code> field.
        /// </summary>
        /// <param name="jobId">the Sauce Job Id, typically equal to the Selenium/WebDriver sessionId</param>
        /// <param name="location"></param>
        /// <exception cref="IOException">thrown if an error occurs invoking the REST request</exception>
        public void DownloadVideo(string jobId, string location)
        {
            var restEndpoint = new Uri(String.Format(DownloadVideoFormat, _username, jobId));
            DownloadFile(location, restEndpoint);
        }

        /// <summary>
        /// Downloads the log file for a Sauce Job to the filesystem.  The file will be stored in
        /// a directory specified by the <code>location</code> field.
        /// </summary>
        /// <param name="jobId">the Sauce Job Id, typically equal to the Selenium/WebDriver sessionId</param>
        /// <param name="location"></param>
        /// <exception cref="IOException">thrown if an error occurs invoking the REST request</exception>
        public void DownloadLog(string jobId, string location)
        {
            var restEndpoint = new Uri(String.Format(DownloadLogFormat, _username, jobId));
            DownloadFile(location, restEndpoint);
        }

        public String RetrieveResults(string path)
        {
            var restEndpoint = new Uri(String.Format(UserResultFormat, _username, path));
            return RetrieveResults(restEndpoint);
        }

        public String GetJobInfo(string jobId)
        {
            var restEndpoint = new Uri(String.Format(JobResultFormat, _username, jobId));
            return RetrieveResults(restEndpoint);
        }

        public String RetrieveResults(Uri restEndpoint)
        {
            var builder = new StringBuilder();
            var request = (HttpWebRequest)HttpWebRequest.Create(restEndpoint);
            request.Method = "GET";
            request.ContentType = "application/text";
            var usernamePassword = _username + ":" + _accessKey;
            var mycache = new CredentialCache {{restEndpoint, "Basic", new NetworkCredential(_username, _accessKey)}};
            request.Credentials = mycache;
            request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(usernamePassword)));

            using (WebResponse response = request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string inputLine;
                    while ((inputLine = reader.ReadLine()) != null)
                    {
                        builder.Append(inputLine);
                    }
                }
            }
            return builder.ToString();
        }

        public void UpdateJobInfo(String jobId, IDictionary<String, Object> updates)
        {
            var url = string.Format(JobResultFormat, _username, jobId);
            var request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "PUT";
            var usernamePassword = _username + ":" + _accessKey;
            var mycache = new CredentialCache {{new Uri(url), "Basic", new NetworkCredential(_username, _accessKey)}};
            request.Credentials = mycache;
            request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(usernamePassword)));

            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(Newtonsoft.Json.JsonConvert.SerializeObject(updates));
            }

            var response = request.GetResponse();
            response.Close();

        }

        private void DownloadFile(string location, Uri restEndpoint)
        {
            using (var webClient = new WebClient())
            {
                webClient.Credentials = new NetworkCredential(_username, _accessKey);
                webClient.DownloadFile(restEndpoint, location);
            }
        }
    }
}
