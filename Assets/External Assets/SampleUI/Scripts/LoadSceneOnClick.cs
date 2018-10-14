using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class LoadSceneOnClickNew:MonoBehaviour {

    public void LoadByIndex(int sceneIndex) {
    //Build Settings'e scene'i ekle
       
    SceneManager.LoadScene(sceneIndex ); 
  }
  
}
