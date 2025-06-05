using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public Button LoginBth;
    public Button optionBth;
    public Button cuiderUI;
    // Start is called before the first frame update
    void Start()
    {
        cuiderUI.gameObject.SetActive(false);

        LoginBth.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });
        optionBth.onClick.AddListener(() =>
        {
            cuiderUI.gameObject.SetActive(true);
        });
        cuiderUI.onClick.AddListener(() =>
        {
            cuiderUI.gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
