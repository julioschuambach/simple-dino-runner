using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown)
            SceneManager.LoadScene("Gameplay");
    }
}
