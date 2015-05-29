using UnityEngine;
using System.Collections;

public class Mision : MonoBehaviour {
	public int idMision;
	public string nombre;
	public string detalles;
	public int estado;
	public string dueño;
	public string finaliza;
	public string escena=null;
	public static readonly int SININICIAR=0;
	public static readonly int ENPROGRESO=1;
	public static readonly int FINALIZADA=2;
	public static readonly int ENTREGADA=3;

	public Mision(int id, string nombre, string detalles, string dueño, string finaliza){
		this.idMision = id;
		this.nombre = nombre;
		this.detalles = detalles;
		this.dueño = dueño;
		this.finaliza = finaliza;
		estado = SININICIAR;
	}
	public Mision(int id, string nombre, string detalles, string dueño, string finaliza, string escena){
		this.idMision = id;
		this.nombre = nombre;
		this.detalles = detalles;
		this.dueño = dueño;
		this.finaliza = finaliza;
		this.escena = escena;
		estado = SININICIAR;
	}
	public Mision(){}
}
