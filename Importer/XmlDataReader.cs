using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Xml;

namespace RutrackerImport
{
    public class XmlDataReader : DbDataReader
    {
        private readonly string _path;
        private readonly IEnumerator<TorrentRow> _raptor;

        public XmlDataReader(string path)
        {
            _path = path;
            _raptor = EnumerateTorrents().GetEnumerator();
        }

        public override int FieldCount => 9;

        public IEnumerable<TorrentRow> EnumerateTorrents()
        {
            using (var stream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.None, 8192))
            {
                var opt = new XmlReaderSettings();
                opt.IgnoreWhitespace = true;
                opt.ConformanceLevel = ConformanceLevel.Fragment;
                opt.CheckCharacters = false;
                opt.IgnoreComments = true;
                opt.IgnoreProcessingInstructions = true;
                opt.DtdProcessing = DtdProcessing.Ignore;

                using (var reader = XmlReader.Create(stream, opt))
                {
                    reader.MoveToContent();

                    TorrentRow row = null;

                    bool inTorrent = false;
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                switch (reader.Name)
                                {   
                                    case "torrent":
                                        if (inTorrent)
                                        {
                                            row.Hash = reader.GetAttribute(0);
                                            row.TrackerId = reader.GetAttribute(1);
                                            inTorrent = false;
                                        }
                                        else
                                        {
                                            row = new TorrentRow();
                                            row.Id = int.Parse(reader.GetAttribute(0));
                                            row.Date = DateTime.Parse(reader.GetAttribute(1));
                                            row.Size = long.Parse(reader.GetAttribute(2));
                                            inTorrent = true;
                                        }
                                        break;
                                    case "title":
                                        reader.ReadStartElement();
                                        row.Title = reader.Value;
                                        reader.Read();
                                        break;
                                    //case "magnet":
                                    //    reader.ReadStartElement();
                                    //    row.Magnet = reader.Value;
                                    //    reader.Read();
                                    //    break;
                                    case "forum":
                                        row.ForumId = int.Parse(reader.GetAttribute(0));
                                        reader.ReadStartElement();
                                        row.ForumTitle = reader.Value;
                                        reader.Read();
                                        break;
                                    case "content":
                                        reader.ReadStartElement();
                                        //using (var mem = new MemoryStream(reader.Value.Length))
                                        //{
                                        //    using (var zipper = new GZipStream(mem, CompressionLevel.Optimal))
                                        //    {
                                        //        var buff = Encoding.UTF8.GetBytes(reader.Value);
                                        //        zipper.Write(buff, 0, buff.Length);
                                        //    }
                                        //    row.Content = mem.ToArray();
                                        //}
                                        row.Content = reader.Value;
                                        reader.Read();
                                        break;
                                }

                                break;
                            case XmlNodeType.EndElement:
                                if(row == null)
                                    yield break;
                                yield return row;
                                row = null;
                                break;
                        }}
                }
            }
        }

        public override bool Read()
        {
            return _raptor.MoveNext();
        }

        public override object GetValue(int i)
        {
            switch (i)
            {
                case 0:
                    return _raptor.Current.Id;
                case 1:
                    return _raptor.Current.Date;
                case 2:
                    return _raptor.Current.Size;
                case 3:
                    return _raptor.Current.Title;
                case 4:
                    return _raptor.Current.ForumId;
                case 5:
                    return _raptor.Current.ForumTitle;
                case 6:
                    return _raptor.Current.Hash;
                case 7:
                    return _raptor.Current.TrackerId;
                case 8:
                    return _raptor.Current.Content;
            }
            throw new InvalidOperationException("Unexpected column ordinal index");
        }

        protected override void Dispose(bool disposing)
        {
            _raptor.Dispose();
            base.Dispose(disposing);
        }


        public override bool IsDBNull(int ordinal)
        {
            return GetValue(ordinal) == null;
        }

        #region NotSupportedException
        public override int GetOrdinal(string name)
        {
            switch (name)
            {
                case nameof(_raptor.Current.Id):
                    return 0;
                case nameof(_raptor.Current.Date):
                    return 1;
                case nameof(_raptor.Current.Size):
                    return 2;
                case nameof(_raptor.Current.Title):
                    return 3;
                case nameof(_raptor.Current.ForumId):
                    return 4;
                case nameof(_raptor.Current.ForumTitle):
                    return 5;
                case nameof(_raptor.Current.Hash):
                    return 6;
                case nameof(_raptor.Current.TrackerId):
                    return 7;
                case nameof(_raptor.Current.Content):
                    return 8;
            }
            throw new InvalidOperationException("Unexpected column ordinal index");
        }

        public override int GetValues(object[] values)
        {
            throw new NotSupportedException();
        }

        public override int Depth
        {
            get { throw new NotSupportedException(); }
        }

        public override bool GetBoolean(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override byte GetByte(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            var data = (byte[]) GetValue(ordinal);
            var t = Math.Min(data.Length - dataOffset, length);
            Buffer.BlockCopy(data, (int) dataOffset, buffer, bufferOffset, (int) t);
            return t;
        }

        public override char GetChar(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            throw new NotSupportedException();
        }

        public override string GetDataTypeName(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override DateTime GetDateTime(int ordinal)
        {
            return (DateTime) GetValue(ordinal);
        }

        public override decimal GetDecimal(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override double GetDouble(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override int GetInt32(int ordinal)
        {
            return (int) GetValue(ordinal);
        }

        public override long GetInt64(int ordinal)
        {
            return (long) GetValue(ordinal);
        }

        public override string GetName(int ordinal)
        {
            throw new NotSupportedException();
        }


        public override string GetString(int ordinal)
        {
            return (string) GetValue(ordinal);
        }

        public override object this[string name]
        {
            get { throw new NotSupportedException(); }
        }

        public override object this[int ordinal]
        {
            get { throw new NotSupportedException(); }
        }

        public override bool HasRows
        {
            get { throw new NotSupportedException(); }
        }

        public override bool IsClosed
        {
            get { throw new NotSupportedException(); }
        }

        public override int RecordsAffected
        {
            get { throw new NotSupportedException(); }
        }


        public override short GetInt16(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override Guid GetGuid(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override float GetFloat(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override Type GetFieldType(int ordinal)
        {
            throw new NotSupportedException();
        }

        public override bool NextResult()
        {
            throw new NotSupportedException();
        }

        public override IEnumerator GetEnumerator()
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}