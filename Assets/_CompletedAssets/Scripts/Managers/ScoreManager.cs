using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CompleteProject
{
    public class ScoreManager : MonoBehaviour
    {
        public static int score;        // The player's score.
		public int WinScore = 500;
		//public int Objectives = 0;
		Animator anim;                          // Reference to the animator component.
		private bool WonGame = false;
		private bool triggered = false;
		static int nextScene = 1;
		Text WinScoreText;


        Text text;                      // Reference to the Text component.


        void Awake ()
        {
            // Set up the reference.

            text = GetComponent <Text> ();
			anim = GetComponent <Animator> ();
			WinScoreText = GameObject.Find ("WinScoreText").GetComponent<Text> ();

            // Reset the score.
            score = 0;
			WinScoreText.text = "Clear Level " + nextScene.ToString() +  " >> " + WinScore.ToString ();
        }


        void Update ()
        {
            // Set the displayed text to be the word "Score" followed by the score value.
			if(!WonGame && !triggered)text.text = "Score: " + score;
			if (score >= WinScore && !triggered) {
				triggered = true;
				text.text = "You Won!";
				PlayerHealth ph = GameObject.FindWithTag ("Player").GetComponent<PlayerHealth> ();
				ph.isDead = true;
				StartCoroutine (waitsec());
			}

        }
		IEnumerator waitsec() {

			yield return new WaitForSeconds(2);
			nextScene++;
			SceneManager.LoadScene(nextScene);
		}
    }


}
