using System.Data;

namespace LottoDriver.Examples.CustomersApi.Common.DataAccess
{
    public interface IDatabase
    {
        void BeginTransaction();
        void RollbackTransaction();
        void CommitTransaction();

        int GetLastSeqNo();
        void SetLastSeqNo(int lastSeqNo);

        Country CountryFindByLottoDriverId(string lottoDriverId);
        void CountryInsert(Country c);

        Lotto LottoFindByLottoDriverId(int lottoDriverLottoId);
        void LottoInsert(Lotto lotto);

        LottoDraw LottoDrawFindByLottoDriverId(long lottoDriverDrawId);
        void LottoDrawInsert(LottoDraw draw);
        int LottoDrawUpdate(LottoDraw draw);

        void LottoDrawFindRecent(DataTable dataTable);

        void UpgradeDb();
    }
}