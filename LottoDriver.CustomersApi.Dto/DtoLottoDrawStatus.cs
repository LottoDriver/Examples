namespace LottoDriver.CustomersApi.Dto
{
    /// <summary>
    /// Known lotto draw statuses in this version of the API
    /// </summary>
    public enum DtoLottoDrawStatus
    {
        /// <summary>
        /// This status should only be possible if an older API SDK is
        /// contacting a newer version of the Customers API.
        /// It should be handled as <see cref="UndoCleared"/>.
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// Before the results are known, a lotto draw in in the
        /// published status. It does not necessarily mean that
        /// bets should be allowed when in this state.
        ///
        /// Betting companies should have their own time until
        /// the betting is valid, which should be before the scheduled
        /// and before the actual draw time (if they differ).
        /// </summary>
        Published = 0,

        /// <summary>
        /// A a lotto draw will be in this state when a result is not yet known
        /// but the LottoDriver detected a discrepancy in the scheduled time.
        /// Bets should not be allowed while a draw is in this state.
        /// </summary>
        Unpublished = 1,

        /// <summary>
        /// A lotto draw is closed with the known result. Bets can be resulted
        /// (processed as winning or losing) based on the result.
        ///
        /// The resulting should take into the account the actual draw time, 
        /// void any bets purchased after the actual draw time.
        /// </summary>
        Cleared = 2,

        /// <summary>
        /// A draw was cleared, but a problem has been detected after that.
        /// Betting on the draw should not be allowed.
        ///
        /// This essentially means that the bets have already been resulted (marked as winning or losing).
        /// In that case, bets should be reversed to "un-resulted" state if possible.
        /// If reversing is not possible, all pay outs of the winning bets should be blocked
        /// until the status changes to either Cleared or Canceled.
        /// </summary>
        UndoCleared = 3,

        /// <summary>
        /// This status signifies that the lotto draw was cancelled and the bets should be
        /// voided (purchase money returned to the player).
        /// </summary>
        Canceled = 4
    }
}