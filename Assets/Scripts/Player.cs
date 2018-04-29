using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

	private int level;
	private float curLevelExperience;
	private float experienceToLevel;

	void Start() {
		level = 1;
		experienceToLevel = 100;
	}

	public void AddExperience(float exp) {
		curLevelExperience += exp;
		if (curLevelExperience >= experienceToLevel) {
			level++;
			curLevelExperience -= experienceToLevel; // Apply leftover experience to the next level
			AddExperience(0); // In case we have even more experience to level up another level. [This code is trying to be clever, so be careful.]
		}
		Debug.Log("LEVEL: " + level + ". EXP: " + curLevelExperience);
	}

}