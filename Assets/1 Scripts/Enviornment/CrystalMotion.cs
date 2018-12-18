using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CrystalMotion : MonoBehaviour
{

    void Start()
    {
         Up();
    }

    void Update()
    {
        transform.Rotate(Vector3.up, 80 * Time.deltaTime);
    }

    void Up()
    {
        transform.transform.DOMoveY(10, 2).SetEase(Ease.InOutQuad).OnComplete(Down);
    }

    void Down()
    {
        transform.transform.DOMoveY(9, 2).SetEase(Ease.InOutQuad).OnComplete(Up);
    }
}
