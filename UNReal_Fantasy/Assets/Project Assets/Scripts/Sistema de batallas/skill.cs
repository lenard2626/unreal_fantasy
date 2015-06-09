using UnityEngine;
using System.Collections;

public class Skill{
	private string skillName;
	private float damageModifier = 0;
	private string  animParamName;

	public Skill(string sn,float dm,string an){
		skillName = sn;
		damageModifier = dm;
		animParamName = an;
	}

	public string SkillName {
		get {
			return this.skillName;
		}
	}

	public float DamageModifier {
		get {
			return this.damageModifier;
		}
	}

	public string AnimParamName {
		get {
			return this.animParamName;
		}
	}
}
