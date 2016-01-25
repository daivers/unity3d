using UnityEngine;
using System.Collections;

public class Object_Spawner : MonoBehaviour {
	public static Object_Spawner object_spawner;
	public GameObject player;
	public GameObject[] coins;
	public GameObject[] trees;
	public GameObject[] bonusGO;
	public GameObject enemy;

	private float coinSpawnTimer = 2.0f;
	private float enemySpawnTimer = 0.5f;
	private float treeSpawnTimer = 0.5f;
	private float treeUpSpawnTimer = 0.5f;
	private float bonusSpawnTimer = 10f;

	private Vector3 treeXSpawnStartLocation;   //убираем задержку в появлении камней
	private float treeStartSpawnTimes = 20;

	public int proverkaHightScore;

	void Start () {
		treeXSpawnStartLocation.x = -30;
		proverkaHightScore = PlayerPrefs.GetInt("highScore");

//		SpawnStartTrees ();
	}
	void FixedUpdate () {
		coinSpawnTimer -= Time.deltaTime;
		enemySpawnTimer -= Time.deltaTime;
		treeSpawnTimer -= Time.deltaTime;
		bonusSpawnTimer -= Time.deltaTime;


		if (coinSpawnTimer < 0.01 && Game_Init.gameIsPlaying == true) {
			SpawnCoins();
		}
		if (enemySpawnTimer < 0.01 && Game_Init.gameIsPlaying == true) {
			SpawnEnemy();
		}
		if (treeSpawnTimer < 0.001 && Game_Init.gameIsPlaying == true) {
			SpawnTrees();
		}
		if (bonusSpawnTimer < 0.001 && Game_Init.gameIsPlaying == true) {
			SpawnBonus();
		}

	}

	//void SpawnStartTrees () {
	//	while (treeStartSpawnTimes > 0) {
	//		GameObject tree = Instantiate (trees [(Random.Range (0, trees.Length))], new Vector3 (treeXSpawnStartLocation.x +50, 3.5f, 1), Quaternion.identity) as GameObject;  //концовка это вращение вокруг своей оси
	//		tree.transform.localScale = new Vector3 (1f, 1f, 1f);  // изменение размеров камней
	//		treeXSpawnStartLocation.x = treeXSpawnStartLocation.x +5;
	//		treeStartSpawnTimes --;
	//	}
	//}

	void SpawnCoins () {     // рандомное появление монеток
		GameObject coin = Instantiate (coins [(Random.Range (0, coins.Length))], new Vector3 (player.transform.position.x +250, Random.Range (-60, 60)), Quaternion.identity) as GameObject;
		coin.transform.localScale = new Vector3 (3, 3);  // изменение размеров камней
		coinSpawnTimer = 0.7f;
	}
	void SpawnEnemy (){    // рандомное появление enemy
		enemy.transform.localScale = new Vector3(5, 5);   //задаем размер препятствий
		Instantiate (enemy, new Vector3 (player.transform.position.x + 200, Random.Range (-20, 20), 0), Quaternion.identity) ;
		enemySpawnTimer = 1.5f;
	}
	void SpawnTrees() {
		if (proverkaHightScore >= 0 && proverkaHightScore < 10) 
		{
			GameObject tree = Instantiate (trees [0], new Vector3 (player.transform.position.x +420, -50), Quaternion.identity) as GameObject;
			GameObject treeUp = Instantiate (trees [0], new Vector3 (player.transform.position.x +600, 50), Quaternion.Euler (180, 0, 0)) as GameObject;
			treeUp.transform.localScale = new Vector3 (15, 15);  // изменение размеров камней
			tree.transform.localScale = new Vector3 (15, 15);  // изменение размеров камней
			treeSpawnTimer = 2.65f;

		}
		if (proverkaHightScore >= 10 && proverkaHightScore < 20) 
		{
			GameObject tree = Instantiate (trees [(Random.Range (0, 1))], new Vector3 (player.transform.position.x +420, -50), Quaternion.identity) as GameObject;
			GameObject treeUp = Instantiate (trees [(Random.Range (0, 1))], new Vector3 (player.transform.position.x +600, 50), Quaternion.Euler (180, 0, 0)) as GameObject;
			treeUp.transform.localScale = new Vector3 (15, 15);  // изменение размеров камней
			tree.transform.localScale = new Vector3 (15, 15);  // изменение размеров камней
			treeSpawnTimer = 2.65f;
		}

		if (proverkaHightScore >= 20 && proverkaHightScore < 30) 
		{
			GameObject tree = Instantiate (trees [(Random.Range (0, 2))], new Vector3 (player.transform.position.x +420, -50), Quaternion.identity) as GameObject;
			GameObject treeUp = Instantiate (trees [(Random.Range (0, 2))], new Vector3 (player.transform.position.x +600, 50), Quaternion.Euler (180, 0, 0)) as GameObject;
			treeUp.transform.localScale = new Vector3 (15, 15);  // изменение размеров камней
			tree.transform.localScale = new Vector3 (15, 15);  // изменение размеров камней
			treeSpawnTimer = 2.65f;
		}

		if (proverkaHightScore >= 30 && proverkaHightScore < 40) 
		{
			GameObject tree = Instantiate (trees [(Random.Range (0, 3))], new Vector3 (player.transform.position.x +420, -50), Quaternion.identity) as GameObject;
			GameObject treeUp = Instantiate (trees [(Random.Range (0, 3))], new Vector3 (player.transform.position.x +600, 50), Quaternion.Euler (180, 0, 0)) as GameObject;
			treeUp.transform.localScale = new Vector3 (15, 15);  // изменение размеров камней
			tree.transform.localScale = new Vector3 (15, 15);  // изменение размеров камней
			treeSpawnTimer = 2.65f;
		}
		if (proverkaHightScore >= 40 && proverkaHightScore < 50) 
		{
			GameObject tree = Instantiate (trees [(Random.Range (0, 4))], new Vector3 (player.transform.position.x +420, -50), Quaternion.identity) as GameObject;
			GameObject treeUp = Instantiate (trees [(Random.Range (0, 4))], new Vector3 (player.transform.position.x +600, 50), Quaternion.Euler (180, 0, 0)) as GameObject;
			treeUp.transform.localScale = new Vector3 (15, 15);  // изменение размеров камней
			tree.transform.localScale = new Vector3 (15, 15);  // изменение размеров камней
			treeSpawnTimer = 2.65f;
		}
		if (proverkaHightScore >= 50 && proverkaHightScore < 60) 
		{
			GameObject tree = Instantiate (trees [(Random.Range (0, 5))], new Vector3 (player.transform.position.x +420, -50), Quaternion.identity) as GameObject;
			GameObject treeUp = Instantiate (trees [(Random.Range (0, 5))], new Vector3 (player.transform.position.x +600, 50), Quaternion.Euler (180, 0, 0)) as GameObject;
			treeUp.transform.localScale = new Vector3 (15, 15);  // изменение размеров камней
			tree.transform.localScale = new Vector3 (15, 15);  // изменение размеров камней
			treeSpawnTimer = 2.65f;
		}
		if (proverkaHightScore >= 60 && proverkaHightScore < 70) 
		{
			GameObject tree = Instantiate (trees [(Random.Range (0, 6))], new Vector3 (player.transform.position.x +420, -50), Quaternion.identity) as GameObject;
			GameObject treeUp = Instantiate (trees [(Random.Range (0, 6))], new Vector3 (player.transform.position.x +600, 50), Quaternion.Euler (180, 0, 0)) as GameObject;
			treeUp.transform.localScale = new Vector3 (15, 15);  // изменение размеров камней
			tree.transform.localScale = new Vector3 (15, 15);  // изменение размеров камней
			treeSpawnTimer = 2.65f;
		}
		if (proverkaHightScore >= 70) 
		{
			GameObject tree = Instantiate (trees [(Random.Range (0, 7))], new Vector3 (player.transform.position.x +420, -50), Quaternion.identity) as GameObject;
			GameObject treeUp = Instantiate (trees [(Random.Range (0, 7))], new Vector3 (player.transform.position.x +600, 50), Quaternion.Euler (180, 0, 0)) as GameObject;
			treeUp.transform.localScale = new Vector3 (15, 15);  // изменение размеров камней
			tree.transform.localScale = new Vector3 (15, 15);  // изменение размеров камней
			treeSpawnTimer = 2.65f;
		}


	}

	void SpawnBonus () {     // рандомное появление монеток
		GameObject bonus = Instantiate (bonusGO [(Random.Range (0, bonusGO.Length))], new Vector3 (player.transform.position.x +250, Random.Range (-60, 60)), Quaternion.identity) as GameObject;
		bonus.transform.localScale = new Vector3 (3, 3);  // изменение размеров камней
		bonusSpawnTimer = Random.Range (5f, 15f);
	}
}


