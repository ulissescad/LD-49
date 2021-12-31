using TMPro;
using UnityEngine;

public class UiElement : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void UpdateUI(string value)
    {
        _text.text = value;
    }

    public void DisableUI()
    {
        GetComponent<Animator>().SetBool("action",false);
        Destroy(this.gameObject,0.25f);
    }
    
    public void DisableUI(float time)
    {
        GetComponent<Animator>().SetBool("action",false);
        Destroy(this.gameObject,time);
        Debug.Log("dentro do ui cobo");
    }
}