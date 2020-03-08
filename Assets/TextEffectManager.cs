using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEditor.Presets;
using UnityEngine;

public class TextEffectManager : MonoBehaviour
{
    public enum Tags
    {
        Default,
        Delete,
        GirlName,
    }
    public string a = "<she starts <tag1>to behavior in </tag1>a pretty wired <tag2>way. However, other people are</tag2> apparently not realizing this.";
    public string girlName;

    void Start()
    {
        Debug.Log(a);
        Debug.Log(ProcessingTags(a));
    }

    public string ProcessingTags(string sentence)
    {
        var splitSentenceList = Regex.Split(sentence, @"(?=[<])|(?<=[>])").ToList();
        while (_GetPairTag(splitSentenceList,out KeyValuePair<Tags,string> startTag, out KeyValuePair<Tags,string> endTag))
        {
            splitSentenceList = _DoTagBehaviorAndCleanTag(startTag, endTag, splitSentenceList);
            //_PrintString(splitSentenceList);
        }
        return _ListToString(splitSentenceList);
    }


    private bool _GetPairTag(List<string> stringList,out KeyValuePair<Tags,string> startTag, out KeyValuePair<Tags,string> endTag)
    {
        for (int i = 0; i < stringList.Count; i++)
        {
            if (_GetActiveTag(stringList[i], out bool isStart,out Tags tagType) == Tags.Default) continue;
            if (isStart)
            {
                startTag = new KeyValuePair<Tags, string>(tagType,stringList[i]);
                continue;
            }
            //is end
            endTag = new KeyValuePair<Tags, string>(tagType,stringList[i]);
            Debug.Assert(endTag.Key == startTag.Key,"tag "+ endTag.Key + "overlay with tag " + startTag.Key);
            return true;
        }

        return false;
    }
    private Tags _GetActiveTag(string testString, out bool isStart, out Tags tagType)
    {
        isStart = false;
        tagType = Tags.Default;
        
        if (!testString.StartsWith("<") || !testString.EndsWith(">")) return Tags.Default;
        
        var tagStr = testString.Substring(1, testString.Length - 2).Trim();
        if (tagStr.StartsWith("/"))
        {
            tagStr = tagStr.Substring(1);
            isStart = false;
        }
        else
            isStart = true;
        
        return Enum.TryParse<Tags>(tagStr, out tagType)? tagType : Tags.Default;
    }
    private List<string> _DoTagBehaviorAndCleanTag(KeyValuePair<Tags,string> startTag, KeyValuePair<Tags,string> endTag, List<string> operateString)
    {
        var startPos = operateString.IndexOf(startTag.Value);
        var endPos = operateString.IndexOf(endTag.Value);
        switch (startTag.Key)
        {
            case Tags.Delete:
                for (int i = startPos + 1; i < endPos; i++) operateString[i] = "";
                break;
            case Tags.GirlName:
                for (int i = startPos + 2; i < endPos; i++) operateString[i] = "";
                operateString[startPos + 1] = girlName; 
                break;
        }

        operateString.Remove(startTag.Value);
        operateString.Remove(endTag.Value);
        
        return operateString;
    }
    private void _PrintString(IEnumerable b)
    {
        foreach (var sentence in b)
        {
            Debug.Log(sentence);
        }
    }
    private string _ListToString(List<string> list)
    {
        var str = "";
        foreach (var item in list)
            str += item;
        return str;
    }
}
