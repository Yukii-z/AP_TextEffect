using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class TextEffectManager : MonoBehaviour
{
    public enum Tags
    {
        
    }
    public string a = "<she starts <tag1>to behavior in </tag1>a pretty wired <tag2>way. However, other people are</tag2> apparently not realizing this.";
    // Start is called before the first frame update
    public TextEffectManager()
    {
    }

    void Start()
    {
        var str = "";
        GetTags(a, out str);
    }

    // Update is called once per frame
    public List<Tags> GetTags(string sentence, out string cleanSentence)
    {
        var wordList = sentence.Split("<"[0],">"[0]).ToList();
        var match = Regex.Matches(a, "[<->]");
        for (int i = 1; i < wordList.Count; i++)
        {
            //wordList[i] = "<" + wordList[i];
        }
        _PrintString(match);
        cleanSentence = "";
        return new List<Tags>();
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
