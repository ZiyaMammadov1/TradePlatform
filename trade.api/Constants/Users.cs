using trade.api.Models.Entities;

namespace trade.api.Constants;
public static class Resource
{
    public static List<User> Users = new List<User>()
    {
        new User() { UserName = "Elizabeth Taylor", Password = "fb306cd84d82e4e5ee44284c154c9b0f", Deposit = 7452 }, //money_is_the_best_deodorant
        new User() { UserName = "Steffi Graf", Password = "4f7e102f0465db0df8d3f3c256ebce31", Deposit = 6954 },      //how_much_money_do_you_have_?
        new User() { UserName = "Bruce Kovner", Password = "3e41e454b23aaa8eaded69b995ba4cca", Deposit = 5152 },     //if_you_personalize_losses_you_can't_trade
    };
}