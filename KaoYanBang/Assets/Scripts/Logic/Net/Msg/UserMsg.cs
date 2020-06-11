using liulaoc.Net.Http;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterMsg : BaseMsg
{
    public RegisterMsg(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
    public string username { get; set; }
    public string password { get; set; }

}
public class LoginMsg : BaseMsg
{
    public LoginMsg(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
    public string username { get; set; }
    public string password { get; set; }
}
public class GetUserMsg : BaseMsg
{
    public GetUserMsg(string username)
    {
        this.username = username;
    }

    public string username { get; set; }
}
public class GetUserByIdMsg : BaseMsg
{
    public GetUserByIdMsg(int user_id)
    {
        this.user_id = user_id;
    }

    public int user_id { get; set; }
}
public class ChangeSchoolIdMsg:BaseMsg
{
    public ChangeSchoolIdMsg(int school_id, int user_id)
    {
        this.school_id = school_id;
        this.user_id = user_id;
    }

    public int school_id { get; set; }
    public int user_id { get; set; }
}
public class ChangeSubjectIdMsg:BaseMsg
{
    public ChangeSubjectIdMsg(int subject_id, int user_id)
    {
        this.subject_id = subject_id;
        this.user_id = user_id;
    }

    public int subject_id { get; set; }
    public int user_id { get; set; }
}
