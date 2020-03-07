using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEffectManager : MonoBehaviour
{
    public enum Tags
    {
        
    }
    public string a = "she starts <tag1>to behavior in </tag1>a pretty wired <tag2>way. However, other people are</tag2> apparently not realizing this.";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public List<Tags> GetTags(string sentence, out string cleanSentence)
    {
        
    }

    private void _PrintString(List<string> b)
    {
        foreach (var sentence in b)
        {
            Debug.Log(sentence);
        }
    }

    private string _PutStringBack(List<string> b)
    {
        var str = "";
        foreach (var sentence in b)
        {
            str += sentence;
        }

        return str;
    }
}
