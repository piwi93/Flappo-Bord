using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Score : MonoBehaviour {

	public static Score myScore;
	private float points = 0;
	private float lastScore = 0;
	private float highScore = 0;
	private String path;


	//Use on object awake - when object is instantiated - before start
	void Awake(){

		//Set the same score object between scenes
		if(myScore == null){
			myScore = this;
			DontDestroyOnLoad(gameObject);
		}
		else if(myScore != this){
			Destroy(gameObject);
		}

		//Set path
		path = Application.persistentDataPath + "/data0.dat";
	}


	// Use this for initialization
	void Start(){

		//Load and set gamescore
		loadScore();
	}


	//add +1 to the actual gamescore
	public void addOne(){
		points++;
		this.GetComponent<TextMesh>().text = points.ToString();
	}


	//reset the actual gamescore
	public void resetScore(){
		saveScore();
		points = 0;
		this.GetComponent<TextMesh>().text = points.ToString();
	}


	//save last score and high score
	public void saveScore(){

		//data file definitions
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(path);
		scoreData data = new scoreData();

		//set new highscore
		if(points > highScore){
			highScore = points;
		}
		data.highScore = highScore;

		//set new lastscore
		lastScore = points;
		data.lastScore = lastScore;

		//save data and close file
		bf.Serialize(file, data);
		file.Close();
	}


	//load last and high score
	public void loadScore(){

		//set new score on screen
		this.GetComponent<TextMesh>().text = points.ToString();

		//if exists a save file
		if(File.Exists(path)){

			//data file definitions
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(path, FileMode.Open);
			scoreData data = (scoreData) bf.Deserialize(file);

			//set highScore from save file
			highScore = data.highScore;
			lastScore = data.lastScore;

			file.Close();
		}
	}
	
}


[Serializable]
class scoreData{
	public float highScore;
	public float lastScore;
}