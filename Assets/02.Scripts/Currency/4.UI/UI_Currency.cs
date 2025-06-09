using TMPro;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;

public class UI_Currency : MonoBehaviour
{
    public TextMeshProUGUI GoldCountText;
    public TextMeshProUGUI DiamondCountText;
    public TextMeshProUGUI BuyHelathText;

    private void Start()
    {
        Refresh();
        CurrencyManager.Instance.OnDataChanged += Refresh;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            BuyHealth();
        }
    }

    private void Refresh()
    {
        var gold = CurrencyManager.Instance.Get(ECurrencyType.Gold);
        var Diamond = CurrencyManager.Instance.Get(ECurrencyType.Diamond);

        GoldCountText.text = $"Gold : {gold.Value}";
        DiamondCountText.text = $"Diamond : {Diamond.Value}";

        BuyHelathText.color = gold.HaveEnought(300) ? Color.green : Color.red;
    }

    public void BuyHealth()
    {
        if (CurrencyManager.Instance.TryBuy(ECurrencyType.Gold, 300))
        {
            var Player = FindFirstObjectByType<PlayerCharacterController>();
            Health playerHelath = Player.GetComponent<Health>();
            playerHelath.Heal(100);
        }
    }

    private void Test()
    {
        // DTO가 없을때 문제를 DTO를 통해 해결
        //CurrencyDTO diamond = CurrencyManager.Instance.Get(ECurrencyType.Diamond);
        //diamond.Add(2000); 
    }
}
