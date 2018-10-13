using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.SceneManagement; 

public class LoadSceneOnClick:MonoBehaviour {

  public void LoadByIndex(int sceneIndex ) {
    //Build Settings'e scene'i ekle
    SceneManager.LoadScene(sceneIndex ); 
  }
  
}
