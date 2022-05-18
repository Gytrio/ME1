using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Manager : MonoBehaviour
{
    public static List<Attractor> Attractors;

    [Header("Flags")]
    

    public bool readyToThrow;
    public bool isFreezing;
    public bool isAttracting;
    public bool isGrabbing;
    public bool isDischarged;
    public bool isDirecting;

    public int totalThrows;
    public float throwCoolDown;
    public Vector3 EndHitPoint;
    public string nameMap;
    private Scene actualScene;

    void Start()
    {
        actualScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        
    }


    public void Restart()
    {     
        nameMap = actualScene.name;
        SceneManager.LoadScene(nameMap);
        actualScene = SceneManager.GetActiveScene();    
    }

    public void NextMap()
    {
        int nMap= actualScene.buildIndex+1;
        SceneManager.LoadScene(nMap);
        actualScene = SceneManager.GetActiveScene();
    }

    public void PreviuosMap()
    {
        int nMap = actualScene.buildIndex - 1;
        SceneManager.LoadScene(nMap);
        actualScene = SceneManager.GetActiveScene();
    }

}
