//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using MillionaireGame.DAL.Entities;

//namespace MillionaireGame.DAL.Repository
//{
//    public class RecordRepository: IRecordRepository
//    {
//        private readonly MillionaireContext _context;
//        private bool _disposed;

//        public RecordRepository(MillionaireContext context)
//        {
//            _context = context;
//        }

//        public void Dispose(bool disposing)
//        {
//            if (!_disposed)
//            {
//                if (disposing)
//                {
//                    _context.Dispose();
//                }
//            }
//            _disposed = true;
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//        public IEnumerable<Record> GetRecords()
//        {
//            return _context.Records;
//        }

//        public Record GetRecordById(int recordId)
//        {
//            return _context.Records.Find(recordId);
//        }

//        public void InsertRecord(Record record)
//        {
//            _context.Records.Add(record);
//        }

//        public void DeleteRecord(int recordId)
//        {
//            Record recordToDelete = _context.Records.Find(recordId);
//            if (recordToDelete != null)
//            {
//                _context.Records.Remove(recordToDelete);
//            }
//        }

//        public void UpdateRecord(Record record)
//        {
//            _context.Entry(record).State = EntityState.Modified;
//        }

//        public void Save()
//        {
//            _context.SaveChanges();
//        }
//    }
//}
