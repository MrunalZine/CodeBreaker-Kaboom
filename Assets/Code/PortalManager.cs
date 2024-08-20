using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public GameObject[] portals; // Assign all portal GameObjects here
    public string[] associatedCodeBlockIDs; // Unique IDs corresponding to code blocks

    void Start()
    {
        // Check if portals should be deactivated based on code block destruction
        for (int i = 0; i < portals.Length; i++)
        {
            if (PlayerPrefs.GetInt(associatedCodeBlockIDs[i], 0) == 1)
            {
                portals[i].SetActive(false);
            }
        }
    }
}
