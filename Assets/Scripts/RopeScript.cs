using UnityEngine;
using System.Collections;
 
public class RopeScript : MonoBehaviour {
 
	public Transform target;
	public float resolution = 10.0f;            //number of segments per unit
    public float weight = 1.0f;
    private float currentY;
    private float currentZ;
    private float currentX;
    private float countSegments;          //number of segments for the rope
    private LineRenderer line;
    
    void Start() {
        line = GetComponent<LineRenderer>();
    }

	void Update()
	{           
        countSegments = (Vector3.Distance(transform.position,target.transform.position)/100)*resolution;
        line.positionCount = (int)countSegments+1;
        line.SetPosition(0,transform.position);
        for(int i=1;i<line.positionCount;++i) {
            currentX = transform.position.x+((target.transform.position.x-transform.position.x)/countSegments)*i;
            currentZ = transform.position.z+((target.transform.position.z-transform.position.z)/countSegments)*i;
            currentY = transform.position.y+((target.transform.position.y-transform.position.y)/countSegments)*i
                        *Mathf.Pow(i/countSegments,weight);
            line.SetPosition(i, new Vector3(currentX, currentY, currentZ));
        }
        line.SetPosition(line.positionCount-1,target.transform.position);
    }
}