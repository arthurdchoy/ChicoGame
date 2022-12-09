using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject CreditPage;
    [SerializeField]
    GameObject MenuPage;
    GameObject CurrentPage;
    // Start is called before the first frame update
    void Start()
    {

        CurrentPage = MenuPage;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Creidts()
    {
        CurrentPage.SetActive(false);
        CreditPage.SetActive(true);
        CurrentPage = CreditPage;
    }
    public void back()
    {
        CurrentPage.SetActive(false);
        MenuPage.SetActive(true);
        CurrentPage = MenuPage;
    }
}
