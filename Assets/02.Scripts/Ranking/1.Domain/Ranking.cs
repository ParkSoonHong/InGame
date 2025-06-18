using System;
using DG.Tweening;
using UnityEngine;

public class Ranking
{
    public int Rank { get; private set; }
    public readonly string Email;
    public readonly string Nickname;
    public int Score { get; private set; }

    public Ranking(string email, string nickname, int score)
    {
        var emailSpecification = new AccountEmailSpecification();
        //if (!emailSpecification.IsSatisfiedBy(email))
        //{
        //    Debug.Log("�̸��� ����");
        //    throw new Exception(emailSpecification.ErrorMessage);
        //}

        //var nickNameSpecification = new AccountNickNameSpedcification();
        //if (!nickNameSpecification.IsSpecificationBy(nickname))
        //{
        //    Debug.Log("�г��� ����");
        //    throw new Exception(nickNameSpecification.ErrorMessage);
        //}

        //if (score < 0)
        //{
        //    throw new Exception("�ùٸ��� ���� �����Դϴ�.");
        //}

        Email = email;
        Nickname = nickname;
        Score = score;
    }

    public void SetRank(int rank)
    {
        if (rank <= 0)
        {
            throw new Exception("�ùٸ��� ���� ����Դϴ�.");
        }

        Rank = rank;
    }

    public void AddScore(int score)
    {
        if (score <= 0)
        {
            throw new Exception("�ùٸ��� ���� �����Դϴ�.");
        }

        Score += score;
    }

    public RankingDTO ToDTO()
    {
        return new RankingDTO(this);
    }
}