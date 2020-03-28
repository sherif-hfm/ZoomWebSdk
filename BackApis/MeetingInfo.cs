using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackApis
{

    public class MeetingRequest
    {
        public string topic { get; set; }
        public int type { get; set; }
        public int duration { get; set; }
        public string password { get; set; }
        public string agenda { get; set; }
        public MeetingSettings settings { get; set; }
    }

    public class MeetingSettings
    {
        public bool? host_video { get; set; }
        public bool? participant_video { get; set; }
        public bool? cn_meeting { get; set; }
        public bool? in_meeting { get; set; }
        public bool? join_before_host { get; set; }
        public bool? mute_upon_entry { get; set; }
        public bool? watermark { get; set; }
        public int approval_type { get; set; }
        public int registration_type { get; set; }
        public string audio { get; set; }
        public string auto_recording { get; set; }
        public bool? enforce_login { get; set; }
        public bool? enforce_login_domains { get; set; }
        public string contact_name { get; set; }
    }


    public class MeetingResponse
    {
        public string uuid { get; set; } // meeting ID
        public int id { get; set; }
        public string host_id { get; set; }
        public string topic { get; set; }
        public int type { get; set; }
        public string status { get; set; }
        public string timezone { get; set; }
        public string agenda { get; set; }
        public DateTime created_at { get; set; }
        public string start_url { get; set; }
        public string join_url { get; set; }
        public string password { get; set; }
        public string h323_password { get; set; }
        public string pstn_password { get; set; }
        public string encrypted_password { get; set; }
        public MeetingSettings settings { get; set; }
    }

  


}