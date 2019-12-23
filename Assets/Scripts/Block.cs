using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Config Params
    [SerializeField] AudioClip destroySound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    //Cached References
    Level level;

    // State Variables
    [SerializeField] int timesBlockHit;  // TODO only serialized to be able to debug in editor

    private void Start()
    {
        level = FindObjectOfType<Level>();
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tag == "Breakable")
        {
            HandleHit();

        }
        else if(tag == "Unbreakable")
        {
            return;
        }
        
    }

    private void HandleHit()
    {
        int maxBlockHits = hitSprites.Length + 1;
        timesBlockHit++;
        if (timesBlockHit >= maxBlockHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void DestroyBlock()
    {
        FindObjectOfType<GameSession>().AddToScore();
        PlayBlockDestroySFX();
        Destroy(gameObject, 0.1f);
        level.BlockDestroyed();
        TriggerSparklesVFX();
    }

    private void PlayBlockDestroySFX()
    {   
        AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesBlockHit - 1;
        if (hitSprites[spriteIndex] != null)
        {

            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.Log("Block sprite is missing from array (" + gameObject.name + ")");
        }
        
    }
}
