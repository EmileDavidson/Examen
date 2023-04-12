namespace Runtime.Customer
{
    /// <summary>
    /// All states the customer can be in
    /// </summary>
    public enum CustomerState
    {
        Spawned,
        WalkingToEntrance,
        WalkingToProducts,
        GettingProducts,
        WalkingToCheckout,
        DroppingProducts,
        FinishingShopping,
        WalkingToExit,
    }
}