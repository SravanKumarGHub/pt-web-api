using System;
using System.Collections.Generic;

#nullable disable

namespace pt_core_api.Models
{
    public partial class PastEvent
    {
        public int Id { get; set; }
        public string PastEventUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
