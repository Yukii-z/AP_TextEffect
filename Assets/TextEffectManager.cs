using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
    }
    public string a = "<she starts <tag1>to behavior in </tag1>a pretty wired <tag2>way. However, other people are</tag2> apparently not realizing this.";


    void Start()
    {
        var str = "";
        GetTags(a, out str);
    }

    public List<Tags> GetTags(string sentence, out string cleanSentence)
    {
        var splitSentenceList = Regex.Split(sentence, @"(?=[<])|(?<=[>])");
        _PrintString(splitSentenceList);
        var hasTag = true;
        while (hasTag)
        {
            var targetTag
        }
        foreach (var parameter in splitSentenceList)
        {
            if(_GetActiveTag(parameter) != Tags.Default) 
        }
        Debug.Log(_GetActiveTag("<tag1>"));
        cleanSentence = "";
        return new List<Tags>();
    }

    private Tags _GetActiveTag(string testString)
    {
        if (!testString.StartsWith("<") || !testString.EndsWith(">")) return Tags.Default;
        var tagStr = testString.Substring(1, testString.Length - 2).Trim();
        return Enum.TryParse<Tags>(tagStr, out Tags parseTag)? parseTag : Tags.Default;
    }
    private void _PrintString(IEnumerable b)
    {
        foreach (var sentence in b)
        {
            Debug.Log(sentence);
        }
    }

    private string _PutStringBack(IEnumerable b)
    {
        var str = "";
        foreach (var sentence in b)
        {
            str += sentence;
        }

        return str;
    }
}
