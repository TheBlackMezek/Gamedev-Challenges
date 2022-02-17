using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public void OpenScene01() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("01.SimplePlayerController");
    }

    public void OpenScene02() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("02.1stPersonController");
    }

}
