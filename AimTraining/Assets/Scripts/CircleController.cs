using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour
{
    float speed;
    bool isInc;
    float maxSize;
    Vector3 newScale = new Vector3();
    Color startColor, midColor, endColor;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(GameController.Instance.circleSpeed * (1 - GameController.Instance.circleSpeedVariation), 
            GameController.Instance.circleSpeed * (1 + GameController.Instance.circleSpeedVariation));
        maxSize = Random.Range(GameController.Instance.circleSize * (1 - GameController.Instance.circleSizeVariation),
            GameController.Instance.circleSize * (1 + GameController.Instance.circleSizeVariation));
        isInc = true;

        spriteRenderer = GetComponent<SpriteRenderer>();

        startColor = GameController.Instance.startColor;
        midColor = GameController.Instance.midColor;
        endColor = GameController.Instance.endColor;

        spriteRenderer.color = startColor;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        newScale.x = transform.localScale.x + speed;
        newScale.y = transform.localScale.y + speed;
        newScale.z = transform.localScale.z;

        if (isInc)
        {
            if (newScale.x >= maxSize)
            {
                isInc = false;
                speed = -speed;
            } else
            {
                spriteRenderer.color = Color.Lerp(startColor, midColor, newScale.x / maxSize);
            }
        } else
        {
            if (newScale.x <= 0)
            {
                GameController.Instance.DecreaseLives();
                Destroy(this.gameObject);
            } else
            {
                spriteRenderer.color = Color.Lerp(midColor, endColor, 1 - newScale.x / maxSize);
            }
        }

        transform.localScale = newScale;
    }
}
