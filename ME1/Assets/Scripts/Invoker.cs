using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoker : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;
    public Camera fpscam;

    [Header("Inputs")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public KeyCode directionKey = KeyCode.Mouse1;
    public KeyCode freezeKey = KeyCode.F;
    public KeyCode attractionKey = KeyCode.E;
    public KeyCode resetKey = KeyCode.R;

    [Header("Settings")]
    public float range;

    [Header("Throwing")]
    public float throwForce;
    public float throwUpwardForce;

    public Manager manager;


    public int freezeActive;
    public int attractionActive;
    [SerializeField] private float timers;
    

    private void Start()
    {
        
        manager.readyToThrow = true;
        
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(throwKey) && manager.readyToThrow && manager.totalThrows > 0)
        {
            Throw();
            
        }

        if (Input.GetKeyDown(freezeKey) && !manager.isAttracting)
        {
            manager.isFreezing = !manager.isFreezing;
        }

        if (Input.GetKeyDown(attractionKey)&& !manager.isFreezing)
        {
            manager.isAttracting = !manager.isAttracting;
        }
   
        if (Input.GetKeyDown(resetKey))
        {
            manager.Restart();
        }

        //if (Input.GetKeyDown(directionKey))
        //{
        //    RaycastHit hitOrb;
        //    if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hitOrb, range))
        //    {
        //        Debug.Log(hitOrb.transform.name);
        //        manager.EndHitPoint = hitOrb.point;
        //    }
        //    manager.isDirecting = !manager.isDirecting;
        //}

    }

    void FixedUpdate()
    {
        
    }

    private void GetPointHit()
    {
        
    }

    #region GrabSystem
   
    #endregion
    private void Throw()
    {
        manager.readyToThrow = false;

        GameObject ProjectileAttractor = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        Rigidbody projectileRB = ProjectileAttractor.GetComponent<Rigidbody>();

        Vector3 forceToAdd = cam.transform.forward * throwForce + transform.up * throwUpwardForce;

        projectileRB.AddForce(forceToAdd, ForceMode.Impulse);

        manager.totalThrows--;

        Invoke(nameof(ResetThrow), manager.throwCoolDown);
    }
 
    private void ResetThrow()
    {
        manager.readyToThrow = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Win"))
        {
            manager.NextMap();
        }
    }

    //private void delete()
    //{
    //    RaycastHit hit;
    //    timers = timers + Time.deltaTime;
    //    if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, rangeDelete))
    //    {
    //        if (Input.GetButton("Delete"))
    //        {
    //            if (hit.collider.CompareTag("Attractors"))
    //            {
    //                Debug.Log(hit.transform.name);

    //                totalThrows++;
    //            }
    //        }
    //    }
    //}
}
