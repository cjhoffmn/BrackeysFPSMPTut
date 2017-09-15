using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovementSSBF : MonoBehaviour
{

    public float trgtmovetime, trgtmovedist, zmoveadj, ymoveadj;
    public float MaxX, MaxY, MaxZ, NewX, NewY, NewZ, ymoved, zmoved;
    float randomizer;
    public Vector3 CurrentPosition, StartingPosition, NewPosition;
    public bool CurXDir, curYDir, CurZDir;
        
    // Use this for initialization
	void Start ()
    {
        trgtmovetime = .5f;
        trgtmovedist = 1.25f;
        zmoveadj = 1.25f;
        ymoveadj = .50f;
        MaxY = 1.25f;
        MaxX = 15f;
        MaxY = .45f;
        MaxZ = 1.25f;
        StartingPosition = transform.localPosition;
        MaxY = StartingPosition.y + MaxY;
        MaxX = StartingPosition.x + MaxX;
        MaxZ = StartingPosition.z + MaxZ;
        CurXDir = true;
        curYDir = true;
        CurZDir = true;
        randomizer = Random.Range(.5f, 1.25f);
        ymoved = trgtmovedist * ymoveadj * randomizer;
        zmoved = trgtmovedist * zmoveadj * randomizer;
        
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        getcolorchange();
    }*/

    // Update is called once per frame
    void Update()
    {
        CurrentPosition = transform.localPosition;
        //GetNewX
        getnewx();
        randomizer = Random.Range(.5f, 2.25f);
        getnewy();
        getnewz();
        
        //NewY = CurrentPosition.y; //+ trgtmovedist;
        NewPosition = new Vector3(NewX, NewY, NewZ);
        transform.localPosition = Vector3.Slerp(CurrentPosition, NewPosition, trgtmovetime * Time.deltaTime);
        
    }
    //Changes Target Color
    public void getcolorchange()
    {
        StopCoroutine("showHitColor");
        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        StartCoroutine("showHitColor");
    }

    public IEnumerator showHitColor()
    {
        yield return new WaitForSeconds(.45f);
        gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
    }
    

    // Gets new Y Coordinate
    void getnewy()
    {
        ymoved = trgtmovedist * ymoveadj * randomizer;

        if (CurrentPosition.y < MaxY)
        {
            if (curYDir == true)
            {
                NewY = CurrentPosition.y + ymoved;
            }
            else
            {
                NewY = CurrentPosition.y - ymoved;
            }
        }
        else
        {
            NewY = CurrentPosition.y - ymoved;
            curYDir = false;
        }
        if (CurrentPosition.y > StartingPosition.y)
        {
            if (curYDir == true)
            {
                NewY = CurrentPosition.y + ymoved;
            }
            else
            {
                NewY = CurrentPosition.y - ymoved;
            }
        }
        else
        {
            NewY = CurrentPosition.y + ymoved;    
            curYDir = true;
        }
    }

    //Get New X Coordinate
    void getnewx()
    {
        if (CurrentPosition.x < MaxX)
        {
            if (CurXDir == true)
            {
                NewX = CurrentPosition.x + trgtmovedist;
            }
            else
            {
                NewX = CurrentPosition.x - trgtmovedist;
            }
        }
        else
        {
            NewX = CurrentPosition.x - trgtmovedist;
            CurXDir = false;
        }
        if (CurrentPosition.x > StartingPosition.x)
        {
            if (CurXDir == true)
            {
                NewX = CurrentPosition.x + trgtmovedist;
            }
            else
            {
                NewX = CurrentPosition.x - trgtmovedist;
            }
        }
        else
        {
            NewX = CurrentPosition.x + trgtmovedist;
            CurXDir = true;
        }
    }
    // Gets new Y Coordinate
    void getnewz()
    {
        zmoved = trgtmovedist * zmoveadj * randomizer;

        if (CurrentPosition.z < MaxZ)
        {
            if (CurZDir == true)
            {
                NewZ = CurrentPosition.z + zmoved;
            }
            else
            {
                NewZ = CurrentPosition.z - zmoved;
            }
        }
        else
        {
            NewZ = CurrentPosition.z - zmoved;
            CurZDir = false;
        }
        if (CurrentPosition.z > StartingPosition.z)
        {
            if (CurZDir == true)
            {
                NewZ = CurrentPosition.z + zmoved;
            }
            else
            {
                NewZ = CurrentPosition.z - zmoved;
            }
        }
        else
        {
            NewZ = CurrentPosition.z + zmoved;
            CurZDir = true;
        }
    }
}
