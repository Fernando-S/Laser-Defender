using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;


    private void Awake() {
        SetUpSingleton();
    }


    // Singleton method
    private void SetUpSingleton(){
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numberOfGameSessions > 1){
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(gameObject);
        }
    }

    
    // Method to inscrease score
    public void AddToScore(int value){
        score += value;
    }


    // Method to reset game
    public void ResetGame(){
        Destroy(gameObject);
    }


    // Getters and Setters
    public int GetScore(){
        return score;
    }

    public void SetScore(int new_score){
        score = new_score;
    }

}
