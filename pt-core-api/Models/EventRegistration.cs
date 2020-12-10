using System;
using System.Collections.Generic;

#nullable disable

namespace pt_core_api.Models
{
    public partial class EventRegistration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public int? SpeakerId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual SpeakerRegistration Speaker { get; set; }
    }
}
