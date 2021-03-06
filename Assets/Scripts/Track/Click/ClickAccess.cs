using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAccess : MonoBehaviour
{
    public bool hold;
    public bool status;
    private bool registed;
    public GameObject currentBase;//底座
    public SpriteRenderer topCoverRenderer;

    public SpriteRenderer currentBaseRenderer;//底座渲染器
    private Sprite[] sprites;//1是off,2是on
    public Sprite[] spritesOfClick;
    public Sprite[] spritesOfHold;
    public Sprite[] spritesOfTopCover;
    // Start is called before the first frame update
    void Start()
    {
        
        registed = false;
        if (!hold)
        {
            InputHandler.Instance.StartListener(this.gameObject, Click);
            sprites = spritesOfClick;
            topCoverRenderer.sprite = spritesOfTopCover[0];
            registed = true;
        }
        else
        {
            sprites = spritesOfHold;
            topCoverRenderer.sprite = spritesOfTopCover[1];
        }
        if (!status)
        {
            currentBaseRenderer.sprite = sprites[1];
            this.gameObject.GetComponent<Collider2D>().enabled = true;
            this.gameObject.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            currentBaseRenderer.sprite = sprites[0];
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            this.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
    private void OnDisable()
    {
        if (InputHandler.IsInitialized && registed)
            InputHandler.Instance.StopListener(this.gameObject, Click);
    }
    void Click()
    {
        if (this.gameObject.GetComponent<Renderer>().enabled == true)
        {
            currentBaseRenderer.sprite = sprites[0];
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            this.gameObject.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            currentBaseRenderer.sprite = sprites[1];
            this.gameObject.GetComponent<Collider2D>().enabled = true;
            this.gameObject.GetComponent<Renderer>().enabled = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (hold)
        {
            if (Input.GetMouseButton(0))
            {
                if (this.gameObject.GetComponent<Renderer>().enabled == true)
                {
                    currentBaseRenderer.sprite = sprites[0];
                    this.gameObject.GetComponent<Collider2D>().enabled = false;
                    this.gameObject.GetComponent<Renderer>().enabled = false;
                }
            }
            else
            {
                currentBaseRenderer.sprite = sprites[1];
                this.gameObject.GetComponent<Collider2D>().enabled = true;
                this.gameObject.GetComponent<Renderer>().enabled = true;
            }
        }
    }
}
