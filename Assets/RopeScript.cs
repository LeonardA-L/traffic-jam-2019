using UnityEngine;
using System.Collections;
 
public class RopeScript : MonoBehaviour {
 
	public Transform target;
	private LineRenderer line;							//  DONT MESS!	 The line renderer variable is set up when its assigned as a new component
	
    void Start() {
        line = GetComponent<LineRenderer>();
    }

	void Update()
	{
        line.SetPosition(0,transform.position);
        line.SetPosition(1,target.transform.position);
	}
}