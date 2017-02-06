using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceManager : MonoBehaviour {
    public string[] possibleKeywords;
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
    void Start () {

        for (int i = 0; i < possibleKeywords.Length; i++) {
            switch (possibleKeywords[i]) {
                case "Start Game":
                    keywords.Add("Start Game", () => { NavigateToScene.GoToScene("Levels");
                    });
                    break; 

                case "Exit Game":
                    keywords.Add("Exit Game", () => { Application.Quit();
                    });
                    break;

                case "Restart Game":
                    keywords.Add("Restart Game", () => { NavigateToScene.GoToScene("Levels");
                    });
                    break;
            }
        }
        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();

    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}