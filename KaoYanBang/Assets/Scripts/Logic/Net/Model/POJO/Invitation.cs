using System;
using System.Collections.Generic;
using System.Text;

namespace POJO
{
    class Invitation
    {

        public int invitation_id { get; set; }
        public string invitation_title { get; set; }
        public string content { get; set; }
        public int plate { get; set; }
        public int invitation_status { get; set; }
        public int scan_number { get; set; }
        public int post_user { get; set; }
        public string update_time { get; set; }
        public string create_time { get; set; }

    }
}
