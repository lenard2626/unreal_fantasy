using UnityEngine;
using System.Collections;

public class Skill{
	private string skillName;
	private float damageModifier = 0;
	private string  animParamName;
	private float  coolDownModifier;

	public Skill(string sn,float dm,string an){
		skillName = sn;
		damageModifier = dm;
		animParamName = an;
		coolDownModifier = 1;
	}
	public Skill(string sn,float dm,string an,float cdm){
		skillName = sn;
		damageModifier = dm;
		animParamName = an;
		coolDownModifier = cdm;
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
	public float CoolDownModifier {
		get {
			return this.coolDownModifier;
		}
	}

}
