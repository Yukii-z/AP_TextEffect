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
    public abstract class GameTagPair
    {
        public string start;
        public string end;
        public virtual GameTagPair Do(string targetText)
        {
            //functions
            return this;
        }
    }
    
    public enum Tags
    {
        Default,
        tag1,
        tag2,
    }
    public string a = "<she starts <tag1>to behavior in </tag1>a pretty wired <tag2>way. However, other people are</tag2> apparently not realizing this.";


    void Start()
    {
        Debug.Log(a);
        Debug.Log(ProcessingTags(a));
    }

    public string ProcessingTags(string sentence)
    {
        var splitSentenceList = Regex.Split(sentence, @"(?=[<])|(?<=[>])").ToList();
        _PrintString(splitSentenceList);
        var hasTag = true;
        while (_GetPairTag(splitSentenceList,out KeyValuePair<Tags,int> startTag, out KeyValuePair<Tags,int> endTag))
        {
            splitSentenceList = _DoTagBehavior(startTag, endTag, splitSentenceList);
        }
        return _ListToString(splitSentenceList);
    }


    private bool _GetPairTag(List<string> stringList,out KeyValuePair<Tags,int> startTag, out KeyValuePair<Tags,int> endTag)
    {
        for (int i = 0; i < stringList.Count; i++)
        {
            if (_GetActiveTag(stringList[i], out bool isStart,out Tags tagType) == Tags.Default) continue;
            if (isStart)
            {
                startTag = new KeyValuePair<Tags, int>(tagType,i);
                continue;
            }
            //is end
            endTag = new KeyValuePair<Tags, int>(tagType,i);
            Debug.Assert(endTag.Key == startTag.Key,"tag "+ endTag.Key + "overlay with tag " + startTag.Key);
            return true;
        }

        return false;
    }
    private Tags _GetActiveTag(string testString, out bool isStart, out Tags tagType)
    {
        isStart = false;
        tagType = default;
        
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
    private void _PrintString(IEnumerable b)
    {
        foreach (var sentence in b)
        {
            Debug.Log(sentence);
        }
    }

    private List<string> _DoTagBehavior(KeyValuePair<Tags,int> startTag, KeyValuePair<Tags,int> endTag, List<string> operateString)
    {
        switch (startTag.Key)
        {
            case Tags.tag1:
                break;
            case Tags.tag2:
                break;
        }
        return operateString;
    }
    private string _ListToString(IEnumerable list)
    {
        var str = "";
        foreach (var item in list)
            str += list;
        return str;
    }
}
