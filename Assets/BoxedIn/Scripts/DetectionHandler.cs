using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetectionHandler : MonoBehaviour
{
    public GameObject detectionPanel;
    public Image panelImage;
    public TextMeshProUGUI panelLine;

    public Color detectedColour;
    public Color undetectedColour;
    public string detectedLine;
    public string undetectedLine;

    public Dictionary<string, bool> detectedBy;
    [SerializeField] bool isPlayerDetected;

    private void Start()
    {
        panelImage = detectionPanel.GetComponent<Image>();
        panelLine = detectionPanel.GetComponentInChildren<TextMeshProUGUI>();
        detectedBy = new Dictionary<string, bool>();
    }

    private void Update()
    {
        if(detectedBy.Count == 0)
        {
            panelImage.color = undetectedColour;
            panelLine.text = undetectedLine;
            isPlayerDetected = false;
        }
        else
        {
            panelImage.color = detectedColour;
            panelLine.text = detectedLine;
            isPlayerDetected = true;
        }
    }

    public void PlayerDetected(string _detectedBy)
    {
        if(!detectedBy.ContainsKey(_detectedBy))
        {
            detectedBy.Add(_detectedBy, true);
        }
    }
    public void PlayerUndetected(string _undetectedBy)
    {
        if(detectedBy.ContainsKey(_undetectedBy)) 
        {
            detectedBy.Remove(_undetectedBy);
        }
    }
}
