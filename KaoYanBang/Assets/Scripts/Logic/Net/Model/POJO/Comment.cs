using System;
using System.Collections.Generic;
using System.Text;

namespace POJO
{
    public class Comment
    {
        public int comment_id { get; set; }
        public string content { get; set; }
        public int comment_user { get; set; }
        public int comment_invitation { get; set; }
        public int comment_status { get; set; }
        public int like_number { get; set; }
        public string create_time { get; set; }
        public string update_time { get; set; }
    }
}
