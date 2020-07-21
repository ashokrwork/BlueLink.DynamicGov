using System;

namespace OneHub360.App.Services.Controllers
{
    public class filteroption
    {
        public int PageLength { get; set; }
        public int pageNumber { get; set; }
        public DateTime Fromdate { get; set; }
        public DateTime Todate { get; set; }
        public string Fromuser { get; set; }
        public string Touser { get; set; }


    }
}