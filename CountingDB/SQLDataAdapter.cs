using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingDB
{
    public partial class sqlDataAdapter
    {

        System.Data.SqlClient.SqlTransaction _transaction;

        public void EnlistTransaction(System.Data.SqlClient.SqlTransaction transaction)
        {
            //System.Data.SqlClient.SqlTransaction _transaction;

            if (this._transaction != null)
            {
                throw new System.InvalidOperationException
            ("This adapter has already been enlisted in a transaction");
            }
            else
            {
                this._transaction = transaction;
                //sqlDataAdapter.UpdateCommand.Transaction = _transaction;
                //sqlDataAdapter.InsertCommand.Transaction = _transaction;
                //sqlDataAdapter.DeleteCommand.Transaction = _transaction;
            }
        }
    }
}
