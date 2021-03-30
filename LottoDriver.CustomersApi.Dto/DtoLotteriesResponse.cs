using System.Collections.Generic;

namespace LottoDriver.CustomersApi.Dto
{
    /// <summary>
    /// Data object containing lotto draws that have changes
    /// between <see cref="From"/> and <see cref="To"/> sequence numbers.
    /// </summary>
    public class DtoLotteriesResponse
    {
        /// <summary>
        /// Sequential number specifying the change starting point.
        /// Normally, the clients of the API do not have not use this property.
        /// It is mostly informational.
        /// </summary>
        public int From { get; set; }

        /// <summary>
        /// This last sequence number in the data change message.
        /// It is very important to permanently store this number on every
        /// data change.
        ///
        /// It can happen that the "To" sequence number changes but the
        /// <see cref="Countries"/> list is empty (because of a change irrelevant to clients).
        /// </summary>
        public int To { get; set; }

        /// <summary>
        /// List of countries with lotto draws that have changes
        /// between <see cref="From"/> and <see cref="To"/> sequence numbers.
        /// </summary>
        public List<DtoCountry> Countries { get; set; }
    }
}
