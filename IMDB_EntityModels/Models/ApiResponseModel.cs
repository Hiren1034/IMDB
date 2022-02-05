using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB_EntityModels.Models
{
    public class ApiResponseModel
    {
        public string Message { get; set; }

        public int Code { get; set; }

        public dynamic Data { get; set; }
    }
}
