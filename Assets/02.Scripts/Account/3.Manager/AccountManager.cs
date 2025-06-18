using System.Collections.Generic;
using System;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;

public class AccountManager : MonoBehaviour
{
    public static AccountManager Instance;

    private Account _myAccount;
    public AccountDTO CurrentAccount => _myAccount.ToDTO();

    private AccountRepository _repository;
    private const string SALT = "k2";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Init();
    }

    public void Init()
    {
        _repository = new AccountRepository();
    }

    public Result TryRegister(string email, string nickname, string password)
    {
        AccountSaveData saveData = _repository.Find(email);
        if(saveData != null)
        {
            return new Result(false, "이미 가입한 이메일입니다.");
        }

        string encryptedPassword = CryptoUtil.Encryption(password,SALT);
        Account account = new Account(email, nickname, encryptedPassword);
        _repository.Save(account.ToDTO());
        return new Result(true);
    }
    public bool TryLogin(string email, string password)
    {
        AccountSaveData saveData = _repository.Find(email);
        if (saveData  == null)
        {
            return false;
        }

        if(CryptoUtil.Verify(password, saveData.Password, SALT))
        {
            _myAccount = new Account(saveData.Email, saveData.Nickname, saveData.Password);
            return true;
        }

        return false;
    }

   
}
