using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{

	 float G = 300f;

	Manager manager;

	public Rigidbody rb;



	public Invoker Player;

    private void Start()
    {
		Player = FindObjectOfType<Invoker>();
		manager = FindObjectOfType<Manager>();
    }

    void FixedUpdate()
	{
		foreach (Attractor attractor in Manager.Attractors)
		{
			if (attractor != this)
			{
				
				if (manager.isAttracting)
				{
					Debug.Log("ATTRACCION");
					rb.constraints = RigidbodyConstraints.None;
					Attract(attractor);
					
				}
                if (manager.isDirecting)
                {
					UpDown(attractor);

				}

				if (manager.isFreezing)
				{
					Debug.Log("Freezeado");
					manager.isFreezing = true;
					rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
					
				}

                if (!manager.isAttracting && !manager.isFreezing)
                {
					rb.constraints = RigidbodyConstraints.None;
				}

			}
			
		}
	}

	
	public void OnEnable()
	{
		if (Manager.Attractors == null)
			Manager.Attractors = new List<Attractor>();
		Manager.Attractors.Add(this);
	}

	public void OnDisable()
	{
		Manager.Attractors.Remove(this);	
	}


    void UpDown(Attractor objToAttract)
    {
        Rigidbody rbToAttract = objToAttract.rb;

        Vector3 direction = manager.EndHitPoint;
        float distance = direction.magnitude;

        if (distance < 0)
        {
            //G = 25f;
            return;
        }

        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(force);
    }

    void Attract(Attractor objToAttract)
	{

		Rigidbody rbToAttract = objToAttract.rb;

		Vector3 direction = rb.position - rbToAttract.position;
		float distance = direction.magnitude;

		if (distance < 0)
		{
			//G = 25f;
			return;
		}
			
		float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
		Vector3 force = direction.normalized * forceMagnitude;

		rbToAttract.AddForce(force);
	}



}