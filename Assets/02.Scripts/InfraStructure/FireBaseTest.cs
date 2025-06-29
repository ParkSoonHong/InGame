using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseTest : MonoBehaviour
{
    private FirebaseApp _app;
    private FirebaseAuth _auth;
    private FirebaseFirestore _db;

    private void Start()
    {
        Init();
    }


    // 파이어베이스 내 프로젝트에 연결
    private void Init()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                Debug.Log("파이어베이스 연결에 성공했습니다.");

                _app = FirebaseApp.DefaultInstance;
                _auth = FirebaseAuth.DefaultInstance;
                _db = FirebaseFirestore.DefaultInstance;
                //Register();
                Login();
            }
            else
            {
                Debug.LogError($"파이어베이스 연결 실패했습니다. ${dependencyStatus}");
            }
        });
    }

    private  void Register()
    {
        var email = "teemo@gmail.com";
        var password = "123456";

        _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"회원가입에 실패했습니다: ${task.Exception.Message}");
                return;
            }

            // Firebase user has been created.
            var result = task.Result;
            Debug.LogFormat("회원가입에 성공했습니다: {0} ({1})", result.User.DisplayName, result.User.UserId);
        });
    }

    private void Login()
    {
        var email = "teemo@gmail.com";
        var password = "123456";

        _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"로그인에 실패했습니다: {task.Exception.Message}");
                return;
            }

            var result = task.Result;
            Debug.LogFormat("로그인에 성공했습니다: {0} ({1})", result.User.DisplayName, result.User.UserId);

            NicknameChange();

            AddMyRanking();
            //GetMyRanking();
            GetMyRankings();
        });
    }

    private void NicknameChange()
    {
        var user = _auth.CurrentUser;

        if (user == null) return;

        var profile = new UserProfile
        {
            DisplayName = "teemo"
        };

        user.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"닉네임 변경에 실패했습니다: {task.Exception.Message}");
                return;
            }

            Debug.Log("닉네임 변경에 성공했습니다.");
        });
    }

    private void GetProfile()
    {
        var user = _auth.CurrentUser;
        if (user == null) return;

        var nickname = user.DisplayName;
        var email = user.Email;

        var account = new Account(email, nickname, "firebase");
    }


    private void AddMyRanking()
    {

        var ranking = new Ranking("tete@test.com", "ttt", 233);

        var rankingDict = new Dictionary<string, object>
        {
            { "Email", ranking.Email},
            { "Nickname",ranking.Nickname },
            { "Score", ranking.Score }
        };


        //_db.Collection("rankings").AddAsync(rankingDict).ContinueWithOnMainThread(task =>
        //{
        //    if (task.IsCanceled || task.IsFaulted)
        //    {
        //        Debug.LogError($"데이터 추가가 실패했습니다.: {task.Exception.Message}");
        //        return;
        //    }
        //});

        _db.Collection("rankings").Document(ranking.Email).SetAsync(rankingDict).ContinueWithOnMainThread(task =>
        {
            Debug.Log(string.Format("Added or Updated document with ID: {0}.", task.Id));
        });
    }

    private void GetMyRanking()
    {
        var email = "huhuhu@gmail.com"; // ID 역할

        var docRef = _db.Collection("rankings").Document(email);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            var snapshot = task.Result;

            if (snapshot.Exists)
            {
                Debug.Log(string.Format("Document data for {0} document:", snapshot.Id));
                var rankingDict = snapshot.ToDictionary();
                foreach (var pair in rankingDict)
                {
                    Debug.Log(string.Format("{0}: {1}", pair.Key, pair.Value));
                }
            }
            else
            {
                Debug.Log(string.Format("Document {0} does not exist!", snapshot.Id));
            }
        });
    }

    private void GetMyRankings()
    {
        //쿼리(질의)란 컬렉션으로부터 데이터를 가져올때 어떻게 가져와라라고 쓰는 명령문
        Query capitalQuery = _db.Collection("rankings").OrderByDescending("Score");

        capitalQuery.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            QuerySnapshot capitalQuerySnapshot = task.Result;

            foreach (DocumentSnapshot documentSnapshot in capitalQuerySnapshot.Documents)
            {
                Debug.Log(String.Format("Document data for {0} document:", documentSnapshot.Id));
                Dictionary<string, object> rankingDict = documentSnapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in rankingDict)
                {
                    Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                }

                // Newline to separate entries
                Debug.Log("");
            }
            ;
        });
    }
}