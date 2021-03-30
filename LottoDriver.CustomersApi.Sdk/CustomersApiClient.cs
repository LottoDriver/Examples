using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json;

using LottoDriver.CustomersApi.Dto;

namespace LottoDriver.CustomersApi.Sdk
{
    /// <summary>
    /// Delegate handler for DataReceived event.
    /// </summary>
    /// <param name="source">The instance that fired the event</param>
    /// <param name="data">Data received by the LottoDriver server (changed data)</param>
    /// <returns></returns>
    public delegate bool DataReceivedHandler(ICustomersApiClient source, DtoLotteriesResponse data);

    /// <summary>
    /// Delegate handler for <see cref="ICustomersApiClient.Error" /> and <see cref="ICustomersApiClient.CallbackError"/> events.
    /// </summary>
    /// <param name="source">The instance that fired the event</param>
    /// <param name="exception">Exception that was raised</param>
    public delegate void ErrorHandler(ICustomersApiClient source, Exception exception);


    /// <inheritdoc />
    public class CustomersApiClient : ICustomersApiClient
    {
        // ReSharper disable NotAccessedField.Local
        private readonly string _clientId;
        private readonly string _clientSecret;
        // ReSharper restore NotAccessedField.Local

        private readonly Timer _timer;
        private readonly HttpClient _httpClient;

        private int _lastSeqNo;
        private bool _connected;
        private DateTime _lastPollTime = DateTime.MinValue;
        private DateTime _tokenExpiresAt = DateTime.MinValue;

        /// <inheritdoc />
        public event DataReceivedHandler DataReceived;

        /// <inheritdoc />
        public event ErrorHandler CallbackError;

        /// <inheritdoc />
        public event ErrorHandler Error;

        /// <summary>
        /// Creates an instance of the LottoDriver Customers API Client.
        /// The client handles reconnect and recovery automatically, so "Connect" should only be called once,
        /// on application start.
        /// </summary>
        /// <param name="apiUrl">LottoDriver Customers API Url</param>
        /// <param name="clientId">Betting company client id</param>
        /// <param name="clientSecret">A secret string assigned to each client id to authenticate the client</param>
        public CustomersApiClient(string apiUrl = "https://api.lottodriver.com/", string clientId = "", string clientSecret = "")
        {
            _clientId = clientId;
            _clientSecret = clientSecret;

            _httpClient = new HttpClient { BaseAddress = new Uri(apiUrl) };

            _timer = new Timer(TimerElapsed);
            _timer.Change(1000, Timeout.Infinite);
        }

        /// <inheritdoc />
        public void Connect(int lastSeqNo)
        {
            _lastSeqNo = lastSeqNo;
            _connected = true;
        }

        /// <inheritdoc />
        public void Disconnect()
        {
            _connected = false;
        }

        private void TimerElapsed(object state)
        {
            try
            {
                if (!_connected) return;

                var utcNow = DateTime.UtcNow;
                if (_lastPollTime.AddSeconds(15) > utcNow) return;
                _lastPollTime = utcNow;

                var data = GetLotteries().Result;

                if (OnDataReceived(data))
                {
                    _lastSeqNo = data.To;

                    // this is to speed up catching up
                    // if the client was offline for a long time
                    if (data.To - data.From > 500)
                    {
                        // this will force a call to the backend API in the next timer callback
                        _lastPollTime = DateTime.MinValue;
                    }
                }
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
            finally
            {
                _timer.Change(1000, Timeout.Infinite);
            }
        }

        private async Task<DtoLotteriesResponse> GetLotteries()
        {
            if (!IsTokenValid())
            {
                await Authenticate();
            }

            using (var response = await _httpClient.GetAsync($"lotteries?lastSeqNo={_lastSeqNo}"))
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ResetToken();
                }

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<DtoLotteriesResponse>(json);
            }
        }

        private async Task Authenticate()
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", _clientId),
                new KeyValuePair<string, string>("client_secret", _clientSecret),
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            });

            using (var response = await _httpClient.PostAsync("token", content))
            {
                response.EnsureSuccessStatusCode();

                var tokenResponseJson = await response.Content.ReadAsStringAsync();

                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(tokenResponseJson);

                if (string.IsNullOrWhiteSpace(tokenResponse.access_token))
                {
                    ResetToken();
                    throw new Exception("Invalid token");
                }

                _tokenExpiresAt = tokenResponse.expires_in > 0
                    ? DateTime.UtcNow.AddSeconds(tokenResponse.expires_in)
                    : DateTime.UtcNow.AddHours(24);

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.access_token);
            }
        }

        private void ResetToken()
        {
            _tokenExpiresAt = DateTime.MinValue;
        }

        private bool IsTokenValid()
        {
            return _tokenExpiresAt > DateTime.UtcNow;
        }

        private bool OnDataReceived(DtoLotteriesResponse data)
        {
            try
            {
                if (data.Countries.Count == 0 && data.From == data.To) return false;

                return DataReceived?.Invoke(this, data) ?? false;
            }
            catch (Exception ex)
            {
                OnCallbackError(ex);
                return false;
            }
        }

        private void OnError(Exception exception)
        {
            try
            {
                Error?.Invoke(this, exception);
            }
            catch (Exception ex)
            {
                OnCallbackError(ex);
            }
        }

        private void OnCallbackError(Exception exception)
        {
            try
            {
                CallbackError?.Invoke(this, exception);
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch
            {
            }
        }
    }
}
