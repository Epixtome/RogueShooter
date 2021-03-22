using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortOrder : MonoBehaviour
{
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        sr.sortingOrder = Mathf.RoundToInt(transform.position.y * -10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
