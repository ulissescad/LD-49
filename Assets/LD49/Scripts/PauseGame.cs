using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void Pause()
    {
        if (Time.timeScale == 1)
        {
            StartCoroutine(Pause());
            IEnumerator Pause()
            {
            yield return new WaitForSeconds(0.7f);
            Time.timeScale = 0;
            }
        }

        else
            Time.timeScale = 1;
    }
}
