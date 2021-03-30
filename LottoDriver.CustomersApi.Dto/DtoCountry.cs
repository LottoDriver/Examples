using System.Collections.Generic;

namespace LottoDriver.CustomersApi.Dto
{
    /// <summary>
    /// Country where the lottery events are taking place
    /// </summary>
    public class DtoCountry
    {
        /// <summary>
        /// Country ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Country name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Lotteries in this country. This list will only contain lotteries
        /// with some data changes. Unchanged lotteries will not be included.
        /// </summary>
        public List<DtoLotto> Lotteries { get; set; }
    }
}
