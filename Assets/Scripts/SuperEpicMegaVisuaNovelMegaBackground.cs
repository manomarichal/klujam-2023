using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperEpicMegaVisuaNovelMegaBackground : MonoBehaviour
{
    public float tSpeed = 0.01f;
    
    public List<Material> Materials;

    private SpriteRenderer sr;

    private float count = 0;
    private int _index = 0;
    private bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.material = Materials[0];
    }

    private void Update()
    {
        if (!active)
        {
            return;
        }

        sr.material.SetFloat("_Transition", count);
        count += tSpeed;

        if (count >= 1)
        {
            _index++;
            active = false;
            count = 0;
            sr.material = Materials[_index];
        }
    }

    public void progressBackground()
    {
        active = true;
    }
}
