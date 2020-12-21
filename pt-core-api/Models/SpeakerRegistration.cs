using System;
using System.Collections.Generic;

#nullable disable

namespace pt_core_api.Models
{
    public partial class SpeakerRegistration
    {
        public SpeakerRegistration()
        {
            EventRegistrations = new HashSet<EventRegistration>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Topic { get; set; }
        public string Country { get; set; }
        public string Theme { get; set; }
        public string OneLineProfile { get; set; }
        public string Phone { get; set; }
        public string ProfilePicture { get; set; }
        public string Description { get; set; }
        public bool? IsApporved { get; set; }

        public virtual ICollection<EventRegistration> EventRegistrations { get; set; }
    }
}
