using System.Collections.Generic;
using UnityEngine;
using Library.Social.Leaderboard;
using System;

public class Leaderboard {

    // Initialize Leaderboards
    public static void InitializeLeaderboard() {
        Debug.Log("InitializeLeaderboard for first Time");
        Dictionary<string, int> setupList = new Dictionary<string, int>();

        setupList.Add(Strings.LEADERBOARD_01, 0);

        PlayFabLeaderboard.SubmitScores(setupList);
    }

    // Submit Score
    public static void SubmitSingleScore(int score) {
        PlayFabLeaderboard.SubmitScore(Strings.LEADERBOARD_01, score);
    }

    // Get Scores
    public static void GetScores(Action<Dictionary<string, int>> resultCallback, Action<string> errorCallback) {
        PlayFabLeaderboard.GetScores(
            (result) => {
                resultCallback(result);
            },
            (errorMessage) => {
                Debug.LogError(errorMessage);
                errorCallback(errorMessage);
            });
    }

    // Get Global Leaderboard that around me as a center
    public static void GetGlobalLeaderboardAroundMe(int maxResultCount, Action<List<ResultPlayer>> resultCallback, Action<string> errorCallback) {
        PlayFabLeaderboard.GetLeaderboardGlobal(
            maxResultCount,
            Strings.LEADERBOARD_01,
            (result) => {
                resultCallback(result);
            },
            (errorMessage) => {
                errorCallback(errorMessage);
            });
    }

    // Get Global Leaderboard by Position around
    public static void GetGlobalLeaderboardTrimByPosition(int startPosition, int maxResultCount, Action<List<ResultPlayer>> resultCallback, Action<string> errorCallback) {
        PlayFabLeaderboard.GetLeaderboardGlobal(
            startPosition,
            maxResultCount,
            Strings.LEADERBOARD_01,
            (result) => {
                resultCallback(result);
            },
            (errorMessage) => {
                errorCallback(errorMessage);
            });
    }

    // Get Facebook Leaderboard that around me as a center
    public static void GetFacebookLeaderboardAroundMe(int maxResultCount, Action<List<ResultPlayer>> resultCallback, Action<string> errorCallback) {
        PlayFabLeaderboard.GetLeaderboardFacebookandFriends(
            maxResultCount,
            Strings.LEADERBOARD_01,
            (result) => {
                resultCallback(result);
            },
            (errorMessage) => {
                errorCallback(errorMessage);
            });
    }

    // Get Facebook Leaderboard by Position around
    public static void GetFacebookLeaderboardTrimByPosition(int startPosition, int maxResultCount, Action<List<ResultPlayer>> resultCallback, Action<string> errorCallback) {
        PlayFabLeaderboard.GetLeaderboardFacebookandFriends(
            startPosition,
            maxResultCount,
            Strings.LEADERBOARD_01,
            (result) => {
                resultCallback(result);
            },
            (errorMessage) => {
                errorCallback(errorMessage);
            });
    }

}
