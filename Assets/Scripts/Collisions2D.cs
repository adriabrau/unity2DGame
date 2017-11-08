using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions2D : MonoBehaviour {
    [Header("State")]
    public bool isGrounded;
    public bool wasGroundedLastFrame;
    public bool justGotGrounded;
    public bool justNOTGrounded;
    public bool isFalling;

    public bool isWalled;
    public bool wasWalledLastFrame;
    public bool justGotWalled;
    public bool justNOTWalled;


    public bool isCelled;
    public bool wasCelledLastFrame;
    public bool justGotCelled;
    public bool justNOTCelled;


    [Header("FilterPropierties")]
    public ContactFilter2D groundFilter;
    public int maxHits;
    [Header("BottomBox")]
    public Vector2 groundBoxPos;
    public Vector2 groundBoxSize;

    [Header("WallBox")]
    public Vector2 wallBoxPos;
    public Vector2 wallBoxSize;

    [Header("CellBox")]
    public Vector2 cellBoxPos;
    public Vector2 cellBoxSize;

    public void Start()
    {
        ResetState();
    }


    void ResetState()
    {
        wasGroundedLastFrame = isGrounded;

        isGrounded = false;
        justGotGrounded = false;
        justNOTGrounded = false;
        isFalling = true;

        isWalled = false;
        wasWalledLastFrame = isWalled;
        justGotWalled = false;
        justNOTWalled = false;

        isCelled = false;
        wasCelledLastFrame = isCelled;
        justGotCelled = false;
        justNOTCelled = false;
}


	public void FixedUpdate ()
    {
        ResetState();
        GroundDetection();
        WallDetection();
        CellDetection();

    }
	
    void GroundDetection()
    {
        
        Collider2D[] results = new Collider2D[maxHits];
        Vector2 pos = this.transform.position;
        int numHits = Physics2D.OverlapBox(pos + groundBoxPos, groundBoxSize, 0, groundFilter, results);

        if(numHits > 0)
        {
            isGrounded = true;
        }

        if(isGrounded) isFalling = false;
        if(isGrounded && !wasGroundedLastFrame) justGotGrounded = true;
        if(!isGrounded && wasGroundedLastFrame) justNOTGrounded = true;

    }
    void WallDetection()
    {
        

        Collider2D[] results = new Collider2D[maxHits];
        Vector2 pos = this.transform.position;
        int numHits = Physics2D.OverlapBox(pos + wallBoxPos, wallBoxSize, 0, groundFilter, results);

        if(numHits > 0)
        {
            isWalled = true;
        }

        if(isWalled) isFalling = false;
        if(isWalled && !wasWalledLastFrame) justGotWalled = true;
        if(!isWalled && wasWalledLastFrame) justNOTWalled = true;

    }

    void CellDetection()
    {

        Collider2D[] results = new Collider2D[maxHits];
        Vector2 pos = this.transform.position;
        int numHits = Physics2D.OverlapBox(pos + cellBoxPos, cellBoxSize, 0, groundFilter, results);

        if(numHits > 0)
        {
            isCelled = true;
        }

        if(isCelled) isFalling = false;
        if(isCelled && !wasCelledLastFrame) justGotCelled = true;
        if(!isCelled && wasCelledLastFrame) justNOTCelled = true;

    }
    public void Flip(bool face)
    {
        if(face) wallBoxPos.x = Mathf.Abs(wallBoxPos.x);
        else wallBoxPos.x = -Mathf.Abs(wallBoxPos.x);
    }
    // Update is called once per frame
    private void OnDrawGizmosSelected ()
    {
        Vector2 pos = this.transform.position;
        Gizmos.DrawWireCube(pos + groundBoxPos, groundBoxSize);
        Gizmos.DrawWireCube(pos + wallBoxPos, wallBoxSize);
        Gizmos.DrawWireCube(pos + cellBoxPos, cellBoxSize);
    }
}
