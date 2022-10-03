using System.Collections.Generic;

namespace Coterie.Api.Models.Requests
{
    public class MiniRaterRequest
    {
        public string Business { get; set; }
        public decimal Revenue { get; set; }
        public List<string> States { get; set; } = new List<string>();
    }
}