using System;
using System.Collections.Generic;
using System.Text;

public enum InvitationType
{
    Regulation = 1,
    Invitation =0,
}
namespace POJO
{
    public class Invitation
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

        public int invitation_type { get; set; }
        public int school_id { get; set; }

    }
}
