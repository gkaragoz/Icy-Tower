[System.Serializable]
public class DataRepo {
    public PlayerStats_SO PlayerStatsSO { get; set; }
    public MarketItem_SO[] MarketItemSOs { get; set; }
}

public class PlayFabVCRewardsHandler {
    public int RewardedGold { get; set; }
    public int RewardedGem { get; set; }
    public int RewardedKey { get; set; }
    public bool HasRewardCollected { get; set; }
    public PlayFabVCRewardsHandler() {
        RewardedGold = 0;
        RewardedGem = 0;
        RewardedKey = 0;
        HasRewardCollected = false;
    }

    public void CheckRewards() {
        if (Account.instance.RewardsVCRepo.HasRewardCollected) {
            UIManager.instance.OpenPopup("Hurraay!", "Ödül aldık lowğğ " + Account.instance.RewardsVCRepo.RewardedGold + "/" + Account.instance.RewardsVCRepo.RewardedGem + "/" + Account.instance.RewardsVCRepo.RewardedKey);

            AddToAccount();
        }
    }
    private void AddToAccount() {
        Account.instance.AddVirtualCurrency(RewardedGold, VirtualCurrency.Gold);
        Account.instance.AddVirtualCurrency(RewardedGem, VirtualCurrency.Gem);
        Account.instance.AddVirtualCurrency(RewardedKey, VirtualCurrency.Key);

        HasRewardCollected = false;
        RewardedGold = 0;
        RewardedGem = 0;
        RewardedKey = 0;
    }

}