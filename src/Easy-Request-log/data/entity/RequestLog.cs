using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Newtonsoft.Json;

namespace Easy_Request_log.data.entity
{
    public sealed class RequestLog
    {
        [Key] [JsonProperty("id")] public Guid Id { get; set; }

        [JsonProperty("datetime")] public DateTime Datetime { get; set; }

        [JsonProperty("username")] public string Username { get; set; }

        [JsonProperty("path")] public string Path { get; set; }

        [JsonProperty("query_string")] public string QueryString { get; set; }

        [JsonProperty("body")] public string Body { get; set; }

        [JsonProperty("status_code")] public HttpStatusCode StatusCode { get; set; }
    }
}