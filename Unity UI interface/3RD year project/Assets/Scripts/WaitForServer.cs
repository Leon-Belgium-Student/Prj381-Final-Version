using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitForServer : MonoBehaviour
{
    public List<Button> ButtonsToActivate;
    public Button homeBtn;
    public bool WaitDone = false;
    public bool WaitStarted = false;

    public void StartWait()
    {
        StartCoroutine(WaitForSeconds());
    }

    private void Update()
    {
        if (homeBtn != null)
        {
            if (WaitStarted)
            {
                homeBtn.interactable = false;
            }
            else
            {
                homeBtn.interactable = true;
            }
        }
        
    }
    public IEnumerator WaitForSeconds()
    {
        WaitStarted = true;
        WaitDone = false;
        yield return new WaitForSeconds(10f);
        ActivateBtns();
    }

    public void ActivateBtns()
    {
        foreach (var btn in ButtonsToActivate)
        {
            btn.interactable = true;
        }
        WaitStarted = false;
        WaitDone = true;
    }
}
