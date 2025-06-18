using System;
using System.Text.RegularExpressions;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Account 
{
    public readonly string Email;
    public readonly string Nickname;
    public readonly string Password;

   

  

    public Account(string email, string nickname, string password)
    {
        // ��Ģ�� ��ü�� ĸ��ȭ�ؼ� �и��Ѵ�.
        // �׷��� �����ΰ� UI�� ��� "�� ��Ģ�� �����ϴ�?" ������ȴ�.
        // ĸ��ȭ�� ��Ģ : ��(Specification)

        // �̸��� ����
        var emailSpecification = new AccountEmailSpecification();
        if(!emailSpecification.IsSpecificationBy(email))
        {
            throw new Exception(emailSpecification.ErrorMessage);
        }

        // �г��� ����
        var nicknameSpecification = new AccountNickNameSpedcification();
        if (!nicknameSpecification.IsSpecificationBy(nickname))
        {
            throw new Exception(nicknameSpecification.ErrorMessage);
        }

        if (string.IsNullOrEmpty(password))
        {
            throw new Exception("��й�ȣ�� ������� �� �����ϴ�.");
        }

        Email = email;
        Nickname = nickname;
        Password = password;
    }

    public AccountDTO ToDTO()
    {
        return new AccountDTO(Email, Nickname, Password);
    }
}
