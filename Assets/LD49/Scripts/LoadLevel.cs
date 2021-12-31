using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private string _levelName;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Load);
    }

    // Update is called once per frame
    void Load()
    {
        SceneManager.LoadScene(_levelName);
    }
}
