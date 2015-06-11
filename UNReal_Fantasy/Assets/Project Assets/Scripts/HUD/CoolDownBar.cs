using UnityEngine;
using System.Collections;

public class CoolDownBar : MonoBehaviour {
	public GUIStyle barraProgresoStyle;
	public Texture2D barraFondo;
	public Texture2D barraFrente;

	private float time=0.01f;
	private static float LIMIT= 1.0f;
	private float progreso= 0f;
	private float velocidadProgreso=0f;
	
	private AsyncOperation async;

	private int barX = Screen.width*2/5;
	public int barY = Screen.height / 3;
	private int barWidth = Screen.width / 6;
	private int barHeigth = Screen.height/12;

	void Start(){
		int barX = Screen.width*2/5;
		int barY = Screen.height / 3;
		int barWidth = Screen.width / 6;
		int barHeigth = Screen.height/12;

	}
	
	void Update(){
		if(LIMIT>=progreso)
			progreso += Time.deltaTime*velocidadProgreso;
	}
	
	public void start(int t){
		this.time = t;
		progreso = 0;
		velocidadProgreso = LIMIT / time;
	}
	
	public void ActivateScene() {
		async.allowSceneActivation = true;
	}
	
	void OnGUI(){
		if (LIMIT >= progreso) {
			GUI.BeginGroup (new Rect (barX,barY, barWidth, barHeigth));
			//GUI.Box (new Rect (0, 0, barWidth,barHeigth), barraFondo, barraProgresoStyle);
			GUI.EndGroup ();
			GUI.BeginGroup (new Rect (barX,barY, (progreso/LIMIT)*barWidth,barHeigth));
			GUI.Box (new Rect (2, 2, barWidth, barHeigth), barraFrente, barraProgresoStyle);
			GUI.EndGroup ();
		}
	}
}