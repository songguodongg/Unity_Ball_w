using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class TimeUtil 
{
   public enum TimeKey
    {
       Second,
       Miniture,
       Hour,
       Day,
       Month,
       Year
    }
    /// <summary>
    /// 按格式输出时分秒
    /// 00:00:00
    /// </summary>
    /// <param name="sec">总秒数</param>
    /// <returns></returns>
    public  string GetTime(long totalSec)
    {

        

        string time = "";
        int hour =(int)totalSec /3600 ;
        int min = (int)totalSec % 3600 / 60;
        int sec = (int)totalSec % 60;
        
        string h = "";//定义小时为个位数的时候的补足位
        string m = "";//定义分钟为个位数的时候的补足位
        string s = "";//定义秒为个位数的时候的补足位
        if (hour < 10)//当小时为个位数的时候
        {
            h = "0";//将小时补足位设为0
        }
        if (min < 10)//当分钟为个位数的时候
        {
            m = "0";//将分钟补足位设为0
        }
        if (sec < 10)//当秒为个位数的时候
        {
            s = "0";//将秒补足位设为0
        }

        time = 0+"天"+(h + hour + ":" + m + min + ":" + s + sec);
        Debug.Log(time);
        return time;
    }
    /// <summary>
    /// 获取秒数对应的时间集合：天,小时，分，秒
    /// </summary>
    /// <param name="totalSec">总秒数</param>
    /// <param name="timeKey">最大显示单位</param>
    /// <returns></returns>
    public List<long> GetTime(long totalSec,TimeKey timeKey)
    {
        List<long> list= new List<long>();
        long day=0;
        long hour=0;
        long min =0;
        long sec=0;
        
        switch (timeKey)
        {
            case TimeKey.Second:
                sec = totalSec;
                break;
            case TimeKey.Miniture:
                sec= totalSec % 60;
                min = totalSec / 60;
                break;
            case TimeKey.Hour:
                hour = totalSec / 3600;
                min = totalSec % 3600 / 60;
                sec = totalSec % 60;
                break;
            case TimeKey.Day:
                 day = totalSec / (3600 * 24);
                 hour = totalSec % (3600 * 24) / 3600;
                 min = totalSec % 3600 / 60;
                 sec = totalSec % 60;
                break;
            default:
                break;
        }
       // Debug.Log("时间" + day.ToString() + " " +hour.ToString() + " "+ min.ToString()+" "+sec.ToString());

        return list;
    }


    public string GetOutPutStr(List<long> list,char splitChar,bool isAddZero)
    {
        string time = "";
        


        return time;
    }


}
