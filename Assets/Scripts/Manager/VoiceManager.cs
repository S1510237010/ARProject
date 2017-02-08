using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

/// <summary>
/// Manages all voice commands
/// </summary>
public class VoiceManager : MonoBehaviour {
    public string[] possibleKeywords;
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    /// <summary>
    /// Adds all necessary keywords to the keywordRecognizer with Lambda expressions doing the next steps
    /// </summary>
    void Start () {

        for (int i = 0; i < possibleKeywords.Length; i++) {
            switch (possibleKeywords[i]) {
                case "Start Game":
                    keywords.Add("Start Game", () => { NavigateToScene.GoToScene("Levels"); print("Start Game"); });
                    break; 

                case "Exit Game":
                    keywords.Add("Exit Game", () =>{ Application.Quit();});
                    break;

                case "Restart Game":
                    keywords.Add("Restart Game", () => { NavigateToScene.GoToScene("Levels");});
                    break;
            }
        }

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();

    }

    /// <summary>
    /// Is called when a keyword is recognized and invoces the Lambda expression
    /// </summary>
    /// <param name="args">Keyword can be read from the object</param>
    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}