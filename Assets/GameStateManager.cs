using Models;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
    public class GameStateManager : MonoBehaviour
    {
        public enum GameState
        {
            NotStarted = 1,
            Running,
            Paused,
            Over
        }

        public int playerScore;
        public int LifeCount;
        public int GameLevel;

        public SavedData savedData;

        private const int OneSecondInterval = 1;
        private float LevelTimer;

        private int currentTime;

        public GameState State;

        public Text scoreText;
        public Text HighScoreText;
        public Text TimerText;
        public Text LevelText;
        public Text LivesText;

        public GameObject StartScreen;
        public GameObject PauseScreen;
        public GameObject GameOverScreen;
        public ControlPlane ControlPlaneScript;

        // Start is called before the first frame update
        void Start()
        {
            State = GameState.NotStarted;
            savedData = new SavedData();

            Initialize();


            currentTime = TimerLevelFunction(GameLevel);
            LevelTimer = 0;
            LifeCount = 5;

            LoadSavedData();

            TimerText.text = ConvertSecondsToTimeString(currentTime);
            LevelText.text = $"Level: {GameLevel}";
            LivesText.text = $"Lives: {LifeCount}";


            // Start game with time paused
            Time.timeScale = 0;


            // Assign with Editor instead since FindGameObjectWithTag does not find inactive gameobjects
            /*
            StartScreen = GameObject.FindGameObjectWithTag("StartScreen")
            PauseScreen = GameObject.FindGameObjectWithTag("PauseScreen")
            GameOverScreen = GameObject.FindGameObjectWithTag("GameOverScreen")
            */

            ControlPlaneScript = GameObject.FindGameObjectWithTag("ControlPlane").GetComponent<ControlPlane>();

        }

        // Update is called once per frame
        void Update()
        {
            scoreText.text = playerScore.ToString();

            if(State == GameState.Running)
            {
                if (LevelTimer > OneSecondInterval)
                {
                    currentTime -= 1;
                    TimerText.text = ConvertSecondsToTimeString(currentTime);
                    LevelTimer = 0;
                }
                else
                {
                    LevelTimer += Time.deltaTime;
                }

                if (currentTime <= 0)
                {
                    OnLevelComplete();
                }
            }


            if (Input.GetKeyDown(KeyCode.Escape))
            {
                switch (State)
                {
                    case GameState.NotStarted:
                    case GameState.Over:
                        break;

                    case GameState.Paused:
                        ResumeGame();
                        break;

                    case GameState.Running:
                        PauseGame();
                        break;

                    default:
                        break;
                }
            }
        }

        public void UpdateScore(int points, int hitEffect)
        {
            if((playerScore + points) >= 0)
            {
                playerScore += points;
            }

            if (IsHighScore(playerScore))
            {
                UpdateHighScore(playerScore);
            }

            LifeCount += hitEffect;

            if (LifeCount <= 0)
            {
                LifeCount = 0;
            }

            HighScoreText.text =  savedData.HighScore.ToString();
            LivesText.text = $"Lives: {LifeCount}";

            if (LifeCount <= 0)
            {
                EndGame();
            }
        }

        public bool IsHighScore(int score)
        {
            return score > savedData.HighScore;
        }

        public void StartGame()
        {
            Initialize();
            StartScreen.SetActive(false);
            ControlPlaneScript.Initialize();
            Time.timeScale = 1;
            State = GameState.Running;
        }

        public void EndGame()
        {
            if(IsHighScore(playerScore))
            {
                UpdateHighScore(playerScore);
            }

            switch (State)
            {
                case GameState.NotStarted:
                case GameState.Over:
                case GameState.Running:
                    break;

                case GameState.Paused:
                    PauseScreen.SetActive(false);
                    break;


                default:
                    break;
            }

            State = GameState.Over;
            GameOverScreen.SetActive(true);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            Initialize();
            LoadSavedData();

            State = GameState.Running;
            Time.timeScale = 1;
        }


        public void PauseGame()
        {
            State = GameState.Paused;
            Time.timeScale = 0;
            PauseScreen.SetActive(true);
        }

        public void ResumeGame()
        {
            State = GameState.Running;
            Time.timeScale = 1;
            PauseScreen.SetActive(false);
        }
        public void QuitGame()
        {
            Application.Quit();
        }

        public void Initialize()
        {
            playerScore = 0;
            GameLevel = 1;
        }

        /// <summary>
        /// Loads Saved Data On Start
        /// </summary>
        public void LoadSavedData()
        {
            string saved = DataManager.LoadJsonData();
            if (string.IsNullOrEmpty(saved))
            {
                return;
            }

            try
            {
                savedData.FromJson(saved);
                HighScoreText.text = savedData.HighScore.ToString();
            }
            catch(Exception e)
            {
                Debug.LogException(e);
            }
        }

        void UpdateHighScore(int newHighScore)
        {
            savedData.HighScore = newHighScore;
            try
            {
                Debug.Log(savedData.ToJson());
                if (DataManager.SaveJsonData(savedData.ToJson()))
                {
                    Debug.Log("Updated High Score");
                }
                else
                {
                    Debug.Log("Unable To Update High Score");
                }
            }
            catch(Exception e)
            {
                Debug.LogException(e);
            }
        }

        private string ConvertSecondsToTimeString(int seconds)
        {
            return TimeSpan.FromSeconds(seconds).ToString(@"mm\:ss");
        }

        void OnLevelComplete()
        {
            GameLevel += 1;
            currentTime = TimerLevelFunction(GameLevel);
            LevelText.text = $"Level: {GameLevel}";
        }


        /// <summary>
        /// The Amount of time each level lasts
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private int TimerLevelFunction(int level)
        {
            if(level == 1)
            {
                return 30; // 30 s
            }
            else if(level >= 2 && level <= 3) {
                return 2 * 30; // 1 min
            }
            else if (level >= 4 && level <= 6)
            {
                return 4 * 30; // 2 min
            }

            return 6 * 30; // 4 min
        }
    }
}