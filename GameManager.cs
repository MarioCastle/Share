using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float AutoDropTime => autoDropTime;

    public static GameManager Instance;

    [SerializeField] GameObject[] tetrominoPrefabs;
    [SerializeField] Transform tetrominoSpawnerTransform; 
    [SerializeField] GameObject gameOverScreen;

    [SerializeField] float autoDropTime = 1.5f;

    void Awake()
    {

        Instance = this;
        SpawnTetromino(); 
    }


    public void SpawnTetromino() => Instantiate(tetrominoPrefabs[Random.Range(0,tetrominoPrefabs.Length)],tetrominoSpawnerTransform.position,Quaternion.identity);      

    public void GameOver()
    {
        Time.timeScale = 0f;
        PlayerInput.DisableAllInputs();
        gameOverScreen.SetActive(true);

    }
    public void OnTryAgainButtonClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    
}
