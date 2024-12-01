using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public string npcName;
    public string dialogue;

    public void Talk()
    {
        Debug.Log(npcName + ": " + dialogue);
    }
}

