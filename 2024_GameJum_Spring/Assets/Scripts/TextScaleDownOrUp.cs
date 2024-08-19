using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScaleDownOrUp : MonoBehaviour
{
    public float time, changeSpeed;
    public bool enlarge;

    void Start()
    {
        enlarge = true;
    }

    void Update()
    {
        // テキストを大きくしたり小さくしたり //
        changeSpeed = Time.deltaTime * 0.1f;

        if (time < 0)
        {
            enlarge = true;
        }
        if (time > 1.5f)
        {
            enlarge = false;
        }

        if (enlarge == true)
        {
            time += Time.deltaTime;
            transform.localScale += new Vector3(changeSpeed, changeSpeed, changeSpeed);
        }
        else
        {
            time -= Time.deltaTime;
            transform.localScale -= new Vector3(changeSpeed, changeSpeed, changeSpeed);
        }
    }
}