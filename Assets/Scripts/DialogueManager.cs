using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

[System.Serializable]
public class AnswerOption
{
    public string text;
    public int next;
}

[System.Serializable]
public class DialogueNode
{
    public int id;
    public string text;
    public float delayBetweenCharacters;
    public int nextId;
    public AnswerOption[] answers;
}

[System.Serializable]
public class DialogueNodes
{
    public DialogueNode[] dialogueNodes;
}

public class DialogueManager : MonoBehaviour
{
    [Header("things")]
    public TextAsset jsonFile;
    public TextMeshProUGUI text;
    public GameObject textBox;
    public SuperEpicMegaVisuaNovelMegaBackground bg;
    public float tSpeed = 0.01f;

    [Header("Visual Novel Images")] 
    public SpriteRenderer haruTraining;
    public SpriteRenderer magicCircle;
    public SpriteRenderer evilHuizinga;
    
    private DialogueNodes nodes;
    private DialogueNode currentNode;
    private bool active;
    private bool is_writing;
    private bool charAppearActive = false;
    private bool charDissapearActive = false;
    private List<SpriteRenderer> _spritesToAppear = new List<SpriteRenderer>();
    private List<SpriteRenderer> _spritesToDisappear = new List<SpriteRenderer>();
    private float count = 0f;
    private float dcount = 1f;

    void Start()
    {
        nodes = JsonUtility.FromJson<DialogueNodes>(jsonFile.text);
        
        if (nodes == null) {Debug.Log("Json error");}

        textBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (charDissapearActive)
        {
            for (int i = 0; i < _spritesToDisappear.Count; i++ )
            {
                SpriteRenderer sr = _spritesToDisappear[i];
                sr.material.SetFloat("_Transition", dcount);
                Debug.Log("dissapear " + sr.material.name + " " + dcount);

            }
            dcount -= tSpeed;
            if (dcount <= 0)
            {
                charDissapearActive = false;
                _spritesToDisappear = new List<SpriteRenderer>();
                dcount = 1f;
            }  
        }
        if (charAppearActive)
        {
            for (int i = 0; i< _spritesToAppear.Count;i++ )
            {
                SpriteRenderer sr = _spritesToAppear[i];
                sr.material.SetFloat("_Transition", count);
                Debug.Log("appear " + sr.material.name + " " + count);
            }
            count += tSpeed;
            if (count >= 1)
            {
                count = 0f;
                charAppearActive = false;
            }  
        }
        if (active && !is_writing)
        {
            checkForInput();
        }
        else if (!active)
        {
            textBox.SetActive(true);
            
            DisplayNode(getNodeByID(0));
            active = true;
        }
    }

    void makeCharactersAppear()
    {
        charAppearActive = true;
    }
    
    void makeCharactersDissappear()
    {
        charDissapearActive = true;
    }
    
    private DialogueNode getNodeByID(int id)
    {
        foreach (var VARIABLE in nodes.dialogueNodes)
        {
            if (VARIABLE.id == id)
            {
                return VARIABLE;
            }
        }

        Debug.Log("requested dialogueNode with id " + id + ", but was not found");
        return null;
    }

    private void DisplayNode(DialogueNode node)
    {
        currentNode = node;
        StartCoroutine(displayText());
    }
    
    
    private void checkForInput()
    {
        if (currentNode.answers.Length == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                reply(-1);
            }
            return;
        }
        // reply functionality
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            reply(-1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            reply(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            reply(2);
        }
    }

    private IEnumerator displayText()
    {
        is_writing = true;
        string currentText = "";
        foreach (char c in currentNode.text)
        {
            currentText += c;
            text.text = currentText;
            if (c != ' ') {yield return new WaitForSeconds(currentNode.delayBetweenCharacters);}
        }
        
        
        // display answers
        for (int i=0; i<currentNode.answers.Length;i++)
        {
            currentText += "\n  " + (i+1) + " - " + currentNode.answers[i].text;
        }

        text.text = currentText;
        is_writing = false;
        
    }

    private void doSpecificThingOnId(int id)
    {
        
        List<int> changeBackgroundIds = new List<int>();
        changeBackgroundIds.AddRange(new List<int>
        {1,3,4,6});
        
        if (changeBackgroundIds.Contains(id))
        {
            bg.progressBackground();
        }
        
        // characters
        if (_spritesToAppear.Count > 0)
        {
            _spritesToDisappear = _spritesToAppear;
            makeCharactersDissappear();
        }
        List<SpriteRenderer> sprites = new List<SpriteRenderer>();
        if (id == 2)
        {
            sprites.AddRange(new List<SpriteRenderer>
                {haruTraining, magicCircle});
        }
        else if (id == 9)
        {
            sprites.AddRange(new List<SpriteRenderer>
                {evilHuizinga});
        }
        else
        {
            _spritesToAppear = new List<SpriteRenderer>();
            return;
        }
        _spritesToAppear = sprites;
        Debug.Log(_spritesToAppear);
        makeCharactersAppear();
        
        
        Debug.Log(_spritesToDisappear);
        _spritesToAppear = sprites;
        Debug.Log(_spritesToAppear);
        makeCharactersAppear();
    }
    
    private void reply(int optionIndex)
    {
        int id;
        
        if (optionIndex == -1)
        {
            // no replies, take next ID
            id = currentNode.nextId;
        }
        else if (optionIndex >= currentNode.answers.Length)
        {
            return;
        }
        else
        {
            id = currentNode.answers[optionIndex].next;
        }
        
        doSpecificThingOnId(id);
        DialogueNode reply = getNodeByID(id);
        DisplayNode(reply);  
    }
}
