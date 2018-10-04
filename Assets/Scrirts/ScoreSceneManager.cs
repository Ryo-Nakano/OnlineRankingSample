using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Canvas使う
using UnityEngine.SceneManagement;//シーン遷移の際に必要！
using NCMB;//NCMB使う為の準備

public class ScoreSceneManager : MonoBehaviour {

	public Text lastScoreText;//inspectorビューからあとでアタッチ！
	public Text highScoreText;//inspectorビューからあとでアタッチ！
	int highScore;
	int lastScore;

	public void RestartButton(){//後でunityから適応するからpublic指定！
		SceneManager.LoadScene ("StartScene");
	}

	void Start(){
		Score();//Scoreの更新・表示を行う関数
	}

	//Scoreの更新・表示を行う関数
	void Score()
	{
		//lastScoreを保存、表示
        lastScore = PlayerPrefs.GetInt("score");//PlayerPrefsで"score"の名前で保存していた値を取得→変数lastScoreに格納
        lastScoreText.text = lastScore.ToString();//lastScoreの値を表示

		if (PlayerPrefs.HasKey("highScore") == true)//既にhighScoreの値が存在していた場合(2回目以降プレイ時)
        {
            highScore = PlayerPrefs.GetInt("highScore");//普通に既にあるhighScoreを取ってくる(比較の為！)

            //=====highScoreとlastScore比較・検討！=====
			if (highScore < lastScore)//highScoreよりlastScoreのがでかい時
            {
                highScore = lastScore;//highScoreにlastScoreの値をぶち込む
                PlayerPrefs.SetInt("highScore", highScore);//新しいhighScoreを"highScore"の名前でPlayerPrefsに保存

				SaveHighScoreToNCMB();//highScoreをNCMBに保存
            }
        }
		else//highScoreが保存されていない場合(初回プレイ時)
        {
            highScore = lastScore;//シンプルに直近のスコアがhighScoreに
            PlayerPrefs.SetInt("highScore", highScore);//変数highScoreの値を"highScore"の名前でPlayerPrefsに保存

			SaveHighScoreToNCMB();//highScoreをNCMBに保存
        }
        highScoreText.text = highScore.ToString();//highScoreの値を表示
	}

    //HighScoreをNCMBに保存する為の関数
	void SaveHighScoreToNCMB()
	{
		Debug.Log("objectId : " + PlayerPrefs.GetString("objectId"));
		NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("OnlineRanking");//OnlineRankingクラスのNCMBObject作る
        query.WhereEqualTo("objectId", PlayerPrefs.GetString("objectId"));//『PlayerPrefs上で保存していたobjectIdに一致する行』という検索条件追加
        query.FindAsync((List<NCMBObject> objList, NCMBException e) => {//上の検索条件を使って検索！
            if (e != null)//検索失敗の時
            {
                //検索失敗時の処理
                Debug.Log("失敗！");
            }
            else//検索成功の時
            {
				Debug.Log("objectId : " + objList[0].ObjectId);//objectIdをコンソール表示
                objList[0]["HighScore"] = highScore;//highScoreの値をNCMBの"HighScore"に適応
                objList[0].SaveAsync();//変更内容save
            }
        });
	}
}
