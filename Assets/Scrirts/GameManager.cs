using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ボタン・・テキスト等、UI系のものを扱うときに必要！
using UnityEngine.SceneManagement;

//シーン遷移扱う為！

public class GameManager : MonoBehaviour
{
	int count = 0;
	public Text countText;
	//Text型の変数を定義　&& Unityからアタッチするからpublic！！
	public Text timerText;
	//Text型の変数を定義　&& Unityからアタッチするからpublic！！
	float gameTimer = 5f;
	public Text buttonText;
	// Use this for initialization
	void Start ()
	{
		buttonText.text = "押せ！";
        countText.text = "0";
	}
	
	// Update is called once per frame
	void Update ()
	{
		gameTimer -= Time.deltaTime;//実質的なタイマー応用！
		timerText.text = gameTimer.ToString ("f2");//引数で小数大何位まで表示するか指定！

		if(gameTimer < 0){
			PlayerPrefs.SetInt ("score", count);//変数countの値を"score"の名前でPlayerPrefsに保存
			SceneManager.LoadScene("ScoreScene");//シーン遷移
		}
	}

    //ボタン押した時に呼ばれる関数
	public void CountUp (){	
		count += 1;
		countText.text = count.ToString ();//Textクラスのメソッド && .ToStringでstring型に変換！
		print (count);//countの値をコンソールに表示
	}
}
