namespace FantasyHelper.Common.DataProvider.Helpers
{
    public static class PropertyHelpers
    {
        /// <summary>
        /// FPL is returning the price as an integer so we need to divide it to get the correct price
        /// </summary>
        public static decimal FormatPlayerPrice(this int price) => Convert.ToDecimal(price) / 10;
    }
}
