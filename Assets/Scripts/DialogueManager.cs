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
    
    
    private DialogueNodes nodes;
    private DialogueNode currentNode;
    private bool active;
    private bool is_writing;
    private AudioSource music;
    
    void Start()
    {
        nodes = JsonUtility.FromJson<DialogueNodes>(jsonFile.text);
        
        if (nodes == null) {Debug.Log("Json error");}

        textBox.SetActive(false);

        music = GetComponent<AudioSource>();
        music.Stop();
    }

    // Update is called once per frame
    void Update()
    {
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

    

    private void reply(uint optionIndex)
    {
        if (optionIndex >= currentNode.answers.Length)
        {
            return;
        }

        int id = currentNode.answers[optionIndex].next;
        
        // bad ending, restart scene
        if (id == -1)
        {
            active = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        // good ending, end dialogue
        else if (id == -2)
        {
            Destroy(textBox.gameObject);
        }
        // end game ending
        else if (id == -3)
        {
            is_writing = false;
            Destroy(textBox.gameObject);
            
        }
        // succesful, goto next level
        else
        {
            if (id == 2)
            {
                music.Play();
            }
            DialogueNode reply = getNodeByID(id);
            DisplayNode(reply);  
        }
   
    }
    private void checkForInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            reply(0);
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
        
        for (int i=0; i<currentNode.answers.Length;i++)
        {
            currentText += "\n  " + (i+1) + " - " + currentNode.answers[i].text;
        }

        text.text = currentText;
        is_writing = false;
    }
}
