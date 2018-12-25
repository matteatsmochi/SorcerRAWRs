using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class healthBar : MonoBehaviour
{
    
    public Image left, center, right;
    public Sprite r1, r2, r3, y1, y2, y3, g1, g2, g3, b1, b2, b3;
    public GameObject character;
    Vector3 removed;
    int type;
    dinoStats ds;
    enemyStats es;
    float currentStamnia;
    float fullStamnia;
    float previousStamina;

    void Awake()
    {
        GameObject p = gameObject.transform.parent.gameObject;
        character = p;
        GameObject canvas = GameObject.Find("Canvas");
        gameObject.transform.SetParent(canvas.transform, false);
    }
    
    void Start()
    {
        //assign components
        ds = character.GetComponent<dinoStats>();
        es = character.GetComponent<enemyStats>();

        if (ds){type = 1;}
        if (es){type = 2;}
    }

    void Update()
    {
        if (character)
        {
            Vector2 screenposition = Camera.main.WorldToScreenPoint(character.transform.position);
            transform.position = new Vector2(screenposition.x - 5, screenposition.y + 40);
            removed = character.transform.position;
        } else
        {
            Vector2 screenposition = Camera.main.WorldToScreenPoint(removed);
            transform.position = new Vector2(screenposition.x - 5, screenposition.y + 40);
        }
        
        switch (type)
        {
            case 1:
                currentStamnia = ds._currentStamnia;
                fullStamnia = ds._stamnia;
            break;
            case 2:
                currentStamnia = es._currentStamnia;
                fullStamnia = es._stamnia;
            break;
        }
        
        if (currentStamnia != previousStamina)
        {
            float prct = Mathf.Clamp(currentStamnia, 0, fullStamnia) /fullStamnia;

            if (prct == 1)
            {
                left.sprite = b1;
                center.sprite = b2;
                right.sprite = b3;
            } else if (prct > 0.5f)
            {
                left.sprite = g1;
                center.sprite = g2;
                right.sprite = g3;
            } else if (prct > 0.2f)
            {
                left.sprite = y1;
                center.sprite = y2;
                right.sprite = y3;
            } else
            {
                left.sprite = r1;
                center.sprite = r2;
                right.sprite = r3;
            }

            Sequence seq = DOTween.Sequence();
            seq.Append(center.rectTransform.DOSizeDelta(new Vector2(prct * 183.5f, center.rectTransform.sizeDelta.y), 1));
            seq.Join(right.rectTransform.DOAnchorPos(new Vector3((prct * 92) + 5.5f, 0, 0), 1));
            seq.SetEase(Ease.OutQuad);

            if (prct == 0){Destroy(gameObject, 1f);}
        }

        previousStamina = currentStamnia;
    }

}
