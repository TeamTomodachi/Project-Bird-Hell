using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager
{
    private static NavigationManager m_instance;
    public static NavigationManager Instance
    {
        get
        {
            if (m_instance == null) m_instance = new NavigationManager();
            return m_instance;
        }
    }

    public Scene Current { get; private set; }
    public object Parameter { get; private set; }

    public NavigationManager()
    {
        Current = SceneManager.GetActiveScene();
        Parameter = null;
    }

    public void Navigate(string name, object parameter)
    {
        Current = SceneManager.GetSceneByName(name);
        Parameter = parameter;

        SceneManager.LoadScene(name);
    }

    public void Reset()
    {
        SceneManager.LoadScene(Current.name);
    }

}
