using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartScreen : MonoBehaviour {


	public void Quit()
	{
		#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;
		#else 
		Application.Quit();
		#endif
	}
	public void BeginGame(){
		SceneManager.LoadScene(1);
	}
}
