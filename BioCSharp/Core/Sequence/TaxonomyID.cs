using System;
using System.Collections.Generic;
using System.Text;

namespace BioCSharp.Core.Sequence
{
    class TaxonomyID
    {

        private string _id = null;
        private DataSource _dataSource = DataSource.UNKNOWN;

        public TaxonomyID(string id, DataSource dataSource)
        {

            _id = id;
            _dataSource = dataSource;

        }

        public string GetID()
        {
            return _id;
        }

        public DataSource GetDataSource()
        {
            return _dataSource;
        }

    }
}
