using liulaoc.Net.Http;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlanMsg: BaseMsg
{
    public GetPlanMsg(int user_id)
    {
        this.user_id = user_id;
    }

    public int user_id { get; set; }
}

public class ChangePlanStatusMsg:BaseMsg
{
    public ChangePlanStatusMsg(int date, int plan_id)
    {
        this.date = date;
        this.plan_id = plan_id;
    }

    public int date { get; set; }
    public int plan_id { get; set; }

}

public class AddPlanMsg:BaseMsg
{
    public AddPlanMsg(string date, string plan_content, int plan_type, int user_id)
    {
        this.date = date;
        this.plan_content = plan_content;
        this.plan_type = plan_type;
        this.user_id = user_id;
    }

    public string date { get; set; }
    public string plan_content { get; set; }
    public int plan_type { get; set; }
    public int user_id { get; set; }
}
public class GetCommentMsg:BaseMsg
{
    public int invitation_id { get; set; }
public GetCommentMsg(int invitation_id)
    {
        this.invitation_id = invitation_id;
    }
}