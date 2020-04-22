using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
   private bool _dirRight = true;
    bool canMove;
    public int MovementRate;
    public float MovementSpeed=.02f;
    Coroutine myMoveCoroutine;

   // public Vector2 MovePoints;
    ///  MovePoint.x -> Potrolling start point on X Axis
    ///  MovePoint.y -> Potrolling end point on X Axis

    public void StartMovement(Vector2 movePoint)
    {
        canMove = true;
        if (myMoveCoroutine==null)
        {
            myMoveCoroutine = StartCoroutine(MovePlatform(movePoint));

        }
        else
        {
            StopCoroutine(myMoveCoroutine);
            myMoveCoroutine = StartCoroutine(MovePlatform(movePoint));
          
        }

    }

    public void StopMovement()
    {
        canMove = false;
        if (myMoveCoroutine != null)
        {

            StopCoroutine(myMoveCoroutine);
        }
     
    }

    IEnumerator MovePlatform(Vector2 movePoints)
    {
        Vector3 firstPoint = new Vector3(movePoints.x,transform.localPosition.y,transform.localPosition.z);
        Vector3 lastPoint = new Vector3(movePoints.y,transform.localPosition.y,transform.localPosition.z);
        while (canMove&&transform.gameObject.activeSelf)
        {
          
              if (_dirRight)
              {
                  if (Vector3.Distance(transform.localPosition, lastPoint) < 0.1f)
                  {
                      _dirRight = false;
                  }
                  transform.localPosition = Vector3.Lerp(transform.localPosition,lastPoint,MovementSpeed*Time.deltaTime);
                  yield return null;
              }
              else
              {
                if (Vector3.Distance(transform.localPosition, firstPoint) < 0.1f)
                {
                    _dirRight = true;
                }
                transform.localPosition = Vector3.Lerp(transform.localPosition, firstPoint, MovementSpeed*Time.deltaTime);

                  yield return null;
              }
        }
    }
}
