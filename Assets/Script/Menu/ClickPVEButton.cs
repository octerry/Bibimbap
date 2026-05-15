using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickPVEButton : MonoBehaviour
{
    void Start () {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void TaskOnClick()
    {
        SceneManager.LoadScene("Arena");
    }
}
