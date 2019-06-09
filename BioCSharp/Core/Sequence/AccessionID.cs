using BioCSharp.Core.Util;

namespace BioCSharp.Core.Sequence
{
    public class AccessionID
    {

        private string _id = null;
        private DataSource _source = DataSource.LOCAL;
        private int _version;
        private string _identifier;

        public AccessionID()
        {
            _id = "";
        }

        public AccessionID(string id)
        {

            _id = id.Trim();
            _source = DataSource.LOCAL;

        }

        public AccessionID(string id, DataSource source, int version, string identifier)
        {

            _id = id;
            _source = source;
            _version = version;
            _identifier = identifier;

        }

        public string GetId()
        {
            return _id;
        }

        public DataSource GetDataSource()
        {
            return _source;
        }

        public int GetVersion()
        {
            return _version;
        }

        public void SetVersion(int version)
        {
            _version = version;
        }

        public string GetIdentifier()
        {
            return _identifier;
        }

        public void SetIdentifier(string identifier)
        {
            _identifier = identifier;
        }

        public override bool Equals(object obj)
        {

            bool equals = false;
            if (obj is AccessionID l)
            {

                equals = GetId() == l.GetId() && 
                         GetDataSource() == l.GetDataSource() &&
                         GetIdentifier() == l.GetIdentifier() && 
                         GetVersion() == l.GetVersion();

            }

            return equals;

        }

        public override int GetHashCode()
        {

            int r = HashCoder.Seed;
            r = HashCoder.Hash(r, GetId());
            r = HashCoder.Hash(r, GetDataSource());
            r = HashCoder.Hash(r, GetIdentifier());
            r = HashCoder.Hash(r, GetVersion());
            return r;

        }

        public override string ToString()
        {
            return _id;
        }
    }

}
