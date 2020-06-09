using liulaoc.Net.Http;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSubjectByIdMsg:BaseMsg
{
    public GetSubjectByIdMsg(int subject_id)
    {
        this.subject_id = subject_id;
    }

    public int subject_id { get; set; }
}
