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
    public AddPostMsg(string content, string invitation_title, int plate, int post_user, int invitation_type, int school_id)
    {
        this.content = content;
        this.invitation_title = invitation_title;
        this.plate = plate;
        this.post_user = post_user;
        this.invitation_type = invitation_type;
        this.school_id = school_id;
    }

    public string content { get; set; } 
    public string invitation_title { get; set; }
    public int plate { get; set; }
    public int post_user { get;set; }
    public int invitation_type { get; set; }
    public int school_id { get; set; }

}
public class GetPlateInvititionMsg:BaseMsg
{
    public int index { get; set; }
    public int plate { get; set; }
}
public class GetInvitationMsg:BaseMsg
{

}
public class AddCommentMsg:BaseMsg
{
    public AddCommentMsg(int comment_invitation, int comment_user, string content)
    {
        this.comment_invitation = comment_invitation;
        this.comment_user = comment_user;
        this.content = content;
    }

    public int comment_invitation { get; set; }
    public int comment_user { get; set; }
    public string content { get; set; }
}

public class JudgeLikeMsg:BaseMsg
{
    public JudgeLikeMsg()
    {
    }

    public JudgeLikeMsg(int comment_id, int user_id)
    {
        this.comment_id = comment_id;
        this.user_id = user_id;
    }

    public int comment_id { get; set; }
    public int user_id { get; set; }
}

public class GetLikeCountMsg:BaseMsg
{
    public GetLikeCountMsg(int comment_id)
    {
        this.comment_id = comment_id;
    }

    public int comment_id { get; set; }
}
