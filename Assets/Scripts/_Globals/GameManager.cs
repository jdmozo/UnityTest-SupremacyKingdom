using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System;

namespace SupremacyKingdom
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public static event Action<bool> OnGameEnd;
        public static event Action<int> ScoreChange;

        [SerializeField] private bool GameStarted;

        public CameraFollow cameraFollow;
        private int currentBirdIndex;
        private int score;
        public SlingShot slingshot;
        [HideInInspector] public static GameState CurrentGameState = GameState.Idle;
        [SerializeField] private List<GameObject> Bricks;
        [SerializeField] private List<GameObject> Birds;
        [SerializeField] private Transform birdsSpwanPosition;
        public List<CardInfo> BirdsSelected;
        [SerializeField] private List<GameObject> Pigs;

        public int Score { get => score; set => score = value; }

        private void Awake() => instance = this;

        void Update()
        {
            switch (CurrentGameState)
            {
                case GameState.Idle:
                    break;
                case GameState.Start:
                    if (Input.GetMouseButtonUp(0))
                        AnimateBirdToSlingshot();
                    break;
                case GameState.BirdMovingToSlingshot:
                    break;
                case GameState.Playing:
                    if (slingshot.slingshotState == SlingshotState.BirdFlying &&
                        (BricksBirdsPigsStoppedMoving() || Time.time - slingshot.TimeSinceThrown > 5f))
                    {
                        slingshot.enabled = false;
                        AnimateCamera_ToStartPosition();
                        CurrentGameState = GameState.BirdMovingToSlingshot;
                    }
                    break;
                case GameState.Won:
                    CurrentGameState = GameState.Idle;
                    OnGameEnd?.Invoke(true);
                    break;
                case GameState.Lost:
                    CurrentGameState = GameState.Idle;
                    OnGameEnd?.Invoke(false);
                    break;
                default:
                    break;
            }
        }
        
        public IEnumerator StartGame()
        {
            slingshot.enabled = false;
            Bricks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Brick"));
            Pigs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Pig"));

            for (int i = 0; i < BirdsSelected.Count; i++)
            {
                GameObject newBird = Instantiate(BirdsSelected[i].birdPrefab, birdsSpwanPosition.position, Quaternion.identity);
                Birds.Add(newBird);
                newBird.transform.position = new Vector3(newBird.transform.position.x - 1 * i, newBird.transform.position.y);
            }

            slingshot.BirdThrown -= Slingshot_BirdThrown; slingshot.BirdThrown += Slingshot_BirdThrown;

            CurrentGameState = GameState.Start;

            yield return null;
        }

        private bool KillAllEnemies() => Pigs.All(x => x == null);

        private void AnimateCamera_ToStartPosition()
        {
            float duration = Vector2.Distance(Camera.main.transform.position, cameraFollow.StartingPosition) / 10f;
            if (duration == 0.0f) duration = 0.1f;
            //animate the camera to start
            Camera.main.transform.positionTo
                (duration,
                cameraFollow.StartingPosition). //end position
                setOnCompleteHandler((x) =>
                {
                    cameraFollow.IsFollowing = false;
                    if (KillAllEnemies())
                        CurrentGameState = GameState.Won;
                    else if (currentBirdIndex == Birds.Count - 1)
                        CurrentGameState = GameState.Lost;
                    else
                    {
                        slingshot.slingshotState = SlingshotState.Idle;
                        currentBirdIndex++;
                        AnimateBirdToSlingshot();
                    }
                });
        }

        private void AnimateBirdToSlingshot()
        {
            CurrentGameState = GameState.BirdMovingToSlingshot;

            for (int i = 0; i < Birds.Count; i++)
                if (Birds[i] != Birds[currentBirdIndex] && currentBirdIndex < i)
                    Birds[i].transform.positionTo(Vector2.Distance(Birds[i].transform.position,
                        new Vector2(Birds[i].transform.position.x + 1 * i, Birds[i].transform.position.y)),
                        new Vector2(Birds[i].transform.position.x + 1, Birds[i].transform.position.y));


            Birds[currentBirdIndex].transform.positionTo
                (Vector2.Distance(Birds[currentBirdIndex].transform.position / 10,
                slingshot.BirdWaitPosition.transform.position) / 10, //duration
                slingshot.BirdWaitPosition.transform.position). //final position
                    setOnCompleteHandler((x) =>
                            {
                                x.complete();
                                x.destroy();
                                CurrentGameState = GameState.Playing;
                                slingshot.enabled = true;
                                slingshot.BirdToThrow = Birds[currentBirdIndex];
                            });
        }

        private void Slingshot_BirdThrown(object sender, System.EventArgs e)
        {
            cameraFollow.BirdToFollow = Birds[currentBirdIndex].transform;
            cameraFollow.IsFollowing = true;
        }

        private bool BricksBirdsPigsStoppedMoving()
        {
            foreach (var item in Bricks.Union(Birds).Union(Pigs))
            {
                if (item != null && item.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > Constants.Min_Velocity)
                    return false;
            }

            return true;
        }

        public void AddScore(int score)
        {
            Score += score;
            ScoreChange?.Invoke(score);
        }

        //public static void AutoResize(int screenWidth, int screenHeight)
        //{
        //    Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
        //    GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
        //}

        //void OnGUI()
        //{
        //    AutoResize(800, 480);
        //    switch (CurrentGameState)
        //    {
        //        case GameState.Start:
        //            GUI.Label(new Rect(0, 150, 200, 100), "Tap the screen to start");
        //            break;
        //        case GameState.Won:
        //            GUI.Label(new Rect(0, 150, 200, 100), "Has ganado! Tap the screen to restart");
        //            break;
        //        case GameState.Lost:
        //            GUI.Label(new Rect(0, 150, 200, 100), "Has perdido! Tap the screen to restart");
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }
}