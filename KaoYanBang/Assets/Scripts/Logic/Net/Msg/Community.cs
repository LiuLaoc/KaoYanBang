using liulaoc.Net.Http;
using POJO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHotInvitationMsg:BaseMsg
{

}
public class AddScanMsg:BaseMsg
{
    public AddScanMsg(int invitation_id)
    {
        this.invitation_id = invitation_id;
    }

    public int invitation_id { get; set; }
}
public class GetPlateInvitationMsg:BaseMsg
{
    public GetPlateInvitationMsg(int plate)
    {
        this.plate = plate;
    }

    public int plate { get; set; }
}
public class LikeMsg:BaseMsg
{
    public LikeMsg(int comment_id, int invitation_id, int user_id)
    {
        this.comment_id = comment_id;
        this.invitation_id = invitation_id;
        this.user_id = user_id;
    }

    public int comment_id { get; set; }
    public int invitation_id { get; set; }
    public int user_id { get; set; }
}
public class AddPostMsg:BaseMsg
{
    public AddPostMsg(string content, string invitation_title, int plate, int post_user)
    {
        this.content = content;
        this.invitation_title = invitation_title;
        this.plate = plate;
        this.post_user = post_user;
    }

    public string content { get; set; } 
    public string invitation_title { get; set; }
    public int plate { get; set; }
    public int post_user { get;set; }

}
