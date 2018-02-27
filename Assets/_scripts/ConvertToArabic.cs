using System.Collections;
using System.Collections.Generic;
using ArabicSupport;
using TMPro;
using TMPro.Examples;
using UnityEngine;

//[ExecuteInEditMode]
public class ConvertToArabic : MonoBehaviour {

    public TMP_Text targettxt;

    public static ConvertToArabic Instance;

    void Start () {
        PlayText();
    }

    public static string FixMyArabic (string source) {
        string temp = ArabicFixer.Fix (source);
        return ReverseText (temp);;
    }

    public string FixArabicEditor (string source) {
        targettxt.isRightToLeftText = true;
        targettxt.alignment = TextAlignmentOptions.Midline;
        string temp = ArabicFixer.Fix (source);
        return ReverseText (temp);;
    }
    static string ReverseText (string source) {
        char[] output = new char[source.Length];
        for (int i = 0; i < source.Length; i++) {
            output[(output.Length - 1) - i] = source[i];
        }
        return new string (output);
    }

    public void PlayText () {

        targettxt = GetComponent<TMP_Text> ();
        if (targettxt != null) {

            targettxt.text = FixArabicEditor (targettxt.text);
            
        }
    }

}