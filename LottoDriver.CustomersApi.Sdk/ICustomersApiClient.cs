namespace LottoDriver.CustomersApi.Sdk
{
    /// <summary>
    /// Represents the LottoDriver Customers API client.
    /// </summary>
    public interface ICustomersApiClient
    {
        /// <summary>
        /// CallbackError event is raised when an unhandled exception is raised in
        /// one of the other events. Users of the API do NOT have to perform any
        /// recovery procedure. This is used mainly for logging the errors.
        /// </summary>
        event ErrorHandler CallbackError;

        /// <summary>
        /// DataReceived event is raised when LottoDriver sends new data.
        /// </summary>
        event DataReceivedHandler DataReceived;

        /// <summary>
        /// Error event is raised when the API client experiences an internal error
        /// (server unavailable etc.). Users of the API do NOT have to perform any
        /// recovery procedure. This is used mainly for logging the errors.
        /// </summary>
        event ErrorHandler Error;

        /// <summary>
        /// Starts the connection to the LottoDriver API. The client will retry
        /// connecting even in case of errors so users of the API do not have to
        /// perform any recovery procedures. Usually this should be called once only,
        /// on application start.
        /// <param name="lastSeqNo">Last sequence as seen in the last DataReceived event in "To" property of the data object.</param>
        /// </summary>
        void Connect(int lastSeqNo);

        /// <summary>
        /// Stops communication with the LottoDriver API.
        /// Usually this should be called once only, on application end.
        /// </summary>
        void Disconnect();
    }
}