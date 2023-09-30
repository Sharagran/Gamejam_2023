using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;

public class ChangeSceneOnCollect : MonoBehaviour
{
    public SceneAsset sceneToLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.Play("win");
            // make collectible invisible
            GetComponent<SpriteRenderer>().enabled = false;
            // load scene
            Invoke("loadScene", 1.5f);
        }
    }

    private void loadScene()
    {
        UnityEditor.SceneManagement.EditorSceneManager.LoadScene(sceneToLoad.name);
    }
}
