using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections; // pentru Coroutine
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // PERSISTENTE
    [Header("Date Persistente")]
    [Tooltip("Nivelul curent al campaniei (1 la 4)")]
    public int currentLevel = 1;
    [Tooltip("Munitia de rezerva care persista intre nivele")]
    public int reservedAmmo = 0;
    [Tooltip("Numarul de provizii colectate")]
    public int provisionsCollected = 0;

    // SETARI SCENE
    [Header("Setari Scene")]
    [Tooltip("Totalul de nivele de joc")]
    public int totalLevels = 4;
    public string menuSceneName = "menu";
    public string hubSceneName = "HUBLevel";
    public string loadingSceneName = "loading_screen";
    public string deathSceneName = "DeathScreen";
    public string youWinSceneName = "YouWinScreen";
    public string missionFailedSceneName = "MissionFailedScreen"; // Pierderea din HUB (fara provizii)
    public string loreSceneName = "LoreScreen";
    
    // CONDITII VICTORY
    [Header("Conditii Victorie")]
    [Tooltip("Numarul minim de provizii necesare pentru a castiga")]
    public int requiredProvisions = 3; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    // TRANZITIE SI SAVE

    /*
    public void StartNewGame()
    {
        StopAllCoroutines();
        Time.timeScale=1f;
        Debug.Log("Incepere Joc Nou. Resetare progres.");
        currentLevel = 1;
        reservedAmmo = 0;
        provisionsCollected = 0;
        StartCoroutine(LoadSceneAsync(loadingSceneName, loreSceneName));
    }
    */
    public void StartNewGame()
{
    Debug.Log("Buton New Game apasat!");

    // 1. REPORNIRE TIMP - Daca e 0, nimic nu se misca
    Time.timeScale = 1f;

    // 2. DEBLOCARE CURSOR - Daca e blocat din HUB, nu poti apasa butoane
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;

    // 3. CURATARE CORUTINE - Oprim orice incarcare veche blocata
    StopAllCoroutines();

    // 4. RESETARE VARIABILE
    currentLevel = 1;
    provisionsCollected = 0;
    reservedAmmo = 0;

    // 5. TESTUL SUPREM: Daca corutina e de vina, folosim incarcarea directa
    // Daca nici asta nu merge, inseamna ca Build Settings e problema
    UnityEngine.SceneManagement.SceneManager.LoadScene(loreSceneName);
}

    public void StartLevel1FromLore()
    {
        // Scena de Lore a terminat. Incarcam primul nivel.
        Debug.Log("Lore terminata. Incarcare Level 1.");
        LoadLevel(1);
    }

    public void LoadLevel(int levelIndex)
    {
        SavePlayerState(); 
        string targetSceneName = $"Level{levelIndex}";
        StartCoroutine(LoadSceneAsync(loadingSceneName, targetSceneName));
    }
    
    public void ReturnToBase()
    {
        SavePlayerState();
        StartCoroutine(LoadSceneAsync(loadingSceneName, hubSceneName)); // -1 pentru HUB scene
    }

    public void ScavengeForNextLevel()
    {
        if (currentLevel < totalLevels)
        {
            currentLevel++;
            Debug.Log($"Schimbare de curs: Scavenge. Se incarca Level {currentLevel}");
            LoadLevel(currentLevel); 
        }
        else
        {
            MissionFailedLostInSpace();
        }
    }

   public void AttemptToWin()
    {
    if (provisionsCollected >= requiredProvisions)
    {
        GameWon(); 
    }
        Debug.Log($"Mai ai nevoie de inca {requiredProvisions - provisionsCollected} provizii pentru a castiga!");
    }

    IEnumerator LoadSceneAsync(string transitionScene, string targetSceneName)
    {
        SceneManager.LoadScene(transitionScene);
        yield return null;

        var asyncLoad = SceneManager.LoadSceneAsync(targetSceneName);
    
         asyncLoad.allowSceneActivation = true; 

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Debug.Log($"Incarcare {targetSceneName}: {progress * 100}%");
        
            yield return null; 
        }
    
        Debug.Log("Incarcare finalizata!");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.StartsWith("Level") || scene.name == hubSceneName)
        {
            RestorePlayerState();
        }

        if (scene.name.StartsWith("Level"))
        {
            if (int.TryParse(scene.name.Replace("Level", ""), out int levelIndex))
            {
                currentLevel = levelIndex;
            }
        }
    if (scene.name == "menu")
    {
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
            Debug.Log("GameManager: EventSystem lipsea, asa ca l-am creat!");
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
    }
    // 1. Verificăm dacă există un EventSystem în noua scenă
    if (FindObjectOfType<EventSystem>() == null)
    {
        // 2. Dacă nu există (cum se întâmplă când te întorci în meniu), îl creăm
        GameObject eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();
        Debug.Log("GameManager: EventSystem a fost creat automat pentru scena: " + scene.name);
    }

    // 3. Deblocăm mouse-ul și timpul dacă suntem în meniu
    if (scene.name == "menu")
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
    }

    }


    // LOGICA DE PERSISTENTA

    void SavePlayerState()
    {
        // Gaseste scriptul de tragere al jucatorului in scena curenta
        PlayerShooting playerShooting = FindObjectOfType<PlayerShooting>();
        if (playerShooting != null)
        {
            reservedAmmo = playerShooting.ammoReserve;
        }
    }

    void RestorePlayerState()
    {
        PlayerShooting playerShooting = FindObjectOfType<PlayerShooting>();
        if (playerShooting != null)
        {
            playerShooting.ammoReserve = reservedAmmo;

            HUD hud = FindObjectOfType<HUD>();
            if (hud != null)
            {
                hud.UpdateAmmo(playerShooting.currentAmmoInGun, playerShooting.ammoReserve);
            }
        }
    }

    // LOGICA DE FINAL JOC
    
    public void PlayerDied()
    {
        Debug.Log("Jucatorul a murit!");
        SceneManager.LoadScene(deathSceneName);
    }

    public void GameWon()
    {
        Debug.Log("Felicitari! Ai castigat jocul!");
        SceneManager.LoadScene(youWinSceneName);
    }

    public void MissionFailedLostInSpace()
    {
        Debug.Log("Game Over: Ai esuat in cautare.");
        SceneManager.LoadScene(missionFailedSceneName);
    }
    
    // INVENTAR
    
    public void CollectProvision()
    {
        provisionsCollected++;
        Debug.Log($"Provizie colectata. Total: {provisionsCollected} / {requiredProvisions}");
    }

    public void ResetGame()
    {
        provisionsCollected = 0;
        currentLevel = 1;
        reservedAmmo = 0;
        Debug.Log("Game State has been reset.");
    }

void OnEnable()
{
    UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
}

void OnDisable()
{
    UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
}

}