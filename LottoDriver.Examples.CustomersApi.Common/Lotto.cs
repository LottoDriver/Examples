namespace LottoDriver.Examples.CustomersApi.Common
{
    public class Lotto
    {
        /// <summary>
        /// Betting company's identifier of the lottery which
        /// can be unrelated to LottoDriver's identifiers.
        ///
        /// A betting company should have their own lotto and lotto draw identifiers,
        /// which may be simple autoincrement fields (as shown in this example).
        /// That way, a betting company can have lotteries and lotto draws
        /// that aren't connected to LottoDriver's feed.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Betting company's identifier of the country.
        /// This is not LottoDriver's id.
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Lottery name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Total number of balls in the lottery "drum".
        /// </summary>
        public int NumbersTotal { get; set; }

        /// <summary>
        /// Number of balls drawn on each drawing event.
        /// </summary>
        public int NumbersDrawn { get; set; }

        /// <summary>
        /// This is LottoDriver's identifier of the Lottery.
        /// It is set as nullable here to show that a betting companies can
        /// have lotteries and lotto draws that aren't connected to LottoDriver.
        /// </summary>
        public int? LottoDriverLottoId { get; set; }
    }
}
