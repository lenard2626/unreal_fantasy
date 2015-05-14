using UnityEngine;
using System.Collections;

/// <summary>
/// Creates wandering behaviour for a CharacterController.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class Wander : MonoBehaviour
{
	public float speed = 5;
	public float directionChangeInterval = 1;
	public float maxHeadingChange = 30;
	public float rangeBeforeStop = 5;

	CharacterController controller;
	Animation wandering_cube_anim;
	float heading;
	bool isWalking =true;
	Vector3 wayPoint;
	Vector3 targetRotation;
	Vector3 forward;
	float rotationCounter;

	void Awake ()
	{
		controller = GetComponent<CharacterController>();
		wandering_cube_anim = GetComponentInChildren<Animation>();

		wayPoint = Random.insideUnitSphere*rangeBeforeStop;
		forward = transform.TransformDirection(Vector3.forward);

		// Set random initial rotation
		heading = Random.Range(0, 360);
		transform.eulerAngles = new Vector3(0, heading, 0);
		//StartCoroutine(NewHeading());

	}
	
	void Update ()
	{

		if((Vector3.Distance(transform.position, wayPoint) < rangeBeforeStop)&&isWalking){

			var forward = transform.TransformDirection(Vector3.forward);
			controller.SimpleMove(forward * speed);
			if(!wandering_cube_anim.IsPlaying("Cube|walk")) 
				wandering_cube_anim.Play ("Cube|walk");
			//Debug.Log("camina");

		}else{
			targetRotation = new Vector3(0,Random.Range(0,30),0);
			if(!wandering_cube_anim.IsPlaying("Cube|attack")) 
				wandering_cube_anim.Play ("Cube|attack");

			//Debug.Log("angulo "+Vector3.Angle(wayPoint,transform.localPosition));

			if(Vector3.Angle(forward,transform.position)<maxHeadingChange){
				transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
				isWalking = false;
				//Debug.Log("rota");
			}else{
				rotationCounter=0f;
				wayPoint=transform.position;
				isWalking = true;
				//Debug.Log("continua");
			}
		}
	}
	
	/// <summary>
	/// Repeatedly calculates a new direction to move towards.
	/// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
	/// </summary>
	IEnumerator NewHeading ()
	{
		while (true) {
			NewHeadingRoutine();
			yield return new WaitForSeconds(directionChangeInterval);
		}
	}
	
	/// <summary>
	/// Calculates a new direction to move towards.
	/// </summary>
	void NewHeadingRoutine ()
	{
		var floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
		var ceil  = Mathf.Clamp(heading + maxHeadingChange, 0, 360);
		heading = Random.Range(floor, ceil);
		targetRotation = new Vector3(0, heading, 0);
	}
}