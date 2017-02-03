using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class StartGame : MonoBehaviour {

    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

	// Use this for initialization
	void Start () {
        keywords.Add("Start Game", () =>
        {
            this.BroadcastMessage("OnStart");
        });

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
	}

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args) {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction)) {
            keywordAction.Invoke();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    bool StartingGame()
    {
        return true;
    }

}
