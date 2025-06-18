using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class UI_Ranking : MonoBehaviour
{
    public List<UI_RankingSlot> RankingSlots;
    public UI_RankingSlot MyRankingSlot;

    private void Start()
    {
        Refresh();

        RankingManager.Instance.OnDataChanged += Refresh;
    }

    public void Refresh()
    {
        var ranking = RankingManager.Instance.Rankings;

        int index = 0;
        foreach(var ui_ranking in RankingSlots)
        {
            ui_ranking.Refresh(ranking[index]);
            index++;
        }

        MyRankingSlot.Refresh(RankingManager.Instance.myRanking);
    }
}
