using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DestinationHandler : MonoBehaviour
{
    public GameObject winPanel;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Contains("Destination"))
        {
            // Game completed
            winPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
