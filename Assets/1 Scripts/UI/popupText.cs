using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class popupText : MonoBehaviour
{
    GameObject p;
    Animator animator;
    TextMeshProUGUI txt;
    GameObject canvas;
    GameObject anchor;
    Vector3 removed;

    void Awake()
    {
        p = gameObject.transform.parent.gameObject;
        animator = GetComponent<Animator>();
        txt = GetComponent<TextMeshProUGUI>();
        canvas = GameObject.Find("Canvas");
        p.transform.SetParent(canvas.transform, false);
    }

    void Update()
    {
        if (anchor)
        {
            Vector2 screenposition = Camera.main.WorldToScreenPoint(anchor.transform.position);
            p.transform.position = screenposition;
            removed = anchor.transform.position;
        } else
        {
            Vector2 screenposition = Camera.main.WorldToScreenPoint(removed);
            p.transform.position = screenposition;
        }
    }



    public void SetText(bool type, int damage, GameObject loc)
    {
        anchor = loc;
        Vector2 screenposition = Camera.main.WorldToScreenPoint(loc.transform.position);
        p.transform.position = screenposition;
        txt.text = damage.ToString();
        if (type){
            txt.faceColor = Color.red;
        } else if (!type) {
            txt.faceColor = Color.green;
        }
        removed = loc.transform.position;

    }

    public void EndOfAnim()
    {
        Destroy(p);
    }
}
