using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
  
public class Manager : MonoBehaviour
{
    public Text textUI;

    [SerializeField] public int MaxEnemy = 2;

    public int RemainEnemies = 2;
 
    // Start is called before the first frame update
    void Start()
    {
        textUI.text = "2/2 Enemy";
    }
  
    // Update is called once per frame
    void Update()
    {
        string Max = MaxEnemy.ToString();
        string Remain = RemainEnemies.ToString();
        textUI.text = Remain + "/" + Max + " Enemy";

        if(RemainEnemies <= 0)
        {
            StartCoroutine( Wait() );
        }
    }

    /*public void ChangeText(){

    }*/
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("EndPage");
    }
}
