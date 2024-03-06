using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFlash : MonoBehaviour
{
    public float lifeTime = 0.5f;
    public Sprite[] possibleFlashes;

    private SpriteRenderer sprRen;

    // Start is called before the first frame update
    void Start()
    {
        sprRen = this.GetComponent<SpriteRenderer>();
        int posFlashLength = possibleFlashes.Length;

        sprRen.sprite = possibleFlashes[Random.Range(0, posFlashLength)];
        if (Random.Range(0f, 1f) > 0.5f)
        {
            sprRen.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifeTime);
    }
}
