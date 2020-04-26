using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class GenClassFromCSV
{
    [MenuItem("Assets/Config Table Tools/Generate Class From Current Directory")]
    static void GenClassFromCurrentDirectory()
    {
        var GUIDs = Selection.assetGUIDs;
        if (GUIDs.Length <= 0) return;
        var dirPath = Path.GetDirectoryName(AssetDatabase.GUIDToAssetPath(GUIDs[0]));
        var dirInfo = new DirectoryInfo(dirPath);
        var files = dirInfo.GetFiles();

        foreach (var file in files)
        {
            var path = file.FullName;

            // 判断后缀名是否为csv
            if (!Path.GetExtension(path).Equals(".csv")) continue;

            Debug.LogFormat("[ConfigTable]: Processing '{0}'.", path);
            ProcessOneCSVFile(path);
            Debug.LogFormat("[ConfigTable]: '{0}' finish.", path);
        }
        AssetDatabase.Refresh();
    }

    [MenuItem("Assets/Config Table Tools/Refresh")]
    static void RefreshTablePath()
    {
        var GUIDs = Selection.assetGUIDs;
        if (GUIDs.Length != 1) return;

        var path = AssetDatabase.GUIDToAssetPath(GUIDs[0]);
        var registry = AssetDatabase.LoadAssetAtPath<TablesInfo>(path);
        if (registry == null) return;

        var dirPath = Path.GetDirectoryName(path);
        var dirInfo = new DirectoryInfo(dirPath);
        var files = dirInfo.GetFiles();
        registry.tablePaths.Clear();

        string resourcePath = Application.dataPath + "/Resources/";

        foreach (var file in files)
        {
            var filePath = file.FullName;

            if (!Path.GetExtension(filePath).ToLower().Equals(".csv")) continue;

            var record = Path.GetDirectoryName(filePath) + "\\" + Path.GetFileNameWithoutExtension(filePath);
            registry.tablePaths.Add(record.Substring(resourcePath.Length));
        }
        EditorUtility.SetDirty(registry);
        AssetDatabase.Refresh();
    }


    static bool ProcessOneCSVFile(string path)
    {
        // 读取csv文件, 生成相应实体类
        // csv文件前几行：
        // Fields
        // Types
        // IsPrimary
        // Description

        var lines = File.ReadAllLines(path);

        var classInfo = new Dictionary<string, string>();
        var fields = lines[0].Split(',');
        var types = lines[1].Split(',');
        var isPrimarys = lines[2].Split(',');

        // 处理前两行
        for (var i = 0; i < fields.Length; i++)
        {
            if (classInfo.ContainsKey(fields[i]))
            {
                Debug.LogErrorFormat("[ConfigTable]: Field '{0}' is already contained in '{1}'. Skip this file.", fields[i], path);
                return false;
            }
            classInfo.Add(fields[i], types[i]);
        }

        var className = Path.GetFileNameWithoutExtension(path);
        var code = TableClassTemplate.GenerateClass(classInfo, className, isPrimarys);

        UnityIOHelper.SaveToFile(code, TableClassTemplate.classOutputFolder + className + ".cs");
        return true;
    }
}
