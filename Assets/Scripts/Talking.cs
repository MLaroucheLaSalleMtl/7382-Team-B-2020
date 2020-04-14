using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talking : MonoBehaviour
{
    public NPC npc;

    public void TriggerNPC()
    {
        FindObjectOfType<Dialog_Manager>().StartTalking(npc);
    }
}
