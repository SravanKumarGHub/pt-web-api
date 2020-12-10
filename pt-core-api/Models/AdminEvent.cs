using System;
using System.Collections.Generic;

#nullable disable

namespace pt_core_api.Models
{
    public partial class AdminEvent
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string PromoVideo { get; set; }
        public string CauroselPicture { get; set; }
        public string Descrption { get; set; }
        public DateTime? EventTime { get; set; }
        public string PromoPicture { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? SpeakerId { get; set; }
    }
}
