using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    [Header("Box")]
    public Vector2 boxPos;
    public Vector2 boxSize;
    public LayerMask mask;
    [Header("Collider")]
    public Collider2D col;
    public ContactFilter2D filter;
    


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void FixedUpdate()
    {
        Collider2D[] results = new Collider2D[5];
        //results = new Collider2D[1];
        int numHits = Physics2D.OverlapBox(this.transform.position, boxSize, 0, filter, results);
        if(numHits > 0) Debug.Log("Se ha detectado un collider");

        //Physics2D.OverlapCollider(col, filter, results);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(this.transform.position, boxSize);
    }
    

}
