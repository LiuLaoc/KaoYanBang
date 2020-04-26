using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class TableClassTemplate
{
    public const string classOutputFolder = @"Assets/Scripts/AutoGenerate/ConfigTable/";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="classInfo">Field-Type键值对</param>
    /// <param name="className">生成的类名</param>
    /// <param name="isPrimarys">是否为主键</param>
    /// <returns></returns>
    public static string GenerateClass(Dictionary<string, string> classInfo, string className, string[] isPrimarys)
    {
        var code = "";
        //namespace
        SetNameSpace("System.Collections.Generic", ref code);
        // IRecord
        SetClassName(className, ref code, "IRecord");
        code += "{\n";
        SetFields(classInfo, ref code);
        code += "}\n\n";

        // ATable
        SetClassName(className + "Table", ref code, "ATable");
        code += "{\n";
        // GetRecord
        code += string.Format("    public {0} GetRecord(", className);

        var index = 0;
        var args = new List<string>();
        foreach (var pair in classInfo)
        {
            if (isPrimarys[index].Equals("1"))
            {
                if (index != 0)
                {
                    code += ", ";
                }
                code += string.Format("{0} {1}", pair.Value, pair.Key);
                args.Add(pair.Key);
            }
            index++;
        }
        code += ")\n\t{\n";
        code += string.Format("\t\treturn ({0}) this[", className);
        SetKey(args, ref code);
        code += "];\n";
        code += "    }\n";

        // AddRecord
        code += string.Format("    void AddRecord(");
        index = 0;
        foreach (var pair in classInfo)
        {
            if (index != 0)
            {
                code += ", ";
            }
            code += string.Format("{0} {1}", pair.Value, pair.Key);
            index++;
        }
        code += ")\n    {\n";
        code += "        string key = ";
        SetKey(args, ref code);
        code += ";\n";
        code += string.Format("        this[key] = new {0}();\n", className);
        foreach (var pair in classInfo)
        {
            code += string.Format("        (this[key] as {0}).{1} = {1};\n", className, pair.Key);
        }
        code += "    }\n";

        //GetAll
        code += string.Format("\tpublic List<{0}> GetAll()\n", className);
        code += "\t{\n";
        code += "\t\tvar list = GetAllRecord();\n";
        code += string.Format("\t\tvar newList = new List<{0}>();\n", className);
        code += "\t\tforeach(var record in list)\n";
        code += "\t\t{\n";
        code += string.Format("\t\t\tnewList.Add(({0})record);\n", className);
        code += "\t\t}\n";
        code += "\t\treturn newList;\n";
        code += "\t}\n";

        code += "}\n";
        return code;
    }

    static void SetKey(List<string> args, ref string code)
    {
        var index = 0;
        foreach (var arg in args)
        {
            //Debug.Log(arg);
            if (index != 0)
            {
                code += " + ";
            }
            code += string.Format("{0} + \"_\"", arg);
            index++;
        }
    }

    /// <summary>
    /// 设置命名空间行
    /// </summary>
    /// <param name="nameSpace"></param>
    /// <param name="code"></param>
    static void SetNameSpace(string nameSpace, ref string code)
    {
        code += string.Format("using {0};\n", nameSpace);
    }
    /// <summary>
    /// 设置类名行
    /// </summary>
    /// <param name="className">类名</param>
    /// <param name="code">传入生成代码引用</param>
    /// <param name="inhert">继承的类名、接口名</param>
    static void SetClassName(string className, ref string code, string inhert)
    {
        code += string.Format("public class {0} : {1}\n", className, inhert);
    }
    static void SetFields(Dictionary<string, string> classInfo, ref string code)
    {
        foreach (var kvp in classInfo)
        {
            SetField(kvp.Value, kvp.Key, ref code);
        }
    }
    static void SetField(string type, string name, ref string code)
    {
        code += string.Format("    public {0} {1};\n", type, name);
    }
}
