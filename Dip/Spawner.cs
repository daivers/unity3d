using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public bool devMode;

	public Wave[] waves;   //массив волн
	public Enemy[] enemy;    //префаб врага



	LivingEntity playerEntity;
	Transform playerT;
	GameObject[] destr;
	Wave currentWave;      //количество волн
	int currentWaveNumber;  //  счетчик волн

	int enemiesRemainingToSpawn;  // количество врагов
	int enemiesRemainingAlive;   // количество оставшихся живых врагов
	float nextSpawnTime;  // задержка перед появлением нового врага

	MapGenerator map;

	float timeBetweenCampingChecks =2;
	float campThresholdDistance = 1.5f;
	float nextCampCheckTime;

	Vector3 campPositionOld;
	bool isCamping;

	bool isDisabled;

	public event System.Action<int> OnNewWave;


	void Start() {
		playerEntity = FindObjectOfType<Player>();
		playerT = playerEntity.transform;

		nextCampCheckTime = timeBetweenCampingChecks + Time.time;
		campPositionOld = playerT.position;
		playerEntity.OnDeath +=OnPlayerDeath;

		map = FindObjectOfType<MapGenerator> ();
		NextWave();
	}
	

	void Update() {
		if (!isDisabled) {
		if (Time.time > nextCampCheckTime) {
			nextCampCheckTime = Time.time + timeBetweenCampingChecks;

			isCamping = (Vector3.Distance(playerT.position,campPositionOld) < campThresholdDistance);
			campPositionOld = playerT.position;
		}


		if ((enemiesRemainingToSpawn > 0 || currentWave.infinite) && Time.time > nextSpawnTime) {   //если количество живых врагов больше нуля и время больше чем задержка перед каждым единичным появлением
			enemiesRemainingToSpawn--; //отнимаем 1 от счетчика всех врагов которые должны появиться 
			nextSpawnTime = Time.time + currentWave.timeBetweenSpawns; 

			StartCoroutine(SpawnEnemy());

		}
	}

		if (devMode) {
			if (Input.GetKeyDown(KeyCode.Escape)) {
				StopCoroutine("SpawnEnemy");
				foreach (Enemy enemy in FindObjectsOfType<Enemy>()) {
					GameObject.Destroy(enemy.gameObject);
				}
				NextWave();
			}
		}
	}



	IEnumerator SpawnEnemy() {    //рандомное появление врагов
		float spawnDelay = 1;
		float tileFlashSpeed = 4;

		Transform SpawnTile = map.GetRandomOpenTile ();
		if (isCamping) {
			SpawnTile = map.GetTileFromPosition(playerT.position);

		}


		Material tileMat = SpawnTile.GetComponent<Renderer> ().material;
		Color initialColour = Color.white;  //оригинальный цвет
		Color flashColour = Color.red; //изменение на красный цыет
		float spawnTimer = 0;

		while (spawnTimer < spawnDelay) {  //изменение цвета

			tileMat.color = Color.Lerp(initialColour,flashColour, Mathf.PingPong(spawnTimer * tileFlashSpeed, 1));

			spawnTimer += Time.deltaTime;
			yield return null;
			

		}
		int x = Random.Range(0,enemy.Length);

		Enemy spawnedEnemy = Instantiate (enemy[x], SpawnTile.position + Vector3.up, Quaternion.identity) as Enemy;  //создаем врага
		spawnedEnemy.OnDeath += OnEnemyDeath;   // Вызываем метод  OnEnemyDeath
		spawnedEnemy.SetCharacteristics (currentWave.moveSpeed, currentWave.hitsToKillPlayer, currentWave.enemyHealth, currentWave.skinColour);
	}


	void OnPlayerDeath() {
		isDisabled = true;    
	}

	void OnEnemyDeath() {
		enemiesRemainingAlive --;  //отнимаем 1 от количества живых игроков

		if (enemiesRemainingAlive == 0) {    //проверка, если живых игроков не осталось запускаем новую волну
			NextWave();
		}
	}

	void ResetPlayerPosition() {
	playerT.position = Vector3.up *3; //сброс позиции когда умераешь
	//	playerT.position = map.GetTileFromPosition (Vector3.zero).position + Vector3.up *3; //сброс позиции когда умераешь
	}

	void NextWave() {
		if (currentWaveNumber > 0) {
			AudioManager.instance.PlaySound2D ("Level completed");
		}
		currentWaveNumber ++;   //счетчик волн

		print ("Wave" + currentWaveNumber); //вывод в консоль счетчика волны
		if (currentWaveNumber - 1 < waves.Length) {  //если счетчик волн меньньше чем длина массива волн то
			currentWave = waves [currentWaveNumber - 1];  // цифра конкретной волны
			enemiesRemainingToSpawn = currentWave.enemyCount;  //количество врагов = счетчику врагов из конкретной волны
			enemiesRemainingAlive = enemiesRemainingToSpawn;  // количество живых врагов = количеству счетчика врагов
		
		if (OnNewWave != null) {
				OnNewWave(currentWaveNumber);
			}
			ResetPlayerPosition();
		
		}

		destr = GameObject.FindGameObjectsWithTag ("Destroy");   //удаляем обьекты при загрузке новой карты

		if (destr != null) {

		for(var i = 0 ; i < destr.Length ; i ++)
		{
			Destroy(destr[i]);
		}
		}
	}

	[System.Serializable]
	public class Wave {
		public bool infinite;  //бесконечная игра
		public int enemyCount;    //количество врагов
		public float timeBetweenSpawns;  //время до появление нового врага

		public float moveSpeed;  //скорость движения врага
		public int hitsToKillPlayer; //очки за убийство
		public float enemyHealth; //здоровье врага
		public Color skinColour;  //цвет врага
	}
}
