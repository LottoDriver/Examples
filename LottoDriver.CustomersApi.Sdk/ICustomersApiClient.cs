using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using LottoDriver.CustomersApi.Dto;

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
        /// Clients should handle this event OR <see cref="DrawsReceived"/> event. No need to handle both.
        /// The only difference between these two events is that this event provides a hierarchical view of the data,
        /// while <see cref="DrawsReceived"/> provides a list of draws.
        /// </summary>
        event DataReceivedHandler DataReceived;

        /// <summary>
        /// DrawsReceived event is raised when LottoDriver sends the list of changed draws.
        /// Clients should handle this event OR <see cref="DataReceived"/> event. No need to handle both.
        /// The only difference between these two events is that this event provides a list of draws
        /// while <see cref="DataReceived"/> provides a hierarchical view of the data.
        /// </summary>
        event DrawsReceivedHandler DrawsReceived;

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

        /// <summary>
        /// Returns all lotteries available in the LottoDriver system
        /// </summary>
        /// <returns></returns>
        Task<List<DtoLotto>> GetLotteriesAsync();

        /// <summary>
        /// Returns draws for a single lottery in the specified period (max. 31 days).
        /// If the date/time values are of Local or Unspecified kind, they will be converted to Utc.
        /// If the date/time values are of Utc kind, they will be used without any conversion.
        /// </summary>
        /// <param name="lottoId">Id of the lottery</param>
        /// <param name="dateFrom">Starting date/time, inclusive</param>
        /// <param name="dateTo">End date/time, exclusive</param>
        /// <returns></returns>
        Task<List<DtoLottoDraw>> GetDrawsAsync(int lottoId, DateTime dateFrom, DateTime dateTo);

        /// <summary>
        /// Returns draws for a single lottery in the specified day.
        /// Provided timezone will be used to determine start/end utc time.
        /// If the timezone is not provided, TimeZoneInfo.Local will be assumed.
        /// </summary>
        /// <param name="lottoId">Id of the lottery</param>
        /// <param name="day">Day for which the draws should be returned. Time component will be ignored</param>
        /// <param name="timeZoneInfo">Timezone which will be used to determine the correct utc period</param>
        /// <returns></returns>
        Task<List<DtoLottoDraw>> GetDrawsAsync(int lottoId, DateTime day, TimeZoneInfo timeZoneInfo = null);

        /// <summary>
        /// Returns a single lotto draw by the specified draw id.
        /// </summary>
        /// <param name="drawId"></param>
        /// <returns></returns>
        Task<DtoLottoDraw> GetDrawAsync(long drawId);
    }
}