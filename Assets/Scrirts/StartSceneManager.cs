using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン遷移の際に必要！
using NCMB;//NCMB使う為の準備！
using UnityEngine.UI;//Canvas使う為の準備

public class StartSceneManager : MonoBehaviour {

	public GameObject userNamePannel;
	public InputField inputField;

	void Start()
	{
		if(PlayerPrefs.HasKey("objectId") == false)//objectIdがPlayerPrefsに保存されていない時→初回プレイの時
		{
			userNamePannel.SetActive(true);//UserNameを決める為のPannelが表示
		}
	}

    //StartButton押した時に呼ばれる関数
	public void StratButton(){//後でunityから適応するからpublic指定！
		SceneManager.LoadScene("Main");
	}

    //UserNamePannelのDoneButton押した時に呼ばれる関数
	public void DoneButton()
	{
		string userName = inputField.text;

		if(userName.Length != 0)//userNameが0文字じゃない時(何か名前が入力されている時)
		{
			NCMBObject onlineRanking = new NCMBObject("OnlineRanking");//OnlineRankingクラスのNCMBObjectを作成→変数onlineRankingに格納
			onlineRanking["UserName"] = userName;//変数userNameの値を"UserName"列に追加
			onlineRanking.SaveAsync((NCMBException e) => { //変更内容Saveする(eにはエラーが入ってる)
				if(e != null)//eがnullでないとき→save時エラー出た時
				{
					Debug.Log("NCMB Save Missed!!");
				}
				else//eがnullの時→save成功した時
				{
					Debug.Log("objectId : " + onlineRanking.ObjectId);
                    PlayerPrefs.SetString("objectId", onlineRanking.ObjectId);//新しくできた行のobjectIdを取得→PlayerPrefsに"objectId"の名前で保存！(Saveの時に使う為)
				}
			});
            
			userNamePannel.SetActive(false);//userNamePannelを非表示に
		}
	}

    //OnlineRankingButtonが入力された時に呼ばれる関数
	public void OnlineRankingButton()
	{
		SceneManager.LoadScene("OnlineRanking");
	}
}
