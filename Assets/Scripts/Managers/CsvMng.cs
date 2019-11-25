using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;

public class CsvMng : MonoSingleton<CsvMng>
{
    //public List<string[]> mapData;
    public Dictionary<string, Dictionary<string, string>> pathData;
    public Dictionary<string, Dictionary<string, string>> bookData;

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void Start()
    {
        pathData = LoadFileToDic("Csv", "Path");
        bookData = LoadFileToDic("Csv", "Book");
    }

    // 读出文件为list, map生成用
    public List<string[]> LoadFileToList(string path, string fileName)
    {
        List<string[]> arrayData = new List<string[]>(); //储存表格数据的数组
        //arrayData.Clear();
        string text = "";
        //StreamReader sr = null;
        string filePath = path + "\\" + fileName; //设定路径
        try
        {
            //Debug.Log("File Find in " + filePath);
            text = Resources.Load<TextAsset>(filePath).text;
            //sr = File.OpenText(filePath);
        }
        catch
        {
            //打开文件失败
            Debug.Log(filePath + " File can't find!");
            return arrayData;
        }

        //string line; //读取配置表时记录一行数据
        string[] data = text.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        foreach (var d in data)
        {
            arrayData.Add(d.Split(',')); //逗号分隔
        }

        return arrayData;
    }
    // 读出文件为dictionary，其他用
    public Dictionary<string, Dictionary<string, string>> LoadFileToDic(string path, string fileName)
    {
        //dicData.Clear();
        Dictionary<string, Dictionary<string, string>> dicData = new Dictionary<string, Dictionary<string, string>>();
        // 先读成list形式
        List<string[]> data = LoadFileToList(path, fileName);
        //Debug.Log(data.Count);
        //获取表头属性， 敌人速度，血条等
        string[] attributes;
        attributes = data[0];
        //遍历list，添加到字典里
        for (int i = 1; i < data.Count; ++i)
        {
            Dictionary<string, string> dicLine = new Dictionary<string, string>();
            for (int j = 1; j < data[i].Length; ++j)
            {
                //Debug.Log(data[i][j]);
                dicLine.Add(attributes[j], data[i][j]);
            }
            dicData.Add(data[i][0], dicLine);
        }

        return dicData;
    }

}