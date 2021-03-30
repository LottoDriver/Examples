namespace LottoDriver.CustomersApi.Dto
{
    /// <summary>
    /// Response structure of the token endpoint
    /// </summary>
    public class TokenResponse
    {
        // ReSharper disable InconsistentNaming
        /// <summary>
        /// Access token to be used in all calls to Customers API
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// Token type is always set to "bearer"
        /// </summary>
        public string token_type { get; set; }

        /// <summary>
        /// Token will expire in specified number of seconds
        /// </summary>
        public int expires_in { get; set; }
        // ReSharper restore InconsistentNaming
    }
}