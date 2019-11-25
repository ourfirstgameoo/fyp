using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class PlayerInfo : Singleton<PlayerInfo>
{
    public SaveInfo save = new SaveInfo();
    string filePath = Application.dataPath + "/Resources/PlayerInfo/PlayerInfo.json";

    public bool LoadByJson()
    {
        if (File.Exists(filePath))
        {
            //创建一个StreamReader，用来读取流
            StreamReader sr = new StreamReader(filePath);
            //将读取到的流赋值给jsonStr
            string jsonStr = sr.ReadToEnd();
            //关闭
            sr.Close();

            //将字符串jsonStr转换为Save对象
            save = JsonMapper.ToObject<SaveInfo>(jsonStr);

            return true;
        }
        else
        {
            Debug.Log("存档文件不存在");
            return false;
        }
    }

    public void SaveByJson()
    {
        //利用JsonMapper将save对象转换为Json格式的字符串
        string saveJsonStr = JsonMapper.ToJson(save);
        //将这个字符串写入到文件中
        //创建一个StreamWriter，并将字符串写入文件中
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(saveJsonStr);
        //关闭StreamWriter
        sw.Close();
    }
}
