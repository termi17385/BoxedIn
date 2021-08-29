using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePauseScript : MonoBehaviour
{
    public GameObject pausePanel;
    [SerializeField]bool isOn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }
        
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isOn)
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0;
                isOn = true;
            }
            else if(isOn)
            {
                pausePanel.SetActive(false);
                Time.timeScale = 1;
                isOn = false;
            }
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        isOn = false;
    }
}
