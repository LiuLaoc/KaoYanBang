using liulaoc.Net.Http;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GetSchoolMsg : BaseMsg
{
    public GetSchoolMsg(int school_id)
    {
        this.school_id = school_id;
    }

    public int school_id { get; set; }
}
