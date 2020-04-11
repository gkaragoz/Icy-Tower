using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _target = null;
    [SerializeField]
    private Transform _followers = null;
    [SerializeField]
    private float _deadZoneOffset = 0f;

    [SerializeField]
    private float _jumpOffset = 0f;

    public float speed = 2.0f;

    bool canMove = false;
    bool flyingUP = false;
    public bool startMatch = false;
    public bool iCanRotate = false;
    private Coroutine mycor;
    public int value;

    private void Update()
    {
        _followers.transform.position = new Vector3(_followers.transform.position.x,transform.position.y-11,_followers.transform.position.z);

        //Check if i died
        if (_target.position.y < transform.position.y - _deadZoneOffset)
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(60, 0, 0), speed * Time.deltaTime);
            return;
        }

        if (startMatch)
        {

            Switcher();
        }



        if (canMove)
        {
            NormalMovement();
        }

        if (flyingUP)
        {
            FlyingUp();
        }
    }



    void NormalMovement()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);

    }

    void FlyingUp()
    {
        float interpolation = speed * Time.deltaTime;
        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, _target.transform.position.y, interpolation*2);
        this.transform.position = position;

    }


    void Switcher()
    {
        if (_target.position.y > transform.position.y + _jumpOffset)
        {
            flyingUP = true;
            canMove = false;
        }

        if (_target.position.y < transform.position.y)
        {
            flyingUP = false;
            canMove = true;
        }
    }

  
    
    public void WallWalk(int value)
    {
        if (mycor!=null)
        {
        iCanRotate = false;
        StopCoroutine(mycor);

        }
        iCanRotate = true;
        mycor =StartCoroutine(GetPump(value));
    }

    IEnumerator GetPump(int value)
    {
             
        while (iCanRotate)
        {

            float interpolation = speed * Time.deltaTime;
            Vector3 rotation = this.transform.eulerAngles;
            rotation.y = 10 * value;

            if (value==0)
            {
                rotation.x = 0;
                rotation.y = 0;
                rotation.z = 0;
            }
            else
            {
                rotation.x = -25;
            }

            
            this.transform.rotation =Quaternion.Lerp(transform.rotation,Quaternion.Euler(rotation),interpolation*2);

            yield return null;
        }
    }

}


//T=x-2 R=y10 x-25