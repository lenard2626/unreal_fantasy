using UnityEngine;
using System.Collections;

/// <summary>
/// Creates wandering behaviour for a CharacterController.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class enemyWander : MonoBehaviour
{

	public int WaypointRadius = 20;			//Si no se especifican "waypoints", indica la distancia maxima entre waypoints aleatorios
	public int WayPointNumber = 4;			//Si no se especifica  "waypoints", indica el numero aleatorio de estos
	public Transform[] waypoints;
	public Transform origin;				//Punto de partida para ubicar los waypoints relativamente

	private NavMeshAgent nma;

	void Start(){
		//Inicializa las variables 

		//Inicializa los waypoints (de ser necesario)
		if (waypoints == null) {
			Vector3 lastWaypoint;
			waypoints = new Transform[WayPointNumber];
			lastWaypoint = origin.position+(new Vector3(Random.Range(0,WaypointRadius),Random.Range(0,WaypointRadius),origin.position.z));		//Configura el primer waypoint aleatorio (solo en x y y)

			for(int i=1;i<WayPointNumber;i++){
				waypoints[i].position = lastWaypoint;
				lastWaypoint = origin.position+(new Vector3(Random.Range(0,WaypointRadius),Random.Range(0,WaypointRadius),Random.Range(0,WaypointRadius)));
			}	
		}
	}

	void Update(){

	}
}