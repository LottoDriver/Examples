namespace LottoDriver.Examples.CustomersApi.Common
{
    public class Country
    {
        /// <summary>
        /// Betting company's identifier of the country which
        /// can be unrelated to LottoDriver's identifiers.
        ///
        /// A betting company should have their own country, lotto and lotto draw identifiers,
        /// which may be simple autoincrement fields (as shown in this example).
        /// That way, a betting company can have lotteries and lotto draws
        /// that aren't connected to LottoDriver's feed.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the country
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// LottoDriver's country identifier
        /// </summary>
        public string LottoDriverCountryId { get; set; }
    }

}
