using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    [Header("Menu GameObjects")]
    
    public GameObject homeMenu;
    public GameObject registerMenu;


    public Menu _menu;

    public enum Menu
    {
        Loading = 1,
        Home,
        Register
    }

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
        _menu = Menu.Home;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (_menu)
        {
            case Menu.Loading:
               
                homeMenu.SetActive(false);
                registerMenu.SetActive(false);
                break;
            case Menu.Home:
                
                homeMenu.SetActive(true);
                registerMenu.SetActive(false);

                break;
            case Menu.Register:
                
                homeMenu.SetActive(false);
                registerMenu.SetActive(true);

                break;
        }
    }

    public void ChangeMenu(int menu)
    {
        _menu = (Menu)menu;
    }
}
