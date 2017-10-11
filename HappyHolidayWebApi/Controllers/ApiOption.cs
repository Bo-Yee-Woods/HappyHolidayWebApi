using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyHolidayWebApi.Controllers
{
    public class Documentation
    {
        public string Title { get; set; }

        public string Summary { get; set; }

        public List<ApiOption> ApiOptions { get; set; }

        public List<object> SampleModels { get; set; }
    }

    public class ApiOption
    {
        public string Name { get; set; }

        public string Uri { get; set; }

        public string HttpMethod { get; set; }

        public IEnumerable<Input> Inputs { get; set; }

        public List<Reponse> Responses { get; set; }

        public string Description { get; set; }        
    }

    public class Input
    {
        public string Parameter { get; set; }

        public string Type { get; set; }
    }

    public class Reponse
    {
        public string Case { get; set; }

        public int StatusCode { get; set; }
    }

    public static class HttpMethod
    {
        public const string GET = "GET";

        public const string POST = "POST";

        public const string PUT = "POST";

        public const string PATCH = "PATCH";

        public const string DELETE = "DELETE";

        public const string OPTIONS = "OPTIONS";
    }
}


