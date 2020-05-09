using liulaoc.Net.Http;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMyInvitationMsg : BaseMsg
{
    public GetMyInvitationMsg(int user_id, int index)
    {
        this.user_id = user_id;
        this.index = index;
    }

    public int user_id { get; set; }
    public int index { get; set; }
}

public class GetMyCommentMsg:BaseMsg
{
    public GetMyCommentMsg(int user_id, int index)
    {
        this.user_id = user_id;
        this.index = index;
    }

    public int user_id { get; set; }
    public int index { get; set; }
}