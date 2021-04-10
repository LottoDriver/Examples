using System.Collections.Generic;

namespace LottoDriver.CustomersApi.Dto
{
    /// <summary>
    /// Describes a lottery type.
    /// One lottery type can have many draws (once a week, or once a day, or multiple times a day)
    /// </summary>
    public class DtoLotto
    {
        /// <summary>
        /// Id of the lottery
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Country where this lotto is being organized
        /// </summary>
        public DtoCountry Country { get; private set; }

        /// <summary>
        /// Name of the lottery (english).
        /// Betting companies should handle the translations themselves,
        /// since each company may have different constraints in length, character encoding etc.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Total number of balls in the lottery "drum".
        /// (eg. in 'Italy 10e Lotto - 20/90' this property will be set to 90)
        /// </summary>
        public int NumbersTotal { get; set; }

        /// <summary>
        /// Number of balls drawn on each lottery draw.
        /// (eg. in 'Italy 10e Lotto - 20/90' this property will be set to 20)
        /// </summary>
        public int NumbersDrawn { get; set; }

        /// <summary>
        /// List of draws for this lottery that have some changes.
        /// </summary>
        public List<DtoLottoDraw> Draws { get; set; }

        internal void SetCountry(DtoCountry country)
        {
            Country = country;
        }
    }
}
