using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    Animator animator;
    private TitlePageSoundEffects SFX;
    MainMenuManager menuManager;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        SFX = GameObject.Find("SoundEffects").GetComponent<TitlePageSoundEffects>();
        menuManager = GameObject.Find("MenuBackground").GetComponent<MainMenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && animator.GetInteger("state") == 2)
        {
            animator.SetInteger("state", 0);
        }
    }
   public void QuitGame()
    {
        Application.Quit();
    }
    public void StartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void playMusic(string name)
    {
        SFX.playMusic(name);
    }
    public void back()
    {
        menuManager.back();
    }
    public void credit()
    {
        menuManager.Creidts();
    }
}
