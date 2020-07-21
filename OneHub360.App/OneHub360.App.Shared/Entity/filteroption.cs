using System;

namespace OneHub360.App.Shared
{
    public class filteroption
    {
        public bool isfirstload { get; set; }
        public string filtertype { get; set; }
        public int datatype { get; set; } 
        public int PageLength { get; set; }
        public int pageNumber { get; set; }
        public DateTime Fromdate { get; set; }
        public DateTime Todate { get; set; }
        public string  Fromuser { get; set; }
        public string Touser { get; set; }
        public int status { get; set; }

    }
}