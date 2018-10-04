using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Canvas使う
using NCMB;//NCMB使う
using UnityEngine.SceneManagement;//シーン遷移扱う

public class OnlineRankingManager : MonoBehaviour {

	public Text[] userNameText = new Text[5];//UerName表示の為のText
	public Text[] highScoreText = new Text[5];//HighScore表示の為のText

	// Use this for initialization
	void Start () {
		MakeRankingView();//Rankingを作る関数
	}

    //Rankingを作る関数
	void MakeRankingView()
    {
        // データストアの「HighScore」クラスから検索
        NCMBQuery<NCMBObject> queryTopRank = new NCMBQuery<NCMBObject>("OnlineRanking");
		queryTopRank.OrderByDescending("HighScore");//降順(大きい順)にHighScore列を並べる
        queryTopRank.Limit = 5;//上から5個だけにする
		queryTopRank.FindAsync((List<NCMBObject> objList, NCMBException e) => {//上記の検索条件で検索！

            if (e != null)//エラーあった時
            {
                //検索失敗時の処理
            }
            else//うまく行ったとき
            {
                for (int i = 0; i < objList.Count; i++)//objListの要素数と同じだけfor回す
                {
                    //順次ランキング表示！
                    userNameText[i].text = System.Convert.ToString(objList[i]["UserName"]);//String型に変換して表示
					highScoreText[i].text = System.Convert.ToString(objList[i]["HighScore"]);//String型に変換して表示
                }
            }
        });
    }

    //BackButton押した時に呼ばれる関数
	public void BackButton()
	{
		SceneManager.LoadScene("StartScene");//StartSceneに遷移
	}
}
