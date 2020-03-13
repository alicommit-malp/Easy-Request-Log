using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Easy_Request_log.data.entity
{
    public sealed class RequestLog
    {
        private string _path;
        private string _queryString;
        private string _body;
        [Key, JsonProperty("id")] public Guid Id { get; set; }

        [JsonProperty("datetime"), Required] public DateTime Datetime { get; set; }

        [JsonProperty("username")] public string Username { get; set; }

        [JsonProperty("path"), Required]
        public string Path
        {
            get => _path;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _path=Regex.Escape(value.ToLower());
                }
            }
        }

        [JsonProperty("query_string")]
        public string QueryString
        {
            get => _queryString;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _queryString=Regex.Escape(value.ToLower());
                }
            }
        }

        [JsonProperty("body")]
        public string Body
        {
            get => _body;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _body=Regex.Escape(value.ToLower());
                }
            }
        }

        [NotMapped]
        public string FullPath => _path + _queryString;

        [JsonProperty("status_code")] public HttpStatusCode StatusCode { get; set; }
    }
}