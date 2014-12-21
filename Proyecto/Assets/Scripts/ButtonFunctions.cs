using UnityEngine;
using System.Collections;

public class ButtonFunctions : MonoBehaviour
{
    public void exit()
    {
        Application.Quit();
    }

    public void menu()
    {
        Application.LoadLevel("mainMenu");
    }
}
