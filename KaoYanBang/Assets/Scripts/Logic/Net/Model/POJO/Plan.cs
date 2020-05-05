using System;
using System.Collections.Generic;
using System.Text;

namespace POJO
{
    class Plan
    {

        public int plan_id { get; set; }
        public int user_id { get; set; }
        public string date { get; set; }
        public int plan_status { get; set; }
        public int plan_type { get; set; }
        public string plan_content { get; set; }
        public string create_time { get; set; }
        public string end_time { get; set; }

    }
}
