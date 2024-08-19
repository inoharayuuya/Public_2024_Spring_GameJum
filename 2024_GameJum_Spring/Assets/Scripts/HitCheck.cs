using Const;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : MonoBehaviour
{
    [SerializeField]
    private bool isLocked;
    private GameObject gameManagerObj;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        isLocked = false;
        gameManagerObj = GameObject.Find(Common.DRAG_OBJ_NAME);
        gameManager = gameManagerObj.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.tag == "Pin") //Pinタグのカウント
        {
            count++;
        }*/
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HitCheck" && !isLocked)
        {
            gameManager.Count();
            isLocked = true;
            Destroy(gameObject, 2.0f);
            //StartCoroutine(SetActive(other));
        }
    }

    private IEnumerator SetActive(Collider collider)
    {
        yield return new WaitForSeconds(1.0f);
        //isLocked = false;
        //collider.gameObject.SetActive(false);
    }
}