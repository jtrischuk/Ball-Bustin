using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int breakableBlocks;  // serialized field just so we can see it in editor

    // Cached references
    SceneLoader sceneLoader;

    public void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBlocks()
    {
        breakableBlocks++;
    }

    public void BlockDestroyed()
    {
        breakableBlocks--;
        if(breakableBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }


}
