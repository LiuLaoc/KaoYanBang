﻿using System;

namespace POJO
{
    public class User
    {
        public int user_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string nickname { get; set; }
        public int root { get; set; }
        public int sex { get; set; }
        public int isBan { get; set; }
        public int invitation_number { get; set; }
        public int comment_number { get; set; }
        public int follow { get; set; }
        public string head_sculpture { get; set; }

        public int school_id { get; set; }
        public int subject_id { get; set; }
    }
}
